using BaseEntities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Extensions.SiteMapBuilder
{
    public enum SiteMapFreq
    {
        [Description("Günlük")]
        daily,
        [Description("Haftalık")]
        weekly,
        [Description("Aylık")]
        monthly
    }
    public class SiteMapBuilder
    {

        public static StringBuilder Build(List<SiteMapEntity> entities, decimal priority, SiteMapFreq freq)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            var pri = priority.ToString().Replace(',', '.');
            var f = freq.ToString();
            foreach (var item in entities)
            {
                sb.AppendLine("<url>");
                sb.AppendLine("<loc>");
                sb.AppendLine(item.Url);
                sb.AppendLine("</loc>");
                sb.AppendLine("<changefreq>");
                sb.AppendLine(f);
                sb.AppendLine("</changefreq>");
                sb.AppendLine("<priority>");
                sb.AppendLine(pri);
                sb.AppendLine("</priority>");
                sb.AppendLine("</url>");
            }
            sb.AppendLine("</urlset>");

            return sb;
        }
    }
}