using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        Rectangle[] UpS = new Rectangle[7];
        Rectangle[] DownS = new Rectangle[7];
        Rectangle[] LeftS = new Rectangle[7];
        Rectangle[] RightS = new Rectangle[7];
        Rectangle[] Object = new Rectangle[7];
        Rectangle Player, PlayerCenter, LSide, TSide, BSide, RSide, LightBar, Stick, boundL, boundR, boundT, boundB;
        string line;
        int Py, Px, LightArea = 250, LightBarLngth, Xmovement, Ymovement, Xshift, MapX, MapY;
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
            {0,5,0,0,0},
            {7,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0}
        };
        Image player = Image.FromFile(Application.StartupPath + @"\Player.png");
        public Form1()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, Game_Pnl, new object[] { true });
            Framerate.Enabled = false;
            Ymovement = -5;
            Xmovement = 0;
            Py = 300;
            MapX = 1;
            MapY = 1;
            MapShift();
            GenLvl();
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
            Player = new Rectangle(Px, Py, 50, 100);//Player Rectangle
            PlayerCenter = new Rectangle(Px+12, Py+15, 26, 65);
            LSide = new Rectangle(Px, Py, 5, 100);//Player Rectangle
            TSide = new Rectangle(Px+5, Py, 40, 15);//Player Rectangle
            BSide = new Rectangle(Px+5, Py+90, 40, 10);//Player Rectangle
            RSide = new Rectangle(Px+45, Py, 5, 100);//Player Rectangle
            LightBar = new Rectangle(685,50 ,LightBarLngth, 20);
            Game_Pnl.Invalidate();
            for (int i = 1; i < 7; i++)
            {
                if (left == true & !LSide.IntersectsWith(RightS[i]))
                {
                    Xmovement = 5;
                }
                else if (left == true & LSide.IntersectsWith(RightS[i]))
                {
                    Px = RightS[i].Right+4;
                }
                else
                {
                    Xmovement = 0;
                }
            }
            for (int i = 1; i < 7; i++)
            {
                if (right == true & !RSide.IntersectsWith(LeftS[i]))
                {
                    Xmovement = -5;
                }
                else if (right == true & RSide.IntersectsWith(LeftS[i]))
                {
                    Px = LeftS[i].Left- 54;
                }
            }
            for (int i = 1; i < 7; i++)
            {
                if (BSide.IntersectsWith(UpS[i]))
                {
                    Ymovement = 0;
                    Py =UpS[i].Top - 100;
                    jump = true;
                }

                if (up == true & jump == true)
                {
                    Ymovement = 40;
                    jump = false;
                }
                else if (!BSide.IntersectsWith(UpS[i]) & Ymovement >= -15)
                {
                    Ymovement = Ymovement - 1;
                }
            }
            for (int i = 1; i < 7; i++)
            {
                if (TSide.IntersectsWith(DownS[i]))
                {
                    Ymovement = 0;
                    Py = DownS[i].Bottom + 1;
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
                    Px = Game_Pnl.Right - 50;
                    MapShift();
                }
            }

            if (Player.IntersectsWith(boundR) & right == true)
            {
                if (MapX <= 2)
                {
                    MapX = MapX + 1;
                    Px = Game_Pnl.Left + 50;
                    MapShift();
                }
            }

            if (Player.IntersectsWith(boundT) & jump == false)
            {
                if (MapY <= 1)
                {
                    MapY = MapY - 1;
                    Py = Game_Pnl.Bottom - 75;
                    Ymovement = Ymovement +10;
                    MapShift();
                }
            }

            if (Player.IntersectsWith(boundB))
            {
                if (MapY <= 1)
                {
                    MapY = MapY + 1;
                    Py = Game_Pnl.Top + 10;
                    MapShift();
                }
            }
           for (int i = 1; i < 7; i++)
           {
               if (PlayerCenter.IntersectsWith(Object[i]))
               {
                   Ymovement = 0;
                   Py = DownS[i].Bottom + 1;
               }
           }
            Px = Px-Xmovement;
            Py = Py-Ymovement;
            Game_Pnl.Invalidate();
        }
        private void MapShift()
        {
            for (int O = 1; O<7; O++)
            {
                Object[O] = Rectangle.Empty;
                UpS[O] = Rectangle.Empty;
                DownS[O] = Rectangle.Empty;
                RightS[O] = Rectangle.Empty;
                LeftS[O] = Rectangle.Empty;
            }
            if (PlayerMap[MapY, MapX] == 0)
            {
                Object[1] = new Rectangle(0, 400, 5000, 50); // this is the ground
                Object[2] = new Rectangle(500, 300, 100, 50);
                Object[3] = new Rectangle(300, 250, 150, 30);
                Object[4] = new Rectangle(100, 150, 150, 30);
                Stick = new Rectangle(375, 200, 50, 50);
                for (int O = 1; O < 7; O++)
                {
                    UpS[O] = new Rectangle(Object[O].Left, Object[O].Top, Object[O].Width, 10);
                    RightS[O] = new Rectangle(Object[O].Right - 5, Object[O].Top + 5, 5, Object[O].Height - 5);
                    LeftS[O] = new Rectangle(Object[O].Left, Object[O].Top + 5, 5, Object[O].Height - 5);
                    DownS[O] = new Rectangle(Object[O].Left, Object[O].Bottom - 5, Object[O].Width, 5);
                }
            }
            if (PlayerMap[MapY, MapX] == 7)
            {
                Object[1] = new Rectangle(0, 300, 100, 50); //this is ground
                Object[2] = new Rectangle(0, 400, 5000, 50); // this is the ground
                for (int O = 1; O < 7; O++)
                {
                    UpS[O] = new Rectangle(Object[O].Left, Object[O].Top, Object[O].Width, 10);
                    RightS[O] = new Rectangle(Object[O].Right - 5, Object[O].Top + 5, 5, Object[O].Height - 5);
                    LeftS[O] = new Rectangle(Object[O].Left, Object[O].Top + 5, 5, Object[O].Height - 5);
                    DownS[O] = new Rectangle(Object[O].Left, Object[O].Bottom - 5, Object[O].Width, 5);
                }
            }
            if (PlayerMap[MapY, MapX] == 5)
            {
                Object[1] = new Rectangle(75, 300, 100, 50); //this is ground
                Object[2] = new Rectangle(250, 400, 450, 50); // this is the ground
                for (int O = 1; O < 7; O++)
                {
                    UpS[O] = new Rectangle(Object[O].Left, Object[O].Top, Object[O].Width, 10);
                    RightS[O] = new Rectangle(Object[O].Right - 5, Object[O].Top + 5, 5, Object[O].Height - 5);
                    LeftS[O] = new Rectangle(Object[O].Left, Object[O].Top + 5, 5, Object[O].Height - 5);
                    DownS[O] = new Rectangle(Object[O].Left, Object[O].Bottom - 5, Object[O].Width, 5);
                }
            }
        }

        private void GenLvl()
        {
            int[,] CorrectPath = new int[5, 5]
{
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0}
};
            int PathX, PathY, PathLength;
            Random R = new System.Random();
            PathX = R.Next(0, 4);
            PathY = R.Next(0, 4);
            PathLength = R.Next(5,7);
            CorrectPath[PathY, PathX] = 1;
        }
        private void Torch_Tmr_Tick(object sender, EventArgs e)
        {
            if(Fuel >= 0 & Fuel <= 1)
            {
                Fuel -= 0.02;
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
            g.DrawImage(player, Player);
            //use the DrawImage method to draw the planet on the panel
            e.Graphics.FillRectangle(Brushes.Black, Object[1]);
            e.Graphics.FillRectangle(Brushes.Black, Object[2]);
            e.Graphics.FillRectangle(Brushes.Black, Object[3]);
            e.Graphics.FillRectangle(Brushes.Black, Object[4]);
            e.Graphics.FillRectangle(Brushes.BurlyWood, Stick);
            e.Graphics.FillRectangle(Brushes.Green, PlayerCenter);
            var rgn = new Region(new Rectangle(0, 0, 1000, 1000));
            var path = new GraphicsPath();
           if(Fuel <= 1 & Fuel >=0.6)
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
            e.Graphics.FillRegion(Brushes.Black, rgn);
            e.Graphics.FillRectangle(Brushes.OrangeRed, LightBar);
        }
    }
}
