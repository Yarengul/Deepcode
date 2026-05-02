using System;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using DeepCodeAnalytics.Application.Services;

namespace DeepCodeAnalytics.UI
{
    public partial class Form1 : Form
    {
        private readonly AnalizYoneticisi _analizYoneticisi;

        // ── Sidebar interactive state ──
        private Button _activeButton;
        private Button _hoveredButton;
        private int _fadeAlpha = 0;
        private System.Windows.Forms.Timer _fadeTimer;

        private static readonly Color SidebarBg = Color.FromArgb(11, 14, 20);
        private static readonly Color HoverTint = Color.FromArgb(18, 22, 32);
        private static readonly Color InactiveText = Color.FromArgb(160, 165, 180);

        public Form1(AnalizYoneticisi analizYoneticisi)
        {
            InitializeComponent();
            _analizYoneticisi = analizYoneticisi;

            // Fade-in timer for selection transitions
            _fadeTimer = new System.Windows.Forms.Timer();
            _fadeTimer.Interval = 16; // ~60 FPS
            _fadeTimer.Tick += FadeTimer_Tick;

            // Wire all sidebar buttons to the unified system
            _activeButton = btnDashboard; // Default active
            foreach (Button btn in new[] { btnDashboard, btnHistory, btnSettings, btnAbout })
            {
                btn.ForeColor = Color.Transparent;
                btn.Paint += SidebarButton_Paint;
                btn.MouseEnter += SidebarButton_MouseEnter;
                btn.MouseLeave += SidebarButton_MouseLeave;
                btn.Click += SidebarButton_Click;
            }

            btnAnalizEt.Paint += BtnAnalizEt_Paint;

            // ── Code Editor wiring ──
            txtKodAlani.TextChanged += TxtKodAlani_TextChanged;
            txtKodAlani.VScroll += (s, ev) => pnlLineNumbers.Invalidate();
            txtKodAlani.Resize += (s, ev) => pnlLineNumbers.Invalidate();
            txtKodAlani.SelectionChanged += (s, ev) => pnlLineNumbers.Invalidate();
            pnlLineNumbers.Paint += PnlLineNumbers_Paint;
            pnlEditorHeader.Resize += PnlEditorHeader_Resize;
            pnlResultsHeader.Resize += (s, ev) => PositionResultsHeaderBadge();

            // Position header badges once initially
            PositionEditorHeaderBadges();
            PositionResultsHeaderBadge();

            lblTotalIssuesBadge.Text = "0 Issue";
            lblTotalIssuesBadge.TextAlign = ContentAlignment.MiddleCenter;
            UpdateFooterStats(0, 0, 0);

            // Flicker-free rendering
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Apply ClearTypeGridFit to every control on load
            this.Load += (s, ev) => ApplyClearType(this);
        }

