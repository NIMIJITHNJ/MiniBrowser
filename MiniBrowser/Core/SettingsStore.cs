namespace MiniBrowser.Core
{
    // Simple POCO for persistence
    public class SettingsData
    {
        public string HomeUrl { get; set; } = UrlTools.DefaultHome;
        public string CurrentUser { get; set; } = "default";
        public string StorageMode { get; set; } = "Json";
    }

    // To loads/saves simple app settings, using for Home URL
    public class SettingsStore
    {
        public SettingsData Data { get; private set; } = new();

        // Setting home URL and keep it in memory
        public void SetHome(string url)
        {
            var clean = string.IsNullOrWhiteSpace(url)
                ? UrlTools.DefaultHome
                : UrlTools.CleanUrl(url);
            Data.HomeUrl = clean;
        }

        // Reset to default home
        public void ResetHome()
        {
            Data.HomeUrl = UrlTools.DefaultHome;
        }

        // Setting current user
        public void SetUser(string userName)
        {
            var name = string.IsNullOrWhiteSpace(userName) ? "default" : userName.Trim();
            Data.CurrentUser = name;
        }

        // Setting storage mode
        public void SetStorageMode(string mode)
        {
            Data.StorageMode = string.Equals(mode, "Db", System.StringComparison.OrdinalIgnoreCase) ? "Db" : "Json";
        }

        // Load settings and falls back to default and normalises
        public void Load()
        {
            var loaded = FileStore.Load<SettingsData>(FileStore.SettingsPath);
            Data = loaded ?? new SettingsData();
            // Normalising whatever loaded
            SetHome(Data.HomeUrl);
            SetUser(Data.CurrentUser);
            SetStorageMode(Data.StorageMode);
        }

        // Save current settings
        public void Save()
        {
            FileStore.Save(FileStore.SettingsPath, Data);
        }

        // Load settings for specific user
        public void LoadForUser(string user)
        {
            var u = CleanUserName(user);

            // If using DB, load from there
            if (Data.StorageMode.Equals("Db", StringComparison.OrdinalIgnoreCase))
            {
                using var db = new MiniBrowserDb();
                var row = db.Settings.FirstOrDefault(s => s.UserName == u);
                if (row == null)
                {
                    SetHome(UrlTools.DefaultHome);
                    return;
                }
                SetHome(row.HomeUrl);
                return;
            }

            // JSON path and file of the particular user
            var loaded = FileStore.Load<SettingsData>(FileStore.UserSettingsPath(u));
            if (loaded == null) { SetHome(UrlTools.DefaultHome); return; }
            SetHome(loaded.HomeUrl);
        }

        public void SaveForUser(string user)
        {
            var u = CleanUserName(user);

            // If using DB, save to there
            if (Data.StorageMode.Equals("Db", StringComparison.OrdinalIgnoreCase))
            {
                using var db = new MiniBrowserDb();
                var row = db.Settings.FirstOrDefault(s => s.UserName == u);
                if (row == null)
                {
                    db.Settings.Add(new DbSetting { UserName = u, HomeUrl = Data.HomeUrl });
                }
                else
                {
                    row.HomeUrl = Data.HomeUrl;
                    db.Settings.Update(row);
                }
                db.SaveChanges();
                return;
            }

            // Save to JSON file for this user
            var dto = new SettingsData { HomeUrl = Data.HomeUrl, CurrentUser = u, StorageMode = Data.StorageMode };
            FileStore.Save(FileStore.UserSettingsPath(u), dto);
        }

        // Keep usernames consistent
        public static string CleanUserName(string user)
        {
            var u = (user ?? "").Trim().ToLowerInvariant();
            return string.IsNullOrWhiteSpace(u) ? "default" : u;
        }
    }
}
