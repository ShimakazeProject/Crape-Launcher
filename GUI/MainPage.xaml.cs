using CLauncher.Configs;
using System;
using System.Collections.Generic;
using System.Json;
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
            init();
            
        }
        private void init()
        {
            JsonObject json = (JsonObject)App.LoadJsonFromFile(Settings.MainPageConf);
            {
                JsonObject js =(JsonObject)App.GetJsonValue(json, "Right");
                _right.Width = new GridLength(App.GetJsonValue(js, "Width"), GridUnitType.Star);
                _right.MaxWidth = App.GetJsonValue(js, "MaxWidth");
                {
                    js = (JsonObject)App.GetJsonValue(js, "Buttons");
                    JsonObject button;
                    {
                        button = (JsonObject)App.GetJsonValue(js, "Mission");
                        btnMission.Content = (string)App.GetJsonValue(button, "Content");
                        {
                            var temp = ((JsonArray)App.GetJsonValue(button, "Margin")).ToList();
                            if (temp.Count == 4)
                                btnMission.Margin = new Thickness(temp[0], temp[1], temp[2], temp[3]);
                            else if (temp.Count == 1)
                                btnMission.Margin = new Thickness(temp[0]);
                        }
                        if (App.GetJsonValue(button, "Visibility"))
                            btnMission.Visibility = Visibility.Visible;
                        else btnMission.Visibility = Visibility.Collapsed;
                    }
                    {
                        button = (JsonObject)App.GetJsonValue(js, "Settings");
                        btnSetting.Content = (string)App.GetJsonValue(button, "Content");
                        {
                            var temp = ((JsonArray)App.GetJsonValue(button, "Margin")).ToList();
                            if (temp.Count == 4)
                                btnSetting.Margin = new Thickness(temp[0], temp[1], temp[2], temp[3]);
                            else if (temp.Count == 1)
                                btnSetting.Margin = new Thickness(temp[0]);
                        }
                        if (App.GetJsonValue(button, "Visibility"))
                            btnSetting.Visibility = Visibility.Visible;
                        else btnSetting.Visibility = Visibility.Collapsed;
                    }
                    {
                        button = (JsonObject)App.GetJsonValue(js, "Exit");
                        btnExit.Content = (string)App.GetJsonValue(button, "Content");
                        {
                            var temp = ((JsonArray)App.GetJsonValue(button, "Margin")).ToList();
                            if (temp.Count == 4)
                                btnExit.Margin = new Thickness(temp[0], temp[1], temp[2], temp[3]);
                            else if (temp.Count == 1)
                                btnExit.Margin = new Thickness(temp[0]);
                        }
                        if (App.GetJsonValue(button, "Visibility"))
                            btnExit.Visibility = Visibility.Visible;
                        else btnExit.Visibility = Visibility.Collapsed;
                    }
                }
            }
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
