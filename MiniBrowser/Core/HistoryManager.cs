using System.Collections.Generic;
using System.Linq;

namespace MiniBrowser.Core
{
    // Saved shape on disk
    public class HistoryData
    {
        public List<string> Items { get; set; } = new();
        public List<string> Full { get; set; } = new();
        public int Index { get; set; } = -1;
    }

    // Keeps a simple browsing history and a pointer to the current page
    public class HistoryManager
    {
        // Stack used by Back/Forward
        private readonly List<string> items = new();
        // Complete history for UI
        private readonly List<string> full =new();
        private int currentIndex = -1;

        public string StorageMode { get; set; } = "Json";

        public bool CanBack    => currentIndex > 0;
        public bool CanForward => currentIndex >= 0 && currentIndex < items.Count - 1;

        public int Count => items.Count;
        public string? Current => currentIndex >= 0 && currentIndex < Count ? items[currentIndex] : null;

        // Add a page to history. If we had gone back, drop the forward part and avoids adding the same URL twice in a row
        public void Add(string url)
        {
            // Normalising the URL
            url = UrlTools.CleanUrl(url);
            if (string.IsNullOrWhiteSpace(url)) return;

            // Ignore if same as current to prevents consecutive duplicates
            if (currentIndex >= 0 && string.Equals(items[currentIndex], url, StringComparison.OrdinalIgnoreCase))
                return;

            // If user went back and now visits a new page, drop all forward entries
            if (currentIndex < Count - 1)
                items.RemoveRange(currentIndex + 1, Count - (currentIndex + 1));

            // Add the new page and update the index
            items.Add(url);
            currentIndex = Count - 1;

            // Also add to full history
            if (full.Count == 0 || !string.Equals(full[^1], url, StringComparison.OrdinalIgnoreCase))
                full.Add(url);
        }

        // Going back one step and returns the new current URL or null
        public string? Back()
        {
            if (!CanBack) return null;
            currentIndex--;
            return items[currentIndex];
        }

        // Going forward one step and returns the new current URL or null
        public string? Forward()
        {
            if (!CanForward) return null;
            currentIndex++;
            return items[currentIndex];
        }

        // Returns up to the last n URLs, newest will be added to last
        public List<string> Recent(int n = 5)
        {
            if (n <= 0 || full.Count == 0) return new List<string>();
            int start = Math.Max(0, full.Count - n);
            return full.GetRange(start, full.Count - start);
        }

        // Clear all history
        public void Clear()
        {
            items.Clear();
            full.Clear();
            currentIndex = -1;
        }

        // Load history for specific user
        public void LoadForUser(string user)
        {
            Clear();
            
            var u = CleanUserName(user);

            if (StorageMode.Equals("Db", StringComparison.OrdinalIgnoreCase))
            {
                using var db = new MiniBrowserDb();
                // Get all history entries for this user, ordered by visit time
                var rows = db.History
                             .Where(h => h.UserName == u)
                             .OrderBy(h => h.VisitedAt)
                             .Select(h => h.Url)
                             .ToList();

                foreach (var url in rows)
                    Add(url); // Add to history

                return;
            }

            // JSON path and file of the particular user
            var data = FileStore.Load<HistoryData>(FileStore.UserHistoryPath(u));
            if (data == null) return;

            // Load either Full or Items list
            var sourceFull = (data.Full != null && data.Full.Count > 0) ? data.Full : data.Items;
            foreach (var url in sourceFull ?? new List<string>())
                Add(url);

            // Keep index similar to json file if valid, else point to last
            currentIndex = (data.Index >= -1 && data.Index < Count) ? data.Index : Count - 1;
        }

        // Save history for specific user
        public void SaveForUser(string user)
        {
            var u = CleanUserName(user);

            if (StorageMode.Equals("Db", StringComparison.OrdinalIgnoreCase))
            {
                using var db = new MiniBrowserDb();

                // Replace this user's history with the current list
                db.History.RemoveRange(db.History.Where(h => h.UserName == u));
                db.SaveChanges();

                // Add all current history entries
                var now = DateTime.UtcNow;
                var rows = full.Select((url, i) => new DbHistory
                {
                    UserName = u,
                    Url = url,
                    VisitedAt = now.AddMilliseconds(i) // Keep order stable
                });

                db.History.AddRange(rows);
                db.SaveChanges();
                return;
            }

            // JSON path and file of the particular user
            var dto = new HistoryData { Items = new List<string>(items), Full = new List<string>(full), Index = currentIndex };
            FileStore.Save(FileStore.UserHistoryPath(u), dto);
        }

        // Keep usernames consistent
        public static string CleanUserName(string user)
        {
            var u = (user ?? "").Trim().ToLowerInvariant();
            return string.IsNullOrWhiteSpace(u) ? "default" : u;
        }

    }
}
