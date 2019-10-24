using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Strings = CLauncher.Configs.Strings;
using Global = CLauncher.Config.Conf;
using System.IO;
using System.Json;
using CLauncher.LogMgr;

namespace CLauncher.GUI
{
    /// <summary>
    /// LauncherPage.xaml 的交互逻辑
    /// </summary>
    public partial class LauncherPage : Page
    {
        private Spawn spawnIni;
        /// <summary>
        /// Json2Struct格式化
        /// </summary>
        struct MissionJson
        {
            public string Name { get; private set; }
            public string Summary { get; private set; }
            public JsonValue Spawn { get; private set; }
            public MissionJson(JsonObject json)
            {
                JsonValue name, summary, spawn;
                json.TryGetValue("mission", out name);
                json.TryGetValue("summary", out summary);
                json.TryGetValue("spawn", out spawn);
                if (name.JsonType == JsonType.String) Name = name;
                else throw new FormatException();
                if (summary.JsonType == JsonType.String) Summary = summary;
                else throw new FormatException();                
                if (spawn.JsonType == JsonType.Object) Spawn = spawn;
                else throw new FormatException();
            }
        }
        /// <summary>
        /// Spawn Ini
        /// </summary>
        class Spawn
        {
            public string Scenario { get; private set; }
            public int GameSpeed { get; private set; }
            public bool Firestrom { get; private set; }
            public string CustomLoadScreen { get; private set; }
            public bool IsSinglePlayer { get; private set; }
            public bool SidebarHack { get; private set; }
            public int Side { get; private set; }
            public bool BuildOffAlly { get; private set; }
            public int DifficultyModeHuman { get; private set; }
            public int DifficultyModeComputer { get; private set; }
            public string Others { get; private set; }
            public Spawn()
            {
                Scenario = "";
                Side = 0;
                GameSpeed = 4;
                Firestrom = false;
                CustomLoadScreen = @"Resources\ls800a01.pcx";
                IsSinglePlayer = true;
                BuildOffAlly = true;
                DifficultyModeHuman = 0;
                DifficultyModeComputer = 2;
                Others = "";
            }
            public Spawn(JsonObject json) : this()
            {
                JsonValue scenario, side, gamespeed, firestrom, customloadscreen, issigleplayer, buildoffally, humanmode, computermode, others;
                if (json.TryGetValue("Scenario", out scenario)) Scenario = scenario;
                if (json.TryGetValue("Side", out side)) Side = side;
                if (json.TryGetValue("GameSpeed", out gamespeed)) GameSpeed = gamespeed;
                if (json.TryGetValue("Firestrom", out firestrom)) Firestrom = firestrom;
                if (json.TryGetValue("CustomLoadScreen", out customloadscreen)) CustomLoadScreen = customloadscreen;
                if (json.TryGetValue("IsSiglePlayer", out issigleplayer)) IsSinglePlayer = issigleplayer;
                if (json.TryGetValue("BuildOffAlly", out buildoffally)) BuildOffAlly = buildoffally;
                if (json.TryGetValue("DifficultyModeHuman", out humanmode)) DifficultyModeHuman = humanmode;
                if (json.TryGetValue("DifficultyModeComputer", out computermode)) DifficultyModeComputer = computermode;
                if (json.TryGetValue("Others", out others)) Others = others;
            }
            public override string ToString()
            {
                string ini = "[Settings]\r\n";
                ini += "Scenario" + ToString(Scenario);
                ini += "Side" + ToString(Side);
                ini += "GameSpeed" + ToString(GameSpeed);
                ini += "Firestrom" + ToString(Firestrom);
                ini += "CustomLoadScreen" + ToString(CustomLoadScreen);
                ini += "IsSiglePlayer" + ToString(IsSinglePlayer);
                ini += "BuildOffAlly" + ToString(BuildOffAlly);
                ini += "DifficultyModeHuman " + ToString(DifficultyModeHuman);
                ini += "DifficultyModeComputer" + ToString(DifficultyModeComputer);
                ini += Others;
                ini += "\r\n\r\n";
                return ini;
            }
            private string ToString(string str)
            {
                return "=" + str + "\r\n";
            }
            private string ToString(bool b)
            {
                if (b) return "=" + "1\r\n";
                else return "=" + "0\r\n";
            }
            private string ToString(int i)
            {
                string ret = "=";
                ret += i.ToString();
                ret += "\r\n";
                return ret;
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public LauncherPage()
        {
            InitializeComponent();
            dgtc1.Header = Strings.Mission;
            tbSummary.Text = Strings.Summary;
            bRun.Content = Strings.Run;
            tbEasy.Text = Strings.Easy;
            tbNormal.Text = Strings.Normal;
            tbHard.Text = Strings.Hard;

            if (!Initializing()) throw new Exception("初始化异常");
        }
        private bool Initializing()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Resource\Missions.json"))
                return false;
            JsonObject missionJson = (JsonObject)JsonValue.Parse(File.ReadAllText(@"Resource\Missions.json"));
            JsonValue tmp;
            missionJson.TryGetValue("missions", out tmp);
            JsonArray missions = (JsonArray)tmp;
            List <JsonValue> missionList = missions.ToList();
            foreach (JsonObject mission in missionList)
            {
                if (mission.JsonType != JsonType.Object) throw new FormatException();
                dgMissionList.Items.Add(new MissionJson(mission));
            }
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Global.IniW();
            if (true)// Program.Program.MD5()
            {
                if (dgMissionList.SelectedItem==null)
                {
                    return;
                }
                FileInfo file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "spawn.ini");
                file.Delete();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "spawn.ini", spawnIni.ToString());
                Run();
            }//*/
        }

        private void SEasyHard_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int Value = (int)sEasyHard.Value;
            Global.Difficult = Value;
            sEasyHard.Value = Value;
        }
        private void Run()
        {
            string gamemd = "syringe.exe \"gamemd.exe\" ";
            string command = GetCmd();
            try
            {
                if (Global.IsWindow) //设置16色
                    Screen.ChangeRes();

                System.Diagnostics.Process proc = System.Diagnostics.Process.Start(
                        AppDomain.CurrentDomain.BaseDirectory + gamemd + command);
                if (Global.IsWindow)// 还原
                    Screen.DisChangeRes();//还原色深
                if (proc != null)
                {

                }
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return;
            }
        }
        private string GetCmd()
        {
            string ret = "-SPAWN -CD ";
            if (Global.SpeedCtrl) ret += "-SPEEDCONTROL ";
            if (Global.UseLog) ret += "-LOG ";
            return ret;
        }

        private void DgMissionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ml = (MissionJson)dgMissionList.SelectedItem;
            tbSummarys.Text = ml.Summary;
            spawnIni = new Spawn((JsonObject)ml.Spawn);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Width>=500)
            {
                _mission.Height = Height - 20;
            }
        }                    
    }
}
