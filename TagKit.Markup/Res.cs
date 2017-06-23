using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    internal sealed class Res
    {
        private static Res loader;
        private ResourceManager resources;

        private static CultureInfo Culture
        {
            get
            {
                return (CultureInfo)null;
            }
        }

        public static ResourceManager Resources
        {
            get
            {
                return Res.GetLoader().resources;
            }
        }

        internal Res()
        {
            this.resources = new ResourceManager("System.Xml", this.GetType().Assembly);
        }

        private static Res GetLoader()
        {
            if (Res.loader == null)
            {
                Res res = new Res();
                Interlocked.CompareExchange<Res>(ref Res.loader, res, (Res)null);
            }
            return Res.loader;
        }

        public static string GetString(string name, params object[] args)
        {
            Res loader = Res.GetLoader();
            if (loader == null)
                return (string)null;
            string format = loader.resources.GetString(name, Res.Culture);
            if (args == null || args.Length == 0)
                return format;
            for (int index = 0; index < args.Length; ++index)
            {
                string str = args[index] as string;
                if (str != null && str.Length > 1024)
                    args[index] = (object)(str.Substring(0, 1021) + "...");
            }
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, format, args);
        }

        public static string GetString(string name)
        {
            Res loader = Res.GetLoader();
            if (loader == null)
                return (string)null;
            return loader.resources.GetString(name, Res.Culture);
        }

        public static string GetString(string name, out bool usedFallback)
        {
            usedFallback = false;
            return Res.GetString(name);
        }

        public static object GetObject(string name)
        {
            Res loader = Res.GetLoader();
            if (loader == null)
                return (object)null;
            return loader.resources.GetObject(name, Res.Culture);
        }
    }
}
