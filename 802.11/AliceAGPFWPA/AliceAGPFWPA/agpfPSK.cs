using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using System.Security.Cryptography;
using System.Globalization;
using System.IO;

namespace AliceAGPFWPA
{
    class agpfPSK
    {
        private static string computePSK(string s)
        {
            return "psk";
        }

        private static string computeSn(string s, string b, SByte hi)
        {
            AliceAGPFWPA.utils u = new AliceAGPFWPA.utils();

            List<string[]> plist = new List<string[]>();
            string config = @".\config.txt";
            string essid = s.Substring(6, 8);
            
            if (hi > 0)
                essid = hi + essid;
                        
            try
            {
                using (StreamReader sr = new StreamReader(config))
                {
                    string line;
                    string[] row;

                    while ((line = sr.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        if (essid.StartsWith(row[0]))
                        {
                            if (row[4].Equals("XXXXXX"))
                            {
                                if (!b.Equals("void"))
                                {
                                    row[4] = b.Replace(":", "").Substring(0, 5);
                                }
                            }
                            plist.Add(row);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            // 995,69102,8,99348190,001D8B
            // 0   1     2 3        4
            Byte[] mac = u.strToByteArray((plist[0][4].Remove(5) + u.strToHex(essid)));
            
            return mac.ToString();
        }

        private static string computeMac(string s)
        {
            return "mac";
        }

        private static SByte computeHi(string m)
        {
            AliceAGPFWPA.utils u = new AliceAGPFWPA.utils();
            m = "0" + m.Replace(":", "").Substring(5, 7);
            UInt64 m2 = UInt64.Parse(m, NumberStyles.HexNumber, CultureInfo.CurrentCulture.NumberFormat);
            m2 = (m2 * 0x55E63B89) >> 57;
            return (SByte)m2;
        }

        public static string agpf_PSK(string essid, string bssid)
        {
            AliceAGPFWPA.utils u = new AliceAGPFWPA.utils();

            SByte hi = 0;

            if (!bssid.Equals("void"))
            {
                hi = computeHi(bssid);
            }

            string sn = computeSn(essid, bssid, hi);
            string mac = computeMac(essid);
            string psk = computePSK(sn);

            return psk;
        }
    }
}
