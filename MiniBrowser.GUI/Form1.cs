using System.ComponentModel;
using MiniBrowser.Core;

namespace MiniBrowser.GUI
{
    public partial class Form1 : Form
    {
        // Core services
        private readonly HttpClientService http = new();
        private readonly SettingsStore settings = new();
        private readonly HistoryManager history = new();
        private readonly BookmarkManager bookmarks = new();

        private string currentUrl = "";

        public Form1()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Ensure DB schema if using DB mode
            settings.Load();
            if (string.Equals(settings.Data.StorageMode, "Db", StringComparison.OrdinalIgnoreCase))
                MiniBrowserDb.EnsureSchema();

            // Load user specific data
            var user = settings.Data.CurrentUser;
            settings.LoadForUser(user);
            bookmarks.StorageMode = settings.Data.StorageMode;
            history.StorageMode = settings.Data.StorageMode;
            history.LoadForUser(user);
            bookmarks.LoadForUser(user);

            this.Text = $"MiniBrowser 1.0 – User: {user}";

            // Wire events
            btnGo.Click += async (_, __) => await NavigateAsync(txtAddress.Text);
            btnReload.Click += async (_, __) => { if (!string.IsNullOrWhiteSpace(currentUrl)) await NavigateAsync(currentUrl); };
            btnHome.Click += async (_, __) => await NavigateAsync(settings.Data.HomeUrl);
            btnSetHome.Click += (_, __) => SetHomeInteractive();
            btnBack.Click += async (_, __) => await BackAsync();
            btnForward.Click += async (_, __) => await ForwardAsync();

            txtAddress.KeyDown += async (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    await NavigateAsync(txtAddress.Text);
                }
            };

            lstLinks.DoubleClick += async (_, __) => { if (lstLinks.SelectedItem is string u) await NavigateAsync(u); };
            lstHistory.DoubleClick += async (_, __) => { if (lstHistory.SelectedItem is string u) await NavigateAsync(u); };
            lstBookmarks.DoubleClick += async (_, __) => { if (lstBookmarks.SelectedItem is Bookmark b) await NavigateAsync(b.Url); };

            btnAddBm.Click += (_, __) => AddBookmarkInteractive();
            btnEditBm.Click += (_, __) => EditBookmarkInteractive();
            btnDelBm.Click += (_, __) => DeleteBookmarkSelected();

            btnRender.Click += async (_, __) =>
            {
                var on = btnRender.Checked;

                // flip visibility
                webView.Visible = on;
                txtHtml.Visible = !on;

                if (on)
                {
                    // init once
                    try { await webView.EnsureCoreWebView2Async(); }
                    catch { btnRender.Checked = false; webView.Visible = false; txtHtml.Visible = true; return; }

                    // if we already have a current page, show it rendered
                    if (!string.IsNullOrWhiteSpace(currentUrl))
                        webView.Source = new Uri(currentUrl);
                }
            };


            // Menu events
            mnuSetHome.Click += (_, __) => SetHomeInteractive();
            mnuAddBookmark.Click += (_, __) => AddBookmarkInteractive();
            mnuEditBookmark.Click += (_, __) => EditBookmarkInteractive();
            mnuDeleteBookmark.Click += (_, __) => DeleteBookmarkSelected();
            mnuAbout.Click += (_, __) => MessageBox.Show("MiniBrowser 1.0\nCreated by Nimijith", "About");
            mnuSwitchUser.Click += (_, __) => SwitchUserInteractive();

            mnuModeJson.Checked = settings.Data.StorageMode.Equals("Json", StringComparison.OrdinalIgnoreCase);
            mnuModeDb.Checked = !mnuModeJson.Checked;

            mnuModeJson.Click += (_, __) => SwitchStorageMode("Json");
            mnuModeDb.Click += (_, __) => SwitchStorageMode("Db");

            // If render mode is ON, ensure WebView is initialized
            if (btnRender.Checked)
            {
                try { await webView.EnsureCoreWebView2Async(); }
                catch { btnRender.Checked = false; webView.Visible = false; txtHtml.Visible = true; }
            }


