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
    private readonly ComboBox _cmbAiProvider = new();

    // ===== UI: sidebar =====
    private readonly Panel _pnlSidebar = new();
    private readonly Button _btnDashboard = new();
    private readonly Button _btnHistory = new();
    private readonly Button _btnSettings = new();
    private readonly Button _btnAbout = new();
    private readonly Label _lblVersion = new();

    // ===== UI: sidebar bottom theme switch =====
    private readonly Panel _pnlSidebarBottom = new();
    private readonly Label _lblSidebarTheme = new();
    private readonly RoundedButton _btnSidebarThemeToggle = new();

    // ===== UI: sidebar bottom user profile & logout =====
    private readonly Panel _pnlUserProfile = new();
    private readonly RoundedPanel _pnlUserCard = new();
    private readonly RoundedPanel _pnlUserAvatar = new();
    private readonly Label _lblUserName = new();
    private readonly Label _lblUserRole = new();
    private readonly Label _btnLogout = new();

    public bool IsLoggingOut { get; private set; } = false;

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

    // ===== UI: Dinamik Ek Sayfalar (History, Settings, About) =====
    private readonly Panel _pnlHistory = new();
    private readonly Panel _pnlSettings = new();
    private readonly Panel _pnlAbout = new();

    // ===== UI: Tema Değiştirme ve Gelişmiş Ayar Bileşenleri =====
    private bool _isDarkMode = true;
    private readonly RoundedPanel _pnlDarkCard = new();
    private readonly RoundedPanel _pnlLightCard = new();
    private readonly Label _lblDarkCheck = new();
    private readonly Label _lblLightCheck = new();

    private readonly ComboBox _cmbSettingsAiModel = new();
    private readonly CheckBox _chkSqlInjection = new();
    private readonly CheckBox _chkHardcodedSecrets = new();
    private readonly CheckBox _chkAutoStart = new();
    private readonly Label _lblGeminiApiStatus = new();
    private readonly Label _lblGroqApiStatus = new();
    private readonly RoundedButton _btnEditApiKeys = new();


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
        _analizYoneticisi = analizYoneticisi;

        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        this.Size = new Size(1280, 800);
        this.StartPosition = FormStartPosition.CenterScreen;


        _sidebarFadeTimer = new System.Windows.Forms.Timer { Interval = 16 };
        _sidebarFadeTimer.Tick += SidebarFadeTimer_Tick;

        // UI tamamen programatik kurulur.
        BuildUi();
        WireUi();

        ApplyView(ViewMode.PreAnalyze);
        ApplyTheme();

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

        _cmbAiProvider.Size = new Size(130, 40);
        _cmbAiProvider.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbAiProvider.BackColor = Color.FromArgb(45, 45, 48);
        _cmbAiProvider.ForeColor = Color.White;
        _cmbAiProvider.Font = FontTextBold;
        _cmbAiProvider.FlatStyle = FlatStyle.Flat;
        _cmbAiProvider.Items.AddRange(new object[] { "Groq", "Gemini", "OpenRouter" });
        _cmbAiProvider.SelectedIndex = 0; // Varsayılan Groq
        _cmbAiProvider.Cursor = Cursors.Hand;

        _pnlTopHeader.Controls.Add(_lblLogoSquare);
        _pnlTopHeader.Controls.Add(_lblLogo);
        _pnlTopHeader.Controls.Add(_lblSubtitle);
        _pnlTopHeader.Controls.Add(_cmbAiProvider);
        _pnlTopHeader.Controls.Add(_btnDosyaYukle);
        _pnlTopHeader.Controls.Add(_btnAnalizEt);
        _pnlTopHeader.Controls.Add(_lblStatusDot);
        // Ana Layout (Sidebar solda sabit, sağ kısım tek satırlı yapı)
        _tblMain.Dock = DockStyle.Fill;
        _tblMain.BackColor = Color.FromArgb(18, 18, 18);
        _tblMain.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        _tblMain.ColumnCount = 3;
        _tblMain.RowCount = 1;
        _tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F)); // Kolon 0: Sidebar
        _tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Kolon 1: Editör / Merkez
        _tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F)); // Kolon 2: Sonuçlar / Sağ Panel
        _tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));        // Satır 0: Editör ve Sonuçlar
        _tblRoot.Controls.Add(_tblMain, 0, 1);


        // Sidebar — Minimalist tasarım
        _pnlSidebar.Dock = DockStyle.Fill;
        _pnlSidebar.BackColor = SidebarBg;
        _pnlSidebar.Padding = new Padding(0, 16, 0, 0);
        _pnlSidebar.BorderStyle = BorderStyle.None;
        _tblMain.Controls.Add(_pnlSidebar, 0, 0);

        // Navigasyon butonları (sabit koordinatlarla temiz dikey sıralama)
        ConfigureSidebarButton(_btnDashboard, new Point(12, 16), nameof(_btnDashboard));
        ConfigureSidebarButton(_btnHistory, new Point(12, 72), nameof(_btnHistory));
        ConfigureSidebarButton(_btnSettings, new Point(12, 128), nameof(_btnSettings));
        ConfigureSidebarButton(_btnAbout, new Point(12, 184), nameof(_btnAbout));

        // Versiyon Etiketi (Dock.Bottom - en altta)
        _lblVersion.Dock = DockStyle.Bottom;
        _lblVersion.Height = 45; // Alt boşluk bırakmak için yükseklik artırıldı
        _lblVersion.TextAlign = ContentAlignment.TopCenter; // Üste yasla
        _lblVersion.Padding = new Padding(0, 5, 0, 0); // Üstten 5px boşluk
        _lblVersion.ForeColor = Color.FromArgb(60, 65, 75);
        _lblVersion.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
        _lblVersion.Text = "Version 1.0.0";
        _lblVersion.BackColor = SidebarBg;

        // Tema Toggle Paneli (Dock.Bottom - versiyon'un hemen üstünde)
        _pnlSidebarBottom.Dock = DockStyle.Bottom;
        _pnlSidebarBottom.Height = 60; // 50'den 60'a çıkarılarak ferahlatıldı
        _pnlSidebarBottom.BackColor = Color.Transparent;

        _lblSidebarTheme.Text = "Koyu Tema";
        _lblSidebarTheme.Location = new Point(20, 20); // 16'dan 20'ye kaydırıldı
        _lblSidebarTheme.AutoSize = true;
        _lblSidebarTheme.Font = FontTextBold;
        _lblSidebarTheme.ForeColor = InactiveText;
        _lblSidebarTheme.BackColor = Color.Transparent;

        _btnSidebarThemeToggle.Size = new Size(54, 28);
        _btnSidebarThemeToggle.Location = new Point(166, 16); // Sağdan 20px padding
        _btnSidebarThemeToggle.BorderRadius = 14;
        _btnSidebarThemeToggle.BorderSize = 0;
        _btnSidebarThemeToggle.Cursor = Cursors.Hand;
        _btnSidebarThemeToggle.Click += btnThemeToggle_Click;
        _btnSidebarThemeToggle.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Color bg = _isDarkMode ? Color.FromArgb(0, 122, 204) : Color.FromArgb(200, 200, 205);
            using var bgBrush = new SolidBrush(bg);
            e.Graphics.FillPath(bgBrush, GetRoundedPath(_btnSidebarThemeToggle.ClientRectangle, 14));
            int circleSize = 20;
            int y = (28 - circleSize) / 2;
            int x = _isDarkMode ? (54 - circleSize - 4) : 4;
            using var circleBrush = new SolidBrush(Color.White);
            e.Graphics.FillEllipse(circleBrush, new Rectangle(x, y, circleSize, circleSize));
        };

        _pnlSidebarBottom.Controls.Add(_lblSidebarTheme);
        _pnlSidebarBottom.Controls.Add(_btnSidebarThemeToggle);

        // Kullanıcı Giriş / Profil Kartı (Dock.Bottom - Tema panelinin hemen üstünde)
        _pnlUserProfile.Dock = DockStyle.Bottom;
        _pnlUserProfile.Height = 70; // 85'ten 70'e düşürüldü (daha kompakt)
        _pnlUserProfile.BackColor = Color.Transparent;

        _pnlUserCard.Size = new Size(216, 52); // Sidebar 240px. 216 genişlik = sağdan ve soldan 12px margin
        _pnlUserCard.Location = new Point(12, 8); // Sola kaydırıldı (X=12), navigasyon butonları ile tam hizalı
        _pnlUserCard.Anchor = AnchorStyles.Top | AnchorStyles.Left; // AnchorRight kaldırıldı (esneme yapıp kesilmesini engeller)
        _pnlUserCard.BorderRadius = 12;
        _pnlUserCard.BorderSize = 1;
        _pnlUserCard.DrawShadow = false;

        _pnlUserAvatar.Size = new Size(32, 32); // 40x40'tan 32x32'ye küçültüldü
        _pnlUserAvatar.Location = new Point(10, 10); // Dikey ortalama ((52-32)/2 = 10)
        _pnlUserAvatar.BorderRadius = 16; // Daire şeklinde avatar
        _pnlUserAvatar.BorderSize = 0;
        _pnlUserAvatar.BackColor = Color.FromArgb(0, 122, 204); // DeepCode Mavi
        _pnlUserAvatar.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using var font = new Font("Segoe UI", 9.5F, FontStyle.Bold); // Yazı boyutu hafif küçüldü
            TextRenderer.DrawText(e.Graphics, "AD", font, _pnlUserAvatar.ClientRectangle, Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        };

        _lblUserName.AutoSize = true;
        _lblUserName.Location = new Point(54, 9); // X=54 (Avatar bittikten sonra 12px boşluk)
        _lblUserName.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); // 9.5F -> 9F
        _lblUserName.BackColor = Color.Transparent;
        _lblUserName.Text = "Admin";

        _lblUserRole.AutoSize = true;
        _lblUserRole.Location = new Point(54, 26); // Alt role yazısı
        _lblUserRole.Font = new Font("Segoe UI", 7.5F, FontStyle.Regular); // 8F -> 7.5F
        _lblUserRole.BackColor = Color.Transparent;
        _lblUserRole.Text = "Geliştirici";

        _btnLogout.Size = new Size(26, 26); // Hafif küçültüldü
        _btnLogout.Location = new Point(168, 13); // Kesinlikle tam görünmesi için sola (X=168) çekildi
        _btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Left; // AnchorRight kaldırıldı (esneyip kaybolmasını engeller)
        _btnLogout.Font = new Font("Segoe UI Semibold", 11.5F, FontStyle.Bold);
        _btnLogout.ForeColor = Color.FromArgb(239, 68, 68); // Kırmızı Çıkış Ikonu
        _btnLogout.BackColor = Color.Transparent;
        _btnLogout.Text = "⏻"; // Elegant kapatma simgesi
        _btnLogout.TextAlign = ContentAlignment.MiddleCenter;
        _btnLogout.Cursor = Cursors.Hand;

        _btnLogout.MouseEnter += (s, e) => _btnLogout.ForeColor = Color.FromArgb(248, 113, 113); // hover parlak kırmızı
        _btnLogout.MouseLeave += (s, e) => _btnLogout.ForeColor = Color.FromArgb(239, 68, 68);
        _btnLogout.Click += BtnLogout_Click;

        _pnlUserCard.Controls.Add(_pnlUserAvatar);
        _pnlUserCard.Controls.Add(_lblUserName);
        _pnlUserCard.Controls.Add(_lblUserRole);
        _pnlUserCard.Controls.Add(_btnLogout);
        _pnlUserProfile.Controls.Add(_pnlUserCard);

        // Sidebar bileşen ekleme sırası (Dock kurallarına uygun)
        _pnlSidebar.Controls.Add(_btnDashboard);
        _pnlSidebar.Controls.Add(_btnHistory);
        _pnlSidebar.Controls.Add(_btnSettings);
        _pnlSidebar.Controls.Add(_btnAbout);
        _pnlSidebar.Controls.Add(_pnlUserProfile);
        _pnlSidebar.Controls.Add(_pnlSidebarBottom);
        _pnlSidebar.Controls.Add(_lblVersion);

        // Center layout (Kod Editörü ve AI Önerileri)
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
        _tblMain.Controls.Add(_tblCenter, 1, 0); // Row 0 of _tblMain (Üst)
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
        _lblAiHeaderIcon.Location = new Point(14, 15);
        _lblAiHeaderIcon.Font = new Font("Segoe UI Emoji", 13F);
        _lblAiHeaderIcon.ForeColor = Color.FromArgb(230, 230, 255);
        _lblAiHeaderIcon.Text = "✨";

        _lblAiHeaderTitle.AutoSize = true;
        _lblAiHeaderTitle.Location = new Point(52, 12);
        _lblAiHeaderTitle.Font = FontTitle;
        _lblAiHeaderTitle.ForeColor = Color.FromArgb(235, 235, 245);
        _lblAiHeaderTitle.Text = "AI Önerileri";

        _lblAiHeaderSubtitle.AutoSize = true;
        _lblAiHeaderSubtitle.Location = new Point(52, 33);
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
        // Sağ panel (Analiz Sonuçları)
        _pnlRight.Dock = DockStyle.Fill;
        _pnlRight.BackColor = Color.FromArgb(18, 18, 18);
        _pnlRight.Padding = new Padding(20);
        _pnlRight.BorderStyle = BorderStyle.None;
        _tblMain.Controls.Add(_pnlRight, 2, 0);

        // Analiz Sonuçları kartını sağ panele direkt yerleştir

        _pnlResults.Dock = DockStyle.Fill;
        _pnlRight.Controls.Add(_pnlResults);
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

        // Dinamik Ek Sayfalar (History, Settings, About) Oluşturulur ve Main Layout'a Eklenir
        BuildHistoryPanel();
        BuildSettingsPanel();
        BuildAboutPanel();

        _tblMain.Controls.Add(_pnlHistory, 1, 0);
        _tblMain.SetColumnSpan(_pnlHistory, 2);
        _tblMain.SetRowSpan(_pnlHistory, 2);
        _pnlHistory.Dock = DockStyle.Fill;
        _pnlHistory.Visible = false;

        _tblMain.Controls.Add(_pnlSettings, 1, 0);
        _tblMain.SetColumnSpan(_pnlSettings, 2);
        _tblMain.SetRowSpan(_pnlSettings, 2);
        _pnlSettings.Dock = DockStyle.Fill;
        _pnlSettings.Visible = false;

        _tblMain.Controls.Add(_pnlAbout, 1, 0);
        _tblMain.SetColumnSpan(_pnlAbout, 2);
        _tblMain.SetRowSpan(_pnlAbout, 2);
        _pnlAbout.Dock = DockStyle.Fill;
        _pnlAbout.Visible = false;

    }



    /// <summary>
    /// Geçmiş (History) sekmesi içeriğini dinamik kartlarla oluşturur.
    /// </summary>
    private void BuildHistoryPanel()
    {
        _pnlHistory.Padding = new Padding(24);
        _pnlHistory.BackColor = Color.FromArgb(9, 9, 11);

        var lblHeader = new Label
        {
            Text = "Analiz Geçmişi",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            ForeColor = Color.FromArgb(244, 244, 245),
            Location = new Point(24, 24),
            AutoSize = true
        };

        var lblSub = new Label
        {
            Text = "Daha önce gerçekleştirilmiş kod taramaları ve güvenlik analizleri.",
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(161, 161, 170),
            Location = new Point(24, 60),
            AutoSize = true
        };

        var flpItems = new FlowLayoutPanel
        {
            Location = new Point(24, 100),
            Size = new Size(700, 480),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            BackColor = Color.Transparent
        };

        // 3 Tane Premium Analiz Geçmişi Kartı Eklenir
        flpItems.Controls.Add(CreateHistoryCard("DbConnection.cs", "17.05.2026 10:14", "SqlInjectionAnalyzer", "RİSKLİ (Kritik)", "SqlCommand parametreleri inline string birleştirme ile oluşturulmuş.", true));
        flpItems.Controls.Add(CreateHistoryCard("GeminiService.cs", "16.05.2026 15:42", "HardcodedSecretAnalyzer", "RİSKLİ (Kritik)", "API Key değişkeninde hardcoded gizli anahtar bulundu.", true));
        flpItems.Controls.Add(CreateHistoryCard("MathUtils.cs", "15.05.2026 09:20", "MagicNumberAnalyzer", "GÜVENLİ", "Kod analizi tamamlandı. Herhangi bir güvenlik açığı tespit edilmedi.", false));

        _pnlHistory.Controls.Add(lblHeader);
        _pnlHistory.Controls.Add(lblSub);
        _pnlHistory.Controls.Add(flpItems);
    }

    /// <summary>
    /// Geçmiş analizler için premium bir liste elemanı/kartı üretir.
    /// </summary>
    private Control CreateHistoryCard(string fileName, string date, string analyzer, string status, string message, bool isRisk)
    {
        var card = new RoundedPanel
        {
            Width = 650,
            Height = 100,
            BorderRadius = 12,
            BorderSize = 1,
            BorderColor = isRisk ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 167, 69),
            BackColor = Color.FromArgb(24, 24, 27),
            Margin = new Padding(0, 0, 0, 16),
            Padding = new Padding(16, 12, 16, 12),
            DrawShadow = false
        };

        card.Paint += (_, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var accent = new SolidBrush(isRisk ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 167, 69));
            e.Graphics.FillRectangle(accent, new Rectangle(0, 10, 4, card.Height - 20));
        };

        var lblName = new Label
        {
            Text = fileName,
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(16, 12),
            AutoSize = true,
            BackColor = Color.Transparent
        };

        var lblDate = new Label
        {
            Text = date,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(113, 113, 122),
            Location = new Point(530, 14),
            AutoSize = true,
            BackColor = Color.Transparent
        };

        var lblStatus = new RoundedLabel
        {
            Text = status,
            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
            BackColor = isRisk ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 167, 69),
            ForeColor = Color.White,
            Size = new Size(95, 20),
            Location = new Point(16, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            BorderRadius = 8
        };

        var lblAnalyzer = new Label
        {
            Text = $"Tarayıcı: {analyzer}",
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(161, 161, 170),
            Location = new Point(120, 42),
            AutoSize = true,
            BackColor = Color.Transparent
        };

        var lblMsg = new Label
        {
            Text = message,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(200, 205, 215),
            Location = new Point(16, 68),
            AutoSize = true,
            BackColor = Color.Transparent
        };

        card.Controls.Add(lblName);
        card.Controls.Add(lblDate);
        card.Controls.Add(lblStatus);
        card.Controls.Add(lblAnalyzer);
        card.Controls.Add(lblMsg);

        return card;
    }

    /// <summary>
    /// Ayarlar (Settings) sekmesini pürüzsüz çalışan tema değiştiricisiyle kurar.
    /// </summary>
    private void BuildSettingsPanel()
    {
        _pnlSettings.Padding = new Padding(24);
        _pnlSettings.BackColor = Color.FromArgb(9, 9, 11);
        _pnlSettings.AutoScroll = true; // İçerik taşarsa premium kaydırma çubuğu aktifleşir

        var lblHeader = new Label
        {
            Text = "Ayarlar",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            ForeColor = Color.FromArgb(244, 244, 245),
            Location = new Point(24, 24),
            AutoSize = true
        };

        var lblSub = new Label
        {
            Text = "Platform tercihlerini ve arayüz temalarını yapılandırın.",
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(161, 161, 170),
            Location = new Point(24, 60),
            AutoSize = true
        };

        // Kart 1: Arayüz Teması (Koyu Tema & Açık Tema Seçim Kartları)
        var pnlThemeCard = new RoundedPanel
        {
            Location = new Point(24, 100),
            Size = new Size(600, 170), // Yükseklik iki şık kartı barındıracak şekilde 170px'e çıkarıldı
            BorderRadius = 12,
            BorderSize = 1,
            BorderColor = Color.FromArgb(45, 45, 48),
            BackColor = Color.FromArgb(24, 24, 27),
            Padding = new Padding(20)
        };

        var lblThemeTitle = new Label
        {
            Text = "Arayüz Teması",
            Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(20, 18),
            AutoSize = true,
            BackColor = Color.Transparent
        };

        // Koyu Tema Kartı (Tıklanabilir Seçim Kutusu)
        _pnlDarkCard.Location = new Point(20, 52);
        _pnlDarkCard.Size = new Size(268, 102);
        _pnlDarkCard.BorderRadius = 12;
        _pnlDarkCard.Cursor = Cursors.Hand;
        _pnlDarkCard.BackColor = Color.FromArgb(18, 18, 20);

        var lblDarkTitle = new Label
        {
            Text = "Koyu Tema",
            Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(12, 12),
            Size = new Size(180, 20),
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand
        };

        _lblDarkCheck.Text = "✓";
        _lblDarkCheck.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblDarkCheck.ForeColor = Color.FromArgb(74, 222, 128);
        _lblDarkCheck.Location = new Point(236, 10);
        _lblDarkCheck.Size = new Size(24, 24);
        _lblDarkCheck.BackColor = Color.Transparent;
        _lblDarkCheck.Cursor = Cursors.Hand;

        var lblDarkDesc = new Label
        {
            Text = "Zinc-950 tabanlı derin karanlık kod analiz arayüzü.",
            Font = new Font("Segoe UI", 9F, FontStyle.Regular),
            ForeColor = Color.FromArgb(161, 161, 170),
            Location = new Point(12, 38),
            Size = new Size(244, 50),
            AutoSize = false,
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand
        };

        _pnlDarkCard.Controls.Add(lblDarkTitle);
        _pnlDarkCard.Controls.Add(_lblDarkCheck);
        _pnlDarkCard.Controls.Add(lblDarkDesc);

        // Açık Tema Kartı (Tıklanabilir Seçim Kutusu)
        _pnlLightCard.Location = new Point(312, 52);
        _pnlLightCard.Size = new Size(268, 102);
        _pnlLightCard.BorderRadius = 12;
        _pnlLightCard.Cursor = Cursors.Hand;
        _pnlLightCard.BackColor = Color.FromArgb(244, 244, 245);

        var lblLightTitle = new Label
        {
            Text = "Açık Tema",
            Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
            ForeColor = Color.FromArgb(24, 24, 27),
            Location = new Point(12, 12),
            Size = new Size(180, 20),
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand
        };

        _lblLightCheck.Text = "";
        _lblLightCheck.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblLightCheck.ForeColor = Color.FromArgb(0, 122, 204);
        _lblLightCheck.Location = new Point(236, 10);
        _lblLightCheck.Size = new Size(24, 24);
        _lblLightCheck.BackColor = Color.Transparent;
        _lblLightCheck.Cursor = Cursors.Hand;

        var lblLightDesc = new Label
        {
            Text = "Zinc-100 tabanlı gözü yormayan yüksek kontrastlı aydınlık tema.",
            Font = new Font("Segoe UI", 9F, FontStyle.Regular),
            ForeColor = Color.FromArgb(113, 113, 122),
            Location = new Point(12, 38),
            Size = new Size(244, 50),
            AutoSize = false,
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand
        };

        _pnlLightCard.Controls.Add(lblLightTitle);
        _pnlLightCard.Controls.Add(_lblLightCheck);
        _pnlLightCard.Controls.Add(lblLightDesc);

        // Tıklama event'lerinin alt kontrollere ve panellere bağlanması
        EventHandler setDarkTheme = (s, e) => { _isDarkMode = true; ApplyTheme(); };
        EventHandler setLightTheme = (s, e) => { _isDarkMode = false; ApplyTheme(); };

        _pnlDarkCard.Click += setDarkTheme;
        lblDarkTitle.Click += setDarkTheme;
        _lblDarkCheck.Click += setDarkTheme;
        lblDarkDesc.Click += setDarkTheme;

        _pnlLightCard.Click += setLightTheme;
        lblLightTitle.Click += setLightTheme;
        _lblLightCheck.Click += setLightTheme;
        lblLightDesc.Click += setLightTheme;

        pnlThemeCard.Controls.Add(lblThemeTitle);
        pnlThemeCard.Controls.Add(_pnlDarkCard);
        pnlThemeCard.Controls.Add(_pnlLightCard);

        // Kart 2: Yapay Zeka Altyapı Ayarları (AI Model Configuration)
        var pnlAiConfigCard = new RoundedPanel
        {
            Location = new Point(24, 286), // Spacing adjusted
            Size = new Size(600, 140),
            BorderRadius = 12,
            BorderSize = 1,
            BorderColor = Color.FromArgb(45, 45, 48),
            BackColor = Color.FromArgb(24, 24, 27),
            Padding = new Padding(20)
        };

        var lblAiTitle = new Label
        {
            Text = "Varsayılan Yapay Zeka Modeli",
            Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(20, 20),
            AutoSize = true,
            BackColor = Color.Transparent
        };

        var lblAiDesc = new Label
        {
            Text = "Kod analizinde kullanılacak ön tanımlı motoru seçin.",
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(161, 161, 170),
            Location = new Point(20, 48),
            Size = new Size(560, 40),
            BackColor = Color.Transparent
        };

        _cmbSettingsAiModel.Size = new Size(300, 36);
        _cmbSettingsAiModel.Location = new Point(20, 88);
        _cmbSettingsAiModel.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSettingsAiModel.BackColor = Color.FromArgb(45, 45, 48);
        _cmbSettingsAiModel.ForeColor = Color.White;
        _cmbSettingsAiModel.Font = FontTextBold;
        _cmbSettingsAiModel.FlatStyle = FlatStyle.Flat;
        _cmbSettingsAiModel.Items.AddRange(new object[] { 
            "Gemini 1.5 Pro (Derin Analiz)", 
            "Groq Llama 3 (Süper Hızlı)", 
            "OpenRouter (Alternatif)" 
        });
        _cmbSettingsAiModel.SelectedIndex = 1; // Varsayılan Groq Llama 3
        _cmbSettingsAiModel.Cursor = Cursors.Hand;

        pnlAiConfigCard.Controls.Add(lblAiTitle);
        pnlAiConfigCard.Controls.Add(lblAiDesc);
        pnlAiConfigCard.Controls.Add(_cmbSettingsAiModel);

        // Kart 3: Tarama ve Analiz Tercihleri (Analyzer Settings)
        var pnlAnalyzerConfigCard = new RoundedPanel
        {
            Location = new Point(24, 442), // Spacing adjusted
            Size = new Size(600, 140),
            BorderRadius = 12,
            BorderSize = 1,
            BorderColor = Color.FromArgb(45, 45, 48),
            BackColor = Color.FromArgb(24, 24, 27),
            Padding = new Padding(20)
        };

        var lblAnalyzerTitle = new Label
        {
            Text = "Güvenlik Analizörleri",
            Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(20, 20),
            AutoSize = true,
            BackColor = Color.Transparent
        };

        var lblAnalyzerDesc = new Label
        {
            Text = "Hangi güvenlik açıklarının taranacağını özelleştirin.",
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(161, 161, 170),
            Location = new Point(20, 48),
            Size = new Size(560, 30),
            BackColor = Color.Transparent
        };

        ConfigureSettingsCheckbox(_chkSqlInjection, "SQL Injection Taraması (Roslyn)", new Point(20, 78), 240);
        ConfigureSettingsCheckbox(_chkHardcodedSecrets, "Hardcoded Secret / Şifre Taraması (Roslyn)", new Point(270, 78), 300);
        ConfigureSettingsCheckbox(_chkAutoStart, "Dosya yüklenir yüklenmez otomatik analizi başlat.", new Point(20, 106), 450);

        _chkSqlInjection.Checked = true;
        _chkHardcodedSecrets.Checked = true;
        _chkAutoStart.Checked = false;

        pnlAnalyzerConfigCard.Controls.Add(lblAnalyzerTitle);
        pnlAnalyzerConfigCard.Controls.Add(lblAnalyzerDesc);
        pnlAnalyzerConfigCard.Controls.Add(_chkSqlInjection);
        pnlAnalyzerConfigCard.Controls.Add(_chkHardcodedSecrets);
        pnlAnalyzerConfigCard.Controls.Add(_chkAutoStart);

        // Kart 4: Geliştirici ve API Anahtarları (API Key Management)
        var pnlApiKeyCard = new RoundedPanel
        {
            Location = new Point(24, 598), // Spacing adjusted
            Size = new Size(600, 150),
            BorderRadius = 12,
            BorderSize = 1,
            BorderColor = Color.FromArgb(45, 45, 48),
            BackColor = Color.FromArgb(24, 24, 27),
            Padding = new Padding(20)
        };

        var lblApiKeyTitle = new Label
        {
            Text = "API Anahtarları Kontrolü",
            Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(20, 20),
            AutoSize = true,
            BackColor = Color.Transparent
        };

        var lblApiKeyDesc = new Label
        {
            Text = "Yapay zeka servisleri için yerel API key durumları.",
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(161, 161, 170),
            Location = new Point(20, 48),
            Size = new Size(560, 30),
            BackColor = Color.Transparent
        };

        _lblGeminiApiStatus.Text = "Gemini API: ● Aktif / Bağlı";
        _lblGeminiApiStatus.Location = new Point(20, 80);
        _lblGeminiApiStatus.Size = new Size(200, 24);
        _lblGeminiApiStatus.Font = FontTextBold;
        _lblGeminiApiStatus.ForeColor = Color.FromArgb(74, 222, 128); // Premium yeşil (#4ade80)
        _lblGeminiApiStatus.BackColor = Color.Transparent;

        _lblGroqApiStatus.Text = "Groq API: ● Aktif / Bağlı";
        _lblGroqApiStatus.Location = new Point(240, 80);
        _lblGroqApiStatus.Size = new Size(200, 24);
        _lblGroqApiStatus.Font = FontTextBold;
        _lblGroqApiStatus.ForeColor = Color.FromArgb(74, 222, 128);
        _lblGroqApiStatus.BackColor = Color.Transparent;

        _btnEditApiKeys.Text = "API Anahtarlarını Düzenle (.json)";
        _btnEditApiKeys.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _btnEditApiKeys.BackColor = Color.FromArgb(39, 39, 42); // Zinc-800
        _btnEditApiKeys.ForeColor = Color.FromArgb(161, 161, 170); // Zinc-400
        _btnEditApiKeys.Size = new Size(240, 30);
        _btnEditApiKeys.Location = new Point(20, 110);
        _btnEditApiKeys.BorderRadius = 6;
        _btnEditApiKeys.BorderSize = 1;
        _btnEditApiKeys.BorderColor = Color.FromArgb(63, 63, 70); // Zinc-700
        _btnEditApiKeys.Cursor = Cursors.Hand;

        _btnEditApiKeys.MouseEnter += (_, _) => {
            _btnEditApiKeys.BackColor = Color.FromArgb(45, 45, 48);
            _btnEditApiKeys.ForeColor = Color.White;
        };
        _btnEditApiKeys.MouseLeave += (_, _) => {
            _btnEditApiKeys.BackColor = Color.FromArgb(39, 39, 42);
            _btnEditApiKeys.ForeColor = Color.FromArgb(161, 161, 170);
        };

        pnlApiKeyCard.Controls.Add(lblApiKeyTitle);
        pnlApiKeyCard.Controls.Add(lblApiKeyDesc);
        pnlApiKeyCard.Controls.Add(_lblGeminiApiStatus);
        pnlApiKeyCard.Controls.Add(_lblGroqApiStatus);
        pnlApiKeyCard.Controls.Add(_btnEditApiKeys);

        // Panelleri Settings sayfasına ekle
        _pnlSettings.Controls.Add(lblHeader);
        _pnlSettings.Controls.Add(lblSub);
        _pnlSettings.Controls.Add(pnlThemeCard);
        _pnlSettings.Controls.Add(pnlAiConfigCard);
        _pnlSettings.Controls.Add(pnlAnalyzerConfigCard);
        _pnlSettings.Controls.Add(pnlApiKeyCard);
    }

    private static void ConfigureSettingsCheckbox(CheckBox chk, string text, Point location, int width)
    {
        chk.Text = text;
        chk.Location = location;
        chk.Size = new Size(width, 24);
        chk.FlatStyle = FlatStyle.Flat;
        chk.Font = FontText;
        chk.ForeColor = Color.FromArgb(244, 244, 245);
        chk.BackColor = Color.Transparent;
        chk.Cursor = Cursors.Hand;
    }

    private void btnThemeToggle_Click(object? sender, EventArgs e)
    {
        _isDarkMode = !_isDarkMode;
        ApplyTheme();
    }

    private void BtnLogout_Click(object? sender, EventArgs e)
    {
        var result = MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "Oturumu Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            IsLoggingOut = true;
            this.Close();

            var loginForm = System.Windows.Forms.Application.OpenForms["LoginForm"] as LoginForm;
            if (loginForm != null)
            {
                loginForm.ResetFormForLogout();
            }
        }
    }

    /// <summary>
    /// Hakkında (About) sekmesini ekibin vizyonuyla minimalist tasarlar.
    /// </summary>
    private void BuildAboutPanel()
    {
        _pnlAbout.Padding = new Padding(24);
        _pnlAbout.BackColor = Color.FromArgb(9, 9, 11);

        var lblHeader = new Label
        {
            Text = "Hakkında",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            ForeColor = Color.FromArgb(244, 244, 245),
            Location = new Point(24, 24),
            AutoSize = true
        };

        var lblSub = new Label
        {
            Text = "DeepCode Analytics platformu ve ekibimiz hakkında.",
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(161, 161, 170),
            Location = new Point(24, 60),
            AutoSize = true
        };

        var pnlAboutCard = new RoundedPanel
        {
            Location = new Point(24, 100),
            Size = new Size(600, 300),
            BorderRadius = 12,
            BorderSize = 1,
            BorderColor = Color.FromArgb(45, 45, 48),
            BackColor = Color.FromArgb(24, 24, 27),
            Padding = new Padding(24)
        };

        var lblAboutTitle = new Label
        {
            Text = "DeepCode Analytics v1.0.0",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(24, 24),
            AutoSize = true,
            BackColor = Color.Transparent
        };

        var lblAboutDesc = new Label
        {
            Text = "DeepCode Analytics; projenizdeki kod kalitesini artırmak ve güvenlik açıklarını tespit etmek üzere Roslyn kod analizörleri ile RAG (Retrieval-Augmented Generation) tabanlı yapay zeka modelini birleştiren modern bir hibrit analiz platformudur.\n\n" +
                   "Ekip Üyeleri & Rol Dağılımı:\n" +
                   "• Deniz — Yapay Zeka RAG Entegrasyonu & Roslyn Kod Analizörleri\n" +
                   "• Yarengul — Tasarım Sistemi (Design System) & Arayüz Mimarisi\n\n" +
                   "Tüm hakları saklıdır. © 2026 DeepCode Analytics.",
            Font = new Font("Segoe UI", 10F, FontStyle.Regular),
            ForeColor = Color.FromArgb(200, 205, 215),
            Location = new Point(24, 60),
            Size = new Size(550, 220),
            BackColor = Color.Transparent,
            AutoSize = false
        };

        pnlAboutCard.Controls.Add(lblAboutTitle);
        pnlAboutCard.Controls.Add(lblAboutDesc);

        _pnlAbout.Controls.Add(lblHeader);
        _pnlAbout.Controls.Add(lblSub);
        _pnlAbout.Controls.Add(pnlAboutCard);
    }

    /// <summary>
    /// Arayüzü Light Mode / Dark Mode arasında akıcı bir şekilde günceller.
    /// </summary>
    private void ApplyTheme()
    {
        Color formBg = _isDarkMode ? Color.FromArgb(9, 9, 11) : Color.FromArgb(244, 244, 245);
        Color headerBg = _isDarkMode ? Color.FromArgb(11, 14, 20) : Color.FromArgb(244, 244, 245);
        Color cardBg = _isDarkMode ? Color.FromArgb(24, 24, 27) : Color.White;
        Color borderCol = _isDarkMode ? Color.FromArgb(51, 51, 51) : Color.FromArgb(228, 228, 231);
        Color textPrimary = _isDarkMode ? Color.FromArgb(244, 244, 245) : Color.FromArgb(24, 24, 27);
        Color textSecondary = _isDarkMode ? Color.FromArgb(161, 161, 170) : Color.FromArgb(113, 113, 122);
        Color sidebarBg = _isDarkMode ? Color.FromArgb(11, 14, 20) : Color.FromArgb(244, 244, 245);

        // Form ve temel düzen
        this.BackColor = formBg;
        _tblRoot.BackColor = formBg;
        _tblMain.BackColor = formBg;
        _tblCenter.BackColor = formBg;
        _pnlRight.BackColor = formBg;
        _pnlTopHeader.BackColor = headerBg;
        _pnlSidebar.BackColor = sidebarBg;
        _lblVersion.BackColor = sidebarBg;

        // Üst panel logoları
        _lblLogo.ForeColor = textPrimary;
        _lblSubtitle.ForeColor = textSecondary;

        // ComboBox & Düğmeler
        _cmbAiProvider.BackColor = _isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
        _cmbAiProvider.ForeColor = textPrimary;
        _btnDosyaYukle.BackColor = _isDarkMode ? Color.FromArgb(45, 45, 48) : Color.FromArgb(228, 228, 231);
        _btnDosyaYukle.ForeColor = textPrimary;

        // Durum Işığı (Status Dot) Premium Tema Uyumu
        if (_isDarkMode)
        {
            _lblStatusDot.BackColor = Color.FromArgb(30, 60, 30);
            _lblStatusDot.ForeColor = Color.FromArgb(60, 179, 113);
        }
        else
        {
            _lblStatusDot.BackColor = Color.FromArgb(220, 252, 231);
            _lblStatusDot.ForeColor = Color.FromArgb(21, 128, 61);
        }

        // Editör Kartı
        _pnlEditor.BackColor = cardBg;
        _pnlEditor.BorderColor = borderCol;
        _pnlEditorBody.BackColor = cardBg;
        _pnlEditorHeader.BackColor = _isDarkMode ? Color.FromArgb(16, 20, 28) : Color.FromArgb(244, 244, 245);
        _pnlEditorFooter.BackColor = _isDarkMode ? Color.FromArgb(16, 20, 28) : Color.FromArgb(244, 244, 245);
        _lblEditorTitle.ForeColor = textPrimary;
        _txtKodAlani.BackColor = cardBg;
        _txtKodAlani.ForeColor = _isDarkMode ? Color.FromArgb(212, 212, 212) : Color.FromArgb(24, 24, 27);
        _pnlLineNumbers.BackColor = _isDarkMode ? Color.FromArgb(14, 17, 24) : Color.FromArgb(228, 228, 231);

        // Editör alt hap etiketleri (UTF-8, Lines)
        _lblEditorUtf8.BackColor = _isDarkMode ? Color.FromArgb(22, 27, 38) : Color.FromArgb(244, 244, 245);
        _lblEditorUtf8.ForeColor = textSecondary;
        _lblEditorLines.BackColor = _isDarkMode ? Color.FromArgb(22, 27, 38) : Color.FromArgb(244, 244, 245);
        _lblEditorLines.ForeColor = textSecondary;

        // AI Kartı
        _pnlAi.BackColor = cardBg;
        _pnlAi.BorderColor = borderCol;
        _pnlAiHeader.BackColor = cardBg;
        _pnlAiBody.BackColor = cardBg;
        _tblAiColumns.BackColor = cardBg;
        _lblAiHeaderTitle.ForeColor = textPrimary;
        _lblAiHeaderSubtitle.ForeColor = textSecondary;
        
        _pnlAiColProblem.BackColor = cardBg;
        _pnlAiColDesc.BackColor = cardBg;
        _pnlAiColSolution.BackColor = cardBg;
        _flpAiProblem.BackColor = cardBg;
        _flpAiDesc.BackColor = cardBg;
        _flpAiSolution.BackColor = cardBg;

        // İnceleme Sonuçları Kartı
        _pnlResults.BackColor = cardBg;
        _pnlResults.BorderColor = borderCol;
        _pnlResultsHeader.BackColor = headerBg;
        _pnlResultsFooter.BackColor = headerBg;
        _flpIssues.BackColor = cardBg;
        _lblResultsTitle.ForeColor = textPrimary;
        _lblHigh.ForeColor = textPrimary;
        _lblMedium.ForeColor = textPrimary;
        _lblLow.ForeColor = textPrimary;

        // Ek sayfaların alt elemanlarını ve dinamik listeleri rekürsif olarak güncelle
        UpdateThemeRecursive(_pnlHistory, formBg, cardBg, textPrimary, textSecondary, borderCol);
        UpdateThemeRecursive(_pnlSettings, formBg, cardBg, textPrimary, textSecondary, borderCol);
        UpdateThemeRecursive(_pnlAbout, formBg, cardBg, textPrimary, textSecondary, borderCol);
        UpdateThemeRecursive(_flpIssues, formBg, cardBg, textPrimary, textSecondary, borderCol);
        UpdateThemeRecursive(_pnlAiBody, formBg, cardBg, textPrimary, textSecondary, borderCol);

        // Ayarlar Tema Seçim Kartlarının Görünüm Güncellemesi
        if (_isDarkMode)
        {
            _pnlDarkCard.BackColor = Color.FromArgb(18, 18, 20);
            _pnlDarkCard.BorderColor = Color.FromArgb(0, 122, 204);
            _pnlDarkCard.BorderSize = 2;
            _lblDarkCheck.Text = "✓";
            _lblDarkCheck.ForeColor = Color.FromArgb(74, 222, 128);

            _pnlLightCard.BackColor = Color.FromArgb(24, 24, 27);
            _pnlLightCard.BorderColor = borderCol;
            _pnlLightCard.BorderSize = 1;
            _lblLightCheck.Text = "";
        }
        else
        {
            _pnlLightCard.BackColor = Color.White;
            _pnlLightCard.BorderColor = Color.FromArgb(0, 122, 204);
            _pnlLightCard.BorderSize = 2;
            _lblLightCheck.Text = "✓";
            _lblLightCheck.ForeColor = Color.FromArgb(0, 122, 204);

            _pnlDarkCard.BackColor = Color.FromArgb(228, 228, 231);
            _pnlDarkCard.BorderColor = borderCol;
            _pnlDarkCard.BorderSize = 1;
            _lblDarkCheck.Text = "";
        }

        // Seçim kartlarının altındaki metinlerin renk güncellemesi
        foreach (Control ctrl in _pnlDarkCard.Controls)
        {
            if (ctrl is Label l && l != _lblDarkCheck)
            {
                l.ForeColor = (l.Font.Bold || l.Font.Size >= 11) ? textPrimary : textSecondary;
            }
        }
        foreach (Control ctrl in _pnlLightCard.Controls)
        {
            if (ctrl is Label l && l != _lblLightCheck)
            {
                l.ForeColor = (l.Font.Bold || l.Font.Size >= 11) ? textPrimary : textSecondary;
            }
        }

        // Ayarlar Model Seçim Kutusu
        _cmbSettingsAiModel.BackColor = _isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
        _cmbSettingsAiModel.ForeColor = textPrimary;

        // API Anahtarı Düzenleme Butonu
        _btnEditApiKeys.BackColor = _isDarkMode ? Color.FromArgb(39, 39, 42) : Color.FromArgb(228, 228, 231);
        _btnEditApiKeys.ForeColor = _isDarkMode ? Color.FromArgb(161, 161, 170) : Color.FromArgb(82, 82, 91);
        _btnEditApiKeys.BorderColor = _isDarkMode ? Color.FromArgb(63, 63, 70) : Color.FromArgb(200, 200, 210);

        // Sidebar Alt Tema Elemanlarının Güncellenmesi
        _lblSidebarTheme.Text = _isDarkMode ? "Koyu Tema" : "Açık Tema";
        _lblSidebarTheme.ForeColor = textSecondary;
        _lblVersion.ForeColor = _isDarkMode ? Color.FromArgb(60, 65, 75) : Color.FromArgb(120, 120, 130);
        _btnSidebarThemeToggle.Invalidate();

        // Kullanıcı Giriş / Profil Kartı Tema Güncellemesi
        _pnlUserProfile.BackColor = sidebarBg;
        _pnlUserCard.BackColor = _isDarkMode ? Color.FromArgb(20, 20, 24) : Color.White;
        _pnlUserCard.BorderColor = _isDarkMode ? Color.FromArgb(45, 45, 48) : Color.FromArgb(228, 228, 231);
        _lblUserName.ForeColor = textPrimary;
        _lblUserRole.ForeColor = textSecondary;
        _pnlUserAvatar.Invalidate();
        _pnlUserCard.Invalidate();

        // Sidebar butonlarını yeniden boyamaya zorla
        _btnDashboard.Invalidate();
        _btnHistory.Invalidate();
        _btnSettings.Invalidate();
        _btnAbout.Invalidate();

        this.Invalidate(true);
    }

    /// <summary>
    /// Ek sayfaların içerisindeki bileşenlerin renklerini rekürsif günceller.
    /// </summary>
    private void UpdateThemeRecursive(Control c, Color bg, Color cardBg, Color primaryText, Color secText, Color border)
    {
        if (c is Panel p && p != _pnlHistory && p != _pnlSettings && p != _pnlAbout && p != _pnlSidebarBottom)
        {
            if (p is RoundedPanel rp)
            {
                if (rp != _pnlDarkCard && rp != _pnlLightCard)
                {
                    rp.BackColor = cardBg;
                    rp.BorderColor = border;
                }
            }
            else
            {
                p.BackColor = cardBg;
            }
        }
        else if (c is Label l && l is not RoundedLabel)
        {
            if (l != _lblGeminiApiStatus && l != _lblGroqApiStatus && 
                l != _lblDarkCheck && l != _lblLightCheck &&
                l.ForeColor != Color.FromArgb(220, 53, 69) && 
                l.ForeColor != Color.FromArgb(40, 167, 69) && 
                l.ForeColor != Color.FromArgb(74, 222, 128))
            {
                l.ForeColor = (l.Font.Bold || l.Font.Size >= 11) ? primaryText : secText;
            }
        }
        else if (c is Button b && b is not RoundedButton)
        {
            b.BackColor = cardBg;
            b.ForeColor = primaryText;
        }
        else if (c is CheckBox cb)
        {
            cb.ForeColor = primaryText;
        }

        foreach (Control child in c.Controls)
        {
            UpdateThemeRecursive(child, bg, cardBg, primaryText, secText, border);
        }
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

        _cmbAiProvider.Location = new Point(right - _cmbAiProvider.Width, yButton + 7);
        right = _cmbAiProvider.Left - 10;

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
            var selectedEngine = DeepCodeAnalytics.Application.Enums.AiEngineType.Groq;
            if (_cmbAiProvider.SelectedItem?.ToString() == "Gemini") selectedEngine = DeepCodeAnalytics.Application.Enums.AiEngineType.Gemini;
            else if (_cmbAiProvider.SelectedItem?.ToString() == "OpenRouter") selectedEngine = DeepCodeAnalytics.Application.Enums.AiEngineType.OpenRouter;

            var result = await _analizYoneticisi.AnalizEtAsync(code, selectedEngine);
            RenderIssues(result.Issues);
            RenderAiSuggestions(result.Suggestions.FirstOrDefault()?.SuggestionText);
        }
        catch (Exception)
        {
            // Olası HTTP 401, KeyNotFound veya diğer backend hatalarında arayüzün çökmesini engeller.
            // Hata metni boşluksuz ham JSON (örn. 401 invalid key) geldiğinde WinForms Label wrap yapamadığı için UI bozuluyordu (Panel dışına taşıyordu).
            // Kullanıcının istediği temiz, şık ve Türkçe mesajı göstererek bu sorunu çözüyoruz.
            ClearAiGrid();
            
            // "Analiz Sonuçları" rozetini hata moduna al
            _lblIssuesBadge.Text = "Hata";
            _lblIssuesBadge.BackColor = Color.FromArgb(220, 53, 69); // Kırmızı
            
            // AI Önerileri paneline kırmızı tonlarında şık bir hata bildirimi ekle
            AddAiRow(
                "BAĞLANTI HATASI", 
                "Hata: API Anahtarı Geçersiz veya Bağlantı Sağlanamadı.", 
                "Lütfen Ayarlar sayfasından API Key bilginizi kontrol edin."
            );
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
            Font = FontText,
            BackColor = Color.Transparent,
            Location = new Point(0, 24),
            UseCompatibleTextRendering = false
        };

        panel.Controls.Add(lblHeader);
        panel.Controls.Add(lblContent);

        // Alt glow çizgisi (2px)
        panel.Paint += (_, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(180, accentColor), 2F);
            int y = panel.Height - 2;
            if (y < 0) return;
            e.Graphics.DrawLine(pen, 0, y, panel.Width, y);
        };

        // Hover büyüme (yumuşak)
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

        panel.SizeChanged += (_, _) => RelayoutAiCard(panel, lblHeader, lblContent);
        return panel;
    }

    private static void RelayoutAiCard(RoundedPanel panel, Label lblHeader, Label lblContent)
    {
        int innerW = Math.Max(80, panel.Width - panel.Padding.Left - panel.Padding.Right);

        lblHeader.Width = innerW;
        lblHeader.Location = new Point(panel.Padding.Left, panel.Padding.Top);

        int contentY = lblHeader.Bottom + 6;
        lblContent.Location = new Point(panel.Padding.Left, contentY);
        lblContent.Width = innerW;

        var flags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
        var measured = TextRenderer.MeasureText(lblContent.Text ?? string.Empty, lblContent.Font, new Size(innerW, int.MaxValue), flags);
        lblContent.Height = Math.Max(18, measured.Height);

        panel.Height = lblContent.Bottom + panel.Padding.Bottom + 4;
        panel.Invalidate();
    }

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

    private static void ConfigureAiColumnPanel(Panel colPanel, Panel headerPanel, FlowLayoutPanel flp, string headerText, Color headerColor)
    {
        colPanel.Dock = DockStyle.Fill;
        colPanel.BackColor = CardBg;
        colPanel.AutoScroll = true;
        colPanel.BorderStyle = BorderStyle.None;

        headerPanel.Dock = DockStyle.Top;
        headerPanel.Height = 28;
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

    /// <summary>
    /// Sidebar buton tıklama olayı. Dinamik sayfa geçişlerini tetikler.
    /// </summary>
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
        // Dinamik Sayfa Geçiş Mantığı
        bool isDashboard = btn == _btnDashboard;
        _tblCenter.Visible = isDashboard;
        _pnlRight.Visible = isDashboard;

        _pnlHistory.Visible = btn == _btnHistory;
        _pnlSettings.Visible = btn == _btnSettings;
        _pnlAbout.Visible = btn == _btnAbout;
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
        
        Color sidebarCurrentBg = _isDarkMode ? SidebarBg : Color.FromArgb(244, 244, 245); // Açık temada Zinc-100 (#f4f4f5)
        Color hoverCurrentTint = _isDarkMode ? HoverTint : Color.FromArgb(228, 228, 231); // Açık temada Zinc-200 (#e4e4e7)
        Color inactiveCurrentText = _isDarkMode ? InactiveText : Color.FromArgb(24, 24, 27); // Açık temada koyu metin (#18181b)
        
        e.Graphics.Clear(sidebarCurrentBg);

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
            using (var hoverBrush = new SolidBrush(hoverCurrentTint))
            {
                e.Graphics.FillPath(hoverBrush, hoverPath);
            }
            DrawSidebarText(e.Graphics, btn, _isDarkMode ? Color.FromArgb(210, 215, 225) : Color.FromArgb(24, 24, 27));
            return;
        }

        DrawSidebarText(e.Graphics, btn, inactiveCurrentText);
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
        
        Color numbersBg = _isDarkMode ? Color.FromArgb(14, 17, 24) : Color.FromArgb(228, 228, 231);
        Color numbersFg = _isDarkMode ? Color.FromArgb(70, 78, 95) : Color.FromArgb(120, 130, 145);
        Color sepColor = _isDarkMode ? Color.FromArgb(30, 35, 48) : Color.FromArgb(200, 200, 210);
        
        e.Graphics.Clear(numbersBg);

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
                numbersFg,
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }

        using var gutterPen = new Pen(sepColor, 1);
        e.Graphics.DrawLine(gutterPen, _pnlLineNumbers.Width - 1, 0, _pnlLineNumbers.Width - 1, _pnlLineNumbers.Height);
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
