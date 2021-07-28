using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiliX.Common
{
    public static class String
    {
        static String()
        {
        }

        public static string Fix(this string text)
        {
            if (text is null)
            {
                return null;
            }

            text =
                text.Trim();

            if (text == string.Empty)
            {
                return null;
            }

            while (text.Contains("  "))
            {
                text =
                    text.Replace("  ", " ");
            }

            return text;
        }
	}
}
