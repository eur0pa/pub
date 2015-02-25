using System;
using System.IO;
using System.Windows.Forms;

namespace Alice_Tools
{
    public partial class mainForm : Form
    {
        Alice_Tools.Utils U = new Alice_Tools.Utils();

        public mainForm()
        {
            InitializeComponent();
        }

        private void psk_update_button_Click(object sender, EventArgs e)
        {
            output.Text = "";
            string ret = null;

            switch (Alice_Tools.configAutoUpdate.doUpdate())
            {
                case 0: ret = "config.txt updated correctly"; break;
                case 1: ret = "failed to update config.txt"; break;
            }

            output.Text = ret;

            return;
        }

        private void psk_psk_button_Click(object sender, EventArgs e)
        {
            output.Text = "";

            string essid = psk_essid_input.Text;
            string mac = psk_bssid_input.Text;
            string ret = U.validateESSID(essid);

            if (!ret.Equals("fine"))
            {
                output.Text = ret;
                return;
            }

            if (string.IsNullOrEmpty(mac))
                mac = "void";

            string[] psk = AGPF_PSK.getPSK(essid, mac);
            output.Text = string.Join("\r\n", psk);

            return;
        }

        private void kq_button_Click(object sender, EventArgs e)
        {
            output.Text = "";
            if ((string.IsNullOrEmpty(kq_essid_input.Text)) || (string.IsNullOrEmpty(kq_serial_input.Text)) ||
                (!kq_essid_input.Text.StartsWith("Alice-")) || (kq_serial_input.Text.Length != 13))
            {
                output.Text = "invalid data";
                return;
            }

            string ret = Alice_Tools.AGPF_KQ.computeKQ(kq_essid_input.Text, kq_serial_input.Text, 8, false);
            string ret2 = Alice_Tools.AGPF_KQ.computeKQ(kq_essid_input.Text, kq_serial_input.Text, 13, false);

            if (!string.IsNullOrEmpty(ret))
            {
                using (TextWriter tw = new StreamWriter(@"config.txt", true))
                {
                    try
                    {
                        tw.WriteLine(ret + ", <------- NEW");
                        tw.WriteLine(ret2 + ", <------- NEW");
                        tw.Close();
                        U.sortUniq("config.txt");
                    }
                    catch (Exception ex)
                    {
                        ret = ex.Message;
                    }
                }

                using (TextWriter tw = new StreamWriter(@"list.txt", true))
                {
                    try
                    {
                        tw.WriteLine(kq_essid_input.Text + " " + kq_serial_input.Text);
                        tw.Close();
                        U.sortUniq("list.txt");
                    }
                    catch (Exception ex)
                    {
                        ret = ex.Message;
                    }
                }
            }

            output.Text = ret + "\r\nadded to config.txt";

            return;
        }

        private void kq_open_button_Click(object sender, EventArgs e)
        {
            output.Text = "";
            if (kq_openfile_dialog.ShowDialog() == DialogResult.OK)
            {
                string ret = AGPF_KQ.openKQList(kq_openfile_dialog.FileName, false);
                U.sortUniq("config.txt");

                output.Text = "config.txt created from " + ret + " entries.";
            }
            return;
        }

        private void brute_snmac_button_Click(object sender, EventArgs e)
        {
            bool quick = brute_snmac_quick.Checked;
            brute_snmac_button.Text = ". . .";
            output.Text = "finding serial for " + brute_snmac_essid_input.Text + "...";
            brute_snmac_button.Enabled = false;
            string ret = AGPF_Brute.bruteSNMAC(brute_snmac_essid_input.Text, brute_snmac_psk_input.Text, brute_snmac_threads.Value.ToString(), quick);
            brute_snmac_button.Enabled = true;
            brute_snmac_button.Text = "brute";

            if(!ret.Contains("nothing found"))
            {
                using (TextWriter config2 = new StreamWriter(@"list.txt", true))
                {
                    try
                    {
                        config2.WriteLine(ret.Substring(0, 14) + " " + ret.Substring(25, 13));
                        config2.Close();
                        U.sortUniq("list.txt");
                    }
                    catch (Exception ex)
                    {
                        output.Text = ex.Message;
                    }
                }
            }

            output.Text = ret + "\r\nadded to list.txt";

            return;
        }

        private void brute_psk_button_Click(object sender, EventArgs e)
        {
            bool quick = brute_psk_quick.Checked;
            brute_psk_button.Text = ". . .";
            output.Text = "creating a rainbow table for " + brute_psk_essid_input.Text + "...";
            brute_psk_button.Enabled = false;
            output.Text = AGPF_Brute.brutePSK(brute_psk_essid_input.Text, brute_psk_threads.Value.ToString(), quick);
            output.SelectionStart = output.TextLength;
            output.ScrollToCaret();
            output.Refresh();
            brute_psk_button.Enabled = true;
            brute_psk_button.Text = "brute";
            return;
        }

        private void psk_essid_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                psk_psk_button_Click(sender, e);

            return;
        }

        private void psk_bssid_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                psk_psk_button_Click(sender, e);

            return;
        }

        private void kq_essid_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                kq_button_Click(sender, e);

            return;
        }

        private void kq_serial_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                kq_button_Click(sender, e);

            return;
        }

        private void brute_snmac_essid_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                brute_snmac_button_Click(sender, e);

            return;
        }

        private void brute_snmac_psk_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                brute_snmac_button_Click(sender, e);

            return;
        }

        private void brute_psk_mac_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                brute_psk_button_Click(sender, e);

            return;
        }

        private void kq_export_button_Click(object sender, EventArgs e)
        {
            output.Text = "";
            if (kq_openfile_dialog.ShowDialog() == DialogResult.OK)
            {
                string ret = AGPF_KQ.openKQList(kq_openfile_dialog.FileName, true);
                U.sortUniq("database.txt");

                output.Text = "database.txt created from " + ret + " entries.";
            }
            return;
        }
    }
}
