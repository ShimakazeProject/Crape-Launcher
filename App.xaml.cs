using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

using Str = CLauncher.Configs.Strings;
using Clr = CLauncher.Config.Colors;

using Global = CLauncher.Config.Conf;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Json;

using CLauncher.Configs;
using Win = CLauncher.Configs.UI.Win;
using System.Windows.Controls;

namespace CLauncher
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static Frame GetFrame(Page page)
        {
            Frame pageFrame = null;
            DependencyObject currParent = VisualTreeHelper.GetParent(page);
            while (currParent != null && pageFrame == null)
            {
                pageFrame = currParent as Frame;
                currParent = VisualTreeHelper.GetParent(currParent);
            }
            return pageFrame;
        }

        public static JsonValue GetJsonValue(JsonObject json,string key)
        {
            JsonValue jvStr;
            json.TryGetValue(key, out jvStr);
            return jvStr;
        }

        public static JsonValue LoadJsonFromFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string json = "";
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine().Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries)[0];
                if (str != null) json += str;
            }
            return JsonValue.Parse(json);
        }

        public static Brush String2Brush(string color)
        {
            System.Drawing.Color clr = System.Drawing.ColorTranslator.FromHtml(color);
            return new SolidColorBrush(Color.FromArgb(clr.A, clr.R, clr.G, clr.B));
        }
        public App()
        {
            #region 字符串初始化
            {
                JsonObject strjson = (JsonObject)LoadJsonFromFile(@"Resource\Strings.json");
                Str.About = GetJsonValue(strjson, "About");
                Str.Mission = GetJsonValue(strjson, "Mission");
                Str.Summary = GetJsonValue(strjson, "Summary");
                Str.Easy = GetJsonValue(strjson, "Easy");
                Str.Normal = GetJsonValue(strjson, "Normal");
                Str.Hard = GetJsonValue(strjson, "Hard");
                Str.Run = GetJsonValue(strjson, "Run");
                Str.VideoSettings = GetJsonValue(strjson, "VideoSettings");
                Str.Screen = GetJsonValue(strjson, "Screen");
                Str.IsWindow = GetJsonValue(strjson, "IsWindow");
                Str.NoFrame = GetJsonValue(strjson, "NoFrame");
                Str.UseGraphicsAPI = GetJsonValue(strjson, "UseGraphicsAPI");
                Str.GraphicsAPI = GetJsonValue(strjson, "GraphicsAPI");
                Str.AudioSettings = GetJsonValue(strjson, "AudioSettings");
                Str.BGM = GetJsonValue(strjson, "BGM");
                Str.VOX = GetJsonValue(strjson, "VOX");
                Str.SEM = GetJsonValue(strjson, "SEM");
            }
            #endregion
            #region UI设置初始化
            JsonObject json = (JsonObject)LoadJsonFromFile(@"Resource\LauncherConfigs.json");
            {
                JsonObject set = (JsonObject)GetJsonValue(json, "Settings");
                {
                    Settings.IniFileName = GetJsonValue(set, "ra2md.ini");
                    set = (JsonObject)GetJsonValue(set, "Resource");
                    Settings.Resource.Images = GetJsonValue(set, "Images") + "\\";
                    Settings.Resource.Renderers = GetJsonValue(set, "Renderers") + "\\";
                    Settings.Resource.Configs = GetJsonValue(set, "Configs") + "\\";
                }
                //JsonObject ui = (JsonObject)GetJsonValue(json, "UI");
                
            }
            #endregion



            #region RA2CA.INI Reader
            {
                string line;
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"ra2ca.ini", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    // 过滤注释
                    line = sr.ReadLine().Split(';')[0];
                    // 如果是空行则跳过本次循环
                    if (line.Length == 0) continue;
                    // 键值对检测
                    else if (line.Contains("="))
                    {
                        string linea = line.Split('=')[0];
                        string lineb = line.Split('=')[1];
                        switch (linea)
                        {
                            case "Difficulty":
                                Global.Difficult = Convert.ToInt32(lineb);
                                break;
                            case "Video.Windowed":
                                if (lineb.Equals("0")) Global.IsWindow = false;
                                else Global.IsWindow = true;
                                break;
                            case "NoWindowFrame":
                                if (lineb.Equals("0")) Global.NoFrame = false;
                                else Global.NoFrame = true;
                                break;
                            case "SoundVolume":
                                Global.BGM = Convert.ToDouble(lineb);
                                break;
                            case "VoiceVolume":
                                Global.VOX = Convert.ToDouble(lineb);
                                break;
                            case "ScoreVolume":
                                Global.SEM = Convert.ToDouble(lineb);
                                break;
                            case "ScreenWidth":
                                Global.Width = Convert.ToInt32(lineb);
                                break;
                            case "ScreenHeight":
                                Global.Height = Convert.ToInt32(lineb);
                                break;
                            default:
                                break;
                        }
                    }
                }
                sr.Close();
                fs.Dispose();
            }
            #endregion
        }
    }
}
