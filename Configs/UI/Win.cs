using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CLauncher.Configs.UI
{
    public static class Win
    {
        public static Brush Foreground { get; set; }
        public static Brush Background { get; set; }
        public static Brush BorderColor { get; set; }
        public static double Height { get; set; }
        public static double MinHeight { get; set; }
        public static double Width { get; set; }
        public static double MinWidth { get; set; }
        public static ImageSource Icon { get; set; }
        public static double ResizeBorderThickness { get; set; }
        public static double BorderThickness { get; set; }
        public static double CornerRadius { get; set; }
        public static double ControlBorder { get; set; }
    }
}
