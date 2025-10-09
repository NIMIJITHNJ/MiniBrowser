namespace MiniBrowser.Core
{
    public static class UrlTools
    {
        public const string DefaultHome = "https://www.hw.ac.uk";               // Default Home page
        public static string CleanUrl(string input)                            // Adds "https://" if user typed a bare domain
        {
            if (string.IsNullOrWhiteSpace(input)) return DefaultHome;
            var url = input.Trim();
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;
            return url;
        }
    }
}
