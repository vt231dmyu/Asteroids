using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsLibrary
{
    public class MyTextRenderer : IDisposable
    {
        private const string DefaultFontFamily = "Segoe UI Light";
        private const float DefaultFontSize = 16f;

        private Font myFont;
        private float size;

        public static string DEFAULT_FONT_FAMILY => DefaultFontFamily;
        public static float DEFAULT_FONT_SIZE => DefaultFontSize;

        public Font MyFont
        {
            get => myFont;
            set => myFont = value;
        }

        public float Size
        {
            get => size;
            set
            {
                if (size != value)
                {
                    size = value;
                    UpdateFont();
                }
            }
        }

        public MyTextRenderer()
        {
            size = DefaultFontSize;
            UpdateFont();
        }

        private void UpdateFont()
        {
            MyFont?.Dispose();
            MyFont = new Font(new FontFamily(DefaultFontFamily), size, FontStyle.Regular);
        }

        public void DrawText(Graphics g, string text, Brush brush, Point point)
        {
            g.DrawString(text, myFont, brush, point);
        }

        public void DrawTextWithLetterSpacing(Graphics g, string text, Brush brush, Point point, float letterSpacing)
        {
            float x = point.X;
            float y = point.Y;

            foreach (char c in text)
            {
                SizeF charSize = g.MeasureString(c.ToString(), myFont);
                g.DrawString(c.ToString(), myFont, brush, x, y);
                x += charSize.Width + letterSpacing;
            }
        }

        public SizeF MeasureTextWithLetterSpacing(Graphics g, string text, Font font, float letterSpacing)
        {
            float spaceBetweenCharacters = letterSpacing;
            float totalWidth = 0;
            foreach (char c in text)
            {
                SizeF charSize = g.MeasureString(c.ToString(), font);
                totalWidth += charSize.Width + spaceBetweenCharacters;
            }
            totalWidth -= spaceBetweenCharacters;
            return new SizeF(totalWidth, font.Height);
        }

        public void Dispose()
        {
            MyFont?.Dispose();
        }
    }
}
