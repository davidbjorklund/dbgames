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
    public partial class MineSweeper : Form
    {
        int width = 400;
        int height = 400;
        int size = 20;
        char[,] grid;
        bool[,] visible;
        bool[,] marked;
        bool spawnedBombs = false;
        Graphics panelGraphics;
        Random chance = new Random();

        public MineSweeper()
        {
            InitializeComponent();
        }

        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            bool left = e.Button == MouseButtons.Left ? true : false;
            int cx = (int)Math.Floor(e.Location.X / Convert.ToDecimal(size));
            int cy = (int)Math.Floor(e.Location.Y / Convert.ToDecimal(size));
            if (!spawnedBombs) createBombs(cx, cy);
            spawnedBombs = true;
            if (left) openSquare(cx, cy);
            else paintFlag(cx, cy);
            //panel.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grid = new char[size, size];
            visible = new bool[size, size];
            marked = new bool[size, size];
            panelGraphics = panel.CreateGraphics();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = '0';
                    visible[i, j] = false;
                    marked[i, j] = false;
                }
            }
            paintBack();
            //panel.Invalidate();
        }

        private void createBombs(int sx = 0, int sy = 0)
        {
            for (int i = 0; i < size * 4; i++)
            {
                createBomb(sx, sy);
            }
        }

        private void createBomb(int sx = 0, int sy = 0)
        {
            int bx;
            int by;
            bool bombAlreadyExists;
            do
            {
                bombAlreadyExists = false;
                bx = chance.Next(0, size);
                by = chance.Next(0, size);
                if (grid[bx, by] == 'x') bombAlreadyExists = true;
                if (Math.Abs(sx - bx) <= 2 && Math.Abs(sy - by) <= 2)
                {
                    bombAlreadyExists = true;
                }
            } while (bombAlreadyExists);
            grid[bx, by] = 'x';
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    addNumberTo(bx + i, by + j);
                }
            }
        }

        private void paintBack()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    panelGraphics.FillRectangle(Brushes.Gray, new Rectangle(i * size, j * size, size - 1, size - 1));
                }
            }
        }

        private void paintBomb(int pbx, int pby)
        {
            panelGraphics.FillRectangle(Brushes.Red, new Rectangle(pbx * size, pby * size, size - 1, size - 1));
        }

        private void paintSquare(int psx, int psy)
        {
            panelGraphics.FillRectangle(Brushes.Aqua, new Rectangle(psx * size, psy * size, size - 1, size - 1));
            if (grid[psx, psy] != '0') panelGraphics.DrawString(Convert.ToString(grid[psx, psy]), new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Brushes.Black, new Point(psx * size, psy * size));
        }

        private void paintFlag(int pfx, int pfy)
        {
            if (visible[pfx, pfy]) return;
            panelGraphics.FillRectangle(Brushes.Gray, new Rectangle(pfx * size, pfy * size, size - 1, size - 1));
            panelGraphics.DrawString("F", new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Brushes.Red, new Point(pfx * size + 3, pfy * size + 1));
        }

        private void openSquare(int ox, int oy)
        {
            if (ox < 0 || ox >= size || oy < 0 || oy >= size || visible[ox, oy]) return;
            visible[ox, oy] = true;
            if (grid[ox, oy] == 'x') { paintBomb(ox, oy); return; }
            if (grid[ox, oy] == '0')
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        openSquare(ox + i, oy + j);
                    }
                }
            }
            paintSquare(ox, oy);
        }

        private void markSquare(int mx, int my)
        {
            if (mx < 0 || mx >= size || my < 0 || my >= size || visible[mx, my]) return;
            marked[mx, my] = true;
        }

        private void addNumberTo(int ax, int ay)
        {
            if (ax < 0 || ax >= size || ay < 0 || ay >= size || grid[ax, ay] == 'x') return;
            grid[ax, ay] = Convert.ToChar(Convert.ToInt32(grid[ax, ay]) + 1);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!visible[i, j])
                    {
                        if (marked[i, j])
                        {
                            panelGraphics.FillRectangle(Brushes.Gray, new Rectangle(i * size, j * size, size - 1, size - 1));
                            panelGraphics.DrawString("F", new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Brushes.Red, new Point(i * size + 3, j * size + 1));
                        }
                        else panelGraphics.FillRectangle(Brushes.Gray, new Rectangle(i * size, j * size, size - 1, size - 1));
                    }
                    else
                    {
                        if (grid[i, j] == 'x') panelGraphics.FillRectangle(Brushes.Red, new Rectangle(i * size, j * size, size - 1, size - 1));
                        else
                        {
                            panelGraphics.FillRectangle(Brushes.Aqua, new Rectangle(i * size, j * size, size - 1, size - 1));
                            if (grid[i, j] != '0') panelGraphics.DrawString(Convert.ToString(grid[i, j]), new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Brushes.Black, new Point(i * size, j * size));
                        }
                    }
                }
            }
        }
    }
}
