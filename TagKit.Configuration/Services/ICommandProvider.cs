using System;
using TagKit.Documents.Nodes.Browser;

namespace TagKit.Services
{
    /// <summary>
    /// Represents a provider to get document commands.
    /// </summary>
    public interface ICommandProvider
    {
        /// <summary>
        /// Gets the command with the given id.
        /// </summary>
        /// <param name="name">The id of the command.</param>
        /// <returns>The document command if any.</returns>
        ICommand GetCommand(String name);
    }
}
