/* David Björklund
 * Snake projekt.
 * 
 * 
 * Utökningar (C):
 * Låt användaren mötas av en inforuta med tydliga spelanvisningar
 * Lägg till en labels-knapp som startar ett nytt spel utan att starta om programmet
 * Gör spelet skalbart genom att panelen, dess element och alla labels är skalbara
 * Lägg till en effekt varje gång ormen tar en matbit
 * Lägg till en timer som visar tiden. 
 * Lägg till några “levlar”, t ex ormen rör sig snabbare och snabbare
 * Lägg till Start- och Stopp-knapp. 
 * När ormen dör ska “Game Over” tändas/blinka tills nytt spel startas
 * 
 * Utökningar (A):
 * Utöka med fler matbitar
 * Låt matbitar synas ett x antal sekunder, om man inte hinner kortas ormens svans!
 * Fixa booster-matbitar.
 * Låt ormen gå igenom väggarna och dyka upp på motsatt sida.
 * Slumpa ut hinder på banan som dödar ormen om den kolliderar med hinder.
 * Låt ormens svans bestå av olikfärgade rektanglar
 * 
 */
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
    public partial class Snake : Form
    {
        // Olika färger på svansen
        bool tailColor;
        // Avsluta spelet när man åker utanför kanten
        bool moveThroughEdges;
        // Rita med bättre grafik
        bool graphics;
        // Boosters finns
        bool allowBoosters;
        // Tillåt flera matbitar att finnas
        bool multipleFoods;
        // Matbitar försvinner efter en viss tid
        bool timerFoods;
        // Hinder ska slumpas ut
        bool obstaclesExist;

        /*
        Image redApple = Image.FromFile("https://github.com/DavidBjorklund/Snake/red-apple.png");
        Image goldApple = Image.FromFile("https://github.com/DavidBjorklund/Snake/gold-apple.png");
        Image spike = Image.FromFile("https://github.com/DavidBjorklund/Snake/spike.png");

        */
        Image redApple = Image.FromFile($"{System.IO.Directory.GetCurrentDirectory()}\\..\\..\\red-apple.png");
        Image goldApple = Image.FromFile($"{System.IO.Directory.GetCurrentDirectory()}\\..\\..\\gold-apple.png");
        Image spike = Image.FromFile($"{System.IO.Directory.GetCurrentDirectory()}\\..\\..\\spike.png");

        int prevSizeAround;
        int prevSize;
        bool isPaused = false;
        int activeBoosterLeft = 0;
        int gridSize = 21;
        int size = 20;
        int sizeAround = 520;
        int x = 10;
        int y = 10;
        int dx = 0;
        int dy = 0;
        int time = 0;
        int scoreCounter = 0;
        int interval = 225;
        class Food
        {
            public Rectangle rect;
            public int time;
            public bool boost;
        }
        List<Food> foodList = new List<Food>();
        Random chance = new Random();
        List<Rectangle> snakeList = new List<Rectangle>();
        List<Brush> colorList = new List<Brush>();
        List<Rectangle> obstacleList = new List<Rectangle>();

        public Snake()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lägg till ny rektangel i snakeList
        /// Slumpa ut ny mat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //snakeList.Add(new Rectangle(x, y, size, size));
            //randomizeFood();

        }

        /// <summary>
        /// Keydown events
        /// Key up, down, left, right: ändrar dx, dy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && dy != 1)
            {
                dy = -1;
                dx = 0;
            }
            if (e.KeyCode == Keys.Down && dy != -1)
            {
                dy = 1;
                dx = 0;
            }
            if (e.KeyCode == Keys.Left && dx != 1)
            {
                dy = 0;
                dx = -1;
            }
            if (e.KeyCode == Keys.Right && dx != -1)
            {
                dy = 0;
                dx = 1;
            }

        }

        /// <summary>
        /// Varje Tick ändrar spelet
        /// Flyttar ormen
        /// Om den träffar kanten
        /// Om den träffar mat
        /// Om den träffar sig sjäv (kannibal)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerGame_Tick(object sender, EventArgs e)
        {
            gameArea.Invalidate();
            moveSnake();
            cornerHit();
            foodHit();
            obstacleHit();
            cannibalismHit();
        }

        /// <summary>
        /// Flytta på ormen
        /// Gå igenom varje och byt x,y till nästas x,y värde
        /// Ändra den första till sitt värde +dx +dy,
        /// </summary>
        private void moveSnake()
        {
            if (!moveThroughEdges)
            {
                //Inte Flytta genom väggar
                x += dx;
                y += dy;
            }
            else
            {
                //Flytta genom väggar
                x = (x + dx + gridSize) % gridSize;
                y = (y + dy + gridSize) % gridSize;
            }
            if (multipleFoods && chance.Next(0, 10000 / interval) == 0) randomizeFood();
            snakeList.Insert(0, new Rectangle(x * size, y * size, size, size));
            snakeList.RemoveAt(snakeList.Count - 1);
        }

        /// <summary>
        /// Lägg till poäng
        /// Skriv ut poäng
        /// </summary>
        private void addScore()
        {
            scoreCounter++;
            lblScore.Text = "Score: " + scoreCounter;
            if (scoreCounter % 10 == 0) levelUp();
        }

        private void removeScore()
        {
            scoreCounter--;
            lblScore.Text = "Score: " + scoreCounter;
            removeSnakeTail();
            if (scoreCounter % 10 == 9) levelDown();
        }

        /// <summary>
        /// Vid level-up:
        ///     Uppdatera interval på timerGame till 67% av tidigare
        ///     Uppdatera level i label level
        /// </summary>
        private void levelUp()
        {
            interval = interval * 2 / (int)3;
            timerGame.Interval = interval;
            lblLevel.Text = "Level: " + (int)Math.Ceiling((scoreCounter + 1) / Convert.ToDecimal(10));
            if (obstaclesExist) { createObstacle(); createObstacle(); }
        }

        private void levelDown()
        {
            interval = interval * 3 / (int)2;
            timerGame.Interval = interval;
            lblLevel.Text = "Level: " + (int)Math.Ceiling((scoreCounter + 1) / Convert.ToDecimal(10));
            if (obstaclesExist) { removeObstacle(); removeObstacle(); }
        }

        /// <summary>
        /// Lägg till orm i slutet av snakeList
        /// Lägg den i motsatt rikting till riktingen som den sista ormbiten åkte i.
        /// </summary>
        private void addSnakeTail()
        {
            int tempX;
            int tempY;
            if (snakeList.Count == 1)
            {
                tempX = -dx;
                tempY = -dy;
            }
            else
            {
                // Riktningen på sista ormbiten
                tempX = snakeList[snakeList.Count - 1].X / size - snakeList[snakeList.Count - 2].X / size;
                tempY = snakeList[snakeList.Count - 1].Y / size - snakeList[snakeList.Count - 2].Y / size;
            }
            snakeList.Add(new Rectangle((snakeList[snakeList.Count - 1].X / size + tempX) * size, (snakeList[snakeList.Count - 1].Y / size + tempY) * size, size, size));
            colorList.Add(new SolidBrush(Color.FromArgb(chance.Next(100, 200), 0, 0, 0)));
        }

        /// <summary>
        /// Tar bort sista delen av ormens svans
        /// </summary>
        private void removeSnakeTail()
        {
            snakeList.RemoveAt(snakeList.Count - 1);
        }



        /// <summary>
        /// Testa om maten träffar ormen
        /// Om den gör det:
        ///     lägg till orm-del
        ///     lägg till poäng
        ///     slumpa ny mat
        ///     aktivera effekten på pictureBox Border
        /// annars:
        ///     avaktivera effekten på pictureBox Border
        /// </summary>
        private void foodHit()
        {
            for (int i = foodList.Count - 1; i >= 0; i--)
            {
                if (foodList[i].rect.IntersectsWith(snakeList[0]))
                {
                    if (foodList[i].boost) activeBoosterLeft = 3;
                    if (activeBoosterLeft > 0)
                    {
                        addSnakeTail();
                        addScore();
                        activeBoosterLeft--;
                    }
                    addSnakeTail();
                    addScore();
                    foodList.RemoveAt(i);
                    if (foodList.Count == 0) randomizeFood();
                    pbBorder.BackColor = Color.Gold;
                    return;
                }
            }
            if (activeBoosterLeft == 0) pbBorder.BackColor = Color.Transparent;
            else pbBorder.BackColor = Color.Blue;
        }

        /// <summary>
        /// Slumpar ny mat
        /// </summary>
        private void randomizeFood()
        {
            // Create a food object
            bool isInterSected = false;
            Rectangle foodRect;
            do
            {
                int fx = chance.Next(0, gridSize);
                int fy = chance.Next(0, gridSize);
                foodRect = new Rectangle(fx * size, fy * size, size, size);
                isInterSected = false;
                foreach (Rectangle s in snakeList)
                {
                    if (foodRect.IntersectsWith(s)) isInterSected = true;
                }
            } while (isInterSected);
            Food foodObject = new Food();
            foodObject.rect = foodRect;
            foodObject.time = chance.Next(4, 10);
            foodObject.boost = (allowBoosters && chance.Next(0, 7) == 0) ? true : false;
            foodList.Add(foodObject);

        }
        /// <summary>
        /// Skapar ett nytt hinder till obstacleList
        /// </summary>
        private void createObstacle()
        {
            Rectangle obstacle;
            bool isInterSected = false;
            do
            {
                int ox = chance.Next(0, gridSize);
                int oy = chance.Next(0, gridSize);
                obstacle = new Rectangle(ox * size, ox * size, size, size);
                isInterSected = false;
                foreach (Rectangle s in snakeList)
                {
                    if (obstacle.IntersectsWith(s)) isInterSected = true;
                }
            } while (isInterSected);
            obstacleList.Add(obstacle);
        }

        /// <summary>
        /// Tar bort det sista hindret i obstacleList
        /// </summary>
        private void removeObstacle()
        {
            if (obstacleList.Count > 0) obstacleList.RemoveAt(obstacleList.Count - 1);
        }

        /// <summary>
        /// Testar ifall man krockat med ett hinder:
        ///   Game over om man krockat
        /// </summary>
        private void obstacleHit()
        {
            foreach (Rectangle o in obstacleList)
            {
                if (o.IntersectsWith(snakeList[0]))
                {
                    gameOver();
                    return;
                }
            }
        }

        /// <summary>
        /// Om den träffar kanten:
        ///     Game over
        /// </summary>
        private void cornerHit()
        {
            if (snakeList[0].X / size < 0 || snakeList[0].X / size >= gridSize || snakeList[0].Y / size < 0 || snakeList[0].Y / size >= gridSize)
            {
                gameOver();
            }
        }


        /// <summary>
        /// Om den träffar sig själv:
        ///     Game Over
        /// </summary>
        private void cannibalismHit()
        {
            for (int i = 1; i < snakeList.Count; i++)
            {
                if (snakeList[0].IntersectsWith(snakeList[i]))
                {
                    gameOver();
                    return;
                }
            }
        }

        /// <summary>
        /// Det är game over:
        ///     Stanna timers
        ///     Visa label Game Over
        /// </summary>
        private void gameOver()
        {
            timerGame.Stop();
            timerTime.Stop();
            timerGameOver.Start();
            lblGameOver.Visible = true;
            lblPause.Enabled = false;
        }

        /// <summary>
        /// Uppdaterar panelens grafik:
        ///     Rita ut röd mat
        ///     Rita ut ormen:
        ///         Om tailColor (svansfärger) är aktiverat:
        ///             Rita ut svans med färgen
        ///         Annars:
        ///             Rita ut svart svans
        ///         Om drawBorder:
        ///             Rita ut 3d border på snaken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics panelGraphics = e.Graphics;

            foreach (Food f in foodList)
            {
                if (graphics) panelGraphics.DrawImage(f.boost ? goldApple : redApple, f.rect.X, f.rect.Y, size, size);
                else panelGraphics.FillRectangle(f.boost ? Brushes.Orange : Brushes.Red, f.rect);
            }
            foreach (Rectangle o in obstacleList)
            {
                if (graphics) panelGraphics.DrawImage(spike, o.X, o.Y, size, size);
                else panelGraphics.FillRectangle(Brushes.Purple, o);
            }

            for (int i = 0; i < snakeList.Count; i++)
            {
                //Random shades of gray on the color;
                if (tailColor && colorList.Count > i) panelGraphics.FillRectangle(colorList[i], snakeList[i]);
                else panelGraphics.FillRectangle(Brushes.Black, snakeList[i]);
                if (graphics) ControlPaint.DrawBorder3D(panelGraphics, snakeList[i], Border3DStyle.Raised);
            }
        }

        /// <summary>
        /// Startar spelet
        /// Döljer label game over
        /// startar timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblStart_Click(object sender, EventArgs e)
        {
            time = 0;
            updateSettings();
            hideInfo();
            unpause();
            obstacleList.Clear();
            reset();
            snakeList.Clear();
            snakeList.Add(new Rectangle(x * size, y * size, size, size));
            colorList.Clear();
            colorList.Add(Brushes.Black);
            foodList.Clear();
            randomizeFood();
        }

        /// <summary>
        /// återställer till startposition
        /// </summary>
        private void reset()
        {
            timerGameOver.Stop();
            lblGameOver.Visible = false;
            interval = 225;
            timerGame.Interval = interval;
            lblPause.Enabled = true;
            x = (int)(Math.Floor(gridSize / 2.0));
            y = (int)(Math.Floor(gridSize / 2.0));
            dx = 1;
            dy = 0;
            scoreCounter = 0;
            levelUp();
        }

        /// <summary>
        /// gömmer info-rutan
        /// </summary>
        private void hideInfo()
        {
            pbInfo.Visible = false;
            lblInfoH1.Visible = false;
            lblInfoP.Visible = false;
            lblInfoH3.Visible = false;
            clbInfo.Visible = false;
            this.ActiveControl = null;
        }

        /// <summary>
        /// Uppdaterar inställningarna med inputen från inforutans checkboxlista
        /// </summary>
        private void updateSettings()
        {
            moveThroughEdges = clbInfo.GetItemChecked(0);
            allowBoosters = clbInfo.GetItemChecked(1);
            multipleFoods = clbInfo.GetItemChecked(2);
            timerFoods = clbInfo.GetItemChecked(3);
            obstaclesExist = clbInfo.GetItemChecked(4);
            tailColor = clbInfo.GetItemChecked(5);
            graphics = clbInfo.GetItemChecked(6);
        }

        /// <summary>
        /// varje sekund:
        ///     Uppdatera label tid-visaren
        ///     Öka tiden
        ///     Om "Matbitar försvinner" från clbInfo:
        ///         Minska tiden på matbiten
        ///         Ta bort matbitar när tiden är slut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = Math.Floor(time / 60.0) + ":" + (time % 60).ToString().ToString().PadLeft(2, '0');
            time++;
            if (timerFoods)
            {
                for (int i = foodList.Count - 1; i >= 0; i--)
                {
                    foodList[i].time--;
                    if (foodList[i].time == 0)
                    {
                        foodList.RemoveAt(i);
                        if (foodList.Count == 0) randomizeFood();
                        if (snakeList.Count > 1) removeScore();
                    }
                }
            }
        }

        /// <summary>
        /// Vid klick på pause/unpause labeln:
        ///     Om spelet är pausat:
        ///         Unpause
        ///     Annars:
        ///         Pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPause_Click(object sender, EventArgs e)
        {
            if (isPaused) unpause();
            else pause();
        }

        /// <summary>
        /// Pausar spelet:
        ///     Stannar timers
        ///     Byter text på pauseknappen
        /// </summary>
        private void pause()
        {
            timerGame.Stop();
            timerTime.Stop();
            lblPause.Text = "Unpause Game";
            isPaused = true;
        }

        /// <summary>
        /// Unpausar spelet:
        ///     Startar timers
        ///     Byter text på pauseknappen
        /// </summary>
        private void unpause()
        {
            timerGame.Start();
            timerTime.Start();
            lblPause.Text = "Pause Game";
            isPaused = false;
        }

        /// <summary>
        /// När Formulärets storlek ändras:
        ///     Uppdatera skalningen metoden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            updateScale();
        }

        /// <summary>
        /// Vid uppdatering av skalningen:
        ///     Läs in nya storleken
        ///     Anpassa allt efter nya storleken
        /// </summary>
        private void updateScale()
        {
            prevSizeAround = sizeAround;
            prevSize = size;
            sizeAround = Math.Min(this.Size.Height, (this.Size.Width - 140));
            size = (int)Math.Floor(sizeAround / Convert.ToDecimal(26));
            gameArea.Left = (int)(size * 1.5);
            gameArea.Top = (int)(size * 1.5);
            gameArea.Width = (int)(size * 21);
            gameArea.Height = (int)(size * 21);
            pbBorder.Left = size;
            pbBorder.Top = size;

            moveBoard();
            gameArea.Invalidate();
            moveLabels();
            moveInfo();
            pbBorder.Left = 25 / 20 * size;
            pbBorder.Top = 25 / 20 * size;
            pbBorder.Width = (int)(size * 21.5);
            pbBorder.Height = (int)(size * 21.5);
        }
        /// <summary>
        /// Anpassa spelplanen efter skalningen
        /// </summary>
        private void moveBoard()
        {
            //food = new Rectangle(food.X * size / prevSize, food.Y * size / prevSize, size, size);
            for (int i = foodList.Count - 1; i >= 0; i--)
            {
                foodList[i].rect = new Rectangle(foodList[i].rect.X * size / prevSize, foodList[i].rect.Y * size / prevSize, size, size);
            }
            for (int i = snakeList.Count - 1; i >= 0; i--)
            {
                snakeList[i] = new Rectangle(snakeList[i].X * size / prevSize, snakeList[i].Y * size / prevSize, size, size);
            }
        }

        /// <summary>
        /// Anpassa formulärets labels efter skalningen
        /// </summary>
        private void moveLabels()
        {
            lblTime.Left = 410 / 20 * size;
            lblTime.Top = 5 / 20 * size;
            lblTime.Font = new Font("Microsoft Sans Serif", (10F / 20 * size));

            lblStart.Left = 470 / 20 * size;
            lblStart.Top = 40 / 20 * size;
            lblStart.Padding = new Padding((size * 10) / 20, (size * 10) / 20, (size * 10) / 20, (size * 10) / 20);
            lblStart.Font = new Font("Microsoft Sans Serif", (12F / 20 * size));

            lblPause.Left = 470 / 20 * size;
            lblPause.Top = 90 / 20 * size;
            lblPause.Padding = new Padding((size * 3) / 20, (size * 3) / 20, (size * 3) / 20, (size * 3) / 20);
            lblPause.Font = new Font("Microsoft Sans Serif", (11F / 20 * size));

            lblScore.Left = 470 / 20 * size;
            lblScore.Top = 134 / 20 * size;
            lblScore.Font = new Font("Microsoft Sans Serif", (11F / 20 * size));

            lblLevel.Left = 470 / 20 * size;
            lblLevel.Top = 168 / 20 * size;
            lblLevel.Font = new Font("Microsoft Sans Serif", (11F / 20 * size));

            lblGameOver.Left = lblGameOver.Left * size / prevSize;
            lblGameOver.Top = lblGameOver.Top * size / prevSize;
            lblGameOver.Font = new Font("Microsoft Sans Serif", (40F / 20 * size));
        }

        /// <summary>
        /// Anpassa Info-rutan efter skalningen
        /// </summary>
        private void moveInfo()
        {
            pbInfo.Left = size / 2;
            pbInfo.Top = size / 2;
            pbInfo.Width = size * 20;
            pbInfo.Height = size * 20;

            lblInfoH1.Left = 30 / 20 * size;
            lblInfoH1.Top = 30 / 20 * size;
            lblInfoH1.Font = new Font("Microsoft Sans Serif", (20F / 20 * size));

            lblInfoP.Left = 30 / 20 * size;
            lblInfoP.Top = 70 / 20 * size;
            lblInfoP.MaximumSize = new Size(360 / 20 * size, 0);
            lblInfoP.Font = new Font("Microsoft Sans Serif", (11F / 20 * size));

            lblInfoH3.Left = 30 / 20 * size;
            lblInfoH3.Top = 157 / 20 * size;
            lblInfoH3.Font = new Font("Microsoft Sans Serif", (15F / 20 * size));

            clbInfo.Left = 30 / 20 * size;
            clbInfo.Top = 186 / 20 * size;
            clbInfo.Font = new Font("Microsoft Sans Serif", (8.25F / 20 * size));
            clbInfo.Size = new Size(178 / 20 * size, 130 / 20 * size);
        }

        /// <summary>
        /// Få labeln game over att blinka var 500ms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerGameOver_Tick(object sender, EventArgs e)
        {
            lblGameOver.Visible = !lblGameOver.Visible;
        }
    }
}
