using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using DeepCodeAnalytics.Application.DTOs;
using DeepCodeAnalytics.Application.Services;
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Entities;
using DeepCodeAnalytics.Infrastructure.Analyzers;
using DeepCodeAnalytics.UI.Controls;

namespace DeepCodeAnalytics.UI;

public partial class Form1 : Form
{
    private readonly AnalizYoneticisi _analizYoneticisi;

    // ===== UI: root =====
    private readonly TableLayoutPanel _tblRoot = new();
    private readonly Panel _pnlTopHeader = new();
    private readonly TableLayoutPanel _tblMain = new();

    // ===== UI: top bar =====
    private readonly RoundedLabel _lblLogoSquare = new();
    private readonly Label _lblLogo = new();
    private readonly Label _lblSubtitle = new();
    private readonly RoundedButton _btnDosyaYukle = new();
    private readonly RoundedButton _btnAnalizEt = new();
    private readonly RoundedLabel _lblStatusDot = new();

    // ===== UI: sidebar =====
    private readonly Panel _pnlSidebar = new();
    private readonly Button _btnDashboard = new();
    private readonly Button _btnHistory = new();
    private readonly Button _btnSettings = new();
    private readonly Button _btnAbout = new();
    private readonly Label _lblVersion = new();

    // ===== UI: center =====
    private readonly TableLayoutPanel _tblCenter = new();
    private readonly Panel _pnlCenterDivider = new();

    private readonly RoundedPanel _pnlEditor = new();
    private readonly Panel _pnlEditorHeader = new();
    private readonly Label _lblEditorTitle = new();
    private readonly Label _lblEditorLangBadge = new();
    private readonly Panel _pnlEditorBody = new();
    private readonly Panel _pnlLineNumbers = new();
    private readonly RichTextBox _txtKodAlani = new();
    private readonly Panel _pnlEditorFooter = new();
    private readonly Label _lblEditorUtf8 = new();
    private readonly Label _lblEditorLines = new();

    private readonly RoundedPanel _pnlAi = new();
    private readonly Panel _pnlAiHeader = new();
    private readonly Label _lblAiHeaderIcon = new();
    private readonly Label _lblAiHeaderTitle = new();
    private readonly Label _lblAiHeaderSubtitle = new();
    private readonly Panel _pnlAiBody = new();
    private readonly TableLayoutPanel _tblAiColumns = new();
    private readonly Panel _pnlAiColProblem = new();
    private readonly Panel _pnlAiColDesc = new();
    private readonly Panel _pnlAiColSolution = new();
    private readonly Panel _pnlAiHdrProblem = new();
    private readonly Panel _pnlAiHdrDesc = new();
    private readonly Panel _pnlAiHdrSolution = new();
    private readonly FlowLayoutPanel _flpAiProblem = new();
    private readonly FlowLayoutPanel _flpAiDesc = new();
    private readonly FlowLayoutPanel _flpAiSolution = new();

    // ===== UI: right/results =====
    private readonly Panel _pnlRight = new();
    private readonly RoundedPanel _pnlResults = new();
    private readonly Panel _pnlResultsHeader = new();
    private readonly Label _lblResultsTitle = new();
    private readonly RoundedLabel _lblIssuesBadge = new();
    private readonly FlowLayoutPanel _flpIssues = new();
    private readonly Panel _pnlResultsFooter = new();
    private readonly Label _lblHigh = new();
    private readonly Label _lblMedium = new();
    private readonly Label _lblLow = new();

    // ===== Two views =====
    private enum ViewMode { PreAnalyze, PostAnalyze }
    private ViewMode _viewMode = ViewMode.PreAnalyze;

    // ===== Sidebar paint state =====
    private Button? _activeSidebarButton;
    private Button? _hoverSidebarButton;
    private int _sidebarFadeAlpha;
    private readonly System.Windows.Forms.Timer _sidebarFadeTimer;

    private static readonly Color SidebarBg = Color.FromArgb(11, 14, 20);
    private static readonly Color HoverTint = Color.FromArgb(18, 22, 32);
    private static readonly Color InactiveText = Color.FromArgb(160, 165, 180);
    private static readonly Color Border333 = Color.FromArgb(51, 51, 51); // #333333
    private static readonly Color CardBg = Color.FromArgb(26, 26, 46); // #1A1A2E
    private static readonly Font FontTitle = new("Segoe UI", 11F, FontStyle.Bold);
    private static readonly Font FontText = new("Segoe UI", 9.5F, FontStyle.Regular);
    private static readonly Font FontTextBold = new("Segoe UI", 9.5F, FontStyle.Bold);
    private static readonly Font FontCode = new("Cascadia Code", 10.5F, FontStyle.Regular);

    /// <summary>
    /// Ana form. Analiz yöneticisi constructor'dan enjekte edilir.
    /// </summary>
    public Form1(AnalizYoneticisi analizYoneticisi)
    {
        // --- Eski Backend Entegrasyon Kodları (Referans Amaçlı Korunmuştur) ---
        // private KodAnalizServisi _kodAnalizServisi;
        // public void SonuclariGoster(AnalizSonucu sonuc) { ... }
        // private void btnAnalizEt_Click(object sender, EventArgs e) { ... }
        // ----------------------------------------------------------------------
        _analizYoneticisi = analizYoneticisi;

        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

        _sidebarFadeTimer = new System.Windows.Forms.Timer { Interval = 16 };
        _sidebarFadeTimer.Tick += SidebarFadeTimer_Tick;

        // UI tamamen programatik kurulur.
        BuildUi();
        WireUi();

        ApplyView(ViewMode.PreAnalyze);

        Shown += (_, _) =>
        {
            PositionTopBarRightControls();
            PositionEditorBadges();
            PositionResultsBadge();
            ApplyDarkScrollbars(this);
        };
        Resize += (_, _) =>
        {
            PositionTopBarRightControls();
            PositionEditorBadges();
            PositionResultsBadge();
        };
    }

