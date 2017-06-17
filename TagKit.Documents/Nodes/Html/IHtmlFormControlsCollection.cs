﻿using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Nodes.Html
{
    /// <summary>
    /// Represents a collection of HTML form controls.
    /// </summary>
    [DomName("HTMLFormControlsCollection")]
    public interface IHtmlFormControlsCollection : IHtmlCollection<IHtmlElement>
    {
    }
}
