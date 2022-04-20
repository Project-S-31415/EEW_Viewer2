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
            this.CurrectLocationExpectedCalcintensity = new System.Windows.Forms.Label();
            this.TsunamiWarn = new System.Windows.Forms.Label();
            this.EEW = new System.Windows.Forms.Timer(this.components);
            this.Distance = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.Label();
            this.EEW2 = new System.Windows.Forms.PictureBox();
            this.RightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.再起動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.EEW2)).BeginInit();
            this.RightClick.SuspendLayout();
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
            this.EEW1.Size = new System.Drawing.Size(569, 133);
            this.EEW1.TabIndex = 0;
            this.EEW1.Text = "緊急地震速報";
            // 
            // CurrectLocationExpectedCalcintensity
            // 
            this.CurrectLocationExpectedCalcintensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.CurrectLocationExpectedCalcintensity.Font = new System.Drawing.Font("Roboto", 18F);
            this.CurrectLocationExpectedCalcintensity.ForeColor = System.Drawing.Color.White;
            this.CurrectLocationExpectedCalcintensity.Location = new System.Drawing.Point(0, 134);
            this.CurrectLocationExpectedCalcintensity.Margin = new System.Windows.Forms.Padding(0);
            this.CurrectLocationExpectedCalcintensity.Name = "CurrectLocationExpectedCalcintensity";
            this.CurrectLocationExpectedCalcintensity.Size = new System.Drawing.Size(220, 32);
            this.CurrectLocationExpectedCalcintensity.TabIndex = 13;
            // 
            // TsunamiWarn
            // 
            this.TsunamiWarn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.TsunamiWarn.Font = new System.Drawing.Font("Roboto", 18F);
            this.TsunamiWarn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.TsunamiWarn.Location = new System.Drawing.Point(221, 134);
            this.TsunamiWarn.Margin = new System.Windows.Forms.Padding(0);
            this.TsunamiWarn.Name = "TsunamiWarn";
            this.TsunamiWarn.Size = new System.Drawing.Size(125, 64);
            this.TsunamiWarn.TabIndex = 14;
            this.TsunamiWarn.Text = "津波発生の\r\n可能性あり";
            this.TsunamiWarn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EEW
            // 
            this.EEW.Enabled = true;
            this.EEW.Interval = 3000;
            this.EEW.Tick += new System.EventHandler(this.EEW_Tick);
            // 
            // Distance
            // 
            this.Distance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.Distance.Font = new System.Drawing.Font("Roboto", 18F);
            this.Distance.ForeColor = System.Drawing.Color.White;
            this.Distance.Location = new System.Drawing.Point(0, 166);
            this.Distance.Margin = new System.Windows.Forms.Padding(0);
            this.Distance.Name = "Distance";
            this.Distance.Size = new System.Drawing.Size(220, 32);
            this.Distance.TabIndex = 15;
            // 
            // Message
            // 
            this.Message.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.Message.Font = new System.Drawing.Font("Roboto", 8F);
            this.Message.ForeColor = System.Drawing.Color.White;
            this.Message.Location = new System.Drawing.Point(347, 134);
            this.Message.Margin = new System.Windows.Forms.Padding(0);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(210, 64);
            this.Message.TabIndex = 16;
            // 
            // EEW2
            // 
            this.EEW2.BackgroundImage = global::EEW_Viewer2.Properties.Resources.Null;
            this.EEW2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EEW2.Location = new System.Drawing.Point(3, 30);
            this.EEW2.Name = "EEW2";
            this.EEW2.Size = new System.Drawing.Size(546, 100);
            this.EEW2.TabIndex = 19;
            this.EEW2.TabStop = false;
            // 
            // RightClick
            // 
            this.RightClick.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.RightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.再起動ToolStripMenuItem});
            this.RightClick.Name = "contextMenuStrip1";
            this.RightClick.Size = new System.Drawing.Size(181, 76);
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.設定ToolStripMenuItem.Text = "設定";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(107, 6);
            // 
            // 再起動ToolStripMenuItem
            // 
            this.再起動ToolStripMenuItem.Name = "再起動ToolStripMenuItem";
            this.再起動ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.再起動ToolStripMenuItem.Text = "再起動";
            // 
            // EEW_Viewer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(552, 197);
            this.Controls.Add(this.EEW2);
            this.Controls.Add(this.TsunamiWarn);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.Distance);
            this.Controls.Add(this.CurrectLocationExpectedCalcintensity);
            this.Controls.Add(this.EEW1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "EEW_Viewer2";
            this.Text = "EEW_Viewer2";
            ((System.ComponentModel.ISupportInitialize)(this.EEW2)).EndInit();
            this.RightClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label EEW1;
        private System.Windows.Forms.Label CurrectLocationExpectedCalcintensity;
        private System.Windows.Forms.Label TsunamiWarn;
        private System.Windows.Forms.Timer EEW;
        private System.Windows.Forms.Label Distance;
        private System.Windows.Forms.Label Message;
        private System.Windows.Forms.PictureBox EEW2;
        private System.Windows.Forms.ContextMenuStrip RightClick;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 再起動ToolStripMenuItem;
    }
}

