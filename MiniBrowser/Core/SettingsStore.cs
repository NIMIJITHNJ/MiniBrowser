namespace MiniBrowser.Core
{
    // Simple POCO for persistence
    public class SettingsData
    {
        public string HomeUrl { get; set; } = UrlTools.DefaultHome;
    }

    public class SettingsStore
    {
        public SettingsData Data { get; private set; } = new();

        public void Load()
        {
            var loaded = FileStore.Load<SettingsData>(FileStore.SettingsPath);
            Data = loaded ?? new SettingsData();
        }

        public void Save()
        {
            FileStore.Save(FileStore.SettingsPath, Data);
        }
    }
}
