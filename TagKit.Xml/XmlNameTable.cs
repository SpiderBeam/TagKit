using System;

namespace TagKit.Xml
{
    public abstract class XmlNameTable
    {
        public abstract String Get(char[] array, int offset, int length);
        public abstract String Get(String array);
        public abstract String Add(char[] array, int offset, int length);
        public abstract String Add(String array);

    }
}
