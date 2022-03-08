
namespace DBGames
{
    partial class Snake
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gameArea = new System.Windows.Forms.Panel();
            this.clbInfo = new System.Windows.Forms.CheckedListBox();
            this.lblInfoH3 = new System.Windows.Forms.Label();
            this.lblInfoP = new System.Windows.Forms.Label();
            this.lblInfoH1 = new System.Windows.Forms.Label();
            this.pbInfo = new System.Windows.Forms.PictureBox();
            this.lblGameOver = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblPause = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.timerGame = new System.Windows.Forms.Timer(this.components);
            this.lblLevel = new System.Windows.Forms.Label();
            this.timerTime = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.pbBorder = new System.Windows.Forms.PictureBox();
            this.timerGameOver = new System.Windows.Forms.Timer(this.components);
            this.gameArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBorder)).BeginInit();
            this.SuspendLayout();
            // 
            // gameArea
            // 
            this.gameArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.gameArea.Controls.Add(this.clbInfo);
            this.gameArea.Controls.Add(this.lblInfoH3);
            this.gameArea.Controls.Add(this.lblInfoP);
            this.gameArea.Controls.Add(this.lblInfoH1);
            this.gameArea.Controls.Add(this.pbInfo);
            this.gameArea.Controls.Add(this.lblGameOver);
            this.gameArea.Location = new System.Drawing.Point(30, 30);
            this.gameArea.Margin = new System.Windows.Forms.Padding(0);
            this.gameArea.Name = "gameArea";
            this.gameArea.Size = new System.Drawing.Size(420, 420);
            this.gameArea.TabIndex = 0;
            this.gameArea.Paint += new System.Windows.Forms.PaintEventHandler(this.gameArea_Paint);
            // 
            // clbInfo
            // 
            this.clbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.clbInfo.FormattingEnabled = true;
            this.clbInfo.Items.AddRange(new object[] {
            "Gå genom väggar",
            "Booster matbitar",
            "Fler matbitar",
            "Matbitar försvinner",
            "Hinder finns",
            "Olikfärgad svans",
            "Bättre Grafik"});
            this.clbInfo.Location = new System.Drawing.Point(36, 186);
            this.clbInfo.Name = "clbInfo";
            this.clbInfo.Size = new System.Drawing.Size(178, 130);
            this.clbInfo.TabIndex = 9;
            this.clbInfo.TabStop = false;
            // 
            // lblInfoH3
            // 
            this.lblInfoH3.AutoSize = true;
            this.lblInfoH3.BackColor = System.Drawing.Color.White;
            this.lblInfoH3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblInfoH3.Location = new System.Drawing.Point(35, 157);
            this.lblInfoH3.Name = "lblInfoH3";
            this.lblInfoH3.Size = new System.Drawing.Size(116, 25);
            this.lblInfoH3.TabIndex = 8;
            this.lblInfoH3.Text = "Inställningar";
            // 
            // lblInfoP
            // 
            this.lblInfoP.AutoSize = true;
            this.lblInfoP.BackColor = System.Drawing.Color.White;
            this.lblInfoP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblInfoP.Location = new System.Drawing.Point(30, 70);
            this.lblInfoP.MaximumSize = new System.Drawing.Size(360, 0);
            this.lblInfoP.Name = "lblInfoP";
            this.lblInfoP.Size = new System.Drawing.Size(350, 72);
            this.lblInfoP.TabIndex = 7;
            this.lblInfoP.Text = resources.GetString("lblInfoP.Text");
            // 
            // lblInfoH1
            // 
            this.lblInfoH1.AutoSize = true;
            this.lblInfoH1.BackColor = System.Drawing.Color.White;
            this.lblInfoH1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblInfoH1.Location = new System.Drawing.Point(30, 30);
            this.lblInfoH1.Name = "lblInfoH1";
            this.lblInfoH1.Size = new System.Drawing.Size(91, 31);
            this.lblInfoH1.TabIndex = 6;
            this.lblInfoH1.Text = "Snake";
            // 
            // pbInfo
            // 
            this.pbInfo.BackColor = System.Drawing.Color.White;
            this.pbInfo.Location = new System.Drawing.Point(10, 10);
            this.pbInfo.Name = "pbInfo";
            this.pbInfo.Size = new System.Drawing.Size(400, 400);
            this.pbInfo.TabIndex = 5;
            this.pbInfo.TabStop = false;
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.BackColor = System.Drawing.Color.Red;
            this.lblGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F);
            this.lblGameOver.Location = new System.Drawing.Point(46, 166);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Padding = new System.Windows.Forms.Padding(15);
            this.lblGameOver.Size = new System.Drawing.Size(323, 93);
            this.lblGameOver.TabIndex = 4;
            this.lblGameOver.Text = "Game over";
            this.lblGameOver.Visible = false;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblStart.Location = new System.Drawing.Point(470, 40);
            this.lblStart.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblStart.Name = "lblStart";
            this.lblStart.Padding = new System.Windows.Forms.Padding(10);
            this.lblStart.Size = new System.Drawing.Size(141, 40);
            this.lblStart.TabIndex = 1;
            this.lblStart.Text = "Start new game";
            this.lblStart.Click += new System.EventHandler(this.lblStart_Click);
            // 
            // lblPause
            // 
            this.lblPause.AutoSize = true;
            this.lblPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblPause.Enabled = false;
            this.lblPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblPause.Location = new System.Drawing.Point(471, 90);
            this.lblPause.Margin = new System.Windows.Forms.Padding(3, 0, 3, 20);
            this.lblPause.Name = "lblPause";
            this.lblPause.Padding = new System.Windows.Forms.Padding(3);
            this.lblPause.Size = new System.Drawing.Size(97, 24);
            this.lblPause.TabIndex = 2;
            this.lblPause.Text = "Pause game";
            this.lblPause.Click += new System.EventHandler(this.lblPause_Click);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblScore.Location = new System.Drawing.Point(471, 134);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(64, 18);
            this.lblScore.TabIndex = 3;
            this.lblScore.Text = "Score: 0";
            // 
            // timerGame
            // 
            this.timerGame.Interval = 500;
            this.timerGame.Tick += new System.EventHandler(this.timerGame_Tick);
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblLevel.Location = new System.Drawing.Point(471, 168);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(58, 18);
            this.lblLevel.TabIndex = 5;
            this.lblLevel.Text = "Level: 0";
            // 
            // timerTime
            // 
            this.timerTime.Interval = 1000;
            this.timerTime.Tick += new System.EventHandler(this.timerTime_Tick);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblTime.Location = new System.Drawing.Point(410, 5);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(36, 17);
            this.lblTime.TabIndex = 6;
            this.lblTime.Text = "0:00";
            // 
            // pbBorder
            // 
            this.pbBorder.BackColor = System.Drawing.Color.Transparent;
            this.pbBorder.Location = new System.Drawing.Point(20, 20);
            this.pbBorder.Name = "pbBorder";
            this.pbBorder.Size = new System.Drawing.Size(440, 440);
            this.pbBorder.TabIndex = 7;
            this.pbBorder.TabStop = false;
            // 
            // timerGameOver
            // 
            this.timerGameOver.Interval = 500;
            this.timerGameOver.Tick += new System.EventHandler(this.timerGameOver_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 481);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblPause);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.gameArea);
            this.Controls.Add(this.pbBorder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.gameArea.ResumeLayout(false);
            this.gameArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBorder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel gameArea;
        private System.Windows.Forms.Label lblGameOver;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblPause;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Timer timerGame;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Timer timerTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox pbBorder;
        private System.Windows.Forms.Timer timerGameOver;
        private System.Windows.Forms.PictureBox pbInfo;
        private System.Windows.Forms.Label lblInfoH1;
        private System.Windows.Forms.Label lblInfoP;
        private System.Windows.Forms.Label lblInfoH3;
        private System.Windows.Forms.CheckedListBox clbInfo;
    }
}

