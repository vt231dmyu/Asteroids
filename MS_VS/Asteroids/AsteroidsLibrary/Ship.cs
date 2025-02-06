using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidsLibrary
{
    public class Ship : GameObject
    {
        private const int DestructionAnimationDuration = 420000;
        private const double Acceleration = 0.01;
        private const double Deceleration = 0.0004;
        private const double MaxVelocity = 1.0;
        private const double RotationSpeed = (Math.PI / 90.0) * 2.4;

        private Keys keyPressed;

        private Pen penWhite = new Pen(Color.White);
        private Brush brushYellow = Brushes.Yellow;
        private Brush brushRed = Brushes.Red;

        private long destructionAnimationTickStart;
        private long[] hyperSpaceSETicks = new long[2];

        private double scalingFactor;
        private double accelerationAngle;
        private double rotationAngle;

        private bool isActive = false;
        private bool isDestroyed = false;
        private bool isRotating = false;
        private bool isAccelerating = false;
        private bool isHyperSpace = false;

        public static int DESTRUCTION_ANIMATION_DURATION => DestructionAnimationDuration;
        public static double ACCELERATION => Acceleration;
        public static double DECELERATION => Deceleration;
        public static double MAX_VELOCITY => MaxVelocity;
        public static double ROTATION_SPEED => RotationSpeed;

        public long DestructionAnimationTickStart
        {
            get => destructionAnimationTickStart;
            set => destructionAnimationTickStart = value;
        }
        public long[] HyperSpaceSETicks
        {
            get => hyperSpaceSETicks;
            set => hyperSpaceSETicks = value;
        }
        public double ScalingFactor
        {
            get => scalingFactor;
            set => scalingFactor = value;
        }
        public double AccelerationAngle
        {
            get => accelerationAngle;
            set => accelerationAngle = value;
        }
        public double RotationAngle
        {
            get => rotationAngle;
            set => rotationAngle = value;
        }
        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }
        public bool IsDestroyed
        {
            set
            {
                if (value)
                    destructionAnimationTickStart = DateTime.Now.Ticks;

                isDestroyed = value;
            }
            get => isDestroyed;
        }
        public bool IsRotating
        {
            get => isRotating;
            set => isRotating = value;
        }
        public bool IsAccelerating
        {
            get => isAccelerating;
            set => isAccelerating = value;
        }
        public bool IsHyperSpace
        {
            get => isHyperSpace;
            set => isHyperSpace = value;
        }

        public Ship(ICanvas canvas) : base(canvas)
        {
            radius = 10.0;
            scalingFactor = 1.0;
            ResetToCenter();
        }

        public void ResetToCenter()
        {
            position = new Point((int)((canvas.Width - radius) / 2), (int)((canvas.Height - radius) / 2));
            accelerationAngle = 0;
        }

        public override void Draw(Graphics graphics)
        {
            if (isHyperSpace && scalingFactor == 0)
                return;
            if (isRotating)
                UpdateRotation();

            if (!isDestroyed)
                DrawShip(graphics);
            else
                DrawDestroyedShip(graphics);
        }

        public void Rotate(Keys key, bool ifRotating)
        {
            keyPressed = key;
            isRotating = ifRotating;
        }

        private void UpdateRotation()
        {
            double rotation = ROTATION_SPEED * (keyPressed == Keys.Right ? 1 : -1);
            rotationAngle = accelerationAngle += rotation;
        }

        private void DrawShip(Graphics graphics)
        {
            DrawShipBody(graphics);

            if (isAccelerating)
            {
                DrawThrusterFlames(graphics);
            }

            UpdateVelocity();

            UpdatePosition();

            BeyondMapEdges();
        }

        private void DrawShipBody(Graphics graphics)
        {
            double sa = Math.Sin(accelerationAngle) * scalingFactor;
            double ca = Math.Cos(accelerationAngle) * scalingFactor;

            Point[] shipLines = new Point[]
            {
                new Point(position.X + (int)(sa * 17), position.Y - (int)(ca * 17)),
                new Point(position.X + (int)(ca * 10) - (int)(sa * 17), position.Y + (int)(ca * 17) + (int)(sa * 10)),

                new Point(position.X + (int)(sa * -10), position.Y - (int)(ca * -10)),
                new Point(position.X + (int)(ca * 10) - (int)(sa * 17), position.Y + (int)(ca * 17) + (int)(sa * 10)),

                new Point(position.X - (int)(ca * 10) - (int)(sa * 17), position.Y + (int)(ca * 17) - (int)(sa * 10)),
                new Point(position.X + (int)(sa * -10), position.Y - (int)(ca * -10)),

                new Point(position.X + (int)(sa * 17), position.Y - (int)(ca * 17)),
                new Point(position.X - (int)(ca * 10) - (int)(sa * 17), position.Y + (int)(ca * 17) - (int)(sa * 10))
            };

            for (int i = 0; i < shipLines.Length; i += 2)
            {
                graphics.DrawLine(penWhite, shipLines[i], shipLines[i + 1]);
            }
        }

        private void DrawThrusterFlames(Graphics graphics)
        {
            Point[] pointsRed = new Point[]
            {
                new Point(position.X + (int)(Math.Sin(accelerationAngle) * -12), position.Y - (int)(Math.Cos(accelerationAngle) * -12)),
                new Point(position.X - (int)(Math.Cos(accelerationAngle) * 4) - (int)(Math.Sin(accelerationAngle) * 14), position.Y + (int)(Math.Cos(accelerationAngle) * 14) - (int)(Math.Sin(accelerationAngle) * 4)),
                new Point(position.X - (int)(Math.Sin(accelerationAngle) * 28), position.Y + (int)(Math.Cos(accelerationAngle) * 28)),
                new Point(position.X + (int)(Math.Cos(accelerationAngle) * 4) - (int)(Math.Sin(accelerationAngle) * 14), position.Y + (int)(Math.Cos(accelerationAngle) * 14) + (int)(Math.Sin(accelerationAngle) * 4))
            };
            graphics.FillPolygon(brushRed, pointsRed);

            Point[] pointsYellow = new Point[]
            {
                new Point(position.X + (int)(Math.Sin(accelerationAngle) * -12), position.Y - (int)(Math.Cos(accelerationAngle) * -12)),
                new Point(position.X - (int)(Math.Cos(accelerationAngle) * 4) - (int)(Math.Sin(accelerationAngle) * 14), position.Y + (int)(Math.Cos(accelerationAngle) * 14) - (int)(Math.Sin(accelerationAngle) * 4)),
                new Point(position.X - (int)(Math.Sin(accelerationAngle) * 20), position.Y + (int)(Math.Cos(accelerationAngle) * 20)),
                new Point(position.X + (int)(Math.Cos(accelerationAngle) * 4) - (int)(Math.Sin(accelerationAngle) * 14), position.Y + (int)(Math.Cos(accelerationAngle) * 14) + (int)(Math.Sin(accelerationAngle) * 4))
            };
            graphics.FillPolygon(brushYellow, pointsYellow);
        }

        private void UpdateVelocity()
        {
            if (!isAccelerating)
            {
                velocity = Math.Max(velocity - DECELERATION, 0);
            }
            else
            {
                if (velocity == 0)
                {
                    velocity = ACCELERATION;
                    angle = accelerationAngle;
                }
                else if (velocity + ACCELERATION < MAX_VELOCITY && angle == accelerationAngle)
                {
                    velocity += ACCELERATION;
                }
                else
                {
                    double velocityX = velocity * Math.Cos(angle);
                    double velocityY = velocity * Math.Sin(angle);
                    double accelerationX = ACCELERATION * Math.Cos(accelerationAngle);
                    double accelerationY = ACCELERATION * Math.Sin(accelerationAngle);

                    double newVelocity = Math.Sqrt(Math.Pow(velocityX + accelerationX, 2) + Math.Pow(velocityY + accelerationY, 2));

                    if (newVelocity < MAX_VELOCITY)
                        velocity = newVelocity;

                    angle = Math.Atan2(velocityY + accelerationY, velocityX + accelerationX);
                }
            }
        }

        private void UpdatePosition()
        {
            double sa = Math.Sin(angle);
            double ca = Math.Cos(angle);

            position = new Point(position.X + (int)(sa * velocity * 16), position.Y - (int)(ca * velocity * 16));
        }

        private void DrawDestroyedShip(Graphics graphics)
        {
            int destructionOffsetTiming = (int)((DateTime.Now.Ticks - destructionAnimationTickStart) / DESTRUCTION_ANIMATION_DURATION);

            if (destructionOffsetTiming < 34)
                graphics.DrawLine(penWhite,
                                new Point(position.X - 2 + (int)(Math.Sin(Math.PI / 3) * destructionOffsetTiming),
                                          position.Y - 9 - (int)(Math.Cos(Math.PI / 3) * destructionOffsetTiming)),
                                new Point(position.X + 15 + (int)(Math.Sin(Math.PI / 3) * destructionOffsetTiming),
                                          position.Y - (int)(Math.Cos(Math.PI / 3) * destructionOffsetTiming)));

            if (destructionOffsetTiming < 22)
                graphics.DrawLine(penWhite,
                                    new Point(position.X - 7,
                                              position.Y - 9 - destructionOffsetTiming),
                                    new Point(position.X + 9,
                                              position.Y - 6 - destructionOffsetTiming));

            double sa = Math.Sin(Math.PI / 6);
            double ca = Math.Cos(Math.PI / 6);
            if (destructionOffsetTiming < 38)
                graphics.DrawLine(penWhite,
                                new Point(position.X + (int)(sa * destructionOffsetTiming),
                                          position.Y + 7 + (int)(ca * destructionOffsetTiming)),
                                new Point(position.X + 10 + (int)(sa * destructionOffsetTiming),
                                          position.Y + 4 + (int)(ca * destructionOffsetTiming)));

            sa = Math.Sin(Math.PI / 2);
            ca = Math.Cos(Math.PI / 2);
            if (destructionOffsetTiming < 36)
                graphics.DrawLine(penWhite,
                                new Point(position.X + 7 + (int)(sa * destructionOffsetTiming),
                                          position.Y + (int)(ca * destructionOffsetTiming)),
                                new Point(position.X + 15 + (int)(sa * destructionOffsetTiming),
                                          position.Y + 10 + (int)(ca * destructionOffsetTiming)));

            sa = Math.Sin(Math.PI / 4);
            ca = Math.Cos(Math.PI / 4);
            if (destructionOffsetTiming < 42)
                graphics.DrawLine(penWhite,
                                new Point(position.X - 3 - (int)(sa * destructionOffsetTiming),
                                          position.Y - 3 - (int)(ca * destructionOffsetTiming)),
                                new Point(position.X - 10 - (int)(sa * destructionOffsetTiming),
                                          position.Y + 9 - (int)(ca * destructionOffsetTiming)));

            sa = Math.Sin(Math.PI / 3);
            ca = Math.Cos(Math.PI / 3);
            if (destructionOffsetTiming < 14)
                graphics.DrawLine(penWhite,
                                new Point(position.X - 7 - (int)(sa * destructionOffsetTiming),
                                          position.Y + 2 + (int)(ca * destructionOffsetTiming)),
                                new Point(position.X - (int)(sa * destructionOffsetTiming),
                                          position.Y + 8 + (int)(ca * destructionOffsetTiming)));
        }

        public void Accelerate(bool ifAccelerating)
        {
            isAccelerating = ifAccelerating;
        }

        private void Scale(bool condition, double targetScale)
        {
            if (condition)
            {
                double initialScale = scalingFactor;

                int numSteps = 8;

                double scaleIncrement = (targetScale - initialScale) / numSteps;

                Timer scaleTimer = new Timer();
                scaleTimer.Interval = 16;
                scaleTimer.Tick += (sender, e) =>
                {
                    scalingFactor += scaleIncrement;

                    if (Math.Abs(scalingFactor - targetScale) < Math.Abs(scaleIncrement))
                    {
                        scaleTimer.Stop();
                        scalingFactor = targetScale;
                    }
                };

                scaleTimer.Start();
            }
        }

        public void InHyperSpace()
        {
            Scale(scalingFactor == 1.0, 0.0);
            hyperSpaceSETicks[1] = DateTime.Now.Ticks;

            if (hyperSpaceSETicks[0] <= hyperSpaceSETicks[1])
            {
                velocity = 0;
                position.X = rnd.Next(50, Screen.PrimaryScreen.Bounds.Width - 50);
                position.Y = rnd.Next(50, Screen.PrimaryScreen.Bounds.Height - 50);
                isHyperSpace = false;
                Scale(scalingFactor == 0.0, 1.0);
            }
        }
    }
}
