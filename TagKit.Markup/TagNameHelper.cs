using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    internal static class TagNameHelper
    {
        public static int GetHashCode(string name)
        {
            int hashCode = 0;
            if (name != null)
            {
                for (int i = name.Length - 1; i >= 0; i--)
                {
                    char ch = name[i];
                    if (ch == ':') break;
                    hashCode += (hashCode << 7) ^ ch;
                }
                hashCode -= hashCode >> 17;
                hashCode -= hashCode >> 11;
                hashCode -= hashCode >> 5;
            }
            return hashCode;
        }
    }
}
