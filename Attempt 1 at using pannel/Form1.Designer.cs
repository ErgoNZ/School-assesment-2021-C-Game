
namespace Attempt_1_at_using_pannel
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Game_Pnl = new System.Windows.Forms.Panel();
            this.Diff_Lbl = new System.Windows.Forms.Label();
            this.Extreme_Btn = new System.Windows.Forms.Button();
            this.Hard_Btn = new System.Windows.Forms.Button();
            this.Normal_Btn = new System.Windows.Forms.Button();
            this.NameSave_Btn = new System.Windows.Forms.Button();
            this.PlayerName_TxtBox = new System.Windows.Forms.TextBox();
            this.Fuel_Lbl = new System.Windows.Forms.Label();
            this.Return_Btn = new System.Windows.Forms.Button();
            this.Save_Btn = new System.Windows.Forms.Button();
            this.Load_Btn = new System.Windows.Forms.Button();
            this.Framerate = new System.Windows.Forms.Timer(this.components);
            this.Torch_Tmr = new System.Windows.Forms.Timer(this.components);
            this.Title_Lbl = new System.Windows.Forms.Label();
            this.Game_Pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // Game_Pnl
            // 
            this.Game_Pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Game_Pnl.Controls.Add(this.Title_Lbl);
            this.Game_Pnl.Controls.Add(this.Diff_Lbl);
            this.Game_Pnl.Controls.Add(this.Extreme_Btn);
            this.Game_Pnl.Controls.Add(this.Hard_Btn);
            this.Game_Pnl.Controls.Add(this.Normal_Btn);
            this.Game_Pnl.Controls.Add(this.NameSave_Btn);
            this.Game_Pnl.Controls.Add(this.PlayerName_TxtBox);
            this.Game_Pnl.Controls.Add(this.Fuel_Lbl);
            this.Game_Pnl.Controls.Add(this.Return_Btn);
            this.Game_Pnl.Controls.Add(this.Save_Btn);
            this.Game_Pnl.Controls.Add(this.Load_Btn);
            this.Game_Pnl.Location = new System.Drawing.Point(1, 0);
            this.Game_Pnl.Name = "Game_Pnl";
            this.Game_Pnl.Size = new System.Drawing.Size(919, 558);
            this.Game_Pnl.TabIndex = 0;
            this.Game_Pnl.TabStop = true;
            this.Game_Pnl.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Diff_Lbl
            // 
            this.Diff_Lbl.AutoSize = true;
            this.Diff_Lbl.BackColor = System.Drawing.Color.Black;
            this.Diff_Lbl.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Diff_Lbl.ForeColor = System.Drawing.Color.Red;
            this.Diff_Lbl.Location = new System.Drawing.Point(336, 370);
            this.Diff_Lbl.Name = "Diff_Lbl";
            this.Diff_Lbl.Size = new System.Drawing.Size(201, 32);
            this.Diff_Lbl.TabIndex = 10;
            this.Diff_Lbl.Text = "Difficulty: Normal";
            // 
            // Extreme_Btn
            // 
            this.Extreme_Btn.Location = new System.Drawing.Point(524, 421);
            this.Extreme_Btn.Name = "Extreme_Btn";
            this.Extreme_Btn.Size = new System.Drawing.Size(126, 79);
            this.Extreme_Btn.TabIndex = 9;
            this.Extreme_Btn.Text = "Extreme (25 Seconds)";
            this.Extreme_Btn.UseVisualStyleBackColor = true;
            this.Extreme_Btn.Click += new System.EventHandler(this.Extreme_Btn_Click);
            // 
            // Hard_Btn
            // 
            this.Hard_Btn.Location = new System.Drawing.Point(380, 421);
            this.Hard_Btn.Name = "Hard_Btn";
            this.Hard_Btn.Size = new System.Drawing.Size(134, 79);
            this.Hard_Btn.TabIndex = 8;
            this.Hard_Btn.Text = "Hard (50 Seconds)";
            this.Hard_Btn.UseVisualStyleBackColor = true;
            this.Hard_Btn.Click += new System.EventHandler(this.Hard_Btn_Click);
            // 
            // Normal_Btn
            // 
            this.Normal_Btn.Location = new System.Drawing.Point(241, 421);
            this.Normal_Btn.Name = "Normal_Btn";
            this.Normal_Btn.Size = new System.Drawing.Size(133, 79);
            this.Normal_Btn.TabIndex = 7;
            this.Normal_Btn.Text = "Normal (100 Seconds)";
            this.Normal_Btn.UseVisualStyleBackColor = true;
            this.Normal_Btn.Click += new System.EventHandler(this.Normal_Btn_Click);
            // 
            // NameSave_Btn
            // 
            this.NameSave_Btn.Location = new System.Drawing.Point(694, 16);
            this.NameSave_Btn.Name = "NameSave_Btn";
            this.NameSave_Btn.Size = new System.Drawing.Size(89, 34);
            this.NameSave_Btn.TabIndex = 6;
            this.NameSave_Btn.Text = "Save name";
            this.NameSave_Btn.UseVisualStyleBackColor = true;
            this.NameSave_Btn.Click += new System.EventHandler(this.NameSave_Btn_Click);
            // 
            // PlayerName_TxtBox
            // 
            this.PlayerName_TxtBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PlayerName_TxtBox.Location = new System.Drawing.Point(789, 15);
            this.PlayerName_TxtBox.Name = "PlayerName_TxtBox";
            this.PlayerName_TxtBox.Size = new System.Drawing.Size(118, 35);
            this.PlayerName_TxtBox.TabIndex = 5;
            // 
            // Fuel_Lbl
            // 
            this.Fuel_Lbl.AutoSize = true;
            this.Fuel_Lbl.BackColor = System.Drawing.Color.Black;
            this.Fuel_Lbl.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Fuel_Lbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.Fuel_Lbl.Location = new System.Drawing.Point(11, 9);
            this.Fuel_Lbl.Name = "Fuel_Lbl";
            this.Fuel_Lbl.Size = new System.Drawing.Size(95, 30);
            this.Fuel_Lbl.TabIndex = 4;
            this.Fuel_Lbl.Text = "Fuel Left";
            // 
            // Return_Btn
            // 
            this.Return_Btn.AutoSize = true;
            this.Return_Btn.Location = new System.Drawing.Point(380, 107);
            this.Return_Btn.Name = "Return_Btn";
            this.Return_Btn.Size = new System.Drawing.Size(134, 55);
            this.Return_Btn.TabIndex = 3;
            this.Return_Btn.Text = "Start/Back to game";
            this.Return_Btn.UseVisualStyleBackColor = true;
            this.Return_Btn.Click += new System.EventHandler(this.Return_Btn_Click);
            // 
            // Save_Btn
            // 
            this.Save_Btn.Location = new System.Drawing.Point(380, 201);
            this.Save_Btn.Name = "Save_Btn";
            this.Save_Btn.Size = new System.Drawing.Size(134, 56);
            this.Save_Btn.TabIndex = 2;
            this.Save_Btn.TabStop = false;
            this.Save_Btn.Text = "Save";
            this.Save_Btn.UseVisualStyleBackColor = true;
            this.Save_Btn.Click += new System.EventHandler(this.Save_Btn_Click);
            // 
            // Load_Btn
            // 
            this.Load_Btn.Location = new System.Drawing.Point(380, 293);
            this.Load_Btn.Name = "Load_Btn";
            this.Load_Btn.Size = new System.Drawing.Size(134, 50);
            this.Load_Btn.TabIndex = 1;
            this.Load_Btn.TabStop = false;
            this.Load_Btn.Text = "Load";
            this.Load_Btn.UseVisualStyleBackColor = true;
            this.Load_Btn.Click += new System.EventHandler(this.Load_Btn_Click);
            // 
            // Framerate
            // 
            this.Framerate.Enabled = true;
            this.Framerate.Interval = 20;
            this.Framerate.Tick += new System.EventHandler(this.Framerate_Tick);
            // 
            // Torch_Tmr
            // 
            this.Torch_Tmr.Interval = 1000;
            this.Torch_Tmr.Tick += new System.EventHandler(this.Torch_Tmr_Tick);
            // 
            // Title_Lbl
            // 
            this.Title_Lbl.AutoSize = true;
            this.Title_Lbl.BackColor = System.Drawing.Color.Black;
            this.Title_Lbl.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Title_Lbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.Title_Lbl.Location = new System.Drawing.Point(356, 34);
            this.Title_Lbl.Name = "Title_Lbl";
            this.Title_Lbl.Size = new System.Drawing.Size(194, 45);
            this.Title_Lbl.TabIndex = 11;
            this.Title_Lbl.Text = "Cave Escape";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(920, 557);
            this.Controls.Add(this.Game_Pnl);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Game_Pnl.ResumeLayout(false);
            this.Game_Pnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Game_Pnl;
        private System.Windows.Forms.Button Load_Btn;
        private System.Windows.Forms.Button Save_Btn;
        private System.Windows.Forms.Timer Framerate;
        private System.Windows.Forms.Button Return_Btn;
        private System.Windows.Forms.Label Fuel_Lbl;
        private System.Windows.Forms.Timer Torch_Tmr;
        private System.Windows.Forms.TextBox PlayerName_TxtBox;
        private System.Windows.Forms.Button NameSave_Btn;
        private System.Windows.Forms.Button Extreme_Btn;
        private System.Windows.Forms.Button Hard_Btn;
        private System.Windows.Forms.Button Normal_Btn;
        private System.Windows.Forms.Label Diff_Lbl;
        private System.Windows.Forms.Label Title_Lbl;
    }
}

