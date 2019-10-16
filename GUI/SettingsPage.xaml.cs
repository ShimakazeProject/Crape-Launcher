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


namespace CLauncher.GUI
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
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
