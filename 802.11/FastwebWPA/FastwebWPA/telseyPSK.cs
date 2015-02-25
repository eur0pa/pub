using System;
using System.Text;

namespace FastwebWPA
{
    class telseyPSK
    {
        public static string CreateTelseyPSK(string macAddress)
        {
            FastwebWPA.utils u = new FastwebWPA.utils();        // useful utils!
            FastwebWPA.hashword h = new FastwebWPA.hashword();  // fuck this shit man, i've grown old implementing this one

            Byte[] mac = u.strToHex(macAddress);                // convert a string to a hexdump (eg: 22 -> 0x22 and not 0x16)

            Byte[] pTable = new Byte[256] {                     // mac nibbles permutation table
		           6,2,1,6,2,1,2,6,5,3,4,3,5,4,3,3,3,5,3,1,3,6,4,2,1,5,1,2,2,5,2,1,
		           3,5,3,3,4,2,4,5,5,2,5,4,6,2,6,6,3,2,1,6,2,1,2,2,5,3,2,4,4,4,6,3,
		           5,5,6,5,6,2,5,1,3,6,1,6,3,2,4,6,6,3,3,5,3,4,2,5,1,5,5,4,4,1,6,4,
		           5,4,1,1,4,3,2,2,3,2,3,6,2,4,5,4,1,3,4,5,1,1,3,3,1,1,1,6,2,2,2,5,
		           5,1,3,3,4,4,4,1,1,3,5,2,6,6,6,1,1,5,6,1,2,2,6,3,3,3,6,2,4,4,3,4,
		           2,1,3,5,2,6,3,6,1,2,5,1,2,2,2,5,3,3,3,3,4,4,4,4,6,5,1,2,5,1,6,6,
		           2,1,6,1,1,2,6,2,3,3,5,3,4,5,5,4,5,4,2,6,6,6,2,5,4,1,2,6,4,2,1,5,
		           5,3,3,6,5,4,4,2,3,5,4,1,3,4,6,2,4,2,3,4,6,1,2,3,6,4,5,2,1,3,4,1
            };

            UInt32[] rTable = new UInt32[64];                   // avalanche table
            UInt32   s1 = 0, s2 = 0;                            // psk 'nibbles'

            for (SByte i = 0; i < 64; i++)
                for (SByte j = 0; j < 4; j++)
                {
                    rTable[i] <<= 8;                            // grab a bYte!
                    rTable[i] |= mac[pTable[(i * 4) + j] - 1];  // shift it around!
                }

            for (SByte i = 0; i < 64; ++i)
                s1 = h.mash(rTable, (UInt32)i, s1);             // jenkin' the table
            s1 &= 0x000fffff;                                   // grabbin' the last 2.5 bytes

            for (SByte i = 0; i < 64; ++i)                      // generate a new table
                if (i < 8) rTable[i] <<= 3;                     // using the old jenked one
                else if (i < 16) rTable[i] >>= 5;
                else if (i < 32) rTable[i] >>= 2;
                else rTable[i] <<= 7;

            for (SByte i = 0; i < 64; ++i)
                s2 = h.mash(rTable, (UInt32)i, s2);             // jenkin' the fuck out of the table
            s2 &= 0xfffff000;                                   // grabbin' the first 2.5 bytes
            s2 >>= 12;

            StringBuilder psk = new StringBuilder();            // rise, my son!
            psk.Append(s1.ToString("x5"));                      // s1 +
            psk.Append(s2.ToString("x5"));                      // s2 =

            return psk.ToString();                              // psk!
        }
    }
}