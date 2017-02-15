using System.Collections.Generic;
using System.Web;
using System.Xml;
using Sitecore.Xml;

namespace Sitecore.Support.Analytics.Pipelines.StartAnalytics
{
    using Sitecore;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Configuration;
    using Sitecore.Analytics.Tracking.Diagnostics.PerformanceCounters;
    using Sitecore.Diagnostics;
    using Sitecore.Layouts;
    using Sitecore.Pipelines;
    using Sitecore.Sites;
    using System;

    public class ExcludeUrls
    {

        private readonly List<string> startUrls = new List<string>();
        //private readonly List<string> endUrls = new List<string>();
        public virtual void AddStartUrl(XmlNode configNode)
        {
            Assert.ArgumentNotNull(configNode, "configNode");
            string path = XmlUtil.GetAttribute("path", configNode);
            startUrls.Add(path);
        }

        //public virtual void AddEndUrl(XmlNode configNode)
        //{
        //    Assert.ArgumentNotNull(configNode, "configNode");
        //    string path = XmlUtil.GetAttribute("path", configNode);
        //    endUrls.Add(path);
        //}

        public virtual void Process(PipelineArgs args)
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null &&
                HttpContext.Current.Request.FilePath != null)
            {
                foreach (var startUrl in startUrls)
                {
                    if (HttpContext.Current.Request.FilePath.StartsWith(startUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        args.AbortPipeline();
                    }
                }
                //foreach (var endUrl in endUrls)
                //{
                //    if (HttpContext.Current.Request.FilePath.EndsWith(endUrl, StringComparison.OrdinalIgnoreCase))
                //    {
                //        args.AbortPipeline();
                //    }
                //}
            }
            
            
        }
    }
}
