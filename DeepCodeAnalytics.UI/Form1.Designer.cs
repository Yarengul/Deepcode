// Bu projede WinForms UI programatik olarak Form1.cs içinde oluşturuluyor.
// Bu Designer dosyası bilerek kullanılmıyor (ayrıca csproj içinde derlemeden hariç).
// Intentionally left empty.
// Intentionally left empty.
// UI is created programmatically in Form1.cs to avoid designer duplication issues.

#if !FORM1_DESIGNER_INCLUDED
#define FORM1_DESIGNER_INCLUDED

#nullable enable
namespace DeepCodeAnalytics.UI;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        SuspendLayout();

        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        ClientSize = new System.Drawing.Size(1280, 780);
        MinimumSize = new System.Drawing.Size(1100, 650);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        ResumeLayout(false);
    }

    #endregion
}

#endif

#if false

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        SuspendLayout();

        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        ClientSize = new System.Drawing.Size(1280, 780);
        MinimumSize = new System.Drawing.Size(1100, 650);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        ResumeLayout(false);
    }

    #endregion
}

#endif

#if false
#nullable enable
namespace DeepCodeAnalytics.UI;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        SuspendLayout();

        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        ClientSize = new System.Drawing.Size(1280, 780);
        MinimumSize = new System.Drawing.Size(1100, 650);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        ResumeLayout(false);
    }

    #endregion
}

#endif

#nullable enable
namespace DeepCodeAnalytics.UI;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        SuspendLayout();

        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18); // #121212
        ClientSize = new System.Drawing.Size(1280, 780);
        MinimumSize = new System.Drawing.Size(1100, 650);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        ResumeLayout(false);
    }

    #endregion
}

#nullable enable
namespace DeepCodeAnalytics.UI;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.TableLayoutPanel tblRoot = null!;
    private System.Windows.Forms.Panel pnlTopHeader = null!;
    private System.Windows.Forms.TableLayoutPanel tblMain = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblLogoSquare = null!;
    private System.Windows.Forms.Label lblLogo = null!;
    private System.Windows.Forms.Label lblSubtitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnDosyaYukle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnAnalizEt = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblStatusDot = null!;

    private System.Windows.Forms.Panel pnlSidebar = null!;
    private System.Windows.Forms.Button btnDashboard = null!;
    private System.Windows.Forms.Button btnHistory = null!;
    private System.Windows.Forms.Button btnSettings = null!;
    private System.Windows.Forms.Button btnAbout = null!;
    private System.Windows.Forms.Label lblVersion = null!;

    private System.Windows.Forms.TableLayoutPanel tblCenter = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlEditor = null!;
    private System.Windows.Forms.Panel pnlEditorHeader = null!;
    private System.Windows.Forms.Label lblEditorTitle = null!;
    private System.Windows.Forms.Label lblEditorLangBadge = null!;
    private System.Windows.Forms.Panel pnlEditorBody = null!;
    private System.Windows.Forms.Panel pnlLineNumbers = null!;
    private System.Windows.Forms.RichTextBox txtKodAlani = null!;
    private System.Windows.Forms.Panel pnlEditorFooter = null!;
    private System.Windows.Forms.Label lblEditorUtf8 = null!;
    private System.Windows.Forms.Label lblEditorLines = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlAi = null!;
    private System.Windows.Forms.Panel pnlAiHeader = null!;
    private System.Windows.Forms.Label lblAiHeaderIcon = null!;
    private System.Windows.Forms.Label lblAiHeaderTitle = null!;
    private System.Windows.Forms.Label lblAiHeaderSubtitle = null!;
    private System.Windows.Forms.Panel pnlAiBody = null!;
    private System.Windows.Forms.Panel pnlAiScroll = null!;
    private System.Windows.Forms.TableLayoutPanel tblAiGrid = null!;

    private System.Windows.Forms.Panel pnlRight = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlResults = null!;
    private System.Windows.Forms.Panel pnlResultsHeader = null!;
    private System.Windows.Forms.Label lblResultsTitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblIssuesBadge = null!;
    private System.Windows.Forms.FlowLayoutPanel flpIssues = null!;
    private System.Windows.Forms.Panel pnlResultsFooter = null!;
    private System.Windows.Forms.Label lblHigh = null!;
    private System.Windows.Forms.Label lblMedium = null!;
    private System.Windows.Forms.Label lblLow = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        tblRoot = new System.Windows.Forms.TableLayoutPanel();
        pnlTopHeader = new System.Windows.Forms.Panel();
        tblMain = new System.Windows.Forms.TableLayoutPanel();

        lblLogoSquare = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        lblLogo = new System.Windows.Forms.Label();
        lblSubtitle = new System.Windows.Forms.Label();
        btnDosyaYukle = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        btnAnalizEt = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        lblStatusDot = new DeepCodeAnalytics.UI.Controls.RoundedLabel();

        pnlSidebar = new System.Windows.Forms.Panel();
        btnDashboard = new System.Windows.Forms.Button();
        btnHistory = new System.Windows.Forms.Button();
        btnSettings = new System.Windows.Forms.Button();
        btnAbout = new System.Windows.Forms.Button();
        lblVersion = new System.Windows.Forms.Label();

        tblCenter = new System.Windows.Forms.TableLayoutPanel();
        pnlEditor = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlEditorHeader = new System.Windows.Forms.Panel();
        lblEditorTitle = new System.Windows.Forms.Label();
        lblEditorLangBadge = new System.Windows.Forms.Label();
        pnlEditorBody = new System.Windows.Forms.Panel();
        pnlLineNumbers = new System.Windows.Forms.Panel();
        txtKodAlani = new System.Windows.Forms.RichTextBox();
        pnlEditorFooter = new System.Windows.Forms.Panel();
        lblEditorUtf8 = new System.Windows.Forms.Label();
        lblEditorLines = new System.Windows.Forms.Label();

        pnlAi = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlAiHeader = new System.Windows.Forms.Panel();
        lblAiHeaderIcon = new System.Windows.Forms.Label();
        lblAiHeaderTitle = new System.Windows.Forms.Label();
        lblAiHeaderSubtitle = new System.Windows.Forms.Label();
        pnlAiBody = new System.Windows.Forms.Panel();
        pnlAiScroll = new System.Windows.Forms.Panel();
        tblAiGrid = new System.Windows.Forms.TableLayoutPanel();

        pnlRight = new System.Windows.Forms.Panel();
        pnlResults = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlResultsHeader = new System.Windows.Forms.Panel();
        lblResultsTitle = new System.Windows.Forms.Label();
        lblIssuesBadge = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        flpIssues = new System.Windows.Forms.FlowLayoutPanel();
        pnlResultsFooter = new System.Windows.Forms.Panel();
        lblHigh = new System.Windows.Forms.Label();
        lblMedium = new System.Windows.Forms.Label();
        lblLow = new System.Windows.Forms.Label();

        tblRoot.SuspendLayout();
        pnlTopHeader.SuspendLayout();
        tblMain.SuspendLayout();
        pnlSidebar.SuspendLayout();
        tblCenter.SuspendLayout();
        pnlEditor.SuspendLayout();
        pnlEditorHeader.SuspendLayout();
        pnlEditorBody.SuspendLayout();
        pnlEditorFooter.SuspendLayout();
        pnlAi.SuspendLayout();
        pnlAiHeader.SuspendLayout();
        pnlAiBody.SuspendLayout();
        pnlAiScroll.SuspendLayout();
        pnlRight.SuspendLayout();
        pnlResults.SuspendLayout();
        pnlResultsHeader.SuspendLayout();
        pnlResultsFooter.SuspendLayout();
        SuspendLayout();

        // Form
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        ClientSize = new System.Drawing.Size(1280, 780);
        MinimumSize = new System.Drawing.Size(1100, 650);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        // Root
        tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
        tblRoot.ColumnCount = 1;
        tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.RowCount = 2;
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.Controls.Add(pnlTopHeader, 0, 0);
        tblRoot.Controls.Add(tblMain, 0, 1);
        Controls.Add(tblRoot);

        // Top header
        pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlTopHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlTopHeader.Controls.Add(lblLogoSquare);
        pnlTopHeader.Controls.Add(lblLogo);
        pnlTopHeader.Controls.Add(lblSubtitle);
        pnlTopHeader.Controls.Add(btnDosyaYukle);
        pnlTopHeader.Controls.Add(btnAnalizEt);
        pnlTopHeader.Controls.Add(lblStatusDot);

        lblLogoSquare.AutoSize = false;
        lblLogoSquare.Size = new System.Drawing.Size(40, 40);
        lblLogoSquare.Location = new System.Drawing.Point(20, 10);
        lblLogoSquare.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblLogoSquare.ForeColor = System.Drawing.Color.White;
        lblLogoSquare.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogoSquare.Text = "DC";
        lblLogoSquare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblLogoSquare.BorderRadius = 10;

        lblLogo.AutoSize = true;
        lblLogo.Location = new System.Drawing.Point(70, 8);
        lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogo.ForeColor = System.Drawing.Color.White;
        lblLogo.Text = "DeepCode Analytics";

        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new System.Drawing.Point(70, 32);
        lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 120, 130);
        lblSubtitle.Text = "AI-Powered Code Analysis";

        btnDosyaYukle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnDosyaYukle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        btnDosyaYukle.ForeColor = System.Drawing.Color.White;
        btnDosyaYukle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnDosyaYukle.Size = new System.Drawing.Size(160, 40);
        btnDosyaYukle.Location = new System.Drawing.Point(860, 10);
        btnDosyaYukle.Text = "📁 Dosya Yükle";
        btnDosyaYukle.BorderRadius = 8;
        btnDosyaYukle.Cursor = System.Windows.Forms.Cursors.Hand;
        btnDosyaYukle.Click += btnDosyaYukle_Click;

        btnAnalizEt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnAnalizEt.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        btnAnalizEt.ForeColor = System.Drawing.Color.White;
        btnAnalizEt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnAnalizEt.Size = new System.Drawing.Size(160, 40);
        btnAnalizEt.Location = new System.Drawing.Point(1030, 10);
        btnAnalizEt.Text = "▶ Analiz Et";
        btnAnalizEt.BorderRadius = 8;
        btnAnalizEt.Cursor = System.Windows.Forms.Cursors.Hand;
        btnAnalizEt.Click += btnAnalizEt_Click;

        lblStatusDot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblStatusDot.AutoSize = false;
        lblStatusDot.Size = new System.Drawing.Size(70, 24);
        lblStatusDot.Location = new System.Drawing.Point(1200, 18);
        lblStatusDot.BackColor = System.Drawing.Color.FromArgb(30, 60, 30);
        lblStatusDot.ForeColor = System.Drawing.Color.FromArgb(60, 179, 113);
        lblStatusDot.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblStatusDot.Text = "● Hazır";
        lblStatusDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblStatusDot.BorderRadius = 12;

        // Main table
        tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
        tblMain.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblMain.ColumnCount = 3;
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
        tblMain.RowCount = 1;
        tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.Controls.Add(pnlSidebar, 0, 0);
        tblMain.Controls.Add(tblCenter, 1, 0);
        tblMain.Controls.Add(pnlRight, 2, 0);

        // Sidebar
        pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlSidebar.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlSidebar.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
        pnlSidebar.Controls.Add(btnDashboard);
        pnlSidebar.Controls.Add(btnHistory);
        pnlSidebar.Controls.Add(btnSettings);
        pnlSidebar.Controls.Add(btnAbout);
        pnlSidebar.Controls.Add(lblVersion);

        ConfigureSidebarButton(btnDashboard, new System.Drawing.Point(12, 8));
        btnDashboard.Name = "btnDashboard";
        ConfigureSidebarButton(btnHistory, new System.Drawing.Point(12, 64));
        btnHistory.Name = "btnHistory";
        ConfigureSidebarButton(btnSettings, new System.Drawing.Point(12, 120));
        btnSettings.Name = "btnSettings";
        ConfigureSidebarButton(btnAbout, new System.Drawing.Point(12, 176));
        btnAbout.Name = "btnAbout";

        lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
        lblVersion.Height = 35;
        lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblVersion.ForeColor = System.Drawing.Color.FromArgb(60, 65, 75);
        lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
        lblVersion.Text = "Version 1.0.0";
        lblVersion.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);

        // Center layout
        tblCenter.Dock = System.Windows.Forms.DockStyle.Fill;
        tblCenter.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblCenter.Padding = new System.Windows.Forms.Padding(20);
        tblCenter.ColumnCount = 1;
        tblCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblCenter.RowCount = 2;
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
        tblCenter.Controls.Add(pnlEditor, 0, 0);
        tblCenter.Controls.Add(pnlAi, 0, 1);

        // Editor card
        pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditor.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditor.BorderRadius = 12;
        pnlEditor.BorderSize = 1;
        pnlEditor.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlEditor.DrawShadow = true;
        pnlEditor.Padding = new System.Windows.Forms.Padding(1);
        pnlEditor.Controls.Add(pnlEditorBody);
        pnlEditor.Controls.Add(pnlEditorFooter);
        pnlEditor.Controls.Add(pnlEditorHeader);

        pnlEditorHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlEditorHeader.Height = 46;
        pnlEditorHeader.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorHeader.Controls.Add(lblEditorTitle);
        pnlEditorHeader.Controls.Add(lblEditorLangBadge);

        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new System.Drawing.Point(14, 14);
        lblEditorTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblEditorTitle.Text = "Kod Giriş Alanı";

        lblEditorLangBadge.AutoSize = false;
        lblEditorLangBadge.Size = new System.Drawing.Size(36, 22);
        lblEditorLangBadge.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblEditorLangBadge.ForeColor = System.Drawing.Color.White;
        lblEditorLangBadge.Text = "C#";
        lblEditorLangBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLangBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
        lblEditorLangBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblEditorLangBadge.Location = new System.Drawing.Point(0, 12);

        pnlEditorFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlEditorFooter.Height = 34;
        pnlEditorFooter.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorFooter.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorFooter.Controls.Add(lblEditorUtf8);
        pnlEditorFooter.Controls.Add(lblEditorLines);

        lblEditorUtf8.AutoSize = false;
        lblEditorUtf8.Size = new System.Drawing.Size(56, 22);
        lblEditorUtf8.Location = new System.Drawing.Point(14, 6);
        lblEditorUtf8.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorUtf8.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorUtf8.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorUtf8.Text = "UTF-8";
        lblEditorUtf8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        lblEditorLines.AutoSize = false;
        lblEditorLines.Size = new System.Drawing.Size(80, 22);
        lblEditorLines.Location = new System.Drawing.Point(0, 6);
        lblEditorLines.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorLines.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorLines.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorLines.Text = "Lines: 1";
        lblEditorLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLines.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

        pnlEditorBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditorBody.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditorBody.Controls.Add(txtKodAlani);
        pnlEditorBody.Controls.Add(pnlLineNumbers);

        pnlLineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
        pnlLineNumbers.Width = 48;
        pnlLineNumbers.BackColor = System.Drawing.Color.FromArgb(14, 17, 24);
        pnlLineNumbers.Padding = new System.Windows.Forms.Padding(0, 4, 8, 4);

        txtKodAlani.Dock = System.Windows.Forms.DockStyle.Fill;
        txtKodAlani.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        txtKodAlani.ForeColor = System.Drawing.Color.FromArgb(212, 212, 212);
        txtKodAlani.BorderStyle = System.Windows.Forms.BorderStyle.None;
        txtKodAlani.Font = new System.Drawing.Font("Cascadia Code", 10.5F);
        txtKodAlani.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
        txtKodAlani.WordWrap = false;

        // AI panel
        pnlAi.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAi.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAi.BorderRadius = 12;
        pnlAi.BorderSize = 1;
        pnlAi.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlAi.DrawShadow = true;
        pnlAi.Controls.Add(pnlAiBody);
        pnlAi.Controls.Add(pnlAiHeader);

        pnlAiHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlAiHeader.Height = 58;
        pnlAiHeader.BackColor = System.Drawing.Color.FromArgb(60, 20, 90);
        pnlAiHeader.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
        pnlAiHeader.Controls.Add(lblAiHeaderIcon);
        pnlAiHeader.Controls.Add(lblAiHeaderTitle);
        pnlAiHeader.Controls.Add(lblAiHeaderSubtitle);

        lblAiHeaderIcon.AutoSize = true;
        lblAiHeaderIcon.Location = new System.Drawing.Point(14, 16);
        lblAiHeaderIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
        lblAiHeaderIcon.ForeColor = System.Drawing.Color.FromArgb(230, 230, 255);
        lblAiHeaderIcon.Text = "✨";

        lblAiHeaderTitle.AutoSize = true;
        lblAiHeaderTitle.Location = new System.Drawing.Point(44, 12);
        lblAiHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblAiHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(235, 235, 245);
        lblAiHeaderTitle.Text = "AI Önerileri";

        lblAiHeaderSubtitle.AutoSize = true;
        lblAiHeaderSubtitle.Location = new System.Drawing.Point(44, 33);
        lblAiHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblAiHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 170, 200);
        lblAiHeaderSubtitle.Text = "Yapay zeka destekli çözüm önerileri";

        pnlAiBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiBody.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiBody.Padding = new System.Windows.Forms.Padding(14);
        pnlAiBody.Controls.Add(pnlAiScroll);

        pnlAiScroll.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiScroll.AutoScroll = true;
        pnlAiScroll.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiScroll.Controls.Add(tblAiGrid);

        tblAiGrid.Dock = System.Windows.Forms.DockStyle.Top;
        tblAiGrid.AutoSize = true;
        tblAiGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tblAiGrid.ColumnCount = 3;
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.RowCount = 0;

        // Right panel
        pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlRight.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlRight.Padding = new System.Windows.Forms.Padding(20);
        pnlRight.Controls.Add(pnlResults);

        pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlResults.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlResults.BorderRadius = 12;
        pnlResults.BorderSize = 1;
        pnlResults.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlResults.DrawShadow = true;
        pnlResults.Controls.Add(flpIssues);
        pnlResults.Controls.Add(pnlResultsFooter);
        pnlResults.Controls.Add(pnlResultsHeader);

        pnlResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlResultsHeader.Height = 46;
        pnlResultsHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlResultsHeader.Controls.Add(lblResultsTitle);
        pnlResultsHeader.Controls.Add(lblIssuesBadge);

        lblResultsTitle.AutoSize = true;
        lblResultsTitle.Location = new System.Drawing.Point(14, 13);
        lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblResultsTitle.ForeColor = System.Drawing.Color.White;
        lblResultsTitle.Text = "Analiz Sonuçları";

        lblIssuesBadge.AutoSize = false;
        lblIssuesBadge.Size = new System.Drawing.Size(86, 24);
        lblIssuesBadge.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        lblIssuesBadge.ForeColor = System.Drawing.Color.White;
        lblIssuesBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblIssuesBadge.Text = "0 Issue";
        lblIssuesBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblIssuesBadge.BorderRadius = 12;
        lblIssuesBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblIssuesBadge.Location = new System.Drawing.Point(0, 11);

        flpIssues.Dock = System.Windows.Forms.DockStyle.Fill;
        flpIssues.AutoScroll = true;
        flpIssues.WrapContents = false;
        flpIssues.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        flpIssues.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        flpIssues.Padding = new System.Windows.Forms.Padding(12);

        pnlResultsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlResultsFooter.Height = 46;
        pnlResultsFooter.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsFooter.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
        pnlResultsFooter.Controls.Add(lblLow);
        pnlResultsFooter.Controls.Add(lblMedium);
        pnlResultsFooter.Controls.Add(lblHigh);

        lblHigh.Dock = System.Windows.Forms.DockStyle.Left;
        lblHigh.Width = 120;
        lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblHigh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblHigh.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblHigh.Text = "✓ Yüksek: 0";

        lblMedium.Dock = System.Windows.Forms.DockStyle.Left;
        lblMedium.Width = 110;
        lblMedium.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblMedium.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblMedium.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblMedium.Text = "✓ Orta: 0";

        lblLow.Dock = System.Windows.Forms.DockStyle.Left;
        lblLow.Width = 110;
        lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblLow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblLow.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblLow.Text = "✓ Düşük: 0";

        tblRoot.ResumeLayout(false);
        pnlTopHeader.ResumeLayout(false);
        pnlTopHeader.PerformLayout();
        tblMain.ResumeLayout(false);
        pnlSidebar.ResumeLayout(false);
        tblCenter.ResumeLayout(false);
        pnlEditor.ResumeLayout(false);
        pnlEditorHeader.ResumeLayout(false);
        pnlEditorHeader.PerformLayout();
        pnlEditorBody.ResumeLayout(false);
        pnlEditorFooter.ResumeLayout(false);
        pnlAi.ResumeLayout(false);
        pnlAiHeader.ResumeLayout(false);
        pnlAiHeader.PerformLayout();
        pnlAiBody.ResumeLayout(false);
        pnlAiScroll.ResumeLayout(false);
        pnlAiScroll.PerformLayout();
        pnlRight.ResumeLayout(false);
        pnlResults.ResumeLayout(false);
        pnlResultsHeader.ResumeLayout(false);
        pnlResultsHeader.PerformLayout();
        pnlResultsFooter.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void ConfigureSidebarButton(System.Windows.Forms.Button btn, System.Drawing.Point location)
    {
        btn.Location = location;
        btn.Size = new System.Drawing.Size(216, 46);
        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.ForeColor = System.Drawing.Color.White;
        btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        btn.Text = "";
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btn.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.Cursor = System.Windows.Forms.Cursors.Hand;
        btn.TabStop = false;
    }

    #endregion
}

