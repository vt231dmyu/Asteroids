using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsLibrary
{
    public class Laser : GameObject
    {
        private const double DecayTime = 0.6;
        private const double LaserSpeed = 20.0;

        private double decay = 0;
 
        public static double DECAY_TIME => DecayTime;
        public static double LASER_SPEED => LaserSpeed;
        public double Decay
        {
            get => decay;
            set => decay = value;
        }

        public Laser(ICanvas canvas) : base(canvas)
        {
            radius = 4;
        }

        public override void Draw(Graphics graphics) { }

    }
}
