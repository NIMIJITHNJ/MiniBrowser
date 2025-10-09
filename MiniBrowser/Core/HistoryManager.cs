using System.Collections.Generic;

namespace MiniBrowser.Core
{
    // DTO for persistence
    public class HistoryData
    {
        public List<string> Items { get; set; } = new();
        public int Index { get; set; } = -1;
    }

    public class HistoryManager
    {
        private readonly List<string> _items = new();
        private int _index = -1;

        public bool CanBack    => _index > 0;
        public bool CanForward => _index >= 0 && _index < _items.Count - 1;

        public void Add(string url)
        {
            // consecutive de-dup
            if (_index >= 0 && _items[_index] == url) return;

            // if we had gone back and now navigate, drop forward tail
            if (_index < _items.Count - 1)
                _items.RemoveRange(_index + 1, _items.Count - (_index + 1));

            _items.Add(url);
            _index = _items.Count - 1;
        }

        public string? Back()
        {
            if (!CanBack) return null;
            _index--;
            return _items[_index];
        }

        public string? Forward()
        {
            if (!CanForward) return null;
            _index++;
            return _items[_index];
        }

        public List<string> Recent(int n = 5)
        {
            int start = _items.Count > n ? _items.Count - n : 0;
            return _items.GetRange(start, _items.Count - start);
        }

        // Persistence
        public void Load()
        {
            var data = FileStore.Load<HistoryData>(FileStore.HistoryPath);
            _items.Clear();
            if (data != null)
            {
                _items.AddRange(data.Items ?? new List<string>());
                _index = data.Index >= -1 && data.Index < _items.Count ? data.Index : _items.Count - 1;
            }
            else
            {
                _index = -1;
            }
        }

        public void Save()
        {
            var data = new HistoryData { Items = new List<string>(_items), Index = _index };
            FileStore.Save(FileStore.HistoryPath, data);
        }
    }
}