#nullable enable
namespace DeepCodeAnalytics.UI;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.TableLayoutPanel tblRoot = null!;
    private System.Windows.Forms.Panel pnlTopHeader = null!;
    private System.Windows.Forms.TableLayoutPanel tblMain = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblLogoSquare = null!;
    private System.Windows.Forms.Label lblLogo = null!;
    private System.Windows.Forms.Label lblSubtitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnDosyaYukle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnAnalizEt = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblStatusDot = null!;

    private System.Windows.Forms.Panel pnlSidebar = null!;
    private System.Windows.Forms.Button btnDashboard = null!;
    private System.Windows.Forms.Button btnHistory = null!;
    private System.Windows.Forms.Button btnSettings = null!;
    private System.Windows.Forms.Button btnAbout = null!;
    private System.Windows.Forms.Label lblVersion = null!;

    private System.Windows.Forms.TableLayoutPanel tblCenter = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlEditor = null!;
    private System.Windows.Forms.Panel pnlEditorHeader = null!;
    private System.Windows.Forms.Label lblEditorTitle = null!;
    private System.Windows.Forms.Label lblEditorLangBadge = null!;
    private System.Windows.Forms.Panel pnlEditorBody = null!;
    private System.Windows.Forms.Panel pnlLineNumbers = null!;
    private System.Windows.Forms.RichTextBox txtKodAlani = null!;
    private System.Windows.Forms.Panel pnlEditorFooter = null!;
    private System.Windows.Forms.Label lblEditorUtf8 = null!;
    private System.Windows.Forms.Label lblEditorLines = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlAi = null!;
    private System.Windows.Forms.Panel pnlAiHeader = null!;
    private System.Windows.Forms.Label lblAiHeaderIcon = null!;
    private System.Windows.Forms.Label lblAiHeaderTitle = null!;
    private System.Windows.Forms.Label lblAiHeaderSubtitle = null!;
    private System.Windows.Forms.Panel pnlAiBody = null!;
    private System.Windows.Forms.Panel pnlAiScroll = null!;
    private System.Windows.Forms.TableLayoutPanel tblAiGrid = null!;

    private System.Windows.Forms.Panel pnlRight = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlResults = null!;
    private System.Windows.Forms.Panel pnlResultsHeader = null!;
    private System.Windows.Forms.Label lblResultsTitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblIssuesBadge = null!;
    private System.Windows.Forms.FlowLayoutPanel flpIssues = null!;
    private System.Windows.Forms.Panel pnlResultsFooter = null!;
    private System.Windows.Forms.Label lblHigh = null!;
    private System.Windows.Forms.Label lblMedium = null!;
    private System.Windows.Forms.Label lblLow = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        tblRoot = new System.Windows.Forms.TableLayoutPanel();
        pnlTopHeader = new System.Windows.Forms.Panel();
        tblMain = new System.Windows.Forms.TableLayoutPanel();

        lblLogoSquare = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        lblLogo = new System.Windows.Forms.Label();
        lblSubtitle = new System.Windows.Forms.Label();
        btnDosyaYukle = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        btnAnalizEt = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        lblStatusDot = new DeepCodeAnalytics.UI.Controls.RoundedLabel();

        pnlSidebar = new System.Windows.Forms.Panel();
        btnDashboard = new System.Windows.Forms.Button();
        btnHistory = new System.Windows.Forms.Button();
        btnSettings = new System.Windows.Forms.Button();
        btnAbout = new System.Windows.Forms.Button();
        lblVersion = new System.Windows.Forms.Label();

        tblCenter = new System.Windows.Forms.TableLayoutPanel();
        pnlEditor = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlEditorHeader = new System.Windows.Forms.Panel();
        lblEditorTitle = new System.Windows.Forms.Label();
        lblEditorLangBadge = new System.Windows.Forms.Label();
        pnlEditorBody = new System.Windows.Forms.Panel();
        pnlLineNumbers = new System.Windows.Forms.Panel();
        txtKodAlani = new System.Windows.Forms.RichTextBox();
        pnlEditorFooter = new System.Windows.Forms.Panel();
        lblEditorUtf8 = new System.Windows.Forms.Label();
        lblEditorLines = new System.Windows.Forms.Label();

        pnlAi = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlAiHeader = new System.Windows.Forms.Panel();
        lblAiHeaderIcon = new System.Windows.Forms.Label();
        lblAiHeaderTitle = new System.Windows.Forms.Label();
        lblAiHeaderSubtitle = new System.Windows.Forms.Label();
        pnlAiBody = new System.Windows.Forms.Panel();
        pnlAiScroll = new System.Windows.Forms.Panel();
        tblAiGrid = new System.Windows.Forms.TableLayoutPanel();

        pnlRight = new System.Windows.Forms.Panel();
        pnlResults = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlResultsHeader = new System.Windows.Forms.Panel();
        lblResultsTitle = new System.Windows.Forms.Label();
        lblIssuesBadge = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        flpIssues = new System.Windows.Forms.FlowLayoutPanel();
        pnlResultsFooter = new System.Windows.Forms.Panel();
        lblHigh = new System.Windows.Forms.Label();
        lblMedium = new System.Windows.Forms.Label();
        lblLow = new System.Windows.Forms.Label();

        tblRoot.SuspendLayout();
        pnlTopHeader.SuspendLayout();
        tblMain.SuspendLayout();
        pnlSidebar.SuspendLayout();
        tblCenter.SuspendLayout();
        pnlEditor.SuspendLayout();
        pnlEditorHeader.SuspendLayout();
        pnlEditorBody.SuspendLayout();
        pnlEditorFooter.SuspendLayout();
        pnlAi.SuspendLayout();
        pnlAiHeader.SuspendLayout();
        pnlAiBody.SuspendLayout();
        pnlAiScroll.SuspendLayout();
        pnlRight.SuspendLayout();
        pnlResults.SuspendLayout();
        pnlResultsHeader.SuspendLayout();
        pnlResultsFooter.SuspendLayout();
        SuspendLayout();

        // Form
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        ClientSize = new System.Drawing.Size(1280, 780);
        MinimumSize = new System.Drawing.Size(1100, 650);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        // Root
        tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
        tblRoot.ColumnCount = 1;
        tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.RowCount = 2;
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.Controls.Add(pnlTopHeader, 0, 0);
        tblRoot.Controls.Add(tblMain, 0, 1);
        Controls.Add(tblRoot);

        // Top header
        pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlTopHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlTopHeader.Controls.Add(lblLogoSquare);
        pnlTopHeader.Controls.Add(lblLogo);
        pnlTopHeader.Controls.Add(lblSubtitle);
        pnlTopHeader.Controls.Add(btnDosyaYukle);
        pnlTopHeader.Controls.Add(btnAnalizEt);
        pnlTopHeader.Controls.Add(lblStatusDot);

        lblLogoSquare.AutoSize = false;
        lblLogoSquare.Size = new System.Drawing.Size(40, 40);
        lblLogoSquare.Location = new System.Drawing.Point(20, 10);
        lblLogoSquare.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblLogoSquare.ForeColor = System.Drawing.Color.White;
        lblLogoSquare.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogoSquare.Text = "DC";
        lblLogoSquare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblLogoSquare.BorderRadius = 10;

        lblLogo.AutoSize = true;
        lblLogo.Location = new System.Drawing.Point(70, 8);
        lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogo.ForeColor = System.Drawing.Color.White;
        lblLogo.Text = "DeepCode Analytics";

        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new System.Drawing.Point(70, 32);
        lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 120, 130);
        lblSubtitle.Text = "AI-Powered Code Analysis";

        btnDosyaYukle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnDosyaYukle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        btnDosyaYukle.ForeColor = System.Drawing.Color.White;
        btnDosyaYukle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnDosyaYukle.Size = new System.Drawing.Size(160, 40);
        btnDosyaYukle.Location = new System.Drawing.Point(860, 10);
        btnDosyaYukle.Text = "📁 Dosya Yükle";
        btnDosyaYukle.BorderRadius = 8;
        btnDosyaYukle.Cursor = System.Windows.Forms.Cursors.Hand;
        btnDosyaYukle.Click += btnDosyaYukle_Click;

        btnAnalizEt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnAnalizEt.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        btnAnalizEt.ForeColor = System.Drawing.Color.White;
        btnAnalizEt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnAnalizEt.Size = new System.Drawing.Size(160, 40);
        btnAnalizEt.Location = new System.Drawing.Point(1030, 10);
        btnAnalizEt.Text = "▶ Analiz Et";
        btnAnalizEt.BorderRadius = 8;
        btnAnalizEt.Cursor = System.Windows.Forms.Cursors.Hand;
        btnAnalizEt.Click += btnAnalizEt_Click;

        lblStatusDot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblStatusDot.AutoSize = false;
        lblStatusDot.Size = new System.Drawing.Size(70, 24);
        lblStatusDot.Location = new System.Drawing.Point(1200, 18);
        lblStatusDot.BackColor = System.Drawing.Color.FromArgb(30, 60, 30);
        lblStatusDot.ForeColor = System.Drawing.Color.FromArgb(60, 179, 113);
        lblStatusDot.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblStatusDot.Text = "● Hazır";
        lblStatusDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblStatusDot.BorderRadius = 12;

        // Main table
        tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
        tblMain.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblMain.ColumnCount = 3;
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
        tblMain.RowCount = 1;
        tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.Controls.Add(pnlSidebar, 0, 0);
        tblMain.Controls.Add(tblCenter, 1, 0);
        tblMain.Controls.Add(pnlRight, 2, 0);

        // Sidebar
        pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlSidebar.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlSidebar.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
        pnlSidebar.Controls.Add(btnDashboard);
        pnlSidebar.Controls.Add(btnHistory);
        pnlSidebar.Controls.Add(btnSettings);
        pnlSidebar.Controls.Add(btnAbout);
        pnlSidebar.Controls.Add(lblVersion);

        ConfigureSidebarButton(btnDashboard, new System.Drawing.Point(12, 8));
        btnDashboard.Name = "btnDashboard";
        ConfigureSidebarButton(btnHistory, new System.Drawing.Point(12, 64));
        btnHistory.Name = "btnHistory";
        ConfigureSidebarButton(btnSettings, new System.Drawing.Point(12, 120));
        btnSettings.Name = "btnSettings";
        ConfigureSidebarButton(btnAbout, new System.Drawing.Point(12, 176));
        btnAbout.Name = "btnAbout";

        lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
        lblVersion.Height = 35;
        lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblVersion.ForeColor = System.Drawing.Color.FromArgb(60, 65, 75);
        lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
        lblVersion.Text = "Version 1.0.0";
        lblVersion.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);

        // Center layout
        tblCenter.Dock = System.Windows.Forms.DockStyle.Fill;
        tblCenter.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblCenter.Padding = new System.Windows.Forms.Padding(20);
        tblCenter.ColumnCount = 1;
        tblCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblCenter.RowCount = 2;
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
        tblCenter.Controls.Add(pnlEditor, 0, 0);
        tblCenter.Controls.Add(pnlAi, 0, 1);

        // Editor card
        pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditor.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditor.BorderRadius = 12;
        pnlEditor.BorderSize = 1;
        pnlEditor.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlEditor.DrawShadow = true;
        pnlEditor.Padding = new System.Windows.Forms.Padding(1);
        pnlEditor.Controls.Add(pnlEditorBody);
        pnlEditor.Controls.Add(pnlEditorFooter);
        pnlEditor.Controls.Add(pnlEditorHeader);

        pnlEditorHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlEditorHeader.Height = 46;
        pnlEditorHeader.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorHeader.Controls.Add(lblEditorTitle);
        pnlEditorHeader.Controls.Add(lblEditorLangBadge);

        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new System.Drawing.Point(14, 14);
        lblEditorTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblEditorTitle.Text = "Kod Giriş Alanı";

        lblEditorLangBadge.AutoSize = false;
        lblEditorLangBadge.Size = new System.Drawing.Size(36, 22);
        lblEditorLangBadge.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblEditorLangBadge.ForeColor = System.Drawing.Color.White;
        lblEditorLangBadge.Text = "C#";
        lblEditorLangBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLangBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
        lblEditorLangBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblEditorLangBadge.Location = new System.Drawing.Point(0, 12);

        pnlEditorFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlEditorFooter.Height = 34;
        pnlEditorFooter.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorFooter.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorFooter.Controls.Add(lblEditorUtf8);
        pnlEditorFooter.Controls.Add(lblEditorLines);

        lblEditorUtf8.AutoSize = false;
        lblEditorUtf8.Size = new System.Drawing.Size(56, 22);
        lblEditorUtf8.Location = new System.Drawing.Point(14, 6);
        lblEditorUtf8.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorUtf8.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorUtf8.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorUtf8.Text = "UTF-8";
        lblEditorUtf8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        lblEditorLines.AutoSize = false;
        lblEditorLines.Size = new System.Drawing.Size(80, 22);
        lblEditorLines.Location = new System.Drawing.Point(0, 6);
        lblEditorLines.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorLines.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorLines.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorLines.Text = "Lines: 1";
        lblEditorLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLines.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

        pnlEditorBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditorBody.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditorBody.Controls.Add(txtKodAlani);
        pnlEditorBody.Controls.Add(pnlLineNumbers);

        pnlLineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
        pnlLineNumbers.Width = 48;
        pnlLineNumbers.BackColor = System.Drawing.Color.FromArgb(14, 17, 24);
        pnlLineNumbers.Padding = new System.Windows.Forms.Padding(0, 4, 8, 4);

        txtKodAlani.Dock = System.Windows.Forms.DockStyle.Fill;
        txtKodAlani.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        txtKodAlani.ForeColor = System.Drawing.Color.FromArgb(212, 212, 212);
        txtKodAlani.BorderStyle = System.Windows.Forms.BorderStyle.None;
        txtKodAlani.Font = new System.Drawing.Font("Cascadia Code", 10.5F);
        txtKodAlani.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
        txtKodAlani.WordWrap = false;

        // AI panel
        pnlAi.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAi.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAi.BorderRadius = 12;
        pnlAi.BorderSize = 1;
        pnlAi.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlAi.DrawShadow = true;
        pnlAi.Controls.Add(pnlAiBody);
        pnlAi.Controls.Add(pnlAiHeader);

        pnlAiHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlAiHeader.Height = 58;
        pnlAiHeader.BackColor = System.Drawing.Color.FromArgb(60, 20, 90);
        pnlAiHeader.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
        pnlAiHeader.Controls.Add(lblAiHeaderIcon);
        pnlAiHeader.Controls.Add(lblAiHeaderTitle);
        pnlAiHeader.Controls.Add(lblAiHeaderSubtitle);

        lblAiHeaderIcon.AutoSize = true;
        lblAiHeaderIcon.Location = new System.Drawing.Point(14, 16);
        lblAiHeaderIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
        lblAiHeaderIcon.ForeColor = System.Drawing.Color.FromArgb(230, 230, 255);
        lblAiHeaderIcon.Text = "✨";

        lblAiHeaderTitle.AutoSize = true;
        lblAiHeaderTitle.Location = new System.Drawing.Point(44, 12);
        lblAiHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblAiHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(235, 235, 245);
        lblAiHeaderTitle.Text = "AI Önerileri";

        lblAiHeaderSubtitle.AutoSize = true;
        lblAiHeaderSubtitle.Location = new System.Drawing.Point(44, 33);
        lblAiHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblAiHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 170, 200);
        lblAiHeaderSubtitle.Text = "Yapay zeka destekli çözüm önerileri";

        pnlAiBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiBody.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiBody.Padding = new System.Windows.Forms.Padding(14);
        pnlAiBody.Controls.Add(pnlAiScroll);

        pnlAiScroll.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiScroll.AutoScroll = true;
        pnlAiScroll.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiScroll.Controls.Add(tblAiGrid);

        tblAiGrid.Dock = System.Windows.Forms.DockStyle.Top;
        tblAiGrid.AutoSize = true;
        tblAiGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tblAiGrid.ColumnCount = 3;
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.RowCount = 0;

        // Right panel
        pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlRight.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlRight.Padding = new System.Windows.Forms.Padding(20);
        pnlRight.Controls.Add(pnlResults);

        pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlResults.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlResults.BorderRadius = 12;
        pnlResults.BorderSize = 1;
        pnlResults.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlResults.DrawShadow = true;
        pnlResults.Controls.Add(flpIssues);
        pnlResults.Controls.Add(pnlResultsFooter);
        pnlResults.Controls.Add(pnlResultsHeader);

        pnlResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlResultsHeader.Height = 46;
        pnlResultsHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlResultsHeader.Controls.Add(lblResultsTitle);
        pnlResultsHeader.Controls.Add(lblIssuesBadge);

        lblResultsTitle.AutoSize = true;
        lblResultsTitle.Location = new System.Drawing.Point(14, 13);
        lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblResultsTitle.ForeColor = System.Drawing.Color.White;
        lblResultsTitle.Text = "Analiz Sonuçları";

        lblIssuesBadge.AutoSize = false;
        lblIssuesBadge.Size = new System.Drawing.Size(86, 24);
        lblIssuesBadge.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        lblIssuesBadge.ForeColor = System.Drawing.Color.White;
        lblIssuesBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblIssuesBadge.Text = "0 Issue";
        lblIssuesBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblIssuesBadge.BorderRadius = 12;
        lblIssuesBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblIssuesBadge.Location = new System.Drawing.Point(0, 11);

        flpIssues.Dock = System.Windows.Forms.DockStyle.Fill;
        flpIssues.AutoScroll = true;
        flpIssues.WrapContents = false;
        flpIssues.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        flpIssues.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        flpIssues.Padding = new System.Windows.Forms.Padding(12);

        pnlResultsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlResultsFooter.Height = 46;
        pnlResultsFooter.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsFooter.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
        pnlResultsFooter.Controls.Add(lblLow);
        pnlResultsFooter.Controls.Add(lblMedium);
        pnlResultsFooter.Controls.Add(lblHigh);

        lblHigh.Dock = System.Windows.Forms.DockStyle.Left;
        lblHigh.Width = 120;
        lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblHigh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblHigh.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblHigh.Text = "✓ Yüksek: 0";

        lblMedium.Dock = System.Windows.Forms.DockStyle.Left;
        lblMedium.Width = 110;
        lblMedium.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblMedium.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblMedium.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblMedium.Text = "✓ Orta: 0";

        lblLow.Dock = System.Windows.Forms.DockStyle.Left;
        lblLow.Width = 110;
        lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblLow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblLow.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblLow.Text = "✓ Düşük: 0";

        tblRoot.ResumeLayout(false);
        pnlTopHeader.ResumeLayout(false);
        pnlTopHeader.PerformLayout();
        tblMain.ResumeLayout(false);
        pnlSidebar.ResumeLayout(false);
        tblCenter.ResumeLayout(false);
        pnlEditor.ResumeLayout(false);
        pnlEditorHeader.ResumeLayout(false);
        pnlEditorHeader.PerformLayout();
        pnlEditorBody.ResumeLayout(false);
        pnlEditorFooter.ResumeLayout(false);
        pnlAi.ResumeLayout(false);
        pnlAiHeader.ResumeLayout(false);
        pnlAiHeader.PerformLayout();
        pnlAiBody.ResumeLayout(false);
        pnlAiScroll.ResumeLayout(false);
        pnlAiScroll.PerformLayout();
        pnlRight.ResumeLayout(false);
        pnlResults.ResumeLayout(false);
        pnlResultsHeader.ResumeLayout(false);
        pnlResultsHeader.PerformLayout();
        pnlResultsFooter.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void ConfigureSidebarButton(System.Windows.Forms.Button btn, System.Drawing.Point location)
    {
        btn.Location = location;
        btn.Size = new System.Drawing.Size(216, 46);
        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.ForeColor = System.Drawing.Color.White;
        btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        btn.Text = "";
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btn.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.Cursor = System.Windows.Forms.Cursors.Hand;
        btn.TabStop = false;
    }

    #endregion
}