    // =========================
    // UI construction
    // =========================
    /// <summary>
    /// Tüm WinForms kontrol ağacını oluşturur ve yerleşimleri kurar.
    /// </summary>
    private void BuildUi()
    {
        Controls.Clear();
        Font = FontText; // Varsayılan font: Segoe UI 9.5
        BackColor = Color.FromArgb(18, 18, 18); // #121212

        // Root
        _tblRoot.Dock = DockStyle.Fill;
        _tblRoot.BackColor = Color.FromArgb(18, 18, 18); // #121212
        _tblRoot.CellBorderStyle = TableLayoutPanelCellBorderStyle.None; // Beyaz çizgi/border olmasın
        _tblRoot.ColumnCount = 1;
        _tblRoot.RowCount = 2;
        _tblRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tblRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        _tblRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        Controls.Add(_tblRoot);

        // Top header
        _pnlTopHeader.Dock = DockStyle.Fill;
        _pnlTopHeader.BackColor = Color.FromArgb(11, 14, 20);
        _pnlTopHeader.BorderStyle = BorderStyle.None; // border çizgisi olmasın
        _tblRoot.Controls.Add(_pnlTopHeader, 0, 0);

        _lblLogoSquare.Size = new Size(40, 40);
        _lblLogoSquare.Location = new Point(20, 10);
        _lblLogoSquare.BackColor = Color.FromArgb(0, 122, 204);
        _lblLogoSquare.ForeColor = Color.White;
        _lblLogoSquare.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        _lblLogoSquare.Text = "DC";
        _lblLogoSquare.TextAlign = ContentAlignment.MiddleCenter;
        _lblLogoSquare.BorderRadius = 10;

        _lblLogo.AutoSize = true;
        _lblLogo.Location = new Point(70, 8);
        _lblLogo.Font = FontTitle;
        _lblLogo.ForeColor = Color.White;
        _lblLogo.Text = "DeepCode Analytics";

        _lblSubtitle.AutoSize = true;
        _lblSubtitle.Location = new Point(70, 32);
        _lblSubtitle.Font = FontText;
        _lblSubtitle.ForeColor = Color.FromArgb(120, 120, 130);
        _lblSubtitle.Text = "AI-Powered Code Analysis";

        _btnDosyaYukle.Size = new Size(160, 40);
        _btnDosyaYukle.BackColor = Color.FromArgb(45, 45, 48); // #2D2D30
        _btnDosyaYukle.ForeColor = Color.White;
        _btnDosyaYukle.Font = FontTextBold;
        _btnDosyaYukle.Text = "📁 Dosya Yükle";
        _btnDosyaYukle.BorderRadius = 8;
        _btnDosyaYukle.Cursor = Cursors.Hand;

        _btnAnalizEt.Size = new Size(160, 40);
        _btnAnalizEt.BackColor = Color.FromArgb(253, 126, 20); // #FD7E14
        _btnAnalizEt.ForeColor = Color.White;
        _btnAnalizEt.Font = FontTextBold;
        _btnAnalizEt.Text = "▶ Analiz Et";
        _btnAnalizEt.BorderRadius = 8;
        _btnAnalizEt.Cursor = Cursors.Hand;

        _lblStatusDot.Size = new Size(70, 24);
        _lblStatusDot.BackColor = Color.FromArgb(30, 60, 30); // #1E3C1E
        _lblStatusDot.ForeColor = Color.FromArgb(60, 179, 113); // #3CB371
        _lblStatusDot.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        _lblStatusDot.Text = "● Hazır";
        _lblStatusDot.TextAlign = ContentAlignment.MiddleCenter;
        _lblStatusDot.BorderRadius = 12;

        _pnlTopHeader.Controls.Add(_lblLogoSquare);
        _pnlTopHeader.Controls.Add(_lblLogo);
        _pnlTopHeader.Controls.Add(_lblSubtitle);
        _pnlTopHeader.Controls.Add(_btnDosyaYukle);
        _pnlTopHeader.Controls.Add(_btnAnalizEt);
        _pnlTopHeader.Controls.Add(_lblStatusDot);

        // Main table
        _tblMain.Dock = DockStyle.Fill;
        _tblMain.BackColor = Color.FromArgb(18, 18, 18);
        _tblMain.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        _tblMain.ColumnCount = 3;
        _tblMain.RowCount = 1;
        _tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F));
        _tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
        _tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tblRoot.Controls.Add(_tblMain, 0, 1);

        // Sidebar
        _pnlSidebar.Dock = DockStyle.Fill;
        _pnlSidebar.BackColor = SidebarBg;
        _pnlSidebar.Padding = new Padding(0, 16, 0, 0);
        _pnlSidebar.BorderStyle = BorderStyle.None;
        _tblMain.Controls.Add(_pnlSidebar, 0, 0);

        ConfigureSidebarButton(_btnDashboard, new Point(12, 8), nameof(_btnDashboard));
        ConfigureSidebarButton(_btnHistory, new Point(12, 64), nameof(_btnHistory));
        ConfigureSidebarButton(_btnSettings, new Point(12, 120), nameof(_btnSettings));
        ConfigureSidebarButton(_btnAbout, new Point(12, 176), nameof(_btnAbout));

        _lblVersion.Dock = DockStyle.Bottom;
        _lblVersion.Height = 35;
        _lblVersion.TextAlign = ContentAlignment.MiddleCenter;
        _lblVersion.ForeColor = Color.FromArgb(60, 65, 75);
        _lblVersion.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
        _lblVersion.Text = "Version 1.0.0";
        _lblVersion.BackColor = SidebarBg;

        _pnlSidebar.Controls.Add(_btnDashboard);
        _pnlSidebar.Controls.Add(_btnHistory);
        _pnlSidebar.Controls.Add(_btnSettings);
        _pnlSidebar.Controls.Add(_btnAbout);
        _pnlSidebar.Controls.Add(_lblVersion);

        // Center layout
        _tblCenter.Dock = DockStyle.Fill;
        _tblCenter.BackColor = Color.FromArgb(18, 18, 18);
        _tblCenter.Padding = new Padding(20);
        _tblCenter.ColumnCount = 1;
        _tblCenter.RowCount = 3;
        _tblCenter.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        _tblCenter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tblCenter.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));  // editor
        _tblCenter.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));  // divider
        _tblCenter.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));  // AI
        _tblMain.Controls.Add(_tblCenter, 1, 0);

        _pnlCenterDivider.Dock = DockStyle.Fill;
        _pnlCenterDivider.BackColor = Border333;

        // Editor card
        _pnlEditor.Dock = DockStyle.Fill;
        _pnlEditor.BackColor = Color.FromArgb(11, 14, 20);
        _pnlEditor.BorderRadius = 12;
        _pnlEditor.BorderSize = 1;
        _pnlEditor.BorderColor = Border333;
        _pnlEditor.DrawShadow = true;
        _pnlEditor.Padding = new Padding(1);

        _pnlEditorHeader.Dock = DockStyle.Top;
        _pnlEditorHeader.Height = 46;
        _pnlEditorHeader.BackColor = Color.FromArgb(16, 20, 28);
        _pnlEditorHeader.Padding = new Padding(14, 0, 14, 0);

        _lblEditorTitle.AutoSize = true;
        _lblEditorTitle.Location = new Point(14, 14);
        _lblEditorTitle.Font = FontTitle;
        _lblEditorTitle.ForeColor = Color.FromArgb(200, 205, 215);
        _lblEditorTitle.Text = "Kod Giriş Alanı";

        _lblEditorLangBadge.Size = new Size(36, 22);
        _lblEditorLangBadge.BackColor = Color.FromArgb(0, 122, 204);
        _lblEditorLangBadge.ForeColor = Color.White;
        _lblEditorLangBadge.Text = "C#";
        _lblEditorLangBadge.TextAlign = ContentAlignment.MiddleCenter;
        _lblEditorLangBadge.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

        _pnlEditorHeader.Controls.Add(_lblEditorTitle);
        _pnlEditorHeader.Controls.Add(_lblEditorLangBadge);

        _pnlEditorFooter.Dock = DockStyle.Bottom;
        _pnlEditorFooter.Height = 34;
        _pnlEditorFooter.BackColor = Color.FromArgb(16, 20, 28);
        _pnlEditorFooter.Padding = new Padding(14, 0, 14, 0);

        ConfigureEditorPill(_lblEditorUtf8, "UTF-8");
        ConfigureEditorPill(_lblEditorLines, "Lines: 1");
        _pnlEditorFooter.Controls.Add(_lblEditorUtf8);
        _pnlEditorFooter.Controls.Add(_lblEditorLines);

        _pnlEditorBody.Dock = DockStyle.Fill;
        _pnlEditorBody.BackColor = Color.FromArgb(11, 14, 20);

        _pnlLineNumbers.Dock = DockStyle.Left;
        _pnlLineNumbers.Width = 48;
        _pnlLineNumbers.BackColor = Color.FromArgb(14, 17, 24);
        _pnlLineNumbers.Padding = new Padding(0, 4, 8, 4);

        _txtKodAlani.Dock = DockStyle.Fill;
        _txtKodAlani.BackColor = Color.FromArgb(11, 14, 20);
        _txtKodAlani.ForeColor = Color.FromArgb(212, 212, 212);
        _txtKodAlani.BorderStyle = BorderStyle.None;
        _txtKodAlani.Font = FontCode;
        _txtKodAlani.ScrollBars = RichTextBoxScrollBars.Vertical;
        _txtKodAlani.WordWrap = false;
        _txtKodAlani.Text =
