using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AliceAGPFWPA
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void psk_output_Click(object sender, EventArgs e)
        {
            if (psk_output.Text.StartsWith("input essid"))
            {
                AliceAGPFWPA.configAutoUpdate upd = new AliceAGPFWPA.configAutoUpdate();

                SByte ret = upd.doUpdate();
                string s = "";

                switch (ret)
                {
                    case  1: s = "config.txt updated"; break;
                    case -1: s = "timeout while reading config.txt"; break;
                    case  0: s = "something went wrong :s"; break;
                }

                psk_output.Text = s;
            }
        }

        private void psk_button_Click(object sender, EventArgs e)
        {
            AliceAGPFWPA.utils u = new AliceAGPFWPA.utils();

            string essid = essid_input.Text;
            string mac = mac_input.Text;
            string ret = u.validateESSID(essid);

            if (!ret.Equals("fine"))
            {
                psk_output.Text = ret;
                return;
            }

            if (string.IsNullOrEmpty(mac))
                mac = "void";

            ret = agpfPSK.agpf_PSK(essid, mac);
            psk_output.Text = ret;

            return;
        }

        private void essid_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                psk_button_Click(sender, e);

            return;
        }

        private void mac_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                psk_button_Click(sender, e);

            return;
        }
    }
}