#nullable enable
namespace DeepCodeAnalytics.UI;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.TableLayoutPanel tblRoot = null!;
    private System.Windows.Forms.Panel pnlTopHeader = null!;
    private System.Windows.Forms.TableLayoutPanel tblMain = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblLogoSquare = null!;
    private System.Windows.Forms.Label lblLogo = null!;
    private System.Windows.Forms.Label lblSubtitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnDosyaYukle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnAnalizEt = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblStatusDot = null!;

    private System.Windows.Forms.Panel pnlSidebar = null!;
    private System.Windows.Forms.Button btnDashboard = null!;
    private System.Windows.Forms.Button btnHistory = null!;
    private System.Windows.Forms.Button btnSettings = null!;
    private System.Windows.Forms.Button btnAbout = null!;
    private System.Windows.Forms.Label lblVersion = null!;

    private System.Windows.Forms.TableLayoutPanel tblCenter = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlEditor = null!;
    private System.Windows.Forms.Panel pnlEditorHeader = null!;
    private System.Windows.Forms.Label lblEditorTitle = null!;
    private System.Windows.Forms.Label lblEditorLangBadge = null!;
    private System.Windows.Forms.Panel pnlEditorBody = null!;
    private System.Windows.Forms.Panel pnlLineNumbers = null!;
    private System.Windows.Forms.RichTextBox txtKodAlani = null!;
    private System.Windows.Forms.Panel pnlEditorFooter = null!;
    private System.Windows.Forms.Label lblEditorUtf8 = null!;
    private System.Windows.Forms.Label lblEditorLines = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlAi = null!;
    private System.Windows.Forms.Panel pnlAiHeader = null!;
    private System.Windows.Forms.Label lblAiHeaderIcon = null!;
    private System.Windows.Forms.Label lblAiHeaderTitle = null!;
    private System.Windows.Forms.Label lblAiHeaderSubtitle = null!;
    private System.Windows.Forms.Panel pnlAiBody = null!;
    private System.Windows.Forms.Panel pnlAiScroll = null!;
    private System.Windows.Forms.TableLayoutPanel tblAiGrid = null!;

    private System.Windows.Forms.Panel pnlRight = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlResults = null!;
    private System.Windows.Forms.Panel pnlResultsHeader = null!;
    private System.Windows.Forms.Label lblResultsTitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblIssuesBadge = null!;
    private System.Windows.Forms.FlowLayoutPanel flpIssues = null!;
    private System.Windows.Forms.Panel pnlResultsFooter = null!;
    private System.Windows.Forms.Label lblHigh = null!;
    private System.Windows.Forms.Label lblMedium = null!;
    private System.Windows.Forms.Label lblLow = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        tblRoot = new System.Windows.Forms.TableLayoutPanel();
        pnlTopHeader = new System.Windows.Forms.Panel();
        tblMain = new System.Windows.Forms.TableLayoutPanel();

        lblLogoSquare = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        lblLogo = new System.Windows.Forms.Label();
        lblSubtitle = new System.Windows.Forms.Label();
        btnDosyaYukle = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        btnAnalizEt = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        lblStatusDot = new DeepCodeAnalytics.UI.Controls.RoundedLabel();

        pnlSidebar = new System.Windows.Forms.Panel();
        btnDashboard = new System.Windows.Forms.Button();
        btnHistory = new System.Windows.Forms.Button();
        btnSettings = new System.Windows.Forms.Button();
        btnAbout = new System.Windows.Forms.Button();
        lblVersion = new System.Windows.Forms.Label();

        tblCenter = new System.Windows.Forms.TableLayoutPanel();
        pnlEditor = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlEditorHeader = new System.Windows.Forms.Panel();
        lblEditorTitle = new System.Windows.Forms.Label();
        lblEditorLangBadge = new System.Windows.Forms.Label();
        pnlEditorBody = new System.Windows.Forms.Panel();
        pnlLineNumbers = new System.Windows.Forms.Panel();
        txtKodAlani = new System.Windows.Forms.RichTextBox();
        pnlEditorFooter = new System.Windows.Forms.Panel();
        lblEditorUtf8 = new System.Windows.Forms.Label();
        lblEditorLines = new System.Windows.Forms.Label();

        pnlAi = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlAiHeader = new System.Windows.Forms.Panel();
        lblAiHeaderIcon = new System.Windows.Forms.Label();
        lblAiHeaderTitle = new System.Windows.Forms.Label();
        lblAiHeaderSubtitle = new System.Windows.Forms.Label();
        pnlAiBody = new System.Windows.Forms.Panel();
        pnlAiScroll = new System.Windows.Forms.Panel();
        tblAiGrid = new System.Windows.Forms.TableLayoutPanel();

        pnlRight = new System.Windows.Forms.Panel();
        pnlResults = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlResultsHeader = new System.Windows.Forms.Panel();
        lblResultsTitle = new System.Windows.Forms.Label();
        lblIssuesBadge = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        flpIssues = new System.Windows.Forms.FlowLayoutPanel();
        pnlResultsFooter = new System.Windows.Forms.Panel();
        lblHigh = new System.Windows.Forms.Label();
        lblMedium = new System.Windows.Forms.Label();
        lblLow = new System.Windows.Forms.Label();

        tblRoot.SuspendLayout();
        pnlTopHeader.SuspendLayout();
        tblMain.SuspendLayout();
        pnlSidebar.SuspendLayout();
        tblCenter.SuspendLayout();
        pnlEditor.SuspendLayout();
        pnlEditorHeader.SuspendLayout();
        pnlEditorBody.SuspendLayout();
        pnlEditorFooter.SuspendLayout();
        pnlAi.SuspendLayout();
        pnlAiHeader.SuspendLayout();
        pnlAiBody.SuspendLayout();
        pnlAiScroll.SuspendLayout();
        pnlRight.SuspendLayout();
        pnlResults.SuspendLayout();
        pnlResultsHeader.SuspendLayout();
        pnlResultsFooter.SuspendLayout();
        SuspendLayout();

        // Form
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        ClientSize = new System.Drawing.Size(1280, 780);
        MinimumSize = new System.Drawing.Size(1100, 650);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        // Root
        tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
        tblRoot.ColumnCount = 1;
        tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.RowCount = 2;
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.Controls.Add(pnlTopHeader, 0, 0);
        tblRoot.Controls.Add(tblMain, 0, 1);
        Controls.Add(tblRoot);

        // Top header
        pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlTopHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlTopHeader.Controls.Add(lblLogoSquare);
        pnlTopHeader.Controls.Add(lblLogo);
        pnlTopHeader.Controls.Add(lblSubtitle);
        pnlTopHeader.Controls.Add(btnDosyaYukle);
        pnlTopHeader.Controls.Add(btnAnalizEt);
        pnlTopHeader.Controls.Add(lblStatusDot);

        lblLogoSquare.AutoSize = false;
        lblLogoSquare.Size = new System.Drawing.Size(40, 40);
        lblLogoSquare.Location = new System.Drawing.Point(20, 10);
        lblLogoSquare.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblLogoSquare.ForeColor = System.Drawing.Color.White;
        lblLogoSquare.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogoSquare.Text = "DC";
        lblLogoSquare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblLogoSquare.BorderRadius = 10;

        lblLogo.AutoSize = true;
        lblLogo.Location = new System.Drawing.Point(70, 8);
        lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogo.ForeColor = System.Drawing.Color.White;
        lblLogo.Text = "DeepCode Analytics";

        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new System.Drawing.Point(70, 32);
        lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 120, 130);
        lblSubtitle.Text = "AI-Powered Code Analysis";

        btnDosyaYukle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnDosyaYukle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        btnDosyaYukle.ForeColor = System.Drawing.Color.White;
        btnDosyaYukle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnDosyaYukle.Size = new System.Drawing.Size(160, 40);
        btnDosyaYukle.Location = new System.Drawing.Point(860, 10);
        btnDosyaYukle.Text = "📁 Dosya Yükle";
        btnDosyaYukle.BorderRadius = 8;
        btnDosyaYukle.Cursor = System.Windows.Forms.Cursors.Hand;
        btnDosyaYukle.Click += btnDosyaYukle_Click;

        btnAnalizEt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnAnalizEt.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        btnAnalizEt.ForeColor = System.Drawing.Color.White;
        btnAnalizEt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnAnalizEt.Size = new System.Drawing.Size(160, 40);
        btnAnalizEt.Location = new System.Drawing.Point(1030, 10);
        btnAnalizEt.Text = "▶ Analiz Et";
        btnAnalizEt.BorderRadius = 8;
        btnAnalizEt.Cursor = System.Windows.Forms.Cursors.Hand;
        btnAnalizEt.Click += btnAnalizEt_Click;

        lblStatusDot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblStatusDot.AutoSize = false;
        lblStatusDot.Size = new System.Drawing.Size(70, 24);
        lblStatusDot.Location = new System.Drawing.Point(1200, 18);
        lblStatusDot.BackColor = System.Drawing.Color.FromArgb(30, 60, 30);
        lblStatusDot.ForeColor = System.Drawing.Color.FromArgb(60, 179, 113);
        lblStatusDot.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblStatusDot.Text = "● Hazır";
        lblStatusDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblStatusDot.BorderRadius = 12;

        // Main table
        tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
        tblMain.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblMain.ColumnCount = 3;
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
        tblMain.RowCount = 1;
        tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.Controls.Add(pnlSidebar, 0, 0);
        tblMain.Controls.Add(tblCenter, 1, 0);
        tblMain.Controls.Add(pnlRight, 2, 0);

        // Sidebar
        pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlSidebar.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlSidebar.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
        pnlSidebar.Controls.Add(btnDashboard);
        pnlSidebar.Controls.Add(btnHistory);
        pnlSidebar.Controls.Add(btnSettings);
        pnlSidebar.Controls.Add(btnAbout);
        pnlSidebar.Controls.Add(lblVersion);

        ConfigureSidebarButton(btnDashboard, new System.Drawing.Point(12, 8));
        btnDashboard.Name = "btnDashboard";
        ConfigureSidebarButton(btnHistory, new System.Drawing.Point(12, 64));
        btnHistory.Name = "btnHistory";
        ConfigureSidebarButton(btnSettings, new System.Drawing.Point(12, 120));
        btnSettings.Name = "btnSettings";
        ConfigureSidebarButton(btnAbout, new System.Drawing.Point(12, 176));
        btnAbout.Name = "btnAbout";

        lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
        lblVersion.Height = 35;
        lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblVersion.ForeColor = System.Drawing.Color.FromArgb(60, 65, 75);
        lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
        lblVersion.Text = "Version 1.0.0";
        lblVersion.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);

        // Center layout
        tblCenter.Dock = System.Windows.Forms.DockStyle.Fill;
        tblCenter.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblCenter.Padding = new System.Windows.Forms.Padding(20);
        tblCenter.ColumnCount = 1;
        tblCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblCenter.RowCount = 2;
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
        tblCenter.Controls.Add(pnlEditor, 0, 0);
        tblCenter.Controls.Add(pnlAi, 0, 1);

        // Editor card
        pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditor.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditor.BorderRadius = 12;
        pnlEditor.BorderSize = 1;
        pnlEditor.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlEditor.DrawShadow = true;
        pnlEditor.Padding = new System.Windows.Forms.Padding(1);
        pnlEditor.Controls.Add(pnlEditorBody);
        pnlEditor.Controls.Add(pnlEditorFooter);
        pnlEditor.Controls.Add(pnlEditorHeader);

        pnlEditorHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlEditorHeader.Height = 46;
        pnlEditorHeader.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorHeader.Controls.Add(lblEditorTitle);
        pnlEditorHeader.Controls.Add(lblEditorLangBadge);

        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new System.Drawing.Point(14, 14);
        lblEditorTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblEditorTitle.Text = "Kod Giriş Alanı";

        lblEditorLangBadge.AutoSize = false;
        lblEditorLangBadge.Size = new System.Drawing.Size(36, 22);
        lblEditorLangBadge.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblEditorLangBadge.ForeColor = System.Drawing.Color.White;
        lblEditorLangBadge.Text = "C#";
        lblEditorLangBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLangBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
        lblEditorLangBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblEditorLangBadge.Location = new System.Drawing.Point(0, 12);

        pnlEditorFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlEditorFooter.Height = 34;
        pnlEditorFooter.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorFooter.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorFooter.Controls.Add(lblEditorUtf8);
        pnlEditorFooter.Controls.Add(lblEditorLines);

        lblEditorUtf8.AutoSize = false;
        lblEditorUtf8.Size = new System.Drawing.Size(56, 22);
        lblEditorUtf8.Location = new System.Drawing.Point(14, 6);
        lblEditorUtf8.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorUtf8.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorUtf8.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorUtf8.Text = "UTF-8";
        lblEditorUtf8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        lblEditorLines.AutoSize = false;
        lblEditorLines.Size = new System.Drawing.Size(80, 22);
        lblEditorLines.Location = new System.Drawing.Point(0, 6);
        lblEditorLines.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorLines.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorLines.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorLines.Text = "Lines: 1";
        lblEditorLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLines.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

        pnlEditorBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditorBody.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditorBody.Controls.Add(txtKodAlani);
        pnlEditorBody.Controls.Add(pnlLineNumbers);

        pnlLineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
        pnlLineNumbers.Width = 48;
        pnlLineNumbers.BackColor = System.Drawing.Color.FromArgb(14, 17, 24);
        pnlLineNumbers.Padding = new System.Windows.Forms.Padding(0, 4, 8, 4);

        txtKodAlani.Dock = System.Windows.Forms.DockStyle.Fill;
        txtKodAlani.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        txtKodAlani.ForeColor = System.Drawing.Color.FromArgb(212, 212, 212);
        txtKodAlani.BorderStyle = System.Windows.Forms.BorderStyle.None;
        txtKodAlani.Font = new System.Drawing.Font("Cascadia Code", 10.5F);
        txtKodAlani.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
        txtKodAlani.WordWrap = false;

        // AI panel
        pnlAi.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAi.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAi.BorderRadius = 12;
        pnlAi.BorderSize = 1;
        pnlAi.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlAi.DrawShadow = true;
        pnlAi.Controls.Add(pnlAiBody);
        pnlAi.Controls.Add(pnlAiHeader);

        pnlAiHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlAiHeader.Height = 58;
        pnlAiHeader.BackColor = System.Drawing.Color.FromArgb(60, 20, 90);
        pnlAiHeader.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
        pnlAiHeader.Controls.Add(lblAiHeaderIcon);
        pnlAiHeader.Controls.Add(lblAiHeaderTitle);
        pnlAiHeader.Controls.Add(lblAiHeaderSubtitle);

        lblAiHeaderIcon.AutoSize = true;
        lblAiHeaderIcon.Location = new System.Drawing.Point(14, 16);
        lblAiHeaderIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
        lblAiHeaderIcon.ForeColor = System.Drawing.Color.FromArgb(230, 230, 255);
        lblAiHeaderIcon.Text = "✨";

        lblAiHeaderTitle.AutoSize = true;
        lblAiHeaderTitle.Location = new System.Drawing.Point(44, 12);
        lblAiHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblAiHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(235, 235, 245);
        lblAiHeaderTitle.Text = "AI Önerileri";

        lblAiHeaderSubtitle.AutoSize = true;
        lblAiHeaderSubtitle.Location = new System.Drawing.Point(44, 33);
        lblAiHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblAiHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 170, 200);
        lblAiHeaderSubtitle.Text = "Yapay zeka destekli çözüm önerileri";

        pnlAiBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiBody.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiBody.Padding = new System.Windows.Forms.Padding(14);
        pnlAiBody.Controls.Add(pnlAiScroll);

        pnlAiScroll.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiScroll.AutoScroll = true;
        pnlAiScroll.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiScroll.Controls.Add(tblAiGrid);

        tblAiGrid.Dock = System.Windows.Forms.DockStyle.Top;
        tblAiGrid.AutoSize = true;
        tblAiGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tblAiGrid.ColumnCount = 3;
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.RowCount = 0;

        // Right panel
        pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlRight.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlRight.Padding = new System.Windows.Forms.Padding(20);
        pnlRight.Controls.Add(pnlResults);

        pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlResults.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlResults.BorderRadius = 12;
        pnlResults.BorderSize = 1;
        pnlResults.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlResults.DrawShadow = true;
        pnlResults.Controls.Add(flpIssues);
        pnlResults.Controls.Add(pnlResultsFooter);
        pnlResults.Controls.Add(pnlResultsHeader);

        pnlResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlResultsHeader.Height = 46;
        pnlResultsHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlResultsHeader.Controls.Add(lblResultsTitle);
        pnlResultsHeader.Controls.Add(lblIssuesBadge);

        lblResultsTitle.AutoSize = true;
        lblResultsTitle.Location = new System.Drawing.Point(14, 13);
        lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblResultsTitle.ForeColor = System.Drawing.Color.White;
        lblResultsTitle.Text = "Analiz Sonuçları";

        lblIssuesBadge.AutoSize = false;
        lblIssuesBadge.Size = new System.Drawing.Size(86, 24);
        lblIssuesBadge.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        lblIssuesBadge.ForeColor = System.Drawing.Color.White;
        lblIssuesBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblIssuesBadge.Text = "0 Issue";
        lblIssuesBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblIssuesBadge.BorderRadius = 12;
        lblIssuesBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblIssuesBadge.Location = new System.Drawing.Point(0, 11);

        flpIssues.Dock = System.Windows.Forms.DockStyle.Fill;
        flpIssues.AutoScroll = true;
        flpIssues.WrapContents = false;
        flpIssues.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        flpIssues.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        flpIssues.Padding = new System.Windows.Forms.Padding(12);

        pnlResultsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlResultsFooter.Height = 46;
        pnlResultsFooter.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsFooter.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
        pnlResultsFooter.Controls.Add(lblLow);
        pnlResultsFooter.Controls.Add(lblMedium);
        pnlResultsFooter.Controls.Add(lblHigh);

        lblHigh.Dock = System.Windows.Forms.DockStyle.Left;
        lblHigh.Width = 120;
        lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblHigh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblHigh.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblHigh.Text = "✓ Yüksek: 0";

        lblMedium.Dock = System.Windows.Forms.DockStyle.Left;
        lblMedium.Width = 110;
        lblMedium.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblMedium.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblMedium.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblMedium.Text = "✓ Orta: 0";

        lblLow.Dock = System.Windows.Forms.DockStyle.Left;
        lblLow.Width = 110;
        lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblLow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblLow.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblLow.Text = "✓ Düşük: 0";

        tblRoot.ResumeLayout(false);
        pnlTopHeader.ResumeLayout(false);
        pnlTopHeader.PerformLayout();
        tblMain.ResumeLayout(false);
        pnlSidebar.ResumeLayout(false);
        tblCenter.ResumeLayout(false);
        pnlEditor.ResumeLayout(false);
        pnlEditorHeader.ResumeLayout(false);
        pnlEditorHeader.PerformLayout();
        pnlEditorBody.ResumeLayout(false);
        pnlEditorFooter.ResumeLayout(false);
        pnlAi.ResumeLayout(false);
        pnlAiHeader.ResumeLayout(false);
        pnlAiHeader.PerformLayout();
        pnlAiBody.ResumeLayout(false);
        pnlAiScroll.ResumeLayout(false);
        pnlAiScroll.PerformLayout();
        pnlRight.ResumeLayout(false);
        pnlResults.ResumeLayout(false);
        pnlResultsHeader.ResumeLayout(false);
        pnlResultsHeader.PerformLayout();
        pnlResultsFooter.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void ConfigureSidebarButton(System.Windows.Forms.Button btn, System.Drawing.Point location)
    {
        btn.Location = location;
        btn.Size = new System.Drawing.Size(216, 46);
        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.ForeColor = System.Drawing.Color.White;
        btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        btn.Text = "";
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btn.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.Cursor = System.Windows.Forms.Cursors.Hand;
        btn.TabStop = false;
    }

    #endregion
}
*** End of File

