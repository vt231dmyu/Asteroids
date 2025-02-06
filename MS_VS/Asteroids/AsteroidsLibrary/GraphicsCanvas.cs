using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsLibrary
{
    public class GraphicsCanvas : ICanvas
    {
        private readonly Graphics graphics;

        public GraphicsCanvas(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public int Width => (int)graphics.VisibleClipBounds.Width;
        public int Height => (int)graphics.VisibleClipBounds.Height;
    }
}
