using System.ComponentModel;
using MiniBrowser.Core;

namespace MiniBrowser.GUI
{
    public partial class Form1 : Form
    {
        // Core services
        private readonly HttpClientService _service = new();
        private readonly SettingsStore _settings = new();
        private readonly HistoryManager _history = new();
        private readonly BookmarkManager _bookmarks = new();

        private string _currentFinalUrl = "";

        public Form1()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Load persisted data
            _settings.Load();
            _history.Load();
            _bookmarks.Load();

            // Wire events
            btnGo.Click += async (_, __) => await NavigateAsync(txtAddress.Text);
            btnReload.Click += async (_, __) => { if (!string.IsNullOrWhiteSpace(_currentFinalUrl)) await NavigateAsync(_currentFinalUrl); };
            btnHome.Click += async (_, __) => await NavigateAsync(_settings.Data.HomeUrl);
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

            // First page
            txtAddress.Text = _settings.Data.HomeUrl;
            await NavigateAsync(_settings.Data.HomeUrl);
        }

        private async Task NavigateAsync(string input)
        {
            var target = string.IsNullOrWhiteSpace(input) ? _settings.Data.HomeUrl : UrlTools.CleanUrl(input);
            txtAddress.Text = target;

            var result = await _service.GetAsync(target);

            lblStatus.Text = $"{result.StatusCode} {result.Reason}";
            lblTitle.Text = ExtractTitle(result.Body ?? "");
            rtbHtml.Text = result.Body ?? "";

            var links = LinkExtractor.FirstFive(result.Body ?? "", result.FinalUrl ?? target);
            lstLinks.Items.Clear();
            foreach (var u in links) lstLinks.Items.Add(u);

            if (result.StatusCode != 0)
            {
                _currentFinalUrl = result.FinalUrl ?? target;
                _history.Add(_currentFinalUrl);
                _history.Save();
            }

            RefreshListsAndButtons();
        }

        private async Task BackAsync()
        {
            var u = _history.Back();
            if (u == null) { MessageBox.Show("No back history."); return; }
            await NavigateAsync(u);
        }

        private async Task ForwardAsync()
        {
            var u = _history.Forward();
            if (u == null) { MessageBox.Show("No forward history."); return; }
            await NavigateAsync(u);
        }

        private void RefreshListsAndButtons()
        {
            lstHistory.Items.Clear();
            foreach (var u in _history.Recent(50)) lstHistory.Items.Add(u);

            lstBookmarks.Items.Clear();
            foreach (var b in _bookmarks.All()) lstBookmarks.Items.Add(b);

            btnBack.Enabled = _history.CanBack;
            btnForward.Enabled = _history.CanForward;
            btnReload.Enabled = !string.IsNullOrWhiteSpace(_currentFinalUrl);
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
            var current = string.IsNullOrWhiteSpace(_currentFinalUrl) ? _settings.Data.HomeUrl : _currentFinalUrl;
            var name = Prompt("Bookmark name:", ExtractTitle(rtbHtml.Text)); if (name == null) return;
            var url = Prompt("Bookmark URL (blank = current):", current); if (url == null) return;

            _bookmarks.Add(name, UrlTools.CleanUrl(url));
            _bookmarks.Save();
            RefreshListsAndButtons();
        }

        private void EditBookmarkInteractive()
        {
            if (lstBookmarks.SelectedIndex < 0) { MessageBox.Show("Select a bookmark."); return; }
            var idx = lstBookmarks.SelectedIndex;
            if (lstBookmarks.Items[idx] is not Bookmark b) return;

            var name = Prompt("New name (blank = keep):", b.Name);
            var url = Prompt("New URL (blank = keep):", b.Url);

            var ok = _bookmarks.Edit(idx,
                string.IsNullOrWhiteSpace(name) ? null : name,
                string.IsNullOrWhiteSpace(url) ? null : UrlTools.CleanUrl(url));
            if (ok) _bookmarks.Save();
            RefreshListsAndButtons();
        }

        private void DeleteBookmarkSelected()
        {
            if (lstBookmarks.SelectedIndex < 0) { MessageBox.Show("Select a bookmark to delete."); return; }
            if (_bookmarks.Delete(lstBookmarks.SelectedIndex))
            {
                _bookmarks.Save();
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

        private void lstBookmarks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
