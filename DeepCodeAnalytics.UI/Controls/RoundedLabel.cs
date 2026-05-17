using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DeepCodeAnalytics.UI.Controls
{
    public class RoundedLabel : Label
    {
        public int BorderRadius { get; set; } = 0;

        public RoundedLabel()
        {
            this.DoubleBuffered = true;
            this.AutoSize = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Clear background with parent's color
            e.Graphics.Clear(this.Parent?.BackColor ?? Color.Transparent);

            // Draw pill shape or rounded square
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            int radius = BorderRadius > 0 ? BorderRadius * 2 : this.Height; // Default to perfect pill if 0
            
            using (GraphicsPath path = GetPillPath(rect, radius))
            {
                // Draw slight glow
                using (Pen glowPen = new Pen(Color.FromArgb(50, this.BackColor), 3))
                {
                    e.Graphics.DrawPath(glowPen, path);
                }

                // Fill pill
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }

            // Draw text
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.None
            };
            
            using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(this.Text, this.Font, textBrush, rect, format);
            }
        }

        private GraphicsPath GetPillPath(Rectangle rect, int diameter)
        {
            GraphicsPath path = new GraphicsPath();
            
            if (diameter >= rect.Height)
            {
                // Pill shape
                int d = rect.Height;
                path.AddArc(rect.X, rect.Y, d, d, 90, 180);
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 180);
            }
            else
            {
                // Rounded Rectangle
                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            }
            
            path.CloseFigure();
            return path;
        }
    }
}
