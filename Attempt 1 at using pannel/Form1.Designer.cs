
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
            this.label1 = new System.Windows.Forms.Label();
            this.Fuel_Lbl = new System.Windows.Forms.Label();
            this.Return_Btn = new System.Windows.Forms.Button();
            this.Save_Btn = new System.Windows.Forms.Button();
            this.Load_Btn = new System.Windows.Forms.Button();
            this.Framerate = new System.Windows.Forms.Timer(this.components);
            this.Torch_Tmr = new System.Windows.Forms.Timer(this.components);
            this.Game_Pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // Game_Pnl
            // 
            this.Game_Pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Game_Pnl.Controls.Add(this.label1);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(418, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // Fuel_Lbl
            // 
            this.Fuel_Lbl.AutoSize = true;
            this.Fuel_Lbl.BackColor = System.Drawing.Color.Black;
            this.Fuel_Lbl.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Fuel_Lbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.Fuel_Lbl.Location = new System.Drawing.Point(681, 7);
            this.Fuel_Lbl.Name = "Fuel_Lbl";
            this.Fuel_Lbl.Size = new System.Drawing.Size(95, 30);
            this.Fuel_Lbl.TabIndex = 4;
            this.Fuel_Lbl.Text = "Fuel Left";
            // 
            // Return_Btn
            // 
            this.Return_Btn.AutoSize = true;
            this.Return_Btn.Location = new System.Drawing.Point(162, 0);
            this.Return_Btn.Name = "Return_Btn";
            this.Return_Btn.Size = new System.Drawing.Size(96, 25);
            this.Return_Btn.TabIndex = 3;
            this.Return_Btn.Text = "Back to game";
            this.Return_Btn.UseVisualStyleBackColor = true;
            this.Return_Btn.Click += new System.EventHandler(this.Return_Btn_Click);
            // 
            // Save_Btn
            // 
            this.Save_Btn.Location = new System.Drawing.Point(81, 0);
            this.Save_Btn.Name = "Save_Btn";
            this.Save_Btn.Size = new System.Drawing.Size(75, 23);
            this.Save_Btn.TabIndex = 2;
            this.Save_Btn.TabStop = false;
            this.Save_Btn.Text = "Save";
            this.Save_Btn.UseVisualStyleBackColor = true;
            this.Save_Btn.Click += new System.EventHandler(this.Save_Btn_Click);
            // 
            // Load_Btn
            // 
            this.Load_Btn.Location = new System.Drawing.Point(0, 0);
            this.Load_Btn.Name = "Load_Btn";
            this.Load_Btn.Size = new System.Drawing.Size(75, 23);
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
        private System.Windows.Forms.Label label1;
    }
}

