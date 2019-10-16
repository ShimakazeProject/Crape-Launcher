using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

using Str = CLauncher.Config.Strings;
using Clr = CLauncher.Config.Colors;

using Global = CLauncher.Config.Conf;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Json;

namespace CLauncher
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static JsonValue GetJsonValue(JsonObject json,string key)
        {
            JsonValue jvStr;
            json.TryGetValue(key, out jvStr);
            return jvStr;
        }
        public App()
        {
            #region 字符串初始化
            JsonObject json = (JsonObject)JsonValue.Parse(File.ReadAllText(@"Resource\Strings.json"));
            Str.Title = GetJsonValue(json, "Title");
            Str.Launch = GetJsonValue(json, "Launch");
            Str.Settings = GetJsonValue(json, "Settings");
            Str.About = GetJsonValue(json, "About");
            Str.Exit = GetJsonValue(json, "Exit");
            Str.Mission = GetJsonValue(json, "Mission");
            Str.Summary = GetJsonValue(json, "Summary");
            Str.Easy = GetJsonValue(json, "Easy");
            Str.Normal = GetJsonValue(json, "Normal");
            Str.Hard = GetJsonValue(json, "Hard");
            Str.Run = GetJsonValue(json, "Run");
            Str.VideoSettings = GetJsonValue(json, "VideoSettings");
            Str.Screen = GetJsonValue(json, "Screen");
            Str.IsWindow = GetJsonValue(json, "IsWindow");
            Str.NoFrame = GetJsonValue(json, "NoFrame");
            Str.UseGraphicsAPI = GetJsonValue(json, "UseGraphicsAPI");
            Str.GraphicsAPI = GetJsonValue(json, "GraphicsAPI");
            Str.AudioSettings = GetJsonValue(json, "AudioSettings");
            Str.BGM = GetJsonValue(json, "BGM");
            Str.VOX = GetJsonValue(json, "VOX");
            Str.SEM = GetJsonValue(json, "SEM");
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
