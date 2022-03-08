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
    public partial class BreakOut : Form
    {
        Graphics panelGraphics;

        bool isGameOver = false;

        static int width = 325;
        static int height = 400;
        static int size = 325 / 13;
        static Brush[] brushes = new Brush[8] { Brushes.Blue, Brushes.Orange, Brushes.Orange, Brushes.Orange, Brushes.Orange, Brushes.Green, Brushes.Green, Brushes.Green };
        int x = 325 / 2;
        int y = 360;
        int dx = 3;
        int dy = -3;
        int ballsize = 5;
        Rectangle ball;
        Rectangle hideBall;
        int playerx = 325 / 2;
        static int playery = 380;
        static Brush playercolor = Brushes.Blue;
        Rectangle hideplayerrect;
        Rectangle playerrect;
        public class Block
        {
            public int bx;
            public int by;
            public Brush bcolor;
            public Rectangle rect;
            public Block(int row, int col)
            {
                bx = col * size + 1;
                by = row * (size - 10) + 50;
                bcolor = brushes[row];
                rect = new Rectangle(bx, by, size - 2, size / 2 - 1);
            }
        }
        List<Block> blockList = new List<Block>();
        public BreakOut()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Block b in blockList)
            {
                panelGraphics.FillRectangle(b.bcolor, b.rect);
            }
            panelGraphics.FillRectangle(Brushes.Yellow, ball);
            panelGraphics.FillRectangle(playercolor, playerrect);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadGame();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            hideBallMethod();
            x += dx;
            y += dy;
            ball.X = x;
            ball.Y = y;
            hideBall.X = x;
            hideBall.Y = y;

            if (x < 0 || x >= width - ballsize) dx = -dx;
            if (y < 0) dy = -dy;
            if (y > height) gameOver();

            checkCollision();

            drawBallMethod();
            showPlayerRectMethod();
            if (blockList.Count == 0)
            {
                youWin();
            }
        }

        private void loadGame()
        {
            isGameOver = false;
            hideYouWin();
            hideGameOver();
            x = 325 / 2;
            y = 360;
            dx = 3;
            dy = -3;
            ballsize = 5;
            playerx = 325 / 2;
            blockList.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    blockList.Add(new Block(i, j));
                }
            }
            ball = new Rectangle(x, y, ballsize, ballsize);
            hideBall = new Rectangle(x, y, ballsize, ballsize);
            playerrect = new Rectangle(playerx, playery, size * 2 - 15, size / 2 - 1);
            hideplayerrect = new Rectangle(playerx, playery, size * 2 - 15, size / 2 - 1);
            panelGraphics = panel1.CreateGraphics();
            gameTimer.Start();
            panel1.Invalidate();
        }

        private void hideYouWin()
        {
            lblYouWin1.Visible = false;
            lblYouWin2.Visible = false;
        }
        private void hideGameOver()
        {
            lblGameOver1.Visible = false;
            lblGameOver2.Visible = false;
        }
        private void showYouWin()
        {
            lblYouWin1.Visible = true;
            lblYouWin2.Visible = true;
        }
        private void showGameOver()
        {
            lblGameOver1.Visible = true;
            lblGameOver2.Visible = true;
        }

        private void gameOver()
        {
            gameTimer.Stop();
            showGameOver();
            isGameOver = true;
        }

        private void youWin()
        {
            gameTimer.Stop();
            showYouWin();
            isGameOver = true;
        }

        private void hideBallMethod()
        {
            panelGraphics.FillRectangle(Brushes.Black, hideBall);
        }

        private void drawBallMethod()
        {
            panelGraphics.FillRectangle(Brushes.Yellow, ball);
        }

        private void checkCollision()
        {
            for (int i = blockList.Count - 1; i >= 0; i--)
            {
                if (ball.IntersectsWith(blockList[i].rect))
                {
                    if (blockList[i].by > y || blockList[i].by + size / 2 - 4 < y) dy = -dy;
                    else dx = -dx;
                    blockList.RemoveAt(i);
                    panel1.Invalidate();
                    return;
                }
            }
            if (ball.IntersectsWith(playerrect))
            {
                dy = -dy;
            }
        }

        private void movePlayer(int direction)
        {
            hidePlayerRectMethod();
            playerx += direction;
            playerrect.X = playerx;
            hideplayerrect.X = playerx;
            showPlayerRectMethod();
        }

        private void hidePlayerRectMethod()
        {
            panelGraphics.FillRectangle(Brushes.Black, hideplayerrect);
        }
        private void showPlayerRectMethod()
        {
            panelGraphics.FillRectangle(playercolor, hideplayerrect);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) movePlayer(-7);
            if (e.KeyCode == Keys.Right) movePlayer(7);
            if (e.KeyCode == Keys.Space)
            {
                if (gameTimer.Enabled) gameTimer.Stop();
                else
                {
                    if (isGameOver) loadGame();
                    else gameTimer.Start();
                }
                ;
            }
        }
    }
}
