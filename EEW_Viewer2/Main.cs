using CoreTweet;
using EEW_Viewer2.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EEW_Viewer2
{
    public partial class EEW_Viewer2 : Form
    {
        public EEW_Viewer2()
        {
            InitializeComponent();
        }
        private void EEW_Viewer2_Load(object sender, EventArgs e)
        {
            NIED.Enabled = true;
            Thread.Sleep(999);
            Iedred.Enabled = true;
            Settings.Default.Reload();
            WarnArea WarnArea = new WarnArea();
            WarnArea.Show();
        }
        public void NIED_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime StartTime = DateTime.Now;
                string Plustext = "";
                WebClient WebClient = new WebClient
                {
                    Encoding = Encoding.UTF8
                };
                string NIEDAccessTimeSt1 = (DateTime.Now - Settings.Default.NIEDDelay).ToString("yyyyMMddHHmmss");
                string NIEDAccessTimeSt2 = (DateTime.Now - Settings.Default.NIEDDelay).ToString("yyyyMMdd/yyyyMMddHHmmss");
                //通常
                string EEWJSON = WebClient.DownloadString($"http://www.kmoni.bosai.go.jp/webservice/hypo/eew/{NIEDAccessTimeSt1}.json");
                //テスト用 EEW予報　震度2
                //string EEWJSON = WebClient.DownloadString("http://www.kmoni.bosai.go.jp/webservice/hypo/eew/20220123095750.json");
                //テスト用　EEW予報 震度不明
                //string EEWJSON = WebClient.DownloadString("http://www.kmoni.bosai.go.jp/webservice/hypo/eew/20210705012355.json");
                //テスト用　EEW警報　2021福島県沖
                //string EEWJSON = WebClient.DownloadString("http://www.kmoni.bosai.go.jp/webservice/hypo/eew/20210213231450.json");
                NIEDJsonClass EEWJson = JsonConvert.DeserializeObject<NIEDJsonClass>(EEWJSON);
                if (EEWJson.Alertflg == "予報" || EEWJson.Alertflg == "警報")
                {
                    if (Convert.ToInt32(EEWJson.Report_num) > ReportNum)
                    {
                        Latitude = Convert.ToDouble(EEWJson.Latitude);
                        Longitude = Convert.ToDouble(EEWJson.Longitude);
                        MaxCalcintensityInt = Convert.ToInt32(EEWJson.Calcintensity.Replace("7", "9").Replace("不明", "0").Replace("5弱", "5").Replace("5強", "6").Replace("6弱", "7").Replace("6強", "8"));
                        MaxCalcintensitySt = EEWJson.Calcintensity.Replace("不明", "0");
                        string OriginTimeStyyyy = EEWJson.Origin_time.Remove(4, 10);
                        string OriginTimeStMM = EEWJson.Origin_time.Remove(0, 4).Remove(2, 8);
                        string OriginTimeStdd = EEWJson.Origin_time.Remove(0, 6).Remove(2, 6);
                        string OriginTimeStHH = EEWJson.Origin_time.Remove(0, 8).Remove(2, 4);
                        string OriginTimeStmm = EEWJson.Origin_time.Remove(0, 10).Remove(2, 2);
                        string OriginTimeStss = EEWJson.Origin_time.Remove(0, 12);
                        OriginTimeSt = $"{OriginTimeStyyyy}/{OriginTimeStMM}/{OriginTimeStdd} {OriginTimeStHH}:{OriginTimeStmm}:{OriginTimeStss}";
                        DepthSt = EEWJson.Depth;
                        Magnitude = Convert.ToDouble(EEWJson.Magunitude);
                        RegionName = EEWJson.Region_name;
                        ReportNum = Convert.ToInt32(EEWJson.Report_num);
                        Flag = EEWJson.Alertflg;
                        if (ReportNum <= 3 && Magnitude > 4.0)
                        {
                            MaxCalcintensitySt = Convert.ToString(EEWJson.Calcintensity).Replace("0", "-");
                        }
                        Final = EEWJson.Is_final == "true";
                        Training = EEWJson.Is_training == "true";
                        Cancel = EEWJson.Is_cancel == "true";
                        Plum = Magnitude == 1.0;
                        int DepthInt = Convert.ToInt32(EEWJson.Depth.Replace("km", ""));
                        TsunamiWarn1 = Magnitude >= 6.0 && DepthInt <= 30 || Magnitude >= 7.0 && DepthInt <= 50;
                        TsunamiWarn2 = Magnitude >= 7.0 && DepthInt <= 10 || Magnitude >= 8.0 && DepthInt <= 30;
                        double DistanceLatitude = Convert.ToDouble(Convert.ToString(Settings.Default.UserLatitude - Latitude).Replace("-", "")) * 111.263;
                        double DistanceLongnitude = 2 * Math.PI * 6378.137 * Math.Cos((Settings.Default.UserLatitude + Latitude) / 2) / 360 * Convert.ToDouble(Convert.ToString(Settings.Default.UserLongitude - Longitude).Replace("-", ""));
                        double DistanceDou = Math.Sqrt(DistanceLatitude * DistanceLatitude + DistanceLongnitude * DistanceLongnitude);//---.-------km
                        DistanceInt = (int)Math.Round(DistanceDou);//---km
                        int Distance10Int = (int)Math.Round(DistanceDou / 10) * 10;//震央距離x0.1→四捨五入→10倍(--0km)
                        Stream EEWShindoStream = WebClient.OpenRead($"http://www.kmoni.bosai.go.jp/data/map_img/EstShindoImg/eew/{NIEDAccessTimeSt2}.eew.gif");
                        Bitmap EEWShindoBitmap = new Bitmap(EEWShindoStream);
                        CurrectLocationExpectedCalcintensity = -5.0;
                        try
                        {
                            Color EEWColor = EEWShindoBitmap.GetPixel(Settings.Default.UserKyoshinPixelX, Settings.Default.UserKyoshinPixelY);
                            string EEWColorSt = EEWColor.R + "," + EEWColor.G + "," + EEWColor.B;
                            CurrectLocationExpectedCalcintensity = KyoshinIntDictionary[EEWColorSt];
                        }
                        catch (ArgumentOutOfRangeException)//予想地図未発表時
                        {

                        }
                        EEWShindoStream.Dispose();
                        EEWShindoBitmap.Dispose();
                        InfoFlagNIED = true;
                        Display();
                        if (EEWJson.Alertflg == "予報")
                        {
                            Settings.Default.QuakeLat = Latitude;
                            Settings.Default.QuakeLong = Longitude;
                            Settings.Default.WarnArea = "予報";
                        }
                    }
                    else
                    {
                        Plustext = "(表示停止中)";
                    }
                }
                else
                {
                    InfoFlagNIED = false;
                }
                DateTime EndTime = DateTime.Now;
                int DisplayTime = Convert.ToInt32((EndTime - StartTime).TotalMilliseconds);
                Message.Text = $"取得元:NIED {Convert.ToString(InfoFlagNIED).Replace("True", "発表中").Replace("False", "発表なし")}{Plustext}\n{DateTime.Now:HH:mm:ss}取得(遅延{Settings.Default.NIEDDelay.TotalSeconds}s)\n表示時間:{DisplayTime}ms";
            }
            catch (Exception ex)
            {
                Message.Text = $"NIED エラー発生: \n{ex}";
            }
        }
        private void Iedred_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime StartTime = DateTime.Now;
                WebClient WebClient = new WebClient
                {
                    Encoding = Encoding.UTF8
                };
                //通常
                string EEWJSON = WebClient.DownloadString("https://api.iedred7584.com/eew/json/");
                //テスト用　EEW警報
                //string EEWJSON = File.ReadAllText($"iedred.warn.json");
                //テスト用　自由
                //string EEWJSON = File.ReadAllText($"iedred.user.json");
                IedredJsonClass EEWJson = JsonConvert.DeserializeObject<IedredJsonClass>(EEWJSON);
                DateTime OriginTime = Convert.ToDateTime(EEWJson.OriginTime.String);
                DateTime NowTime = DateTime.Now;
                long ProgressTimeLong = (long)(NowTime - OriginTime).TotalSeconds;
                if (ProgressTimeLong <= 240)//通常時　<=
                {
                    Latitude = EEWJson.Hypocenter.Location.Lat;
                    Longitude = EEWJson.Hypocenter.Location.Long;
                    MaxCalcintensityInt = Convert.ToInt32(EEWJson.MaxIntensity.String.Replace("7", "9").Replace("不明", "0").Replace("5弱", "5").Replace("5強", "6").Replace("6弱", "7").Replace("6強", "8"));
                    MaxCalcintensitySt = EEWJson.MaxIntensity.String.Replace("不明", "0");
                    OriginTimeSt = EEWJson.OriginTime.String;
                    DepthSt = EEWJson.Hypocenter.Location.Depth.Int + "km";
                    Magnitude = EEWJson.Hypocenter.Magnitude.Float;
                    RegionName = EEWJson.Hypocenter.Name;
                    ReportNum = EEWJson.Serial;
                    Flag = EEWJson.Title.String.Replace("緊急地震速報（", "").Replace("）", ""); ;
                    int DepthInt = EEWJson.Hypocenter.Location.Depth.Int;
                    if (ReportNum <= 3 && Magnitude > 4.0 || DepthInt >150)
                    {
                        MaxCalcintensitySt = MaxCalcintensitySt.Replace("0", "-");
                    }
                    Final = EEWJson.Type.Detail.IndexOf("最終") != -1;
                    Training = EEWJson.Status.Code != "00" && EEWJson.Status.Code != "10";
                    Plum = EEWJson.Hypocenter.IsAssumption == true;
                    Cancel = EEWJson.Status.Code == "10";
                    TsunamiWarn1 = Magnitude >= 6.0 && DepthInt <= 30 || Magnitude >= 7.0 && DepthInt <= 50;
                    TsunamiWarn2 = Magnitude >= 7.0 && DepthInt <= 10 || Magnitude >= 8.0 && DepthInt <= 30;
                    double DistanceLatitude = Convert.ToDouble(Convert.ToString(Settings.Default.UserLatitude - Latitude).Replace("-", "")) * 111.263;
                    double DistanceLongitude = 2 * Math.PI * 6378.137 * Math.Cos((Settings.Default.UserLatitude + Latitude) / 2) / 360 * Convert.ToDouble(Convert.ToString(Settings.Default.UserLongitude - Longitude).Replace("-", ""));
                    double DistanceDou = Math.Sqrt(DistanceLatitude * DistanceLatitude + DistanceLongitude * DistanceLongitude);//---.-------km
                    DistanceInt = (int)Math.Round(DistanceDou);//---km
                    int Distance10Int = (int)Math.Round(DistanceDou / 10) * 10;//震央距離x0.1→四捨五入→10倍(--0km)
                    string NIEDAccessTimeSt = (DateTime.Now - Settings.Default.NIEDDelay).ToString("yyyyMMdd/yyyyMMddHHmmss");
                    Stream EEWShindoStream = WebClient.OpenRead($"http://www.kmoni.bosai.go.jp/data/map_img/EstShindoImg/eew/{NIEDAccessTimeSt}.eew.gif");
                    Bitmap EEWShindoBitmap = new Bitmap(EEWShindoStream);
                    CurrectLocationExpectedCalcintensity = -5.0;
                    try
                    {
                        Color EEWColor = EEWShindoBitmap.GetPixel(Settings.Default.UserKyoshinPixelX, Settings.Default.UserKyoshinPixelY);
                        string EEWColorSt = EEWColor.R + "," + EEWColor.G + "," + EEWColor.B;
                        CurrectLocationExpectedCalcintensity = KyoshinIntDictionary[EEWColorSt];
                    }
                    catch (ArgumentOutOfRangeException)//予想地図未発表時
                    {

                    }
                    EEWShindoStream.Dispose();
                    EEWShindoBitmap.Dispose();
                    InfoFlagIedred = true;
                    Display();
                    Settings.Default.QuakeLat = Latitude;
                    Settings.Default.QuakeLong = Longitude;
                    long WarnTweetProgressTimeLong = (long)(NowTime - LatestWarnTweetTime).TotalSeconds;

                    if (Flag == "警報")
                    {
                        string WarnArea1 = "";
                        string WarnArea2 = "";
                        string WarnArea3 = "";
                        for (int i = 0; i < EEWJson.WarnForecast.District.Count; i++)
                        {
                            WarnArea1 += $"　{EEWJson.WarnForecast.District[i]}";
                        }
                        WarnArea1 = WarnArea1.Remove(0, 1);
                        for (int i = 0; i < EEWJson.WarnForecast.LocalAreas.Count; i++)
                        {
                            WarnArea2 += $"　{EEWJson.WarnForecast.LocalAreas[i]}";
                        }
                        WarnArea2 = WarnArea2.Remove(0, 1);
                        for (int i = 0; i < EEWJson.WarnForecast.Regions.Count; i++)
                        {
                            WarnArea3 += $"　{EEWJson.WarnForecast.Regions[i]}";
                        }
                        WarnArea3 = WarnArea3.Remove(0, 1);
                        Settings.Default.WarnArea = WarnArea1;
                        Settings.Default.WarnArea2 = WarnArea2;
                        if (ProgressTimeLong <= 600 && Settings.Default.IsTweet && WarnTweetProgressTimeLong <= 240)
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                            string TokensJSON = File.ReadAllText($"Tokens.json");
                            Tokens_JSON TokensJson = JsonConvert.DeserializeObject<Tokens_JSON>(TokensJSON);
                            Tokens Tokens = Tokens.Create(TokensJson.ConsumerKey, TokensJson.ConsumerSecret, TokensJson.AccessToken, TokensJson.AccessSecret);
                            string TweetText1 = "緊急地震速報(警報) 　" + RegionName + "で地震　強い揺れに警戒\n" + WarnArea1;
                            if (TweetText1.Length > 120)
                            {
                                TweetText1 = TweetText1.Remove(120, TweetText1.Length - 120) + "…";
                            }
                            string TweetText2 = "詳細:" + WarnArea2;
                            if (TweetText2.Length > 119)
                            {
                                TweetText2 = TweetText2.Remove(119, TweetText1.Length - 119) + "…";
                            }
                            CoreTweet.Status Status1 = Tokens.Statuses.Update(new { status = TweetText1 });
                            CoreTweet.Status Status2 = Tokens.Statuses.Update(new { status = TweetText2, in_reply_to_status_id = Status1.Id });
                        }
                    }
                    else
                    {
                        Settings.Default.WarnArea = "予報";
                    }
                    if (Final && Settings.Default.IsTweet)
                    {
                        string TweetText = $"緊急地震速報({Flag}) 第{ReportNum}報(最終)\n震源:{RegionName}\n推定最大震度{MaxCalcintensitySt}　M{(Magnitude + ".0").Replace(".1.0", ".1").Replace(".2.0", ".2").Replace(".3.0", ".3").Replace(".4.0", ".4").Replace(".5.0", ".5").Replace(".6.0", ".6").Replace(".7.0", ".7").Replace(".8.0", ".8").Replace(".9.0", ".9")}　深さ{DepthSt}\n{OriginTimeSt}発生";
                        if (Training)
                        {
                            TweetText = TweetText.Replace(Flag, "訓練");
                        }
                        if (Plum)
                        {
                            TweetText = TweetText.Replace($"M{(Magnitude + ".0").Replace(".1.0", ".1").Replace(".2.0", ".2").Replace(".3.0", ".3").Replace(".4.0", ".4").Replace(".5.0", ".5").Replace(".6.0", ".6").Replace(".7.0", ".7").Replace(".8.0", ".8").Replace(".9.0", ".9")}　深さ{DepthSt}", "PLUM法による予測");
                        }
                        if (TsunamiWarn1)
                        {
                            TweetText += "念のため津波に注意";
                        }
                        if (FinalTweetText != TweetText)
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                            string TokensJSON = File.ReadAllText($"Tokens.json");
                            Tokens_JSON TokensJson = JsonConvert.DeserializeObject<Tokens_JSON>(TokensJSON);
                            Tokens Tokens = Tokens.Create(TokensJson.ConsumerKey, TokensJson.ConsumerSecret, TokensJson.AccessToken, TokensJson.AccessSecret);
                            Tokens.Statuses.Update(new { status = TweetText });
                            FinalTweetText = TweetText;
                        }
                    }
                }
                else
                {
                    InfoFlagIedred = false;
                }
                DateTime EndTime = DateTime.Now;
                int DisplayTime = Convert.ToInt32((EndTime - StartTime).TotalMilliseconds);
                Message.Text = $"取得元:iedred {Convert.ToString(InfoFlagIedred).Replace("True", "発表中").Replace("False", "発表なし")}\n{DateTime.Now:HH:mm:ss}取得(遅延0s)\n表示時間:{DisplayTime}ms";
            }
            catch (Exception ex)
            {
                Message.Text = $"iedred エラー発生: \n{ex}";
            }
        }
        public void Display()
        {
            Bitmap Bitmap = new Bitmap(500, 100);
            Graphics Graphics = Graphics.FromImage(Bitmap);
            Font ShingenFont = new Font("Koruri Regular", 30);
            Font SaidaiFont = new Font("Koruri Regular", 10);
            Font MagDepFont = new Font("Roboto", 25);
            Brush DrawBrush = Brushes.White;
            if (MaxCalcintensitySt == "3" || MaxCalcintensitySt == "4" || MaxCalcintensitySt == "5弱" || MaxCalcintensitySt == "5強")
            {
                DrawBrush = Brushes.Black;
            }
            string MagDepText = $"M{(Magnitude + ".0").Replace(".1.0", ".1").Replace(".2.0", ".2").Replace(".3.0", ".3").Replace(".4.0", ".4").Replace(".5.0", ".5").Replace(".6.0", ".6").Replace(".7.0", ".7").Replace(".8.0", ".8").Replace(".9.0", ".9")}　　　{DepthSt}";
            if (Plum)
            {
                MagDepText = "PLUM法による予測";
            }
            if (Training)
            {
                Graphics.DrawString($"《訓練》{RegionName}", ShingenFont, DrawBrush, 102, 4);
            }
            else
            {
                Graphics.DrawString($"{RegionName}", ShingenFont, DrawBrush, 102, 4);
            }
            Graphics.DrawString($"予想最大震度", SaidaiFont, DrawBrush, 5, 5);
            Graphics.DrawString($"{MagDepText}", MagDepFont, DrawBrush, 230, 60);
            ShingenFont.Dispose();
            SaidaiFont.Dispose();
            MagDepFont.Dispose();
            Graphics.Dispose();
            EEW2.Image = null;
            EEW2.Image = Bitmap;
            string FinalSt = "　　";
            if (Final)
            {
                FinalSt = "最終";
            }
            EEW1.Text = $"緊急地震速報({Flag}) 第{ReportNum}報 {FinalSt}　{OriginTimeSt}";
            CurrectLocationCalcintensity.Text = $"現在地予想震度:{(CurrectLocationExpectedCalcintensity + ".0").Replace(".1.0", ".1").Replace(".2.0", ".2").Replace(".3.0", ".3").Replace(".4.0", ".4").Replace(".5.0", ".5").Replace(".6.0", ".6").Replace(".7.0", ".7").Replace(".8.0", ".8").Replace(".9.0", ".9").Replace("-5.0", "- - -")}";
            Distance.Text = $"震央距離 約{DistanceInt}km";
            if (Flag == "予報")
            {
                EEW1.BackColor = Color.FromArgb(25, 100, 25);
            }
            else if (Flag == "警報")
            {
                EEW1.BackColor = Color.FromArgb(255, 0, 0);
            }
            if (TsunamiWarn1 == false && TsunamiWarn2 == false)
            {
                TsunamiWarn.BackColor = Color.FromArgb(60, 60, 90);
            }
            if (MaxCalcintensitySt == "0")
            {
                EEW2.BackgroundImage = Resources.Shindo0;
            }
            else if (MaxCalcintensitySt == "1")
            {
                EEW2.BackgroundImage = Resources.Shindo1;
            }
            else if (MaxCalcintensitySt == "2")
            {
                EEW2.BackgroundImage = Resources.Shindo2;
            }
            else if (MaxCalcintensitySt == "3")
            {
                EEW2.BackgroundImage = Resources.Shindo3;
            }
            else if (MaxCalcintensitySt == "4")
            {
                EEW2.BackgroundImage = Resources.Shindo4;
            }
            else if (MaxCalcintensitySt == "5弱")
            {
                EEW2.BackgroundImage = Resources.Shindo5;
            }
            else if (MaxCalcintensitySt == "5強")
            {
                EEW2.BackgroundImage = Resources.Shindo6;
            }
            else if (MaxCalcintensitySt == "6弱")
            {
                EEW2.BackgroundImage = Resources.Shindo7;
            }
            else if (MaxCalcintensitySt == "6強")
            {
                EEW2.BackgroundImage = Resources.Shindo8;
            }
            else if (MaxCalcintensitySt == "7")
            {
                EEW2.BackgroundImage = Resources.Shindo9;
            }
            else
            {
                EEW2.BackgroundImage = Resources.ShindoNull;
            }
            if (Training)
            {
                EEW2.BackgroundImage = Resources.ShindoNull;
                EEW1.Text = $"緊急地震速報(訓練) 第{ReportNum}報 {FinalSt}　{OriginTimeSt}";
            }
            if (TsunamiWarn1)
            {
                TsunamiWarn.BackColor = Color.Yellow;
            }
            if (TsunamiWarn2)
            {
                TsunamiWarn.BackColor = Color.Red;
            }
        }
        private void NoEEW_Tick(object sender, EventArgs e)
        {
            if (InfoFlagNIED == false && InfoFlagIedred == false)
            {
                EEW1.Text = $"緊急地震速報　受信待機中";
                EEW1.BackColor = Color.FromArgb(45, 45, 90);
                EEW1.ForeColor = Color.White;
                EEW2.BackgroundImage = Resources.Null;
                EEW2.Image = null;
                CurrectLocationCalcintensity.Text = "";
                Distance.Text = "";
                Settings.Default.WarnArea = "";
                TsunamiWarn.BackColor = Color.FromArgb(60, 60, 90);
            }
        }
        public DateTime LatestWarnTweetTime = DateTime.Now;
        public bool InfoFlagNIED = false;
        public bool InfoFlagIedred = false;
        public double Latitude = 0;
        public double Longitude = 0;
        public string MaxCalcintensitySt = "";
        public int MaxCalcintensityInt = 0;
        public string OriginTimeSt = "";
        public string DepthSt = "";
        public int ReportNum = 0;
        public double Magnitude = 0;
        public string RegionName = "";
        public bool Training = false;
        public bool Final = false;
        public bool Plum = false;
        public int DistanceInt = 0;
        public bool TsunamiWarn1 = false;
        public bool TsunamiWarn2 = false;
        public string Flag = "";
        public double CurrectLocationExpectedCalcintensity = -5.0;
        public bool Cancel = false;
        public string FinalTweetText = "";


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

        private void TimeControl_Tick(object sender, EventArgs e)
        {
            Message.Text = "時刻補正中…";
            NIED.Enabled = false;
            Iedred.Enabled = false;
            NIED.Enabled = true;
            Thread.Sleep(1000);
            Iedred.Enabled = true;
        }
    }
}