#nullable enable
namespace DeepCodeAnalytics.UI;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.TableLayoutPanel tblRoot = null!;
    private System.Windows.Forms.Panel pnlTopHeader = null!;
    private System.Windows.Forms.TableLayoutPanel tblMain = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblLogoSquare = null!;
    private System.Windows.Forms.Label lblLogo = null!;
    private System.Windows.Forms.Label lblSubtitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnDosyaYukle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnAnalizEt = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblStatusDot = null!;

    private System.Windows.Forms.Panel pnlSidebar = null!;
    private System.Windows.Forms.Button btnDashboard = null!;
    private System.Windows.Forms.Button btnHistory = null!;
    private System.Windows.Forms.Button btnSettings = null!;
    private System.Windows.Forms.Button btnAbout = null!;
    private System.Windows.Forms.Label lblVersion = null!;

    private System.Windows.Forms.TableLayoutPanel tblCenter = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlEditor = null!;
    private System.Windows.Forms.Panel pnlEditorHeader = null!;
    private System.Windows.Forms.Label lblEditorTitle = null!;
    private System.Windows.Forms.Label lblEditorLangBadge = null!;
    private System.Windows.Forms.Panel pnlEditorBody = null!;
    private System.Windows.Forms.Panel pnlLineNumbers = null!;
    private System.Windows.Forms.RichTextBox txtKodAlani = null!;
    private System.Windows.Forms.Panel pnlEditorFooter = null!;
    private System.Windows.Forms.Label lblEditorUtf8 = null!;
    private System.Windows.Forms.Label lblEditorLines = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlAi = null!;
    private System.Windows.Forms.Panel pnlAiHeader = null!;
    private System.Windows.Forms.Label lblAiHeaderIcon = null!;
    private System.Windows.Forms.Label lblAiHeaderTitle = null!;
    private System.Windows.Forms.Label lblAiHeaderSubtitle = null!;
    private System.Windows.Forms.Panel pnlAiBody = null!;
    private System.Windows.Forms.Panel pnlAiScroll = null!;
    private System.Windows.Forms.TableLayoutPanel tblAiGrid = null!;

    private System.Windows.Forms.Panel pnlRight = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlResults = null!;
    private System.Windows.Forms.Panel pnlResultsHeader = null!;
    private System.Windows.Forms.Label lblResultsTitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblIssuesBadge = null!;
    private System.Windows.Forms.FlowLayoutPanel flpIssues = null!;
    private System.Windows.Forms.Panel pnlResultsFooter = null!;
    private System.Windows.Forms.Label lblHigh = null!;
    private System.Windows.Forms.Label lblMedium = null!;
    private System.Windows.Forms.Label lblLow = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        tblRoot = new System.Windows.Forms.TableLayoutPanel();
        pnlTopHeader = new System.Windows.Forms.Panel();
        tblMain = new System.Windows.Forms.TableLayoutPanel();

        lblLogoSquare = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        lblLogo = new System.Windows.Forms.Label();
        lblSubtitle = new System.Windows.Forms.Label();
        btnDosyaYukle = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        btnAnalizEt = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        lblStatusDot = new DeepCodeAnalytics.UI.Controls.RoundedLabel();

        pnlSidebar = new System.Windows.Forms.Panel();
        btnDashboard = new System.Windows.Forms.Button();
        btnHistory = new System.Windows.Forms.Button();
        btnSettings = new System.Windows.Forms.Button();
        btnAbout = new System.Windows.Forms.Button();
        lblVersion = new System.Windows.Forms.Label();

        tblCenter = new System.Windows.Forms.TableLayoutPanel();
        pnlEditor = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlEditorHeader = new System.Windows.Forms.Panel();
        lblEditorTitle = new System.Windows.Forms.Label();
        lblEditorLangBadge = new System.Windows.Forms.Label();
        pnlEditorBody = new System.Windows.Forms.Panel();
        pnlLineNumbers = new System.Windows.Forms.Panel();
        txtKodAlani = new System.Windows.Forms.RichTextBox();
        pnlEditorFooter = new System.Windows.Forms.Panel();
        lblEditorUtf8 = new System.Windows.Forms.Label();
        lblEditorLines = new System.Windows.Forms.Label();

        pnlAi = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlAiHeader = new System.Windows.Forms.Panel();
        lblAiHeaderIcon = new System.Windows.Forms.Label();
        lblAiHeaderTitle = new System.Windows.Forms.Label();
        lblAiHeaderSubtitle = new System.Windows.Forms.Label();
        pnlAiBody = new System.Windows.Forms.Panel();
        pnlAiScroll = new System.Windows.Forms.Panel();
        tblAiGrid = new System.Windows.Forms.TableLayoutPanel();

        pnlRight = new System.Windows.Forms.Panel();
        pnlResults = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlResultsHeader = new System.Windows.Forms.Panel();
        lblResultsTitle = new System.Windows.Forms.Label();
        lblIssuesBadge = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        flpIssues = new System.Windows.Forms.FlowLayoutPanel();
        pnlResultsFooter = new System.Windows.Forms.Panel();
        lblHigh = new System.Windows.Forms.Label();
        lblMedium = new System.Windows.Forms.Label();
        lblLow = new System.Windows.Forms.Label();

        tblRoot.SuspendLayout();
        pnlTopHeader.SuspendLayout();
        tblMain.SuspendLayout();
        pnlSidebar.SuspendLayout();
        tblCenter.SuspendLayout();
        pnlEditor.SuspendLayout();
        pnlEditorHeader.SuspendLayout();
        pnlEditorBody.SuspendLayout();
        pnlEditorFooter.SuspendLayout();
        pnlAi.SuspendLayout();
        pnlAiHeader.SuspendLayout();
        pnlAiBody.SuspendLayout();
        pnlAiScroll.SuspendLayout();
        pnlRight.SuspendLayout();
        pnlResults.SuspendLayout();
        pnlResultsHeader.SuspendLayout();
        pnlResultsFooter.SuspendLayout();
        SuspendLayout();

        // Form
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        ClientSize = new System.Drawing.Size(1280, 780);
        MinimumSize = new System.Drawing.Size(1100, 650);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        // Root
        tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
        tblRoot.ColumnCount = 1;
        tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.RowCount = 2;
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.Controls.Add(pnlTopHeader, 0, 0);
        tblRoot.Controls.Add(tblMain, 0, 1);
        Controls.Add(tblRoot);

        // Top header
        pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlTopHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlTopHeader.Controls.Add(lblLogoSquare);
        pnlTopHeader.Controls.Add(lblLogo);
        pnlTopHeader.Controls.Add(lblSubtitle);
        pnlTopHeader.Controls.Add(btnDosyaYukle);
        pnlTopHeader.Controls.Add(btnAnalizEt);
        pnlTopHeader.Controls.Add(lblStatusDot);

        lblLogoSquare.AutoSize = false;
        lblLogoSquare.Size = new System.Drawing.Size(40, 40);
        lblLogoSquare.Location = new System.Drawing.Point(20, 10);
        lblLogoSquare.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblLogoSquare.ForeColor = System.Drawing.Color.White;
        lblLogoSquare.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogoSquare.Text = "DC";
        lblLogoSquare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblLogoSquare.BorderRadius = 10;

        lblLogo.AutoSize = true;
        lblLogo.Location = new System.Drawing.Point(70, 8);
        lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogo.ForeColor = System.Drawing.Color.White;
        lblLogo.Text = "DeepCode Analytics";

        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new System.Drawing.Point(70, 32);
        lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 120, 130);
        lblSubtitle.Text = "AI-Powered Code Analysis";

        btnDosyaYukle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnDosyaYukle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        btnDosyaYukle.ForeColor = System.Drawing.Color.White;
        btnDosyaYukle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnDosyaYukle.Size = new System.Drawing.Size(160, 40);
        btnDosyaYukle.Location = new System.Drawing.Point(860, 10);
        btnDosyaYukle.Text = "📁 Dosya Yükle";
        btnDosyaYukle.BorderRadius = 8;
        btnDosyaYukle.Cursor = System.Windows.Forms.Cursors.Hand;
        btnDosyaYukle.Click += btnDosyaYukle_Click;

        btnAnalizEt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnAnalizEt.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        btnAnalizEt.ForeColor = System.Drawing.Color.White;
        btnAnalizEt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnAnalizEt.Size = new System.Drawing.Size(160, 40);
        btnAnalizEt.Location = new System.Drawing.Point(1030, 10);
        btnAnalizEt.Text = "▶ Analiz Et";
        btnAnalizEt.BorderRadius = 8;
        btnAnalizEt.Cursor = System.Windows.Forms.Cursors.Hand;
        btnAnalizEt.Click += btnAnalizEt_Click;

        lblStatusDot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblStatusDot.AutoSize = false;
        lblStatusDot.Size = new System.Drawing.Size(70, 24);
        lblStatusDot.Location = new System.Drawing.Point(1200, 18);
        lblStatusDot.BackColor = System.Drawing.Color.FromArgb(30, 60, 30);
        lblStatusDot.ForeColor = System.Drawing.Color.FromArgb(60, 179, 113);
        lblStatusDot.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblStatusDot.Text = "● Hazır";
        lblStatusDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblStatusDot.BorderRadius = 12;

        // Main table
        tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
        tblMain.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblMain.ColumnCount = 3;
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
        tblMain.RowCount = 1;
        tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.Controls.Add(pnlSidebar, 0, 0);
        tblMain.Controls.Add(tblCenter, 1, 0);
        tblMain.Controls.Add(pnlRight, 2, 0);

        // Sidebar
        pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlSidebar.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlSidebar.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
        pnlSidebar.Controls.Add(btnDashboard);
        pnlSidebar.Controls.Add(btnHistory);
        pnlSidebar.Controls.Add(btnSettings);
        pnlSidebar.Controls.Add(btnAbout);
        pnlSidebar.Controls.Add(lblVersion);

        ConfigureSidebarButton(btnDashboard, new System.Drawing.Point(12, 8));
        btnDashboard.Name = "btnDashboard";
        ConfigureSidebarButton(btnHistory, new System.Drawing.Point(12, 64));
        btnHistory.Name = "btnHistory";
        ConfigureSidebarButton(btnSettings, new System.Drawing.Point(12, 120));
        btnSettings.Name = "btnSettings";
        ConfigureSidebarButton(btnAbout, new System.Drawing.Point(12, 176));
        btnAbout.Name = "btnAbout";

        lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
        lblVersion.Height = 35;
        lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblVersion.ForeColor = System.Drawing.Color.FromArgb(60, 65, 75);
        lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
        lblVersion.Text = "Version 1.0.0";
        lblVersion.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);

        // Center layout
        tblCenter.Dock = System.Windows.Forms.DockStyle.Fill;
        tblCenter.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblCenter.Padding = new System.Windows.Forms.Padding(20);
        tblCenter.ColumnCount = 1;
        tblCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblCenter.RowCount = 2;
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
        tblCenter.Controls.Add(pnlEditor, 0, 0);
        tblCenter.Controls.Add(pnlAi, 0, 1);

        // Editor
        pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditor.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditor.BorderRadius = 12;
        pnlEditor.BorderSize = 1;
        pnlEditor.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlEditor.DrawShadow = true;
        pnlEditor.Padding = new System.Windows.Forms.Padding(1);
        pnlEditor.Controls.Add(pnlEditorBody);
        pnlEditor.Controls.Add(pnlEditorFooter);
        pnlEditor.Controls.Add(pnlEditorHeader);

        pnlEditorHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlEditorHeader.Height = 46;
        pnlEditorHeader.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorHeader.Controls.Add(lblEditorTitle);
        pnlEditorHeader.Controls.Add(lblEditorLangBadge);

        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new System.Drawing.Point(14, 14);
        lblEditorTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblEditorTitle.Text = "Kod Giriş Alanı";

        lblEditorLangBadge.AutoSize = false;
        lblEditorLangBadge.Size = new System.Drawing.Size(36, 22);
        lblEditorLangBadge.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblEditorLangBadge.ForeColor = System.Drawing.Color.White;
        lblEditorLangBadge.Text = "C#";
        lblEditorLangBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLangBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
        lblEditorLangBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblEditorLangBadge.Location = new System.Drawing.Point(0, 12);

        pnlEditorFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlEditorFooter.Height = 34;
        pnlEditorFooter.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorFooter.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorFooter.Controls.Add(lblEditorUtf8);
        pnlEditorFooter.Controls.Add(lblEditorLines);

        lblEditorUtf8.AutoSize = false;
        lblEditorUtf8.Size = new System.Drawing.Size(56, 22);
        lblEditorUtf8.Location = new System.Drawing.Point(14, 6);
        lblEditorUtf8.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorUtf8.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorUtf8.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorUtf8.Text = "UTF-8";
        lblEditorUtf8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        lblEditorLines.AutoSize = false;
        lblEditorLines.Size = new System.Drawing.Size(80, 22);
        lblEditorLines.Location = new System.Drawing.Point(0, 6);
        lblEditorLines.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorLines.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorLines.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorLines.Text = "Lines: 1";
        lblEditorLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLines.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

        pnlEditorBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditorBody.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditorBody.Controls.Add(txtKodAlani);
        pnlEditorBody.Controls.Add(pnlLineNumbers);

        pnlLineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
        pnlLineNumbers.Width = 48;
        pnlLineNumbers.BackColor = System.Drawing.Color.FromArgb(14, 17, 24);
        pnlLineNumbers.Padding = new System.Windows.Forms.Padding(0, 4, 8, 4);

        txtKodAlani.Dock = System.Windows.Forms.DockStyle.Fill;
        txtKodAlani.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        txtKodAlani.ForeColor = System.Drawing.Color.FromArgb(212, 212, 212);
        txtKodAlani.BorderStyle = System.Windows.Forms.BorderStyle.None;
        txtKodAlani.Font = new System.Drawing.Font("Cascadia Code", 10.5F);
        txtKodAlani.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
        txtKodAlani.WordWrap = false;

        // AI panel
        pnlAi.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAi.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAi.BorderRadius = 12;
        pnlAi.BorderSize = 1;
        pnlAi.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlAi.DrawShadow = true;
        pnlAi.Controls.Add(pnlAiBody);
        pnlAi.Controls.Add(pnlAiHeader);

        pnlAiHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlAiHeader.Height = 58;
        pnlAiHeader.BackColor = System.Drawing.Color.FromArgb(60, 20, 90);
        pnlAiHeader.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
        pnlAiHeader.Controls.Add(lblAiHeaderIcon);
        pnlAiHeader.Controls.Add(lblAiHeaderTitle);
        pnlAiHeader.Controls.Add(lblAiHeaderSubtitle);

        lblAiHeaderIcon.AutoSize = true;
        lblAiHeaderIcon.Location = new System.Drawing.Point(14, 16);
        lblAiHeaderIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
        lblAiHeaderIcon.ForeColor = System.Drawing.Color.FromArgb(230, 230, 255);
        lblAiHeaderIcon.Text = "✨";

        lblAiHeaderTitle.AutoSize = true;
        lblAiHeaderTitle.Location = new System.Drawing.Point(44, 12);
        lblAiHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblAiHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(235, 235, 245);
        lblAiHeaderTitle.Text = "AI Önerileri";

        lblAiHeaderSubtitle.AutoSize = true;
        lblAiHeaderSubtitle.Location = new System.Drawing.Point(44, 33);
        lblAiHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblAiHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 170, 200);
        lblAiHeaderSubtitle.Text = "Yapay zeka destekli çözüm önerileri";

        pnlAiBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiBody.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiBody.Padding = new System.Windows.Forms.Padding(14);
        pnlAiBody.Controls.Add(pnlAiScroll);

        pnlAiScroll.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiScroll.AutoScroll = true;
        pnlAiScroll.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiScroll.Controls.Add(tblAiGrid);

        tblAiGrid.Dock = System.Windows.Forms.DockStyle.Top;
        tblAiGrid.AutoSize = true;
        tblAiGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tblAiGrid.ColumnCount = 3;
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.RowCount = 0;

        // Right panel
        pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlRight.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlRight.Padding = new System.Windows.Forms.Padding(20);
        pnlRight.Controls.Add(pnlResults);

        pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlResults.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlResults.BorderRadius = 12;
        pnlResults.BorderSize = 1;
        pnlResults.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlResults.DrawShadow = true;
        pnlResults.Controls.Add(flpIssues);
        pnlResults.Controls.Add(pnlResultsFooter);
        pnlResults.Controls.Add(pnlResultsHeader);

        pnlResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlResultsHeader.Height = 46;
        pnlResultsHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlResultsHeader.Controls.Add(lblResultsTitle);
        pnlResultsHeader.Controls.Add(lblIssuesBadge);

        lblResultsTitle.AutoSize = true;
        lblResultsTitle.Location = new System.Drawing.Point(14, 13);
        lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblResultsTitle.ForeColor = System.Drawing.Color.White;
        lblResultsTitle.Text = "Analiz Sonuçları";

        lblIssuesBadge.AutoSize = false;
        lblIssuesBadge.Size = new System.Drawing.Size(86, 24);
        lblIssuesBadge.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        lblIssuesBadge.ForeColor = System.Drawing.Color.White;
        lblIssuesBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblIssuesBadge.Text = "0 Issue";
        lblIssuesBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblIssuesBadge.BorderRadius = 12;
        lblIssuesBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblIssuesBadge.Location = new System.Drawing.Point(0, 11);

        flpIssues.Dock = System.Windows.Forms.DockStyle.Fill;
        flpIssues.AutoScroll = true;
        flpIssues.WrapContents = false;
        flpIssues.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        flpIssues.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        flpIssues.Padding = new System.Windows.Forms.Padding(12);

        pnlResultsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlResultsFooter.Height = 46;
        pnlResultsFooter.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsFooter.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
        pnlResultsFooter.Controls.Add(lblLow);
        pnlResultsFooter.Controls.Add(lblMedium);
        pnlResultsFooter.Controls.Add(lblHigh);

        lblHigh.Dock = System.Windows.Forms.DockStyle.Left;
        lblHigh.Width = 120;
        lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblHigh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblHigh.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblHigh.Text = "✓ Yüksek: 0";

        lblMedium.Dock = System.Windows.Forms.DockStyle.Left;
        lblMedium.Width = 110;
        lblMedium.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblMedium.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblMedium.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblMedium.Text = "✓ Orta: 0";

        lblLow.Dock = System.Windows.Forms.DockStyle.Left;
        lblLow.Width = 110;
        lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblLow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblLow.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblLow.Text = "✓ Düşük: 0";

        tblRoot.ResumeLayout(false);
        pnlTopHeader.ResumeLayout(false);
        pnlTopHeader.PerformLayout();
        tblMain.ResumeLayout(false);
        pnlSidebar.ResumeLayout(false);
        tblCenter.ResumeLayout(false);
        pnlEditor.ResumeLayout(false);
        pnlEditorHeader.ResumeLayout(false);
        pnlEditorHeader.PerformLayout();
        pnlEditorBody.ResumeLayout(false);
        pnlEditorFooter.ResumeLayout(false);
        pnlAi.ResumeLayout(false);
        pnlAiHeader.ResumeLayout(false);
        pnlAiHeader.PerformLayout();
        pnlAiBody.ResumeLayout(false);
        pnlAiScroll.ResumeLayout(false);
        pnlAiScroll.PerformLayout();
        pnlRight.ResumeLayout(false);
        pnlResults.ResumeLayout(false);
        pnlResultsHeader.ResumeLayout(false);
        pnlResultsHeader.PerformLayout();
        pnlResultsFooter.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void ConfigureSidebarButton(System.Windows.Forms.Button btn, System.Drawing.Point location)
    {
        btn.Location = location;
        btn.Size = new System.Drawing.Size(216, 46);
        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.ForeColor = System.Drawing.Color.White;
        btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        btn.Text = "";
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btn.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.Cursor = System.Windows.Forms.Cursors.Hand;
        btn.TabStop = false;
    }

    #endregion
}

