using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CLauncher
{
    public static class Config
    {
        public static class Strings
        {
            public static string Title { get; set; }
            public static string Launch { get; set; }
            public static string Settings { get; set; }
            public static string About { get; set; }
            public static string Exit { get; set; }
            public static string Mission { get; set; }
            public static string Summary { get; set; }
            public static string Easy { get; set; }
            public static string Normal { get; set; }
            public static string Hard { get; set; }
            public static string Run { get; set; }
            public static string VideoSettings { get; set; }
            public static string Screen { get; set; }
            public static string IsWindow { get; set; }
            public static string NoFrame { get; set; }
            public static string UseGraphicsAPI { get; set; }
            public static string GraphicsAPI { get; set; }
            public static string AudioSettings { get; set; }
            public static string BGM { get; set; }
            public static string VOX { get; set; }
            public static string SEM { get; set; }
        }
        public static class Colors
        {
            public static Brush Foreground { get; set; }
            public static Brush ThemeColor { get; set; }
        }

        public static class Conf
        {
            public static int Height { get; set; }
            public static int Width { get; set; }
            public static int Difficult { get; set; }
            public static bool IsWindow { get; set; }
            public static bool NoFrame { get; set; }
            public static bool SpeedCtrl { get; set; }
            public static bool UseLog { get; set; }
            public static double BGM { get; set; }
            public static double VOX { get; set; }
            public static double SEM { get; set; }

            public static string Summary { get; set; }
            public static Brush MWBG { get; set; }
            public static List<MissionList> Missions { get; set; }
            /// <summary>
            /// 写入INI
            /// </summary>
            public static void IniW()
            {
                IniW RA2CA = new IniW(AppDomain.CurrentDomain.BaseDirectory + "ra2ca.ini");
                if (IsWindow) RA2CA.IniWriteValue("Video", "Video.Windowed", "1");
                else RA2CA.IniWriteValue("Video", "Video.Windowed", "0");

                if (NoFrame) RA2CA.IniWriteValue("Video", "NoWindowFrame", "1");
                else RA2CA.IniWriteValue("Video", "NoWindowFrame", "0");

                RA2CA.IniWriteValue("Options", "Difficulty", Difficult.ToString());

                RA2CA.IniWriteValue("Audio", "SoundVolume", BGM.ToString());
                RA2CA.IniWriteValue("Audio", "VoiceVolume", VOX.ToString());
                RA2CA.IniWriteValue("Audio", "ScoreVolume", SEM.ToString());

                RA2CA.IniWriteValue("Video", "ScreenWidth", Width.ToString());
                RA2CA.IniWriteValue("Video", "ScreenHeight", Height.ToString());

            }

            public static string FormatStr(string str)
            {
                return str.Replace(@"\n", "\r\n").Replace(@"\t", "\t");
            }
            static Conf()
            {
                Missions = new List<MissionList>();
            }
        }
    }
}
