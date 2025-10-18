using System.Collections.Generic;
using System.Linq;

namespace MiniBrowser.Core
{
    public class Bookmark
    {
        public string Name { get; set; } = "";
        public string Url  { get; set; } = "";

        // Showing a friendly label in lists
        public override string ToString() => string.IsNullOrWhiteSpace(Name) ? Url : Name;
    }

    // Keeping a simple in-memory list of bookmarks with load/save helpers
    public class BookmarkManager
    {
        private readonly List<Bookmark> items = new();

        public IReadOnlyList<Bookmark> Items => items;
        public int Count => items.Count;

        public void Load()
        {
            var data = FileStore.Load<List<Bookmark>>(FileStore.BookmarksPath);
            items.Clear();
            if (data == null) return;

            foreach (var b in data ?? new List<Bookmark>())
            {
                // Skip if bookmark or URL is missing
                if (b == null || string.IsNullOrWhiteSpace(b.Url))
                    continue;
                // Trim and normalise what we load as well
                var url = UrlTools.CleanUrl(b.Url);
                var name = (b.Name ?? "").Trim();

                // Avoid duplicates
                bool exists = items.Any(x => string.Equals(x.Url, url, StringComparison.OrdinalIgnoreCase));
                if (!exists) items.Add(new Bookmark { Name = string.IsNullOrWhiteSpace(name) ? url : name, Url = url });
            }
        }

        public void Save()
        {
            FileStore.Save(FileStore.BookmarksPath, items);
        }

        // Load bookmarks for specific user
        public void LoadForUser(string user)
        {
            items.Clear();
            var data = FileStore.Load<List<Bookmark>>(FileStore.UserBookmarksPath(user));
            if (data == null) return;

            foreach (var b in data)
            {
                if (b == null || string.IsNullOrWhiteSpace(b.Url)) continue;
                var url = UrlTools.CleanUrl(b.Url);
                var name = (b.Name ?? "").Trim();
                if (!items.Any(x => string.Equals(x.Url, url, StringComparison.OrdinalIgnoreCase)))
                    items.Add(new Bookmark { Name = string.IsNullOrWhiteSpace(name) ? url : name, Url = url });
            }
        }

        // Save bookmarks for specific user
        public void SaveForUser(string user)
        {
            FileStore.Save(FileStore.UserBookmarksPath(user), items);
        }

        // Adding a bookmark if URL not already available
        public void Add(string name, string url)
        {
            url = UrlTools.CleanUrl(url);
            name = (name ?? "").Trim();

            // Avoid duplicate URLs and rename if needed
            if (items.Any(b => string.Equals(b.Url, url, StringComparison.OrdinalIgnoreCase)))
                return;

            items.Add(new Bookmark
            {
                Name = string.IsNullOrWhiteSpace(name) ? url : name,
                Url = url
            });
        }

        // Editing the name and/or URL at index, then returns true if updated
        public bool Edit(int index, string? newName = null, string? newUrl = null)
        {
            if (index < 0 || index >= items.Count) return false;
            var name = newName?.Trim();
            var url = string.IsNullOrWhiteSpace(newUrl) ? null : UrlTools.CleanUrl(newUrl!);

            // If URL is changing, keep it unique
            if (url != null && items.Where((_, i) => i != index).Any(x => string.Equals(x.Url, url, StringComparison.OrdinalIgnoreCase)))
                return false;

            if (!string.IsNullOrWhiteSpace(name)) items[index].Name = name!;
            if (url != null) items[index].Url = url;

            return true;
        }

        // Deleting bookmark at index, then returns true if removed
        public bool Delete(int index)
        {
            if (index < 0 || index >= items.Count) return false;
            items.RemoveAt(index);
            return true;
        }

        // Removing all bookmarks
        public void Clear()
        {
            items.Clear();
        }
    }
}
