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

namespace CLauncher.GUI
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            btnMission.Content = Configs.Strings.Mission;
            btnSetting.Content = Configs.Strings.Settings;
            btnExit.Content = Configs.Strings.Exit;
        }

        private void Battle_Click(object sender, RoutedEventArgs e)
        {
            App.GetFrame(this).Content = new LauncherPage();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ((Window)((Border)((Grid)App.GetFrame(this).Parent).Parent).Parent).Close();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            App.GetFrame(this).Content = new SettingsPage();
        }
    }
}
