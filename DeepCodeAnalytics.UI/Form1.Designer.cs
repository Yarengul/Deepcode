namespace DeepCodeAnalytics.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Top Header
        private System.Windows.Forms.Panel pnlTopHeader;
        private DeepCodeAnalytics.UI.Controls.RoundedLabel lblLogoSquare;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblSubtitle;
        private DeepCodeAnalytics.UI.Controls.RoundedButton btnAnalizEt;
        private DeepCodeAnalytics.UI.Controls.RoundedButton btnDosyaYukle;
        private DeepCodeAnalytics.UI.Controls.RoundedLabel lblStatusDot;

        // Sidebar
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Panel pnlUserProfile;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Label lblSidebarDivider;
        private System.Windows.Forms.Label lblVersion;
        
        // Main Container
        private System.Windows.Forms.Panel pnlMainContainer;
        private System.Windows.Forms.Panel pnlBreadcrumb;
        private System.Windows.Forms.Label lblBreadcrumb;
        
        // Table Layout
        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.TableLayoutPanel tblRightLayout;
        
        // Editor
        private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlEditorContainer;
        private System.Windows.Forms.Label lblEditorTitle;
        private System.Windows.Forms.Panel pnlEditorHeader;
        private System.Windows.Forms.Label lblEditorLangBadge;
        private System.Windows.Forms.Label lblEditorLinesBadge;
        private System.Windows.Forms.Label lblEditorUtf8;
        private System.Windows.Forms.Label lblMacDots;
        private System.Windows.Forms.Panel pnlLineNumbers;
        private System.Windows.Forms.RichTextBox txtKodAlani;
        
        // Quality Score
        private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlQualityScoreCard;
        private System.Windows.Forms.Label lblKalitePuaniTitle;
        private System.Windows.Forms.Label lblKalitePuani; 
        private DeepCodeAnalytics.UI.Controls.RoundedLabel lblStatusBadge;
        
        // Results Card
        private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlResultsCard;
        private System.Windows.Forms.Panel pnlResultsHeader;
        private System.Windows.Forms.Label lblResultsTitle;
        private System.Windows.Forms.Label lblResultsMacDots;
        private DeepCodeAnalytics.UI.Controls.RoundedLabel lblTotalIssuesBadge;
        private System.Windows.Forms.FlowLayoutPanel pnlErrorCards;
        private System.Windows.Forms.Panel pnlResultsFooter;
        private System.Windows.Forms.Label lblFooterYusek;
        private System.Windows.Forms.Label lblFooterOrta;
        private System.Windows.Forms.Label lblFooterDusuk;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTopHeader = new System.Windows.Forms.Panel();
            this.lblLogoSquare = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnAnalizEt = new DeepCodeAnalytics.UI.Controls.RoundedButton();
            this.btnDosyaYukle = new DeepCodeAnalytics.UI.Controls.RoundedButton();
            this.lblStatusDot = new DeepCodeAnalytics.UI.Controls.RoundedLabel();

            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.pnlUserProfile = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblSidebarDivider = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            
            this.pnlMainContainer = new System.Windows.Forms.Panel();
            this.pnlBreadcrumb = new System.Windows.Forms.Panel();
            this.lblBreadcrumb = new System.Windows.Forms.Label();
            
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tblRightLayout = new System.Windows.Forms.TableLayoutPanel();
            
            this.pnlEditorContainer = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
            this.lblEditorTitle = new System.Windows.Forms.Label();
            this.pnlEditorHeader = new System.Windows.Forms.Panel();
            this.lblEditorLangBadge = new System.Windows.Forms.Label();
            this.lblEditorLinesBadge = new System.Windows.Forms.Label();
            this.lblEditorUtf8 = new System.Windows.Forms.Label();
            this.lblMacDots = new System.Windows.Forms.Label();
            this.pnlLineNumbers = new System.Windows.Forms.Panel();
            this.txtKodAlani = new System.Windows.Forms.RichTextBox();
            
            this.pnlQualityScoreCard = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
            this.lblKalitePuaniTitle = new System.Windows.Forms.Label();
            this.lblKalitePuani = new System.Windows.Forms.Label();
            this.lblStatusBadge = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
            
            this.pnlResultsCard = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
            this.pnlResultsHeader = new System.Windows.Forms.Panel();
            this.lblResultsTitle = new System.Windows.Forms.Label();
            this.lblResultsMacDots = new System.Windows.Forms.Label();
            this.lblTotalIssuesBadge = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
            this.pnlErrorCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlResultsFooter = new System.Windows.Forms.Panel();
            this.lblFooterYusek = new System.Windows.Forms.Label();
            this.lblFooterOrta = new System.Windows.Forms.Label();
            this.lblFooterDusuk = new System.Windows.Forms.Label();

            this.pnlTopHeader.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlUserProfile.SuspendLayout();
            this.pnlMainContainer.SuspendLayout();
            this.pnlBreadcrumb.SuspendLayout();
            this.tblMainLayout.SuspendLayout();
            this.tblRightLayout.SuspendLayout();
            this.pnlEditorContainer.SuspendLayout();
            this.pnlEditorHeader.SuspendLayout();
            this.pnlQualityScoreCard.SuspendLayout();
            this.pnlResultsCard.SuspendLayout();
            this.pnlResultsHeader.SuspendLayout();
            this.pnlResultsFooter.SuspendLayout();
            this.SuspendLayout();

            // --- pnlTopHeader ---
            this.pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopHeader.Height = 80;
            this.pnlTopHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20); // #0B0E14
            this.pnlTopHeader.Controls.Add(this.lblLogoSquare);
            this.pnlTopHeader.Controls.Add(this.lblLogo);
            this.pnlTopHeader.Controls.Add(this.lblSubtitle);
            this.pnlTopHeader.Controls.Add(this.btnAnalizEt);
            this.pnlTopHeader.Controls.Add(this.btnDosyaYukle);
            this.pnlTopHeader.Controls.Add(this.lblStatusDot);

            // lblLogoSquare
            this.lblLogoSquare.AutoSize = false;
            this.lblLogoSquare.Size = new System.Drawing.Size(42, 42);
            this.lblLogoSquare.Location = new System.Drawing.Point(24, 19);
            this.lblLogoSquare.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.lblLogoSquare.ForeColor = System.Drawing.Color.White;
            this.lblLogoSquare.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogoSquare.Text = "DC";
            this.lblLogoSquare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogoSquare.BorderRadius = 10;

            // lblLogo
            this.lblLogo.AutoSize = true;
            this.lblLogo.Location = new System.Drawing.Point(78, 18);
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Text = "DeepCode Analytics";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(78, 46);
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 120, 130);
            this.lblSubtitle.Text = "AI-Powered Code Analysis";

            // btnAnalizEt
            this.btnAnalizEt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnAnalizEt.Location = new System.Drawing.Point(1000, 20);
            this.btnAnalizEt.Size = new System.Drawing.Size(160, 40);
            this.btnAnalizEt.BackColor = System.Drawing.Color.FromArgb(253, 126, 20); // Orange
            this.btnAnalizEt.ForeColor = System.Drawing.Color.White;
            this.btnAnalizEt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnalizEt.Text = "▶  Analiz Et";
            this.btnAnalizEt.BorderRadius = 8;
            this.btnAnalizEt.Click += new System.EventHandler(this.btnAnalizEt_Click);

            // btnDosyaYukle
            this.btnDosyaYukle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnDosyaYukle.Location = new System.Drawing.Point(830, 20);
            this.btnDosyaYukle.Size = new System.Drawing.Size(160, 40);
            this.btnDosyaYukle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.btnDosyaYukle.ForeColor = System.Drawing.Color.White;
            this.btnDosyaYukle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDosyaYukle.Text = "📁  Dosya Yükle";
            this.btnDosyaYukle.BorderRadius = 8;
            this.btnDosyaYukle.Click += new System.EventHandler(this.btnDosyaYukle_Click);

            // lblStatusDot — green "Hazır" badge
            this.lblStatusDot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblStatusDot.Location = new System.Drawing.Point(1170, 28);
            this.lblStatusDot.AutoSize = false;
            this.lblStatusDot.Size = new System.Drawing.Size(70, 24);
            this.lblStatusDot.BackColor = System.Drawing.Color.FromArgb(30, 60, 30);
            this.lblStatusDot.ForeColor = System.Drawing.Color.FromArgb(60, 179, 113);
            this.lblStatusDot.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusDot.Text = "● Hazır";
            this.lblStatusDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatusDot.BorderRadius = 12;

            // --- pnlSidebar ---
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(11, 14, 20); // #0B0E14 deep navy
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Width = 240;
            this.pnlSidebar.Padding = new System.Windows.Forms.Padding(0, 24, 0, 0);
            this.pnlSidebar.Controls.Add(this.lblVersion);
            this.pnlSidebar.Controls.Add(this.lblSidebarDivider);
            this.pnlSidebar.Controls.Add(this.pnlUserProfile);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.btnHistory);
            this.pnlSidebar.Controls.Add(this.btnSettings);
            this.pnlSidebar.Controls.Add(this.btnAbout);

            // btnDashboard (Active)
            this.btnDashboard.Location = new System.Drawing.Point(12, 0);
            this.btnDashboard.Size = new System.Drawing.Size(216, 46);
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
            this.btnDashboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnDashboard.Text = "";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;

            // btnHistory
            this.btnHistory.Location = new System.Drawing.Point(12, 56);
            this.btnHistory.Size = new System.Drawing.Size(216, 46);
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(20, 24, 33);
            this.btnHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(20, 24, 33);
            this.btnHistory.ForeColor = System.Drawing.Color.LightGray;
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnHistory.Text = "";
            this.btnHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistory.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
            this.btnHistory.Cursor = System.Windows.Forms.Cursors.Hand;

            // btnSettings
            this.btnSettings.Location = new System.Drawing.Point(12, 112);
            this.btnSettings.Size = new System.Drawing.Size(216, 46);
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(20, 24, 33);
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(20, 24, 33);
            this.btnSettings.ForeColor = System.Drawing.Color.LightGray;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnSettings.Text = "";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;

            // btnAbout
            this.btnAbout.Location = new System.Drawing.Point(12, 168);
            this.btnAbout.Size = new System.Drawing.Size(216, 46);
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(20, 24, 33);
            this.btnAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(20, 24, 33);
            this.btnAbout.ForeColor = System.Drawing.Color.LightGray;
            this.btnAbout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnAbout.Text = "";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbout.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
            this.btnAbout.Cursor = System.Windows.Forms.Cursors.Hand;

            // pnlUserProfile
            this.pnlUserProfile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUserProfile.Height = 80;
            this.pnlUserProfile.BackColor = System.Drawing.Color.Transparent;
            this.pnlUserProfile.Controls.Add(this.lblUserName);
            this.pnlUserProfile.Controls.Add(this.lblUserRole);

            // lblUserName
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(24, 20);
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Text = "👤 Admin User";

            // lblUserRole
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Location = new System.Drawing.Point(48, 45);
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(100, 108, 120);
            this.lblUserRole.Text = "Senior Developer";

            // lblSidebarDivider
            this.lblSidebarDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSidebarDivider.Height = 1;
            this.lblSidebarDivider.BackColor = System.Drawing.Color.FromArgb(35, 40, 50);
            this.lblSidebarDivider.Text = "";

            // lblVersion
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblVersion.Height = 35;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(60, 65, 75);
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblVersion.Text = "Version 1.0.0";
            this.lblVersion.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);

            // --- pnlMainContainer ---
            this.pnlMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContainer.BackColor = System.Drawing.Color.FromArgb(18, 18, 18); // #121212 clean dark grey
            this.pnlMainContainer.Padding = new System.Windows.Forms.Padding(24);
            this.pnlMainContainer.Controls.Add(this.tblMainLayout);

            // --- tblMainLayout ---
            this.tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainLayout.ColumnCount = 2;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMainLayout.RowCount = 1;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.Controls.Add(this.pnlEditorContainer, 0, 0);
            this.tblMainLayout.Controls.Add(this.tblRightLayout, 1, 0);
            this.pnlEditorContainer.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblRightLayout.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);

            // --- pnlEditorContainer ---
            this.pnlEditorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditorContainer.BackColor = System.Drawing.Color.FromArgb(11, 14, 20); // #0B0E14
            this.pnlEditorContainer.BorderRadius = 12;
            this.pnlEditorContainer.BorderSize = 1;
            this.pnlEditorContainer.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51); // #333333
            this.pnlEditorContainer.DrawShadow = true;
            this.pnlEditorContainer.Padding = new System.Windows.Forms.Padding(1);
            this.pnlEditorContainer.Controls.Add(this.txtKodAlani);
            this.pnlEditorContainer.Controls.Add(this.pnlLineNumbers);
            this.pnlEditorContainer.Controls.Add(this.pnlEditorHeader);

            // pnlEditorHeader (title bar with window dots + badges)
            this.pnlEditorHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEditorHeader.Height = 42;
            this.pnlEditorHeader.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
            this.pnlEditorHeader.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlEditorHeader.Controls.Add(this.lblMacDots);
            this.pnlEditorHeader.Controls.Add(this.lblEditorUtf8);
            this.pnlEditorHeader.Controls.Add(this.lblEditorLinesBadge);
            this.pnlEditorHeader.Controls.Add(this.lblEditorLangBadge);

            // lblMacDots — colored window control dots (Red, Yellow, Green)
            this.lblMacDots.Location = new System.Drawing.Point(12, 14);
            this.lblMacDots.AutoSize = false;
            this.lblMacDots.Size = new System.Drawing.Size(60, 16);
            this.lblMacDots.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMacDots.Text = "";

            // lblEditorLangBadge — right-aligned C# badge
            this.lblEditorLangBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblEditorLangBadge.AutoSize = false;
            this.lblEditorLangBadge.Size = new System.Drawing.Size(36, 22);
            this.lblEditorLangBadge.ForeColor = System.Drawing.Color.White;
            this.lblEditorLangBadge.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.lblEditorLangBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEditorLangBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblEditorLangBadge.Text = "C#";

            // lblEditorLinesBadge — line count metadata
            this.lblEditorLinesBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblEditorLinesBadge.AutoSize = false;
            this.lblEditorLinesBadge.Size = new System.Drawing.Size(65, 22);
            this.lblEditorLinesBadge.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.lblEditorLinesBadge.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
            this.lblEditorLinesBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEditorLinesBadge.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblEditorLinesBadge.Text = "Lines: 7";

            // lblEditorUtf8 — encoding badge
            this.lblEditorUtf8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblEditorUtf8.AutoSize = false;
            this.lblEditorUtf8.Size = new System.Drawing.Size(48, 22);
            this.lblEditorUtf8.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.lblEditorUtf8.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
            this.lblEditorUtf8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEditorUtf8.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblEditorUtf8.Text = "UTF-8";

            // pnlLineNumbers — line number gutter
            this.pnlLineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLineNumbers.Width = 48;
            this.pnlLineNumbers.BackColor = System.Drawing.Color.FromArgb(14, 17, 24); // Slightly lighter than editor bg
            this.pnlLineNumbers.Padding = new System.Windows.Forms.Padding(0, 4, 8, 4);

            // txtKodAlani
            this.txtKodAlani.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKodAlani.BackColor = System.Drawing.Color.FromArgb(11, 14, 20); // #0B0E14
            this.txtKodAlani.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKodAlani.Font = new System.Drawing.Font("Cascadia Code", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtKodAlani.ForeColor = System.Drawing.Color.FromArgb(212, 212, 212);
            this.txtKodAlani.Text = "public class Example\n{\n    public void Test()\n    {\n        // Yazılım kurallarına aykırı bir metot\n    }\n}";
            this.txtKodAlani.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtKodAlani.WordWrap = false;

            // --- tblRightLayout ---
            this.tblRightLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRightLayout.ColumnCount = 1;
            this.tblRightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRightLayout.RowCount = 2;
            this.tblRightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblRightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRightLayout.Controls.Add(this.pnlQualityScoreCard, 0, 0);
            this.tblRightLayout.Controls.Add(this.pnlResultsCard, 0, 1);
            this.pnlQualityScoreCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlResultsCard.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);

            // --- pnlQualityScoreCard ---
            this.pnlQualityScoreCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQualityScoreCard.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.pnlQualityScoreCard.BorderRadius = 12;
            this.pnlQualityScoreCard.BorderSize = 1;
            this.pnlQualityScoreCard.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.pnlQualityScoreCard.DrawShadow = true;
            this.pnlQualityScoreCard.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlQualityScoreCard.Controls.Add(this.lblKalitePuaniTitle);
            this.pnlQualityScoreCard.Controls.Add(this.lblKalitePuani);
            this.pnlQualityScoreCard.Controls.Add(this.lblStatusBadge);

            // lblKalitePuaniTitle
            this.lblKalitePuaniTitle.AutoSize = true;
            this.lblKalitePuaniTitle.Location = new System.Drawing.Point(24, 18);
            this.lblKalitePuaniTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKalitePuaniTitle.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
            this.lblKalitePuaniTitle.Text = "Code Quality Score";

            // lblKalitePuani
            this.lblKalitePuani.AutoSize = true;
            this.lblKalitePuani.Location = new System.Drawing.Point(24, 48);
            this.lblKalitePuani.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblKalitePuani.ForeColor = System.Drawing.Color.White;
            this.lblKalitePuani.Text = "%100";
            this.lblKalitePuani.Paint += new System.Windows.Forms.PaintEventHandler(this.lblKalitePuani_Paint);

            // lblStatusBadge
            this.lblStatusBadge.Location = new System.Drawing.Point(170, 68);
            this.lblStatusBadge.Size = new System.Drawing.Size(110, 24);
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.White;
            this.lblStatusBadge.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatusBadge.Text = "EXCELLENT";

            // --- pnlResultsCard ---
            this.pnlResultsCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultsCard.BackColor = System.Drawing.Color.FromArgb(18, 18, 18); // #121212
            this.pnlResultsCard.BorderRadius = 12;
            this.pnlResultsCard.BorderSize = 1;
            this.pnlResultsCard.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.pnlResultsCard.DrawShadow = true;
            this.pnlResultsCard.Controls.Add(this.pnlErrorCards);
            this.pnlResultsCard.Controls.Add(this.pnlResultsHeader);
            this.pnlResultsCard.Controls.Add(this.pnlResultsFooter);

            // pnlResultsHeader
            this.pnlResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResultsHeader.Height = 46;
            this.pnlResultsHeader.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
            this.pnlResultsHeader.Controls.Add(this.lblResultsMacDots);
            this.pnlResultsHeader.Controls.Add(this.lblResultsTitle);
            this.pnlResultsHeader.Controls.Add(this.lblTotalIssuesBadge);

            // lblResultsMacDots — colored window dots (Paint handler draws Red/Yellow/Green)
            this.lblResultsMacDots.Location = new System.Drawing.Point(12, 14);
            this.lblResultsMacDots.AutoSize = false;
            this.lblResultsMacDots.Size = new System.Drawing.Size(60, 16);
            this.lblResultsMacDots.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblResultsMacDots.Text = "";

            // lblResultsTitle
            this.lblResultsTitle.Location = new System.Drawing.Point(70, 13);
            this.lblResultsTitle.AutoSize = true;
            this.lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblResultsTitle.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
            this.lblResultsTitle.Text = "Analiz Sonuçları";

            // lblTotalIssuesBadge
            this.lblTotalIssuesBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblTotalIssuesBadge.Size = new System.Drawing.Size(80, 24);
            this.lblTotalIssuesBadge.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
            this.lblTotalIssuesBadge.ForeColor = System.Drawing.Color.White;
            this.lblTotalIssuesBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalIssuesBadge.Text = "0 Issue";

            // pnlErrorCards
            this.pnlErrorCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlErrorCards.AutoScroll = true;
            this.pnlErrorCards.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlErrorCards.WrapContents = false;
            this.pnlErrorCards.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlErrorCards.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);

            // pnlResultsFooter
            this.pnlResultsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlResultsFooter.Height = 38;
            this.pnlResultsFooter.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
            this.pnlResultsFooter.Controls.Add(this.lblFooterYusek);
            this.pnlResultsFooter.Controls.Add(this.lblFooterOrta);
            this.pnlResultsFooter.Controls.Add(this.lblFooterDusuk);

            // lblFooterYusek
            this.lblFooterYusek.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFooterYusek.Width = 120;
            this.lblFooterYusek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFooterYusek.ForeColor = System.Drawing.Color.FromArgb(180, 185, 195);
            this.lblFooterYusek.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFooterYusek.Text = "🔴 Yüksek: 0";

            // lblFooterOrta
            this.lblFooterOrta.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFooterOrta.Width = 110;
            this.lblFooterOrta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFooterOrta.ForeColor = System.Drawing.Color.FromArgb(180, 185, 195);
            this.lblFooterOrta.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFooterOrta.Text = "🟠 Orta: 0";

            // lblFooterDusuk
            this.lblFooterDusuk.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFooterDusuk.Width = 110;
            this.lblFooterDusuk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFooterDusuk.ForeColor = System.Drawing.Color.FromArgb(180, 185, 195);
            this.lblFooterDusuk.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFooterDusuk.Text = "🔵 Düşük: 0";
            
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.Controls.Add(this.pnlMainContainer);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlTopHeader);
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.Name = "Form1";
            this.Text = "DeepCode Analytics - Premium";

            this.pnlTopHeader.ResumeLayout(false);
            this.pnlTopHeader.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.pnlUserProfile.ResumeLayout(false);
            this.pnlUserProfile.PerformLayout();
            this.pnlMainContainer.ResumeLayout(false);
            this.tblMainLayout.ResumeLayout(false);
            this.tblRightLayout.ResumeLayout(false);
            this.pnlEditorContainer.ResumeLayout(false);
            this.pnlEditorHeader.ResumeLayout(false);
            this.pnlEditorHeader.PerformLayout();
            this.pnlQualityScoreCard.ResumeLayout(false);
            this.pnlQualityScoreCard.PerformLayout();
            this.pnlResultsCard.ResumeLayout(false);
            this.pnlResultsHeader.ResumeLayout(false);
            this.pnlResultsFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
