using System;
using System.IO;
using System.Text.Json;

namespace MiniBrowser.Core
{
    // Handles saving and loading small JSON files used by the browser
    public static class FileStore
    {
        // Folder under AppData where all files are stored
        public static string AppDir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MiniBrowser");

        public static string HistoryPath   => Path.Combine(AppDir, "history.json");
        public static string BookmarksPath => Path.Combine(AppDir, "bookmarks.json");
        public static string SettingsPath  => Path.Combine(AppDir, "settings.json");

        private static readonly JsonSerializerOptions jsonOptions = new() 
        {
            WriteIndented = true 
        };

        // Making sure that the folder exists before reading or writing
        public static void EnsureDir()
        {
            if (!Directory.Exists(AppDir)) Directory.CreateDirectory(AppDir);
        }

        // Load and deserialise a JSON file and returns default(T) if file is missing or invalid
        public static T? Load<T>(string path)
        {
            try
            {
                EnsureDir();
                if (!File.Exists(path)) return default;
                string json = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(json)) return default;
                return JsonSerializer.Deserialize<T>(json, jsonOptions);
            }
            catch
            {
                // Ignore errors to keep app running even on bad/corrupt files
                return default;
            }
        }

        // Saving an object as JSON to the given path
        public static void Save<T>(string path, T data)
        {
            EnsureDir();
            var json = JsonSerializer.Serialize(data, jsonOptions);
            File.WriteAllText(path, json);
        }

        // Bookmark paths for specific user
        public static string UserBookmarksPath(string user) => Path.Combine(AppDir, $"bookmarks_{SafeFileName(user)}.json");

        // History path for specific user
        public static string UserHistoryPath(string user) => Path.Combine(AppDir, $"history_{SafeFileName(user)}.json");

        // Settings path for specific user
        public static string UserSettingsPath(string user) => Path.Combine(AppDir, $"settings_{SafeFileName(user)}.json");

        // Keep filenames safe
        private static string SafeFileName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            name = name.Trim();
            return string.IsNullOrWhiteSpace(name) ? "default" : name;
        }
    }
}
