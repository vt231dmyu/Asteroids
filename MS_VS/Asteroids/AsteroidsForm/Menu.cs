using AsteroidsLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidsForm
{
    public partial class Menu : Form
    {
        private Game game;
        private Leaderboard leaderboard;
        private MyTextRenderer myTextRenderer;

        public Menu()
        {
            InitializeComponent();

            Cursor.Hide();

            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Top = 0;
            Left = 0;

            myTextRenderer = new MyTextRenderer();

            buttonPlay.Location = new Point((int)((Width - buttonPlay.Width) / 2.0), (int)((Height - buttonPlay.Height) / 2.0 - buttonPlay.Height - 10));
            buttonLeaderboard.Location = new Point((int)((Width - buttonPlay.Width) / 2.0), (int)((Height - buttonPlay.Height) / 2.0));
            buttonExit.Location = new Point((int)((Width - buttonExit.Width) / 2.0), (int)((Height - buttonExit.Height) / 2.0 + buttonExit.Height + 10));
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            Cursor.Position = PointToScreen(new Point(0, 0));
            MouseMove += Menu_MouseMove;

            buttonPlay.BackColor = Color.Transparent;
            buttonLeaderboard.BackColor = Color.Transparent;
            buttonExit.BackColor = Color.Transparent;
        }

        private void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Position = PointToScreen(new Point(0, 0));
        }

        private void Menu_Paint(object sender, PaintEventArgs e)
        {
            DrawTitle(e.Graphics);
        }

        private void DrawTitle(Graphics graphics)
        {
            myTextRenderer.Size = 50f;
            string text = "ASTEROIDS";
            float letterSpacing = 2.5f;
            SizeF textSize = myTextRenderer.MeasureTextWithLetterSpacing(graphics, text, myTextRenderer.MyFont, letterSpacing);

            int x = (int)((Width - textSize.Width) / 2.0);
            int y = (int)((Height - textSize.Height) / 7.0);

            myTextRenderer.DrawTextWithLetterSpacing(graphics, text, Brushes.White, new Point(x, y), letterSpacing);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            game = new Game();
            game.ShowDialog();
        }

        private void buttonLeaderboard_Click(object sender, EventArgs e)
        {
            leaderboard = new Leaderboard();
            leaderboard.ShowDialog();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonPlay_Enter(object sender, EventArgs e)
        {
            btnEnter(buttonPlay);
        }

        private void buttonLeaderboard_Enter(object sender, EventArgs e)
        {
            btnEnter(buttonLeaderboard);
        }

        private void buttonExit_Enter(object sender, EventArgs e)
        {
            btnEnter(buttonExit);
        }

        private void buttonPlay_Leave(object sender, EventArgs e)
        {
            btnLeave(buttonPlay);
        }

        private void buttonLeaderboard_Leave(object sender, EventArgs e)
        {
            btnLeave(buttonLeaderboard);
        }

        private void buttonExit_Leave(object sender, EventArgs e)
        {
            btnLeave(buttonExit);
        }

        private void btnEnter(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
        }

        private void btnLeave(Button btn)
        {
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
        }
    }
}