#nullable enable
namespace DeepCodeAnalytics.UI;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    // Root layout
    private System.Windows.Forms.TableLayoutPanel tblRoot = null!;
    private System.Windows.Forms.Panel pnlTopHeader = null!;
    private System.Windows.Forms.TableLayoutPanel tblMain = null!;

    // Top bar
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblLogoSquare = null!;
    private System.Windows.Forms.Label lblLogo = null!;
    private System.Windows.Forms.Label lblSubtitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnDosyaYukle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnAnalizEt = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblStatusDot = null!;

    // Sidebar
    private System.Windows.Forms.Panel pnlSidebar = null!;
    private System.Windows.Forms.Button btnDashboard = null!;
    private System.Windows.Forms.Button btnHistory = null!;
    private System.Windows.Forms.Button btnSettings = null!;
    private System.Windows.Forms.Button btnAbout = null!;
    private System.Windows.Forms.Label lblVersion = null!;

    // Center area (editor + AI)
    private System.Windows.Forms.TableLayoutPanel tblCenter = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlEditor = null!;
    private System.Windows.Forms.Panel pnlEditorHeader = null!;
    private System.Windows.Forms.Label lblEditorTitle = null!;
    private System.Windows.Forms.Label lblEditorLangBadge = null!;
    private System.Windows.Forms.Panel pnlEditorBody = null!;
    private System.Windows.Forms.Panel pnlLineNumbers = null!;
    private System.Windows.Forms.RichTextBox txtKodAlani = null!;
    private System.Windows.Forms.Panel pnlEditorFooter = null!;
    private System.Windows.Forms.Label lblEditorUtf8 = null!;
    private System.Windows.Forms.Label lblEditorLines = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlAi = null!;
    private System.Windows.Forms.Panel pnlAiHeader = null!;
    private System.Windows.Forms.Label lblAiHeaderIcon = null!;
    private System.Windows.Forms.Label lblAiHeaderTitle = null!;
    private System.Windows.Forms.Label lblAiHeaderSubtitle = null!;
    private System.Windows.Forms.Panel pnlAiBody = null!;
    private System.Windows.Forms.Panel pnlAiScroll = null!;
    private System.Windows.Forms.TableLayoutPanel tblAiGrid = null!;

    // Right panel (results)
    private System.Windows.Forms.Panel pnlRight = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlResults = null!;
    private System.Windows.Forms.Panel pnlResultsHeader = null!;
    private System.Windows.Forms.Label lblResultsTitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblIssuesBadge = null!;
    private System.Windows.Forms.FlowLayoutPanel flpIssues = null!;
    private System.Windows.Forms.Panel pnlResultsFooter = null!;
    private System.Windows.Forms.Label lblHigh = null!;
    private System.Windows.Forms.Label lblMedium = null!;
    private System.Windows.Forms.Label lblLow = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        tblRoot = new System.Windows.Forms.TableLayoutPanel();
        pnlTopHeader = new System.Windows.Forms.Panel();
        tblMain = new System.Windows.Forms.TableLayoutPanel();

        lblLogoSquare = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        lblLogo = new System.Windows.Forms.Label();
        lblSubtitle = new System.Windows.Forms.Label();
        btnDosyaYukle = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        btnAnalizEt = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        lblStatusDot = new DeepCodeAnalytics.UI.Controls.RoundedLabel();

        pnlSidebar = new System.Windows.Forms.Panel();
        btnDashboard = new System.Windows.Forms.Button();
        btnHistory = new System.Windows.Forms.Button();
        btnSettings = new System.Windows.Forms.Button();
        btnAbout = new System.Windows.Forms.Button();
        lblVersion = new System.Windows.Forms.Label();

        tblCenter = new System.Windows.Forms.TableLayoutPanel();
        pnlEditor = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlEditorHeader = new System.Windows.Forms.Panel();
        lblEditorTitle = new System.Windows.Forms.Label();
        lblEditorLangBadge = new System.Windows.Forms.Label();
        pnlEditorBody = new System.Windows.Forms.Panel();
        pnlLineNumbers = new System.Windows.Forms.Panel();
        txtKodAlani = new System.Windows.Forms.RichTextBox();
        pnlEditorFooter = new System.Windows.Forms.Panel();
        lblEditorUtf8 = new System.Windows.Forms.Label();
        lblEditorLines = new System.Windows.Forms.Label();

        pnlAi = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlAiHeader = new System.Windows.Forms.Panel();
        lblAiHeaderIcon = new System.Windows.Forms.Label();
        lblAiHeaderTitle = new System.Windows.Forms.Label();
        lblAiHeaderSubtitle = new System.Windows.Forms.Label();
        pnlAiBody = new System.Windows.Forms.Panel();
        pnlAiScroll = new System.Windows.Forms.Panel();
        tblAiGrid = new System.Windows.Forms.TableLayoutPanel();

        pnlRight = new System.Windows.Forms.Panel();
        pnlResults = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlResultsHeader = new System.Windows.Forms.Panel();
        lblResultsTitle = new System.Windows.Forms.Label();
        lblIssuesBadge = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        flpIssues = new System.Windows.Forms.FlowLayoutPanel();
        pnlResultsFooter = new System.Windows.Forms.Panel();
        lblHigh = new System.Windows.Forms.Label();
        lblMedium = new System.Windows.Forms.Label();
        lblLow = new System.Windows.Forms.Label();

        tblRoot.SuspendLayout();
        pnlTopHeader.SuspendLayout();
        tblMain.SuspendLayout();
        pnlSidebar.SuspendLayout();
        tblCenter.SuspendLayout();
        pnlEditor.SuspendLayout();
        pnlEditorHeader.SuspendLayout();
        pnlEditorBody.SuspendLayout();
        pnlEditorFooter.SuspendLayout();
        pnlAi.SuspendLayout();
        pnlAiHeader.SuspendLayout();
        pnlAiBody.SuspendLayout();
        pnlAiScroll.SuspendLayout();
        pnlRight.SuspendLayout();
        pnlResults.SuspendLayout();
        pnlResultsHeader.SuspendLayout();
        pnlResultsFooter.SuspendLayout();
        SuspendLayout();

        // Form
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18); // #121212
        ClientSize = new System.Drawing.Size(1280, 780);
        MinimumSize = new System.Drawing.Size(1100, 650);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        // Root: TopBar + Main
        tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
        tblRoot.ColumnCount = 1;
        tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.RowCount = 2;
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
        tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblRoot.Controls.Add(pnlTopHeader, 0, 0);
        tblRoot.Controls.Add(tblMain, 0, 1);
        Controls.Add(tblRoot);

        // Top header (60px, #0B0E14)
        pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlTopHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlTopHeader.Controls.Add(lblLogoSquare);
        pnlTopHeader.Controls.Add(lblLogo);
        pnlTopHeader.Controls.Add(lblSubtitle);
        pnlTopHeader.Controls.Add(btnDosyaYukle);
        pnlTopHeader.Controls.Add(btnAnalizEt);
        pnlTopHeader.Controls.Add(lblStatusDot);

        // DC logo square
        lblLogoSquare.AutoSize = false;
        lblLogoSquare.Size = new System.Drawing.Size(40, 40);
        lblLogoSquare.Location = new System.Drawing.Point(20, 10);
        lblLogoSquare.BackColor = System.Drawing.Color.FromArgb(0, 122, 204); // #007ACC
        lblLogoSquare.ForeColor = System.Drawing.Color.White;
        lblLogoSquare.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogoSquare.Text = "DC";
        lblLogoSquare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblLogoSquare.BorderRadius = 10;

        // Title + subtitle
        lblLogo.AutoSize = true;
        lblLogo.Location = new System.Drawing.Point(70, 8);
        lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogo.ForeColor = System.Drawing.Color.White;
        lblLogo.Text = "DeepCode Analytics";

        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new System.Drawing.Point(70, 32);
        lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 120, 130);
        lblSubtitle.Text = "AI-Powered Code Analysis";

        // Upload button
        btnDosyaYukle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnDosyaYukle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48); // #2D2D30
        btnDosyaYukle.ForeColor = System.Drawing.Color.White;
        btnDosyaYukle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnDosyaYukle.Size = new System.Drawing.Size(160, 40);
        btnDosyaYukle.Location = new System.Drawing.Point(860, 10);
        btnDosyaYukle.Text = "📁 Dosya Yükle";
        btnDosyaYukle.BorderRadius = 8;
        btnDosyaYukle.Cursor = System.Windows.Forms.Cursors.Hand;
        btnDosyaYukle.Click += btnDosyaYukle_Click;

        // Analyze button
        btnAnalizEt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnAnalizEt.BackColor = System.Drawing.Color.FromArgb(253, 126, 20); // #FD7E14
        btnAnalizEt.ForeColor = System.Drawing.Color.White;
        btnAnalizEt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnAnalizEt.Size = new System.Drawing.Size(160, 40);
        btnAnalizEt.Location = new System.Drawing.Point(1030, 10);
        btnAnalizEt.Text = "▶ Analiz Et";
        btnAnalizEt.BorderRadius = 8;
        btnAnalizEt.Cursor = System.Windows.Forms.Cursors.Hand;
        btnAnalizEt.Click += btnAnalizEt_Click;

        // Status badge
        lblStatusDot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblStatusDot.AutoSize = false;
        lblStatusDot.Size = new System.Drawing.Size(70, 24);
        lblStatusDot.Location = new System.Drawing.Point(1200, 18);
        lblStatusDot.BackColor = System.Drawing.Color.FromArgb(30, 60, 30); // #1E3C1E-ish
        lblStatusDot.ForeColor = System.Drawing.Color.FromArgb(60, 179, 113); // #3CB371
        lblStatusDot.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblStatusDot.Text = "● Hazır";
        lblStatusDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblStatusDot.BorderRadius = 12;

        // Main: Sidebar + Center + Right
        tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
        tblMain.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblMain.ColumnCount = 3;
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
        tblMain.RowCount = 1;
        tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblMain.Controls.Add(pnlSidebar, 0, 0);
        tblMain.Controls.Add(tblCenter, 1, 0);
        tblMain.Controls.Add(pnlRight, 2, 0);

        // Sidebar
        pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlSidebar.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlSidebar.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
        pnlSidebar.Controls.Add(btnDashboard);
        pnlSidebar.Controls.Add(btnHistory);
        pnlSidebar.Controls.Add(btnSettings);
        pnlSidebar.Controls.Add(btnAbout);
        pnlSidebar.Controls.Add(lblVersion);

        ConfigureSidebarButton(btnDashboard, new System.Drawing.Point(12, 8));
        btnDashboard.Name = "btnDashboard";
        ConfigureSidebarButton(btnHistory, new System.Drawing.Point(12, 64));
        btnHistory.Name = "btnHistory";
        ConfigureSidebarButton(btnSettings, new System.Drawing.Point(12, 120));
        btnSettings.Name = "btnSettings";
        ConfigureSidebarButton(btnAbout, new System.Drawing.Point(12, 176));
        btnAbout.Name = "btnAbout";

        lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
        lblVersion.Height = 35;
        lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblVersion.ForeColor = System.Drawing.Color.FromArgb(60, 65, 75);
        lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
        lblVersion.Text = "Version 1.0.0";
        lblVersion.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);

        // Center layout (view-dependent row heights are set in Form1.cs)
        tblCenter.Dock = System.Windows.Forms.DockStyle.Fill;
        tblCenter.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        tblCenter.Padding = new System.Windows.Forms.Padding(20);
        tblCenter.ColumnCount = 1;
        tblCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblCenter.RowCount = 2;
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // editor
        tblCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F)); // AI header-only in view1
        tblCenter.Controls.Add(pnlEditor, 0, 0);
        tblCenter.Controls.Add(pnlAi, 0, 1);

        // Editor card
        pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditor.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditor.BorderRadius = 12;
        pnlEditor.BorderSize = 1;
        pnlEditor.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlEditor.DrawShadow = true;
        pnlEditor.Padding = new System.Windows.Forms.Padding(1);
        pnlEditor.Controls.Add(pnlEditorBody);
        pnlEditor.Controls.Add(pnlEditorFooter);
        pnlEditor.Controls.Add(pnlEditorHeader);

        pnlEditorHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlEditorHeader.Height = 46;
        pnlEditorHeader.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorHeader.Controls.Add(lblEditorTitle);
        pnlEditorHeader.Controls.Add(lblEditorLangBadge);

        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new System.Drawing.Point(14, 14);
        lblEditorTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblEditorTitle.Text = "Kod Giriş Alanı";

        lblEditorLangBadge.AutoSize = false;
        lblEditorLangBadge.Size = new System.Drawing.Size(36, 22);
        lblEditorLangBadge.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblEditorLangBadge.ForeColor = System.Drawing.Color.White;
        lblEditorLangBadge.Text = "C#";
        lblEditorLangBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLangBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
        lblEditorLangBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblEditorLangBadge.Location = new System.Drawing.Point(0, 12); // positioned in code-behind

        pnlEditorFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlEditorFooter.Height = 34;
        pnlEditorFooter.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorFooter.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlEditorFooter.Controls.Add(lblEditorUtf8);
        pnlEditorFooter.Controls.Add(lblEditorLines);

        lblEditorUtf8.AutoSize = false;
        lblEditorUtf8.Size = new System.Drawing.Size(56, 22);
        lblEditorUtf8.Location = new System.Drawing.Point(14, 6);
        lblEditorUtf8.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorUtf8.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorUtf8.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorUtf8.Text = "UTF-8";
        lblEditorUtf8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        lblEditorLines.AutoSize = false;
        lblEditorLines.Size = new System.Drawing.Size(80, 22);
        lblEditorLines.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorLines.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorLines.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorLines.Text = "Lines: 1";
        lblEditorLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLines.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblEditorLines.Location = new System.Drawing.Point(0, 6); // positioned in code-behind

        pnlEditorBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditorBody.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditorBody.Controls.Add(txtKodAlani);
        pnlEditorBody.Controls.Add(pnlLineNumbers);

        pnlLineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
        pnlLineNumbers.Width = 48;
        pnlLineNumbers.BackColor = System.Drawing.Color.FromArgb(14, 17, 24);
        pnlLineNumbers.Padding = new System.Windows.Forms.Padding(0, 4, 8, 4);

        txtKodAlani.Dock = System.Windows.Forms.DockStyle.Fill;
        txtKodAlani.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        txtKodAlani.ForeColor = System.Drawing.Color.FromArgb(212, 212, 212);
        txtKodAlani.BorderStyle = System.Windows.Forms.BorderStyle.None;
        txtKodAlani.Font = new System.Drawing.Font("Cascadia Code", 10.5F);
        txtKodAlani.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
        txtKodAlani.WordWrap = false;

        // AI panel (bottom)
        pnlAi.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAi.BackColor = System.Drawing.Color.FromArgb(26, 26, 46); // #1A1A2E
        pnlAi.BorderRadius = 12;
        pnlAi.BorderSize = 1;
        pnlAi.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlAi.DrawShadow = true;
        pnlAi.Controls.Add(pnlAiBody);
        pnlAi.Controls.Add(pnlAiHeader);

        pnlAiHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlAiHeader.Height = 58;
        pnlAiHeader.BackColor = System.Drawing.Color.FromArgb(60, 20, 90);
        pnlAiHeader.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
        pnlAiHeader.Controls.Add(lblAiHeaderIcon);
        pnlAiHeader.Controls.Add(lblAiHeaderTitle);
        pnlAiHeader.Controls.Add(lblAiHeaderSubtitle);

        lblAiHeaderIcon.AutoSize = true;
        lblAiHeaderIcon.Location = new System.Drawing.Point(14, 16);
        lblAiHeaderIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
        lblAiHeaderIcon.ForeColor = System.Drawing.Color.FromArgb(230, 230, 255);
        lblAiHeaderIcon.Text = "✨";

        lblAiHeaderTitle.AutoSize = true;
        lblAiHeaderTitle.Location = new System.Drawing.Point(44, 12);
        lblAiHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblAiHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(235, 235, 245);
        lblAiHeaderTitle.Text = "AI Önerileri";

        lblAiHeaderSubtitle.AutoSize = true;
        lblAiHeaderSubtitle.Location = new System.Drawing.Point(44, 33);
        lblAiHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblAiHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 170, 200);
        lblAiHeaderSubtitle.Text = "Yapay zeka destekli çözüm önerileri";

        pnlAiBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiBody.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiBody.Padding = new System.Windows.Forms.Padding(14);
        pnlAiBody.Controls.Add(pnlAiScroll);

        pnlAiScroll.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiScroll.AutoScroll = true;
        pnlAiScroll.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiScroll.Controls.Add(tblAiGrid);

        tblAiGrid.Dock = System.Windows.Forms.DockStyle.Top;
        tblAiGrid.AutoSize = true;
        tblAiGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tblAiGrid.ColumnCount = 3;
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.RowCount = 0;

        // Right panel container
        pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlRight.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlRight.Padding = new System.Windows.Forms.Padding(20);
        pnlRight.Controls.Add(pnlResults);

        pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlResults.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlResults.BorderRadius = 12;
        pnlResults.BorderSize = 1;
        pnlResults.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlResults.DrawShadow = true;
        pnlResults.Controls.Add(flpIssues);
        pnlResults.Controls.Add(pnlResultsFooter);
        pnlResults.Controls.Add(pnlResultsHeader);

        pnlResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlResultsHeader.Height = 46;
        pnlResultsHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsHeader.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
        pnlResultsHeader.Controls.Add(lblResultsTitle);
        pnlResultsHeader.Controls.Add(lblIssuesBadge);

        lblResultsTitle.AutoSize = true;
        lblResultsTitle.Location = new System.Drawing.Point(14, 13);
        lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblResultsTitle.ForeColor = System.Drawing.Color.White;
        lblResultsTitle.Text = "Analiz Sonuçları";

        lblIssuesBadge.AutoSize = false;
        lblIssuesBadge.Size = new System.Drawing.Size(86, 24);
        lblIssuesBadge.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        lblIssuesBadge.ForeColor = System.Drawing.Color.White;
        lblIssuesBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblIssuesBadge.Text = "0 Issue";
        lblIssuesBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblIssuesBadge.BorderRadius = 12;
        lblIssuesBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblIssuesBadge.Location = new System.Drawing.Point(0, 11); // positioned in code-behind

        flpIssues.Dock = System.Windows.Forms.DockStyle.Fill;
        flpIssues.AutoScroll = true;
        flpIssues.WrapContents = false;
        flpIssues.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        flpIssues.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        flpIssues.Padding = new System.Windows.Forms.Padding(12);

        pnlResultsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlResultsFooter.Height = 46;
        pnlResultsFooter.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlResultsFooter.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
        pnlResultsFooter.Controls.Add(lblLow);
        pnlResultsFooter.Controls.Add(lblMedium);
        pnlResultsFooter.Controls.Add(lblHigh);

        lblHigh.Dock = System.Windows.Forms.DockStyle.Left;
        lblHigh.Width = 120;
        lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblHigh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblHigh.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblHigh.Text = "✓ Yüksek: 0";

        lblMedium.Dock = System.Windows.Forms.DockStyle.Left;
        lblMedium.Width = 110;
        lblMedium.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblMedium.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblMedium.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblMedium.Text = "✓ Orta: 0";

        lblLow.Dock = System.Windows.Forms.DockStyle.Left;
        lblLow.Width = 110;
        lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblLow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblLow.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblLow.Text = "✓ Düşük: 0";

        tblRoot.ResumeLayout(false);
        pnlTopHeader.ResumeLayout(false);
        pnlTopHeader.PerformLayout();
        tblMain.ResumeLayout(false);
        pnlSidebar.ResumeLayout(false);
        tblCenter.ResumeLayout(false);
        pnlEditor.ResumeLayout(false);
        pnlEditorHeader.ResumeLayout(false);
        pnlEditorHeader.PerformLayout();
        pnlEditorBody.ResumeLayout(false);
        pnlEditorFooter.ResumeLayout(false);
        pnlAi.ResumeLayout(false);
        pnlAiHeader.ResumeLayout(false);
        pnlAiHeader.PerformLayout();
        pnlAiBody.ResumeLayout(false);
        pnlAiScroll.ResumeLayout(false);
        pnlAiScroll.PerformLayout();
        pnlRight.ResumeLayout(false);
        pnlResults.ResumeLayout(false);
        pnlResultsHeader.ResumeLayout(false);
        pnlResultsHeader.PerformLayout();
        pnlResultsFooter.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void ConfigureSidebarButton(System.Windows.Forms.Button btn, System.Drawing.Point location)
    {
        btn.Location = location;
        btn.Size = new System.Drawing.Size(216, 46);
        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.ForeColor = System.Drawing.Color.White;
        btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        btn.Text = "";
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btn.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.Cursor = System.Windows.Forms.Cursors.Hand;
        btn.TabStop = false;
    }

    #endregion
}

