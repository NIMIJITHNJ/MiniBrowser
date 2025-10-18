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
            // Keep CurrentUser consistent
            SetUser(user);

            var loaded = FileStore.Load<SettingsData>(FileStore.UserSettingsPath(user));
            if (loaded == null)
            {
                // First time default home for this user
                SetHome(UrlTools.DefaultHome);
                return;
            }

            // Normalise and keep user
            SetHome(loaded.HomeUrl);
            SetUser(user);
            SetStorageMode(loaded.StorageMode);
        }

        public void SaveForUser(string user)
        {
            // Ensure Data.CurrentUser is correct and URL is clean
            SetUser(user);
            SetHome(Data.HomeUrl);
            FileStore.Save(FileStore.UserSettingsPath(user), Data);
        }
    }
}
