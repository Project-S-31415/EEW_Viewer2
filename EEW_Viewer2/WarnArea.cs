using EEW_Viewer2.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
                else if (Settings.Default.WarnArea != "予報")
                {
                    EEW.Text = "緊急地震速報(警報)　強い揺れに警戒";
                    EEW.BackColor = Color.Red;
                    Area.Text = Settings.Default.WarnArea;
                    Bitmap MapImage = new Bitmap(Resources.JapanMap_Replace);
                    Graphics Graphics = Graphics.FromImage(MapImage);
                    ColorMap[] ColorChange = new ColorMap[]
                {
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap(),
                    new ColorMap()
                };
                    try
                    {
                        for (int i = 0; i < 47; i++)
                        {
                            ColorChange[i].OldColor = Color.FromArgb(i, 0, 0);
                        }
                        string[] WarnPref = Settings.Default.WarnArea2.Split('　');
                        for (int i = 0; i < 47; i++)
                        {
                            ColorChange[i].NewColor = Color.FromArgb(90, 90, 120);
                        }
                        for (int i = 0; i < WarnPref.Length; i++)
                        {
                            ColorChange[PrefID[WarnPref[i]]].NewColor = Color.Yellow;

                        }
                    }
                    catch
                    {

                    }
                    ImageAttributes IA = new ImageAttributes();
                    IA.SetRemapTable(ColorChange);
                    Graphics.DrawImage(MapImage, new Rectangle(0, 0, 900, 700), 0, 0, 900, 700, GraphicsUnit.Pixel, IA);
                    int Y_ = (int)((50.0 - Settings.Default.QuakeLat) * 20);//]
                    int X_ = (int)((Settings.Default.QuakeLong - 115) * 20);//-
                    int X = X_ * -1 + 80;
                    int Y = Y_ * -1 + 120;
                    Graphics.DrawImage(Resources.Point, X_ - 10, Y_ - 10, 20, 20);
                    Map.Image = MapImage;
                    Map.Location = new Point(X, Y);
                    Graphics.Dispose();
                    Green.Size = new Size(0, 0);

                }
                else if (Settings.Default.WarnArea == "予報")
                {
                    EEW.Text = "緊急地震速報(予報)　揺れに注意";
                    EEW.BackColor = Color.Green;
                    Bitmap MapImage = new Bitmap(Resources.JapanMap_Nomal);
                    Graphics Graphics = Graphics.FromImage(MapImage);
                    int Y_ = (int)((50.0 - Settings.Default.QuakeLat) * 20);//]
                    int X_ = (int)((Settings.Default.QuakeLong - 115) * 20);//-
                    int X = X_ * -1 + 80;
                    int Y = Y_ * -1 + 120;
                    Graphics.DrawImage(Resources.Point, X_ - 10, Y_ - 10, 20, 20);
                    Map.Image = MapImage;
                    Map.Location = new Point(X, Y);
                    Graphics.Dispose();
                    Green.Size = new Size(0, 0);
                }
            }
            catch
            {
                Map.Location = new Point(1000, 1000);
                EEW.Text = "エラー発生";
            }
        }
        public Dictionary<string, int> PrefID = new Dictionary<string, int>
        {
{"北海道",0},
{"青森県",1},
{"岩手県",2},
{"宮城県",3},
{"秋田県",4},
{"山形県",5},
{"福島県",6},
{"茨城県",7},
{"栃木県",8},
{"群馬県",9},
{"埼玉県",10},
{"千葉県",11},
{"東京都",12},
{"神奈川県",13},
{"新潟県",14},
{"富山県",15},
{"石川県",16},
{"福井県",17},
{"山梨県",18},
{"長野県",19},
{"岐阜県",20},
{"静岡県",21},
{"愛知県",22},
{"三重県",23},
{"滋賀県",24},
{"京都府",25},
{"大阪府",26},
{"兵庫県",27},
{"奈良県",28},
{"和歌山県",29},
{"鳥取県",30},
{"島根県",31},
{"岡山県",32},
{"広島県",33},
{"山口県",34},
{"徳島県",35},
{"香川県",36},
{"愛媛県",37},
{"高知県",38},
{"福岡県",39},
{"佐賀県",40},
{"長崎県",41},
{"熊本県",42},
{"大分県",43},
{"宮崎県",44},
{"鹿児島県",45},
{"沖縄県",46},
{"北海",0},
{"青森",1},
{"岩手",2},
{"宮城",3},
{"秋田",4},
{"山形",5},
{"福島",6},
{"茨城",7},
{"栃木",8},
{"群馬",9},
{"埼玉",10},
{"千葉",11},
{"東京",12},
{"神奈川",13},
{"新潟",14},
{"富山",15},
{"石川",16},
{"福井",17},
{"山梨",18},
{"長野",19},
{"岐阜",20},
{"静岡",21},
{"愛知",22},
{"三重",23},
{"滋賀",24},
{"京都",25},
{"大阪",26},
{"兵庫",27},
{"奈良",28},
{"和歌山",29},
{"鳥取",30},
{"島根",31},
{"岡山",32},
{"広島",33},
{"山口",34},
{"徳島",35},
{"香川",36},
{"愛媛",37},
{"高知",38},
{"福岡",39},
{"佐賀",40},
{"長崎",41},
{"熊本",42},
{"大分",43},
{"宮崎",44},
{"鹿児島",45},
{"沖縄",46}
        };
    }
}
