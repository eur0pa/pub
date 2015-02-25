using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AliceAGPF_kQ
{
    public partial class KQ : Form
    {
        public KQ()
        {
            InitializeComponent();
        }

        private void kq_ok_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(essid_input.Text)) || (string.IsNullOrEmpty(serial_input.Text)) ||
                (!essid_input.Text.StartsWith("Alice-")) || (serial_input.Text.Length != 13))
            {
                kq_output.Text = "invalid data";
                return;
            }

            kq_output.Text = kq_compute(essid_input.Text, serial_input.Text);

            return;
        }

        private string kq_compute(string e, string sn)
        {

            UInt32 essid = Convert.ToUInt32(e.Substring(6, 8)), tmp_essid = 0;
            string serie  = sn.Substring(0, 5);
            UInt32 serial = Convert.ToUInt32(sn.Substring(6, 7)), tmp_serial = 0;
            UInt32 Q = 0;
            Byte k = 13;
            string mac = "000000", ret;

            if ((essid > 84549376) && (essid < 99999993))
            {
                k = 13;
                mac = "001D8B";
            } else
            if ((essid > 50331648) && (essid < 67108863))
            {
                k = (byte)(essid % 13 == 0 ? 13 : 8);
                mac = essid % 13 == 0 ? "002233" : "002553";
            } else
            if ((essid > 34881024) && (essid < 51658239))
            {
                k = 13;
                mac = "00238E";
            } else
            if ((essid > 33554432) && (essid < 34881023))
            {
                k = 13;
                mac = "001CA2";
            } else
            if ((essid > 1000005) && (essid < 1326591))
            {
                k = 13;
                mac = "001D8B";
            }

            compute:
            Q = essid - (serial * k);
            tmp_serial = (essid - Q) / k;
            tmp_essid  = (serial * k) + Q;

            if ((tmp_essid == essid) && (tmp_serial == serial) && (((essid - Q) % k) == 0))
            {
                ret = "\"" + (essid / 100000).ToString() + "," + serie + "," + k + "," + Q + "," + mac + "\";";
            }
            else
            {
                k = 8;
                goto compute;
            }

            kq_output.Modified = true;

            return ret;
        }

        private void essid_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                kq_ok_Click(sender, e);
            }
            
            return;
        }

        private void serial_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                kq_ok_Click(sender, e);
            }

            return;
        }

        private void kq_output_MouseClick(object sender, MouseEventArgs e)
        {
            if (!kq_output.Modified)
            {
                if (openfile_dialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader sr = new
                    System.IO.StreamReader(openfile_dialog.FileName);
                    
                    List<string[]> lines = new List<string[]>();
                    List<string> output = new List<string>();

                    try
                    {
                        using (System.IO.StreamReader r = new System.IO.StreamReader(openfile_dialog.FileName))
                        {
                            string line;
                            string[] row;

                            while ((line = r.ReadLine()) != null)
                            {
                                row = line.Split(' ');
                                lines.Add(row);
                            }

                            sr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                    foreach (string[] item in lines)
                    {
                        output.Add(kq_compute(item[0], item[1]));
                    }

                    try
                    {
                        using (System.IO.StreamWriter w = new System.IO.StreamWriter(@".\config.txt"))
                        {
                            foreach (string item in output.Distinct())
                            {
                                w.WriteLine(item);
                            }
                            w.Flush();
                            w.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        kq_output.Text = "config.txt creato con " + output.Count + " elementi";
                    }

                    return;
                }
            }
        }
    }
}
