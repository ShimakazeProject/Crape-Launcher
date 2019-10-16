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

using Str = CLauncher.Configs.Strings;
using Clr = CLauncher.Config.Colors;

using Global = CLauncher.Config.Conf;
using System.IO;

namespace CLauncher
{
    /// <summary>
    /// UC2.xaml 的交互逻辑
    /// </summary>
    public partial class UC2 : UserControl
    {
        public UC2()
        {
            InitializeComponent();
            #region SSet
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "800x600",
                DataContext = new c_ScreenWH(800, 600)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1024x768",
                DataContext = new c_ScreenWH(1024, 768)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1152x864",
                DataContext = new c_ScreenWH(1152, 864)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1280x720",
                DataContext = new c_ScreenWH(1280, 720)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1280x768",
                DataContext = new c_ScreenWH(1280, 768)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1280x800",
                DataContext = new c_ScreenWH(1280, 800)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1280x960",
                DataContext = new c_ScreenWH(1280, 960)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1280x1024",
                DataContext = new c_ScreenWH(1280, 1024)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1360x768",
                DataContext = new c_ScreenWH(1360, 768)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1360x1024",
                DataContext = new c_ScreenWH(1360, 1024)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1440x900",
                DataContext = new c_ScreenWH(1440, 900)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1680x1050",
                DataContext = new c_ScreenWH(1680, 1050)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1776x1000",
                DataContext = new c_ScreenWH(1776, 1000)
            });
            ScreenSet.Items.Add(new ComboBoxItem()
            {
                Content = "1920x1080",
                DataContext = new c_ScreenWH(1920, 1080)
            });
            #endregion
            tbBGM.Text = Str.BGM;
            gbAudioSet.Header = Str.AudioSettings;
            gbVideoSet.Header = Str.VideoSettings;
            tbScreen.Text = Str.Screen;
            tbSEM.Text = Str.SEM;
            tbVOX.Text = Str.VOX;
            cbGraphicsAPI.Content = Str.UseGraphicsAPI;
            cbNoFrame.Content = Str.NoFrame;
            cbWindow.Content = Str.IsWindow;
            cbGraphicsAPI.Foreground = Clr.Foreground;
            cbNoFrame.Foreground = Clr.Foreground;
            cbWindow.Foreground = Clr.Foreground;
            
            ScreenSet.Text = Global.Width.ToString() + "x" + Global.Height.ToString();
            BGM.Value = Global.BGM;
            cbNoFrame.IsChecked = Global.NoFrame;
            cbWindow.IsChecked = Global.IsWindow;
            VOX.Value = Global.VOX;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ts-ddraw.dll")) cbGraphicsAPI.IsChecked = true; // 检查是否使用ddraw
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ddraw.dll"))  // 检查是否使用ddraw
            {
                cbGraphicsAPI.IsChecked = true;
                cbGraphicsAPI.Content = "已启用ddraw";
            }
            SEM.Value = Global.SEM;
            //*/
        }
        private void WindowSet(object sender, RoutedEventArgs e)
        {
            
            if (cbWindow.IsChecked != null)
                if (cbWindow.IsChecked == true) Global.IsWindow = true;
                else cbNoFrame.IsChecked = Global.IsWindow = false;
            cbNoFrame.IsEnabled = Global.IsWindow;
            //*/
        }
        private void NoWindowFrameSet(object sender, RoutedEventArgs e)//无边框窗口化设置
        {
            
            if (cbNoFrame.IsChecked != null)
                if (cbNoFrame.IsChecked == true) Global.NoFrame = true;
                else Global.NoFrame = false;
            //*/
        }
        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // 音量设置
        {

            BGM.Value = Convert.ToDouble(BGM.Value.ToString("#0.0"));
            VOX.Value = Convert.ToDouble(VOX.Value.ToString("#0.0"));
            SEM.Value = Convert.ToDouble(SEM.Value.ToString("#0.0"));
            
            Global.BGM = BGM.Value;
            Global.VOX = VOX.Value;
            Global.SEM = SEM.Value;
            //*/
        }

        private void DDrawSet(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (cbGraphicsAPI.IsChecked == true)
                {
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Resource\\ts-ddraw.dll",
                        AppDomain.CurrentDomain.BaseDirectory + @"ts-ddraw.dll", true);
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Resource\\ddraw.ini",
                        AppDomain.CurrentDomain.BaseDirectory + @"ddraw.ini", true);
                }
                else
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ddraw.ini");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ts-ddraw.dll");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ddraw.dll");
                    cbGraphicsAPI.Content = "启用ts-ddraw";
                }
            }
            catch (DirectoryNotFoundException)
            {
                cbGraphicsAPI.IsEnabled = false;
                cbGraphicsAPI.Content = "无法启用ts-ddraw";
                return;
            }
            //*/
        }
        private void ScreenSet_SelectionChanged(object sender, SelectionChangedEventArgs e) //调整分辨率
        {
            if (ScreenSet.SelectedItem is ComboBoxItem cbi)
            {
                if (cbi.DataContext is c_ScreenWH wh)
                {
                    Global.Width = (int)wh.Width;
                    Global.Height = (int)wh.Height;
                }
            }
        }

        private struct c_ScreenWH
        {
            public uint Width { set; get; }
            public uint Height { set; get; }
            public c_ScreenWH(uint Width, uint Height)
            {
                this.Width = Width;
                this.Height = Height;
            }
        }
    }
}
