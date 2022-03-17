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
 * l arrow moves block l if possible
 * right arrow moves block right if possible
 * up arrow rotates block clock-wise if possible
 * created block and tile rotation (clock-wise and counter-clock-wise)
 * created block and tile movement width direction (l -1, right 1)
 * created method to check for filled row (checkTetris)
 * created method to clear tetris row and move tiles above down
 * 
 * V3:
 * Wallkick - Super Rotation System
 * Created scoring system
 * Created "hold" block function
 * 
 * V4:
 * Added Comments
 * 
 * Todo:
 * Game over
 * Better scoring system
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
        public Block holdb;
        private Random chance = new Random();

        private Rectangle[,] background = new Rectangle[10, 16];
        private Rectangle[,] backgroundNext = new Rectangle[5, 5];

        Dictionary<string, int[,]> srs;
        Dictionary<string, int[,]> isrs;

        //states zero 0 , right 1 , two 2 , left 3
        //from zero to right or left
        //from right to zero or two
        //from two to right or left
        //from left to zero or two
        string[,] states = new string[4, 4] {
            {"","zr","","zl"},
            {"rz","","rt",""},
            {"","tr","","tl"},
            {"lz","","lt",""}
        };

        /// <summary>
        /// takes x,y coordinate and returns whether tile is:
        /// -1 outside bounds,
        /// 0 occupied,
        /// 1 empty
        /// </summary>
        /// <param name="x">grid coordinate x</param>
        /// <param name="y">grid coordinate y</param>
        /// <returns>-1 if outside bounds, 0 if occupied by tileList, 1 if empty</returns>
        public int validSquare(int x, int y)
        {
            if (x >= width || x < 0 || y >= height || y < 0) return -1;
            foreach (Tile t in tileList)
            {
                if (t.x == x && t.y == y) return 0;
            }
            return 1;
        }

        /// <summary>
        /// tests if block position is already occupied or not possible in grid
        /// </summary>
        /// <param name="offsetX">possible offset of the block</param>
        /// <param name="offsetY">possible offset of the block</param>
        /// <returns>true if block position is valid, false if block position is occupied </returns>
        private bool blockIsValid(int offsetX = 0, int offsetY = 0)
        {
            foreach (Tile tile in b.blockTileList)
            {
                if (validSquare(tile.x + offsetX, tile.y + offsetY) != 1) return false;
            }
            return true;
        }

        /// <summary>
        /// starts an attempt to shift the block into a valid position
        /// shift is based on blocktype
        /// </summary>
        /// <param name="blocktype">type of block [O,I,J,L,S,T,Z]</param>
        /// <param name="newstate">the state the shift is from</param>
        /// <param name="prevstate">the state the shift is going to</param>
        private void tryOffset(string blocktype = "O", int newstate = 0, int prevstate = 1)
        {
            if (blocktype == "O") return;
            if (blocktype == "I")
            {

                shift(isrs["i" + states[prevstate, newstate]]);
                return;
            }
            shift(srs[states[prevstate, newstate]]);
        }

        /// <summary>
        /// uses a shift array to test shift scenarios of the block
        /// if shift is possible, then it will move the block and return true
        /// </summary>
        /// <param name="shiftArray">the values of the shift</param>
        /// <returns>True if shift was possible and block is moved; False if shift not possible</returns>
        private bool shift(int[,] shiftArray)
        {
            for (int i = 0; i < 5; i++)
            {
                if (blockIsValid(shiftArray[i, 0], shiftArray[i, 1]) == true)
                {
                    b.move(shiftArray[i, 0], shiftArray[i, 1]);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Paints the game panel:
        ///     Paints the back-color
        ///     Paints the tiles in tileList
        ///     Paints the tiles in blocks blockTileList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// When form is loaded
        /// Create a background grid for main
        /// Create a background grid for Next
        /// Create a new block
        /// Create a new next block
        /// Create the srs offset data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            srs = new Srs().data;
            isrs = new Isrs().data;

            panel.Invalidate();
        }

        /// <summary>
        /// Method to test an object in debugger
        /// </summary>
        /// <param name="obj">Object to debug</param>
        private void test(object obj)
        {
            obj.ToString();
        }

        /// <summary>
        /// When start button is clicked:
        ///     Create a new block
        ///     Clear the tileList
        ///     Start the gameTimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblStart_Click(object sender, EventArgs e)
        {
            b = new Block(chance.Next(0, 7));
            tileList.Clear();
            gameTimer.Stop();
            gameTimer.Start();
            panel.Invalidate();
        }

        /// <summary>
        /// Every tick of the game timer
        /// If block can move (could be placed at y+1):
        ///     move the block
        /// Else:
        ///     change the block
        ///     test for completed row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (b != null)
            {
                if (blockIsValid(0, 1)) b.moveBlock();
                else { changeBlock(); checkTetris(); }
            }
            panel.Invalidate();
        }

        /// <summary>
        /// splits the tiles into an yList per row
        /// if an yList row is completed (10):
        ///     Clear the row and move down tiles above
        ///     Restart the checking process
        /// </summary>
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

        /// <summary>
        /// Adds points to the score (needs ammendment)
        /// If tile is part of row to be cleared:
        ///     Remove Tile from tileList
        /// If tile is above the row cleared
        ///     Move tile down
        /// </summary>
        /// <param name="row">The row that should be cleared</param>
        private void clearRow(int row)
        {
            lblPoints.Text = (Convert.ToInt32(lblPoints.Text) + 82).ToString();
            //backwards iteration because elements are removed
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

        /// <summary>
        /// Places the current block in hold
        /// If a block is in hold:
        ///     Change the current block to the block in hold
        /// If no block in hold:
        ///     Update the block normally through changeBlock
        /// </summary>
        private void placeInHold()
        {
            if (holdb == null)
            {
                holdb = new Block(b.typeint);
                b = null;
                changeBlock();
            }
            else
            {
                int holdtypeint = b.typeint;
                b = holdb;
                holdb = new Block(holdtypeint);
            }
            panelHold.Invalidate();
            panelNext.Invalidate();
            panel.Invalidate();
        }

        /// <summary>
        /// places the current block in the permanent tileList
        /// updates the current block from the nextblock
        /// updates the nextblock
        /// </summary>
        private void changeBlock()
        {
            if (b != null)
            {
                foreach (Tile tile in b.blockTileList)
                {
                    tileList.Add(tile);
                }
            }
            b = nextb;
            nextb = new Block(chance.Next(0, 7));
            panelNext.Invalidate();
        }

        /// <summary>
        /// On a keydown:
        /// If Key left:
        ///     Move left if possible
        /// If Key Right:
        ///     Move right if possible
        /// If Key Up:
        ///     Rotate clock-wise
        ///     If not possible, try offset with srs
        ///     If still not possible, rotate back
        /// If Key Down:
        ///     If can move down: move down
        /// If Key Space:
        ///     Move down to the last possible down move
        /// If Key Tab:
        ///     Place block in hold
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    if (!blockIsValid()) tryOffset(b.type, b.state, b.prevstate);
                    if (!blockIsValid()) b.rotate(false);
                    panel.Invalidate();
                    break;
                case Keys.Down:
                    if (blockIsValid(0, 1)) b.moveBlock();
                    break;
                case Keys.Space:
                    while (blockIsValid(0, 1)) b.moveBlock();
                    break;
                case Keys.Tab:
                    placeInHold();
                    break;
            }
        }

        /// <summary>
        /// Paints the next-panel
        /// Paints the background of next
        /// Paints the block inside next
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Paints the hold-panel
        /// Paints the backcolor
        /// Paints the hold-block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelHold_Paint(object sender, PaintEventArgs e)
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

            if (holdb != null)
            {
                foreach (Tile nextTile in holdb.blockTileList)
                {
                    panelNextGraphics.FillRectangle(nextTile.color, new Rectangle((nextTile.x - 3) * size + 1, (nextTile.y + 1) * size + 1, size - 1, size - 1));
                }
            }
        }
    }

    /// <summary>
    /// Every Tile is an instance of this class Tile
    /// 
    /// Test Add Tile: 
    /// new Tile(1, 4,Brushes.Yellow)
    /// 
    /// Tile has public propeties:
    /// color (Brush),
    /// x,
    /// y,
    /// size,
    /// r (Rectangle)
    /// </summary>
    public class Tile
    {
        public Brush color;
        public int x;
        public int y;
        public int size;
        public Rectangle r;

        /// <summary>
        /// Constructor of Tile
        /// </summary>
        /// <param name="setX">x position of tile</param>
        /// <param name="setY">y position of tile</param>
        /// <param name="brushColor">Brush color of the Tile</param>
        /// <param name="setSize">size of the Tile</param>
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

        /// <summary>
        /// updates r (Rectangle) with Tiles properties values
        /// </summary>
        public void updateRectangle()
        {
            r = new Rectangle(x * size + 1, y * size + 1, size - 1, size - 1);
        }

        /// <summary>
        /// moves down one tile
        /// updates the r (Rectangle)
        /// </summary>
        public void moveDown()
        {
            y += 1;
            updateRectangle();
        }

        /// <summary>
        /// Moves in direction dx, dy
        /// Updates r (Rectangle)
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void move(int dx = 0, int dy = 0)
        {
            x += dx;
            y += dy;
            updateRectangle();
        }

        /// <summary>
        /// Rotates rectangle around center: cx,cy
        /// According to 2d rotation matrix system
        /// Updates r (Rectangle)
        /// </summary>
        /// <param name="cw"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        public void rotate(bool cw, double cx, double cy)
        {
            double tempx = cw ? (y - cy) * -1 : (y - cy);
            double tempy = cw ? (x - cx) : (x - cx) * -1;
            x = (int)(cx + tempx);
            y = (int)(cy + tempy);
            updateRectangle();
        }

    }

    /// <summary>
    /// Block is a storage for Tiles and allows them to move as a group
    /// 
    /// public properties:
    /// type (string)
    /// state,
    /// prevstate,
    /// color (Brush)
    /// centerx (double),
    /// centery (double),
    /// </summary>
    public class Block
    {
        public List<Tile> blockTileList = new List<Tile>();
        public string type;
        public int typeint;
        public int state = 0;
        public int prevstate = 0;

        private Brush color;
        private double centerx;
        private double centery = 1;

        //Starting coordinates for different types
        private int[,] I = new int[4, 2] { { 3, 1 }, { 4, 1 }, { 5, 1 }, { 6, 1 } };
        private int[,] O = new int[4, 2] { { 4, 1 }, { 4, 0 }, { 5, 0 }, { 5, 1 } };
        private int[,] T = new int[4, 2] { { 4, 1 }, { 5, 1 }, { 5, 0 }, { 6, 1 } };
        private int[,] J = new int[4, 2] { { 4, 0 }, { 4, 1 }, { 5, 1 }, { 6, 1 } };
        private int[,] L = new int[4, 2] { { 4, 1 }, { 5, 1 }, { 6, 1 }, { 6, 0 } };
        private int[,] S = new int[4, 2] { { 4, 1 }, { 5, 1 }, { 5, 0 }, { 6, 0 } };
        private int[,] Z = new int[4, 2] { { 4, 0 }, { 5, 0 }, { 5, 1 }, { 6, 1 } };

        /// <summary>
        /// Constructor of Block:
        ///     Creates a block with type of typeNum
        /// </summary>
        /// <param name="typeNum">type of the block 0-6</param>
        public Block(int typeNum = 0)
        {
            typeint = typeNum;
            createType(typeNum);
        }

        /// <summary>
        /// Moves every Tile in Block one step down
        /// Moves the center of the block one step down
        /// </summary>
        public void moveBlock()
        {
            foreach (Tile tile in blockTileList)
            {
                tile.moveDown();
            }
            centery++;
        }

        /// <summary>
        /// Moves every Tile in Block in direction of dx, dy
        /// Moves the center of the Block in direction of dx, dy
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void move(int dx, int dy = 0)
        {
            foreach (Tile tile in blockTileList)
            {
                tile.move(dx, dy);
            }
            centerx += dx;
            centery += dy;
        }

        /// <summary>
        /// Creates the Tiles of the block,
        /// According to typeArrays coordinates of the new Tiles
        /// </summary>
        /// <param name="typeArrays">Coordinates of the new Tiles</param>
        private void createTiles(int[,] typeArrays)
        {
            for (int i = 0; i < 4; i++)
            {
                blockTileList.Add(new Tile(typeArrays[i, 0], typeArrays[i, 1], color));
            }
        }

        /// <summary>
        /// Rotates every Tile in Block,
        /// Clock-wise or Counter-Clock-wise
        /// updates the state of the Block
        /// </summary>
        /// <param name="cw"></param>
        public void rotate(bool cw)
        {
            if (type == "O") return;
            foreach (Tile tile in blockTileList)
            {
                tile.rotate(cw, centerx, centery);
            }
            prevstate = state;
            state = cw ? state + 1 : state - 1;
            state = (state + 4) % 4;
        }

        /// <summary>
        /// Creates the block from the parameters of the Block Type
        /// </summary>
        /// <param name="t">string type of the Block</param>
        /// <param name="c">Brush color of the Block</param>
        /// <param name="cx">double centerx of the Block</param>
        /// <param name="cy">double centery of the Block</param>
        /// <param name="typeArrays">int[,] positions of every Tile in Block</param>
        private void createBlock(string t, Brush c, double cx, double cy, int[,] typeArrays)
        {
            type = t;
            color = c;
            centerx = cx;
            centery = cy;
            createTiles(typeArrays);
        }

        /// <summary>
        /// Creates the Block according to the BlockType
        /// </summary>
        /// <param name="typeNum"></param>
        private void createType(int typeNum)
        {
            switch (typeNum)
            {
                case 0:
                    /*type = "I";
                    color = Brushes.Aqua;
                    centerx = 4.5;
                    centery = 1.5;
                    createTiles(I);*/
                    createBlock("I", Brushes.Aqua, 4.5, 1.5, I);
                    break;
                case 1:
                    /*type = "O";
                    color = Brushes.Yellow;
                    centerx = 4.5;
                    centery = 1.5;
                    createTiles(O);*/
                    createBlock("O", Brushes.Yellow, 4.5, 1.5, O);
                    break;
                case 2:
                    /*type = "T";
                    color = Brushes.Purple;
                    centerx = 5;
                    createTiles(T);*/
                    createBlock("T", Brushes.Purple, 5, 1, T);
                    break;
                case 3:
                    /*type = "J";
                    color = Brushes.Blue;
                    centerx = 5;
                    createTiles(J);*/
                    createBlock("J", Brushes.Blue, 5, 1, J);
                    break;
                case 4:
                    /*type = "L";
                    color = Brushes.Orange;
                    centerx = 5;
                    createTiles(L);*/
                    createBlock("L", Brushes.Orange, 5, 1, L);
                    break;
                case 5:
                    /*type = "S";
                    color = Brushes.Green;
                    centerx = 5;
                    createTiles(S);*/
                    createBlock("S", Brushes.Green, 5, 1, S);
                    break;
                case 6:
                    /*type = "Z";
                    color = Brushes.Red;
                    centerx = 5;
                    createTiles(Z);*/
                    createBlock("Z", Brushes.Red, 5, 1, Z);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Super Rotation System
    /// 
    /// All Data for the Super Rotation System of the TJLSZ tetraminoes
    /// </summary>
    public class Srs
    {
        private int[,] zr = new int[5, 2] { { 0, 0 }, { -1, 0 }, { -1, 1 }, { 0, -2 }, { -1, -2 } };
        private int[,] rz = new int[5, 2] { { 0, 0 }, { 1, 0 }, { 1, -1 }, { 0, 2 }, { 1, 2 } };
        private int[,] rt = new int[5, 2] { { 0, 0 }, { 1, 0 }, { 1, -1 }, { 0, 2 }, { 1, 2 } };
        private int[,] tr = new int[5, 2] { { 0, 0 }, { -1, 0 }, { -1, 1 }, { 0, -2 }, { -1, -2 } };
        private int[,] tl = new int[5, 2] { { 0, 0 }, { 1, 0 }, { 1, 1 }, { 0, -2 }, { 1, -2 } };
        private int[,] lt = new int[5, 2] { { 0, 0 }, { -1, 0 }, { -1, -1 }, { 0, 2 }, { -1, 2 } };
        private int[,] lz = new int[5, 2] { { 0, 0 }, { -1, 0 }, { -1, -1 }, { 0, 2 }, { -1, 2 } };
        private int[,] zl = new int[5, 2] { { 0, 0 }, { 1, 0 }, { 1, 1 }, { 0, -2 }, { 1, -2 } };

        public Dictionary<string, int[,]> data = new Dictionary<string, int[,]>();

        /// <summary>
        /// Data for the rotations
        /// </summary>
        public Srs()
        {

            data.Add("zr", zr);
            data.Add("rz", rz);
            data.Add("lz", lz);
            data.Add("zl", zl);
            data.Add("tl", tl);
            data.Add("lt", lt);
            data.Add("rt", rt);
            data.Add("tr", tr);
        }
    }

    /// <summary>
    /// I tetraminoes Super Rotation System
    /// 
    /// All Data for the Super Rotation System of the I tetraminoe
    /// </summary>
    public class Isrs
    {
        private int[,] izr = new int[5, 2] { { 0, 0 }, { -2, 0 }, { 1, 0 }, { -2, -1 }, { 1, 2 } };
        private int[,] irz = new int[5, 2] { { 0, 0 }, { 2, 0 }, { -1, 0 }, { 2, 1 }, { -1, -2 } };
        private int[,] irt = new int[5, 2] { { 0, 0 }, { -1, 0 }, { 2, 0 }, { -1, 2 }, { 2, -1 } };
        private int[,] itr = new int[5, 2] { { 0, 0 }, { 1, 0 }, { -2, 0 }, { 1, -2 }, { -2, 1 } };
        private int[,] itl = new int[5, 2] { { 0, 0 }, { 2, 0 }, { -1, 0 }, { 2, 1 }, { -1, -2 } };
        private int[,] ilt = new int[5, 2] { { 0, 0 }, { -2, 0 }, { 1, 0 }, { -2, -1 }, { 1, 2 } };
        private int[,] ilz = new int[5, 2] { { 0, 0 }, { 1, 0 }, { -2, 0 }, { 1, -2 }, { -2, 1 } };
        private int[,] izl = new int[5, 2] { { 0, 0 }, { -1, 0 }, { 2, 0 }, { -1, 2 }, { 2, -1 } };

        public Dictionary<string, int[,]> data = new Dictionary<string, int[,]>();

        /// <summary>
        /// Data for the rotations
        /// </summary>
        public Isrs()
        {
            data.Add("izr", izr);
            data.Add("irz", irz);
            data.Add("ilz", ilz);
            data.Add("izl", izl);
            data.Add("itl", itl);
            data.Add("ilt", ilt);
            data.Add("irt", irt);
            data.Add("itr", itr);
        }
    }

}
