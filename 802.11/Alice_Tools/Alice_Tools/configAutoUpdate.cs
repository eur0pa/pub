using System;
using System.Text;

using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Alice_Tools
{
    class configAutoUpdate
    {
        public static SByte doUpdate()
        {
            try
            {
                using (Stream sConfig = WebRequest.Create("http://www.gibit.net/forum/viewtopic.php?p=3").GetResponse().GetResponseStream())
                {
                    sConfig.ReadTimeout = 5000;

                    String uConfig = new StreamReader(sConfig, Encoding.ASCII).ReadToEnd();
                    MatchCollection cMatches = Regex.Matches(uConfig, "(?:[0-9]{3}|[0-9]{2}X),6(?:79|91)0[1-4]+,(?:8|13),[0-9]{8},(?:[0-9A-F]{6}|[X]{6})");

                    if (cMatches.Count == 0)
                    {
                        throw new Exception("0 lines returned");
                    }

                    TextWriter tConfig = new StreamWriter(@".\config.txt", true);
                    foreach (Match cMatch in cMatches)
                    {
                        tConfig.WriteLine(cMatch.Value);
                    }

                    tConfig.Close();

                    Alice_Tools.Utils U = new Alice_Tools.Utils();
                    U.sortUniq("config.txt");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            return 0;
        }
    }
}
