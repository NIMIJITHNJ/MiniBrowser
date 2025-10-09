using System;
using System.IO;
using System.Text.Json;

namespace MiniBrowser.Core
{
    public static class FileStore
    {
        // Change only here if you want a different folder name
        public static string AppDir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MiniBrowser");

        public static string HistoryPath   => Path.Combine(AppDir, "history.json");
        public static string BookmarksPath => Path.Combine(AppDir, "bookmarks.json");
        public static string SettingsPath  => Path.Combine(AppDir, "settings.json");

        static readonly JsonSerializerOptions J = new() { WriteIndented = true };

        public static void EnsureDir()
        {
            if (!Directory.Exists(AppDir)) Directory.CreateDirectory(AppDir);
        }

        // Load JSON file → T (or default if missing/corrupt)
        public static T? Load<T>(string path)
        {
            try
            {
                EnsureDir();
                if (!File.Exists(path)) return default;
                string json = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(json)) return default;
                return JsonSerializer.Deserialize<T>(json, J);
            }
            catch
            {
                // keep the app alive on bad files
                return default;
            }
        }

        // Save object → JSON file
        public static void Save<T>(string path, T data)
        {
            EnsureDir();
            var json = JsonSerializer.Serialize(data, J);
            File.WriteAllText(path, json);
        }
    }
}
