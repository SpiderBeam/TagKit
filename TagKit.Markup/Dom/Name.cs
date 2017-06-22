using System;
using System.Runtime.Serialization;

namespace TagKit.Markup
{
    public sealed class Name : IEquatable<Name>, ISerializable
    {
        #region Implementation of IEquatable<Name>

        public bool Equals(Name other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
