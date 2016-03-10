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
using System.IO;

namespace DirectionCombine
{
    /// <summary>
    /// DirectionPathControl.xaml 的交互逻辑
    /// </summary>
    public partial class DirectionPathControl : UserControl
    {
        public DirectionPathControl()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string path = PathControl.GetPath();
            if(!System.IO.Directory.Exists(path))
                return;

            DirectoryInfo directInfo = new DirectoryInfo(path);
            var files = directInfo.GetFiles("*.*", SearchOption.AllDirectories);
            string destDirect = System.IO.Path.Combine(path, "CombineFiles");
            int idx = 0;
            foreach (FileInfo filePath in files)
            {
                string combineDirect = System.IO.Path.Combine(destDirect, filePath.Extension.Replace('.', '_'));
                System.IO.Directory.CreateDirectory(combineDirect);
                //string combineFile = combineDirect + "\\" + filePath.FullName.Replace('\\', '_').Replace(':','_');
                //if (combineFile.Length < 260)
                //{
                //    filePath.CopyTo(combineFile);
                //}
                //else
                {
                    int dotIdx = filePath.Name.IndexOf('.');
                    if (dotIdx > 0)
                    {
                        string fileName = filePath.Name.Remove(filePath.Name.IndexOf('.')) + (idx++) + filePath.Extension;
                        string combineName = combineDirect + "\\" + fileName;
                        filePath.CopyTo(combineName);
                    }
                }
            }
        }
    }
}