        private void ApplyClearType(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.UseCompatibleTextRendering = false;
                ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size, ctrl.Font.Style);
                ApplyClearType(ctrl);
            }
        }

        // ── Sidebar icon/label mapping ──
        private string GetSidebarIcon(Button btn)
        {
            if (btn == btnDashboard) return "📊";
            if (btn == btnHistory) return "🕒";
            if (btn == btnSettings) return "⚙";
            if (btn == btnAbout) return "ℹ";
            return "";
        }

        private string GetSidebarLabel(Button btn)
        {
            if (btn == btnDashboard) return "Kod Analizi";
            if (btn == btnHistory) return "Geçmiş";
            if (btn == btnSettings) return "Ayarlar";
            if (btn == btnAbout) return "Hakkında";
            return "";
        }

        // ── Hover handlers ──
        private void SidebarButton_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || btn == _activeButton) return;
            _hoveredButton = btn;
            btn.Invalidate();
        }

        private void SidebarButton_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            if (_hoveredButton == btn) _hoveredButton = null;
            btn.Invalidate();
        }

        // ── Click handler — selection transfer ──
        private void SidebarButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || btn == _activeButton) return;

            Button previousActive = _activeButton;
            _activeButton = btn;
            _hoveredButton = null;

            // Start fade-in animation
            _fadeAlpha = 0;
            _fadeTimer.Start();

            // Repaint both the old and new active buttons
            previousActive?.Invalidate();
            btn.Invalidate();
        }

        // ── Fade animation tick ──
        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            _fadeAlpha += 25; // ~10 steps to full opacity (250ms at 60fps)
            if (_fadeAlpha >= 255)
            {
                _fadeAlpha = 255;
                _fadeTimer.Stop();
            }
            _activeButton?.Invalidate();
        }

        // ── Unified sidebar paint handler ──
        private void SidebarButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            bool isActive = (btn == _activeButton);
            bool isHovered = (btn == _hoveredButton);

            // 1. Clear to sidebar base color
            e.Graphics.Clear(SidebarBg);

            Rectangle rect = btn.ClientRectangle;
            Rectangle innerRect = new Rectangle(rect.X + 4, rect.Y + 3, rect.Width - 8, rect.Height - 6);

            if (isActive)
            {
                // Compute alpha for fade-in animation
                int alpha = _fadeAlpha;

                // A. Subtle blue outer glow (layered strokes)
                using (GraphicsPath glowPath = GetRoundedPath(innerRect, 12))
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        int glowAlpha = (int)(10 * (6 - i) * (alpha / 255.0));
                        using (Pen pen = new Pen(Color.FromArgb(Math.Max(0, Math.Min(255, glowAlpha)), 0, 100, 200), i * 2))
                        {
                            pen.LineJoin = LineJoin.Round;
                            e.Graphics.DrawPath(pen, glowPath);
                        }
                    }
                }

                // B. Blue Gradient fill with 12px rounded corners
                using (GraphicsPath path = GetRoundedPath(innerRect, 12))
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    innerRect,
                    Color.FromArgb(alpha, 0, 110, 195),
                    Color.FromArgb(alpha, 0, 55, 130),
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // C. Vertical Orange Accent Bar — extreme left edge (X=0)
                using (SolidBrush orangeBrush = new SolidBrush(Color.FromArgb(alpha, 253, 126, 20)))
                {
                    e.Graphics.FillRectangle(orangeBrush, new Rectangle(0, innerRect.Y + 6, 4, innerRect.Height - 12));
                }

                // D. Draw text in white
                DrawSidebarText(e.Graphics, btn, Color.White);
            }
            else if (isHovered)
            {
                // Hover: subtle light tint background with rounded corners
                using (GraphicsPath hoverPath = GetRoundedPath(innerRect, 10))
                using (SolidBrush hoverBrush = new SolidBrush(HoverTint))
                {
                    e.Graphics.FillPath(hoverBrush, hoverPath);
                }

                // Draw text slightly brighter than inactive
                DrawSidebarText(e.Graphics, btn, Color.FromArgb(210, 215, 225));
            }
            else
            {
                // Default/Inactive state
                DrawSidebarText(e.Graphics, btn, InactiveText);
            }
        }

        // ── Shared text drawing for sidebar ──
        private void DrawSidebarText(Graphics g, Button btn, Color textColor)
        {
            using (Font iconFont = new Font("Segoe UI Emoji", 11F, FontStyle.Regular))
            using (Font labelFont = new Font("Segoe UI", 10F, FontStyle.Regular))
            {
                Rectangle iconRect = new Rectangle(18, 0, 24, btn.Height);
                Rectangle labelRect = new Rectangle(48, 0, btn.Width - 52, btn.Height);

                TextRenderer.DrawText(g, GetSidebarIcon(btn), iconFont, iconRect, textColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding);
                TextRenderer.DrawText(g, GetSidebarLabel(btn), labelFont, labelRect, textColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding);
            }
        }
        
        private GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);
            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        // ═══════════════════════════════════════════
        // ── Code Editor: Line Gutter & Header ──
        // ═══════════════════════════════════════════

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private const int EM_GETFIRSTVISIBLELINE = 0x00CE;

        private void PnlLineNumbers_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.Clear(Color.FromArgb(14, 17, 24));

            int firstVisibleLine = SendMessage(txtKodAlani.Handle, EM_GETFIRSTVISIBLELINE, 0, 0);
            int totalLines = txtKodAlani.Lines.Length;
            if (totalLines == 0) totalLines = 1;

            float lineHeight = txtKodAlani.Font.GetHeight(e.Graphics);
            int visibleLines = (int)(pnlLineNumbers.Height / lineHeight) + 2;

            using (Font gutterFont = new Font("Cascadia Code", 9.5F, FontStyle.Regular))
            {
                for (int i = 0; i < visibleLines; i++)
                {
                    int lineNum = firstVisibleLine + i + 1;
                    if (lineNum > totalLines) break;

                    float y = i * lineHeight + 1;
                    string numStr = lineNum.ToString();

                    Color numColor = (lineNum <= totalLines)
                        ? Color.FromArgb(70, 78, 95)
                        : Color.FromArgb(40, 45, 55);

                    TextRenderer.DrawText(e.Graphics, numStr, gutterFont,
                        new Rectangle(0, (int)y, pnlLineNumbers.Width - 10, (int)lineHeight),
                        numColor,
                        TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                }
            }

            // Draw right border separator
            using (Pen sepPen = new Pen(Color.FromArgb(30, 35, 48), 1))
            {
                e.Graphics.DrawLine(sepPen, pnlLineNumbers.Width - 1, 0, pnlLineNumbers.Width - 1, pnlLineNumbers.Height);
            }
        }

        private void TxtKodAlani_TextChanged(object sender, EventArgs e)
        {
            int lineCount = txtKodAlani.Lines.Length;
            if (lineCount == 0) lineCount = 1;
            lblEditorLinesBadge.Text = $"Lines: {lineCount}";
            pnlLineNumbers.Invalidate();
        }

        private void PnlEditorHeader_Resize(object sender, EventArgs e)
        {
            PositionEditorHeaderBadges();
        }

        private void PositionEditorHeaderBadges()
        {
            int rightMargin = 12;
            int spacing = 6;
            int y = 10;

            // Position from right to left: UTF-8 → Lines → C#
            lblEditorUtf8.Location = new Point(pnlEditorHeader.Width - lblEditorUtf8.Width - rightMargin, y);
            lblEditorLinesBadge.Location = new Point(lblEditorUtf8.Left - lblEditorLinesBadge.Width - spacing, y);
            lblEditorLangBadge.Location = new Point(lblEditorLinesBadge.Left - lblEditorLangBadge.Width - spacing, y);
        }

        private void PositionResultsHeaderBadge()
        {
            lblTotalIssuesBadge.Location = new Point(pnlResultsHeader.Width - lblTotalIssuesBadge.Width - 12, 11);
        }

        private void BtnAnalizEt_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(btnAnalizEt.ClientRectangle, Color.FromArgb(253, 126, 20), Color.FromArgb(220, 53, 69), LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                int radius = 8;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(btnAnalizEt.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(btnAnalizEt.Width - radius, btnAnalizEt.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, btnAnalizEt.Height - radius, radius, radius, 90, 90);
                    path.CloseFigure();
                    
                    e.Graphics.FillPath(brush, path);
                }

                TextRenderer.DrawText(e.Graphics, btnAnalizEt.Text, btnAnalizEt.Font, btnAnalizEt.ClientRectangle, Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
            }
        }
        
        private void UpdateFooterStats(int high, int medium, int low)
        {
            lblFooterYusek.Text = $"🔴 Yüksek: {high}";
            lblFooterOrta.Text = $"🟠 Orta: {medium}";
            lblFooterDusuk.Text = $"🔵 Düşük: {low}";
        }

        private async void btnAnalizEt_Click(object sender, EventArgs e)
        {
            string sourceCode = txtKodAlani.Text;

            if (string.IsNullOrWhiteSpace(sourceCode))
            {
                MessageBox.Show("Lütfen analiz edilecek kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guard: excessively long code
            if (sourceCode.Length > 50000)
            {
                MessageBox.Show("Kod çok uzun (maks. 50.000 karakter). Lütfen daha kısa bir dosya yükleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // UI state: analyzing
            btnAnalizEt.Enabled = false;
            btnAnalizEt.Text = "Analiz Ediliyor...";
            lblStatusDot.BackColor = Color.FromArgb(253, 126, 20); // Orange = busy
            pnlErrorCards.Controls.Clear();
            lblKalitePuani.Text = "%0";

            // Show loading placeholder
            AddSectionHeader("⏳ Analiz Ediliyor...", "Roslyn ve AI analizi devam ediyor, lütfen bekleyin.");

            try
            {
                var result = await _analizYoneticisi.AnalizEtAsync(sourceCode);

                // Clear loading placeholder
                pnlErrorCards.Controls.Clear();

                int qualityScore = Math.Max(0, 100 - (result.Issues.Count * 10));
                UpdateStatusBadge(qualityScore);
                _ = AnimateQualityScoreAsync(qualityScore);

                int highCount = 0, mediumCount = 0, lowCount = 0;
                int totalIssues = result.Issues.Count;

                // ── Roslyn issues — show DiagnosticId, Severity, Line, Message ──
                foreach (var issue in result.Issues)
                {
                    string diagTitle = !string.IsNullOrEmpty(issue.DiagnosticId)
                        ? $"{issue.DiagnosticId} — {issue.Message}"
                        : issue.Message;

                    // Map Roslyn severity string to Turkish display
                    string sev;
                    if (issue.Severity.Equals("Error", StringComparison.OrdinalIgnoreCase) ||
                        issue.Severity.Equals("Warning", StringComparison.OrdinalIgnoreCase))
                    { sev = "Yüksek"; highCount++; }
                    else if (issue.Severity.Equals("Info", StringComparison.OrdinalIgnoreCase))
                    { sev = "Düşük"; lowCount++; }
                    else
                    { sev = "Orta"; mediumCount++; }

                    AddAIFindingCard(diagTitle, "", "", sev, issue.Line.ToString());
                }

                // ── AI suggestions ──
                var suggestion = result.Suggestions.FirstOrDefault();
                if (suggestion != null)
                {
                    AddSectionHeader("✨ AI Önerileri", "Yapay zeka destekli çözüm önerileri");

                    try
                    {
                        string cleanJson = suggestion.SuggestionText.Replace("```json", "").Replace("```", "").Trim();
                        using JsonDocument doc = JsonDocument.Parse(cleanJson);
                        var root = doc.RootElement;

                        var issuesArray = root.TryGetProperty("issues", out JsonElement issues) ? issues.EnumerateArray().ToList() : new System.Collections.Generic.List<JsonElement>();
                        var suggestionsArray = root.TryGetProperty("suggestions", out JsonElement suggs) ? suggs.EnumerateArray().ToList() : new System.Collections.Generic.List<JsonElement>();

                        int count = Math.Max(issuesArray.Count, suggestionsArray.Count);
                        totalIssues += count;

                        for (int i = 0; i < count; i++)
                        {
                            string prob = i < issuesArray.Count ? (issuesArray[i].TryGetProperty("message", out var m) ? m.GetString() ?? "Bilinmeyen sorun" : "Bilinmeyen sorun") : "Belirtilmedi";
                            string sev = i < issuesArray.Count ? (issuesArray[i].TryGetProperty("severity", out var s) ? s.GetString() ?? "Orta" : "Orta") : "Orta";
                            string desc = i < suggestionsArray.Count ? (suggestionsArray[i].TryGetProperty("suggestionText", out var st) ? st.GetString() ?? "" : "") : "";
                            string sol = i < suggestionsArray.Count ? (suggestionsArray[i].TryGetProperty("proposedCode", out var pc) ? pc.GetString() ?? "" : "") : "";

                            if (sev.Equals("High", StringComparison.OrdinalIgnoreCase)) highCount++;
                            else if (sev.Equals("Low", StringComparison.OrdinalIgnoreCase)) lowCount++;
                            else mediumCount++;

                            AddAIFindingCard(prob, desc, sol, sev, "");
                        }
                    }
                    catch
                    {
                        AddAIFindingCard("Format Hatası", "AI önerisi başarıyla ayrıştırılamadı.", suggestion.SuggestionText, "Düşük", "");
                    }
                }

                lblTotalIssuesBadge.Text = $"{totalIssues} Issue{(totalIssues > 1 ? "s" : "")}";
                UpdateFooterStats(highCount, mediumCount, lowCount);

                if (!result.Issues.Any() && suggestion == null)
                {
                    AddSectionHeader("✨ Harika!", "Kodunuzda herhangi bir sorun bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Analiz hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAnalizEt.Enabled = true;
                btnAnalizEt.Text = "Analiz Et";
                lblStatusDot.BackColor = Color.MediumSeaGreen; // Green = ready
            }
        }

        private void btnDosyaYukle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "C# Dosyası Seç";
                ofd.Filter = "C# Files (*.cs)|*.cs|All Files (*.*)|*.*";
                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        txtKodAlani.Text = System.IO.File.ReadAllText(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Dosya okunamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async Task AnimateQualityScoreAsync(int targetScore)
        {
            int currentScore = 0;
            int durationMs = 1500; // 1.5 seconds
            int steps = 30;
            int delayMs = durationMs / steps;
            int increment = Math.Max(1, targetScore / steps);

            while (currentScore < targetScore)
            {
                currentScore += increment;
                if (currentScore > targetScore) currentScore = targetScore;

                lblKalitePuani.Text = $"%{currentScore}";
                UpdateStatusBadge(currentScore);
                lblKalitePuani.Invalidate(); // Force repaint for gradient
                await Task.Delay(delayMs);
            }
            lblKalitePuani.Text = $"%{targetScore}";
            lblKalitePuani.Invalidate();
        }

        private void UpdateStatusBadge(int score)
        {
            if (score >= 90)
            {
                lblStatusBadge.Text = "EXCELLENT";
                lblStatusBadge.BackColor = Color.MediumSeaGreen;
            }
            else if (score >= 70)
            {
                lblStatusBadge.Text = "GOOD";
                lblStatusBadge.BackColor = Color.FromArgb(253, 126, 20); // Orange
            }
            else
            {
                lblStatusBadge.Text = "CRITICAL";
                lblStatusBadge.BackColor = Color.FromArgb(220, 53, 69); // Red
            }
        }

        private void lblKalitePuani_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl == null) return;

            // Clear the default drawn text
            e.Graphics.Clear(lbl.Parent.BackColor);

            // Create neon gradient
            using (LinearGradientBrush brush = new LinearGradientBrush(
                lbl.ClientRectangle,
                Color.FromArgb(0, 255, 128),   // Neon Green
                Color.FromArgb(0, 204, 255),   // Cyan
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit; // For crisp typography
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                
                // Draw text with gradient
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(lbl.Text, lbl.Font, brush, lbl.ClientRectangle, format);
            }
        }

        private void AddSectionHeader(string title, string subtitle)
        {
            Panel headerPanel = new Panel
            {
                Width = pnlErrorCards.Width - 30,
                Height = 55,
                Margin = new Padding(0, 5, 0, 5),
                BackColor = Color.Transparent
            };

            Label lblTitle = new Label
            {
                Text = title,
                ForeColor = Color.FromArgb(140, 170, 255),
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(2, 0),
                UseCompatibleTextRendering = false
            };

            Label lblSub = new Label
            {
                Text = subtitle,
                ForeColor = Color.FromArgb(100, 108, 125),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(2, 28),
                UseCompatibleTextRendering = false
            };

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSub);
            pnlErrorCards.Controls.Add(headerPanel);
        }

        private void AddAIFindingCard(string problem, string description, string solution, string severity, string line)
        {
            // Resolve severity display values
            string sevText = severity;
            if (severity.Equals("High", StringComparison.OrdinalIgnoreCase) || severity.Equals("Yüksek", StringComparison.OrdinalIgnoreCase))
                sevText = "Yüksek";
            else if (severity.Equals("Low", StringComparison.OrdinalIgnoreCase) || severity.Equals("Düşük", StringComparison.OrdinalIgnoreCase))
                sevText = "Düşük";
            else
                sevText = "Orta";

            Color sevColor = sevText == "Yüksek" ? Color.FromArgb(220, 53, 69)
                           : sevText == "Orta"   ? Color.FromArgb(253, 126, 20)
                           : Color.FromArgb(0, 122, 204);

            string sevIcon = sevText == "Yüksek" ? "❗" : sevText == "Orta" ? "⚠" : "ℹ";
            bool isHighSeverity = (sevText == "Yüksek");

            // --- Main Card ---
            int cardWidth = pnlErrorCards.Width - 30;

            DeepCodeAnalytics.UI.Controls.RoundedPanel card = new DeepCodeAnalytics.UI.Controls.RoundedPanel
            {
                Width = cardWidth,
                BorderRadius = 12,
                BorderSize = 1,
                BorderColor = Color.FromArgb(40, sevColor.R, sevColor.G, sevColor.B),
                BackColor = Color.FromArgb(30, 30, 30), // #1E1E1E
                DrawShadow = isHighSeverity,
                Margin = new Padding(0, 0, 0, 12),
                Padding = new Padding(0)
            };

            // Paint: left accent bar + optional high-severity glow
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Left severity accent bar (4px wide)
                using (SolidBrush accentBrush = new SolidBrush(sevColor))
                {
                    e.Graphics.FillRectangle(accentBrush, new Rectangle(0, 8, 4, card.Height - 16));
                }

                // High severity: faint red top glow
                if (isHighSeverity)
                {
                    using (LinearGradientBrush glowBrush = new LinearGradientBrush(
                        new Rectangle(0, 0, card.Width, 30),
                        Color.FromArgb(20, 220, 53, 69), Color.Transparent,
                        LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(glowBrush, 0, 0, card.Width, 30);
                    }
                }
            };

            int yPos = 14;
            int leftPad = 18;

            // Row 1: Icon + Title + Severity Badge
            Label lblIcon = new Label
            {
                Text = sevIcon,
                Font = new Font("Segoe UI Emoji", 12F),
                AutoSize = true,
                Location = new Point(leftPad, yPos - 2),
                BackColor = Color.Transparent,
                ForeColor = sevColor
            };
            card.Controls.Add(lblIcon);

            Label lblTitle = new Label
            {
                Text = problem,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(230, 232, 238),
                AutoSize = true,
                MaximumSize = new Size(cardWidth - 140, 0),
                Location = new Point(leftPad + 30, yPos),
                BackColor = Color.Transparent,
                UseCompatibleTextRendering = false
            };
            card.Controls.Add(lblTitle);

            // Severity pill badge — top right
            DeepCodeAnalytics.UI.Controls.RoundedLabel lblSevBadge = new DeepCodeAnalytics.UI.Controls.RoundedLabel
            {
                Text = sevText,
                BackColor = sevColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                Size = new Size(58, 20),
                Location = new Point(cardWidth - 78, yPos + 1),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblSevBadge);

            yPos += Math.Max(lblTitle.Height, 22) + 8;

            // Row 2: Description
            if (!string.IsNullOrEmpty(description))
            {
                Label lblDesc = new Label
                {
                    Text = description,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(170, 175, 185),
                    AutoSize = true,
                    MaximumSize = new Size(cardWidth - 45, 0),
                    Location = new Point(leftPad, yPos),
                    BackColor = Color.Transparent,
                    UseCompatibleTextRendering = false
                };
                card.Controls.Add(lblDesc);
                yPos += lblDesc.Height + 8;
            }

            // Row 3: Solution (if present) — code block style
            if (!string.IsNullOrEmpty(solution))
            {
                Panel pnlSolution = new Panel
                {
                    Location = new Point(leftPad, yPos),
                    Width = cardWidth - 36,
                    AutoSize = true,
                    BackColor = Color.FromArgb(22, 24, 30),
                    Padding = new Padding(10, 8, 10, 8)
                };

                Label lblSolHeader = new Label
                {
                    Text = "✅ Çözüm Önerisi",
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    ForeColor = Color.MediumSeaGreen,
                    AutoSize = true,
                    Location = new Point(10, 6),
                    BackColor = Color.Transparent
                };
                pnlSolution.Controls.Add(lblSolHeader);

                Label lblSolCode = new Label
                {
                    Text = solution,
                    Font = new Font("Cascadia Code", 9F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(190, 200, 210),
                    AutoSize = true,
                    MaximumSize = new Size(cardWidth - 80, 0),
                    Location = new Point(10, 26),
                    BackColor = Color.Transparent,
                    UseCompatibleTextRendering = false
                };
                pnlSolution.Controls.Add(lblSolCode);

                card.Controls.Add(pnlSolution);
                yPos += pnlSolution.Height + 8;
            }

            // Row 4: Line marker (if present)
            if (!string.IsNullOrEmpty(line))
            {
                Label lblLine = new Label
                {
                    Text = "  Line " + line + "  ",
                    BackColor = Color.FromArgb(38, 42, 52),
                    ForeColor = Color.FromArgb(130, 140, 158),
                    Font = new Font("Cascadia Code", 8F),
                    AutoSize = true,
                    Padding = new Padding(4, 2, 4, 2),
                    Location = new Point(leftPad, yPos)
                };
                card.Controls.Add(lblLine);
                yPos += lblLine.Height + 8;
            }

            card.Height = yPos + 12;
            pnlErrorCards.Controls.Add(card);
        }
    }
}
