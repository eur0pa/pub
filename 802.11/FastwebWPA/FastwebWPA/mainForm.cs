using System;
using System.Windows.Forms;

namespace FastwebWPA
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void PSK_button_Click(object sender, EventArgs e)
        {
            FastwebWPA.utils u = new FastwebWPA.utils();

            string ret, mac, essid;

            essid = ESSID_input.Text;

            ret = u.validateESSID(essid);
            if (!ret.Equals("fine"))
            {
                PSK_output.Text = ret;
                return;
            }

            mac = u.essidToMac(essid);

            ret = u.checkRouterClass(mac);
            if ((!ret.Equals("pirelli")) && (!ret.Equals("telsey")))
            {
                PSK_output.Text = ret;
                return;
            }

            switch (ret)
            {
                case "pirelli": PSK_output.Text = pirelliPSK.CreatePirelliPSK(mac); break;
                case "telsey": PSK_output.Text = telseyPSK.CreateTelseyPSK(mac); break;
                default: return;
            }

            return;
        }

        private void ESSID_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                PSK_button_Click(sender, e);
            }
        }
    }
}
