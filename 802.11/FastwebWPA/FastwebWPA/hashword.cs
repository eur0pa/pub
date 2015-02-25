using System;

namespace FastwebWPA
{
    public class hashword
    {
        public UInt32 mash(UInt32[] k, UInt32 length, UInt32 initval)
        {
            UInt32 a, b, c;

            a = b = c = 0xdeadbeef + (length << 2) + initval;
            int i = 0;

            while (length > 3)
            {
                a += k[i+0];
                b += k[i+1];
                c += k[i+2];
                a -= c; a ^= ((c <<  4) | (c >> 32 -  4)); c += b;
                b -= a; b ^= ((a <<  6) | (a >> 32 -  6)); a += c;
                c -= b; c ^= ((b <<  8) | (b >> 32 -  8)); b += a;
                a -= c; a ^= ((c << 16) | (c >> 32 - 16)); c += b;
                b -= a; b ^= ((a << 19) | (a >> 32 - 19)); a += c;
                c -= b; c ^= ((b <<  4) | (b >> 32 -  4)); b += a;
                length -= 3;
                i += 3;
            }

            switch (length)
            {
                case 3: c += k[i+2]; goto case 2;
                case 2: b += k[i+1]; goto case 1;
                case 1: a += k[i+0]; break;
                case 0: return c;
            }

            c ^= b; c -= (b << 14) | (b >> 32 - 14);
            a ^= c; a -= (c << 11) | (c >> 32 - 11);
            b ^= a; b -= (a << 25) | (a >> 32 - 25);
            c ^= b; c -= (b << 16) | (b >> 32 - 16);
            a ^= c; a -= (c <<  4) | (c >> 32 -  4);
            b ^= a; b -= (a << 14) | (a >> 32 - 14);
            c ^= b; c -= (b << 24) | (b >> 32 - 24);

            return c;
        }
    }
}
