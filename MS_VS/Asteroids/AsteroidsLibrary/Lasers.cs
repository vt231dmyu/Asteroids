using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsLibrary
{
    public class Lasers : Laser
    {
        // Objects
        private readonly ICanvas canvas;
        private readonly Random rnd = new Random();

        private List<Laser> lasersList = new List<Laser>();

        // Variables
        private long lastTick = 0;
        private long ticksSinceLastFire = 0;

        // Properties
        public List<Laser> LasersList { set { lasersList = value; } get { return lasersList; } }

        public long LastTick { set { lastTick = value; } get { return lastTick; } }
        public long TicksSinceLastFire { set { ticksSinceLastFire = value; } get { return ticksSinceLastFire; } }

        public Lasers(ICanvas canvas)
        {
            this.canvas = canvas;
            lastTick = System.DateTime.Now.Ticks;
        }

        public void Draw(Graphics graphics)
        {
            foreach (Laser l in lasersList)
            {
                double sa = Math.Sin(l.Angle - (Math.PI / 2));
                double ca = Math.Cos(l.Angle - (Math.PI / 2));
                Brush brushLime = Brushes.Lime;
                Point position = new Point(l.Position.X + (int)(ca * LASER_SPEED), l.Position.Y + (int)(sa * LASER_SPEED));

                if (position.X < 0) 
                    position = new Point(canvas.Width + position.X, position.Y);
                else if (position.X > canvas.Width)
                    position = new Point(position.X - canvas.Width, position.Y);
                if (position.Y < 0)
                    position = new Point(position.X, canvas.Height + position.Y);
                else if (position.Y > canvas.Height)
                    position = new Point(position.X, position.Y - canvas.Height);

                // Малюємо лазер
                graphics.FillEllipse(brushLime, position.X - (int)RADIUS, position.Y, (int)RADIUS * 2, (int)RADIUS * 2);

                // Оновюємо інформацію
                l.Position = position;
                l.Decay += System.DateTime.Now.Ticks - lastTick;
            }
            lastTick = System.DateTime.Now.Ticks;

            // Видаляємо зруйновані лазери
            if (lasersList.Count > 0)
            {
                for (int nCur = lasersList.Count - 1; nCur >= 0; nCur--)
                {
                    if ((lasersList[nCur].Decay / 10000000) > DECAY_TIME)
                        lasersList.RemoveAt(nCur);
                }
            }
        }

        public bool Fire(Point objPosition, double objAngle)
        {
            bool isFire = false;
            long curTicks = System.DateTime.Now.Ticks;

            if (lasersList.Count < 5 && ((curTicks - ticksSinceLastFire) > 1000000))
            {
                ticksSinceLastFire = curTicks;
                lasersList.Add(new Laser() { Decay = 0, Position = objPosition, Angle = objAngle });
                isFire = true;
            }
            return isFire;
        }
    }
}
