using System.Collections.Generic;

namespace MiniBrowser.Core
{
    // Saved shape on disk
    public class HistoryData
    {
        public List<string> Items { get; set; } = new();
        public int Index { get; set; } = -1;
    }

    // Keeps a simple browsing history and a pointer to the current page
    public class HistoryManager
    {
        private readonly List<string> items = new();
        private int currentIndex = -1;

        public bool CanBack    => currentIndex > 0;
        public bool CanForward => currentIndex >= 0 && currentIndex < items.Count - 1;

        public int Count => items.Count;
        public string? Current => currentIndex >= 0 && currentIndex < items.Count ? items[currentIndex] : null;

        // Add a page to history. If we had gone back, drop the forward part and avoids adding the same URL twice in a row
        public void Add(string url)
        {
            // Normalising the URL
            url = UrlTools.CleanUrl(url);

            // Ignore if same as current to prevents consecutive duplicates
            if (currentIndex >= 0 && string.Equals(items[currentIndex], url, StringComparison.OrdinalIgnoreCase))
                return;

            // If user went back and now visits a new page, drop all forward entries
            if (currentIndex < items.Count - 1)
                items.RemoveRange(currentIndex + 1, items.Count - (currentIndex + 1));

            // Add the new page and update the index
            items.Add(url);
            currentIndex = items.Count - 1;
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
            if (n <= 0 || items.Count == 0) return new List<string>();
            int start = Math.Max(0, items.Count - n);
            return items.GetRange(start, items.Count - start);
        }

        // Clear all history
        public void Clear()
        {
            items.Clear();
            currentIndex = -1;
        }

        // Persistence
        public void Load()
        {
            var data = FileStore.Load<HistoryData>(FileStore.HistoryPath);
            items.Clear();
            if (data is { Items: { } loaded })
                items.AddRange(loaded);

            // Fix index to valid range or -1
            currentIndex = (data != null && data.Index >= -1 && data.Index < items.Count)
                ? data.Index : items.Count - 1;
        }

        public void Save()
        {
            var data = new HistoryData { Items = new List<string>(items), Index = currentIndex };
            FileStore.Save(FileStore.HistoryPath, data);
        }

        // Load history for specific user
        public void LoadForUser(string user)
        {
            Clear();
            var data = FileStore.Load<HistoryData>(FileStore.UserHistoryPath(user));
            if (data?.Items is { Count: > 0 })
            {
                foreach (var u in data.Items) Add(u);
            }
        }

        // Save history for specific user
        public void SaveForUser(string user)
        {
            var data = new HistoryData
            {
                Items = new List<string>(items),
                Index = currentIndex
            };
            FileStore.Save(FileStore.UserHistoryPath(user), data);
        }

    }
}
