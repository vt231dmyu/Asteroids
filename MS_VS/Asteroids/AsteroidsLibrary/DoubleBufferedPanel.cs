using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidsLibrary
{
    public class DoubleBufferedPanel : Panel
    {
        private MyTextRenderer myTextRenderer;
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            myTextRenderer = new MyTextRenderer();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawPauseText(e.Graphics);
        }

        public void DrawPauseText(Graphics graphics)
        {
            myTextRenderer.Size = 60f;
            string gameOverText = "Для продовження гри натисніть ESC";
            SizeF textSize = graphics.MeasureString(gameOverText, myTextRenderer.MyFont);
            int x = (int)((Width - textSize.Width) / 2.0);
            int y = (int)((Height - textSize.Height) / 4.0);
            myTextRenderer.DrawText(graphics, gameOverText, Brushes.White, new Point(x, y));
        }

    }
}
