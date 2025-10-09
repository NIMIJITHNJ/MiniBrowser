using System;
using System.Threading.Tasks;
using MiniBrowser.Core;

class Program
{
    // Managers shared across the app
    private static readonly HttpClientService service = new();
    private static readonly SettingsStore settings = new();
    private static readonly HistoryManager history = new();
    private static readonly BookmarkManager bookmarks = new();

    private static string currentFinalUrl = "";   // last successfully loaded page

    static async Task Main(string[] args)
    {
        // Load persisted data
        settings.Load();
        history.Load();
        bookmarks.Load();

        Console.WriteLine("MiniBrowser (console)");
        Console.WriteLine("Commands: blank=home | r=reload | b=back | f=forward | h=history |");
        Console.WriteLine("          bm=list bookmarks | addbm | openbm <n> | delbm <n> | sethome <url> | q=quit");

        while (true)
        {
            Console.Write("\nEnter URL or command: ");
            var line = Console.ReadLine() ?? string.Empty;
            var parts = line.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var cmd = parts.Length > 0 ? parts[0].ToLowerInvariant() : "";
            var arg = parts.Length > 1 ? parts[1] : "";

            if (cmd == "q") break;

            switch (cmd)
            {
                case "": // blank -> home
                    await NavigateAsync(settings.Data.HomeUrl);
                    break;

                case "r":
                    if (string.IsNullOrWhiteSpace(currentFinalUrl))
                        Console.WriteLine("Nothing to reload yet.");
                    else
                        await NavigateAsync(currentFinalUrl);
                    break;

                case "b":
                    await BackAsync();
                    break;

                case "f":
                    await ForwardAsync();
                    break;

                case "h":
                    ShowRecent(history);
                    break;

                case "bm":
                    ListBookmarks();
                    break;

                case "addbm":
                    AddBookmarkInteractive();
                    break;

                case "openbm":
                    OpenBookmarkByIndex(arg);
                    break;

                case "delbm":
                    DeleteBookmarkByIndex(arg);
                    break;

                case "sethome":
                    SetHome(arg);
                    break;

                default:
                    // treat as URL
                    await NavigateAsync(line);
                    break;
            }
        }
    }

    // -------- Navigation helpers --------

    static async Task NavigateAsync(string input)
    {
        var target = ResolveInput(input);
        var result = await service.GetAsync(target);

        Console.WriteLine($"\nStatus: {result.StatusCode} {result.Reason}");
        Console.WriteLine($"Final URL: {result.FinalUrl}");
        Console.WriteLine($"Title: {ExtractTitle(result.Body)}");
        Console.WriteLine("\n=== HTML preview (first 500 chars) ===");
        Console.WriteLine(result.Body?.Substring(0, Math.Min(500, result.Body.Length)));
        Console.WriteLine();

        if (result.StatusCode != 0)
        {
            currentFinalUrl = result.FinalUrl;
            history.Add(result.FinalUrl);
            history.Save();
        }

        var links = LinkExtractor.FirstFive(result.Body ?? "", result.FinalUrl ?? "");

        if (links.Count > 0)
        {
            Console.WriteLine("First 5 links:");
            for (int i = 0; i < links.Count; i++)
                Console.WriteLine($"{i + 1}. {links[i]}");
        }
    }

    static async Task BackAsync()
    {
        var url = history.Back();
        if (url == null) { Console.WriteLine("No back history."); return; }
        await NavigateAsync(url);
    }

    static async Task ForwardAsync()
    {
        var url = history.Forward();
        if (url == null) { Console.WriteLine("No forward history."); return; }
        await NavigateAsync(url);
    }

    // -------- Bookmarks --------

    static void ListBookmarks()
    {
        var all = bookmarks.All();
        if (all.Count == 0) { Console.WriteLine("No bookmarks."); return; }

        Console.WriteLine("\nBookmarks:");
        for (int i = 0; i < all.Count; i++)
            Console.WriteLine($"{i + 1}. {all[i].Name} → {all[i].Url}");
    }

    static void AddBookmarkInteractive()
    {
        var currentFallback = string.IsNullOrWhiteSpace(currentFinalUrl) ? settings.Data.HomeUrl : currentFinalUrl;

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("URL (blank = current page): ");
        var urlIn = Console.ReadLine() ?? "";

        var url = string.IsNullOrWhiteSpace(urlIn) ? currentFallback : UrlTools.CleanUrl(urlIn);
        bookmarks.Add(name, url);
        bookmarks.Save();
        Console.WriteLine("Bookmark saved.");
    }

    static void OpenBookmarkByIndex(string arg)
    {
        if (!int.TryParse(arg, out int n) || n <= 0 || n > bookmarks.All().Count)
        {
            Console.WriteLine("Usage: openbm <index>");
            return;
        }
        var url = bookmarks.All()[n - 1].Url;
        _ = NavigateAsync(url); // fire and forget from command handler
    }

    static void DeleteBookmarkByIndex(string arg)
    {
        if (!int.TryParse(arg, out int n) || n <= 0 || n > bookmarks.All().Count)
        {
            Console.WriteLine("Usage: delbm <index>");
            return;
        }
        var ok = bookmarks.Delete(n - 1);
        if (ok) { bookmarks.Save(); Console.WriteLine("Deleted."); }
        else Console.WriteLine("Delete failed.");
    }

    // -------- Settings --------

    static void SetHome(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            Console.WriteLine("Usage: sethome <url>");
            return;
        }
        var url = UrlTools.CleanUrl(raw);
        settings.Data.HomeUrl = url;
        settings.Save();
        Console.WriteLine($"Home set to: {url}");
    }

    // -------- Utilities --------

    static string ResolveInput(string input)
        => string.IsNullOrWhiteSpace(input) ? settings.Data.HomeUrl : UrlTools.CleanUrl(input);

    static void ShowRecent(HistoryManager h)
    {
        var list = h.Recent(5);
        if (list.Count == 0) { Console.WriteLine("History is empty."); return; }

        Console.WriteLine("\nLast 5 visited:");
        for (int i = list.Count - 1, n = 1; i >= 0; i--, n++)
            Console.WriteLine($"{n}. {list[i]}");
    }

    static string ExtractTitle(string html)
    {
        if (string.IsNullOrEmpty(html)) return "(no title)";
        var start = html.IndexOf("<title", StringComparison.OrdinalIgnoreCase);
        if (start < 0) return "(no title)";
        start = html.IndexOf(">", start);
        if (start < 0) return "(no title)";
        var end = html.IndexOf("</title>", start, StringComparison.OrdinalIgnoreCase);
        if (end < 0) return "(no title)";
        return html.Substring(start + 1, end - start - 1).Trim();
    }
}
