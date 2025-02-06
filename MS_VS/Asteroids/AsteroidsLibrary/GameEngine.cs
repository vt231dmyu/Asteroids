using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace AsteroidsLibrary
{
    public class GameEngine
    {
        private const long TicksPerSecond = 10000000;
        private const long DelayBetweenLevels = 3;
        private const long GameOverDelay = 6;
        private const int DistanceFromInstantCollision = 250;

        private Random rnd = new Random();
        private GraphicsCanvas graphicsCanvas;

        private Ship player;
        private LaserGun laserGun;
        private List<Asteroid> asteroids = new List<Asteroid>();
        private ScorePanel scorePanel;


        private int curLevel;

        private long ticksSinceStart;
        private Timer gameTimer;
        private DateTime startTime;

        private bool isPaused = false;
        private long pausedTicks = 0;
        private long elapsedTicks = 0;

        private long[] playerDestructionSETicks = new long[2];
        private long resurrectionDelay = 2;
        private long[] gameOverSETicks = new long[2];

        private bool inputShow = false;

        private bool gameOver = false;
        private bool curLevelTransition = false;
        private bool isSpacePressed = false;
        private bool instantASCollisionDetected = false;

        public static long TICKS_PER_SECOND => TicksPerSecond;
        public static long DELAY_BETWEEN_LEVELS => DelayBetweenLevels;
        public static long GAME_OVER_DELAY => GameOverDelay;
        public static int DISTANCE_FROM_INSTANT_COLLISION => DistanceFromInstantCollision;

        public ScorePanel ScorePanel
        {
            get => scorePanel;
            set => scorePanel = value;
        }
        public bool GameOver
        {
            get => gameOver;
            set => gameOver = value;
        }
        public bool IsPaused
        {
            get => isPaused;
            set => isPaused = value;
        }
        public bool InputShow
        {
            get => inputShow;
            set => inputShow = value;
        }

        public GameEngine(Graphics graphics)
        {
            ticksSinceStart = System.DateTime.Now.Ticks;

            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            startTime = new DateTime(ticksSinceStart);
            gameTimer.Start();

            graphicsCanvas = new GraphicsCanvas(graphics);
            player = new Ship(graphicsCanvas);
            laserGun = new LaserGun(graphicsCanvas);
            scorePanel = new ScorePanel(graphicsCanvas);

            scorePanel.IsPlaying = true;
            scorePanel.Lives = 3;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                TimeSpan elapsedTime;
                if (pausedTicks == 0)
                {
                    elapsedTime = new TimeSpan(DateTime.Now.Ticks - (startTime.Ticks + elapsedTicks));
                }
                else
                {
                    elapsedTime = new TimeSpan(pausedTicks - (startTime.Ticks + elapsedTicks));
                }
                scorePanel.Time = elapsedTime.Ticks;
            }
        }

        public void Update()
        {
            if (player.IsHyperSpace)
            {
                player.InHyperSpace();
            }

            long curTicks = System.DateTime.Now.Ticks;
            if (!gameOver)
            {
                if (!player.IsActive)
                {
                    player.IsActive = true;
                    ticksSinceStart = System.DateTime.Now.Ticks;
                    curLevel = 1;
                    curLevelTransition = true;

                    scorePanel.Alpha = 255;
                }
                else if (asteroids.Count == 0 && player.IsActive && !curLevelTransition)
                {
                    ticksSinceStart = System.DateTime.Now.Ticks;
                    curLevel += 1;
                    curLevelTransition = true;

                    scorePanel.Alpha = 255;
                }
                else if (curLevelTransition && ((curTicks - ticksSinceStart) > DELAY_BETWEEN_LEVELS * TICKS_PER_SECOND))
                {
                    scorePanel.Alpha = 0;

                    LoadAsteroids();
                    curLevelTransition = false;
                }
                scorePanel.Wave = curLevel;

                if (!player.IsDestroyed)
                {
                    foreach (Asteroid a in asteroids)
                    {
                        if (player.IsCollide(a.Position, a.Radius))
                        {
                            a.IsCollided = true;
                            PlayerDestructionSequence();
                            break;
                        }
                    }
                }
                else
                {
                    playerDestructionSETicks[1] = DateTime.Now.Ticks;
                    if ((playerDestructionSETicks[1] - playerDestructionSETicks[0]) > (resurrectionDelay * TICKS_PER_SECOND))
                    {
                        player.ResetToCenter();
                        instantASCollisionDetected = false;

                        foreach (Asteroid a in asteroids)
                        {
                            if (a.IsCollide(player.Position, player.Radius))
                            {
                                instantASCollisionDetected = true;
                                resurrectionDelay += 2;
                                break;
                            }
                        }

                        if (!instantASCollisionDetected)
                        {
                            player.Velocity = 0;
                            player.RotationAngle = 0;
                            player.IsDestroyed = false;

                            resurrectionDelay = 2;
                        }
                        else
                        {
                            playerDestructionSETicks[0] = DateTime.Now.Ticks;
                        }
                    }
                }
            }
            else
            {
                gameOverSETicks[1] = DateTime.Now.Ticks;
                if ((gameOverSETicks[1] - gameOverSETicks[0]) > (GAME_OVER_DELAY * TICKS_PER_SECOND))
                {
                    scorePanel.IsPlaying = false;
                    scorePanel.IsGameOver = true;
                    inputShow = true;
                }
            }
        }

        private void LoadAsteroids()
        {
            int count = 0;
            switch (curLevel)
            {
                case 1:
                    count = 2;
                    break;
                case 2:
                    count = 4;
                    break;
                case 3:
                    count = 6;
                    break;
                default:
                    count = rnd.Next(6, 21);
                    break;
            }
            for (int i = 0; i < count; i++)
            {
            tryAgain:
                Asteroid newAsteroid = new Asteroid(graphicsCanvas, SIZE_OF_ASTEROID.LARGE);
                if (curLevel > 3)
                {
                    int astType = rnd.Next(0, 4);
                    switch (astType)
                    {
                        case 0:
                            newAsteroid = new Asteroid(graphicsCanvas, SIZE_OF_ASTEROID.SMALL);
                            break;
                        case 1:
                            newAsteroid = new Asteroid(graphicsCanvas, SIZE_OF_ASTEROID.MEDIUM);
                            break;
                        case 2:
                            newAsteroid = new Asteroid(graphicsCanvas, SIZE_OF_ASTEROID.LARGE);
                            break;
                        case 3:
                            newAsteroid = new HealingAsteroid(graphicsCanvas, SIZE_OF_ASTEROID.LARGE);
                            break;
                        default:
                            break;
                    }
                }
                if (player.IsCollide(newAsteroid.Position, newAsteroid.Radius + DISTANCE_FROM_INSTANT_COLLISION))
                    goto tryAgain;
                foreach (Asteroid a in asteroids)
                {
                    if (newAsteroid.IsCollide(a.Position, a.Radius))
                        goto tryAgain;
                }

                asteroids.Add(newAsteroid);
            }
        }

        private void PlayerDestructionSequence()
        {
            player.IsDestroyed = true;

            scorePanel.Lives -= 1;
            if (scorePanel.Lives == 0)
            {
                gameOver = true;
                gameTimer.Stop();
                gameOverSETicks[0] = DateTime.Now.Ticks;
            }

            playerDestructionSETicks[0] = DateTime.Now.Ticks;
        }

        public void Draw(Graphics graphics)
        {
            List<Laser> listOfLasers = laserGun.LasersList;

            for (int aCur = asteroids.Count - 1; aCur >= 0; aCur--)
            {
                Asteroid a = asteroids[aCur];
                if (!a.IsDestroyed)
                {
                    if (listOfLasers.Count > 0)
                    {
                        for (int nCur = listOfLasers.Count - 1; nCur >= 0; nCur--)
                        {
                            if (a.IsCollide(listOfLasers[nCur].Position, laserGun.Radius) && !player.IsDestroyed)
                            {
                                a.IsDestroyed = true;
                                scorePanel.Score += a.ASTEROID_POINTS_VALUES[(int)a.Size];
                                asteroids.AddRange(a.Split(scorePanel));
                                listOfLasers.RemoveAt(nCur);
                            }
                        }
                    }

                    if (a.IsCollided)
                    {
                        a.IsDestroyed = true;
                        asteroids.AddRange(a.Split(scorePanel));
                    }

                    a.Draw(graphics);
                    a.Move();
                }
                else if (a.DestructionRadius < Asteroid.MAX_DESTRUCTION_RADIUS)
                {
                    a.DestructionRadius += 2;
                    a.Draw(graphics);
                }
                else
                    asteroids.RemoveAt(aCur);
            }

            if (player.IsActive)
            {
                player.Draw(graphics);
                laserGun.Draw(graphics);
            }

            scorePanel.Draw(graphics);
        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (player.IsActive)
                    {
                        player.Accelerate(true);
                    }
                    break;
                case Keys.Left:
                case Keys.Right:
                    if (player.IsActive)
                    {
                        player.Rotate(e.KeyCode, true);
                    }
                    break;
                case Keys.Down:
                    if (player.IsActive)
                    {
                        player.HyperSpaceSETicks[0] = System.DateTime.Now.Ticks + (rnd.Next(5, 11) * 1000000);
                        player.IsHyperSpace = true;
                    }
                    break;
                case Keys.Space:
                    if (player.IsActive && !player.IsDestroyed && player.ScalingFactor == 1.0)
                    {
                        if (!isSpacePressed)
                        {
                            if (laserGun.Fire(player.Position, player.RotationAngle))
                            {
                                isSpacePressed = true;
                            }
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        public void KeyUp(KeyEventArgs e, Timer timer)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (player.IsActive)
                    {
                        player.Accelerate(false);
                    }
                    break;
                case Keys.Left:
                case Keys.Right:
                    if (player.IsActive)
                    {
                        player.Rotate(e.KeyCode, false);
                    }
                    break;
                case Keys.Escape:
                    {
                        GamePaused(timer);
                    }
                    break;
                case Keys.Space:
                    if (isSpacePressed)
                    {
                        isSpacePressed = false;
                    }
                    break;

                default:
                    break;
            }
        }

        public void GamePaused(Timer timer)
        {
            if (!gameOver)
            {
                if (isPaused)
                {
                    elapsedTicks += DateTime.Now.Ticks - pausedTicks;
                    pausedTicks = 0;
                }
                else
                {
                    pausedTicks = DateTime.Now.Ticks;
                }
                isPaused = !isPaused;
                timer.Enabled = !timer.Enabled;
            }
        }
    }
}
