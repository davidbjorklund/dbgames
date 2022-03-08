﻿using System;
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
    public partial class PacMan : Form
    {
        Graphics g;
        Pen p;
        int[,] grid = new int[17, 58] {
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0},
            { 0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,1,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,2,0},
            { 0,3,0,0,0,2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,1,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,3,0},
            { 0,2,2,2,2,2,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,0},
            { 0,0,0,0,0,2,0,0,0,2,2,2,2,2,2,2,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,0},
            { 0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1,0,0,1,1,1,1,0,0,1,0,0,0,2,0,0,0,0,0,0,0,2,0,1,1,1,1},
            { 0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1,0,0,1,1,1,1,0,0,1,0,0,0,2,0,0,0,1,1,1,0,2,0,0,0,0,0},
            { 0,0,0,0,0,2,0,0,0,2,2,3,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1,0,0,1,1,1,1,0,0,1,0,0,0,2,0,0,0,0,0,0,0,2,1,1,1,1,1},
            { 0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,2,0,0,0,2,2,2,2,2,0,0,0,0,0},
            { 0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,2,0,0,0,0,0,0,0,2,0,1,1,1,1},
            { 0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,0},
            { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0},
            { 0,2,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,2,0,0,2,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,2,2,2,2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0},
            { 0,2,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,2,0,0,2,0,0,0,0,0,0,0,0,0,0,2,0,0,2,0,0,2,0,0,2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0},
            { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,2,2,0,0,2,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
        int x = 36;
        int y = 15;
        int dx = 0;
        int dy = 0;
        int nextdx = 0;
        int nextdy = 0;
        class Ghost
        {
            public int x;
            public int y;
            public Brush color;
            public Ghost(int ghostx, int ghosty, Brush ghostcolor)
            {
                x = ghostx;
                y = ghosty;
                color = ghostcolor;
            }
        }
        List<Ghost> ghostList = new List<Ghost>();
        public PacMan()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            ghostList.Add(new Ghost(36, 7, Brushes.Red));
            p = new Pen(Brushes.Blue, 2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private bool valid(int vx, int vy)
        {
            if (vx < 0 || vx >= 58 || vy < 0 || vy >= 17) return false;
            if (grid[vy, vx] == 0) return false;
            return true;
        }

        private bool isEmpty(int ex, int ey)
        {
            if (ex < 0 || ex >= 58 || ey < 0 || ey >= 17) return false;
            if (grid[ey, ex] == 0) return true;
            return false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g.FillRectangle(Brushes.Black, 0, 0, 16 * 58, 16 * 17);
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 58; j++)
                {
                    if (grid[i, j] == 0)
                    {
                        //g.FillRectangle(Brushes.Blue, new Rectangle(16 * j, 16 * i, 16, 16));
                        g.DrawEllipse(p, new Rectangle(16 * j + 1, 16 * i + 1, 16 - 2, 16 - 2));
                        if (isEmpty(j, i + 1))
                        {
                            g.FillRectangle(Brushes.Black, new Rectangle(16 * j, 16 * i + 8, 16, 8));
                            if (!isEmpty(j + 1, i)) g.FillRectangle(Brushes.Blue, new Rectangle(16 * j + 14, 16 * i + 8, 2, 8));
                            if (!isEmpty(j - 1, i)) g.FillRectangle(Brushes.Blue, new Rectangle(16 * j, 16 * i + 8, 2, 8));
                        }
                        if (isEmpty(j, i - 1))
                        {
                            g.FillRectangle(Brushes.Black, new Rectangle(16 * j, 16 * i, 16, 8));
                            if (!isEmpty(j + 1, i)) g.FillRectangle(Brushes.Blue, new Rectangle(16 * j + 14, 16 * i, 2, 8));
                            if (!isEmpty(j - 1, i)) g.FillRectangle(Brushes.Blue, new Rectangle(16 * j, 16 * i, 2, 8));
                        }
                        if (isEmpty(j + 1, i))
                        {
                            g.FillRectangle(Brushes.Black, new Rectangle(16 * j + 8, 16 * i, 8, 16));
                            if (!isEmpty(j, i + 1)) g.FillRectangle(Brushes.Blue, new Rectangle(16 * j + 8, 16 * i + 14, 8, 2));
                            if (!isEmpty(j, i - 1)) g.FillRectangle(Brushes.Blue, new Rectangle(16 * j + 8, 16 * i, 8, 2));
                        }
                        if (isEmpty(j - 1, i))
                        {
                            g.FillRectangle(Brushes.Black, new Rectangle(16 * j, 16 * i, 8, 16));
                            if (!isEmpty(j, i + 1)) g.FillRectangle(Brushes.Blue, new Rectangle(16 * j, 16 * i + 14, 8, 2));
                            if (!isEmpty(j, i - 1)) g.FillRectangle(Brushes.Blue, new Rectangle(16 * j, 16 * i, 8, 2));
                        }

                    }
                    else g.FillRectangle(Brushes.Black, new Rectangle(16 * j, 16 * i, 16, 16));

                    if (grid[i, j] == 2) g.FillRectangle(Brushes.Pink, new Rectangle(16 * j + 6, 16 * i + 6, 4, 4));
                    else if (grid[i, j] == 3) g.FillRectangle(Brushes.Pink, new Rectangle(16 * j + 3, 16 * i + 3, 10, 10));
                }
            }
            drawPac();
        }

        private void erasePac()
        {
            g.FillRectangle(Brushes.Black, new Rectangle(16 * x, 16 * y, 16, 16));
        }

        private void drawPac()
        {
            g.FillRectangle(Brushes.Yellow, new Rectangle(16 * x, 16 * y, 16, 16));
        }

        private void eraseGhost(int gx, int gy)
        {
            g.FillRectangle(Brushes.Black, new Rectangle(16 * gx, 16 * gy, 16, 16));
        }

        private void drawGhost(int gx, int gy, Brush gcolor)
        {
            g.FillRectangle(gcolor, new Rectangle(16 * gx, 16 * gy, 16, 16));
        }

        private void drawFood(int fx, int fy)
        {
            g.FillRectangle(Brushes.Pink, new Rectangle(16 * fx + 6, 16 * fy + 6, 4, 4));
        }

        private void drawBigFood(int fx, int fy)
        {
            g.FillRectangle(Brushes.Pink, new Rectangle(16 * fx + 3, 16 * fy + 3, 10, 10));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                nextdy = 0;
                nextdx = -1;
            }
            if (e.KeyCode == Keys.Right)
            {
                nextdy = 0;
                nextdx = 1;
            }
            if (e.KeyCode == Keys.Up)
            {
                nextdy = -1;
                nextdx = 0;
            }
            if (e.KeyCode == Keys.Down)
            {
                nextdy = 1;
                nextdx = 0;
            }
        }


        private void move()
        {
            if (!valid(x + dx, y + dy)) return;
            erasePac();
            x += dx;
            y += dy;
            drawPac();
        }

        private void updateD()
        {
            dx = nextdx;
            dy = nextdy;
            nextdx = 0;
            nextdy = 0;
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if ((nextdx != 0 || nextdy != 0) && valid(x + nextdx, y + nextdy)) updateD();
            move();
            foreach (Ghost ghoul in ghostList)
            {
                drawGhost(ghoul.x, ghoul.y, ghoul.color);
            }
        }
    }
}
