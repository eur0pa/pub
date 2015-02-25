using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliceAGPFWPA
{
    class utils
    {
        public string byteToStr(Byte[] b)
        {
            System.Text.UTF8Encoding str = new System.Text.UTF8Encoding();
            return str.GetString(b);
        }

        public Byte[] strToByte(string s)
        {
            System.Text.UTF8Encoding bin = new System.Text.UTF8Encoding();
            return bin.GetBytes(s);
        }

        public Byte[] strToByteArray(string s)
        {
            List<Byte> b = new List<Byte>();
            for (Byte i = 0; i < s.Length; i += 2)
                b.Add((Byte)Convert.ToByte(s.Substring(i, 2), 16));

            return b.ToArray();
        }

        public string strToHex(string s)
        {
            UInt32 i = UInt32.Parse(s);
            string h = String.Format("{0:X}", i);
            return h;
        }

        public string validateESSID(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "essid needed";

            if (s.Length != 14)
                return "invalid essid";

            if (String.Compare(s.Substring(0, 6), "Alice-", true) != 0)
                return "not alice";

            return "fine";
        }
    }
}