#nullable enable
namespace DeepCodeAnalytics.UI;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Panel pnlTopHeader = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblLogoSquare = null!;
    private System.Windows.Forms.Label lblLogo = null!;
    private System.Windows.Forms.Label lblSubtitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnAnalizEt = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedButton btnDosyaYukle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblStatusDot = null!;

    private System.Windows.Forms.Panel pnlSidebar = null!;
    private System.Windows.Forms.Button btnDashboard = null!;
    private System.Windows.Forms.Button btnHistory = null!;
    private System.Windows.Forms.Button btnSettings = null!;
    private System.Windows.Forms.Button btnAbout = null!;
    private System.Windows.Forms.Label lblSidebarDivider = null!;
    private System.Windows.Forms.Label lblVersion = null!;

    private System.Windows.Forms.Panel pnlMainContainer = null!;
    private System.Windows.Forms.TableLayoutPanel tblMainLayout = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlEditorContainer = null!;
    private System.Windows.Forms.Panel pnlEditorHeader = null!;
    private System.Windows.Forms.Label lblEditorTitle = null!;
    private System.Windows.Forms.Label lblEditorLangBadge = null!;
    private System.Windows.Forms.Panel pnlEditorFooter = null!;
    private System.Windows.Forms.Label lblEditorUtf8 = null!;
    private System.Windows.Forms.Label lblEditorLinesBadge = null!;
    private System.Windows.Forms.Panel pnlLineNumbers = null!;
    private System.Windows.Forms.RichTextBox txtKodAlani = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlResultsCard = null!;
    private System.Windows.Forms.Panel pnlResultsHeader = null!;
    private System.Windows.Forms.Label lblResultsTitle = null!;
    private DeepCodeAnalytics.UI.Controls.RoundedLabel lblTotalIssuesBadge = null!;
    private System.Windows.Forms.FlowLayoutPanel pnlErrorCards = null!;
    private System.Windows.Forms.Panel pnlResultsFooter = null!;
    private System.Windows.Forms.Label lblFooterYusek = null!;
    private System.Windows.Forms.Label lblFooterOrta = null!;
    private System.Windows.Forms.Label lblFooterDusuk = null!;

    private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlAiPanel = null!;
    private System.Windows.Forms.Panel pnlAiHeader = null!;
    private System.Windows.Forms.Label lblAiIcon = null!;
    private System.Windows.Forms.Label lblAiTitle = null!;
    private System.Windows.Forms.Label lblAiSubtitle = null!;
    private System.Windows.Forms.Panel pnlAiBody = null!;
    private System.Windows.Forms.Panel pnlAiScroll = null!;
    private System.Windows.Forms.TableLayoutPanel tblAiGrid = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        pnlTopHeader = new System.Windows.Forms.Panel();
        lblLogoSquare = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        lblLogo = new System.Windows.Forms.Label();
        lblSubtitle = new System.Windows.Forms.Label();
        btnAnalizEt = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        btnDosyaYukle = new DeepCodeAnalytics.UI.Controls.RoundedButton();
        lblStatusDot = new DeepCodeAnalytics.UI.Controls.RoundedLabel();

        pnlSidebar = new System.Windows.Forms.Panel();
        btnDashboard = new System.Windows.Forms.Button();
        btnHistory = new System.Windows.Forms.Button();
        btnSettings = new System.Windows.Forms.Button();
        btnAbout = new System.Windows.Forms.Button();
        lblSidebarDivider = new System.Windows.Forms.Label();
        lblVersion = new System.Windows.Forms.Label();

        pnlMainContainer = new System.Windows.Forms.Panel();
        tblMainLayout = new System.Windows.Forms.TableLayoutPanel();

        pnlEditorContainer = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlEditorHeader = new System.Windows.Forms.Panel();
        lblEditorTitle = new System.Windows.Forms.Label();
        lblEditorLangBadge = new System.Windows.Forms.Label();
        lblEditorUtf8 = new System.Windows.Forms.Label();
        lblEditorLinesBadge = new System.Windows.Forms.Label();
        pnlEditorFooter = new System.Windows.Forms.Panel();
        pnlLineNumbers = new System.Windows.Forms.Panel();
        txtKodAlani = new System.Windows.Forms.RichTextBox();

        pnlResultsCard = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlResultsHeader = new System.Windows.Forms.Panel();
        lblResultsTitle = new System.Windows.Forms.Label();
        lblTotalIssuesBadge = new DeepCodeAnalytics.UI.Controls.RoundedLabel();
        pnlErrorCards = new System.Windows.Forms.FlowLayoutPanel();
        pnlResultsFooter = new System.Windows.Forms.Panel();
        lblFooterYusek = new System.Windows.Forms.Label();
        lblFooterOrta = new System.Windows.Forms.Label();
        lblFooterDusuk = new System.Windows.Forms.Label();

        pnlAiPanel = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
        pnlAiHeader = new System.Windows.Forms.Panel();
        lblAiIcon = new System.Windows.Forms.Label();
        lblAiTitle = new System.Windows.Forms.Label();
        lblAiSubtitle = new System.Windows.Forms.Label();
        pnlAiBody = new System.Windows.Forms.Panel();
        pnlAiScroll = new System.Windows.Forms.Panel();
        tblAiGrid = new System.Windows.Forms.TableLayoutPanel();

        pnlTopHeader.SuspendLayout();
        pnlSidebar.SuspendLayout();
        pnlMainContainer.SuspendLayout();
        tblMainLayout.SuspendLayout();
        pnlEditorContainer.SuspendLayout();
        pnlEditorHeader.SuspendLayout();
        pnlEditorFooter.SuspendLayout();
        pnlResultsCard.SuspendLayout();
        pnlResultsHeader.SuspendLayout();
        pnlResultsFooter.SuspendLayout();
        pnlAiPanel.SuspendLayout();
        pnlAiHeader.SuspendLayout();
        pnlAiBody.SuspendLayout();
        pnlAiScroll.SuspendLayout();
        SuspendLayout();

        // Form
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        ClientSize = new System.Drawing.Size(1240, 760);
        MinimumSize = new System.Drawing.Size(1120, 680);
        Name = "Form1";
        Text = "DeepCode Analytics";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        // Top Header
        pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlTopHeader.Height = 60;
        pnlTopHeader.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlTopHeader.Controls.Add(lblLogoSquare);
        pnlTopHeader.Controls.Add(lblLogo);
        pnlTopHeader.Controls.Add(lblSubtitle);
        pnlTopHeader.Controls.Add(btnDosyaYukle);
        pnlTopHeader.Controls.Add(btnAnalizEt);
        pnlTopHeader.Controls.Add(lblStatusDot);

        lblLogoSquare.AutoSize = false;
        lblLogoSquare.Size = new System.Drawing.Size(40, 40);
        lblLogoSquare.Location = new System.Drawing.Point(20, 10);
        lblLogoSquare.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblLogoSquare.ForeColor = System.Drawing.Color.White;
        lblLogoSquare.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogoSquare.Text = "DC";
        lblLogoSquare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblLogoSquare.BorderRadius = 10;

        lblLogo.AutoSize = true;
        lblLogo.Location = new System.Drawing.Point(70, 8);
        lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        lblLogo.ForeColor = System.Drawing.Color.White;
        lblLogo.Text = "DeepCode Analytics";

        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new System.Drawing.Point(70, 32);
        lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 120, 130);
        lblSubtitle.Text = "AI-Powered Code Analysis";

        btnDosyaYukle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnDosyaYukle.Location = new System.Drawing.Point(850, 10);
        btnDosyaYukle.Size = new System.Drawing.Size(150, 40);
        btnDosyaYukle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        btnDosyaYukle.ForeColor = System.Drawing.Color.White;
        btnDosyaYukle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnDosyaYukle.Text = "📁 Dosya Yükle";
        btnDosyaYukle.BorderRadius = 8;
        btnDosyaYukle.Cursor = System.Windows.Forms.Cursors.Hand;
        btnDosyaYukle.Click += btnDosyaYukle_Click;

        btnAnalizEt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnAnalizEt.Location = new System.Drawing.Point(1010, 10);
        btnAnalizEt.Size = new System.Drawing.Size(150, 40);
        btnAnalizEt.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        btnAnalizEt.ForeColor = System.Drawing.Color.White;
        btnAnalizEt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnAnalizEt.Text = "▶ Analiz Et";
        btnAnalizEt.BorderRadius = 8;
        btnAnalizEt.Cursor = System.Windows.Forms.Cursors.Hand;
        btnAnalizEt.Click += btnAnalizEt_Click;

        lblStatusDot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblStatusDot.Location = new System.Drawing.Point(1170, 18);
        lblStatusDot.AutoSize = false;
        lblStatusDot.Size = new System.Drawing.Size(70, 24);
        lblStatusDot.BackColor = System.Drawing.Color.FromArgb(30, 58, 30); // #1E3A1E
        lblStatusDot.ForeColor = System.Drawing.Color.FromArgb(60, 179, 113); // #3CB371
        lblStatusDot.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblStatusDot.Text = "● Hazır";
        lblStatusDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblStatusDot.BorderRadius = 12;

        // Sidebar
        pnlSidebar.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
        pnlSidebar.Width = 240;
        pnlSidebar.Padding = new System.Windows.Forms.Padding(0, 24, 0, 0);
        pnlSidebar.Controls.Add(btnDashboard);
        pnlSidebar.Controls.Add(btnHistory);
        pnlSidebar.Controls.Add(btnSettings);
        pnlSidebar.Controls.Add(btnAbout);
        pnlSidebar.Controls.Add(lblVersion);
        pnlSidebar.Controls.Add(lblSidebarDivider);

        ConfigureSidebarButton(btnDashboard, new System.Drawing.Point(12, 0));
        btnDashboard.Name = "btnDashboard";
        ConfigureSidebarButton(btnHistory, new System.Drawing.Point(12, 56));
        btnHistory.Name = "btnHistory";
        ConfigureSidebarButton(btnSettings, new System.Drawing.Point(12, 112));
        btnSettings.Name = "btnSettings";
        ConfigureSidebarButton(btnAbout, new System.Drawing.Point(12, 168));
        btnAbout.Name = "btnAbout";

        lblSidebarDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
        lblSidebarDivider.Height = 1;
        lblSidebarDivider.BackColor = System.Drawing.Color.FromArgb(35, 40, 50);
        lblSidebarDivider.Text = "";

        lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
        lblVersion.Height = 35;
        lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblVersion.ForeColor = System.Drawing.Color.FromArgb(60, 65, 75);
        lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
        lblVersion.Text = "Version 1.0.0";
        lblVersion.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);

        // Main Container
        pnlMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlMainContainer.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
        pnlMainContainer.Padding = new System.Windows.Forms.Padding(24);
        pnlMainContainer.Controls.Add(tblMainLayout);

        // Main Layout
        tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        tblMainLayout.ColumnCount = 2;
        tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
        tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
        tblMainLayout.RowCount = 2;
        tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
        tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
        tblMainLayout.Controls.Add(pnlEditorContainer, 0, 0);
        tblMainLayout.Controls.Add(pnlResultsCard, 1, 0);
        tblMainLayout.Controls.Add(pnlAiPanel, 0, 1);
        tblMainLayout.SetColumnSpan(pnlAiPanel, 2);
        pnlEditorContainer.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
        pnlResultsCard.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
        pnlAiPanel.Margin = new System.Windows.Forms.Padding(0, 14, 0, 0);

        // Editor Card
        pnlEditorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlEditorContainer.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        pnlEditorContainer.BorderRadius = 12;
        pnlEditorContainer.BorderSize = 1;
        pnlEditorContainer.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlEditorContainer.DrawShadow = true;
        pnlEditorContainer.Padding = new System.Windows.Forms.Padding(1);
        pnlEditorContainer.Controls.Add(txtKodAlani);
        pnlEditorContainer.Controls.Add(pnlLineNumbers);
        pnlEditorContainer.Controls.Add(pnlEditorFooter);
        pnlEditorContainer.Controls.Add(pnlEditorHeader);

        pnlEditorHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlEditorHeader.Height = 46;
        pnlEditorHeader.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorHeader.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
        pnlEditorHeader.Controls.Add(lblEditorTitle);
        pnlEditorHeader.Controls.Add(lblEditorLangBadge);

        lblEditorTitle.AutoSize = true;
        lblEditorTitle.Location = new System.Drawing.Point(14, 14);
        lblEditorTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblEditorTitle.Text = "Kod Giriş Alanı";

        lblEditorLangBadge.AutoSize = false;
        lblEditorLangBadge.Size = new System.Drawing.Size(36, 22);
        lblEditorLangBadge.ForeColor = System.Drawing.Color.White;
        lblEditorLangBadge.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        lblEditorLangBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLangBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
        lblEditorLangBadge.Text = "C#";
        lblEditorLangBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblEditorLangBadge.Location = new System.Drawing.Point(0, 12); // Form1.cs will position precisely

        // Editor Footer (UTF-8 + Lines)
        pnlEditorFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlEditorFooter.Height = 34;
        pnlEditorFooter.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlEditorFooter.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
        pnlEditorFooter.Controls.Add(lblEditorUtf8);
        pnlEditorFooter.Controls.Add(lblEditorLinesBadge);

        lblEditorUtf8.AutoSize = false;
        lblEditorUtf8.Size = new System.Drawing.Size(56, 22);
        lblEditorUtf8.Location = new System.Drawing.Point(12, 6);
        lblEditorUtf8.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorUtf8.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorUtf8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorUtf8.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorUtf8.Text = "UTF-8";

        lblEditorLinesBadge.AutoSize = false;
        lblEditorLinesBadge.Size = new System.Drawing.Size(80, 22);
        lblEditorLinesBadge.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
        lblEditorLinesBadge.BackColor = System.Drawing.Color.FromArgb(22, 27, 38);
        lblEditorLinesBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblEditorLinesBadge.Font = new System.Drawing.Font("Consolas", 8F);
        lblEditorLinesBadge.Text = "Lines: 1";
        lblEditorLinesBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblEditorLinesBadge.Location = new System.Drawing.Point(0, 6); // Form1.cs will position precisely

        pnlLineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
        pnlLineNumbers.Width = 48;
        pnlLineNumbers.BackColor = System.Drawing.Color.FromArgb(14, 17, 24);
        pnlLineNumbers.Padding = new System.Windows.Forms.Padding(0, 4, 8, 4);

        txtKodAlani.Dock = System.Windows.Forms.DockStyle.Fill;
        txtKodAlani.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        txtKodAlani.BorderStyle = System.Windows.Forms.BorderStyle.None;
        txtKodAlani.Font = new System.Drawing.Font("Cascadia Code", 10.5F);
        txtKodAlani.ForeColor = System.Drawing.Color.FromArgb(212, 212, 212);
        txtKodAlani.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
        txtKodAlani.WordWrap = false;
        txtKodAlani.Text =
