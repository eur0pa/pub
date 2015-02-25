using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Alice_Tools
{
    class Utils
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

        public string validateBSSID(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "no bssid";
            
            if (s.Length != 17)
                return "invalid bssid";

            return "fine";
        }

        public void sortUniq(string f)
        {
            using (TextReader file = new StreamReader(@f))
            {
                try
                {
                    List<string> lines = new List<string>();
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                    lines.Sort();
                    file.Close();

                    using (TextWriter file_out = new StreamWriter(@f))
                    {
                        try
                        {
                            foreach (string line2 in lines.Distinct())
                            {
                                file_out.WriteLine(line2);
                            }
                        }
                        finally
                        {
                            file_out.Close();
                        }
                    }
                }
                finally
                {

                }
            }
            return;
        }
    }
}
