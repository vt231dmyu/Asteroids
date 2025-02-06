using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsLibrary
{
    public class ScorePanel
    {
        private const double MaxLives = 5;

        private readonly ICanvas canvas;
        private readonly MyTextRenderer myTextRenderer;
        private readonly List<Ship> livesList = new List<Ship>();

        private long score;
        private long time;
        private int lives;
        private int wave = 1;
        private byte alpha = 0;
        private bool isPlaying = false;
        private bool isGameOver = false;

        public static double MAX_LIVES => MaxLives;

        public long Score
        {
            get => score;
            set => score = value;
        }

        public long Time
        {
            get => time;
            set => time = value;
        }

        public int Lives
        {
            get => lives;
            set
            {
                if (lives != value)
                {
                    lives = value;
                    UpdateLivesList();
                }
            }
        }

        public int Wave
        {
            get => wave;
            set => wave = value;
        }

        public byte Alpha
        {
            get => alpha;
            set => alpha = value;
        }

        public bool IsPlaying
        {
            get => isPlaying;
            set => isPlaying = value;
        }

        public bool IsGameOver
        {
            get => isGameOver;
            set => isGameOver = value;
        }

        public ScorePanel(ICanvas canvas)
        {
            this.canvas = canvas;
            myTextRenderer = new MyTextRenderer();
        }

        private void UpdateLivesList()
        {
            livesList.Clear();
            for (int i = 0; i < lives; i++)
            {
                var ship = new Ship(canvas)
                {
                    Position = new Point((canvas.Width / 2) + 410 + (i * 28), 80)
                };
                livesList.Add(ship);
            }
        }

        public void Draw(Graphics graphics)
        {
            if (isPlaying)
            {
                DrawLives(graphics);
                DrawScore(graphics);
                DrawTime(graphics);
            }

            if (isPlaying && lives > 0)
            {
                DrawWave(graphics);
            }
            else if (isPlaying && lives == 0)
            {
                DrawGameOver(graphics);
            }

            if (isGameOver)
            {
                DrawWhoAreYou(graphics);
            }
        }

        private void DrawLives(Graphics graphics)
        {
            foreach (var ship in livesList)
            {
                ship.Draw(graphics);
            }
        }

        private void DrawScore(Graphics graphics)
        {
            myTextRenderer.Size = 30f;
            int x = (canvas.Width / 2) - 410 - 84;
            int y = 50;
            myTextRenderer.DrawText(graphics, score.ToString(), Brushes.White, new Point(x, y));
        }

        private void DrawTime(Graphics graphics)
        {
            TimeSpan elapsedTime = new TimeSpan(time);
            string timeString = elapsedTime.ToString(@"hh\:mm\:ss");
            SizeF textSize = graphics.MeasureString(timeString, myTextRenderer.MyFont);
            int x = (int)((canvas.Width - textSize.Width) / 2.0);
            int y = 50;
            myTextRenderer.DrawText(graphics, timeString, Brushes.White, new Point(x, y));
        }

        private void DrawWave(Graphics graphics)
        {
            myTextRenderer.Size = 50f;
            Color textColor = Color.FromArgb(alpha, 255, 255, 255);
            Brush textBrush = new SolidBrush(textColor);
            string waveText = $"Хвиля {wave}";
            SizeF textSize = graphics.MeasureString(waveText, myTextRenderer.MyFont);
            int x = (int)((canvas.Width - textSize.Width) / 2.0);
            int y = (int)((canvas.Height - textSize.Height) / 4.0);
            myTextRenderer.DrawText(graphics, waveText, textBrush, new Point(x, y));
        }

        private void DrawGameOver(Graphics graphics)
        {
            myTextRenderer.Size = 50f;
            string gameOverText = "КІНЕЦЬ ГРИ";
            SizeF textSize = graphics.MeasureString(gameOverText, myTextRenderer.MyFont);
            int x = (int)((canvas.Width - textSize.Width) / 2.0);
            int y = (int)((canvas.Height - textSize.Height) / 2.0);
            myTextRenderer.DrawText(graphics, gameOverText, Brushes.White, new Point(x, y));
        }

        private void DrawWhoAreYou(Graphics graphics)
        {
            myTextRenderer.Size = 70f;
            string gameOverMessage = "Хто ти воїн???";
            SizeF textSize = graphics.MeasureString(gameOverMessage, myTextRenderer.MyFont);
            int x = (int)((canvas.Width - textSize.Width) / 2.0);
            int y = (int)((canvas.Height - textSize.Height) / 2.0 - textSize.Height - 20);
            myTextRenderer.DrawText(graphics, gameOverMessage, Brushes.White, new Point(x, y));
        }

    }
}
