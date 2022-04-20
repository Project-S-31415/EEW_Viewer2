using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using EEW_Viewer2.Properties;
using CoreTweet;
using System.Threading;

namespace EEW_Viewer2
{
    public partial class EEW_Viewer2 : Form
    {
        public EEW_Viewer2()
        {
            InitializeComponent();
            WarnArea Form2 = new WarnArea();
            Form2.Show();
        }
        public void EEW_Tick(object sender, EventArgs e)
        {
            try
            {
                WebClient wc = new WebClient
                {
                    Encoding = Encoding.UTF8
                };

                bool InfoFlag_NIED = false;
                bool InfoFlag_iedred = false;

                DateTime DataTime1_NIED = DateTime.Now;
                int DataTime2_NIED = DataTime1_NIED.Second - 2;
                long AccessTime1_NIED = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                long AccessTime2_NIED = AccessTime1_NIED - 2;//x秒前
                if (DataTime1_NIED.Second == 0 || DataTime1_NIED.Second == 1)//1:00-0:01=0:99になるため   2秒前の場合追加→　 || DataTime1.Second == 1
                {
                    AccessTime2_NIED = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss")) - 42;
                    //1秒前の場合 -41  2秒前の場合 -42
                }
                string EEW_NIED_AccessURL = "http://www.kmoni.bosai.go.jp/webservice/hypo/eew/" + AccessTime2_NIED + ".json";
                //通常
                string eew_nied_json = wc.DownloadString(EEW_NIED_AccessURL);

                //テスト用 EEW予報　震度2
                //string eew_nied_json = wc.DownloadString("http://www.kmoni.bosai.go.jp/webservice/hypo/eew/20220123095750.json");

                //テスト用　EEW予報 震度不明
                //string eew_nied_json = wc.DownloadString("http://www.kmoni.bosai.go.jp/webservice/hypo/eew/20210705012355.json");

                //テスト用　EEW警報　2021福島県沖
                //string eew_nied_json = wc.DownloadString("http://www.kmoni.bosai.go.jp/webservice/hypo/eew/20210213231450.json");

                EEW_NIED_json EEW_NIED_json = JsonConvert.DeserializeObject<EEW_NIED_json>(eew_nied_json);


                if (EEW_NIED_json.Alertflg == "予報" || EEW_NIED_json.Alertflg == "警報")
                {

                    InfoFlag_NIED = true;
                    double Lat_NIED = Convert.ToDouble(EEW_NIED_json.Latitude);
                    double Long_NIED = Convert.ToDouble(EEW_NIED_json.Longitude);
                    int MaxCal_int_NIED = Convert.ToInt32(EEW_NIED_json.Calcintensity.Replace("7", "9").Replace("不明", "0").Replace("5弱", "5").Replace("5強", "6").Replace("6弱", "7").Replace("6強", "8"));
                    string MaxCal_str_NIED = Convert.ToString(EEW_NIED_json.Calcintensity).Replace("不明", "0");
                    
                    string Origin_Time_str_NIED_full = EEW_NIED_json.Origin_time;
                    string Origin_Time_str_NIED_yyyy = Origin_Time_str_NIED_full.Remove(4, 10);
                    string Origin_Time_str_NIED_MM = Origin_Time_str_NIED_full.Remove(0, 4).Remove(2, 8);
                    string Origin_Time_str_NIED_dd = Origin_Time_str_NIED_full.Remove(0, 6).Remove(2, 6);
                    string Origin_Time_str_NIED_HH = Origin_Time_str_NIED_full.Remove(0, 8).Remove(2, 4);
                    string Origin_Time_str_NIED_mm = Origin_Time_str_NIED_full.Remove(0, 10).Remove(2, 2);
                    string Origin_Time_str_NIED_ss = Origin_Time_str_NIED_full.Remove(0, 12);

                    string Origin_Time_str_NIED = $"{Origin_Time_str_NIED_yyyy}/{Origin_Time_str_NIED_MM}/{Origin_Time_str_NIED_dd}　{Origin_Time_str_NIED_HH}:{Origin_Time_str_NIED_mm}:{Origin_Time_str_NIED_ss}";
                    long Origin_Time_long_NIED = Convert.ToInt64(EEW_NIED_json.Origin_time);
                    int Depth_int_NIED = Convert.ToInt32(EEW_NIED_json.Depth.Replace("km", ""));

                    string Depth_str_NIED = EEW_NIED_json.Depth;
                    double Magunitude_NIED = Convert.ToDouble(EEW_NIED_json.Magunitude);
                    string Region_name_NIED = EEW_NIED_json.Region_name;
                    int Num_NIED = Convert.ToInt32(EEW_NIED_json.Report_num);
                    string Flg_NIED = EEW_NIED_json.Alertflg;
                    if (Num_NIED <= 3)
                    {
                        MaxCal_str_NIED = Convert.ToString(EEW_NIED_json.Calcintensity).Replace("0", "-");
                        MaxCal_int_NIED = Convert.ToInt32(EEW_NIED_json.Calcintensity.Replace("0", "10"));
                    }


                    bool Final_NIED = EEW_NIED_json.Is_final == "true";
                    bool Plum_NIED = Magunitude_NIED == 1.0;
                    bool TsunamiWarn1_NIED = Magunitude_NIED >= 6.0 && Depth_int_NIED <= 100;
                    bool TsunamiWarn2_NIED = Magunitude_NIED >= 7.5 && Depth_int_NIED <= 50;


                    double Distance1_NIED = double.Parse(Convert.ToString(Settings.Default.UserLatitude - Lat_NIED).Replace("-", "")) * 111.263;
                    double Distance2_NIED = double.Parse(Convert.ToString(Settings.Default.UserLongitude - Long_NIED).Replace("-", "")) * 40075.01668 * Math.Cos((Settings.Default.UserLatitude + Lat_NIED) / 0.0349) / 360;
                    double Distance3_NIED = Math.Sqrt(Distance1_NIED * Distance1_NIED + Distance2_NIED * Distance2_NIED);//---.-------km
                    int Distance4_NIED = (int)Math.Round(Distance3_NIED * 10) / 10;//---km
                    int Distance5_NIED = (int)Math.Round(Distance3_NIED / 10) * 10;//震央距離x0.1→四捨五入→10倍(--0km)

                    string EEWShindo_URL = "http://www.kmoni.bosai.go.jp/data/map_img/EstShindoImg/eew/" + DateTime.Now.ToString("yyyyMMdd/") + AccessTime2_NIED + ".eew.gif";
                    //テスト
                    //string EEWShindo_URL = "http://www.kmoni.bosai.go.jp/data/map_img/EstShindoImg/eew/20220122/20220122010851.eew.gif";
                    //テストの場合EEWShindoURLをテストにしないとここでエラー(1x1の画像になるため)
                    Stream EEWShindo1 = wc.OpenRead(EEWShindo_URL);
                    Bitmap EEWShindo2 = new Bitmap(EEWShindo1);
                    double CurrectLocationExpectedCalcintensity1 = -5.0;
                    try
                    {
                        Color EEWColor1 = EEWShindo2.GetPixel(Settings.Default.UserPixelX, Settings.Default.UserPixelY);
                        string EEWColor2 = EEWColor1.R + "," + EEWColor1.G + "," + EEWColor1.B;
                        CurrectLocationExpectedCalcintensity1 = KyoshinIntDictionary[EEWColor2];
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                    EEWShindo1.Close();
                    CurrectLocationExpectedCalcintensity.Text = "現在地予想震度:" + (CurrectLocationExpectedCalcintensity1 + ".0").Replace(".1.0", ".1").Replace(".2.0", ".2").Replace(".3.0", ".3").Replace(".4.0", ".4").Replace(".5.0", ".5").Replace(".6.0", ".6").Replace(".7.0", ".7").Replace(".8.0", ".8").Replace(".9.0", ".9").Replace("-5.0", "- - -");

                    Distance.Text = "震央距離 約" + Distance4_NIED + "km";

                    string Final_str = "　　";
                    if (Final_NIED)
                    {
                        Final_str = "最終";
                    }

                    string MagDepText = $"M{Magunitude_NIED}　　　{Depth_str_NIED}";

                    if (Plum_NIED)
                    {
                        MagDepText = "PLUM法による予測";
                    }

                    if (MaxCal_int_NIED == 0)
                    {
                            EEW2.BackgroundImage = Resources.Shindo0;
                    }
                    else if (MaxCal_int_NIED == 1)
                    {
                        EEW2.BackgroundImage = Resources.Shindo1;
                    }
                    else if (MaxCal_int_NIED == 2)
                    {
                        EEW2.BackgroundImage = Resources.Shindo2;
                    }
                    else if (MaxCal_int_NIED == 3)
                    {
                        EEW2.BackgroundImage = Resources.Shindo3;
                    }
                    else if (MaxCal_int_NIED == 4)
                    {
                        EEW2.BackgroundImage = Resources.Shindo4;
                    }
                    else if (MaxCal_int_NIED == 5)
                    {
                        EEW2.BackgroundImage = Resources.Shindo5;
                    }
                    else if (MaxCal_int_NIED == 6)
                    {
                        EEW2.BackgroundImage = Resources.Shindo6;
                    }
                    else if (MaxCal_int_NIED == 7)
                    {
                        EEW2.BackgroundImage = Resources.Shindo7;
                    }
                    else if (MaxCal_int_NIED == 8)
                    {
                        EEW2.BackgroundImage = Resources.Shindo8;
                    }
                    else if (MaxCal_int_NIED == 9)
                    {
                        EEW2.BackgroundImage = Resources.Shindo9;
                    }
                    else
                    {
                        EEW2.BackgroundImage = Resources.ShindoNull;
                    }

                    Bitmap Canvas = new Bitmap(500, 100);
                    Graphics Gra = Graphics.FromImage(Canvas);
                    Font Shingen_Font = new Font("Koruri Regular", 30);
                    Font Saidai_Font = new Font("Koruri Regular", 10);
                    Font MagDep_Font = new Font("Roboto", 25);
                    Brush DrawColor = Brushes.White;
                    if (MaxCal_int_NIED == 3 || MaxCal_int_NIED == 4 || MaxCal_int_NIED == 5 || MaxCal_int_NIED == 6)
                    {
                        DrawColor = Brushes.Black;
                    }
                    Gra.DrawString($"{Region_name_NIED}", Shingen_Font, DrawColor, 102, 4);
                    Gra.DrawString($"予想最大震度", Saidai_Font, DrawColor, 5, 5);
                    Gra.DrawString($"{MagDepText}", MagDep_Font, DrawColor, 230, 60);
                    Shingen_Font.Dispose();
                    Saidai_Font.Dispose();
                    MagDep_Font.Dispose();
                    Gra.Dispose();
                    EEW2.Image = null;
                    EEW2.Image = Canvas;


                    EEW1.Text = $"緊急地震速報({Flg_NIED}) 第{Num_NIED}報 {Final_str}　{Origin_Time_str_NIED}";


                    if (Flg_NIED == "予報")
                    {
                        EEW1.BackColor = Color.FromArgb(25, 100, 25);

                    }
                    else if (Flg_NIED == "警報")
                    {
                        EEW1.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    if (TsunamiWarn1_NIED == false && TsunamiWarn2_NIED == false)
                    {
                        TsunamiWarn.BackColor = Color.FromArgb(60, 60, 90);
                    }
                    if (TsunamiWarn1_NIED)
                    {
                        TsunamiWarn.BackColor = Color.Yellow;
                    }
                    if (TsunamiWarn2_NIED)
                    {
                        TsunamiWarn.BackColor = Color.Red;
                    }


                }
                //NIED終わり
                Thread.Sleep(1400);

                //通常
                string eew_iedred_json = wc.DownloadString("https://api.iedred7584.com/eew/json/");
                //テスト用　EEW警報
                //string eew_iedred_json = File.ReadAllText($"iedred.warn.json");
                //テスト用　自由
                //string eew_iedred_json = File.ReadAllText($"iedred.user.json");

                EEW_iedred_JSON EEW_iedred_json = JsonConvert.DeserializeObject<EEW_iedred_JSON>(eew_iedred_json);

                long Origin_Time_long_iedred = Convert.ToInt64(EEW_iedred_json.OriginTime.String.Replace("/", "").Replace(":", "").Replace(" ", ""));
                string Origin_Time_str_iedred = EEW_iedred_json.OriginTime.String;
                long NowTime_iedred = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                long ProgressTime_iedred = NowTime_iedred - Origin_Time_long_iedred;
                //テスト時ifを消す
                if (ProgressTime_iedred < 222)
                {
                    InfoFlag_iedred = true;

                    double Lat_iedred = Convert.ToDouble(EEW_iedred_json.Hypocenter.Location.Lat);
                    double Long_iedred = Convert.ToDouble(EEW_iedred_json.Hypocenter.Location.Long);
                    int MaxCal_int_iedred = Convert.ToInt32(EEW_iedred_json.MaxIntensity.String.Replace("7", "9").Replace("不明", "0").Replace("5弱", "5").Replace("5強", "6").Replace("6弱", "7").Replace("6強", "8"));
                    string MaxCal_str_iedred = EEW_iedred_json.MaxIntensity.String;

                    int Depth_int_iedred = EEW_iedred_json.Hypocenter.Location.Depth.Int;

                    string Depth_str_iedred = $"{EEW_iedred_json.Hypocenter.Location.Depth.Int}km";
                    double Magunitude_iedred = Convert.ToDouble(EEW_iedred_json.Hypocenter.Magnitude.Float);
                    string Region_name_iedred = EEW_iedred_json.Hypocenter.Name;
                    int Num_iedred = Convert.ToInt32(EEW_iedred_json.Serial);
                    string Flg_iedred = EEW_iedred_json.Title.String.Replace("緊急地震速報（", "").Replace("）", "");



                    bool Final_iedred = EEW_iedred_json.Type.Detail.IndexOf("最終") != -1;
                    bool Plum_iedred = EEW_iedred_json.Hypocenter.IsAssumption == true;
                    bool TsunamiWarn1_iedred = Magunitude_iedred >= 6.0 && Depth_int_iedred <= 100;
                    bool TsunamiWarn2_iedred = Magunitude_iedred >= 7.5 && Depth_int_iedred <= 50;


                    double Distance1_iedred = double.Parse(Convert.ToString(Settings.Default.UserLatitude - Lat_iedred).Replace("-", "")) * 111.263;
                    double Distance2_iedred = double.Parse(Convert.ToString(Settings.Default.UserLongitude - Long_iedred).Replace("-", "")) * 40075.01668 * Math.Cos((Settings.Default.UserLatitude + Lat_iedred) / 0.0349) / 360;
                    double Distance3_iedred = Math.Sqrt(Distance1_iedred * Distance1_iedred + Distance2_iedred * Distance2_iedred);//---.-------km
                    int Distance4_iedred = (int)Math.Round(Distance3_iedred * 10) / 10;//---km
                    int Distance5_iedred = (int)Math.Round(Distance3_iedred / 10) * 10;//--0km

                    if (Depth_int_iedred > 150 || Num_iedred >= 3)
                    {
                        MaxCal_str_iedred = MaxCal_str_iedred.Replace("不明", "-");
                    }
                    else
                    {
                        MaxCal_str_iedred = MaxCal_str_iedred.Replace("不明", "0");
                    }

                    if (Depth_int_iedred <= 150)
                    {
                        DateTime DataTime1_iedred = DateTime.Now;
                        long AccessTime1_iedred = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                        long AccessTime2_iedred = AccessTime1_iedred - 2;//x秒前
                        if (DataTime1_iedred.Second == 0 || DataTime1_iedred.Second == 1)//1:00-0:01=0:99になるため   2秒前の場合 || DataTime1.Second == 1
                        {
                            AccessTime2_iedred = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss")) - 42;
                            //1秒前の場合 -41  2秒前の場合 -42
                        }
                        string EEWShindo_URL = "http://www.kmoni.bosai.go.jp/data/map_img/EstShindoImg/eew/" + DateTime.Now.ToString("yyyyMMdd/") + AccessTime2_NIED + ".eew.gif";
                        //テスト
                        //string EEWShindo_URL = "http://www.kmoni.bosai.go.jp/data/map_img/EstShindoImg/eew/20220122/20220122010851.eew.gif";
                        //テストの場合EEWShindoURLをテストにしないとここでエラー(1x1の画像になるため)
                        Stream EEWShindo1 = wc.OpenRead(EEWShindo_URL);

                        Bitmap EEWShindo2 = new Bitmap(EEWShindo1);
                        double CurrectLocationExpectedCalcintensity1 = -5.0;
                        try
                        {
                            Color EEWColor1 = EEWShindo2.GetPixel(Settings.Default.UserPixelX, Settings.Default.UserPixelY);
                            string EEWColor2 = EEWColor1.R + "," + EEWColor1.G + "," + EEWColor1.B;
                            CurrectLocationExpectedCalcintensity1 = KyoshinIntDictionary[EEWColor2];
                        }
                        catch (ArgumentOutOfRangeException)
                        {

                        }
                        EEWShindo1.Close();
                        CurrectLocationExpectedCalcintensity.Text = "現在地予想震度:" + (CurrectLocationExpectedCalcintensity1 + ".0").Replace(".1.0", ".1").Replace(".2.0", ".2").Replace(".3.0", ".3").Replace(".4.0", ".4").Replace(".5.0", ".5").Replace(".6.0", ".6").Replace(".7.0", ".7").Replace(".8.0", ".8").Replace(".9.0", ".9").Replace("-5.0", "- - -");

                    }
                    Distance.Text = "震央距離 約" + Distance4_iedred + "km";

                    string Final_str = "　　";
                    if (Final_iedred == true)
                    {
                        Final_str = "最終";
                    }

                    string MagDepText = $"M{Magunitude_iedred}　　　{Depth_str_iedred}";



                    if (Plum_iedred == true)
                    {
                        MagDepText = "PLUM法による予測";
                    }

                    if (MaxCal_int_iedred == 0)
                    {
                        EEW2.BackgroundImage = Resources.Shindo0;
                    }
                    else if (MaxCal_int_iedred == 1)
                    {
                        EEW2.BackgroundImage = Resources.Shindo1;
                    }
                    else if (MaxCal_int_iedred == 2)
                    {
                        EEW2.BackgroundImage = Resources.Shindo2;
                    }
                    else if (MaxCal_int_iedred == 3)
                    {
                        EEW2.BackgroundImage = Resources.Shindo3;
                    }
                    else if (MaxCal_int_iedred == 4)
                    {
                        EEW2.BackgroundImage = Resources.Shindo4;
                    }
                    else if (MaxCal_int_iedred == 5)
                    {
                        EEW2.BackgroundImage = Resources.Shindo5;
                    }
                    else if (MaxCal_int_iedred == 6)
                    {
                        EEW2.BackgroundImage = Resources.Shindo6;
                    }
                    else if (MaxCal_int_iedred == 7)
                    {
                        EEW2.BackgroundImage = Resources.Shindo7;
                    }
                    else if (MaxCal_int_iedred == 8)
                    {
                        EEW2.BackgroundImage = Resources.Shindo8;
                    }
                    else if (MaxCal_int_iedred == 9)
                    {
                        EEW2.BackgroundImage = Resources.Shindo9;
                    }
                    if (MaxCal_str_iedred == "-")
                    {
                        EEW2.BackgroundImage = Resources.ShindoNull;
                    }
                    Bitmap Canvas = new Bitmap(500, 100);
                    Graphics Gra = Graphics.FromImage(Canvas);
                    Font Shingen_Font = new Font("Koruri Regular", 30);
                    Font Saidai_Font = new Font("Koruri Regular", 10);
                    Font MagDep_Font = new Font("Roboto", 25);
                    Brush DrawColor = Brushes.White;
                    if (MaxCal_int_iedred == 3 || MaxCal_int_iedred == 4 || MaxCal_int_iedred == 5 || MaxCal_int_iedred == 6)
                    {
                        DrawColor = Brushes.Black;
                    }
                    Gra.DrawString($"{Region_name_iedred}", Shingen_Font, DrawColor, 102, 4);
                    Gra.DrawString($"予想最大震度", Saidai_Font, DrawColor, 5, 5);
                    Gra.DrawString($"{MagDepText}", MagDep_Font, DrawColor, 230, 60);
                    Shingen_Font.Dispose();
                    Saidai_Font.Dispose();
                    MagDep_Font.Dispose();
                    Gra.Dispose();
                    EEW2.Image = null;
                    EEW2.Image = Canvas;


                    //Graphics.Clear(Color.Teal);
                    EEW1.Text = $"緊急地震速報({Flg_iedred})　第{Num_iedred}報 {Final_str}　{Origin_Time_str_iedred}";

                    if (Flg_iedred == "予報")
                    {
                        EEW1.BackColor = Color.FromArgb(25, 100, 25);

                    }
                    else if (Flg_iedred == "警報")
                    {
                        EEW1.BackColor = Color.FromArgb(255, 0, 0);
                        string WarnArea = "　";
                        foreach (string WarnArea_ in EEW_iedred_json.WarnForecast.District)
                        {
                            if (WarnArea.Count() <= 20)
                            {
                                WarnArea = (WarnArea + WarnArea_ + "　").Replace("　　", "");
                            }
                        }
                        Settings.Default.WarnArea = WarnArea;

                        if (NowTime_iedred - Origin_Time_long_iedred <= 20)//秒数を長くし、設定ファイルを読み込む
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


                            string tokens_json = File.ReadAllText($"Tokens.json");

                            Tokens_JSON Tokens_jsondata = JsonConvert.DeserializeObject<Tokens_JSON>(tokens_json);
                            Tokens tokens = Tokens.Create(Tokens_jsondata.ConsumerKey, Tokens_jsondata.ConsumerSecret, Tokens_jsondata.AccessToken, Tokens_jsondata.AccessSecret);
                            string TweetText = "緊急地震速報(警報) 強い揺れに警戒\n" + WarnArea;
                            CoreTweet.Status status = tokens.Statuses.Update(new { status = TweetText });
                        }//TweetTest
                        /*
                        else if (NowTime_iedred - Origin_Time_iedred >= 9999999)
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                            string tokens_json = File.ReadAllText($"Tokens.json");

                            Tokens_JSON Tokens_jsondata = JsonConvert.DeserializeObject<Tokens_JSON>(tokens_json);
                            Tokens tokens = Tokens.Create(Tokens_jsondata.ConsumerKey, Tokens_jsondata.ConsumerSecret, Tokens_jsondata.AccessToken, Tokens_jsondata.AccessSecret);
                            string TweetText = "EEWTweetTest";
                            CoreTweet.Status status = tokens.Statuses.Update(new { status = TweetText });
                        }*/

                    }
                    if (TsunamiWarn1_iedred == false && TsunamiWarn2_iedred == false)
                    {
                        TsunamiWarn.BackColor = Color.FromArgb(60, 60, 90);
                    }
                    if (TsunamiWarn1_iedred)
                    {
                        TsunamiWarn.BackColor = Color.Yellow;
                    }
                    if (TsunamiWarn2_iedred)
                    {
                        TsunamiWarn.BackColor = Color.Red;
                    }

                }
                if (InfoFlag_NIED == false && InfoFlag_iedred == false)
                {
                    EEW1.Text = $"緊急地震速報";
                    EEW1.BackColor = Color.FromArgb(45, 45, 90);
                    EEW2.BackgroundImage = Resources.Null;
                    EEW2.Image = null;
                    CurrectLocationExpectedCalcintensity.Text = "";
                    Distance.Text = "";
                    EEW1.ForeColor = Color.White;
                    Settings.Default.WarnArea = "";
                    TsunamiWarn.BackColor = Color.FromArgb(60, 60, 90);
                    TsunamiWarn.Text = "";
                }
                Message.Text = "";
            }
            catch (Exception ex)
            {
                Message.Text = $"エラー発生: \n{ex}";
            }
        }



        public Dictionary<string, double> KyoshinIntDictionary = new Dictionary<string, double>
        {

{"0,0,0",-3.0},
{"0,0,205",-3.0},
{"0,7,209",-2.9},
{"0,14,214",-2.8},
{"0,21,218",-2.7},
{"0,28,223",-2.6},
{"0,36,227",-2.5},
{"0,43,231",-2.4},
{"0,50,236",-2.3},
{"0,57,240",-2.2},
{"0,64,245",-2.1},
{"0,72,250",-2.0},
{"0,85,238",-1.9},
{"0,99,227",-1.8},
{"0,112,216",-1.7},
{"0,126,205",-1.6},
{"0,140,194",-1.5},
{"0,153,183",-1.4},
{"0,167,172",-1.3},
{"0,180,161",-1.2},
{"0,194,150",-1.1},
{"0,208,139",-1.0},
{"6,212,130",-0.9},
{"12,216,121",-0.8},
{"18,220,113",-0.7},
{"25,224,104",-0.6},
{"31,228,96",-0.5},
{"37,233,88",-0.4},
{"44,237,79",-0.3},
{"50,241,71",-0.2},
{"56,245,62",-0.1},
{"63,250,54",0.0},
{"75,250,49",0.1},
{"88,250,45",0.2},
{"100,251,41",0.3},
{"113,251,37",0.4},
{"125,252,33",0.5},
{"138,252,28",0.6},
{"151,253,24",0.7},
{"163,253,20",0.8},
{"176,254,16",0.9},
{"189,255,12",1.0},
{"195,254,10",1.1},
{"202,254,9",1.2},
{"208,254,8",1.3},
{"215,254,7",1.4},
{"222,255,5",1.5},
{"228,254,4",1.6},
{"235,255,3",1.7},
{"241,254,2",1.8},
{"248,255,1",1.9},
{"255,255,0",2.0},
{"254,251,0",2.1},
{"254,248,0",2.2},
{"254,244,0",2.3},
{"254,241,0",2.4},
{"255,238,0",2.5},
{"254,234,0",2.6},
{"255,231,0",2.7},
{"254,227,0",2.8},
{"255,224,0",2.9},
{"255,221,0",3.0},
{"254,213,0",3.1},
{"254,205,0",3.2},
{"254,197,0",3.3},
{"254,190,0",3.4},
{"255,182,0",3.5},
{"254,174,0",3.6},
{"255,167,0",3.7},
{"254,159,0",3.8},
{"255,151,0",3.9},
{"255,144,0",4.0},
{"254,136,0",4.1},
{"254,128,0",4.2},
{"254,121,0",4.3},
{"254,113,0",4.4},
{"255,106,0",4.5},
{"254,98,0",4.6},
{"255,90,0",4.7},
{"254,83,0",4.8},
{"255,75,0",4.9},
{"255,68,0",5.0},
{"254,61,0",5.1},
{"253,54,0",5.2},
{"252,47,0",5.3},
{"251,40,0",5.4},
{"250,33,0",5.5},
{"249,27,0",5.6},
{"248,20,0",5.7},
{"247,13,0",5.8},
{"246,6,0",5.9},
{"245,0,0",6.0},
{"238,0,0",6.1},
{"230,0,0",6.2},
{"223,0,0",6.3},
{"215,0,0",6.4},
{"208,0,0",6.5},
{"200,0,0",6.6},
{"192,0,0",6.7},
{"185,0,0",6.8},
{"177,0,0",6.9},
{"170,0,0",7.0}
        };
    }
}