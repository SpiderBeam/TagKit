using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Documents.Nodes;
using TagKit.Documents.Nodes.Html;
using TagKit.Markup.IO.Processors;

namespace TagKit.Markup.Nodes.Html.LinkRels
{
    class StyleSheetLinkRelation : BaseLinkRelation
    {
        #region ctor

        public StyleSheetLinkRelation(IHtmlLinkElement link)
            : base(link, new StyleSheetRequestProcessor(link?.Owner.Context, link))
        {
        }

        #endregion

        #region Properties

        public IStyleSheet Sheet
        {
            get
            {
                var processor = Processor as StyleSheetRequestProcessor;
                return processor?.Sheet;
            }
        }

        #endregion

        #region Methods

        public override Task LoadAsync()
        {
            var request = Link.CreateRequestFor(Url);
            return Processor?.ProcessAsync(request);
        }

        #endregion
    }
}
