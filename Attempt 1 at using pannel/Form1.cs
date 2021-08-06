using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace Attempt_1_at_using_pannel
{
    public partial class Form1 : Form
    {
        Graphics g;
        Rectangle[] UpS = new Rectangle[20];
        Rectangle[] DownS = new Rectangle[20];
        Rectangle[] LeftS = new Rectangle[20];
        Rectangle[] RightS = new Rectangle[20];
        Rectangle[] Object = new Rectangle[20];
        Rectangle Player, PlayerCenter, LSide, TSide, BSide, RSide, LightBar, Stick, boundL, boundR, boundT, boundB;
        string line;
        int Py, Px, LightArea = 250, LightBarLngth, Xmovement, Ymovement, Xshift, MapX, MapY, PathX, PathY, RecColour;
        double Fuel = 1.0;
        bool left, right, up, jump;
        int[,] Var = new int[10,2]
        {
            {0,0},//Px
            {1,0},//Py
            {2,0},//MapX
            {3,0},//MapY
            {4,0},
            {5,0},
            {6,0},
            {7,0},
            {8,0},
            {9,0}

        };
        int[,] PlayerMap = new int[5, 5]
        {
            {10,2,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0}
        };
        public Form1()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, Game_Pnl, new object[] { true });
            Framerate.Enabled = false;
            Ymovement = -5;
            Xmovement = 0;
            Py = 300;
            MapShift();
            boundB = new Rectangle(0,Game_Pnl.Bottom,Game_Pnl.Width, 5);
            boundL = new Rectangle(0, 0, 5, Game_Pnl.Height);
            boundR = new Rectangle(Game_Pnl.Right-5, 0, 5, Game_Pnl.Height);
            boundT = new Rectangle(0, Game_Pnl.Top, Game_Pnl.Width, 5);
        }

        private void Save_Btn_Click(object sender, EventArgs e)
        {
            Var[0, 1] = Px;
            Var[1, 1] = Py;
            Var[2, 1] = MapX;
            Var[3, 1] = MapY;
            TextWriter Save = new StreamWriter(Application.StartupPath + @"\Test.txt");

            //Writing text to the file.
            Save.WriteLine("Hello this line is skipped by the loading system so I just added this here and wanted to say hi!");
            Save.WriteLine(Var[0, 1]);
            Save.WriteLine(Var[1, 1]);
            Save.WriteLine(Var[2, 1]);
            Save.WriteLine(Var[3, 1]);
            Save.WriteLine(Fuel);
            System.Windows.Forms.MessageBox.Show("File Saved");

            //Close the file.
            Save.Close();
        }
        private void Load_Btn_Click(object sender, EventArgs e)
        {
            int ID = 0;

            System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath + @"\Test.txt");
            file.ReadLine();
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
                try
                {
                    Var[ID, 1] = Convert.ToInt32(line);
                    ID++;
                }
                catch (FormatException)
                {
                    if (line.Contains("."))
                    {
                        Fuel = Convert.ToDouble(line);
                    }
                }
            }
            Px = Var[0, 1];
            Py = Var[1, 1];
            MapX = Var[2, 1];
            MapY = Var[3, 1];
            System.Windows.Forms.MessageBox.Show("File Loaded");
            file.Close();
            MapShift();
            // Suspend the screen.  
            // System.Windows.Forms.MessageBox.Show(counter + "");

        }
        private void Return_Btn_Click(object sender, EventArgs e)
        {
            Load_Btn.Enabled = false;
            Load_Btn.Visible = false;
            Return_Btn.Enabled = false;
            Return_Btn.Visible = false;
            Save_Btn.Enabled = false;
            Save_Btn.Visible = false;
            Framerate.Enabled = true;
            Torch_Tmr.Enabled = true;
        }
        private void Framerate_Tick(object sender, EventArgs e)
        {
            LightBarLngth = (int)Math.Round(100 * Fuel);
            Player = new Rectangle(Px, Py, 35, 70);//Player Rectangle
            PlayerCenter = new Rectangle(Px+6, Py+10, 22, 40);
            LSide = new Rectangle(Px, Py, 5, 70);//Player Rectangle
            TSide = new Rectangle(Px+5, Py, 25, 10);//Player Rectangle
            BSide = new Rectangle(Px+5, Py+60, 25, 10);//Player Rectangle
            RSide = new Rectangle(Px+30, Py, 5, 70);//Player Rectangle
            LightBar = new Rectangle(685,50 ,LightBarLngth, 20);
            Game_Pnl.Invalidate();
            for (int i = 1; i < RecColour; i++)
            {
                if (left == true & !LSide.IntersectsWith(RightS[i]))
                {
                    Xmovement = 5;
                }
                else if (left == true & LSide.IntersectsWith(RightS[i]))
                {
                    Px = RightS[i].Right+1;
                }
                else
                {
                    Xmovement = 0;
                }
            }
            for (int i = 1; i < RecColour; i++)
            {
                if (right == true & !RSide.IntersectsWith(LeftS[i]))
                {
                    Xmovement = -5;
                }
                else if (right == true & RSide.IntersectsWith(LeftS[i]))
                {
                    Px = LeftS[i].Left- 38;
                }
            }
            for (int i = 1; i < RecColour; i++)
            {
                if (BSide.IntersectsWith(UpS[i]))
                {
                    Ymovement = 0;
                    Py = UpS[i].Top - 70;
                    jump = true;
                }                           
            }
            for (int i = 1; i < RecColour; i++)
            {
                if (TSide.IntersectsWith(DownS[i]))
                {
                    Ymovement = 0;
                    Py = DownS[i].Bottom + 7;
                }
            }
             if(Player.IntersectsWith(Stick))
            {
                Fuel = Fuel + 0.15;
                Stick = Rectangle.Empty;
                if (Fuel >= 1)
                {
                    Fuel = 1;
                }
            }

             if(Player.IntersectsWith(boundL) & left == true)
            {
                if(MapX >= 1)
                {
                    MapX = MapX - 1;
                    Px = Game_Pnl.Right;
                    MapShift();
                }
            }

            if (Player.IntersectsWith(boundR) & right == true)
            {
                if (MapX <= 3)
                {
                    MapX = MapX + 1;
                    Px = Game_Pnl.Left - 10;
                    MapShift();
                }
            }

            if (Player.IntersectsWith(boundT) & jump == false)
            {
                if (MapY >= 1)
                {
                    MapY = MapY -1;
                    Py = Game_Pnl.Bottom - 75;
                    Ymovement += 5;
                    MapShift();
                }
            }

            if (Player.IntersectsWith(boundB))
            {
                if (MapY <= 3)
                {
                    MapY = MapY + 1;
                    Py = Game_Pnl.Top + 1;
                    MapShift();
                }
            }
            for (int i = 1; i < RecColour; i++)
            {
                if (PlayerCenter.IntersectsWith(UpS[i]) || PlayerCenter.IntersectsWith(Object[i]))
                {
                    Ymovement = -10;
                }
            }
            if (up == true & jump == true)
            {
                Ymovement = 40;
                jump = false;
            }

            if (Ymovement >= -14)
            {
                Ymovement -= 5;
            }
            Px = Px-Xmovement;
            Py = Py-Ymovement;
            Game_Pnl.Invalidate();
        }
        private void MapShift()
        {          
            for (int O = 1; O<RecColour; O++)
            {
                Object[O] = Rectangle.Empty;
                UpS[O] = Rectangle.Empty;
                DownS[O] = Rectangle.Empty;
                RightS[O] = Rectangle.Empty;
                LeftS[O] = Rectangle.Empty;
            }
            //Left exit/enterence
            if (PlayerMap[MapY, MapX] == 1)
            { //X,Y,Width,height
                RecColour = 9;
                Object[1] = new Rectangle(0, 0, 1000, 50); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(870, 0, 50, 600); // right wall
                Object[4] = new Rectangle(350, 390, 80, 150); // box on floor
                Object[5] = new Rectangle(465, 0, 35, 150); //box coming out of roof
                Object[6] = new Rectangle(250, 0, 25, 185); //box 2 coming out of roof
                Object[7] = new Rectangle(685, 390, 40, 150); // box 2 on floor
                Object[8] = new Rectangle(634, 0, 25, 300); //box 3 coming out of roof              
            }
            if (PlayerMap[MapY, MapX] == 2)
            {   //X,Y,Width,height
                RecColour = 9;
                Object[1] = new Rectangle(0, 0, 1000, 50); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(870, 0, 50, 600); // right wall
                Object[4] = new Rectangle(200, 390, 80, 150); // box 1 on floor
                Object[5] = new Rectangle(300, 260, 50, 40); // floating platform 1
                Object[6] = new Rectangle(800, 300, 50, 40); // floating platform 2
                Object[7] = new Rectangle(500, 300, 250, 40); // floating platform 3
                Object[8] = new Rectangle(400, 360, 50, 40); // floating platform 4               
            }
            if (PlayerMap[MapY, MapX] == 3)
            {//X,Y,Width,height
                RecColour = 8;
                Object[1] = new Rectangle(0, 0, 1000, 50); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(500, 0, 500, 600); // right wall
                Object[4] = new Rectangle(400, 350, 100, 50); // right wall platform
                Object[5] = new Rectangle(275, 440, 50, 150); // Ground box 1
                Object[6] = new Rectangle(275, 235, 100, 25); // Floating box 1
                Object[7] = new Rectangle(75, 235, 100, 25); // Floating box 2
            }
            if (PlayerMap[MapY, MapX] == 4)
            {//X,Y,Width,height
                RecColour = 8;
                Object[1] = new Rectangle(10, 0, 1000, 300); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(870, 0, 50, 600); // right wall
                Object[4] = new Rectangle(230, 390, 80, 150); // box on floor
                Object[5] = new Rectangle(635, 300, 80, 140); // box on roof
                Object[6] = new Rectangle(500, 420, 40, 120); // box 2 on floor
                Object[7] = new Rectangle(400, 460, 40, 120); // box 3 on floor
            }
            //Right exit/enterence
            if (PlayerMap[MapY, MapX] == 5)
            {//X,Y,Width,height
                RecColour = 8;
                Object[1] = new Rectangle(10, 0, 1000, 250); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(0, 0, 50, 600); // left wall
                Object[4] = new Rectangle(670, 390, 80, 150); // box on floor
                Object[5] = new Rectangle(180, 250, 80, 140); // box on roof
                Object[6] = new Rectangle(500, 420, 40, 120); // box 2 on floor
                Object[7] = new Rectangle(350, 390, 40, 120); // box 3 on floor
            }
            if (PlayerMap[MapY, MapX] == 6)
            {//X,Y,Width,height
                RecColour = 10;
                Object[1] = new Rectangle(10, 0, 1000, 100); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(0, 0, 50, 600); // left wall
                Object[4] = new Rectangle(670, 390, 80, 150); // box 1 on floor
                Object[5] = new Rectangle(40, 200, 80, 100); // box on left wall
                Object[6] = new Rectangle(475, 450, 35, 60); // box 2 on floor
                Object[7] = new Rectangle(350, 390, 40, 120); // box 3 on floor
                Object[8] = new Rectangle(250, 310, 40, 120); // box 1 mid air
                Object[9] = new Rectangle(150, 240, 40, 60); // box 2 mid air
            }
            if (PlayerMap[MapY, MapX] == 7)
            {//X,Y,Width,height
                RecColour = 5;
                Object[1] = new Rectangle(10, 0, 1000, 100); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(0, 0, 50, 600); // left wall
                Object[4] = new Rectangle(345, 390, 80, 150); // box 1 on floor
            }
            if (PlayerMap[MapY, MapX] == 8)
            {//X,Y,Width,height
                RecColour = 8;
                Object[1] = new Rectangle(10, 0, 1000, 100); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(0, 0, 50, 600); // left wall
                Object[4] = new Rectangle(345, 390, 80, 150); // box 1 on floor
                Object[5] = new Rectangle(534, 0, 25, 300); //box 1 coming out of roof   
                Object[6] = new Rectangle(265, 0, 35, 150); //box 2 coming out of roof
                Object[7] = new Rectangle(735, 0, 15, 270); //box 1 coming out of roof 
            }
            //Down exit/enterance
            if (PlayerMap[MapY, MapX] == 9)
            {//X,Y,Width,height
                RecColour = 10;
                Object[1] = new Rectangle(0, 10, 225, 600); // left wall
                Object[2] = new Rectangle(0, 540, 1000, 50); // ground
                Object[3] = new Rectangle(750, 10, 300, 600); // right wall
                Object[4] = new Rectangle(415, 410, 150, 150); // box 1 on floor
                Object[5] = new Rectangle(220, 320, 120, 50); // box 1 on wall
                Object[6] = new Rectangle(630, 320, 120, 50); // box 2 on wall
                Object[7] = new Rectangle(415, 210, 150, 50); // floating box 1
                Object[8] = new Rectangle(220, 110, 120, 50); // box 3 on wall
                Object[9] = new Rectangle(630, 110, 120, 50); // box 4 on wall
            }
            if (PlayerMap[MapY, MapX] == 10)
            {//X,Y,Width,height
                RecColour = 11;
                Object[1] = new Rectangle(0, 10, 225, 600); // left wall
                Object[2] = new Rectangle(0, 540, 1000, 50); // ground
                Object[3] = new Rectangle(750, 10, 300, 600); // right wall
                Object[4] = new Rectangle(415, 410, 150, 50); // floating box 1
                Object[5] = new Rectangle(220, 320, 120, 50); // box 1 on wall
                Object[7] = new Rectangle(415, 210, 150, 50); // floating box 2
                Object[9] = new Rectangle(630, 110, 120, 50); // box 2 on wall
                Object[10] = new Rectangle(630, 500, 120, 50); // box 1 on floor
            }
            //Up exit/enterance

            //Elbow rooms

            //T-Rooms

            //Plus-Shape rooms

            for (int O = 1; O <= RecColour; O++)
            {
                UpS[O] = new Rectangle(Object[O].Left, Object[O].Top, Object[O].Width, 10);
                RightS[O] = new Rectangle(Object[O].Right - 5, Object[O].Top + 5, 5, Object[O].Height - 5);
                LeftS[O] = new Rectangle(Object[O].Left, Object[O].Top + 5, 5, Object[O].Height - 5);
                DownS[O] = new Rectangle(Object[O].Left, Object[O].Bottom - 5, Object[O].Width, 5);
            }
        }
        
        // Random level generation
        public void GenLvl()
        {
            int[,] CorrectPath = new int[5, 5]
{
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0}
};
            int PathLength, CurrentLength, Yarrmin, Yarrmax, Xarrmin, Xarrmax, Nextdir, OriginX, OriginY;
            bool Left,Up,Down,Right;
            string PathRepeat = "";
            Random R = new System.Random();
            PathX = R.Next(0, 5);
            PathY = R.Next(0, 5);
            OriginX = PathX;
            OriginY = PathY;
            MapX = PathX;
            MapY = PathY;
            MapShift();
            label1.Text = MapX +","+ MapY;
            PathLength = R.Next(7,9);
            CurrentLength = 0;
            Yarrmin = 0;
            Yarrmax = 4;
            Xarrmin = 0;
            Xarrmax = 4;
            CorrectPath[PathY, PathX] = 1;
            while (CurrentLength < PathLength)
            {
                //1 means up PathY--
                //2 means right PathX++
                //3 means down PathY++
                //4 means left PathX--
             Nextdir = R.Next(1, 5);
                if (Nextdir == 1)
                {
                    PathY--;
                    if (PathY < Yarrmin || CorrectPath[PathY, PathX] == 1)
                    {
                        PathY++;
                    }      
                    else if (CorrectPath[PathY, PathX] == 0)
                    {
                        PathRepeat = PathRepeat + "U";
                    }
                }
                else if (Nextdir == 2)
                {
                    PathX++;
                    if (PathX > Xarrmax || CorrectPath[PathY, PathX] == 1)
                    {
                        PathX--;
                    }
                    else if (CorrectPath[PathY, PathX] == 0)
                    {
                        PathRepeat = PathRepeat + "R";
                    }
                }
                else if (Nextdir == 3)
                {
                    PathY++;
                    if (PathY > Yarrmax || CorrectPath[PathY, PathX] == 1)
                    {
                        PathY--;
                    }
                    else if (CorrectPath[PathY, PathX] == 0)
                    {
                        PathRepeat = PathRepeat + "D";
                    }
                }
                else if (Nextdir == 4)
                {
                    PathX--;                  
                    if (PathX < Xarrmin || CorrectPath[PathY, PathX] == 1)
                    {
                        PathX++;
                    }
                    else if (CorrectPath[PathY, PathX] == 0)
                    {
                        PathRepeat = PathRepeat + "L";
                    }
                }
                if (CorrectPath[PathY, PathX] == 0)
                {
                    Down = false;
                    Up = false;
                    Left = false;
                    Right = false;
                    CorrectPath[PathY, PathX] = 1;
                    CurrentLength++;
                    if (CurrentLength == PathLength)
                    {
                        PathX = OriginX;
                        PathY = OriginY;
                        for (int i = 1; i <= CurrentLength; i++)
                        {
                            try
                            {
                                if (CorrectPath[PathY + 1, PathX] == 1)
                                {
                                    Down = true;
                                }
                            }
                            catch (Exception)
                            {
                                Down = false;
                            }
                            try
                            {
                                if (CorrectPath[PathY - 1, PathX] == 1)
                                {
                                    Up = true;
                                }
                            }
                            catch (Exception)
                            {
                                Up = false;
                            }
                            try
                            {
                                if (CorrectPath[PathY, PathX + 1] == 1)
                                {
                                    Right = true;
                                }
                            }
                            catch (Exception)
                            {
                                Right = false;
                            }
                            try
                            {
                                if (CorrectPath[PathY, PathX - 1] == 1)
                                {
                                    Left = true;
                                }
                            }
                            catch (Exception)
                            {
                                Left = false;
                            }

                            //RANDOM GENERATION CHECK FOR PICKING MAP TITLES
                            //TOTAL MAP TILES IF CHECKING FOR EACH POSSIBLE COMBINATION: 60
                            //SINGLE DIRECTION CHECK
                            if (Left == true && Right == false && Up == false && Down == false)//Map tile 1-4
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Right == true && Left == false && Up == false && Down == false)//Map tile 5-8
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Down == true && Right == false && Up == false && Left == false)//Map tile 9-12
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Up == true && Right == false && Left == false && Down == false)//Map tile 13-16
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            //DUAL DIRECTION CHECK
                            if (Left == true && Right == true && Up == false && Down == false)//Map tile  17-20
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Left == true && Up == true && Up == false && Down == false)//Map tile 21-24
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Left == true && Down == true && Up == false && Right == false)//Map tile 25-28
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Right == true && Up == true && Left == false && Down == false)//Map tile 29-32
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Right == true && Down == true && Up == false && Left == false)//Map tile 33-36
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Up == true && Down == true && Left == false && Right == false)//Map tile 37-40
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }
                            //TRIPLE DIRECTION CHECK
                            if (Left == true && Right == true && Up == true && Down == false)//Map tile 41-44
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Left == true && Right == true && Down == true && Up == false)//Map tile 45-48
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Left == true && Down == true && Up == true && Right == false)//Map tile 49-52
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            if (Right == true && Down == true && Up == true && Left == false)//Map tile 53-56
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }

                            //QUAD DIRECTION CHECK
                            if (Left == true && Right == true && Up == true && Down == true)//Map tile 57-60
                            {
                                PlayerMap[PathY, PathX] = R.Next(0, 4);
                            }
                            //U means up PathY--
                            //R means right PathX++
                            //D means down PathY++
                            //L means left PathX--
                            if ( PathRepeat.Substring(0,i) == "L")
                            {
                                PathX--;
                            }
                            else if (PathRepeat.Substring(0, i) == "D")
                            {
                                PathY++;
                            }
                            else if (PathRepeat.Substring(0, i) == "R")
                            {
                                PathX++;
                            }
                            else if (PathRepeat.Substring(0, i) == "U")
                            {
                                PathY--;
                            }
                        }
                    }
                }
                //write and write line dont work for some reason...   so debug is used here instead          
                Debug.AutoFlush = true;
                Debug.Indent();
                Debug.WriteLine(PathY, PathX + PathRepeat +"," + CurrentLength );
                Debug.Unindent();
            }
        }
        private void Torch_Tmr_Tick(object sender, EventArgs e)
        {
            if(Fuel >= 0 & Fuel <= 1)
            {
                Fuel -= 0.01;
                Xshift = (int)((1-Fuel)*100 + 2);
            } 
            else
            {
                Fuel = 0;
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { left = true; }
            if (e.KeyData == Keys.Right) { right = true; }
            if (e.KeyData == Keys.Up) { up = true; }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { left = false; }
            if (e.KeyData == Keys.Right) { right = false; }
            if (e.KeyData == Keys.Up) { up = false; }
            if (e.KeyData == Keys.Escape)
            {
                Load_Btn.Enabled = true;
                Load_Btn.Visible = true;
                Return_Btn.Enabled = true;
                Return_Btn.Visible = true;
                Save_Btn.Enabled = true;
                Save_Btn.Visible = true;
                Framerate.Enabled = false;
                Torch_Tmr.Enabled = false;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            //use the DrawImage method to draw the spaceship on the panel
            //use the DrawImage method to draw the planet on the panel
            e.Graphics.FillRectangle(Brushes.Black, Player);
            for( int i = 1; i<RecColour; i++)
            {
                e.Graphics.FillRectangle(Brushes.Black, Object[i]);
            }          
            e.Graphics.FillRectangle(Brushes.BurlyWood, Stick);
            var rgn = new Region(new Rectangle(0, 0, 1000, 1000));
            var path = new GraphicsPath();
            if (Fuel <= 1 & Fuel >=0.6)
           { 
                path.AddEllipse(Px - 120 + Xshift, Py - 75, (int)(LightArea * (Fuel + 0.2)), (int)(LightArea * (Fuel + 0.2)));
            }
           else if (Fuel <= 0.6 & Fuel >= 0.4)
            {
                path.AddEllipse(Px - 120 + Xshift, Py - 50, (int)(LightArea * (Fuel + 0.2)), (int)(LightArea * (Fuel + 0.2)));

            }
           else if (Fuel <= 0.4 & Fuel >= 0.01)
            {
                path.AddEllipse(Px - 110 + Xshift, Py - 30, (int)(LightArea * (Fuel + 0.2)), (int)(LightArea * (Fuel + 0.2)));
            }
            rgn.Exclude(path);
            //e.Graphics.FillRegion(Brushes.Black, rgn);
            e.Graphics.FillRectangle(Brushes.OrangeRed, LightBar);
        }
    }
}
