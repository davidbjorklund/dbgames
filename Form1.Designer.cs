
namespace DBGames
{
    partial class Form1
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
            this.lblBreakOut = new System.Windows.Forms.Label();
            this.lblTetris = new System.Windows.Forms.Label();
            this.lblSnake = new System.Windows.Forms.Label();
            this.lblMineSweeper = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBreakOut
            // 
            this.lblBreakOut.AutoSize = true;
            this.lblBreakOut.BackColor = System.Drawing.Color.LawnGreen;
            this.lblBreakOut.Font = new System.Drawing.Font("Open Sans", 18F);
            this.lblBreakOut.Location = new System.Drawing.Point(67, 50);
            this.lblBreakOut.Name = "lblBreakOut";
            this.lblBreakOut.Padding = new System.Windows.Forms.Padding(40);
            this.lblBreakOut.Size = new System.Drawing.Size(202, 113);
            this.lblBreakOut.TabIndex = 0;
            this.lblBreakOut.Text = "BreakOut";
            this.lblBreakOut.Click += new System.EventHandler(this.lblBreakOut_Click);
            // 
            // lblTetris
            // 
            this.lblTetris.AutoSize = true;
            this.lblTetris.BackColor = System.Drawing.Color.Turquoise;
            this.lblTetris.Font = new System.Drawing.Font("Open Sans", 18F);
            this.lblTetris.Location = new System.Drawing.Point(289, 50);
            this.lblTetris.Name = "lblTetris";
            this.lblTetris.Padding = new System.Windows.Forms.Padding(40);
            this.lblTetris.Size = new System.Drawing.Size(156, 113);
            this.lblTetris.TabIndex = 1;
            this.lblTetris.Text = "Tetris";
            this.lblTetris.Click += new System.EventHandler(this.lblTetris_Click);
            // 
            // lblSnake
            // 
            this.lblSnake.AutoSize = true;
            this.lblSnake.BackColor = System.Drawing.Color.BlueViolet;
            this.lblSnake.Font = new System.Drawing.Font("Open Sans", 18F);
            this.lblSnake.Location = new System.Drawing.Point(466, 50);
            this.lblSnake.Name = "lblSnake";
            this.lblSnake.Padding = new System.Windows.Forms.Padding(40);
            this.lblSnake.Size = new System.Drawing.Size(162, 113);
            this.lblSnake.TabIndex = 2;
            this.lblSnake.Text = "Snake";
            this.lblSnake.Click += new System.EventHandler(this.lblSnake_Click);
            // 
            // lblMineSweeper
            // 
            this.lblMineSweeper.AutoSize = true;
            this.lblMineSweeper.BackColor = System.Drawing.Color.Pink;
            this.lblMineSweeper.Font = new System.Drawing.Font("Open Sans", 18F);
            this.lblMineSweeper.Location = new System.Drawing.Point(67, 184);
            this.lblMineSweeper.Name = "lblMineSweeper";
            this.lblMineSweeper.Padding = new System.Windows.Forms.Padding(40);
            this.lblMineSweeper.Size = new System.Drawing.Size(247, 113);
            this.lblMineSweeper.TabIndex = 3;
            this.lblMineSweeper.Text = "MineSweeper";
            this.lblMineSweeper.Click += new System.EventHandler(this.lblMineSweeper_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkMagenta;
            this.label1.Font = new System.Drawing.Font("Open Sans", 18F);
            this.label1.Location = new System.Drawing.Point(335, 184);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(40);
            this.label1.Size = new System.Drawing.Size(183, 113);
            this.label1.TabIndex = 4;
            this.label1.Text = "PacMan";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMineSweeper);
            this.Controls.Add(this.lblSnake);
            this.Controls.Add(this.lblTetris);
            this.Controls.Add(this.lblBreakOut);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBreakOut;
        private System.Windows.Forms.Label lblTetris;
        private System.Windows.Forms.Label lblSnake;
        private System.Windows.Forms.Label lblMineSweeper;
        private System.Windows.Forms.Label label1;
    }
}