@"public class UserService
{
    public void UpdateUserProfile()
    {
        // ...
    }
}";

        // Results Card
        pnlResultsCard.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlResultsCard.BackColor = System.Drawing.Color.FromArgb(26, 26, 46); // #1A1A2E
        pnlResultsCard.BorderRadius = 12;
        pnlResultsCard.BorderSize = 1;
        pnlResultsCard.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51); // #333333
        pnlResultsCard.DrawShadow = true;
        pnlResultsCard.Controls.Add(pnlErrorCards);
        pnlResultsCard.Controls.Add(pnlResultsHeader);
        pnlResultsCard.Controls.Add(pnlResultsFooter);

        pnlResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlResultsHeader.Height = 46;
        pnlResultsHeader.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlResultsHeader.Controls.Add(lblResultsTitle);
        pnlResultsHeader.Controls.Add(lblTotalIssuesBadge);

        lblResultsTitle.Location = new System.Drawing.Point(14, 13);
        lblResultsTitle.AutoSize = true;
        lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblResultsTitle.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
        lblResultsTitle.Text = "Analiz Sonuçları";

        lblTotalIssuesBadge.Size = new System.Drawing.Size(86, 24);
        lblTotalIssuesBadge.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
        lblTotalIssuesBadge.ForeColor = System.Drawing.Color.White;
        lblTotalIssuesBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblTotalIssuesBadge.Text = "4 Issue";
        lblTotalIssuesBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblTotalIssuesBadge.BorderRadius = 12;
        lblTotalIssuesBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblTotalIssuesBadge.Location = new System.Drawing.Point(0, 11); // Form1.cs will position precisely

        pnlErrorCards.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlErrorCards.AutoScroll = true;
        pnlErrorCards.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        pnlErrorCards.WrapContents = false;
        pnlErrorCards.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
        pnlErrorCards.BackColor = System.Drawing.Color.FromArgb(26, 26, 46); // #1A1A2E

        pnlResultsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnlResultsFooter.Height = 38;
        pnlResultsFooter.BackColor = System.Drawing.Color.FromArgb(16, 20, 28);
        pnlResultsFooter.Controls.Add(lblFooterDusuk);
        pnlResultsFooter.Controls.Add(lblFooterOrta);
        pnlResultsFooter.Controls.Add(lblFooterYusek);

        lblFooterYusek.Dock = System.Windows.Forms.DockStyle.Left;
        lblFooterYusek.Width = 120;
        lblFooterYusek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblFooterYusek.ForeColor = System.Drawing.Color.FromArgb(180, 185, 195);
        lblFooterYusek.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        lblFooterYusek.Text = "🔴 Yüksek: 0";

        lblFooterOrta.Dock = System.Windows.Forms.DockStyle.Left;
        lblFooterOrta.Width = 110;
        lblFooterOrta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblFooterOrta.ForeColor = System.Drawing.Color.FromArgb(180, 185, 195);
        lblFooterOrta.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        lblFooterOrta.Text = "🟠 Orta: 0";

        lblFooterDusuk.Dock = System.Windows.Forms.DockStyle.Left;
        lblFooterDusuk.Width = 110;
        lblFooterDusuk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lblFooterDusuk.ForeColor = System.Drawing.Color.FromArgb(180, 185, 195);
        lblFooterDusuk.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        lblFooterDusuk.Text = "🔵 Düşük: 0";

        // AI Panel (Bottom)
        pnlAiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiPanel.BackColor = System.Drawing.Color.FromArgb(26, 26, 46); // #1A1A2E
        pnlAiPanel.BorderRadius = 12;
        pnlAiPanel.BorderSize = 1;
        pnlAiPanel.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        pnlAiPanel.DrawShadow = true;
        pnlAiPanel.Controls.Add(pnlAiBody);
        pnlAiPanel.Controls.Add(pnlAiHeader);

        pnlAiHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlAiHeader.Height = 58;
        pnlAiHeader.BackColor = System.Drawing.Color.FromArgb(40, 18, 60); // fallback, real gradient in Form1.cs Paint
        pnlAiHeader.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
        pnlAiHeader.Controls.Add(lblAiIcon);
        pnlAiHeader.Controls.Add(lblAiTitle);
        pnlAiHeader.Controls.Add(lblAiSubtitle);

        lblAiIcon.AutoSize = true;
        lblAiIcon.Location = new System.Drawing.Point(14, 16);
        lblAiIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F, System.Drawing.FontStyle.Regular);
        lblAiIcon.ForeColor = System.Drawing.Color.FromArgb(220, 220, 255);
        lblAiIcon.Text = "✨";

        lblAiTitle.AutoSize = true;
        lblAiTitle.Location = new System.Drawing.Point(44, 12);
        lblAiTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblAiTitle.ForeColor = System.Drawing.Color.FromArgb(235, 235, 245);
        lblAiTitle.Text = "AI Önerileri";

        lblAiSubtitle.AutoSize = true;
        lblAiSubtitle.Location = new System.Drawing.Point(44, 33);
        lblAiSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        lblAiSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 170, 200);
        lblAiSubtitle.Text = "Yapay zeka destekli çözüm önerileri";

        pnlAiBody.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiBody.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiBody.Padding = new System.Windows.Forms.Padding(14);
        pnlAiBody.Controls.Add(pnlAiScroll);

        pnlAiScroll.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlAiScroll.AutoScroll = true;
        pnlAiScroll.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
        pnlAiScroll.Controls.Add(tblAiGrid);

        tblAiGrid.AutoSize = true;
        tblAiGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tblAiGrid.Dock = System.Windows.Forms.DockStyle.Top;
        tblAiGrid.ColumnCount = 3;
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
        tblAiGrid.RowCount = 1;
        tblAiGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));

        // Root Controls
        Controls.Add(pnlMainContainer);
        Controls.Add(pnlSidebar);
        Controls.Add(pnlTopHeader);

        pnlTopHeader.ResumeLayout(false);
        pnlTopHeader.PerformLayout();
        pnlSidebar.ResumeLayout(false);
        pnlMainContainer.ResumeLayout(false);
        tblMainLayout.ResumeLayout(false);
        pnlEditorContainer.ResumeLayout(false);
        pnlEditorHeader.ResumeLayout(false);
        pnlEditorHeader.PerformLayout();
        pnlEditorFooter.ResumeLayout(false);
        pnlEditorFooter.PerformLayout();
        pnlResultsCard.ResumeLayout(false);
        pnlResultsHeader.ResumeLayout(false);
        pnlResultsHeader.PerformLayout();
        pnlResultsFooter.ResumeLayout(false);
        pnlAiPanel.ResumeLayout(false);
        pnlAiHeader.ResumeLayout(false);
        pnlAiHeader.PerformLayout();
        pnlAiBody.ResumeLayout(false);
        pnlAiScroll.ResumeLayout(false);
        pnlAiScroll.PerformLayout();
        ResumeLayout(false);
    }

    private static void ConfigureSidebarButton(System.Windows.Forms.Button btn, System.Drawing.Point location)
    {
        btn.Location = location;
        btn.Size = new System.Drawing.Size(216, 46);
        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.ForeColor = System.Drawing.Color.White;
        btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        btn.Text = "";
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btn.BackColor = System.Drawing.Color.FromArgb(11, 14, 20);
        btn.Cursor = System.Windows.Forms.Cursors.Hand;
        btn.TabStop = false;
    }

    #endregion
}
