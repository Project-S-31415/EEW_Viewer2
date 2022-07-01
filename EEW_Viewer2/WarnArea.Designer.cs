namespace EEW_Viewer2
{
    partial class WarnArea
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.EEW = new System.Windows.Forms.Label();
            this.Area = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.Green = new System.Windows.Forms.Label();
            this.Map = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.SuspendLayout();
            // 
            // EEW
            // 
            this.EEW.BackColor = System.Drawing.Color.Red;
            this.EEW.Location = new System.Drawing.Point(0, 0);
            this.EEW.Margin = new System.Windows.Forms.Padding(0);
            this.EEW.Name = "EEW";
            this.EEW.Size = new System.Drawing.Size(600, 40);
            this.EEW.TabIndex = 0;
            this.EEW.Text = "緊急地震速報(警報)　強い揺れに警戒";
            this.EEW.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Area
            // 
            this.Area.Font = new System.Drawing.Font("Koruri Regular", 28F);
            this.Area.Location = new System.Drawing.Point(161, 37);
            this.Area.Margin = new System.Windows.Forms.Padding(0);
            this.Area.Name = "Area";
            this.Area.Size = new System.Drawing.Size(439, 170);
            this.Area.TabIndex = 1;
            this.Area.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 333;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // Green
            // 
            this.Green.BackColor = System.Drawing.Color.Lime;
            this.Green.Location = new System.Drawing.Point(0, 0);
            this.Green.Margin = new System.Windows.Forms.Padding(0);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(600, 200);
            this.Green.TabIndex = 4;
            // 
            // Map
            // 
            this.Map.Image = global::EEW_Viewer2.Properties.Resources.JapanMap_Replace;
            this.Map.Location = new System.Drawing.Point(-350, -175);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(900, 700);
            this.Map.TabIndex = 2;
            this.Map.TabStop = false;
            // 
            // WarnArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 46F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(600, 200);
            this.Controls.Add(this.Green);
            this.Controls.Add(this.EEW);
            this.Controls.Add(this.Area);
            this.Controls.Add(this.Map);
            this.Font = new System.Drawing.Font("Koruri Regular", 20F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.Name = "WarnArea";
            this.Text = "EEW_Viewer2 - 警報地域";
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label EEW;
        private System.Windows.Forms.Label Area;
        private System.Windows.Forms.PictureBox Map;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Label Green;
    }
}