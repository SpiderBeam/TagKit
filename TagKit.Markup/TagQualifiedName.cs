using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    public class TagQualifiedName
    {
        private string _name;
        private string _ns;

        private Int32 _hash;

        public static readonly TagQualifiedName Empty = new TagQualifiedName(string.Empty);

        public TagQualifiedName() : this(string.Empty, string.Empty) { }

        public TagQualifiedName(string name) : this(name, string.Empty) { }

        public TagQualifiedName(string name, string ns)
        {
            _ns = ns == null ? string.Empty : ns;
            _name = name == null ? string.Empty : name;
        }

        public string Namespace
        {
            get { return _ns; }
        }
        public string Name
        {
            get { return _name; }
        }
        public override int GetHashCode()
        {
            if (_hash == 0)
            {
                _hash = Name.GetHashCode() /*+ Namespace.GetHashCode()*/; // for perf reasons we are not taking ns's hashcode.
            }
            return _hash;
        }
        public bool IsEmpty
        {
            get { return Name.Length == 0 && Namespace.Length == 0; }
        }
        public override string ToString()
        {
            return Namespace.Length == 0 ? Name : string.Concat(Namespace, ":", Name);
        }
        public override bool Equals(object other)
        {
            TagQualifiedName qname;

            if ((object)this == other)
            {
                return true;
            }

            qname = other as TagQualifiedName;
            if (qname != null)
            {
                return (Name == qname.Name && Namespace == qname.Namespace);
            }
            return false;
        }
        public static bool operator ==(TagQualifiedName a, TagQualifiedName b)
        {
            if ((object)a == (object)b)
                return true;

            if ((object)a == null || (object)b == null)
                return false;

            return (a.Name == b.Name && a.Namespace == b.Namespace);
        }
        public static bool operator !=(TagQualifiedName a, TagQualifiedName b)
        {
            return !(a == b);
        }
        public static string ToString(string name, string ns)
        {
            return ns == null || ns.Length == 0 ? name : ns + ":" + name;
        }

    }
}
