using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CLauncher
{
    public static class Tools
    {
        public static Brush String2Brush(string color)
        {
            System.Drawing.Color clr = System.Drawing.ColorTranslator.FromHtml(color);
            return
                new SolidColorBrush(
                    Color.FromArgb(
                        clr.A,
                        clr.R,
                        clr.G,
                        clr.B
                        )
                    );
        }
        public static Color String2Color(string color)
        {
            System.Drawing.Color clr = System.Drawing.ColorTranslator.FromHtml(color);
            return Color.FromArgb(
                        clr.A,
                        clr.R,
                        clr.G,
                        clr.B
                    );
        }
    }
    public class IniW
    {
        /// <summary>
        /// ini文件路径
        /// </summary>
        public string inipath;
        //声明API函数
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        /// <summary> 
        /// 构造方法 
        /// </summary> 
        /// <param name="INIPath">文件路径</param> 
        public IniW(string INIPath)
        {
            inipath = INIPath;
        }
        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.inipath);
        }


    }
    public class MissionList
    {
        public string Name { get; private set; }
        public string Summary { get; private set; }
        public string Spawn { get; private set; }
        public MissionList() { }
        public MissionList(string Name, string Summary,string Spawn)
        {
            this.Name = Name;
            this.Summary = Summary;
            this.Spawn = Spawn;
        }
    }
}
