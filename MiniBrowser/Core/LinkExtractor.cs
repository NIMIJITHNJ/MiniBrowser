using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiniBrowser.Core
{
    public static class LinkExtractor
    {
        static readonly Regex Link = new Regex(
            "<a\\s+[^>]*href=[\"'](?<u>[^\"'#]+)[\"']", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static List<string> FirstFive(string html, string baseUrl)
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(html)) return list;

            foreach (Match m in Link.Matches(html))
            {
                var raw = m.Groups["u"].Value.Trim();
                if (string.IsNullOrEmpty(raw)) continue;
                try
                {
                    var resolved = new Uri(new Uri(baseUrl), raw).ToString();
                    if (!list.Contains(resolved))
                    {
                        list.Add(resolved);
                        if (list.Count == 5) break;
                    }
                }
                catch { /* ignore bad urls */ }
            }
            return list;
        }
    }
}
