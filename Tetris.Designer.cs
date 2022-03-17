
namespace DBGames
{
    partial class Tetris
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
            this.panel = new System.Windows.Forms.Panel();
            this.lblStart = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.panelNext = new System.Windows.Forms.Panel();
            this.lblNext = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblHold = new System.Windows.Forms.Label();
            this.panelHold = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel.Location = new System.Drawing.Point(208, 37);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(251, 401);
            this.panel.TabIndex = 0;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.BackColor = System.Drawing.Color.Lime;
            this.lblStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblStart.Location = new System.Drawing.Point(477, 37);
            this.lblStart.Name = "lblStart";
            this.lblStart.Padding = new System.Windows.Forms.Padding(42, 20, 42, 20);
            this.lblStart.Size = new System.Drawing.Size(130, 64);
            this.lblStart.TabIndex = 1;
            this.lblStart.Text = "Start";
            this.lblStart.Click += new System.EventHandler(this.lblStart_Click);
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 300;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // panelNext
            // 
            this.panelNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelNext.Location = new System.Drawing.Point(476, 141);
            this.panelNext.Name = "panelNext";
            this.panelNext.Size = new System.Drawing.Size(126, 126);
            this.panelNext.TabIndex = 2;
            this.panelNext.Paint += new System.Windows.Forms.PaintEventHandler(this.panelNext_Paint);
            // 
            // lblNext
            // 
            this.lblNext.AutoSize = true;
            this.lblNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNext.Location = new System.Drawing.Point(478, 119);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(40, 17);
            this.lblNext.TabIndex = 3;
            this.lblNext.Text = "Next:";
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblPoints.Location = new System.Drawing.Point(211, 6);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(23, 25);
            this.lblPoints.TabIndex = 4;
            this.lblPoints.Text = "0";
            this.lblPoints.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblHold
            // 
            this.lblHold.AutoSize = true;
            this.lblHold.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblHold.Location = new System.Drawing.Point(64, 15);
            this.lblHold.Name = "lblHold";
            this.lblHold.Size = new System.Drawing.Size(41, 17);
            this.lblHold.TabIndex = 5;
            this.lblHold.Text = "Hold:";
            // 
            // panelHold
            // 
            this.panelHold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelHold.Location = new System.Drawing.Point(62, 37);
            this.panelHold.Name = "panelHold";
            this.panelHold.Size = new System.Drawing.Size(126, 126);
            this.panelHold.TabIndex = 4;
            this.panelHold.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHold_Paint);
            // 
            // Tetris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblHold);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.panelHold);
            this.Controls.Add(this.lblNext);
            this.Controls.Add(this.panelNext);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.panel);
            this.Name = "Tetris";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tetris_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Panel panelNext;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblHold;
        private System.Windows.Forms.Panel panelHold;
    }
}

