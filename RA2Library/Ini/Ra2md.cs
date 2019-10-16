namespace RA2Library.Ini
{
    /// <summary>
    /// Ra2md.ini纯静态类
    /// </summary>
    public static class Ra2md
    {
        private static IniFile ra2md;
        private static string path;
        /// <summary>
        /// 显示设置
        /// </summary>
        public static class Video
        {
            public static bool VideoBackBuffer
            {
                set => ra2md.AddValue("Video", "VideoBackBuffer", value);
                get => ra2md.GetBool("Video", "VideoBackBuffer");
            }
            public static bool AllowHiResModes
            {
                set => ra2md.AddValue("Video", "AllowHiResModes", value);
                get => ra2md.GetBool("Video", "AllowHiResModes");
            }
            public static bool AllowVRAMSidebar
            {
                set => ra2md.AddValue("Video", "AllowVRAMSidebar", value);
                get => ra2md.GetBool("Video", "AllowVRAMSidebar");
            }
            public static bool StretchMovies
            {
                set => ra2md.AddValue("Video", "StretchMovies", value);
                get => ra2md.GetBool("Video", "StretchMovies");
            }
            /// <summary>
            /// 设置是否窗口化
            /// </summary>
            public static bool Windowed
            {
                set => ra2md.AddValue("Video", "Video.Windowed", value);
                get => ra2md.GetBool("Video", "Video.Windowed");
            }
            public static bool ForceLowestDetailLevel
            {
                set => ra2md.AddValue("Video", "ForceLowestDetailLevel", value);
                get => ra2md.GetBool("Video", "ForceLowestDetailLevel");
            }
            /// <summary>
            /// 设置窗口化是否显示边框
            /// </summary>
            public static bool NoWindowFrame
            {
                set => ra2md.AddValue("Video", "NoWindowFrame", value);
                get => ra2md.GetBool("Video", "NoWindowFrame");
            }
            /// <summary>
            /// 设置窗口分辨率 宽
            /// </summary>
            public static int ScreenWidth
            {
                set => ra2md.AddValue("Video", "ScreenWidth", value);
                get => ra2md.GetInt("Video", "ScreenWidth");
            }
            /// <summary>
            /// 设置窗口分辨率 高
            /// </summary>
            public static int ScreenHeight
            {
                set => ra2md.AddValue("Video", "ScreenHeight", value);
                get => ra2md.GetInt("Video", "ScreenHeight");
            }
            public static string Renderer
            {
                set => ra2md.AddValue("Video", "Renderer", value);
                get => ra2md.GetStr("Video", "Renderer");
            }// 渲染器
        }
        /// <summary>
        /// 多人游戏设置
        /// </summary>
        public class MultiPlayer
        {
            /// <summary>
            /// 游戏昵称设置
            /// </summary>
            public static string Handle
            {
                set => ra2md.AddValue("MultiPlayer", "Handle", value);
                get => ra2md.GetStr("MultiPlayer", "Handle");
            }
        }
        /// <summary>
        /// 遭遇战设置
        /// </summary>
        public class Skirmish
        {
            /// <summary>
            /// 快速游戏
            /// </summary>
            public static bool ShortGame
            {
                get => ra2md.GetBool("Skirmish", "ShortGame");
                set => ra2md.AddValue("Skirmish", "ShortGame", value);
            }
            /// <summary>
            /// 超级武器
            /// </summary>
            public static bool SuperWeaponsAllowed
            {
                get => ra2md.GetBool("Skirmish", "SuperWeaponsAllowed");
                set => ra2md.AddValue("Skirmish", "SuperWeaponsAllowed", value);
            }
            /// <summary>
            /// 于盟友建造场旁建造
            /// </summary>
            public static bool BuildOffAlly
            {
                get => ra2md.GetBool("Skirmish", "BuildOffAlly");
                set => ra2md.AddValue("Skirmish", "BuildOffAlly", value);
            }
            /// <summary>
            /// MCV重新部署
            /// </summary>
            public static bool MCVRepacks
            {
                get => ra2md.GetBool("Skirmish", "MCVRepacks");
                set => ra2md.AddValue("Skirmish", "MCVRepacks", value);
            }
            /// <summary>
            /// 小箱子
            /// </summary>
            public static bool CratesAppear
            {
                get => ra2md.GetBool("Skirmish", "CratesAppear");
                set => ra2md.AddValue("Skirmish", "CratesAppear", value);
            }
            /// <summary>
            /// 游戏模式
            /// </summary>
            public static int GameMode
            {
                get => ra2md.GetInt("Skirmish", "GameMode");
                set => ra2md.AddValue("Skirmish", "GameMode", value);
            }
            public static int ScenIndex
            {
                get => ra2md.GetInt("Skirmish", "ScenIndex");
                set => ra2md.AddValue("Skirmish", "ScenIndex", value);
            }
            /// <summary>
            /// 游戏速度
            /// </summary>
            public static int GameSpeed
            {
                get => ra2md.GetInt("Skirmish", "GameSpeed");
                set => ra2md.AddValue("Skirmish", "GameSpeed", value);
            }
            /// <summary>
            /// 资金
            /// </summary>
            public static int Credits
            {
                get => ra2md.GetInt("Skirmish", "Credits");
                set => ra2md.AddValue("Skirmish", "Credits", value);
            }
            /// <summary>
            /// 初始部队数
            /// </summary>
            public static int UnitCount
            {
                get => ra2md.GetInt("Skirmish", "UnitCount");
                set => ra2md.AddValue("Skirmish", "UnitCount", value);
            }

        }
        /// <summary>
        /// 声音设置
        /// </summary>
        public class Audio
        {
            /// <summary>
            /// 播放选单音乐
            /// </summary>
            public static bool PlayMenuMusic
            {
                get => ra2md.GetBool("Audio", "PlayMenuMusic");
                set => ra2md.AddValue("Audio", "PlayMenuMusic", value);
            }
            public static bool EnableButtonHoverSound
            {
                get => ra2md.GetBool("Audio", "EnableButtonHoverSound");
                set => ra2md.AddValue("Audio", "EnableButtonHoverSound", value);
            }
            /// <summary>
            /// 播放主选单音乐
            /// </summary>
            public static bool PlayMainMenuMusic
            {
                get => ra2md.GetBool("Audio", "PlayMainMenuMusic");
                set => ra2md.AddValue("Audio", "PlayMainMenuMusic", value);
            }
            public static bool StopMusicOnMenu
            {
                get => ra2md.GetBool("Audio", "StopMusicOnMenu");
                set => ra2md.AddValue("Audio", "StopMusicOnMenu", value);
            }
            public static bool IsScoreShuffle
            {
                get => ra2md.GetBool("Audio", "IsScoreShuffle");
                set => ra2md.AddValue("Audio", "IsScoreShuffle", value);
            }
            public static bool InGameMusic
            {
                get => ra2md.GetBool("Audio", "InGameMusic");
                set => ra2md.AddValue("Audio", "InGameMusic", value);
            }
            /// <summary>
            /// 重复播放
            /// </summary>
            public static bool IsScoreRepeat
            {
                get => ra2md.GetBool("Audio", "IsScoreRepeat");
                set => ra2md.AddValue("Audio", "IsScoreRepeat", value);
            }
            /// <summary>
            /// 特殊键值对 客户端音量大小
            /// </summary>
            public static double ClientVolume
            {
                get => ra2md.GetDouble("Audio", "ClientVolume");
                set => ra2md.AddValue("Audio", "ClientVolume", value);
            }
            /// <summary>
            /// 主音量
            /// </summary>
            public static double SoundVolume
            {
                get => ra2md.GetDouble("Audio", "SoundVolume");
                set => ra2md.AddValue("Audio", "SoundVolume", value);
            }
            /// <summary>
            /// 语音音量
            /// </summary>
            public static double VoiceVolume
            {
                get => ra2md.GetDouble("Audio", "VoiceVolume");
                set => ra2md.AddValue("Audio", "VoiceVolume", value);
            }
            /// <summary>
            /// 效果音量
            /// </summary>
            public static double ScoreVolume
            {
                get => ra2md.GetDouble("Audio", "VoiceVolume");
                set => ra2md.AddValue("Audio", "ScoreVolume", value);
            }
            public static int SoundLatency
            {
                get => ra2md.GetInt("Audio", "SoundLatency");
                set => ra2md.AddValue("Audio", "SoundLatency", value);
            }
        }
        /// <summary>
        /// 其他选项
        /// </summary>
        public class Options
        {
            /// <summary>
            /// 画质等级
            /// </summary>
            public static int DetailLevel
            {
                get => ra2md.GetInt("Options", "DetailLevel");
                set => ra2md.AddValue("Options", "DetailLevel", value);
            }
            /// <summary>
            /// 显示目标线
            /// </summary>
            public static bool UnitActionLines
            {
                get => ra2md.GetBool("Options", "UnitActionLines");
                set => ra2md.AddValue("Options", "UnitActionLines", value);
            }
            public static bool ScrollMethod
            {
                get => ra2md.GetBool("Options", "ScrollMethod");
                set => ra2md.AddValue("Options", "ScrollMethod", value);
            }
            /// <summary>
            /// 提示隐藏单位
            /// </summary>
            public static bool ShowHidden
            {
                get => ra2md.GetBool("Options", "ShowHidden");
                set => ra2md.AddValue("Options", "ShowHidden", value);
            }
            /// <summary>
            /// 工具提示
            /// </summary>
            public static bool ToolTips
            {
                get => ra2md.GetBool("Options", "ToolTips");
                set => ra2md.AddValue("Options", "ToolTips", value);
            }
            public static int ScrollRate
            {
                get => ra2md.GetInt("Options", "ScrollRate");
                set => ra2md.AddValue("Options", "ScrollRate", value);
            }
            /// <summary>
            /// 难度设置
            /// </summary>
            public static int Difficulty
            {
                get => ra2md.GetInt("Options", "Difficulty");
                set => ra2md.AddValue("Options", "Difficulty", value);
            }
        }
        /// <summary>
        /// 重设ra2md.ini的文件位置并重新读取
        /// </summary>
        /// <param name="path">ra2md.ini的文件位置</param>
        public static void ReSet(string path)
        {
            Ra2md.path = path;
            ra2md = new IniFile(path);
        }
        static Ra2md()
        {
            path = System.AppDomain.CurrentDomain.BaseDirectory + "ra2md.ini";
            ra2md = new IniFile(path);
        }
    }
}
