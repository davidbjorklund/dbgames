using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBGames
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void lblBreakOut_Click(object sender, EventArgs e)
        {
            Form breakout = new BreakOut();
            breakout.Show();
        }
        private void lblTetris_Click(object sender, EventArgs e)
        {
            Form tetris = new Tetris();
            tetris.Show();
        }

        private void lblSnake_Click(object sender, EventArgs e)
        {
            Form snake = new Snake();
            snake.Show();
        }

        private void lblMineSweeper_Click(object sender, EventArgs e)
        {
            Form mineSweeper = new MineSweeper();
            mineSweeper.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form pacman = new PacMan();
            pacman.Show();
        }
    }
}
