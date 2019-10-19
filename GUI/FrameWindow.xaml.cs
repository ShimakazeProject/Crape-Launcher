using CLauncher.Configs;
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CLauncher.GUI
{

    /// <summary>
    /// FrameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FrameWindow : Window
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        public FrameWindow()
        {
            InitializeComponent();
            init();
            _frame.Content = new MainPage();
            /* 窗口毛玻璃 */
            {
                Loaded += _window_Loaded;
                MouseDown += _window_MouseDown;
            }
        }
        private void init()
        {
            JsonObject json = (JsonObject)App.LoadJsonFromFile("Resource\\FrameWindowConfig.json");
            Title = App.GetJsonValue(json, "Title");
            Foreground = App.String2Brush(App.GetJsonValue(json, "Foreground"));
            Background = App.String2Brush(App.GetJsonValue(json, "Background"));
            Height = App.GetJsonValue(json, "Height");
            MinHeight = App.GetJsonValue(json, "MinHeight");
            Width = App.GetJsonValue(json, "Width");
            MinWidth = App.GetJsonValue(json, "MinWidth");
            _border.BorderBrush = App.String2Brush(App.GetJsonValue(json, "BorderBrush"));
            _border.BorderThickness = new Thickness(App.GetJsonValue(json, "BorderThickness"));
            _border.CornerRadius = new CornerRadius(App.GetJsonValue(json, "CornerRadius"));
            _workarea.Margin = new Thickness(App.GetJsonValue(json, "WorkAreaMargin"));
            Icon = BitmapFrame.Create(new Uri(Settings.Resource.Images + App.GetJsonValue(json, "Icon"), UriKind.RelativeOrAbsolute));
            json = (JsonObject)App.GetJsonValue(json, "TitleBar");
            _titlebar.Height = new GridLength(App.GetJsonValue(json, "Height"));
            JsonObject subjson = (JsonObject)App.GetJsonValue(json, "TitleText");
            _title.FontSize = App.GetJsonValue(subjson, "FontSize");
            _title.Visibility = App.GetJsonValue(subjson, "Visibility") ? Visibility.Visible : Visibility.Collapsed;
            subjson = (JsonObject)App.GetJsonValue(json, "GoBackBtn");
            _back.Content = (string)App.GetJsonValue(subjson, "Content");
            _back.Width = App.GetJsonValue(subjson, "Width");
            _back.Visibility = App.GetJsonValue(subjson, "Visibility") ? Visibility.Visible : Visibility.Collapsed;
            subjson = (JsonObject)App.GetJsonValue(json, "GoForwardBtn");
            _forward.Content = (string)App.GetJsonValue(subjson, "Content");
            _forward.Width = App.GetJsonValue(subjson, "Width");
            _forward.Visibility = App.GetJsonValue(subjson, "Visibility") ? Visibility.Visible : Visibility.Collapsed;
            subjson = (JsonObject)App.GetJsonValue(json, "ToMinimizedBtn");
            _minimized.Content = (string)App.GetJsonValue(subjson, "Content");
            _minimized.Width = App.GetJsonValue(subjson, "Width");
            _minimized.Visibility = App.GetJsonValue(subjson, "Visibility") ? Visibility.Visible : Visibility.Collapsed;
            subjson = (JsonObject)App.GetJsonValue(json, "ToMaximizedBtn");
            _maximized.Content = (string)App.GetJsonValue(subjson, "Content");
            _maximized.Width = App.GetJsonValue(subjson, "Width");
            _maximized.Visibility = App.GetJsonValue(subjson, "Visibility") ? Visibility.Visible : Visibility.Collapsed;
            subjson = (JsonObject)App.GetJsonValue(json, "CloseBtn");
            _close.Content = (string)App.GetJsonValue(subjson, "Content");
            _close.Width = App.GetJsonValue(subjson, "Width");
            _close.Visibility = App.GetJsonValue(subjson, "Visibility") ? Visibility.Visible : Visibility.Collapsed;
        }
        #region 窗口毛玻璃
        private void _window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
        private void _window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion
        private void _close_Click(object sender, RoutedEventArgs e) => Close();
        private void _minimized_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void _maximized_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Maximized;
        private void _back_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
            if (_frame.CanGoBack) _back.IsEnabled = true;
            else _back.IsEnabled = false;
        }
        private void _forward_Click(object sender, RoutedEventArgs e)
        {
            GoForward();
            if (_frame.CanGoForward) _forward.IsEnabled = true;
            else _forward.IsEnabled = false;
        }
        private void _title_MouseDown(object sender, MouseButtonEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        private void _title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
        private void _frame_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_frame.CanGoBack) _back.IsEnabled = true;
            else _back.IsEnabled = false;
        }
        public void GoBack() => _frame.GoBack();
        public void GoForward() => _frame.GoForward();

    }
}
