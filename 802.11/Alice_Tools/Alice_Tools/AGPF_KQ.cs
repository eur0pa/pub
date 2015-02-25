using System;
using System.Collections.Generic;
using System.Linq;

namespace Alice_Tools
{
    class AGPF_KQ
    {
        public static string computeKQ(string e, string s, byte k, bool export)
        {
            UInt32 essid = Convert.ToUInt32(e.Substring(6, 8)), tmp_essid = 0;
            string serie = s.Substring(0, 5);
            UInt32 serial = Convert.ToUInt32(s.Substring(6, 7)), tmp_serial = 0;
            UInt32 Q = 0;
            string mac = "000000", ret;

            if ((essid > 84549376) && (essid < 99999993))
            {
                mac = "001D8B";
            }
            else
                if ((essid > 50331648) && (essid < 67108863))
                {
                    if ((serie.Equals("67902")) || (serie.Equals("69102")))
                    {
                        mac = "002233";
                    }
                    else
                    {
                        mac = "002553";
                    }
                }
                else
                    if ((essid > 34881024) && (essid < 51658239))
                    {
                        mac = "00238E";
                    }
                    else
                        if ((essid > 33554432) && (essid < 34881023))
                        {
                            mac = "001CA2";
                        }
                        else
                            if ((essid > 18103808) && (essid < 34881023))
                            {
                                mac = "00268D";
                            }
                            else
                                if ((essid > 1000005) && (essid < 1326591))
                                {
                                    mac = "001D8B";
                                }

        compute:
            Q = essid - (serial * k);
            tmp_serial = (essid - Q) / k;
            tmp_essid = (serial * k) + Q;

            if ((tmp_essid == essid) && (tmp_serial == serial) && (((essid - Q) % k) == 0))
            {
                if (export == true)
                {
                    ret = "Alice-" + essid.ToString() + "\t"
                        + serie + "X" + serial.ToString().PadLeft(7, '0') + "\t"
                        + k + "\t"
                        + Q + "\t"
                        + mac;
                }
                else
                {
                    ret = (essid / 100000).ToString() + "," + serie + "," + k + "," + Q + "," + mac;
                }
            }
            else
            {
                mac = "002553";
                goto compute;
            }

            return ret;
        }

        public static string openKQList(string f, bool export)
        {
            System.IO.StreamReader sr = new
            System.IO.StreamReader(f);

            List<string[]> lines = new List<string[]>();
            List<string> output2 = new List<string>();
            List<string> output3 = new List<string>();

            try
            {
                using (System.IO.StreamReader r = new System.IO.StreamReader(f))
                {
                    string line;
                    string[] row;

                    while ((line = r.ReadLine()) != null)
                    {
                        row = line.Split(' ');
                        lines.Add(row);
                        output3.Add(line);
                    }

                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            foreach (string[] item in lines.Distinct())
            {
                if (export == true)
                {
                    output2.Add(Alice_Tools.AGPF_KQ.computeKQ(item[0], item[1], 8, true));
                    output2.Add(Alice_Tools.AGPF_KQ.computeKQ(item[0], item[1], 13, true));
                }
                else
                {
                    output2.Add(Alice_Tools.AGPF_KQ.computeKQ(item[0], item[1], 8, false));
                    output2.Add(Alice_Tools.AGPF_KQ.computeKQ(item[0], item[1], 13, false));
                }
            }

            try
            {
                if (export == false)
                {
                    using (System.IO.StreamWriter w = new System.IO.StreamWriter(@".\config.txt", true))
                    {
                        output2.Sort();
                        foreach (string item in output2.Distinct())
                        {
                            w.WriteLine(item);
                        }
                        w.Close();
                    }
                    using (System.IO.StreamWriter w = new System.IO.StreamWriter(@".\list.txt"))
                    {
                        output3.Sort();
                        foreach (string item in output3.Distinct())
                        {
                            w.WriteLine(item);
                        }
                        w.Close();
                    }
                }
                else
                {
                    using (System.IO.StreamWriter w = new System.IO.StreamWriter(@".\database.txt", true))
                    {
                        output2.Sort();
                        foreach (string item in output2.Distinct())
                        {
                            w.WriteLine(item);
                        }
                        w.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return output2.Count().ToString();
        }
    }
}
