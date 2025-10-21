using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MiniBrowser.Core
{
    // Each bookmark belongs to a user and can be stored either in JSON or DB
    public class Bookmark
    {
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";

        // Show a friendly label in lists
        public override string ToString() => string.IsNullOrWhiteSpace(Name) ? Url : Name;
    }

    // To handle bookmark operations and automatically chooses where to save
    public class BookmarkManager
    {
        private readonly List<Bookmark> items = new();

        public string User { get; private set; } = "default";
        public string StorageMode { get; set; } = "Json";           // Json or Db

        public IReadOnlyList<Bookmark> Items => items;
        public int Count => items.Count;

        public BookmarkManager(string currentUser = "default", string mode = "Json")
        {
            User = CleanUserName(currentUser);
            StorageMode = string.IsNullOrWhiteSpace(mode) ? "Json" : mode;
        }

        // Load this user's bookmarks from JSON or DB
        public void LoadForUser(string userName)
        {
            User = CleanUserName(userName);
            items.Clear();

            if (StorageMode.Equals("Db", StringComparison.OrdinalIgnoreCase))
            {
                using var db = new MiniBrowserDb();
                var rows = db.Bookmarks
                             .Where(x => x.UserName == User)
                             .OrderBy(x => x.Name)
                             .AsNoTracking()
                             .ToList();

                foreach (var r in rows)
                {
                    var url = UrlTools.CleanUrl(r.Url);
                    var name = (r.Name ?? "").Trim();
                    if (!items.Any(x => x.Url.Equals(url, StringComparison.OrdinalIgnoreCase)))
                        items.Add(new Bookmark { Name = string.IsNullOrWhiteSpace(name) ? url : name, Url = url });
                }
                return;
            }

            // JSON path and file of the particular user
            var data = FileStore.Load<List<Bookmark>>(FileStore.UserBookmarksPath(User));
            if (data == null) return;

            foreach (var b in data)
            {
                if (b == null || string.IsNullOrWhiteSpace(b.Url)) continue;
                var url = UrlTools.CleanUrl(b.Url);
                var name = (b.Name ?? "").Trim();
                if (!items.Any(x => x.Url.Equals(url, StringComparison.OrdinalIgnoreCase)))
                    items.Add(new Bookmark { Name = string.IsNullOrWhiteSpace(name) ? url : name, Url = url });
            }
        }

        // Save bookmarks for specific user
        public void SaveForUser(string userName)
        {
            User = CleanUserName(userName);

            if (StorageMode.Equals("Db", StringComparison.OrdinalIgnoreCase))
            {
                using var db = new MiniBrowserDb();

                // Replace this user's set with the current list
                db.Bookmarks.RemoveRange(db.Bookmarks.Where(b => b.UserName == User));
                db.SaveChanges();

                var rows = items.Select(b => new DbBookmark
                {
                    UserName = User,
                    Name = b.Name ?? "",
                    Url = b.Url ?? ""
                });

                db.Bookmarks.AddRange(rows);
                db.SaveChanges();
                return;
            }

            // JSON path and user file
            FileStore.Save(FileStore.UserBookmarksPath(User), items);
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
            if (index < 0 || index >= Count) return false;
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
            if (index < 0 || index >= Count) return false;
            items.RemoveAt(index);
            return true;
        }

        // Removing all bookmarks
        public void Clear()
        {
            items.Clear();
        }

        // Keep usernames consistent
        public static string CleanUserName(string user)
        {
            var u = (user ?? "").Trim().ToLowerInvariant();
            return string.IsNullOrWhiteSpace(u) ? "default" : u;
        }
    }
}
