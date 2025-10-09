using System.Collections.Generic;
using System.Linq;

namespace MiniBrowser.Core
{
    public class Bookmark
    {
        public string Name { get; set; } = "";
        public string Url  { get; set; } = "";

        public override string ToString()
        {
            // Show the bookmark’s display name nicely in lists
            return string.IsNullOrWhiteSpace(Name) ? Url : $"{Name}";
        }
    }

    public class BookmarkManager
    {
        private readonly List<Bookmark> _items = new();

        public IReadOnlyList<Bookmark> All() => _items;

        public void Load()
        {
            var data = FileStore.Load<List<Bookmark>>(FileStore.BookmarksPath);
            _items.Clear();
            if (data != null) _items.AddRange(data.Where(b => !string.IsNullOrWhiteSpace(b.Url)));
        }

        public void Save()
        {
            FileStore.Save(FileStore.BookmarksPath, _items);
        }

        public void Add(string name, string url)
        {
            // Avoid duplicate URLs; rename if needed
            if (_items.Any(b => b.Url == url)) return;
            _items.Add(new Bookmark { Name = string.IsNullOrWhiteSpace(name) ? url : name.Trim(), Url = url.Trim() });
        }

        public bool Edit(int index, string? newName = null, string? newUrl = null)
        {
            if (index < 0 || index >= _items.Count) return false;
            var b = _items[index];
            if (!string.IsNullOrWhiteSpace(newName)) b.Name = newName.Trim();
            if (!string.IsNullOrWhiteSpace(newUrl))
            {
                var url = newUrl.Trim();
                if (_items.Where((x, i) => i != index).Any(x => x.Url == url)) return false; // no duplicates
                b.Url = url;
            }
            return true;
        }

        public bool Delete(int index)
        {
            if (index < 0 || index >= _items.Count) return false;
            _items.RemoveAt(index);
            return true;
        }
    }
}
