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

namespace CLauncher.GUI
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        struct RendererJson
        {
            public string Name { get; private set; }
            public string Dll { get; private set; }
            public List<string> Files { get; private set; }
            public RendererJson(JsonObject json)
            {
                Files = new List<string>();
                JsonValue name, dll, files;
                json.TryGetValue("Name", out name);
                json.TryGetValue("Dll", out dll);
                json.TryGetValue("Files", out files);
                if (name.JsonType == JsonType.String) Name = name;
                else throw new FormatException();
                if (dll.JsonType == JsonType.String) Dll = dll;
                else throw new FormatException();
                if (files != null)
                    if (files.JsonType == JsonType.Array)
                        foreach (JsonValue file in (JsonArray)files)
                            if (file.JsonType == JsonType.String) Files.Add(file);
                            else throw new FormatException();
                    else throw new FormatException();
            }
        }
        public SettingsPage()
        {
            InitializeComponent();
            StringInitializing();
            Initializing();
        }

        private void StringInitializing()
        {
            gbVideoSet.Header = Strings.VideoSettings;
            gbAudioSet.Header = Strings.AudioSettings;

            tbScreen.Text = Strings.Screen;
            cbWindow.Content = Strings.IsWindow;
            cbNoFrame.Content = Strings.NoFrame;
            tbUseDDraw.Text = Strings.GraphicsAPI;
            cbGraphicsAPI.Content = Strings.UseGraphicsAPI;

            tbBGM.Text = Strings.BGM;
            tbVOX.Text = Strings.VOX;
            tbSEM.Text = Strings.SEM;
        }
        private bool Initializing()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Resource\GraphicsAPIs.json"))
                return false;
            JsonArray renderers = (JsonArray)JsonValue.Parse(File.ReadAllText(@"Resource\GraphicsAPIs.json"));
            List<JsonValue> rendererList = renderers.ToList();
            foreach (JsonObject renderer in rendererList)
            {
                if (renderer.JsonType != JsonType.Object) throw new FormatException();
                cbxGraphics.Items.Add(new RendererJson(renderer));
            }
            return true;
        }
        private void WindowSet(object sender, RoutedEventArgs e)
        {
            if (cbWindow.IsChecked != null)
                if (cbWindow.IsChecked == true) Global.IsWindow = true;
                else cbNoFrame.IsChecked = Global.IsWindow = false;
            cbNoFrame.IsEnabled = Global.IsWindow;
        }
        private void NoWindowFrameSet(object sender, RoutedEventArgs e)//无边框窗口化设置
        {
            if (cbNoFrame.IsChecked != null)
                if (cbNoFrame.IsChecked == true) Global.NoFrame = true;
                else Global.NoFrame = false;
        }
        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // 音量设置
        {
            BGM.Value = Convert.ToDouble(BGM.Value.ToString("#0.0"));
            VOX.Value = Convert.ToDouble(VOX.Value.ToString("#0.0"));
            SEM.Value = Convert.ToDouble(SEM.Value.ToString("#0.0"));

            Global.BGM = BGM.Value;
            Global.VOX = VOX.Value;
            Global.SEM = SEM.Value;            
        }

        private void DDrawSet(object sender, RoutedEventArgs e)
        {
            if (cbGraphicsAPI.IsChecked == true)
            {
                cbxGraphics.IsEnabled = true;
                tbUseDDraw.IsEnabled = true;
                /*
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Resource\\ts-ddraw.dll",
                    AppDomain.CurrentDomain.BaseDirectory + @"ts-ddraw.dll", true);
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Resource\\ddraw.ini",
                    AppDomain.CurrentDomain.BaseDirectory + @"ddraw.ini", true);
                //*/
            }
            else
            {
                cbxGraphics.IsEnabled = false;
                tbUseDDraw.IsEnabled = false;
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ddraw.ini");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"ddraw.dll");
            }
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

        private void cbxGraphics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RendererJson renderer = (RendererJson)((ComboBox)sender).SelectedItem;
            string local = AppDomain.CurrentDomain.BaseDirectory, resdir = local + @"Resource\";
            File.Copy(resdir + renderer.Dll, local + @"ddraw.dll", true);

            System.Diagnostics.Debug.WriteLine(renderer.Files.Count.ToString());
            if (renderer.Files.Count > 0)
            {
                foreach (var file in renderer.Files)
                {
                    System.Diagnostics.Debug.WriteLine(file);
                    File.Copy(resdir + file, local + file, true);
                }
            }
        }
    }
}
