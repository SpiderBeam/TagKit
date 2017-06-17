using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TagKit.Documents;
using TagKit.Documents.Net;
using TagKit.Documents.Nodes;
using TagKit.Documents.Nodes.Html;
using TagKit.Foundation;
using TagKit.Foundation.Text;
using TagKit.Services.Configuration;
using TagKit.Services.Css;

namespace TagKit.Markup.IO.Processors
{
    sealed class StyleSheetRequestProcessor : BaseRequestProcessor
    {
        #region Fields

        private readonly IHtmlLinkElement _link;
        private readonly IBrowsingContext _context;
        private IStylingService _engine;

        #endregion

        #region ctor

        public StyleSheetRequestProcessor(IBrowsingContext context, IHtmlLinkElement link)
            : base(context?.GetService<IResourceLoader>())
        {
            _context = context;
            _link = link;
        }

        #endregion

        #region Properties

        public IStyleSheet Sheet
        {
            get;
            private set;
        }

        public IStylingService Engine
        {
            get { return _engine ?? (_engine = _context.GetStyling(LinkType)); }
        }

        public String LinkType
        {
            get { return _link.Type ?? MimeTypeNames.Css; }
        }

        #endregion

        #region Methods

        public override Task ProcessAsync(ResourceRequest request)
        {
            if (IsAvailable && Engine != null && IsDifferentToCurrentDownloadUrl(request.Target))
            {
                CancelDownload();
                Download = DownloadWithCors(new CorsRequest(request)
                {
                    Setting = _link.CrossOrigin.ToEnum(CorsSetting.None),
                    Behavior = OriginBehavior.Taint,
                    Integrity = _context.GetProvider<IIntegrityProvider>()
                });
                return FinishDownloadAsync();
            }

            return null;
        }

        protected override async Task ProcessResponseAsync(IResponse response)
        {
            var cancel = CancellationToken.None;
            var options = new StyleOptions(_link.Owner)
            {
                Element = _link,
                IsDisabled = _link.IsDisabled,
                IsAlternate = _link.RelationList.Contains(Keywords.Alternate)
            };

            var task = _engine.ParseStylesheetAsync(response, options, cancel);
            var sheet = await task.ConfigureAwait(false);
            sheet.Media.MediaText = _link.Media ?? String.Empty;
            Sheet = sheet;
        }

        #endregion
    }
}
