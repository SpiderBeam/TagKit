using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup.Sets
{
    /// <summary>
    /// A list of tokens that can be modified.
    /// </summary>
    sealed class SettableTokenList : TokenList, ISettableTokenList
    {
        #region ctor

        internal SettableTokenList(String value)
            : base(value)
        {
        }

        #endregion

        #region Properties

        public String Value
        {
            get { return ToString(); }
            set { Update(value); }
        }

        #endregion
    }
}
