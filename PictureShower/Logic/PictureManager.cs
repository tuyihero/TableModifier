using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace PictureShower
{
    class PictureManager
    {
        #region 静态

        private static PictureManager _Instance = null;
        public static PictureManager Instance
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new PictureManager();
                return _Instance;
            }
        }

        private PictureManager()
        {
 
        }

        #endregion

        #region 

        public string PictureRootPath { get; set; }

        public List<string> PictureDirectories = new List<string>();
        public void FindDirectFromPath(string basePath)
        {
            if (string.IsNullOrEmpty(basePath))
                return;

            PictureRootPath = basePath;
            PictureDirectories.Clear();

            //如果根目录直接包含图片文件，也列出来
            if (IsPictureInDir(PictureRootPath))
            {
                PictureDirectories.Add(PictureRootPath);
            }

            //列出包含图片的目录
            foreach (string dirPath in Directory.EnumerateDirectories(PictureRootPath, "*", SearchOption.AllDirectories))
            {
                if (IsPictureInDir(dirPath))
                {
                    PictureDirectories.Add(dirPath);
                }
            }
        }

        public string PictureShowPath { get; set; }
        public List<string> PictureShowNames = new List<string>();
        public void FindPictureFromDir(string dir)
        {
            if (string.IsNullOrEmpty(dir))
                return;

            PictureShowPath = dir;
            PictureShowNames.Clear();
            foreach (string fileName in Directory.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly))
            {
                if (IsFilePicture(fileName))
                {
                    PictureShowNames.Add(fileName);
                }
            }
        }

        public bool IsPictureInDir(string dirPath)
        {
            foreach (string fileName in Directory.EnumerateFiles(dirPath, "*", SearchOption.TopDirectoryOnly))
            {
                if (IsFilePicture(fileName))
                    return true;
            }

            return false;
        }

        //通过后缀名判定文件是否图片
        public bool IsFilePicture(string fileName)
        {
            if (fileName.Contains(".jpg"))
            {
                return true;
            }
            else if (fileName.Contains(".png"))
            {
                return true;
            }
            else if (fileName.Contains(".bmp"))
            {
                return true;
            }
            else if (fileName.Contains(".gif"))
            {
                return true;
            }
            else if (fileName.Contains(".tga"))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
