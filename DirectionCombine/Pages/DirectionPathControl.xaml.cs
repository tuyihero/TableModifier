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
        #region 

        private static DirectionPathControl _Instance;
        public static DirectionPathControl Instance
        {
            get
            {
                return _Instance;
            }
        }

        #endregion
        public DirectionPathControl()
        {
            InitializeComponent();
            _Instance = this;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string path = PathControl.GetPath();
            if (!System.IO.Directory.Exists(path))
                return;

            DirectoryInfo directInfo = new DirectoryInfo(path);
            var files = directInfo.GetFiles("*.unity3d", SearchOption.AllDirectories);
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

        private void Button_Flip(object sender, RoutedEventArgs e)
        {
            string path = FlipPathControl.GetPath();
            if (!System.IO.Directory.Exists(path))
                return;

            ImgOperate.Instance.FlipAllImages(path);
        }

        private void Button_Alpha(object sender, RoutedEventArgs e)
        {
            string path = AlphaPathControl.GetPath();
            if (!System.IO.Directory.Exists(path))
                return;

            ImgOperate.Instance.CombineImageAlphas(path);
        }

        private void Button_ChangeName(object sender, RoutedEventArgs e)
        {
            string path = ChangeName.GetPath();
            //if (!System.IO.Directory.Exists(path))
            //    return;

            //CppToCSharp.Instance.ChangeTabFold(path);
            ImgOperate.Instance.ChangeAllFileToShader(path);
            //ImgOperate.Instance.ClassifyFile(path);
        }

        private void Button_SplitImg(object sender, RoutedEventArgs e)
        {
            string path = SplitImg.GetPath();
            //if (!System.IO.Directory.Exists(path))
            //    return;

            //CppToCSharp.Instance.ChangeTabFold(path);
            ImgOperate.Instance.SplitFold(path);
            //ImgOperate.Instance.ClassifyFile(path);
        }

        public void ShowDebugMsg(string debugMsg)
        {
            DebugMsg_TextBlock.Text = debugMsg;
        }

        public void ShowErrorMsg(string errorMsg)
        {
            ErrorMsg_TextBlock.Text = errorMsg;
        }
    }
}
