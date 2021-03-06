using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
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
        Rectangle Player, PlayerCenter, LSide, TSide, BSide, RSide, LightBar, BoundL, BoundR, BoundT, BoundB, Escape;
        string line;
        int Py, Px, LightArea = 250, LightBarLngth, Xmovement, Ymovement, Xshift, MapX, MapY, RecColour, Difficulty, Level;
        double Fuel = 1.0;
        bool left, right, up, jump, start;
        int[,] Var = new int[10, 2]
        {
            {0,0},//Px
            {1,0},//Py
            {2,0},//MapX
            {3,0},//MapY
            {4,0},//Level
            {5,0},//Difficulty 
            {6,0},
            {7,0},
            {8,0},
            {9,0}

        };
        int[,] PlayerMap = new int[5, 5]
        {
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0}
        };
        int[,] Level1 = new int[5, 5]
        {
            {5,30,1,0,0},
            {0,9,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0}
        };
        int[,] Level2 = new int[5, 5]
        {
            {6,26,1,0,0},
            {7,29,3,0,0},
            {0,22,0,0,0},
            {0,30,0,0,0},
            {0,10,0,0,0}
        };
        int[,] Level3 = new int[5, 5]
        {
            {5,26,26,2,0},
            {0,9,29,0,0},
            {0,30,25,4,0},
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
            Level = 1;
            Py = 120;
            Px = 415;
            PlayerMap = Level1;
            start = false;
            Fuel_Lbl.Hide();
            MessageBox.Show("Arrow keys to move. You need to escape the cave before your fuel runs out. You can escape by finding the white door at the end of each level.");
            //GenLvl(); this doesnt work :(
            MapShift();
            BoundB = new Rectangle(0, Game_Pnl.Bottom, Game_Pnl.Width, 5);
            BoundL = new Rectangle(0, 0, 5, Game_Pnl.Height);
            BoundR = new Rectangle(Game_Pnl.Right - 5, 0, 5, Game_Pnl.Height);
            BoundT = new Rectangle(0, Game_Pnl.Top, Game_Pnl.Width, 5);
        }

        private void Save_Btn_Click(object sender, EventArgs e)
        {
            Var[0, 1] = Px;
            Var[1, 1] = Py;
            Var[2, 1] = MapX;
            Var[3, 1] = MapY;
            Var[4, 1] = Level;
            Var[5, 1] = Difficulty;
            TextWriter Save = new StreamWriter(Application.StartupPath + @"\Test.txt");

            //Writing text to the file.
            Save.WriteLine("Hello this line is skipped by the loading system so I just added this here and wanted to say hi!");
            Save.WriteLine(Var[0, 1]);
            Save.WriteLine(Var[1, 1]);
            Save.WriteLine(Var[2, 1]);
            Save.WriteLine(Var[3, 1]);
            Save.WriteLine(Var[4, 1]);
            Save.WriteLine(Var[5, 1]);
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
            Level = Var[4, 1];
            Difficulty = Var[5, 1];
            System.Windows.Forms.MessageBox.Show("File Loaded");
            file.Close();
            if (Level == 2)
            {
                PlayerMap = Level2;
            }
            if (Level == 3)
            {
                PlayerMap = Level3;
            }
            MapShift();
            // Suspend the screen.  
            // System.Windows.Forms.MessageBox.Show(counter + "");

        }
        private void Extreme_Btn_Click(object sender, EventArgs e)
        {
            Diff_Lbl.Text = "Difficulty:Extreme";
            Difficulty = 3;
        }

        private void Hard_Btn_Click(object sender, EventArgs e)
        {
            Diff_Lbl.Text = "Difficulty:Hard";
            Difficulty = 2;
        }

        private void Normal_Btn_Click(object sender, EventArgs e)
        {
            Diff_Lbl.Text = "Difficulty:Normal";
            Difficulty = 1;
        }

        private void NameSave_Btn_Click(object sender, EventArgs e)
        {
            PlayerName_TxtBox.Enabled = false;
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
            NameSave_Btn.Visible = false;
            NameSave_Btn.Enabled = false;
            Normal_Btn.Visible = false;
            Normal_Btn.Enabled = false;
            Hard_Btn.Visible = false;
            Hard_Btn.Enabled = false;
            Extreme_Btn.Visible = false;
            Extreme_Btn.Enabled = false;
            PlayerName_TxtBox.Enabled = false;
            Diff_Lbl.Visible = false;
            start = true;
            Title_Lbl.Visible = false;
            Fuel_Lbl.Show();
        }
        private void Framerate_Tick(object sender, EventArgs e)
        {
            LightBarLngth = (int)Math.Round(100 * Fuel);
            Player = new Rectangle(Px, Py, 35, 70);//Player Rectangle
            PlayerCenter = new Rectangle(Px + 6, Py + 10, 22, 40);
            LSide = new Rectangle(Px, Py, 5, 70);//Player Rectangle
            TSide = new Rectangle(Px + 5, Py, 25, 10);//Player Rectangle
            BSide = new Rectangle(Px + 5, Py + 60, 25, 10);//Player Rectangle
            RSide = new Rectangle(Px + 30, Py, 5, 70);//Player Rectangle
            LightBar = new Rectangle(15, 50, LightBarLngth, 20);
            Game_Pnl.Invalidate();
            for (int i = 1; i < RecColour; i++)
            {
                if (left == true & !LSide.IntersectsWith(RightS[i]))
                {
                    Xmovement = 5;
                }
                else if (left == true & LSide.IntersectsWith(RightS[i]))
                {
                    Px = RightS[i].Right + 1;
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
                    Px = LeftS[i].Left - 38;
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

            if (Player.IntersectsWith(BoundL) & left == true)
            {
                if (MapX >= 1)
                {
                    MapX = MapX - 1;
                    MapShift();
                    Px = Game_Pnl.Right;
                }
            }
            //changing screens
            if (Player.IntersectsWith(BoundR) & right == true)
            {
                if (MapX <= 3)
                {
                    MapX = MapX + 1;
                    MapShift();
                    Px = Game_Pnl.Left - 10;
                }
            }
            //changing screens
            if (Player.IntersectsWith(BoundT) & jump == false)
            {
                if (MapY >= 1)
                {
                    MapY = MapY - 1;
                    MapShift();
                    Py = Game_Pnl.Bottom - 75;
                    Ymovement += 5;
                }
            }
            //changing screens
            if (Player.IntersectsWith(BoundB))
            {
                if (MapY <= 3)
                {
                    MapY = MapY + 1;
                    MapShift();
                    Py = Game_Pnl.Top + 1;
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
            //Level check
            if (PlayerCenter.IntersectsWith(Escape))
            {
                Level++;
                if (Level == 2)
                {
                    PlayerMap = Level2;
                }
                if (Level == 3)
                {
                    PlayerMap = Level3;
                }
                if (Level == 4)
                {
                    MessageBox.Show("You escaped the cave! Score:" + (int)(Fuel * 1000) + "Points");
                    this.Close();
                }
                MapX = 0;
                MapY = 0;
                Fuel += 0.1;
                MapShift();
            }
            Px = Px - Xmovement;
            Py = Py - Ymovement;
            Game_Pnl.Invalidate();
        }

        private void MapShift()
        {
            //debug info
            Debug.WriteLine(MapY, MapX + "");
            // setting up the rectangles to be loaded
            Escape = Rectangle.Empty;
            for (int O = 0; O < RecColour; O++)
            {
                Object[O] = Rectangle.Empty;
                UpS[O] = Rectangle.Empty;
                DownS[O] = Rectangle.Empty;
                RightS[O] = Rectangle.Empty;
                LeftS[O] = Rectangle.Empty;
            }
            //Each number is a different screen type that the game can load
            //Left exit/enterence X4
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
            //Right exit/enterence X4
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
                Object[7] = new Rectangle(735, 0, 15, 270); //box 3 coming out of roof 
            }
            //Down exit/enterance X4
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
                RecColour = 10;
                Object[1] = new Rectangle(0, 10, 225, 600); // left wall
                Object[2] = new Rectangle(0, 540, 1000, 50); // ground
                Object[3] = new Rectangle(750, 10, 300, 600); // right wall
                Object[4] = new Rectangle(415, 410, 150, 50); // floating box 1
                Object[5] = new Rectangle(220, 320, 120, 50); // box 1 on wall
                Object[6] = new Rectangle(415, 210, 150, 50); // floating box 2
                Object[7] = new Rectangle(630, 110, 120, 50); // box 2 on wall
                Object[8] = new Rectangle(630, 500, 120, 50); // box 1 on floor
                Object[9] = new Rectangle(210, 110, 120, 50); // box 2 on wall
            }
            //Up exit/enterance X4
            if (PlayerMap[MapY, MapX] == 11)
            {//X,Y,Width,height
                RecColour = 11;
                Object[1] = new Rectangle(0, 10, 225, 600); // left wall
                Object[2] = new Rectangle(0, 0, 1000, 20); // ground
                Object[3] = new Rectangle(750, 10, 300, 600); // right wall
            }
            if (PlayerMap[MapY, MapX] == 12)
            {//X,Y,Width,height
                RecColour = 11;
                Object[1] = new Rectangle(0, 10, 225, 600); // left wall
                Object[2] = new Rectangle(0, 0, 1000, 20); // ground
                Object[3] = new Rectangle(750, 10, 300, 600); // right wall
                Object[4] = new Rectangle(415, 500, 150, 50); // floating box 1
                Object[5] = new Rectangle(220, 420, 120, 50); // box 1 on wall
                Object[6] = new Rectangle(415, 310, 150, 50); // floating box 2
                Object[7] = new Rectangle(630, 210, 120, 50); // box 2 on wall
            }
            //Elbow rooms
            //Left Up
            if (PlayerMap[MapY, MapX] == 13)
            {//X,Y,Width,height
                RecColour = 8;
                Object[1] = new Rectangle(0, 0, 225, 100); // left wall
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(750, 10, 300, 600); // right wall
                Object[4] = new Rectangle(630, 310, 120, 50); // box 1 on wall
                Object[5] = new Rectangle(500, 440, 80, 100); // box 1 on floor
                Object[6] = new Rectangle(430, 210, 150, 50); // floating box 1
                Object[7] = new Rectangle(230, 110, 150, 50); // floating box 2
            }
            if (PlayerMap[MapY, MapX] == 14)
            {//X,Y,Width,height
                RecColour = 8;
                Object[1] = new Rectangle(0, 0, 225, 100); // left wall
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(750, 0, 300, 600); // right wall
                Object[4] = new Rectangle(430, 210, 150, 50); // floating box 1
                Object[5] = new Rectangle(430, 410, 150, 50); // ground box 1
                Object[6] = new Rectangle(630, 310, 120, 50); // box 1 on wall
                Object[7] = new Rectangle(630, 110, 120, 50); // box 2 on wall
            }
            //Left Down
            if (PlayerMap[MapY, MapX] == 15)
            {//X,Y,Width,height
                RecColour = 4;
                Object[1] = new Rectangle(0, 0, 750, 100); // left wall
                Object[2] = new Rectangle(0, 510, 225, 50); // this is the ground
                Object[3] = new Rectangle(750, 0, 300, 600); // right wall
            }
            if (PlayerMap[MapY, MapX] == 16)
            {//X,Y,Width,height
                RecColour = 7;
                Object[1] = new Rectangle(0, 0, 750, 100); // left wall
                Object[2] = new Rectangle(0, 510, 225, 50); // this is the ground
                Object[3] = new Rectangle(750, 0, 300, 600); // right wall
                Object[4] = new Rectangle(534, 100, 25, 300); //box 1 coming out of roof   
                Object[5] = new Rectangle(265, 100, 20, 150); //box 2 coming out of roof
                Object[6] = new Rectangle(135, 100, 15, 270); //box 3 coming out of roof 
            }
            //Right Up
            if (PlayerMap[MapY, MapX] == 17)
            {//X,Y,Width,height
                RecColour = 9;
                Object[1] = new Rectangle(0, 0, 225, 600); // left wall
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(750, 0, 300, 160); // right wall
                Object[4] = new Rectangle(630, 310, 120, 50); // box 1 on wall
                Object[5] = new Rectangle(500, 440, 80, 100); // box 1 on floor
                Object[6] = new Rectangle(430, 210, 150, 50); // floating box 1
                Object[7] = new Rectangle(230, 110, 150, 50); // floating box 2
                Object[8] = new Rectangle(630, 110, 120, 50); // box 2 on wall
            }
            if (PlayerMap[MapY, MapX] == 18)
            {//X,Y,Width,height
                RecColour = 9;
                Object[1] = new Rectangle(0, 0, 225, 600); // left wall
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(750, 0, 300, 160); // right wall
                Object[4] = new Rectangle(570, 330, 120, 100); // floating box 1
                Object[5] = new Rectangle(650, 400, 80, 30); // floating box 1
                Object[6] = new Rectangle(430, 210, 150, 50); // floating box 2
                Object[7] = new Rectangle(230, 110, 150, 50); // box 1 on wall
                Object[8] = new Rectangle(630, 110, 120, 50); // box 2 on wall
            }
            //Right Down
            if (PlayerMap[MapY, MapX] == 19)
            {//X,Y,Width,height
                RecColour = 6;
                Object[1] = new Rectangle(0, 0, 225, 600); // left wall
                Object[2] = new Rectangle(0, 0, 1000, 160); // right wall
                Object[3] = new Rectangle(750, 510, 300, 160); // ground 
                Object[4] = new Rectangle(434, 100, 25, 300); //box 1 coming out of roof   
                Object[5] = new Rectangle(635, 100, 15, 270); //box 2 coming out of roof 
            }
            if (PlayerMap[MapY, MapX] == 20)
            {//X,Y,Width,height
                RecColour = 4;
                Object[1] = new Rectangle(0, 0, 225, 600); // left wall
                Object[2] = new Rectangle(0, 0, 1000, 160); // right wall
                Object[3] = new Rectangle(750, 510, 300, 160); // ground 
            }
            //Hallway rooms
            //Up Down
            if (PlayerMap[MapY, MapX] == 21)
            {//X,Y,Width,height
                RecColour = 11;
                Object[1] = new Rectangle(0, 10, 225, 600); // left wall
                Object[2] = new Rectangle(750, 10, 300, 600); // right wall
                Object[3] = new Rectangle(220, 320, 120, 50); // box 1 on wall
                Object[4] = new Rectangle(630, 320, 120, 50); // box 2 on wall
                Object[5] = new Rectangle(415, 210, 150, 50); // floating box 1
                Object[6] = new Rectangle(220, 110, 120, 50); // box 3 on wall
                Object[7] = new Rectangle(630, 110, 120, 50); // box 4 on wall
                Object[8] = new Rectangle(415, 410, 150, 50); // floating box 2
                Object[9] = new Rectangle(220, 510, 120, 50); // box 5 on wall
                Object[10] = new Rectangle(630, 510, 120, 50); // box 6 on wall
            }
            if (PlayerMap[MapY, MapX] == 22)
            {//X,Y,Width,height
                RecColour = 11;
                Object[1] = new Rectangle(0, 10, 225, 600); // left wall
                Object[2] = new Rectangle(750, 10, 300, 600); // right wall
                Object[4] = new Rectangle(630, 320, 120, 50); // box 2 on wall
                Object[5] = new Rectangle(415, 210, 150, 50); // floating box 1
                Object[6] = new Rectangle(220, 110, 120, 50); // box 3 on wall
                Object[7] = new Rectangle(630, 110, 120, 50); // box 3 on wall
                Object[8] = new Rectangle(415, 410, 150, 50); // floating box 2
                Object[10] = new Rectangle(630, 510, 120, 50); // box 6 on wall
            }
            //Left,Right
            if (PlayerMap[MapY, MapX] == 23)
            {//X,Y,Width,height
                RecColour = 4;
                Object[1] = new Rectangle(0, 0, 1000, 50); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
            }
            if (PlayerMap[MapY, MapX] == 24)
            {//X,Y,Width,height
                RecColour = 11;
                Object[1] = new Rectangle(0, 0, 1000, 50); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(465, 0, 35, 150); //box coming out of roof
                Object[4] = new Rectangle(250, 0, 25, 185); //box 2 coming out of roof
                Object[5] = new Rectangle(165, 0, 35, 150); //box 3 coming out of roof
                Object[6] = new Rectangle(650, 0, 25, 185); //box 4 coming out of roof
                Object[7] = new Rectangle(285, 410, 30, 100); // box 1 on floor
                Object[8] = new Rectangle(465, 380, 35, 130); // box 2 on floor
                Object[9] = new Rectangle(635, 390, 40, 150); // box 3 on floor
                Object[10] = new Rectangle(385, 350, 35, 160); // box 4 on floor
            }
            //T-Rooms
            //left right up
            if (PlayerMap[MapY, MapX] == 25)
            {//X,Y,Width,height
                RecColour = 10;
                Object[1] = new Rectangle(0, 0, 225, 100); //this is a roof
                Object[2] = new Rectangle(0, 510, 1000, 50); // this is the ground
                Object[3] = new Rectangle(750, 0, 300, 100); // right wall
                Object[4] = new Rectangle(220, 320, 120, 50); //Floating box 1
                Object[5] = new Rectangle(630, 320, 120, 50); //Floating box 2
                Object[6] = new Rectangle(415, 210, 150, 50); //Floating box 3
                Object[7] = new Rectangle(220, 110, 120, 50); //Floating box 4
                Object[8] = new Rectangle(630, 110, 120, 50); //Floating box 5
                Object[9] = new Rectangle(415, 410, 150, 50); // floating box 6
            }
            //left right down
            if (PlayerMap[MapY, MapX] == 26)
            {//X,Y,Width,height
                RecColour = 8;
                Object[1] = new Rectangle(0, 0, 1000, 100); //this is a roof
                Object[2] = new Rectangle(0, 510, 225, 50); // Left ground
                Object[3] = new Rectangle(750, 0, 300, 100); // right wall
                Object[4] = new Rectangle(750, 510, 300, 50); // Right ground
                Object[5] = new Rectangle(225, 350, 500, 50); // floating platform
                Object[6] = new Rectangle(145, 460, 35, 50); // box on ground left
                Object[7] = new Rectangle(795, 460, 35, 50); // box on ground right
            }
            //left down up
            if (PlayerMap[MapY, MapX] == 27)
            {//X,Y,Width,height
                RecColour = 12;
                Object[1] = new Rectangle(0, 0, 225, 100); //this is a roof
                Object[2] = new Rectangle(0, 510, 225, 50); // Left ground
                Object[3] = new Rectangle(750, 0, 300, 100); // right wall
                Object[4] = new Rectangle(750, 0, 300, 1000); // right wall
                Object[5] = new Rectangle(220, 320, 120, 50); //Floating box 1
                Object[6] = new Rectangle(630, 320, 120, 50); //Floating box 2
                Object[7] = new Rectangle(415, 210, 150, 50); //Floating box 3
                Object[8] = new Rectangle(220, 110, 120, 50); //Floating box 4
                Object[9] = new Rectangle(630, 110, 120, 50); //Floating box 5
                Object[10] = new Rectangle(415, 410, 150, 50); // floating box 6
                Object[11] = new Rectangle(145, 460, 35, 50); // box on ground left
            }
            //right down up
            if (PlayerMap[MapY, MapX] == 28)
            {//X,Y,Width,height
                RecColour = 12;
                Object[1] = new Rectangle(0, 0, 225, 100); //this is a roof
                Object[2] = new Rectangle(0, 0, 225, 1000); // left wall
                Object[3] = new Rectangle(750, 0, 300, 50); // right roof
                Object[4] = new Rectangle(750, 510, 300, 50); // ground right
                Object[5] = new Rectangle(220, 320, 120, 50); //Floating box 1
                Object[6] = new Rectangle(630, 320, 120, 50); //Floating box 2
                Object[7] = new Rectangle(415, 210, 150, 50); //Floating box 3
                Object[8] = new Rectangle(220, 110, 120, 50); //Floating box 4
                Object[9] = new Rectangle(630, 110, 120, 50); //Floating box 5
                Object[10] = new Rectangle(415, 410, 150, 50); // floating box 6
                Object[11] = new Rectangle(820, 460, 35, 50); // box on ground right
            }
            //Plus-Shape rooms X1
            if (PlayerMap[MapY, MapX] == 29)
            {//X,Y,Width,height
                RecColour = 13;
                Object[1] = new Rectangle(0, 0, 225, 100); //Left roof
                Object[2] = new Rectangle(0, 510, 225, 50); // Left floor
                Object[3] = new Rectangle(750, 0, 300, 100); // right wall
                Object[4] = new Rectangle(750, 510, 300, 100); // right floor
                Object[5] = new Rectangle(220, 320, 120, 50); //Floating box 1
                Object[6] = new Rectangle(630, 320, 120, 50); //Floating box 2
                Object[7] = new Rectangle(415, 210, 150, 50); //Floating box 3
                Object[8] = new Rectangle(220, 110, 120, 50); //Floating box 4
                Object[9] = new Rectangle(630, 110, 120, 50); //Floating box 5
                Object[10] = new Rectangle(415, 410, 150, 50); // floating box 6
                Object[11] = new Rectangle(145, 460, 35, 50); // box on ground left
                Object[12] = new Rectangle(795, 460, 35, 50); // box on ground right
            }
            //Exit room
            if (PlayerMap[MapY, MapX] == 30)
            {//X,Y,Width,height
                RecColour = 13;
                Object[1] = new Rectangle(0, 0, 225, 100); //Left roof
                Object[2] = new Rectangle(0, 510, 225, 50); // Left floor
                Object[3] = new Rectangle(750, 0, 300, 100); // right wall
                Object[4] = new Rectangle(750, 510, 300, 100); // right floor
                Object[5] = new Rectangle(220, 320, 120, 50); //Floating box 1
                Object[6] = new Rectangle(630, 320, 120, 50); //Floating box 2
                Object[7] = new Rectangle(415, 210, 150, 50); //Floating box 3
                Object[8] = new Rectangle(220, 110, 120, 50); //Floating box 4
                Object[9] = new Rectangle(630, 110, 120, 50); //Floating box 5
                Object[10] = new Rectangle(415, 410, 150, 50); // floating box 6
                Object[11] = new Rectangle(145, 460, 35, 50); // box on ground left
                Object[12] = new Rectangle(795, 460, 35, 50); // box on ground right
                Escape = new Rectangle(440, 310, 50, 100); //Exit
            }
            //Creates hitboxes for each object on screen
            for (int O = 1; O <= RecColour; O++)
            {
                UpS[O] = new Rectangle(Object[O].Left, Object[O].Top, Object[O].Width, 10);
                RightS[O] = new Rectangle(Object[O].Right - 5, Object[O].Top + 5, 5, Object[O].Height - 5);
                LeftS[O] = new Rectangle(Object[O].Left, Object[O].Top + 5, 5, Object[O].Height - 5);
                DownS[O] = new Rectangle(Object[O].Left, Object[O].Bottom - 5, Object[O].Width, 5);
            }
        }

        private void Torch_Tmr_Tick(object sender, EventArgs e)
        {

            //Checks if the player has no more fuel and will trigger game over and shut the program down
            if (Fuel <= 0)
            {
                Torch_Tmr.Enabled = false;
                MessageBox.Show("Your last embers of light flicker out and you become lost in the darkness. Game Over, you made it to level " + Level + "!");
                this.Close();
            }
            //This checks that the fuel is between 0 and 1 and if it is not fuel is set to 0
            //This also changes the shift of the vision circle by running a calulation based of the fuel value
            // This is also based off difficulty level
            if (Fuel >= 0.01 & Fuel <= 1)
            {
                if (Difficulty == 1)
                {
                    Fuel -= 0.01;
                }
                if (Difficulty == 2)
                {
                    Fuel -= 0.02;
                }
                if (Difficulty == 3)
                {
                    Fuel -= 0.04;
                }
                Xshift = (int)((1 - Fuel) * 100 + 2);
            }
            else if (Fuel > 1)
            {
                Fuel = 1;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //key dectection when pressed down
            if (e.KeyData == Keys.Left)
            {
                left = true;
            }
            if (e.KeyData == Keys.Right)
            {
                right = true;
            }
            if (e.KeyData == Keys.Up)
            {
                up = true;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //checking when a key is no longer being pressed
            if (e.KeyData == Keys.Left)
            {
                left = false;
            }
            if (e.KeyData == Keys.Right)
            {
                right = false;
            }
            if (e.KeyData == Keys.Up)
            {
                up = false;
            }
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
            //use the FillRectangle method to fill in the objects so the player can see where the platforms are
            e.Graphics.FillRectangle(Brushes.Black, Player);
            for (int i = 1; i < RecColour; i++)
            {
                e.Graphics.FillRectangle(Brushes.Black, Object[i]);
            }
            var rgn = new Region(new Rectangle(0, 0, 1000, 1000));
            var path = new GraphicsPath();
            if (Fuel <= 1 & Fuel >= 0.6)
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
            if (start == true)
            {
                rgn.Exclude(path);
            }
            e.Graphics.FillRegion(Brushes.Black, rgn);
            e.Graphics.FillRectangle(Brushes.OrangeRed, LightBar);
            e.Graphics.FillRectangle(Brushes.White, Escape);
        }
    }
}
