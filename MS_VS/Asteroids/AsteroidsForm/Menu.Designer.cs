namespace AsteroidsForm
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonLeaderboard = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonExit
            // 
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Segoe UI Light", 30F);
            this.buttonExit.ForeColor = System.Drawing.Color.White;
            this.buttonExit.Location = new System.Drawing.Point(94, 284);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(453, 88);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Вийти";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.Enter += new System.EventHandler(this.buttonExit_Enter);
            this.buttonExit.Leave += new System.EventHandler(this.buttonExit_Leave);
            // 
            // buttonLeaderboard
            // 
            this.buttonLeaderboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLeaderboard.Font = new System.Drawing.Font("Segoe UI Light", 30F);
            this.buttonLeaderboard.ForeColor = System.Drawing.Color.White;
            this.buttonLeaderboard.Location = new System.Drawing.Point(94, 187);
            this.buttonLeaderboard.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.buttonLeaderboard.Name = "buttonLeaderboard";
            this.buttonLeaderboard.Size = new System.Drawing.Size(453, 88);
            this.buttonLeaderboard.TabIndex = 1;
            this.buttonLeaderboard.Text = "Таблиця лідерів";
            this.buttonLeaderboard.UseVisualStyleBackColor = true;
            this.buttonLeaderboard.Click += new System.EventHandler(this.buttonLeaderboard_Click);
            this.buttonLeaderboard.Enter += new System.EventHandler(this.buttonLeaderboard_Enter);
            this.buttonLeaderboard.Leave += new System.EventHandler(this.buttonLeaderboard_Leave);
            // 
            // buttonPlay
            // 
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Font = new System.Drawing.Font("Segoe UI Light", 30F);
            this.buttonPlay.ForeColor = System.Drawing.Color.White;
            this.buttonPlay.Location = new System.Drawing.Point(94, 90);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(453, 88);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "Грати";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            this.buttonPlay.Enter += new System.EventHandler(this.buttonPlay_Enter);
            this.buttonPlay.Leave += new System.EventHandler(this.buttonPlay_Leave);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::AsteroidsForm.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(645, 473);
            this.ControlBox = false;
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonLeaderboard);
            this.Controls.Add(this.buttonPlay);
            this.Font = new System.Drawing.Font("Segoe UI Light", 18F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Menu";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Menu_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Menu_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Menu_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonLeaderboard;
        private System.Windows.Forms.Button buttonPlay;
    }
}