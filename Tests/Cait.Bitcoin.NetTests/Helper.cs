using System.IO;
using System.Text;

namespace Cait.Bitcoin.NetTests
{
    public static class Helper
    {
        public static string SimpleHex(this string hexDumpDescription)
        {
            StringBuilder sb = new StringBuilder();
            using (TextReader sr = new StringReader(hexDumpDescription))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (char c in line)
                    {
                        if (c == '/')
                            break;

                        if (char.IsLetterOrDigit(c))
                            sb.Append(c);
                    }
                }
            }
            return sb.ToString().ToLower();
        }
    }
}