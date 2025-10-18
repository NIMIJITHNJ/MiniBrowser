using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiniBrowser.Core
{
    public static class LinkExtractor
    {
        // Regular expression to find links inside HTML
        static readonly Regex Link = new Regex(
            "<a\\s+[^>]*href=[\"'](?<u>[^\"'#]+)[\"']", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Method to collect up to 5 unique links from the HTML page
        public static List<string> FirstFive(string html, string baseUrl)
        {
            var list = new List<string>();
            // To skip if HTML is empty
            if (string.IsNullOrEmpty(html)) return list;

            // Going through all links found in the HTML
            foreach (Match m in Link.Matches(html))
            {
                var raw = m.Groups["u"].Value.Trim();
                if (string.IsNullOrEmpty(raw)) continue;
                try
                {
                    var resolved = new Uri(new Uri(baseUrl), raw).ToString();

                    // Add only if it’s not already in the list
                    if (!list.Contains(resolved))
                    {
                        list.Add(resolved);
                        // Stop once 5 links are added
                        if (list.Count == 5) break;
                    }
                }
                catch {
                    // Ignore broken or invalid links
                }
            }
            return list;
        }
    }
}
