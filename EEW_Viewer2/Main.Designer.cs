namespace EEW_Viewer2
{
    partial class EEW_Viewer2
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
            this.EEW1 = new System.Windows.Forms.Label();
            this.CurrectLocationCalcintensity = new System.Windows.Forms.Label();
            this.TsunamiWarn = new System.Windows.Forms.Label();
            this.NIED = new System.Windows.Forms.Timer(this.components);
            this.Distance = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.Label();
            this.RightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.再起動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Iedred = new System.Windows.Forms.Timer(this.components);
            this.NoEEW = new System.Windows.Forms.Timer(this.components);
            this.EEW2 = new System.Windows.Forms.PictureBox();
            this.TimeControl = new System.Windows.Forms.Timer(this.components);
            this.RightClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EEW2)).BeginInit();
            this.SuspendLayout();
            // 
            // EEW1
            // 
            this.EEW1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(90)))));
            this.EEW1.Font = new System.Drawing.Font("Koruri Regular", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EEW1.ForeColor = System.Drawing.Color.White;
            this.EEW1.Location = new System.Drawing.Point(0, 0);
            this.EEW1.Margin = new System.Windows.Forms.Padding(0);
            this.EEW1.Name = "EEW1";
            this.EEW1.Size = new System.Drawing.Size(759, 166);
            this.EEW1.TabIndex = 0;
            this.EEW1.Text = "緊急地震速報　受信待機中";
            // 
            // CurrectLocationCalcintensity
            // 
            this.CurrectLocationCalcintensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.CurrectLocationCalcintensity.Font = new System.Drawing.Font("Roboto", 18F);
            this.CurrectLocationCalcintensity.ForeColor = System.Drawing.Color.White;
            this.CurrectLocationCalcintensity.Location = new System.Drawing.Point(0, 168);
            this.CurrectLocationCalcintensity.Margin = new System.Windows.Forms.Padding(0);
            this.CurrectLocationCalcintensity.Name = "CurrectLocationCalcintensity";
            this.CurrectLocationCalcintensity.Size = new System.Drawing.Size(293, 40);
            this.CurrectLocationCalcintensity.TabIndex = 13;
            // 
            // TsunamiWarn
            // 
            this.TsunamiWarn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.TsunamiWarn.Font = new System.Drawing.Font("Roboto", 18F);
            this.TsunamiWarn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.TsunamiWarn.Location = new System.Drawing.Point(295, 168);
            this.TsunamiWarn.Margin = new System.Windows.Forms.Padding(0);
            this.TsunamiWarn.Name = "TsunamiWarn";
            this.TsunamiWarn.Size = new System.Drawing.Size(167, 80);
            this.TsunamiWarn.TabIndex = 14;
            this.TsunamiWarn.Text = "津波発生の\r\n可能性あり";
            this.TsunamiWarn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NIED
            // 
            this.NIED.Interval = 2000;
            this.NIED.Tick += new System.EventHandler(this.NIED_Tick);
            // 
            // Distance
            // 
            this.Distance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.Distance.Font = new System.Drawing.Font("Roboto", 18F);
            this.Distance.ForeColor = System.Drawing.Color.White;
            this.Distance.Location = new System.Drawing.Point(0, 208);
            this.Distance.Margin = new System.Windows.Forms.Padding(0);
            this.Distance.Name = "Distance";
            this.Distance.Size = new System.Drawing.Size(293, 40);
            this.Distance.TabIndex = 15;
            // 
            // Message
            // 
            this.Message.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.Message.Font = new System.Drawing.Font("Koruri Regular", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message.ForeColor = System.Drawing.Color.White;
            this.Message.Location = new System.Drawing.Point(463, 168);
            this.Message.Margin = new System.Windows.Forms.Padding(0);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(280, 80);
            this.Message.TabIndex = 16;
            // 
            // RightClick
            // 
            this.RightClick.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.RightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.再起動ToolStripMenuItem});
            this.RightClick.Name = "contextMenuStrip1";
            this.RightClick.Size = new System.Drawing.Size(124, 58);
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.設定ToolStripMenuItem.Text = "設定";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 6);
            // 
            // 再起動ToolStripMenuItem
            // 
            this.再起動ToolStripMenuItem.Name = "再起動ToolStripMenuItem";
            this.再起動ToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.再起動ToolStripMenuItem.Text = "再起動";
            // 
            // Iedred
            // 
            this.Iedred.Interval = 2000;
            this.Iedred.Tick += new System.EventHandler(this.Iedred_Tick);
            // 
            // NoEEW
            // 
            this.NoEEW.Enabled = true;
            this.NoEEW.Interval = 250;
            this.NoEEW.Tick += new System.EventHandler(this.NoEEW_Tick);
            // 
            // EEW2
            // 
            this.EEW2.BackgroundImage = global::EEW_Viewer2.Properties.Resources.Null;
            this.EEW2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EEW2.Location = new System.Drawing.Point(4, 38);
            this.EEW2.Margin = new System.Windows.Forms.Padding(4);
            this.EEW2.Name = "EEW2";
            this.EEW2.Size = new System.Drawing.Size(727, 124);
            this.EEW2.TabIndex = 19;
            this.EEW2.TabStop = false;
            // 
            // TimeControl
            // 
            this.TimeControl.Enabled = true;
            this.TimeControl.Interval = 3600000;
            this.TimeControl.Tick += new System.EventHandler(this.TimeControl_Tick);
            // 
            // EEW_Viewer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(736, 246);
            this.Controls.Add(this.EEW2);
            this.Controls.Add(this.TsunamiWarn);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.Distance);
            this.Controls.Add(this.CurrectLocationCalcintensity);
            this.Controls.Add(this.EEW1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "EEW_Viewer2";
            this.Text = "EEW_Viewer2";
            this.Load += new System.EventHandler(this.EEW_Viewer2_Load);
            this.RightClick.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EEW2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label EEW1;
        private System.Windows.Forms.Label CurrectLocationCalcintensity;
        private System.Windows.Forms.Label TsunamiWarn;
        private System.Windows.Forms.Timer NIED;
        private System.Windows.Forms.Label Distance;
        private System.Windows.Forms.Label Message;
        private System.Windows.Forms.PictureBox EEW2;
        private System.Windows.Forms.ContextMenuStrip RightClick;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 再起動ToolStripMenuItem;
        private System.Windows.Forms.Timer Iedred;
        private System.Windows.Forms.Timer NoEEW;
        private System.Windows.Forms.Timer TimeControl;
    }
}

