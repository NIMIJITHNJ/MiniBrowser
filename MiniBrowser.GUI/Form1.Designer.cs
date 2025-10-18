namespace MiniBrowser.GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip = new ToolStrip();
            btnBack = new ToolStripButton();
            btnForward = new ToolStripButton();
            btnReload = new ToolStripButton();
            btnHome = new ToolStripButton();
            btnSetHome = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            txtAddress = new ToolStripTextBox();
            btnGo = new ToolStripButton();
            splitMain = new SplitContainer();
            txtHtml = new TextBox();
            tabs = new TabControl();
            tabPage1 = new TabPage();
            lstLinks = new ListBox();
            tabPage2 = new TabPage();
            lstBookmarks = new ListBox();
            btnDelBm = new Button();
            btnEditBm = new Button();
            btnAddBm = new Button();
            tabPage3 = new TabPage();
            lstHistory = new ListBox();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblTitle = new ToolStripStatusLabel();
            panelTopHeader = new Panel();
            lblTopTitle = new Label();
            lblTopStatus = new Label();
            menuStrip1 = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            mnuSetHome = new ToolStripMenuItem();
            bookmarkToolStripMenuItem = new ToolStripMenuItem();
            mnuAddBookmark = new ToolStripMenuItem();
            mnuEditBookmark = new ToolStripMenuItem();
            mnuDeleteBookmark = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            mnuAbout = new ToolStripMenuItem();
            userToolStripMenuItem = new ToolStripMenuItem();
            mnuSwitchUser = new ToolStripMenuItem();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            btnRender = new ToolStripButton();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            tabs.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            statusStrip1.SuspendLayout();
            panelTopHeader.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(20, 20);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnBack, btnForward, btnReload, btnHome, btnSetHome, toolStripSeparator1, txtAddress, btnGo, btnRender });
            toolStrip.Location = new Point(0, 28);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(1213, 27);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";            
            // 
            // btnBack
            // 
            btnBack.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnBack.Image = (Image)resources.GetObject("btnBack.Image");
            btnBack.ImageTransparentColor = Color.Magenta;
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(61, 24);
            btnBack.Text = "◀ Back";
            // 
            // btnForward
            // 
            btnForward.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnForward.Image = (Image)resources.GetObject("btnForward.Image");
            btnForward.ImageTransparentColor = Color.Magenta;
            btnForward.Name = "btnForward";
            btnForward.Size = new Size(84, 24);
            btnForward.Text = "Forward ▶";
            // 
            // btnReload
            // 
            btnReload.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnReload.Image = (Image)resources.GetObject("btnReload.Image");
            btnReload.ImageTransparentColor = Color.Magenta;
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(60, 24);
            btnReload.Text = "Reload";
            // 
            // btnHome
            // 
            btnHome.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnHome.Image = (Image)resources.GetObject("btnHome.Image");
            btnHome.ImageTransparentColor = Color.Magenta;
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(54, 24);
            btnHome.Text = "Home";
            // 
            // btnSetHome
            // 
            btnSetHome.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnSetHome.Image = (Image)resources.GetObject("btnSetHome.Image");
            btnSetHome.ImageTransparentColor = Color.Magenta;
            btnSetHome.Name = "btnSetHome";
            btnSetHome.Size = new Size(79, 24);
            btnSetHome.Text = "Set Home";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // txtAddress
            // 
            txtAddress.AutoSize = false;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(600, 27);
            // 
            // btnGo
            // 
            btnGo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnGo.Image = (Image)resources.GetObject("btnGo.Image");
            btnGo.ImageTransparentColor = Color.Magenta;
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(32, 24);
            btnGo.Text = "Go";
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 79);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(webView);
            splitMain.Panel1.Controls.Add(txtHtml);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(tabs);
            splitMain.Size = new Size(1213, 427);
            splitMain.SplitterDistance = 909;
            splitMain.TabIndex = 1;
            // 
            // txtHtml
            // 
            txtHtml.BackColor = SystemColors.Window;
            txtHtml.BorderStyle = BorderStyle.None;
            txtHtml.Dock = DockStyle.Fill;
            txtHtml.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtHtml.Location = new Point(0, 0);
            txtHtml.Multiline = true;
            txtHtml.Name = "txtHtml";
            txtHtml.ReadOnly = true;
            txtHtml.ScrollBars = ScrollBars.Both;
            txtHtml.Size = new Size(909, 427);
            txtHtml.TabIndex = 0;
            txtHtml.WordWrap = false;
            // 
            // tabs
            // 
            tabs.Controls.Add(tabPage1);
            tabs.Controls.Add(tabPage2);
            tabs.Controls.Add(tabPage3);
            tabs.Dock = DockStyle.Fill;
            tabs.Location = new Point(0, 0);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.Size = new Size(300, 427);
            tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(lstLinks);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(292, 394);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Links";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // lstLinks
            // 
            lstLinks.Dock = DockStyle.Fill;
            lstLinks.FormattingEnabled = true;
            lstLinks.Location = new Point(3, 3);
            lstLinks.Name = "lstLinks";
            lstLinks.Size = new Size(286, 388);
            lstLinks.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(lstBookmarks);
            tabPage2.Controls.Add(btnDelBm);
            tabPage2.Controls.Add(btnEditBm);
            tabPage2.Controls.Add(btnAddBm);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(292, 394);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Bookmarks";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // lstBookmarks
            // 
            lstBookmarks.Dock = DockStyle.Fill;
            lstBookmarks.FormattingEnabled = true;
            lstBookmarks.Location = new Point(3, 93);
            lstBookmarks.Margin = new Padding(3, 10, 3, 3);
            lstBookmarks.Name = "lstBookmarks";
            lstBookmarks.Size = new Size(286, 298);
            lstBookmarks.TabIndex = 3;
            // 
            // btnDelBm
            // 
            btnDelBm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnDelBm.Dock = DockStyle.Top;
            btnDelBm.Location = new Point(3, 63);
            btnDelBm.Name = "btnDelBm";
            btnDelBm.Size = new Size(286, 30);
            btnDelBm.TabIndex = 2;
            btnDelBm.Text = "Delete";
            btnDelBm.UseVisualStyleBackColor = true;
            // 
            // btnEditBm
            // 
            btnEditBm.Dock = DockStyle.Top;
            btnEditBm.Location = new Point(3, 33);
            btnEditBm.Name = "btnEditBm";
            btnEditBm.Size = new Size(286, 30);
            btnEditBm.TabIndex = 1;
            btnEditBm.Text = "Edit";
            btnEditBm.UseVisualStyleBackColor = true;
            // 
            // btnAddBm
            // 
            btnAddBm.Dock = DockStyle.Top;
            btnAddBm.Location = new Point(3, 3);
            btnAddBm.Name = "btnAddBm";
            btnAddBm.Size = new Size(286, 30);
            btnAddBm.TabIndex = 0;
            btnAddBm.Text = "Add";
            btnAddBm.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(lstHistory);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(292, 394);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "History";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // lstHistory
            // 
            lstHistory.Dock = DockStyle.Fill;
            lstHistory.FormattingEnabled = true;
            lstHistory.Location = new Point(3, 3);
            lstHistory.Name = "lstHistory";
            lstHistory.Size = new Size(286, 388);
            lstHistory.TabIndex = 0;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, lblTitle });
            statusStrip1.Location = new Point(0, 506);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1213, 26);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(151, 20);
            lblStatus.Text = "toolStripStatusLabel1";            
            // 
            // lblTitle
            // 
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1047, 20);
            lblTitle.Spring = true;
            lblTitle.Text = "toolStripStatusLabel2";
            // 
            // panelTopHeader
            // 
            panelTopHeader.BackColor = SystemColors.ActiveCaption;
            panelTopHeader.BorderStyle = BorderStyle.Fixed3D;
            panelTopHeader.Controls.Add(lblTopTitle);
            panelTopHeader.Controls.Add(lblTopStatus);
            panelTopHeader.Dock = DockStyle.Top;
            panelTopHeader.Location = new Point(0, 55);
            panelTopHeader.MaximumSize = new Size(0, 28);
            panelTopHeader.Name = "panelTopHeader";
            panelTopHeader.Size = new Size(1213, 24);
            panelTopHeader.TabIndex = 3;
            // 
            // lblTopTitle
            // 
            lblTopTitle.AutoSize = true;
            lblTopTitle.Location = new Point(185, 0);
            lblTopTitle.Name = "lblTopTitle";
            lblTopTitle.Size = new Size(50, 20);
            lblTopTitle.TabIndex = 1;
            lblTopTitle.Text = "label2";
            // 
            // lblTopStatus
            // 
            lblTopStatus.AutoSize = true;
            lblTopStatus.Location = new Point(0, 0);
            lblTopStatus.Name = "lblTopStatus";
            lblTopStatus.Size = new Size(50, 20);
            lblTopStatus.TabIndex = 0;
            lblTopStatus.Text = "label1";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem, userToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1213, 28);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuSetHome, bookmarkToolStripMenuItem, helpToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(60, 24);
            menuToolStripMenuItem.Text = "Menu";
            // 
            // mnuSetHome
            // 
            mnuSetHome.Name = "mnuSetHome";
            mnuSetHome.ShortcutKeys = Keys.Control | Keys.H;
            mnuSetHome.Size = new Size(211, 26);
            mnuSetHome.Text = "Set Home";
            // 
            // bookmarkToolStripMenuItem
            // 
            bookmarkToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuAddBookmark, mnuEditBookmark, mnuDeleteBookmark });
            bookmarkToolStripMenuItem.Name = "bookmarkToolStripMenuItem";
            bookmarkToolStripMenuItem.Size = new Size(211, 26);
            bookmarkToolStripMenuItem.Text = "Bookmark";
            // 
            // mnuAddBookmark
            // 
            mnuAddBookmark.Name = "mnuAddBookmark";
            mnuAddBookmark.ShortcutKeys = Keys.Control | Keys.A;
            mnuAddBookmark.Size = new Size(189, 26);
            mnuAddBookmark.Text = "Add";
            // 
            // mnuEditBookmark
            // 
            mnuEditBookmark.Name = "mnuEditBookmark";
            mnuEditBookmark.ShortcutKeys = Keys.Control | Keys.E;
            mnuEditBookmark.Size = new Size(189, 26);
            mnuEditBookmark.Text = "Edit";
            // 
            // mnuDeleteBookmark
            // 
            mnuDeleteBookmark.Name = "mnuDeleteBookmark";
            mnuDeleteBookmark.ShortcutKeys = Keys.Control | Keys.D;
            mnuDeleteBookmark.Size = new Size(189, 26);
            mnuDeleteBookmark.Text = "Delete";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuAbout });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(211, 26);
            helpToolStripMenuItem.Text = "Help";
            // 
            // mnuAbout
            // 
            mnuAbout.Name = "mnuAbout";
            mnuAbout.ShortcutKeys = Keys.F1;
            mnuAbout.Size = new Size(157, 26);
            mnuAbout.Text = "About";
            // 
            // userToolStripMenuItem
            // 
            userToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuSwitchUser });
            userToolStripMenuItem.Name = "userToolStripMenuItem";
            userToolStripMenuItem.Size = new Size(52, 24);
            userToolStripMenuItem.Text = "User";
            // 
            // mnuSwitchUser
            // 
            mnuSwitchUser.Name = "mnuSwitchUser";
            mnuSwitchUser.Size = new Size(224, 26);
            mnuSwitchUser.Text = "Switch";
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 0);
            webView.Name = "webView";
            webView.Size = new Size(909, 427);
            webView.TabIndex = 1;
            webView.Visible = false;
            webView.ZoomFactor = 1D;
            // 
            // btnRender
            // 
            btnRender.Alignment = ToolStripItemAlignment.Right;
            btnRender.CheckOnClick = true;
            btnRender.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRender.Image = (Image)resources.GetObject("btnRender.Image");
            btnRender.ImageTransparentColor = Color.Magenta;
            btnRender.Name = "btnRender";
            btnRender.Size = new Size(60, 24);
            btnRender.Text = "Render";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1213, 532);
            Controls.Add(splitMain);
            Controls.Add(statusStrip1);
            Controls.Add(panelTopHeader);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip1);
            Name = "Form1";
            Text = "MiniBrowser 1.0";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel1.PerformLayout();
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            tabs.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panelTopHeader.ResumeLayout(false);
            panelTopHeader.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton btnBack;
        private ToolStripButton btnForward;
        private ToolStripButton btnReload;
        private ToolStripButton btnHome;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripTextBox txtAddress;
        private ToolStripButton btnGo;
        private SplitContainer splitMain;
        private TabControl tabs;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Button btnDelBm;
        private Button btnEditBm;
        private Button btnAddBm;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblTitle;
        private ListBox lstLinks;
        private ListBox lstBookmarks;
        private ListBox lstHistory;
        private TabPage tabPage1;
        private Panel panelTopHeader;
        private Label lblTopTitle;
        private Label lblTopStatus;
        private TextBox txtHtml;
        private ToolStripButton btnSetHome;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem mnuSetHome;
        private ToolStripMenuItem bookmarkToolStripMenuItem;
        private ToolStripMenuItem mnuAddBookmark;
        private ToolStripMenuItem mnuEditBookmark;
        private ToolStripMenuItem mnuDeleteBookmark;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem mnuAbout;
        private ToolStripMenuItem userToolStripMenuItem;
        private ToolStripMenuItem mnuSwitchUser;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private ToolStripButton btnRender;
    }
}
