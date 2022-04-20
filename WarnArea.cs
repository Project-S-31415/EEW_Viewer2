using EEW_Viewer2.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EEW_Viewer2
{
    public partial class WarnArea : Form
    {
        public WarnArea()
        {
            InitializeComponent();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Settings.Default.WarnArea == "")
                {
                    Green.Size = new Size(600, 200);
                }
                else
                {
                    Green.Size = new Size(0, 0);
                    Area.Text = Settings.Default.WarnArea;
                    int Y_ = (int)((50.0 - Settings.Default.QuakeLat) * 22.1714285);//]
                    int X_ = (int)((Settings.Default.QuakeLong - 115) * 22.2222222);//-
                    int X = X_ * -1 + 80;
                    int Y = Y_ * -1 + 120;
                    Map.Location = new Point(X, Y);
                }
            }
            catch
            {

            }
        }
    }
}