            // First page
            txtAddress.Text = settings.Data.HomeUrl;
            await NavigateAsync(settings.Data.HomeUrl);
        }

        private async Task NavigateAsync(string input)
        {
            var target = string.IsNullOrWhiteSpace(input) ? settings.Data.HomeUrl : UrlTools.CleanUrl(input);
            txtAddress.Text = target;

            var result = await http.GetAsync(target);

            lblStatus.Text = $"{result.StatusCode} {result.Reason}";
            lblTitle.Text = ExtractTitle(result.Body ?? "");

            lblTopStatus.Text = $"{(int)result.StatusCode} {result.Reason}";
            lblTopTitle.Text = $"| {ExtractTitle(result.Body ?? "")}";

            // Show body in the TextBox
            var body = result.Body ?? string.Empty;
            body = body.Replace("\r\n", "\n").Replace("\n", "\r\n");

            txtHtml.Text = body;
            txtHtml.SelectionStart = 0;
            txtHtml.SelectionLength = 0;

            // If render mode is ON and webview ready, navigate it
            if (btnRender.Checked && webView?.CoreWebView2 != null)
            {
                // Use the final URL if available, else target
                var go = result.FinalUrl ?? target;
                if (Uri.TryCreate(go, UriKind.Absolute, out var u))
                    webView.Source = u;
            }


            var links = LinkExtractor.FirstFive(result.Body ?? "", result.FinalUrl ?? target);

            lstLinks.BeginUpdate();
            lstLinks.Items.Clear();
            lstLinks.Items.AddRange(links.ToArray());
            lstLinks.EndUpdate();

            if (result.StatusCode != 0)
            {
                currentUrl = result.FinalUrl ?? target;
                history.Add(currentUrl);
                history.SaveForUser(settings.Data.CurrentUser);
            }

            RefreshListsAndButtons();
        }

        private async Task BackAsync()
        {
            var u = history.Back();
            if (u == null) { MessageBox.Show("No back history."); return; }
            await NavigateAsync(u);
        }

        private async Task ForwardAsync()
        {
            var u = history.Forward();
            if (u == null) { MessageBox.Show("No forward history."); return; }
            await NavigateAsync(u);
        }

        private void RefreshListsAndButtons()
        {
            lstHistory.Items.Clear();
            foreach (var u in history.Recent(50)) lstHistory.Items.Add(u);

            lstBookmarks.Items.Clear();
            foreach (var b in bookmarks.Items) lstBookmarks.Items.Add(b);

            btnBack.Enabled = history.CanBack;
            btnForward.Enabled = history.CanForward;
            btnReload.Enabled = !string.IsNullOrWhiteSpace(currentUrl);
        }

        private void SetHomeInteractive()
        {
            var current = string.IsNullOrWhiteSpace(currentUrl) ? settings.Data.HomeUrl : currentUrl;
            var input = Prompt("Set Home URL:", current);
            // Cancelled
            if (input == null) return;
            // Normalises and sets default if blank
            settings.SetHome(string.IsNullOrWhiteSpace(input) ? current : input);
            // Save settings per user
            settings.SaveForUser(settings.Data.CurrentUser);
            // Reflects new home in the address box
            txtAddress.Text = settings.Data.HomeUrl;
        }

        private static string ExtractTitle(string html)
        {
            if (string.IsNullOrEmpty(html)) return "(no title)";
            var s = html.IndexOf("<title", StringComparison.OrdinalIgnoreCase); if (s < 0) return "(no title)";
            s = html.IndexOf(">", s); if (s < 0) return "(no title)";
            var e = html.IndexOf("</title>", s, StringComparison.OrdinalIgnoreCase); if (e < 0) return "(no title)";
            return html.Substring(s + 1, e - s - 1).Trim();
        }


        private void AddBookmarkInteractive()
        {
            var current = string.IsNullOrWhiteSpace(currentUrl) ? settings.Data.HomeUrl : currentUrl;
            var name = Prompt("Bookmark name:", ExtractTitle(txtHtml.Text)); if (name == null) return;
            var url = Prompt("Bookmark URL:", current); if (url == null) return;

            bookmarks.Add(name, UrlTools.CleanUrl(url));
            bookmarks.SaveForUser(settings.Data.CurrentUser);
            RefreshListsAndButtons();
        }

        private void EditBookmarkInteractive()
        {
            if (lstBookmarks.SelectedIndex < 0) { MessageBox.Show("Select a bookmark."); return; }
            var idx = lstBookmarks.SelectedIndex;
            if (lstBookmarks.Items[idx] is not Bookmark b) return;

            var name = Prompt("New name:", b.Name);
            var url = Prompt("New URL:", b.Url);

            var ok = bookmarks.Edit(idx,
                string.IsNullOrWhiteSpace(name) ? null : name,
                string.IsNullOrWhiteSpace(url) ? null : UrlTools.CleanUrl(url));
            if (ok) bookmarks.SaveForUser(settings.Data.CurrentUser);
            RefreshListsAndButtons();
        }

        private void DeleteBookmarkSelected()
        {
            if (lstBookmarks.SelectedIndex < 0) { MessageBox.Show("Select a bookmark to delete."); return; }
            if (bookmarks.Delete(lstBookmarks.SelectedIndex))
            {
                bookmarks.SaveForUser(settings.Data.CurrentUser);
                RefreshListsAndButtons();
            }
        }

        private static string? Prompt(string message, string? defaultValue = "")
        {
            using var f = new Form() { Width = 420, Height = 150, FormBorderStyle = FormBorderStyle.FixedDialog, Text = "Input", StartPosition = FormStartPosition.CenterParent };
            var lbl = new Label() { Left = 10, Top = 10, Width = 380, Text = message };
            var tb = new TextBox() { Left = 10, Top = 35, Width = 380, Text = defaultValue ?? "" };
            var ok = new Button() { Text = "OK", Left = 220, Width = 80, Top = 70, DialogResult = DialogResult.OK };
            var cancel = new Button() { Text = "Cancel", Left = 310, Width = 80, Top = 70, DialogResult = DialogResult.Cancel };
            f.Controls.AddRange(new Control[] { lbl, tb, ok, cancel }); f.AcceptButton = ok; f.CancelButton = cancel;
            return f.ShowDialog() == DialogResult.OK ? tb.Text : null;
        }

        // To make sure everything is saved when the browser closes; already saving after each action. This is a final safety step
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Always call base first
            base.OnFormClosing(e);
            webView?.Dispose();
            var user = settings.Data.CurrentUser;

            // Save everything before exit
            history.SaveForUser(user);
            bookmarks.SaveForUser(user);
            settings.Save();
        }

        private async void SwitchUserInteractive()
        {
            var currentUser = settings.Data.CurrentUser;

            // Ask for a name and keep current as default suggestion
            var input = Prompt("User name (new, existing or leave blank for 'default'):", currentUser);
            if (input == null) return; // user cancelled

            // Keep original user name for display
            var displayUser = string.IsNullOrWhiteSpace(input) ? "default" : input.Trim();

            // Cleaned version of user name internally for storage
            var targetUser = CleanUserName(displayUser);

            // Check if profile file already exists
            bool alreadyExists = File.Exists(FileStore.UserSettingsPath(targetUser));

            // Set and save selected user
            settings.SetUser(targetUser);
            // To remember last used user globally
            settings.Save();

            // Load this user's settings and will give default home if first time
            settings.LoadForUser(targetUser);
            bookmarks.StorageMode = settings.Data.StorageMode;
            history.StorageMode = settings.Data.StorageMode;

            // If new user, save initial empty data files
            if (!alreadyExists)
            {
                settings.SaveForUser(targetUser);
                MessageBox.Show($"Welcome, {displayUser}! A new profile has been created.", "New User");
            }

            // Reload per-user data
            history.LoadForUser(targetUser);
            bookmarks.LoadForUser(targetUser);

            RefreshListsAndButtons();

            this.Text = $"MiniBrowser 1.0 – User: {displayUser}";

            // Navigate to the user's home page
            txtAddress.Text = settings.Data.HomeUrl;
            await NavigateAsync(settings.Data.HomeUrl);
        }

        // Switch between JSON and DB storage modes
        private void SwitchStorageMode(string mode)
        {
            // No change
            if (string.Equals(settings.Data.StorageMode, mode, StringComparison.OrdinalIgnoreCase))
                return;

            // Set and save new mode
            settings.SetStorageMode(mode);
            settings.Save();

            // Update menu checks
            bool useJson = mode.Equals("Json", StringComparison.OrdinalIgnoreCase);
            mnuModeJson.Checked = useJson;
            mnuModeDb.Checked = !useJson;

            // Ensure DB schema if switching to DB
            if (!useJson)
                MiniBrowserDb.EnsureSchema();

            // Update storage mode in managers
            bookmarks.StorageMode = settings.Data.StorageMode;
            history.StorageMode = settings.Data.StorageMode;

            // Reload current user's data
            var user = settings.Data.CurrentUser;
            history.LoadForUser(user);
            bookmarks.LoadForUser(user);
            RefreshListsAndButtons();

            MessageBox.Show($"Storage mode switched to {mode}.", "Storage");
        }

        // Keep usernames consistent
        public static string CleanUserName(string user)
        {
            var u = (user ?? "").Trim().ToLowerInvariant();
            return string.IsNullOrWhiteSpace(u) ? "default" : u;
        }

        private void jSONToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
