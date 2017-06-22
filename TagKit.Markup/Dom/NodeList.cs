using System;
using System.Collections;

namespace TagKit.Markup
{
    /// <summary>
    /// Represents an ordered collection of nodes.
    /// </summary>
    public abstract class NodeList : IEnumerable, IDisposable
    {
        /// <summary>
        /// Retrieves a node at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract Node Item(int index);

        /// <summary>
        /// Gets the number of nodes in this NodeList.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Provides a simple ForEach-style iteration over the collection of nodes in
        /// this NodeList.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator GetEnumerator();

        /// <summary>
        /// Retrieves a node at the given index.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.IndexerName("ItemOf")]
        public virtual Node this[int i] { get { return Item(i); } }

        void IDisposable.Dispose()
        {
            PrivateDisposeNodeList();
        }

        protected virtual void PrivateDisposeNodeList() { }
    }
}
