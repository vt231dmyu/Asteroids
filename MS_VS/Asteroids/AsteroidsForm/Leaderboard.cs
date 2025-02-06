using AsteroidsLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AsteroidsForm
{
    public partial class Leaderboard : Form
    {
        private MyTextRenderer myTextRenderer;
        private GameLeaderboard gameLeaderboard;

        public Leaderboard()
        {
            InitializeComponent();

            Cursor.Hide();

            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Top = 0;
            Left = 0;

            dataGridViewLeaderboard.Top = 127;
            dataGridViewLeaderboard.Left = 10;
            buttonBack.Top = 20;
            buttonBack.Left = 10;

            myTextRenderer = new MyTextRenderer();
            gameLeaderboard = new GameLeaderboard();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Leaderboard_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void buttonBack_Enter(object sender, EventArgs e)
        {
            buttonBack.BackColor = Color.White;
            buttonBack.ForeColor = Color.Black;
        }

        private void buttonBack_Leave(object sender, EventArgs e)
        {
            buttonBack.BackColor = Color.Transparent;
            buttonBack.ForeColor = Color.White;
        }

        private void Leaderboard_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            buttonBack.Focus();

            Cursor.Position = PointToScreen(new Point(0, 0));
            MouseMove += Leaderboard_MouseMove;

            dataGridViewLeaderboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLeaderboard.RowHeadersVisible = false;

            dataGridViewLeaderboard.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridViewLeaderboard.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridViewLeaderboard.DefaultCellStyle.BackColor = Color.Transparent;
            dataGridViewLeaderboard.DefaultCellStyle.ForeColor = Color.White;

            if (File.Exists("leaderboard.txt") && new FileInfo("leaderboard.txt").Length > 0)
            {
                gameLeaderboard.LoadEntries();

                dataGridViewLeaderboard.DataSource = gameLeaderboard.Entries.Take(15).ToList();

                dataGridViewLeaderboard.Columns["Number"].HeaderText = "№";
                dataGridViewLeaderboard.Columns["Name"].HeaderText = "Ім'я";
                dataGridViewLeaderboard.Columns["Score"].HeaderText = "К-сть очок";
                dataGridViewLeaderboard.Columns["Time"].HeaderText = "Час";
                dataGridViewLeaderboard.Columns["Wave"].HeaderText = "Хвиля";

                AdjustDataGridViewHeight();
            }
            else
            {
                MessageBox.Show("Файл таблиці лідерів не знайдено або він ще порожній.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void AdjustDataGridViewHeight()
        {
            int totalHeight = dataGridViewLeaderboard.ColumnHeadersHeight;
            int minRowHeight = 44;

            foreach (DataGridViewRow row in dataGridViewLeaderboard.Rows)
            {
                if (row.Visible)
                {
                    row.MinimumHeight = minRowHeight;
                    totalHeight += row.Height;
                }
            }

            dataGridViewLeaderboard.Height = totalHeight + 2;

            if (dataGridViewLeaderboard.Bottom > Height)
            {
                dataGridViewLeaderboard.Height = Height - dataGridViewLeaderboard.Top - 50;
            }

            if (totalHeight < Height - dataGridViewLeaderboard.Top - 50)
            {
                this.Height = totalHeight + dataGridViewLeaderboard.Top + 50;
            }
        }

        private void Leaderboard_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Position = PointToScreen(new Point(0, 0));
        }

        private void Leaderboard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            myTextRenderer.Size = 50f;
            SizeF textSize = g.MeasureString("Топ 15", myTextRenderer.MyFont);
            int x = (int)(this.Width - textSize.Width - 10);
            int y = 20;
            myTextRenderer.DrawText(g, "Топ 15", Brushes.White, new Point(x, y));
        }
    }
}
