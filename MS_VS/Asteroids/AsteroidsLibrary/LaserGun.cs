using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidsLibrary
{
    public class LaserGun : Laser
    {
        private List<Laser> lasersList = new List<Laser>();

        private long lastTick = 0;
        private long ticksSinceLastFire = 0;

        public List<Laser> LasersList
        {
            get => lasersList;
            set => lasersList = value;
        }

        public long LastTick
        {
            get => lastTick;
            set => lastTick = value;
        }

        public long TicksSinceLastFire
        {
            get => ticksSinceLastFire;
            set => ticksSinceLastFire = value;
        }

        public LaserGun(ICanvas canvas) : base(canvas)
        {
            lastTick = System.DateTime.Now.Ticks;
        }

        public override void Draw(Graphics graphics)
        {
            foreach (Laser l in lasersList)
            {
                l.Draw(graphics);
                l.Decay += System.DateTime.Now.Ticks - lastTick;
            }
            lastTick = System.DateTime.Now.Ticks;

            lasersList.RemoveAll(l => (l.Decay / 10000000) > DECAY_TIME);
        }

        public bool Fire(Point objPosition, double objAngle)
        {
            bool isFire = false;
            long curTicks = System.DateTime.Now.Ticks;

            if (lasersList.Count < 5 && ((curTicks - ticksSinceLastFire) > 1000000))
            {
                ticksSinceLastFire = curTicks;
                lasersList.Add(new Laser(canvas) { Decay = 0, Position = objPosition, Angle = objAngle });
                isFire = true;
            }
            return isFire;
        }
    }
}
