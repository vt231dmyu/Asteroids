using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace AsteroidsLibrary
{
    public class Asteroid : GameObject
    {
        private const double LargeRadius = 60;
        private const double MediumRadius = 35;
        private const double SmallRadius = 15;
        private const double MaxDestructionRadius = LargeRadius;
        private const double AngleCorrection = -(Math.PI / 2.0);

        private const int SlowSpeed = 3;

        private const int LargeFastSpeed = 4;
        private const int MediumFastSpeed = 5;
        private const int SmallFastSpeed = 6;

        private readonly List<int> AsteroidPointsValues = new List<int> { 100, 50, 20 };

        protected readonly List<Point> points = new List<Point>();
        private readonly List<List<Vector2>> asteroidShapes = new List<List<Vector2>>();
        private readonly List<Vector2> asteroidDestruction = new List<Vector2>();

        protected SIZE_OF_ASTEROID size;
        private int shape = -1;

        private double rotationAngle;
        private double destructionRadius;

        protected bool isDestroyed = false;
        protected bool isCollided = false;

        public static double LARGE_RADIUS => LargeRadius;
        public static double MEDIUM_RADIUS => MediumRadius;
        public static double SMALL_RADIUS => SmallRadius;
        public static double MAX_DESTRUCTION_RADIUS => MaxDestructionRadius;
        public static double ANGLE_CORRECTION => AngleCorrection;

        public static int SLOW_SPEED => SlowSpeed;
        public static int LARGE_FAST_SPEED => LargeFastSpeed;
        public static int MEDIUM_FAST_SPEED => MediumFastSpeed;
        public static int SMALL_FAST_SPEED => SmallFastSpeed;

        public List<int> ASTEROID_POINTS_VALUES => AsteroidPointsValues;

        public List<Point> Points => points;
        public List<List<Vector2>> AsteroidShapes => asteroidShapes;
        public List<Vector2> AsteroidDestruction => asteroidDestruction;

        public SIZE_OF_ASTEROID Size => size;
        public int Shape => shape;

        public double RotationAngle => rotationAngle;
        public double DestructionRadius
        {
            get => destructionRadius;
            set => destructionRadius = value;
        }

        public bool IsDestroyed
        {
            get => isDestroyed;
            set => isDestroyed = value;
        }
        public bool IsCollided
        {
            get => isCollided;
            set => isCollided = value;
        }

        public Asteroid(ICanvas canvas, SIZE_OF_ASTEROID asteroidSize, Point? objPosition = null) : base(canvas)
        {
            int curSpeed;
            size = asteroidSize;

            if (asteroidShapes.Count == 0 || asteroidDestruction.Count == 0)
            {
                AsteroidShapesPoints();
                AsteroidDestructionPoints();
            }

            if (size == SIZE_OF_ASTEROID.SMALL)
                curSpeed = SMALL_FAST_SPEED;
            else if (size == SIZE_OF_ASTEROID.MEDIUM)
                curSpeed = MEDIUM_FAST_SPEED;
            else
                curSpeed = LARGE_FAST_SPEED;

            if (objPosition == null)
                RandomPosition();
            else
                position = (Point)objPosition;

            Generate(size);
            velocity = rnd.Next(SLOW_SPEED, curSpeed);
            angle = rnd.Next(0, 361);
        }

        private void Generate(SIZE_OF_ASTEROID asteroidSize)
        {
            switch (asteroidSize)
            {
                case SIZE_OF_ASTEROID.LARGE:
                    radius = LARGE_RADIUS;
                    break;
                case SIZE_OF_ASTEROID.MEDIUM:
                    radius = MEDIUM_RADIUS;
                    break;
                case SIZE_OF_ASTEROID.SMALL:
                    radius = SMALL_RADIUS;
                    break;
            }

            rotationAngle = rnd.Next(0, 361);
            RandomShape();
        }

        public void RandomShape()
        {
            shape = rnd.Next(0, asteroidShapes.Count);
            foreach (var vec in asteroidShapes[shape])
            {
                double angleRad = (vec.Y + rotationAngle) * Math.PI / 180.0;
                int nX = (int)(Math.Cos(angleRad + ANGLE_CORRECTION) * vec.X * radius);
                int nY = (int)(Math.Sin(angleRad + ANGLE_CORRECTION) * vec.X * radius);
                points.Add(new Point(nX, nY));
            }
        }

        private void AsteroidShapesPoints()
        {
            List<Vector2> lCur = new List<Vector2>
            {
            new Vector2(0.7f, 13.0f),
            new Vector2(0.85f, 45.0f),
            new Vector2(0.6f, 90.0f),
            new Vector2(0.95f, 120.0f),
            new Vector2(0.85f, 160.0f),
            new Vector2(0.8f, 210.0f),
            new Vector2(1.0f, 240.0f),
            new Vector2(0.85f, 290.0f),
            new Vector2(0.9f, 330.0f)
            };
            asteroidShapes.Add(lCur);

            lCur = new List<Vector2>
            {
                new Vector2(0.9f, 20.0f),
                new Vector2(0.95f, 40.0f),
                new Vector2(0.6f, 90.0f),
                new Vector2(0.85f, 130.0f),
                new Vector2(1.0f, 150.0f),
                new Vector2(0.85f, 180.0f),
                new Vector2(0.9f, 220.0f),
                new Vector2(0.8f, 260.0f),
                new Vector2(0.95f, 300.0f),
                new Vector2(0.75f, 330.0f)
            };
            asteroidShapes.Add(lCur);

            lCur = new List<Vector2>
            {
                new Vector2(0.6f, 0.0f),
                new Vector2(0.9f, 40.0f),
                new Vector2(0.8f, 80.0f),
                new Vector2(0.85f, 120.0f),
                new Vector2(1.0f, 160.0f),
                new Vector2(0.84f, 200.0f),
                new Vector2(1.0f, 240.0f),
                new Vector2(0.88f, 280.0f),
                new Vector2(0.72f, 320.0f),
                new Vector2(0.66f, 340.0f)
            };
            asteroidShapes.Add(lCur);

            lCur = new List<Vector2>
            {
                new Vector2(0.87f, 0.0f),
                new Vector2(0.93f, 40.0f),
                new Vector2(0.85f, 60.0f),
                new Vector2(0.92f, 80.0f),
                new Vector2(0.75f, 120.0f),
                new Vector2(0.83f, 140.0f),
                new Vector2(0.91f, 160.0f),
                new Vector2(0.86f, 180.0f),
                new Vector2(0.95f, 200.0f),
                new Vector2(0.82f, 240.0f),
                new Vector2(0.99f, 260.0f),
                new Vector2(0.94f, 280.0f),
                new Vector2(0.89f, 320.0f),
                new Vector2(0.92f, 340.0f)
            };
            asteroidShapes.Add(lCur);

            lCur = new List<Vector2>
            {
                new Vector2(0.90f, 0.0f),
                new Vector2(0.84f, 40.0f),
                new Vector2(0.93f, 60.0f),
                new Vector2(0.88f, 70.0f),
                new Vector2(0.92f, 80.0f),
                new Vector2(0.78f, 100.0f),
                new Vector2(0.85f, 120.0f),
                new Vector2(0.96f, 160.0f),
                new Vector2(0.78f, 200.0f),
                new Vector2(0.86f, 240.0f),
                new Vector2(0.92f, 260.0f),
                new Vector2(0.94f, 280.0f),
                new Vector2(0.92f, 320.0f),
                new Vector2(0.84f, 340.0f)
            };
            asteroidShapes.Add(lCur);
        }

        private void AsteroidDestructionPoints()
        {
            asteroidDestruction.Add(new Vector2(1.0f, 313.0f));
            asteroidDestruction.Add(new Vector2(0.5f, 343.0f));
            asteroidDestruction.Add(new Vector2(0.7f, 210.0f));
            asteroidDestruction.Add(new Vector2(1.0f, 113.0f));
            asteroidDestruction.Add(new Vector2(0.8f, 136.0f));
            asteroidDestruction.Add(new Vector2(1.0f, 68.0f));
            asteroidDestruction.Add(new Vector2(0.6f, 118.0f));
            asteroidDestruction.Add(new Vector2(0.85f, 40.0f));
        }

        public override void Draw(Graphics graphics)
        {
            Pen penW = new Pen(Color.White);

            Point from, to;
            if (!isDestroyed)
            {
                for (int nPoint = 0; nPoint < points.Count; nPoint++)
                {
                    from = new Point(Position.X + points[nPoint].X, Position.Y + points[nPoint].Y);
                    if (nPoint < (points.Count - 1))
                        to = new Point(Position.X + points[nPoint + 1].X, Position.Y + points[nPoint + 1].Y);
                    else
                        to = new Point(Position.X + points[0].X, Position.Y + points[0].Y);
                    graphics.DrawLine(penW, from, to);

                }
            }
            else
            {
                Brush brushWhite = Brushes.White;
                foreach (var vec in asteroidDestruction)
                {
                    int nX = (int)(Math.Cos((vec.Y / 180.0) * Math.PI + ANGLE_CORRECTION) * vec.X * destructionRadius);
                    int nY = (int)(Math.Sin((vec.Y / 180.0) * Math.PI + ANGLE_CORRECTION) * vec.X * destructionRadius);
                    graphics.FillRectangle(brushWhite, position.X + nX, position.Y + nY, 2, 2);
                }
            }
        }

        public void Move()
        {
            position.X += (int)(Math.Cos(angle * Math.PI / 180.0) * velocity);
            position.Y += (int)(Math.Sin(angle * Math.PI / 180.0) * velocity);

            BeyondMapEdges();
        }

        public void RandomVelocity(Asteroid a)
        {
            double lowVelocity = Math.Max(a.Velocity - 4, SLOW_SPEED);
            double highVelocity;
            switch (size)
            {
                case SIZE_OF_ASTEROID.LARGE:
                    highVelocity = Math.Min(a.Velocity + 4, LARGE_FAST_SPEED);
                    break;
                case SIZE_OF_ASTEROID.MEDIUM:
                    highVelocity = Math.Min(a.Velocity + 4, MEDIUM_FAST_SPEED);
                    break;
                case SIZE_OF_ASTEROID.SMALL:
                    highVelocity = Math.Min(a.Velocity + 4, SMALL_FAST_SPEED);
                    break;
                default:
                    throw new InvalidOperationException("Невідомий розмір астероїда.");
            }

            velocity = rnd.Next((int)lowVelocity, (int)highVelocity + 1);
        }

        private void RandomPosition()
        {
            position.X = rnd.Next((int)LARGE_RADIUS, Screen.PrimaryScreen.Bounds.Width - (int)LARGE_RADIUS);
            position.Y = rnd.Next((int)LARGE_RADIUS, Screen.PrimaryScreen.Bounds.Height - (int)LARGE_RADIUS);
        }

        public virtual List<Asteroid> Split(ScorePanel scorePanel)
        {
            List<Asteroid> newAsteroids = new List<Asteroid>();

            if (Size != SIZE_OF_ASTEROID.SMALL)
            {
                int countSplit = (Size == SIZE_OF_ASTEROID.MEDIUM) ? 3 : 2;

                for (int i = 0; i < countSplit; i++)
                {
                    Asteroid newAsteroid = new Asteroid(canvas, Size - 1, Position);

                    bool isValid = false;
                    int attemptCount = 0;
                    while (!isValid && attemptCount < 100)
                    {
                        newAsteroid.Angle = Angle + rnd.Next(-20, 20);
                        newAsteroid.RandomVelocity(this);
                        newAsteroid.Points.Clear();
                        newAsteroid.RandomShape();

                        isValid = true;
                        for (int j = 0; j < newAsteroids.Count; j++)
                        {
                            if (newAsteroid.Velocity == newAsteroids[j].Velocity ||
                                newAsteroid.Angle == newAsteroids[j].Angle ||
                                newAsteroid.Shape == newAsteroids[j].Shape)
                            {
                                isValid = false;
                                break;
                            }
                        }

                        attemptCount++;
                    }

                    newAsteroids.Add(newAsteroid);
                }
            }
            return newAsteroids;
        }
    }
}