@"public class UserService
{
    public void UpdateUserProfile()
    {
        // ...
    }
}";

        _pnlEditorBody.Controls.Add(_txtKodAlani);
        _pnlEditorBody.Controls.Add(_pnlLineNumbers);

        _pnlEditor.Controls.Add(_pnlEditorBody);
        _pnlEditor.Controls.Add(_pnlEditorFooter);
        _pnlEditor.Controls.Add(_pnlEditorHeader);

        // AI panel
        _pnlAi.Dock = DockStyle.Fill;
        _pnlAi.BackColor = CardBg; // #1A1A2E
        _pnlAi.BorderRadius = 12;
        _pnlAi.BorderSize = 1;
        _pnlAi.BorderColor = Border333;
        _pnlAi.DrawShadow = false; // requested: no shadow

        _pnlAiHeader.Dock = DockStyle.Top;
        _pnlAiHeader.Height = 58;
        _pnlAiHeader.BackColor = CardBg; // requested: no gradient, flat
        _pnlAiHeader.Padding = new Padding(14, 10, 14, 10);
        _pnlAiHeader.BorderStyle = BorderStyle.None;

        _lblAiHeaderIcon.AutoSize = true;
        _lblAiHeaderIcon.Location = new Point(14, 16);
        _lblAiHeaderIcon.Font = new Font("Segoe UI Emoji", 13F);
        _lblAiHeaderIcon.ForeColor = Color.FromArgb(230, 230, 255);
        _lblAiHeaderIcon.Text = "✨";

        _lblAiHeaderTitle.AutoSize = true;
        _lblAiHeaderTitle.Location = new Point(44, 12);
        _lblAiHeaderTitle.Font = FontTitle;
        _lblAiHeaderTitle.ForeColor = Color.FromArgb(235, 235, 245);
        _lblAiHeaderTitle.Text = "AI Önerileri";

        _lblAiHeaderSubtitle.AutoSize = true;
        _lblAiHeaderSubtitle.Location = new Point(44, 33);
        _lblAiHeaderSubtitle.Font = FontText;
        _lblAiHeaderSubtitle.ForeColor = Color.FromArgb(180, 170, 200);
        _lblAiHeaderSubtitle.Text = "Yapay zeka destekli çözüm önerileri";

        _pnlAiHeader.Controls.Add(_lblAiHeaderIcon);
        _pnlAiHeader.Controls.Add(_lblAiHeaderTitle);
        _pnlAiHeader.Controls.Add(_lblAiHeaderSubtitle);

        _pnlAiBody.Dock = DockStyle.Fill;
        _pnlAiBody.BackColor = CardBg;
        _pnlAiBody.Padding = new Padding(14);
        _pnlAiBody.BorderStyle = BorderStyle.None;

        // requested: 3 equal columns, each scrollable
        _tblAiColumns.Dock = DockStyle.Fill;
        _tblAiColumns.BackColor = CardBg;
        _tblAiColumns.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        _tblAiColumns.ColumnCount = 3;
        _tblAiColumns.RowCount = 1;
        _tblAiColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
        _tblAiColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
        _tblAiColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
        _tblAiColumns.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        ConfigureAiColumnPanel(_pnlAiColProblem, _pnlAiHdrProblem, _flpAiProblem, "SORUN", Color.FromArgb(220, 53, 69));     // #DC3545
        ConfigureAiColumnPanel(_pnlAiColDesc, _pnlAiHdrDesc, _flpAiDesc, "AÇIKLAMA", Color.FromArgb(255, 193, 7));           // #FFC107
        ConfigureAiColumnPanel(_pnlAiColSolution, _pnlAiHdrSolution, _flpAiSolution, "ÇÖZÜM", Color.FromArgb(40, 167, 69));  // #28A745

        // Kolonlar arası 8px boşluk
        _pnlAiColProblem.Margin = new Padding(0, 0, 8, 0);
        _pnlAiColDesc.Margin = new Padding(0, 0, 8, 0);
        _pnlAiColSolution.Margin = new Padding(0);

        _tblAiColumns.Controls.Add(_pnlAiColProblem, 0, 0);
        _tblAiColumns.Controls.Add(_pnlAiColDesc, 1, 0);
        _tblAiColumns.Controls.Add(_pnlAiColSolution, 2, 0);

        _pnlAiBody.Controls.Add(_tblAiColumns);

        _pnlAi.Controls.Add(_pnlAiBody);
        _pnlAi.Controls.Add(_pnlAiHeader);

        _tblCenter.Controls.Add(_pnlEditor, 0, 0);
        _tblCenter.Controls.Add(_pnlCenterDivider, 0, 1);
        _tblCenter.Controls.Add(_pnlAi, 0, 2);

        // Right panel (results)
        _pnlRight.Dock = DockStyle.Fill;
        _pnlRight.BackColor = Color.FromArgb(18, 18, 18);
        _pnlRight.Padding = new Padding(20);
        _pnlRight.BorderStyle = BorderStyle.None;
        _tblMain.Controls.Add(_pnlRight, 2, 0);

        _pnlResults.Dock = DockStyle.Fill;
        _pnlResults.BackColor = Color.FromArgb(18, 18, 18);
        _pnlResults.BorderRadius = 12;
        _pnlResults.BorderSize = 1;
        _pnlResults.BorderColor = Border333;
        _pnlResults.DrawShadow = true;

        _pnlResultsHeader.Dock = DockStyle.Top;
        _pnlResultsHeader.Height = 46;
        _pnlResultsHeader.BackColor = Color.FromArgb(11, 14, 20);
        _pnlResultsHeader.Padding = new Padding(14, 0, 14, 0);
        _pnlResultsHeader.BorderStyle = BorderStyle.None;

        _lblResultsTitle.AutoSize = true;
        _lblResultsTitle.Location = new Point(14, 13);
        _lblResultsTitle.Font = FontTitle;
        _lblResultsTitle.ForeColor = Color.White;
        _lblResultsTitle.Text = "Analiz Sonuçları";

        _lblIssuesBadge.Size = new Size(86, 24);
        _lblIssuesBadge.BackColor = Color.FromArgb(253, 126, 20);
        _lblIssuesBadge.ForeColor = Color.White;
        _lblIssuesBadge.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        _lblIssuesBadge.Text = "0 Issue";
        _lblIssuesBadge.TextAlign = ContentAlignment.MiddleCenter;
        _lblIssuesBadge.BorderRadius = 12;

        _pnlResultsHeader.Controls.Add(_lblResultsTitle);
        _pnlResultsHeader.Controls.Add(_lblIssuesBadge);

        _flpIssues.Dock = DockStyle.Fill;
        _flpIssues.AutoScroll = true;
        _flpIssues.WrapContents = false;
        _flpIssues.FlowDirection = FlowDirection.TopDown;
        _flpIssues.BackColor = Color.FromArgb(18, 18, 18);
        _flpIssues.Padding = new Padding(12);
        _flpIssues.BorderStyle = BorderStyle.None;

        _pnlResultsFooter.Dock = DockStyle.Bottom;
        _pnlResultsFooter.Height = 46;
        _pnlResultsFooter.BackColor = Color.FromArgb(11, 14, 20);
        _pnlResultsFooter.Padding = new Padding(12, 0, 12, 0);

        ConfigureStatLabel(_lblHigh, "✓ Yüksek: 0", 120);
        ConfigureStatLabel(_lblMedium, "✓ Orta: 0", 110);
        ConfigureStatLabel(_lblLow, "✓ Düşük: 0", 110);

        _pnlResultsFooter.Controls.Add(_lblLow);
        _pnlResultsFooter.Controls.Add(_lblMedium);
        _pnlResultsFooter.Controls.Add(_lblHigh);

        _pnlResults.Controls.Add(_flpIssues);
        _pnlResults.Controls.Add(_pnlResultsFooter);
        _pnlResults.Controls.Add(_pnlResultsHeader);

        _pnlRight.Controls.Add(_pnlResults);
    }

    private static void ConfigureEditorPill(Label lbl, string text)
    {
        lbl.Size = new Size(80, 22);
        lbl.BackColor = Color.FromArgb(22, 27, 38);
        lbl.ForeColor = Color.FromArgb(120, 130, 145);
        lbl.Font = new Font("Consolas", 8F);
        lbl.Text = text;
        lbl.TextAlign = ContentAlignment.MiddleCenter;
    }

    /// <summary>
    /// Sağ panel alt istatistik etiketlerini standart font ve yerleşimle ayarlar.
    /// </summary>
    private static void ConfigureStatLabel(Label lbl, string text, int width)
    {
        lbl.Dock = DockStyle.Left;
        lbl.Width = width;
        lbl.TextAlign = ContentAlignment.MiddleLeft;
        lbl.Font = FontTextBold;
        lbl.ForeColor = Color.FromArgb(200, 205, 215);
        lbl.Text = text;
    }

    /// <summary>
    /// Sol menü butonlarını ortak stil ile hazırlar (çizim Paint ile yapılır).
    /// </summary>
    private static void ConfigureSidebarButton(Button btn, Point location, string name)
    {
        btn.Name = name;
        btn.Location = location;
        btn.Size = new Size(216, 46);
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = SidebarBg;
        btn.FlatAppearance.MouseDownBackColor = SidebarBg;
        btn.ForeColor = Color.Transparent;
        btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        btn.Text = "";
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.BackColor = SidebarBg;
        btn.Cursor = Cursors.Hand;
        btn.TabStop = false;
    }

    // =========================
    // Wiring
    // =========================
    /// <summary>
    /// Event handler bağlantılarını kurar (hover/click/resize/scroll vb.).
    /// </summary>
    private void WireUi()
    {
        // requested: AI header gradient removed (flat background)

        _btnDosyaYukle.Click += btnDosyaYukle_Click;
        _btnAnalizEt.Click += btnAnalizEt_Click;

        _btnDosyaYukle.MouseEnter += (_, _) => _btnDosyaYukle.BackColor = Color.FromArgb(58, 58, 62);
        _btnDosyaYukle.MouseLeave += (_, _) => _btnDosyaYukle.BackColor = Color.FromArgb(45, 45, 48);

        _btnAnalizEt.MouseEnter += (_, _) =>
        {
            if (_btnAnalizEt.Enabled) _btnAnalizEt.BackColor = Color.FromArgb(255, 146, 40);
        };
        _btnAnalizEt.MouseLeave += (_, _) =>
        {
            if (_btnAnalizEt.Enabled) _btnAnalizEt.BackColor = Color.FromArgb(253, 126, 20);
        };

        // Sidebar
        _activeSidebarButton = _btnDashboard;
        _sidebarFadeAlpha = 255;

        foreach (var btn in new[] { _btnDashboard, _btnHistory, _btnSettings, _btnAbout })
        {
            btn.Paint += SidebarButton_Paint;
            btn.MouseEnter += SidebarButton_MouseEnter;
            btn.MouseLeave += SidebarButton_MouseLeave;
            btn.Click += SidebarButton_Click;
        }

        // Editor line numbers
        _txtKodAlani.TextChanged += TxtKodAlani_TextChanged;
        _txtKodAlani.VScroll += (_, _) => _pnlLineNumbers.Invalidate();
        _txtKodAlani.Resize += (_, _) => _pnlLineNumbers.Invalidate();
        _txtKodAlani.SelectionChanged += (_, _) => _pnlLineNumbers.Invalidate();
        _pnlLineNumbers.Paint += PnlLineNumbers_Paint;

        _pnlResultsHeader.Resize += (_, _) => PositionResultsBadge();

        _flpIssues.SizeChanged += (_, _) => ResizeIssueCardsToFill();

        _pnlAiColProblem.SizeChanged += (_, _) => ResizeAiCardsToFill();
        _pnlAiColDesc.SizeChanged += (_, _) => ResizeAiCardsToFill();
        _pnlAiColSolution.SizeChanged += (_, _) => ResizeAiCardsToFill();
    }

    private void PositionTopBarRightControls()
    {
        int right = _pnlTopHeader.ClientSize.Width - 20;
        int yButton = 10;
        int yBadge = 18;

        _lblStatusDot.Location = new Point(right - _lblStatusDot.Width, yBadge);
        right = _lblStatusDot.Left - 12;

        _btnAnalizEt.Location = new Point(right - _btnAnalizEt.Width, yButton);
        right = _btnAnalizEt.Left - 10;

        _btnDosyaYukle.Location = new Point(right - _btnDosyaYukle.Width, yButton);
    }

    private void PositionEditorBadges()
    {
        int rightMargin = 14;
        _lblEditorLangBadge.Location = new Point(_pnlEditorHeader.Width - _lblEditorLangBadge.Width - rightMargin, 12);
        _lblEditorLines.Location = new Point(_pnlEditorFooter.Width - _lblEditorLines.Width - rightMargin, 6);
        _lblEditorUtf8.Location = new Point(14, 6);
    }

    private void PositionResultsBadge()
    {
        _lblIssuesBadge.Location = new Point(_pnlResultsHeader.Width - _lblIssuesBadge.Width - 14, 11);
    }

    // =========================
    // Two views
    // =========================
    private void ApplyView(ViewMode mode)
    {
        _viewMode = mode;

        if (mode == ViewMode.PreAnalyze)
        {
            _pnlAiBody.Visible = false;
            _flpIssues.Visible = false;
            _flpIssues.Controls.Clear();
            _lblIssuesBadge.Text = "0 Issue";
            UpdateStats(0, 0, 0);

            // Clear AI columns
            ClearAiGrid();
        }
        else
        {
            _pnlAiBody.Visible = true;
            _flpIssues.Visible = true;
        }
    }

    // =========================
    // Backend actions
    // =========================
    private async void btnAnalizEt_Click(object? sender, EventArgs e)
    {
        string code = _txtKodAlani.Text;
        if (string.IsNullOrWhiteSpace(code))
        {
            MessageBox.Show("Lütfen analiz edilecek kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        ApplyView(ViewMode.PostAnalyze);
        ClearAiGrid();
        _flpIssues.Controls.Clear();

        SetAnalyzingUi(true);
        try
        {
            var result = await _analizYoneticisi.AnalizEtAsync(code);
            RenderIssues(result.Issues);
            RenderAiSuggestions(result.Suggestions.FirstOrDefault()?.SuggestionText);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Analiz hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetAnalyzingUi(false);
        }
    }

    private void btnDosyaYukle_Click(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog
        {
            Title = "C# Dosyası Seç",
            Filter = "C# Files (*.cs)|*.cs",
            FilterIndex = 1,
            CheckFileExists = true,
            Multiselect = false
        };

        if (ofd.ShowDialog() != DialogResult.OK) return;

        try
        {
            _txtKodAlani.Text = System.IO.File.ReadAllText(ofd.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Dosya okunamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SetAnalyzingUi(bool isAnalyzing)
    {
        _btnAnalizEt.Enabled = !isAnalyzing;
        _btnAnalizEt.Text = isAnalyzing ? "Analiz ediliyor..." : "▶ Analiz Et";

        if (isAnalyzing)
        {
            _lblStatusDot.BackColor = Color.FromArgb(60, 40, 10);
            _lblStatusDot.ForeColor = Color.FromArgb(253, 126, 20);
            _lblStatusDot.Text = "● Analiz...";
        }
        else
        {
            _lblStatusDot.BackColor = Color.FromArgb(30, 60, 30);
            _lblStatusDot.ForeColor = Color.FromArgb(60, 179, 113);
            _lblStatusDot.Text = "● Hazır";
        }
    }

    // =========================
    // Issues rendering (right)
    // =========================
    private void RenderIssues(IEnumerable<AnalysisIssue> issues)
    {
        int high = 0, medium = 0, low = 0;

        foreach (var issue in issues)
        {
            var sev = NormalizeSeverity(issue.Severity);
            if (sev.Level == SeverityLevel.High) high++;
            else if (sev.Level == SeverityLevel.Low) low++;
            else medium++;

            _flpIssues.Controls.Add(CreateIssueCard(issue, sev));
        }

        int total = high + medium + low;
        _lblIssuesBadge.Text = $"{total} Issue{(total == 1 ? "" : "s")}";
        UpdateStats(high, medium, low);

        ResizeIssueCardsToFill();
    }

    private void UpdateStats(int high, int medium, int low)
    {
        _lblHigh.Text = $"✓ Yüksek: {high}";
        _lblMedium.Text = $"✓ Orta: {medium}";
        _lblLow.Text = $"✓ Düşük: {low}";
    }

    private enum SeverityLevel { Low, Medium, High }
    private sealed record SeverityView(SeverityLevel Level, string LabelTr, Color Color);

    private static SeverityView NormalizeSeverity(string severity)
    {
        if (severity.Equals("High", StringComparison.OrdinalIgnoreCase) ||
            severity.Equals("Error", StringComparison.OrdinalIgnoreCase) ||
            severity.Equals("Warning", StringComparison.OrdinalIgnoreCase))
            return new SeverityView(SeverityLevel.High, "Yüksek", Color.FromArgb(220, 53, 69));

        if (severity.Equals("Low", StringComparison.OrdinalIgnoreCase) ||
            severity.Equals("Info", StringComparison.OrdinalIgnoreCase))
            return new SeverityView(SeverityLevel.Low, "Düşük", Color.FromArgb(0, 122, 204));

        return new SeverityView(SeverityLevel.Medium, "Orta", Color.FromArgb(255, 193, 7));
    }

    private static Control CreateIssueCard(AnalysisIssue issue, SeverityView sev)
    {
        const int minHeight = 70;
        const int padding = 10;
        const int badgeW = 64;
        const int badgeH = 20;
        const int gap = 8;

        var card = new RoundedPanel
        {
            Width = 300, // resized later to fill container
            BorderRadius = 12,
            BorderSize = 1,
            BorderColor = Border333,
            BackColor = CardBg,
            Margin = new Padding(0, 0, 0, 12),
            Padding = new Padding(padding),
            DrawShadow = sev.Level == SeverityLevel.High
        };

        card.Paint += (_, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var accent = new SolidBrush(sev.Color);
            e.Graphics.FillRectangle(accent, new Rectangle(0, 10, 4, Math.Max(0, card.Height - 20)));
        };

        int x = padding;
        int y = padding;

        var lblTitle = new Label
        {
            AutoSize = false,
            Location = new Point(x, y),
            Font = new Font("Segoe UI", 10.2F, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Text = string.IsNullOrWhiteSpace(issue.DiagnosticId) ? issue.Message : issue.DiagnosticId
        };

        var badge = new RoundedLabel
        {
            Size = new Size(badgeW, badgeH),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Location = new Point(card.Width - padding - badgeW, y),
            BackColor = sev.Color,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Text = sev.LabelTr,
            BorderRadius = 10
        };

        y += badgeH + gap;

        var lblMsg = new Label
        {
            AutoSize = false,
            Location = new Point(x, y),
            Font = new Font("Segoe UI", 9.2F, FontStyle.Regular),
            ForeColor = Color.FromArgb(200, 205, 215),
            BackColor = Color.Transparent,
            Text = issue.Message
        };

        y += 36 + gap;

        var lblLine = new Label
        {
            AutoSize = true,
            Location = new Point(x, y),
            Font = new Font("Cascadia Code", 8.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(140, 150, 165),
            BackColor = Color.FromArgb(22, 27, 38),
            Padding = new Padding(6, 2, 6, 2),
            Text = issue.Line > 0 ? $"Line {issue.Line}" : "Line -"
        };

        card.Controls.Add(lblTitle);
        card.Controls.Add(badge);
        card.Controls.Add(lblMsg);
        card.Controls.Add(lblLine);

        void Relayout()
        {
            int contentW = Math.Max(120, card.Width - (padding * 2));
            badge.Location = new Point(card.Width - padding - badgeW, padding);

            int titleW = Math.Max(80, contentW - badgeW - gap);
            lblTitle.Location = new Point(padding, padding);
            lblTitle.Size = new Size(titleW, 0);
            lblTitle.MaximumSize = new Size(titleW, 0);
            lblTitle.AutoSize = true;

            int msgY = Math.Max(badge.Bottom, lblTitle.Bottom) + gap;
            lblMsg.Location = new Point(padding, msgY);
            lblMsg.Size = new Size(contentW, 0);
            lblMsg.MaximumSize = new Size(contentW, 0);
            lblMsg.AutoSize = true;

            lblLine.Location = new Point(padding, lblMsg.Bottom + gap);
            card.Height = Math.Max(minHeight, lblLine.Bottom + padding);
        }

        card.SizeChanged += (_, _) => Relayout();
        Relayout();

        return card;
    }

    // =========================
    // AI grid rendering
    // =========================
    private void ClearAiGrid()
    {
        _flpAiProblem.SuspendLayout();
        _flpAiDesc.SuspendLayout();
        _flpAiSolution.SuspendLayout();

        _flpAiProblem.Controls.Clear();
        _flpAiDesc.Controls.Clear();
        _flpAiSolution.Controls.Clear();

        _flpAiProblem.ResumeLayout();
        _flpAiDesc.ResumeLayout();
        _flpAiSolution.ResumeLayout();
    }

    private void RenderAiSuggestions(string? suggestionText)
    {
        if (string.IsNullOrWhiteSpace(suggestionText))
        {
            AddAiRow("—", "AI önerisi yok.", "—");
            return;
        }

        var cards = TryParseCards(suggestionText);
        if (cards.Count == 0)
        {
            AddAiRow("AI Yanıtı", "AI yanıtı ayrıştırılamadı.", suggestionText);
            return;
        }

        foreach (var c in cards)
            AddAiRow(c.Sorun, c.Aciklama, c.Cozum);
    }

    private static List<AnalysisCardDto> TryParseCards(string raw)
    {
        try
        {
            var cleaned = raw.Replace("```json", "", StringComparison.OrdinalIgnoreCase).Replace("```", "").Trim();
            return JsonSerializer.Deserialize<List<AnalysisCardDto>>(
                       cleaned,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<AnalysisCardDto>();
        }
        catch
        {
            return new List<AnalysisCardDto>();
        }
    }

    /// <summary>
    /// AI önerilerini 3 kolona (SORUN/AÇIKLAMA/ÇÖZÜM) satır bazında ekler.
    /// </summary>
    private void AddAiRow(string problem, string description, string solution)
    {
        _flpAiProblem.Controls.Add(CreateAiCell("SORUN", problem, Color.FromArgb(220, 53, 69)));
        _flpAiDesc.Controls.Add(CreateAiCell("AÇIKLAMA", description, Color.FromArgb(255, 193, 7)));
        _flpAiSolution.Controls.Add(CreateAiCell("ÇÖZÜM", solution, Color.FromArgb(40, 167, 69)));

        ResizeAiCardsToFill();
    }

    /// <summary>
    /// Tek bir AI kartı oluşturur. Metin taşmasını önlemek için yüksekliği içeriğe göre ayarlanır.
    /// Ayrıca alt glow çizgisi ve hover animasyonu ekler.
    /// </summary>
    private RoundedPanel CreateAiCell(string header, string content, Color accentColor)
    {
        var panel = new RoundedPanel
        {
            BackColor = CardBg,
            BorderRadius = 12,
            BorderSize = 1,
            BorderColor = accentColor,
            DrawShadow = false,
            Margin = new Padding(0, 0, 0, 12),
            Padding = new Padding(14, 12, 14, 12),
            AutoSize = false
        };

        var lblHeader = new Label
        {
            AutoSize = false,
            Text = header,
            ForeColor = accentColor,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            BackColor = Color.Transparent,
            Location = new Point(0, 0),
            TextAlign = ContentAlignment.MiddleLeft,
            Height = 18
        };

        var lblContent = new Label
        {
            AutoSize = false,
            Text = content,
            ForeColor = Color.FromArgb(220, 225, 235),
            // İstek: AI kart metinleri Segoe UI 9.5 ve word-wrap.
            Font = FontText,
            BackColor = Color.Transparent,
            Location = new Point(0, 24),
            UseCompatibleTextRendering = false
        };

        panel.Controls.Add(lblHeader);
        panel.Controls.Add(lblContent);

        // Alt glow çizgisi (2px) - karta göre renkli ve yarı saydam
        panel.Paint += (_, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(180, accentColor), 2F);
            int y = panel.Height - 2;
            if (y < 0) return;
            e.Graphics.DrawLine(pen, 0, y, panel.Width, y);
        };

        // Hover büyüme (yumuşak) - padding 2px azalır, border belirginleşir
        var basePadding = panel.Padding;
        var hoverPadding = new Padding(
            Math.Max(0, basePadding.Left - 2),
            Math.Max(0, basePadding.Top - 2),
            Math.Max(0, basePadding.Right - 2),
            Math.Max(0, basePadding.Bottom - 2));

        var anim = new HoverAnimState(panel, basePadding, hoverPadding);
        panel.Tag = anim;
        panel.MouseEnter += (_, _) => anim.AnimateToHover();
        panel.MouseLeave += (_, _) => anim.AnimateToBase();
        foreach (Control c in panel.Controls)
        {
            c.MouseEnter += (_, _) => anim.AnimateToHover();
            c.MouseLeave += (_, _) => anim.AnimateToBase();
        }

        // İlk yerleşim: genişlik set edilince yükseklik otomatik hesaplanacak
        panel.SizeChanged += (_, _) => RelayoutAiCard(panel, lblHeader, lblContent);
        return panel;
    }

    /// <summary>
    /// AI kartı genişliğine göre içerik yüksekliğini hesaplar; metin kesilmesini engeller.
    /// </summary>
    private static void RelayoutAiCard(RoundedPanel panel, Label lblHeader, Label lblContent)
    {
        int innerW = Math.Max(80, panel.Width - panel.Padding.Left - panel.Padding.Right);

        lblHeader.Width = innerW;
        lblHeader.Location = new Point(panel.Padding.Left, panel.Padding.Top);

        int contentY = lblHeader.Bottom + 6;
        lblContent.Location = new Point(panel.Padding.Left, contentY);
        lblContent.Width = innerW;

        // Word-wrap ölçümü (TextRenderer ile WordBreak)
        var flags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
        var measured = TextRenderer.MeasureText(lblContent.Text ?? string.Empty, lblContent.Font, new Size(innerW, int.MaxValue), flags);
        lblContent.Height = Math.Max(18, measured.Height);

        panel.Height = lblContent.Bottom + panel.Padding.Bottom + 4;
        panel.Invalidate();
    }

    /// <summary>
    /// Hover animasyonu için küçük state nesnesi (Timer ile yumuşak geçiş).
    /// </summary>
    private sealed class HoverAnimState
    {
        private readonly RoundedPanel _panel;
        private readonly Padding _basePadding;
        private readonly Padding _hoverPadding;
        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 15 };
        private int _t;
        private bool _toHover;

        public HoverAnimState(RoundedPanel panel, Padding basePadding, Padding hoverPadding)
        {
            _panel = panel;
            _basePadding = basePadding;
            _hoverPadding = hoverPadding;
            _timer.Tick += (_, _) => Tick();
        }

        public void AnimateToHover()
        {
            _toHover = true;
            _timer.Start();
        }

        public void AnimateToBase()
        {
            _toHover = false;
            _timer.Start();
        }

        private void Tick()
        {
            // 0..8 arası adım
            _t += _toHover ? 1 : -1;
            if (_t <= 0) { _t = 0; _timer.Stop(); }
            if (_t >= 8) { _t = 8; _timer.Stop(); }

            float k = _t / 8f;
            _panel.Padding = Lerp(_basePadding, _hoverPadding, k);
            _panel.BorderSize = k > 0.5f ? 2 : 1;
            _panel.Invalidate();
        }

        private static Padding Lerp(Padding a, Padding b, float k) =>
            new(
                (int)Math.Round(a.Left + (b.Left - a.Left) * k),
                (int)Math.Round(a.Top + (b.Top - a.Top) * k),
                (int)Math.Round(a.Right + (b.Right - a.Right) * k),
                (int)Math.Round(a.Bottom + (b.Bottom - a.Bottom) * k));
    }

    /// <summary>
    /// AI kolon panelini; üstte renkli başlık + altta scroll edilebilir içerik olacak şekilde kurar.
    /// </summary>
    private static void ConfigureAiColumnPanel(Panel colPanel, Panel headerPanel, FlowLayoutPanel flp, string headerText, Color headerColor)
    {
        colPanel.Dock = DockStyle.Fill;
        colPanel.BackColor = CardBg;
        colPanel.AutoScroll = true;
        colPanel.BorderStyle = BorderStyle.None;

        headerPanel.Dock = DockStyle.Top;
        headerPanel.Height = 28;
        // Başlık arka planı: kolon rengiyle uyumlu, alpha 40
        headerPanel.BackColor = Color.FromArgb(40, headerColor);
        headerPanel.BorderStyle = BorderStyle.None;

        var headerLabel = new Label
        {
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Text = headerText,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.Transparent
        };
        headerPanel.Controls.Add(headerLabel);

        flp.Dock = DockStyle.Top;
        flp.AutoSize = true;
        flp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flp.FlowDirection = FlowDirection.TopDown;
        flp.WrapContents = false;
        flp.Padding = new Padding(0);
        flp.BackColor = CardBg;
        flp.BorderStyle = BorderStyle.None;

        colPanel.Controls.Add(flp);
        colPanel.Controls.Add(headerPanel);
    }

    private void ResizeAiCardsToFill()
    {
        void ResizeColumn(Panel col, FlowLayoutPanel flp)
        {
            int w = Math.Max(120, col.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 2);
            foreach (Control c in flp.Controls)
            {
                c.Width = w;
                if (c is RoundedPanel rp && rp.Controls.Count >= 2 &&
                    rp.Controls[0] is Label hdr && rp.Controls[1] is Label body)
                {
                    RelayoutAiCard(rp, hdr, body);
                }
            }
        }

        ResizeColumn(_pnlAiColProblem, _flpAiProblem);
        ResizeColumn(_pnlAiColDesc, _flpAiDesc);
        ResizeColumn(_pnlAiColSolution, _flpAiSolution);
    }

    private void ResizeIssueCardsToFill()
    {
        int w = Math.Max(200, _flpIssues.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 2);
        foreach (Control c in _flpIssues.Controls) c.Width = w;
    }

    // =========================
    // Sidebar paint helpers
    // =========================
    private static string SidebarIcon(Button btn) =>
        btn.Name switch
        {
            nameof(_btnDashboard) => "</>",
            nameof(_btnHistory) => "🕒",
            nameof(_btnSettings) => "⚙",
            nameof(_btnAbout) => "ℹ",
            _ => ""
        };

    private static string SidebarLabel(Button btn) =>
        btn.Name switch
        {
            nameof(_btnDashboard) => "Kod Analizi",
            nameof(_btnHistory) => "Geçmiş",
            nameof(_btnSettings) => "Ayarlar",
            nameof(_btnAbout) => "Hakkında",
            _ => ""
        };

    private void SidebarButton_MouseEnter(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        if (btn == _activeSidebarButton) return;
        _hoverSidebarButton = btn;
        btn.Invalidate();
    }

    private void SidebarButton_MouseLeave(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        if (_hoverSidebarButton == btn) _hoverSidebarButton = null;
        btn.Invalidate();
    }

    private void SidebarButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        if (btn == _activeSidebarButton) return;

        var previous = _activeSidebarButton;
        _activeSidebarButton = btn;
        _hoverSidebarButton = null;

        _sidebarFadeAlpha = 0;
        _sidebarFadeTimer.Start();

        previous?.Invalidate();
        btn.Invalidate();
    }

    private void SidebarFadeTimer_Tick(object? sender, EventArgs e)
    {
        _sidebarFadeAlpha += 25;
        if (_sidebarFadeAlpha >= 255)
        {
            _sidebarFadeAlpha = 255;
            _sidebarFadeTimer.Stop();
        }
        _activeSidebarButton?.Invalidate();
    }

    private void SidebarButton_Paint(object? sender, PaintEventArgs e)
    {
        if (sender is not Button btn) return;

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        e.Graphics.Clear(SidebarBg);

        bool isActive = btn == _activeSidebarButton;
        bool isHovered = btn == _hoverSidebarButton;

        Rectangle rect = btn.ClientRectangle;
        Rectangle innerRect = new(rect.X + 4, rect.Y + 3, rect.Width - 8, rect.Height - 6);

        if (isActive)
        {
            int alpha = _sidebarFadeAlpha;
            using (GraphicsPath path = GetRoundedPath(innerRect, 12))
            using (var brush = new LinearGradientBrush(
                       innerRect,
                       Color.FromArgb(alpha, 0, 110, 195),
                       Color.FromArgb(alpha, 0, 55, 130),
                       LinearGradientMode.Horizontal))
            {
                e.Graphics.FillPath(brush, path);
            }

            using (var accent = new SolidBrush(Color.FromArgb(alpha, 253, 126, 20)))
            {
                e.Graphics.FillRectangle(accent, new Rectangle(0, innerRect.Y + 6, 4, innerRect.Height - 12));
            }

            DrawSidebarText(e.Graphics, btn, Color.White);
            return;
        }

        if (isHovered)
        {
            using (GraphicsPath hoverPath = GetRoundedPath(innerRect, 10))
            using (var hoverBrush = new SolidBrush(HoverTint))
            {
                e.Graphics.FillPath(hoverBrush, hoverPath);
            }
            DrawSidebarText(e.Graphics, btn, Color.FromArgb(210, 215, 225));
            return;
        }

        DrawSidebarText(e.Graphics, btn, InactiveText);
    }

    private static void DrawSidebarText(Graphics g, Button btn, Color textColor)
    {
        using var iconFont = new Font("Segoe UI Emoji", 11F, FontStyle.Regular);
        using var labelFont = new Font("Segoe UI", 10F, FontStyle.Regular);

        Rectangle iconRect = new(18, 0, 28, btn.Height);
        Rectangle labelRect = new(52, 0, btn.Width - 60, btn.Height);

        TextRenderer.DrawText(g, SidebarIcon(btn), iconFont, iconRect, textColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding);
        TextRenderer.DrawText(g, SidebarLabel(btn), labelFont, labelRect, textColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding);
    }

    private static GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
    {
        int diameter = radius * 2;
        Size size = new(diameter, diameter);
        Rectangle arc = new(bounds.Location, size);
        GraphicsPath path = new();

        if (radius <= 0)
        {
            path.AddRectangle(bounds);
            return path;
        }

        path.AddArc(arc, 180, 90);
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }

    // =========================
    // Line numbers
    // =========================
    [DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

    private const int EM_GETFIRSTVISIBLELINE = 0x00CE;

    private void PnlLineNumbers_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        e.Graphics.Clear(Color.FromArgb(14, 17, 24));

        int firstVisibleLine = SendMessage(_txtKodAlani.Handle, EM_GETFIRSTVISIBLELINE, 0, 0);
        int totalLines = Math.Max(1, _txtKodAlani.Lines.Length);

        float lineHeight = _txtKodAlani.Font.GetHeight(e.Graphics);
        int visibleLines = (int)(_pnlLineNumbers.Height / lineHeight) + 2;

        using var gutterFont = new Font("Cascadia Code", 9.5F, FontStyle.Regular);
        for (int i = 0; i < visibleLines; i++)
        {
            int lineNum = firstVisibleLine + i + 1;
            if (lineNum > totalLines) break;

            float y = i * lineHeight + 1;
            TextRenderer.DrawText(
                e.Graphics,
                lineNum.ToString(),
                gutterFont,
                new Rectangle(0, (int)y, _pnlLineNumbers.Width - 10, (int)lineHeight),
                Color.FromArgb(70, 78, 95),
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }

        using var sepPen = new Pen(Color.FromArgb(30, 35, 48), 1);
        e.Graphics.DrawLine(sepPen, _pnlLineNumbers.Width - 1, 0, _pnlLineNumbers.Width - 1, _pnlLineNumbers.Height);
    }

    private void TxtKodAlani_TextChanged(object? sender, EventArgs e)
    {
        int lineCount = Math.Max(1, _txtKodAlani.Lines.Length);
        _lblEditorLines.Text = $"Lines: {lineCount}";
        _pnlLineNumbers.Invalidate();
    }

    // =========================
    // Scrollbar theming (best-effort)
    // =========================
    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hWnd, string? pszSubAppName, string? pszSubIdList);

    private static void ApplyDarkScrollbars(Control root)
    {
        void Apply(Control c)
        {
            void OnHandleCreated(object? _, EventArgs __) => _ = SetWindowTheme(c.Handle, "DarkMode_Explorer", null);

            if (c.IsHandleCreated) _ = SetWindowTheme(c.Handle, "DarkMode_Explorer", null);
            else c.HandleCreated += OnHandleCreated;

            foreach (Control child in c.Controls) Apply(child);
        }

        Apply(root);
    }
}

