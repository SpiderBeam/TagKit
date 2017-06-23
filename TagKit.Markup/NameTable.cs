using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    public class NameTable : TagNameTable
    {
        #region Private types
        private class Entry
        {
            internal string str;
            internal int hashCode;
            internal Entry next;

            internal Entry(string str, int hashCode, Entry next)
            {
                this.str = str;
                this.hashCode = hashCode;
                this.next = next;
            }
        }

        #endregion

        #region Fields
        private Entry[] _entries;
        private int _count;
        private int _mask;
        private int _hashCodeRandomizer;
        #endregion

        #region Constructor
        /// <summary>
        /// Public constructor.
        /// </summary>
        public NameTable()
        {
            _mask = 31;
            _entries = new Entry[_mask + 1];
            _hashCodeRandomizer = Environment.TickCount;
        }
        #endregion

        #region TagNameTable public methods
        /// <summary>
        /// Add the given string to the NameTable or return
        ///      the existing string if it is already in the NameTable.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override string Add(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            int len = key.Length;
            if (len == 0)
            {
                return string.Empty;
            }

            int hashCode;
            unchecked
            {
                hashCode = len + _hashCodeRandomizer;
                // use key.Length to eliminate the range check
                for (int i = 0; i < key.Length; i++)
                {
                    hashCode += (hashCode << 7) ^ key[i];
                }
                // mix it a bit more
                hashCode -= hashCode >> 17;
                hashCode -= hashCode >> 11;
                hashCode -= hashCode >> 5;
            }

            for (Entry e = _entries[hashCode & _mask]; e != null; e = e.next)
            {
                if (e.hashCode == hashCode && e.str.Equals(key))
                {
                    return e.str;
                }
            }
            return AddEntry(key, hashCode);
        }
        /// <summary>
        /// Add the given string to the NameTable or return
        ///      the existing string if it is already in the NameTable.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public override string Add(char[] key, int start, int len)
        {
            if (len == 0)
            {
                return string.Empty;
            }

            int hashCode;
            unchecked
            {
                hashCode = len + _hashCodeRandomizer;
                hashCode += (hashCode << 7) ^ key[start];   // this will throw IndexOutOfRangeException in case the start index is invalid
                int end = start + len;
                for (int i = start + 1; i < end; i++)
                {
                    hashCode += (hashCode << 7) ^ key[i];
                }
                // mix it a bit more
                hashCode -= hashCode >> 17;
                hashCode -= hashCode >> 11;
                hashCode -= hashCode >> 5;
            }

            for (Entry e = _entries[hashCode & _mask]; e != null; e = e.next)
            {
                if (e.hashCode == hashCode && TextEquals(e.str, key, start, len))
                {
                    return e.str;
                }
            }
            return AddEntry(new string(key, start, len), hashCode);
        }
        /// <summary>
        /// Find the matching string in the NameTable.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override string Get(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 0)
            {
                return string.Empty;
            }

            int hashCode;
            unchecked
            {
                int len = value.Length + _hashCodeRandomizer;
                hashCode = len;
                // use value.Length to eliminate the range check
                for (int i = 0; i < value.Length; i++)
                {
                    hashCode += (hashCode << 7) ^ value[i];
                }
                // mix it a bit more
                hashCode -= hashCode >> 17;
                hashCode -= hashCode >> 11;
                hashCode -= hashCode >> 5;
            }

            for (Entry e = _entries[hashCode & _mask]; e != null; e = e.next)
            {
                if (e.hashCode == hashCode && e.str.Equals(value))
                {
                    return e.str;
                }
            }
            return null;
        }
        /// <summary>
        /// Find the matching string atom given a range of
        /// characters.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public override string Get(char[] key, int start, int len)
        {
            if (len == 0)
            {
                return string.Empty;
            }

            int hashCode;
            unchecked
            {
                hashCode = len + _hashCodeRandomizer;
                hashCode += (hashCode << 7) ^ key[start];   // this will throw IndexOutOfRangeException in case the start index is invalid
                int end = start + len;
                for (int i = start + 1; i < end; i++)
                {
                    hashCode += (hashCode << 7) ^ key[i];
                }
                // mix it a bit more
                hashCode -= hashCode >> 17;
                hashCode -= hashCode >> 11;
                hashCode -= hashCode >> 5;
            }

            for (Entry e = _entries[hashCode & _mask]; e != null; e = e.next)
            {
                if (e.hashCode == hashCode && TextEquals(e.str, key, start, len))
                {
                    return e.str;
                }
            }
            return null;
        }

        #endregion

        #region Private methods
        private string AddEntry(string str, int hashCode)
        {
            int index = hashCode & _mask;
            Entry e = new Entry(str, hashCode, _entries[index]);
            _entries[index] = e;
            if (_count++ == _mask)
            {
                Grow();
            }
            return e.str;
        }
        private void Grow()
        {
            int newMask = _mask * 2 + 1;
            Entry[] oldEntries = _entries;
            Entry[] newEntries = new Entry[newMask + 1];

            // use oldEntries.Length to eliminate the range check            
            for (int i = 0; i < oldEntries.Length; i++)
            {
                Entry e = oldEntries[i];
                while (e != null)
                {
                    int newIndex = e.hashCode & newMask;
                    Entry tmp = e.next;
                    e.next = newEntries[newIndex];
                    newEntries[newIndex] = e;
                    e = tmp;
                }
            }

            _entries = newEntries;
            _mask = newMask;
        }

        private static bool TextEquals(string str1, char[] str2, int str2Start, int str2Length)
        {
            if (str1.Length != str2Length)
            {
                return false;
            }
            // use array.Length to eliminate the range check
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[str2Start + i])
                {
                    return false;
                }
            }
            return true;
        }


        #endregion
    }
}
