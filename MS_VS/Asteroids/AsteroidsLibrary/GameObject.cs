using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidsLibrary
{
    public abstract class GameObject
    {
        protected readonly ICanvas canvas;
        protected readonly Random rnd = new Random();
        protected Point position;

        protected double radius;
        protected double velocity;
        protected double angle;

        public Point Position { get => position; set => position = value; }
        public double Radius { get => radius; set => radius = value; }
        public double Velocity { get => velocity; set => velocity = value; }
        public double Angle { get => angle; set => angle = value; }

        public GameObject(ICanvas canvas)
        {
            this.canvas = canvas;
        }

        public abstract void Draw(Graphics graphics);
        public bool IsCollide(Point objPosition, double objRadius)
        {
            double check = Math.Sqrt(Math.Pow(Position.X - objPosition.X, 2) + Math.Pow(Position.Y - objPosition.Y, 2));
            if (check < radius + objRadius)
                return true;
            return false;
        }
        protected Point BeyondMapEdges()
        {
            if (position.X < 0)
                position = new Point(canvas.Width + position.X, position.Y);
            else if (position.X > canvas.Width)
                position = new Point(position.X - canvas.Width, position.Y);
            if (position.Y < 0)
                position = new Point(position.X, canvas.Height + position.Y);
            else if (position.Y > canvas.Height)
                position = new Point(position.X, position.Y - canvas.Height);
            return position;
        }
    }
}
