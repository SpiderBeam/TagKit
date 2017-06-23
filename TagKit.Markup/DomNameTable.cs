using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    internal class DomNameTable
    {
        private TagName[] _entries;
        private int _count;
        private int _mask;
        private Document _ownerDocument;
        private TagNameTable _nameTable;

        private const int InitialSize = 64; // must be a power of two

        public DomNameTable(Document document)
        {
            _ownerDocument = document;
            _nameTable = document.NameTable;
            _entries = new TagName[InitialSize];
            _mask = InitialSize - 1;
            Debug.Assert((_entries.Length & _mask) == 0);  // entries.Length must be a power of two
        }

        public TagName GetName(string prefix, string localName, string ns)
        {
            if (prefix == null)
            {
                prefix = string.Empty;
            }
            if (ns == null)
            {
                ns = string.Empty;
            }

            int hashCode = TagNameHelper.GetHashCode(localName);

            for (TagName e = _entries[hashCode & _mask]; e != null; e = e.next)
            {
                if (e.HashCode == hashCode
                    && ((object)e.LocalName == (object)localName
                        || e.LocalName.Equals(localName))
                    && ((object)e.Prefix == (object)prefix
                        || e.Prefix.Equals(prefix))
                    && ((object)e.NamespaceURI == (object)ns
                        || e.NamespaceURI.Equals(ns)))
                {
                    return e;
                }
            }
            return null;
        }

        public TagName AddName(string prefix, string localName, string ns)
        {
            if (prefix == null)
            {
                prefix = string.Empty;
            }
            if (ns == null)
            {
                ns = string.Empty;
            }

            int hashCode = TagNameHelper.GetHashCode(localName);

            for (TagName e = _entries[hashCode & _mask]; e != null; e = e.next)
            {
                if (e.HashCode == hashCode
                    && ((object)e.LocalName == (object)localName
                        || e.LocalName.Equals(localName))
                    && ((object)e.Prefix == (object)prefix
                        || e.Prefix.Equals(prefix))
                    && ((object)e.NamespaceURI == (object)ns
                        || e.NamespaceURI.Equals(ns)))
                {
                    return e;
                }
            }

            prefix = _nameTable.Add(prefix);
            localName = _nameTable.Add(localName);
            ns = _nameTable.Add(ns);
            int index = hashCode & _mask;
            TagName name = TagName.Create(prefix, localName, ns, hashCode, _ownerDocument, _entries[index]);
            _entries[index] = name;

            if (_count++ == _mask)
            {
                Grow();
            }

            return name;
        }

        private void Grow()
        {
            int newMask = _mask * 2 + 1;
            TagName[] oldEntries = _entries;
            TagName[] newEntries = new TagName[newMask + 1];

            // use oldEntries.Length to eliminate the range check            
            for (int i = 0; i < oldEntries.Length; i++)
            {
                TagName name = oldEntries[i];
                while (name != null)
                {
                    int newIndex = name.HashCode & newMask;
                    TagName tmp = name.next;
                    name.next = newEntries[newIndex];
                    newEntries[newIndex] = name;
                    name = tmp;
                }
            }
            _entries = newEntries;
            _mask = newMask;
        }
    }
}
