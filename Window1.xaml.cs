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
using System.Windows.Shapes;

namespace CCLibrary
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            
        }


        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void _close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void _small_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void _back_Click(object sender, RoutedEventArgs e)
        {
            _frame.GoBack();
            if (_frame.CanGoBack) _back.IsEnabled = true;
            else _back.IsEnabled = false;
        }

        private void _title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void _title_MouseDown(object sender, MouseButtonEventArgs e)
        {

            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        }

        private void _frame_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_frame.CanGoBack) _back.IsEnabled = true;
            else _back.IsEnabled = false;
        }


        public object FrameContent
        {
            get { return (object)GetValue(FrameContentProperty); }
            set { SetValue(FrameContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FrameContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FrameContentProperty =
            DependencyProperty.Register("FrameContent", typeof(object), typeof(Window1), new PropertyMetadata(new object()));



        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("背景色", typeof(Brush), typeof(Window1), new PropertyMetadata(Brushes.Transparent));
        public new Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }
        public new static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("边框色", typeof(Brush), typeof(Window1), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0, 0x78, 0xD7))));


    }
}
