
namespace DBGames
{
    partial class BreakOut
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblYouWin2 = new System.Windows.Forms.Label();
            this.lblYouWin1 = new System.Windows.Forms.Label();
            this.lblGameOver2 = new System.Windows.Forms.Label();
            this.lblGameOver1 = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.lblYouWin2);
            this.panel1.Controls.Add(this.lblYouWin1);
            this.panel1.Controls.Add(this.lblGameOver2);
            this.panel1.Controls.Add(this.lblGameOver1);
            this.panel1.Location = new System.Drawing.Point(91, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 400);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblYouWin2
            // 
            this.lblYouWin2.AutoSize = true;
            this.lblYouWin2.BackColor = System.Drawing.Color.Green;
            this.lblYouWin2.Font = new System.Drawing.Font("Open Sans", 14F);
            this.lblYouWin2.ForeColor = System.Drawing.Color.White;
            this.lblYouWin2.Location = new System.Drawing.Point(92, 88);
            this.lblYouWin2.Name = "lblYouWin2";
            this.lblYouWin2.Size = new System.Drawing.Size(139, 26);
            this.lblYouWin2.TabIndex = 3;
            this.lblYouWin2.Text = "Press <space>";
            // 
            // lblYouWin1
            // 
            this.lblYouWin1.AutoSize = true;
            this.lblYouWin1.BackColor = System.Drawing.Color.Green;
            this.lblYouWin1.Font = new System.Drawing.Font("Open Sans", 30F);
            this.lblYouWin1.ForeColor = System.Drawing.Color.White;
            this.lblYouWin1.Location = new System.Drawing.Point(40, 33);
            this.lblYouWin1.Name = "lblYouWin1";
            this.lblYouWin1.Size = new System.Drawing.Size(256, 55);
            this.lblYouWin1.TabIndex = 2;
            this.lblYouWin1.Text = "GG! You Win";
            // 
            // lblGameOver2
            // 
            this.lblGameOver2.AutoSize = true;
            this.lblGameOver2.BackColor = System.Drawing.Color.Red;
            this.lblGameOver2.Font = new System.Drawing.Font("Open Sans", 14F);
            this.lblGameOver2.ForeColor = System.Drawing.Color.White;
            this.lblGameOver2.Location = new System.Drawing.Point(92, 193);
            this.lblGameOver2.Name = "lblGameOver2";
            this.lblGameOver2.Size = new System.Drawing.Size(139, 26);
            this.lblGameOver2.TabIndex = 1;
            this.lblGameOver2.Text = "Press <space>";
            // 
            // lblGameOver1
            // 
            this.lblGameOver1.AutoSize = true;
            this.lblGameOver1.BackColor = System.Drawing.Color.Red;
            this.lblGameOver1.Font = new System.Drawing.Font("Open Sans", 30F);
            this.lblGameOver1.ForeColor = System.Drawing.Color.White;
            this.lblGameOver1.Location = new System.Drawing.Point(50, 138);
            this.lblGameOver1.Name = "lblGameOver1";
            this.lblGameOver1.Size = new System.Drawing.Size(233, 55);
            this.lblGameOver1.TabIndex = 0;
            this.lblGameOver1.Text = "Game Over";
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 25;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Open Sans Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(458, 44);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(109, 28);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "BreakOut";
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Open Sans", 9F);
            this.lblInstructions.Location = new System.Drawing.Point(460, 89);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(149, 68);
            this.lblInstructions.TabIndex = 2;
            this.lblInstructions.Text = "How to play:\r\n<left-arrow> move left\r\n<right-arrow> move right\r\n<space> pause/pla" +
    "y/start";
            // 
            // BreakOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.panel1);
            this.Name = "BreakOut";
            this.Text = "BreakOut";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblGameOver1;
        private System.Windows.Forms.Label lblGameOver2;
        private System.Windows.Forms.Label lblYouWin1;
        private System.Windows.Forms.Label lblYouWin2;
    }
}

