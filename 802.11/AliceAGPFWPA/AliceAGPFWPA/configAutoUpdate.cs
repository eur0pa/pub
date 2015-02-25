using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AliceAGPFWPA
{
    class configAutoUpdate
    {
        public SByte doUpdate()
        {
            try
            {
                Stream sConfig = WebRequest.Create("http://www.gibit.net/forum/viewtopic.php?p=3").GetResponse().GetResponseStream();
                sConfig.ReadTimeout = 5000;

                String uConfig = new StreamReader(sConfig, Encoding.ASCII).ReadToEnd();
                MatchCollection cMatches = Regex.Matches(uConfig, "(?:[0-9]{3}|[0-9]{2}X),6(?:79|91)0[1-4]+,(?:8|13),[0-9]{8},(?:[0-9A-F]{6}|[X]{6})");
                TextWriter tConfig = new StreamWriter(Application.StartupPath + "\\config.txt");
                foreach (Match cMatch in cMatches)
                    tConfig.WriteLine(cMatch.Value);

                tConfig.Flush();
                tConfig.Dispose();

                return 1;
            }
            catch (InvalidOperationException)
            {
                return -1;
            }
        }
    }
}
