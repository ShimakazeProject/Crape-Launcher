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

using Str = CLauncher.Config.Strings;
using Clr = CLauncher.Config.Colors;

using Global = CLauncher.Config.Conf;
using System.IO;

namespace CLauncher
{
    /// <summary>
    /// UC1.xaml 的交互逻辑
    /// </summary>
    public partial class UC1 : UserControl
    {
        private string spawnIni;
        public UC1()
        {
            InitializeComponent();
            tbSummary.Text = Str.Summary;
            tbEasy.Text = Str.Easy;
            tbNormal.Text = Str.Normal;
            tbHard.Text = Str.Hard;
            bRun.Content = Str.Run;
            bRun.Foreground = Clr.Foreground;
            dgMissionList.Columns.Add(new DataGridTextColumn()
            {
                Header = Str.Mission,
                Binding = new Binding("Name"),
                Width = 340
            });
            foreach (MissionList missionList in Global.Missions)
            {
                dgMissionList.Items.Add(missionList);
            }
            dgMissionList.SelectedIndex = 0;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Global.IniW();
            if (true)// Program.Program.MD5()
            {
                FileInfo file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "spawn.ini");
                file.Delete();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "spawn.ini", spawnIni);
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
            MissionList ml=(MissionList)dgMissionList.SelectedItem;
            tbSummarys.Text = Global.FormatStr(ml.Summary);
            spawnIni = Global.FormatStr(ml.Spawn);
        }
    }
}
