using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsteroidsLibrary;

namespace AsteroidsForm
{
    public partial class Game : Form
    {
        private GameEngine gameEngine;
        private DoubleBufferedPanel pauseOverlay;

        private string playerName;

        public Game()
        {
            InitializeComponent();

            Cursor.Hide();
            buttonExit.Visible = false;
            textBoxPlayerName.Visible = false;

            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Top = 0;
            Left = 0;

            timer.Interval = (int)(1000.0 / 60.0);

            gameEngine = new GameEngine(this.CreateGraphics());

            pauseOverlay = new DoubleBufferedPanel();
            pauseOverlay.BackColor = Color.FromArgb(200, 0, 0, 0);
            pauseOverlay.Dock = DockStyle.Fill;
            pauseOverlay.Visible = false;
            this.Controls.Add(pauseOverlay);

            buttonExit.Location = new Point((int)((Width - buttonExit.Width) / 2.0), (int)((Height - buttonExit.Height) / 2.0));
            textBoxPlayerName.Location = new Point((int)((Width - textBoxPlayerName.Width) / 2.0), (int)((Height - textBoxPlayerName.Height) / 2.0));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            gameEngine.Update();

            if (gameEngine.InputShow)
            {
                textBoxPlayerName.Visible = true;
                textBoxPlayerName.Focus();
            }

            Invalidate();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            Cursor.Position = PointToScreen(new Point(0, 0));
            MouseMove += Game_MouseMove;

            buttonExit.BackColor = Color.Transparent;
        }

        private void Game_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Position = PointToScreen(new Point(0, 0));
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            gameEngine.Draw(e.Graphics);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            gameEngine.KeyDown(e);
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            gameEngine.KeyUp(e, timer);

            if (!gameEngine.IsPaused)
            {
                pauseOverlay.Visible = false;
                buttonExit.Visible = false;
                this.Focus();
            }
            else if (!gameEngine.GameOver)
            {
                pauseOverlay.Visible = true;
                buttonExit.Visible = true;
                buttonExit.Focus();
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonExit_Enter(object sender, EventArgs e)
        {
            buttonExit.BackColor = Color.White;
            buttonExit.ForeColor = Color.Black;
        }

        private void buttonExit_Leave(object sender, EventArgs e)
        {
            buttonExit.BackColor = Color.Transparent;
            buttonExit.ForeColor = Color.White;
        }

        private void textBoxPlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (IsValidPlayerName(textBoxPlayerName.Text))
                {
                    textBoxPlayerName.Visible = false;

                    playerName = textBoxPlayerName.Text;

                    SaveScore(playerName, gameEngine.ScorePanel.Score, new TimeSpan(gameEngine.ScorePanel.Time), gameEngine.ScorePanel.Wave);


                    this.Close();
                }
                else
                {
                    MessageBox.Show("Будь ласка, введіть валідне ім'я. Ім'я може містити тільки українські та англійські літери, цифри, символи підкреслення (_) та дефіса (-).", "Некоректне ім'я", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        
        private bool IsValidPlayerName(string playerName)
        {
            string pattern = @"^[a-zA-Zа-яіїєА-ЯІЇЄ0-9_-]+$";
            return !string.IsNullOrEmpty(playerName) && Regex.IsMatch(playerName, pattern);
        }

        private void SaveScore(string playerName, long score, TimeSpan gameTime, int wave)
        {
            using (StreamWriter writer = new StreamWriter("leaderboard.txt", true))
            {
                writer.WriteLine($"{playerName},{score},{gameTime.ToString(@"hh\:mm\:ss")},{wave}");
            }
        }
    }
}
