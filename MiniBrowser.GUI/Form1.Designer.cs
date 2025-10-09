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
            toolStripSeparator1 = new ToolStripSeparator();
            txtAddress = new ToolStripTextBox();
            btnGo = new ToolStripButton();
            splitMain = new SplitContainer();
            rtbHtml = new RichTextBox();
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
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(20, 20);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnBack, btnForward, btnReload, btnHome, toolStripSeparator1, txtAddress, btnGo });
            toolStrip.Location = new Point(0, 0);
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
            splitMain.Location = new Point(0, 27);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(rtbHtml);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(tabs);
            splitMain.Size = new Size(1213, 505);
            splitMain.SplitterDistance = 909;
            splitMain.TabIndex = 1;
            // 
            // rtbHtml
            // 
            rtbHtml.Dock = DockStyle.Fill;
            rtbHtml.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbHtml.Location = new Point(0, 0);
            rtbHtml.Name = "rtbHtml";
            rtbHtml.ReadOnly = true;
            rtbHtml.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            rtbHtml.Size = new Size(909, 505);
            rtbHtml.TabIndex = 0;
            rtbHtml.Text = "";
            rtbHtml.WordWrap = false;
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
            tabs.Size = new Size(300, 505);
            tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(lstLinks);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(292, 472);
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
            lstLinks.Size = new Size(286, 466);
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
            tabPage2.Size = new Size(292, 472);
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
            lstBookmarks.Size = new Size(286, 376);
            lstBookmarks.TabIndex = 3;
            lstBookmarks.SelectedIndexChanged += lstBookmarks_SelectedIndexChanged;
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
            tabPage3.Size = new Size(292, 472);
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
            lstHistory.Size = new Size(286, 466);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1213, 532);
            Controls.Add(statusStrip1);
            Controls.Add(splitMain);
            Controls.Add(toolStrip);
            Name = "Form1";
            Text = "MiniBrowser 1.0";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            tabs.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
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
        private RichTextBox rtbHtml;
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
    }
}
