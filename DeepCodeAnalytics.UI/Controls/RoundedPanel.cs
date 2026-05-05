using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DeepCodeAnalytics.UI.Controls
{
    public class RoundedPanel : Panel
    {
        private int _borderRadius = 15;
        private Color _borderColor = Color.Transparent;
        private int _borderSize = 0;
        private bool _drawShadow = false;

        public int BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = value; this.Invalidate(); }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; this.Invalidate(); }
        }

        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = value; this.Invalidate(); }
        }

        public bool DrawShadow
        {
            get { return _drawShadow; }
            set { _drawShadow = value; this.Invalidate(); }
        }

        public RoundedPanel()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Resize += (s, e) => this.Invalidate();
        }

        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -_borderSize, -_borderSize);
            int smoothSize = 2;
            if (_borderSize > 0) smoothSize = _borderSize;

            if (_borderRadius > 2) 
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, _borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, _borderRadius - _borderSize))
                using (Pen penSurface = new Pen(this.Parent?.BackColor ?? Color.Black, smoothSize))
                using (Pen penBorder = new Pen(_borderColor, _borderSize))
                {
                    // If DrawShadow is true, we draw a faux shadow using alpha blended pens BEFORE drawing the main panel
                    if (_drawShadow)
                    {
                        // To avoid clipping the shadow, we do NOT set Region if we want the shadow to bleed out.
                        // However, WinForms panels are opaque by default unless BackColor is Transparent.
                        // Since we set BackColor to 30,30,30, we must clear the background with Parent's color first.
                        e.Graphics.Clear(this.Parent?.BackColor ?? Color.FromArgb(18, 18, 18));

                        // Draw Shadow / Glow (Neon green-ish glow)
                        for (int i = 0; i < 8; i++)
                        {
                            Rectangle rectShadow = new Rectangle(
                                rectSurface.X + i, rectSurface.Y + i,
                                rectSurface.Width - (i * 2), rectSurface.Height - (i * 2)
                            );
                            using (GraphicsPath pathShadow = GetFigurePath(rectShadow, _borderRadius))
                            using (Pen penShadow = new Pen(Color.FromArgb(10, 0, 255, 128), 3)) // Subtle neon green glow
                            {
                                e.Graphics.DrawPath(penShadow, pathShadow);
                            }
                        }

                        // Fill the main card body
                        using (SolidBrush brush = new SolidBrush(this.BackColor))
                        {
                            e.Graphics.FillPath(brush, pathSurface);
                        }
                    }
                    else
                    {
                        // Strict clipping for non-shadow panels
                        this.Region = new Region(pathSurface);
                        e.Graphics.Clear(this.BackColor);
                    }
                    
                    if (!this._drawShadow) e.Graphics.DrawPath(penSurface, pathSurface);
                    if (_borderSize >= 1) e.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else 
            {
                this.Region = new Region(rectSurface);
                if (_borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(_borderColor, _borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }
    }
}
