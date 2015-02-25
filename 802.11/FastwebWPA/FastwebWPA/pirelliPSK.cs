using System;
using System.Collections.Generic;
using System.Text;

namespace FastwebWPA
{
    using System.Security.Cryptography;

    class pirelliPSK
    {
        public static string CreatePirelliPSK(string mac)
        {
            FastwebWPA.utils u = new FastwebWPA.utils();    // useful utils! see utils.cs

            Byte[] macAddress = u.strToHex(mac);            // convert a string to a hexdump (eg: 22 -> 0x22 and not 0x16)
            Byte[] magic = {                                // found @unk_100220D0
                		0x22, 0x33, 0x11, 0x34, 0x02,
		                0x81, 0xFA, 0x22, 0x11, 0x41,
		                0x68, 0x11, 0x12, 0x01, 0x05,
		                0x22, 0x71, 0x42, 0x10, 0x66
            };

            UInt32 bin = 0;                                 // will hold the binarized hash to shift and mod
            Byte[] psk = new Byte[5];                       // 10 chars

            List<Byte> b = new List<Byte>();                // we use a list
            b.AddRange(macAddress);                         // to concatenate
            b.AddRange(magic);                              // mac and magic

            MD5 md5 = MD5.Create();                         // initialize md5() routines
            Byte[] hash = md5.ComputeHash(b.ToArray());     // make the list into an array and hash it

            for (SByte i = 0; i < 3; i++)                   // make couples of four
                bin = (bin | hash[i]) << 8;                 // and shift
            bin = (bin | hash[3]) >> 7;                     // take 25 bits

            for (SByte i = 4; i >= 0; i--)
            {
                uint a = bin & 0x1F;                        // take 5 bits
                psk[i] = (byte)a;
                bin >>= 5;                                  // and get the 5 next
            }

            for (SByte i = 0; i < 5; i++)
                if (psk[i] >= 0x0A)                         // if byte is equal or greater than 10
                    psk[i] += 0x57;                         // sum 87 (to get a valid ascii value)

            StringBuilder sb = new StringBuilder();         // stringbuilder constructor
            for (SByte i = 0; i < psk.Length; i++)          // form a string from bytes
                sb.Append(psk[i].ToString("x2"));

            return sb.ToString();                           // psk
        }
    }
}
