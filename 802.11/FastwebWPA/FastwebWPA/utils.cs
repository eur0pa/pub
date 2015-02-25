using System;
using System.Collections.Generic;
using System.Linq;

/* miscellaneous and useful utilities:                  *
 * byteToStr: return string from array of bytes         *
 * strToByteArray: return an array of bytes from string *
 * validateESSID: check for invalid essid input         *
 * checkRouterClass: is it a pirelli or a telsey?       */

namespace FastwebWPA
{
    class utils
    {
        public string byteToStr(Byte[] b)
        {
            System.Text.UTF8Encoding str = new System.Text.UTF8Encoding();
            return str.GetString(b);
        }

        public Byte[] strToByteArray(string s)
        {
            System.Text.UTF8Encoding bin = new System.Text.UTF8Encoding();
            return bin.GetBytes(s);
        }

        public Byte[] strToHex(string s)
        {
            List<Byte> b = new List<Byte>();
            for (Byte i = 0; i < s.Length; i += 2)
                b.Add((Byte)Convert.ToByte(s.Substring(i, 2), 16));

            return b.ToArray();
        }

        public string essidToMac(string s)
        {
            return s.Substring(10, 12).ToUpper();
        }

        public string validateESSID(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "essid needed";

            if (s.Length != 22)
                return "invalid essid";

            if (String.Compare(s.Substring(0, 8), "FASTWEB-", true) != 0)
                return "not fastweb";

            return "fine";
        }

        public string checkRouterClass(string m)
        {
            string[] pirelliMac = {
		                "000827", "0013C8", "0017C2",
		                "00193E", "001CA2", "001D8B",
		                "002233", "00238E", "002553",
                        "38229D", "6487D7", "00A02F",
                        "080018"
                     };

            string[] telseyMac = {
                        "00036F", "002196"
                     };

            if (pirelliMac.Any(m.StartsWith))
                return "pirelli";

            else if (telseyMac.Any(m.StartsWith))
                return "telsey";

            return "unknown";
        }
    }
}