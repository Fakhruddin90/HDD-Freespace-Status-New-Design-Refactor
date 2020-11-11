using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HDD_Freespace_Status_Design_1
{
    internal static class ExtenssionMethods
    {
        public static Color FromHex(this string hex) =>
            ColorTranslator.FromHtml(hex);
    }
}
