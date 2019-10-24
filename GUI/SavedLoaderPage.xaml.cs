using System;
using System.Collections.Generic;
using System.IO;
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
    /// SavedLoaderPage.xaml 的交互逻辑
    /// </summary>
    public partial class SavedLoaderPage : Page
    {
        struct SavedInfomation
        {
            public string Name { get; set; }
            public FileInfo FileInfo { get; set; }

            public SavedInfomation(string filePath) : this(new FileInfo(filePath))
            {
            }
            public SavedInfomation(FileInfo fileInfo)
            {
                FileInfo = fileInfo;
                Name = Encoding.Unicode.GetString(File.ReadAllBytes(FileInfo.FullName)
                                                      .Skip(0x9c0)
                                                      .Take(0x40)
                                                      .ToArray())
                                       .Replace(Encoding.Unicode.GetString(new byte[] { 0x00, 0x00 }), null);
            }
        }
        public SavedLoaderPage()
        {
            InitializeComponent();
            DirectoryInfo folder = new DirectoryInfo(@"\Saved Games\");
            try
            {
                foreach (FileInfo file in folder.GetFiles("*.sav"))
                {
                    new SavedInfomation(file);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                new SavedInfomation
                {
                    Name = "没有发现可用存档",
                    FileInfo = null
                };
            }
        }
    }
}
