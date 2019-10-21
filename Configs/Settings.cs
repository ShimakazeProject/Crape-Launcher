using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLauncher.Configs
{
    public static class Settings
    {
        public static string IniFileName { get; set; }
        public static string FrameWindowConf { get; set; }
        public static string MainPageConf { get; set; }
        public static string MissionPageConf { get; set; }
        public static class Resource
        {
            public static string Images { get; set; }
            public static string Renderers { get; set; }
            public static string Configs { get; set; }
        }
    }
}
