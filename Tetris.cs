/* Author: David Björklund
 * Created: 2022/02/17
 * Updated: 2022/02/17
 * 
 * Tetris in C#
 * 
 * V1:
 * created class Tile that holds x,y,color,Rectangle of a Tile
 * created class Block that holds a list of Tiles and a type
 * Tiles and Blocks can be created and drawn on the panel
 * Blocks can move and stop when they hit the ground or an existing Tile
 * Currently: game restarts when the start button is hit
 * 
 * V2:
 * created listeners and actions to button presses
 * left arrow moves block left if possible
 * right arrow moves block right if possible
 * up arrow rotates block clock-wise if possible
 * created block and tile rotation (clock-wise and counter-clock-wise)
 * created block and tile movement width direction (left -1, right 1)
 * created method to check for filled row (checkTetris)
 * created method to clear tetris row and move tiles above down
 * 
 * Todo:
 * Offset in rotation
 * Better axis of rotation for I and O block
 * Test game over
 * 
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBGames
{
    public partial class Tetris : Form
    {
        public Tetris()
        {
            InitializeComponent();

        }
        public int width = 10;
        public int height = 16;
        public int size = 25;
        public List<Tile> tileList = new List<Tile>();
        public Block b;
        public Block nextb;
        private Random chance = new Random();

        private Dictionary<string, int[,]> wallkick = new Dictionary<string, int[,]>();


        private Rectangle[,] background = new Rectangle[10, 16];
        private Rectangle[,] backgroundNext = new Rectangle[5, 5];

        public int validSquare(int x, int y)
        {
            if (x >= width || x < 0 || y >= height || y < 0) return -1;
            foreach (Tile t in tileList)
            {
                if (t.x == x && t.y == y) return 0;
            }
            return 1;
        }

        private bool blockIsValid(int offsetX = 0, int offsetY = 0)
        {
            foreach (Tile tile in b.blockTileList)
            {
                if (validSquare(tile.x + offsetX, tile.y + offsetY) != 1) return false;
            }
            return true;
        }

        private void tryOffset(string blocktype = "O", int prevstate = 0, int newstate = 1)
        {
            if (blocktype == "O") return;
            if (blocktype == "I")
            {
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                if (wallkick[prevstate.ToString() + newstate.ToString()][i, 0] == 0) continue;
            }


        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Graphics panelGraphics = e.Graphics;

            Brush backcolor = Brushes.Gray;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    panelGraphics.FillRectangle(backcolor, background[i, j]);
                }
            }
            foreach (Tile tile in tileList)
            {
                panelGraphics.FillRectangle(tile.color, tile.r);
            }
            if (b != null)
            {
                foreach (Tile tile in b.blockTileList)
                {
                    panelGraphics.FillRectangle(tile.color, tile.r);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    background[i, j] = new Rectangle(i * size + 1, j * size + 1, size - 1, size - 1);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    backgroundNext[i, j] = new Rectangle(i * size + 1, j * size + 1, size - 1, size - 1);
                }
            }

            b = new Block(chance.Next(0, 7));
            nextb = new Block(chance.Next(0, 7));

            int[,] zeroone = new int[5, 2] { { 0, 0 }, { -1, 0 }, { -1, 1 }, { 0, -2 }, { -1, -2 } };
            //wallkick.Add("01",zeroone);

            panel.Invalidate();
        }

        private void lblStart_Click(object sender, EventArgs e)
        {
            b = new Block(chance.Next(0, 7));
            tileList.Clear();
            gameTimer.Stop();
            gameTimer.Start();
            panel.Invalidate();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (b != null)
            {
                if (blockIsValid(0, 1)) b.moveBlock();
                else { changeBlock(); checkTetris(); }
            }
            panel.Invalidate();
        }

        private void checkTetris()
        {
            int[] yList = new int[16];
            for (int i = 0; i < 16; i++) { yList[i] = 0; }
            foreach (Tile tile in tileList)
            {
                yList[tile.y] += 1;
            }

            for (int i = 15; i >= 0; i--)
            {
                if (yList[i] == 10)
                {
                    clearRow(i);
                    checkTetris();
                    return;
                }
            }

        }

        private void clearRow(int row)
        {
            for (int i = tileList.Count - 1; i >= 0; i--)
            {
                if (tileList[i].y == row)
                {
                    tileList.RemoveAt(i);
                }
                else if (tileList[i].y < row)
                {
                    tileList[i].moveDown();
                }
            }
        }

        private void changeBlock()
        {
            foreach (Tile tile in b.blockTileList)
            {
                tileList.Add(tile);
            }
            b = nextb;
            nextb = new Block(chance.Next(0, 7));
            panelNext.Invalidate();
        }

        private void Tetris_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    b.move(-1);
                    if (!blockIsValid()) b.move(1);
                    panel.Invalidate();
                    break;
                case Keys.Right:
                    b.move(1);
                    if (!blockIsValid()) b.move(-1);
                    panel.Invalidate();
                    break;
                case Keys.Up:
                    b.rotate(true);
                    //if (!blockIsValid()) tryOffset(b.type,b.state,(b.state-1+4)%4);
                    if (!blockIsValid()) b.rotate(false);
                    panel.Invalidate();
                    break;
                case Keys.Down:
                    if (blockIsValid(0, 1)) b.moveBlock();
                    break;
                case Keys.Space:
                    while (blockIsValid(0, 1)) b.moveBlock();
                    break;
            }
        }

        private void panelNext_Paint(object sender, PaintEventArgs e)
        {
            Graphics panelNextGraphics = e.Graphics;

            Brush backcolor = Brushes.Gray;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    panelNextGraphics.FillRectangle(backcolor, backgroundNext[i, j]);
                }
            }

            foreach (Tile nextTile in nextb.blockTileList)
            {
                panelNextGraphics.FillRectangle(nextTile.color, new Rectangle((nextTile.x - 3) * size + 1, (nextTile.y + 1) * size + 1, size - 1, size - 1));
            }
        }
    }
    public class Tile
    {
        public Brush color;
        public int x;
        public int y;
        public int size;
        public Rectangle r;
        public Tile(int setX = 0, int setY = 0, Brush brushColor = null, int setSize = 25)
        {
            x = setX;
            y = setY;
            size = setSize;
            if (brushColor == null) color = Brushes.Blue;
            else color = brushColor;
            size = setSize;
            updateRectangle();
        }
        public void updateRectangle()
        {
            r = new Rectangle(x * size + 1, y * size + 1, size - 1, size - 1);
        }
        public void moveDown()
        {
            y += 1;
            updateRectangle();
        }

        public void move(int dir)
        {
            x += dir;
            updateRectangle();
        }

        public void rotate(bool cw, double cx, double cy)
        {
            double tempx = cw ? (y - cy) * -1 : (y - cy);
            double tempy = cw ? (x - cx) : (x - cx) * -1;
            x = (int)(cx + tempx);
            y = (int)(cy + tempy);
            updateRectangle();
        }

    }
    public class Block
    {
        public List<Tile> blockTileList = new List<Tile>();
        public string type;
        public int state = 0;

        private Brush color;
        private double centerx;
        private double centery = 1;

        private int[,] I = new int[4, 2] { { 3, 1 }, { 4, 1 }, { 5, 1 }, { 6, 1 } };
        private int[,] O = new int[4, 2] { { 4, 1 }, { 4, 0 }, { 5, 0 }, { 5, 1 } };
        private int[,] T = new int[4, 2] { { 4, 1 }, { 5, 1 }, { 5, 0 }, { 6, 1 } };
        private int[,] J = new int[4, 2] { { 4, 0 }, { 4, 1 }, { 5, 1 }, { 6, 1 } };
        private int[,] L = new int[4, 2] { { 4, 1 }, { 5, 1 }, { 6, 1 }, { 6, 0 } };
        private int[,] S = new int[4, 2] { { 4, 1 }, { 5, 1 }, { 5, 0 }, { 6, 0 } };
        private int[,] Z = new int[4, 2] { { 4, 0 }, { 5, 0 }, { 5, 1 }, { 6, 1 } };
        public Block(int typeNum = 0)
        {
            createType(typeNum);
        }

        public void moveBlock()
        {
            foreach (Tile tile in blockTileList)
            {
                tile.moveDown();
            }
            centery++;
        }

        public void move(int dir)
        {
            foreach (Tile tile in blockTileList)
            {
                tile.move(dir);
            }
            centerx += dir;
        }

        private void createTiles(int[,] typeArrays)
        {
            for (int i = 0; i < 4; i++)
            {
                blockTileList.Add(new Tile(typeArrays[i, 0], typeArrays[i, 1], color));
            }
        }

        public void rotate(bool cw)
        {
            if (type == "O") return;
            foreach (Tile tile in blockTileList)
            {
                tile.rotate(cw, centerx, centery);
            }
            state = cw ? state + 1 : state - 1;
            state = (state + 4) % 4;
        }

        private void createType(int typeNum)
        {
            switch (typeNum)
            {
                case 0:
                    type = "I";
                    color = Brushes.Aqua;
                    centerx = 4.5;
                    centery = 1.5;
                    createTiles(I);
                    break;
                case 1:
                    type = "O";
                    color = Brushes.Yellow;
                    centerx = 4.5;
                    centery = 1.5;
                    createTiles(O);
                    break;
                case 2:
                    type = "T";
                    color = Brushes.Purple;
                    centerx = 5;
                    createTiles(T);
                    break;
                case 3:
                    type = "J";
                    color = Brushes.Blue;
                    centerx = 5;
                    createTiles(J);
                    break;
                case 4:
                    type = "L";
                    color = Brushes.Orange;
                    centerx = 5;
                    createTiles(L);
                    break;
                case 5:
                    type = "S";
                    color = Brushes.Green;
                    centerx = 5;
                    createTiles(S);
                    break;
                case 6:
                    type = "Z";
                    color = Brushes.Red;
                    centerx = 5;
                    createTiles(Z);
                    break;
                default:
                    break;
            }
        }
    }
}
