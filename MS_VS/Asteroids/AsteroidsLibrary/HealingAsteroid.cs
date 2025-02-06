using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsLibrary
{
    public class HealingAsteroid : Asteroid
    {
        public HealingAsteroid(ICanvas canvas, SIZE_OF_ASTEROID asteroidSize, Point? objPosition = null) : base(canvas, asteroidSize, objPosition) { }

        public override void Draw(Graphics graphics)
        {
            Brush brush = new SolidBrush(Color.FromArgb(255, 203, 8, 8));

            base.Draw(graphics);

            if (!isDestroyed && size == SIZE_OF_ASTEROID.LARGE && radius == LARGE_RADIUS)
            {
                for (int nPoint = 0; nPoint < points.Count; nPoint++)
                {
                    graphics.FillRectangle(brush, Position.X - 20, Position.Y - 5, 40, 10);
                    graphics.FillRectangle(brush, Position.X - 5, Position.Y - 20, 10, 40);
                }
            }
        }

        public override List<Asteroid> Split(ScorePanel scorePanel)
        {
            if (scorePanel.Lives < ScorePanel.MAX_LIVES && !isCollided)
            {
                scorePanel.Lives++;
            }
            return base.Split(scorePanel);
        }
    }
}
