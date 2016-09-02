using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DirectionCombine
{
    public class ImgOperate
    {
        #region 单例

        private static ImgOperate _Instance = null;
        public static ImgOperate Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ImgOperate();
                return _Instance;
            }
        }

        private ImgOperate()
        {

        }

        #endregion

        #region 

        public string DebugMsg;
        public string ErrorMsg;

        public void PushDebugMsg(string msg)
        {
            DebugMsg = msg;
            DirectionPathControl.Instance.ShowDebugMsg(DebugMsg);
        }

        public void PushErrorMsg(string errorMsg)
        {
            ErrorMsg += "\n" + errorMsg;
            DirectionPathControl.Instance.ShowErrorMsg(errorMsg);
        }


        #endregion

        #region FlipImgs

        public void FlipAllImages(string path)
        {
            string outPutPath = path/* + "/FlipOut"*/;
            Directory.CreateDirectory(outPutPath);
            foreach (string fileName in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            {
                if (IsFilePicture(fileName))
                {
                    try
                    {
                        FlipImage(fileName, fileName.Replace(path, outPutPath));
                        PushDebugMsg("FlipFile:" + fileName);
                        
                    }
                    catch (Exception e)
                    {
                        PushErrorMsg("FlipFile Fail:" + fileName + " E:" + e.ToString());
                    }
                }
            }
        }

        public void FlipImage(string name, string outPath)
        {
            Bitmap mybm = new Bitmap(name);

            Bitmap bm = new Bitmap(mybm.Width, mybm.Height);
            int x, y, z;
            Color pixel;

            for (x = 0; x < mybm.Width; x++)
            {
                for (y = mybm.Height - 1, z = 0; y >= 0; y--)
                {
                    pixel = mybm.GetPixel(x, y);//获取当前像素的值
                    bm.SetPixel(x, z++, Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B));//绘图
                }
            }

            mybm.Dispose();

            bm.Save(outPath);
            bm.Dispose();
        }
        #endregion

        #region CombineAlpha
        public void CombineImageAlphas(string path)
        {
            string outPutPath = path + "/FlipOut";
            Directory.CreateDirectory(outPutPath);
            foreach (string fileName in Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly))
            {
                if (IsFilePicture(fileName))
                {
                    try
                    {
                        if (!IsFileAlpha(fileName))
                        {
                            string alphaFile = GetAlphaFile(fileName, path);
                            if (!string.IsNullOrEmpty(alphaFile))
                            {
                                CombineAlpha(fileName, alphaFile, fileName.Replace(path, outPutPath));
                                PushDebugMsg("CombineAlphaFile:" + fileName);
                            }
                            else
                            {
                                File.Copy(fileName, fileName.Replace(path, outPutPath));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        PushErrorMsg("CombineAlphaFile Fail:" + fileName + " E:" + e.ToString());
                    }
                }
            }
        }

        public void CombineAlpha(string name, string alphaName, string outPath)
        {
            Bitmap mybm = new Bitmap(name);
            Bitmap alphabm = new Bitmap(alphaName);

            if (mybm.Width != alphabm.Width || mybm.Height != alphabm.Height)
            {
                File.Copy(name, outPath);
                return;
            }

            Bitmap bm = new Bitmap(mybm.Width, mybm.Height);
            int x, y;
            Color pixel;
            Color alpha;

            for (x = 0; x < mybm.Width; ++x)
            {
                for (y =0; y < mybm.Height; ++y)
                {
                    pixel = mybm.GetPixel(x, y);//获取当前像素的值
                    alpha = alphabm.GetPixel(x, y);
                    bm.SetPixel(x, y, Color.FromArgb(alpha.R, pixel.R, pixel.G, pixel.B));//绘图
                }
            }

            bm.Save(outPath);
        }

        public string GetAlphaFile(string name, string path)
        {
            if (IsFileAlpha(name))
            {
                return "";
            }

            //if (name.EndsWith("alpha"))
            //    return "";

            //if (name.EndsWith("al"))
            //    return "";

            //if (name.EndsWith("AlphaChannel"))
            //    return "";

            string fileName = Path.GetFileNameWithoutExtension(name);
            foreach (string checkFilePath in Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly))
            {
                if (checkFilePath.Contains(fileName))
                {
                    string checkFileName = Path.GetFileNameWithoutExtension(checkFilePath);
                    if (checkFileName == fileName + "_alpha"
                        || checkFileName == fileName + "_AUTO_A"
                        || checkFileName == fileName + "_A"
                        || checkFileName == fileName + "_AlphaChannel"
                        || checkFileName == fileName + "_Alpha")
                        return checkFilePath;

                    if (fileName.Contains("color") && checkFileName == fileName.Replace("color", "alpha"))
                    {
                        return checkFilePath;
                    }
                }
            }
            return "";
        }

        #endregion

        #region changeExname

        public void ChangeAllFileToShader(string path)
        {
            foreach (string filePath in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            {
                string shaderName = filePath;
                
                if(filePath.Contains(".as"))
                    shaderName = filePath.Replace(".as", ".unity3d");

                if (filePath.Contains(".jpg.bytes"))
                    shaderName = filePath.Replace(".jpg.bytes", ".jpg");
                if (filePath.Contains(".png.bytes"))
                    shaderName = filePath.Replace(".png.bytes", ".png");

                if (filePath.Contains(".txt"))
                    shaderName = filePath.Replace(".txt", ".shader");
                if (!Path.HasExtension(filePath))
                {
                    shaderName = filePath + ".png";
                }
                //string shaderName = filePath + ".assets";
                File.Move(filePath, shaderName);
            }
        }

        #endregion

        #region packetChange

        private class PacketClass
        {
            public int classID;
            public string className;
            public Dictionary<string, string> attrNames = new Dictionary<string, string>();
        }

        public void PacketChange(string packetPath)
        {
            StreamReader stream = new StreamReader(packetPath);
            List<PacketClass> classes = new List<PacketClass>();
            PacketClass curClass = new PacketClass();
            string str;
            string className;
            int sqidx;

            while (!stream.EndOfStream)
            {
                str = stream.ReadLine();
                if (str.Contains("public class"))
                {
                    str = str.Remove(0, 13);
                    sqidx = str.IndexOf(':');
                    str = str.Remove(sqidx);
                    curClass = new PacketClass();
                    curClass.classID = classes.Count;
                    curClass.className = str;

                    classes.Add(curClass);
                }
                else if (str.Contains("output.Write"))
                {
                    str = str.Remove(0, 12);
                    sqidx = str.IndexOf('(');
                    string attrType = str.Substring(0, sqidx);
                    sqidx = str.IndexOf(')');
                    int idxSpace = str.IndexOf(',');
                    string attrName = str.Substring(idxSpace + 1, sqidx - idxSpace - 1);
                    curClass.attrNames.Add(attrName.Trim(' '), attrType);
                }
            }

            string output = "";
            string outClientPath = Path.GetDirectoryName(packetPath) + "\\packets";
            if (!Directory.Exists(outClientPath))
            {
                Directory.CreateDirectory(outClientPath);
            }
            foreach (var clas in classes)
            {
                //output += "typeof(" + clas + "),\n";
                //if (clas.className.Contains("CG"))
                {
                    CreateClientActionFun(outClientPath, clas);
                }
            }
            //string fileName = Path.GetFileNameWithoutExtension(packetPath);
            //string outPath = packetPath.Replace(fileName, fileName + "_out");
            //StreamWriter fileWrite = File.CreateText(outPath);

            //fileWrite.Write(output);
        }

        private void CreateClientActionFun(string packetPath, PacketClass classInfo)
        {
            string fileName = packetPath + "\\" + classInfo.className + ".cs";

            StreamWriter fileWrite = File.CreateText(fileName);

            //********************************************
            fileWrite.Write("//Client Read\n{\n");

            fileWrite.Write("protected override void DecodePackage(NetReader reader)\n");
            fileWrite.Write("{\n");

            fileWrite.Write("var " + classInfo.className + " = new " + classInfo.className + "();\n");
            foreach (var attr in classInfo.attrNames)
            {
                string typeStr = attr.Value;
                if (typeStr == "Int32")
                    typeStr = "Int";
                fileWrite.Write(classInfo.className + "." + attr.Key + " = " + "reader.get" + attr.Value + "();\n");
            }

            fileWrite.Write("\n}\n");

            //****************************************
            fileWrite.Write("//Client write\n{\n");

            fileWrite.Write("protected override void SendParameter(NetWriter writer, ActionParam actionParam)\n");
            fileWrite.Write("{\n");

            fileWrite.Write(classInfo.className + " packet = (" + classInfo.className + ")actionParam._Packet;\n");
            foreach (var attr in classInfo.attrNames)
            {
                string typeStr = attr.Value;
                if (typeStr == "Int32")
                    typeStr = "Int";
                fileWrite.Write("writer.write" + typeStr + "(\"" + attr.Key + "\", packet." + attr.Key + ");\n");
            }

            fileWrite.Write("\n}\n");

            //****************************************
            fileWrite.Write("/server read\n{\n");

            foreach (var attr in classInfo.attrNames)
            {
                string typeStr = attr.Value;
                fileWrite.Write("private " + typeStr + " " + attr.Key + ";\n");
            }

            fileWrite.Write("public override bool GetUrlElement()\n");
            fileWrite.Write("{\n");

            foreach (var attr in classInfo.attrNames)
            {
                string typeStr = attr.Value;
                if (typeStr == "Int32")
                    typeStr = "Int";
                fileWrite.Write("httpGet.Get" + typeStr + "(\"" + attr.Key + "\", packet." + attr.Key + ", ref " + attr.Key + ");\n");
            }

            fileWrite.Write("\n}\n");

            //*******************************************
            fileWrite.Write("/server write\n{\n");

            fileWrite.Write("public override void BuildPacket()\n");
            fileWrite.Write("{\n");

            foreach (var attr in classInfo.attrNames)
            {
                string typeStr = attr.Value;
                if (typeStr == "Int32")
                    typeStr = "Int";
                string attrValue = attr.Key;
                if (typeStr == "String")
                {
                    attrValue = "\"" + attrValue + "\"";
                }
                fileWrite.Write("PushIntoStack(" + attrValue + ");\n");
            }

            fileWrite.Write("\n}\n");

            fileWrite.Close();
        }

        private void CreateServerActionFun(string path, PacketClass classInfo)
        {
            //string fileName = Path.GetFileNameWithoutExtension(packetPath);
            //string outPath = packetPath.Replace(fileName, fileName + "_out");
            //StreamWriter fileWrite = File.CreateText(outPath);

            //fileWrite.Write(output);
        }

        #endregion

        #region classify file

        public void ClassifyFile(string path)
        {
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                string name = Path.GetFileName(file);
                string spName = "";
                if (name.Contains("wq"))
                {
                    spName = "wq";
                }
                if (name.Contains("hero"))
                {
                    spName = "hero";
                }
                if (name.Contains("Frame"))
                {
                    spName = "Frame";
                }
                if (name.Contains("Head"))
                {
                    spName = "Head";
                }

                if (!string.IsNullOrEmpty(spName))
                {
                    MoveFile(file, spName);
                }
            }
        }

        public void MoveFile(string file, string directName)
        {
            string outDirect = Path.GetDirectoryName(file) + "/" + directName;
            if (!Directory.Exists(outDirect))
            {
                Directory.CreateDirectory(outDirect);
            }

            File.Move(file, outDirect + "/" + Path.GetFileName(file));
        }

        #endregion

        #region splitImg

        public class SplitInfo
        {
            public string imgName;
            public string name;
            public int x, y;
            public int width, height;
        }

        public void SplitFold(string path)
        {
            foreach (string fileName in Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly))
            {
                var splitInfos = GetSplitInfo(fileName);
                if (splitInfos != null)
                {
                    foreach (var splitInfo in splitInfos)
                    {
                        try
                        {
                            string imgPath = path + "/" + splitInfo.imgName;
                            SplitImg(path + "/" + Path.GetFileNameWithoutExtension(fileName), imgPath, splitInfo.name, splitInfo.x, splitInfo.y, splitInfo.width, splitInfo.height);
                            PushDebugMsg("FlipFile:" + fileName);
                        }
                        catch (Exception e)
                        {
                            PushErrorMsg("FlipFile Fail:" + fileName + " E:" + e.ToString());
                        }
                    }
                }
            }
        }

        public void SplitImg(string dataPath, string imgPath, string splitName, int startX, int startY, int splitWidth, int splitHeight)
        {
            Bitmap mybm = new Bitmap(imgPath);
            string destFold = dataPath + "/" + splitName + ".png";
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            Bitmap splitedBM = new Bitmap(splitWidth, splitHeight);

            Color pixel;

            for (int x = 0; x < splitWidth; ++x)
            {
                for (int y = 0; y < splitHeight; ++y)
                {
                    pixel = mybm.GetPixel(x + startX, y + startY);//获取当前像素的值
                    splitedBM.SetPixel(x, y, pixel);//绘图
                }
            }

            mybm.Dispose();

            splitedBM.Save(destFold);
            splitedBM.Dispose();
        }

        public List<SplitInfo> GetSplitInfo(string path)
        {
            
            if (path.Contains(".pack"))
            {
                List<SplitInfo> SplitList = new List<SplitInfo>();
                StreamReader reader = new StreamReader(path);
                SplitInfo splitInfo = null;
                string imgFileName = "";
                while (!reader.EndOfStream)
                {
                    string readStr = reader.ReadLine();
                    if (readStr.Contains(".png"))
                    {
                        imgFileName = readStr;
                    }

                    if (readStr.Contains("repeat:") || readStr.Contains("index:"))
                    {
                        var name = reader.ReadLine();
                        if (string.IsNullOrEmpty(name))
                            continue;

                        splitInfo = new SplitInfo();
                        splitInfo.name = name;
                        splitInfo.imgName = imgFileName;
                    }

                    if (readStr.Contains("xy:"))
                    {
                        var values = readStr.Split(':');
                        var valueStrs = values[1].Split(',');
                        splitInfo.x = int.Parse(valueStrs[0]);
                        splitInfo.y = int.Parse(valueStrs[1]);
                    }

                    if (readStr.Contains("size:"))
                    {
                        var values = readStr.Split(':');
                        var valueStrs = values[1].Split(',');
                        splitInfo.width = int.Parse(valueStrs[0]);
                        splitInfo.height = int.Parse(valueStrs[1]);

                        SplitList.Add(splitInfo);
                        splitInfo = null;
                    }
                }

                return SplitList;
            }

            return null;
        }

        #endregion

        public bool IsFileAlpha(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);

            if (name.EndsWith("_alpha"))
                return true;

            if (name.EndsWith("_A"))
                return true;

            if (name.EndsWith("_AUTO_A"))
                return true;

            if (name.EndsWith("AlphaChannel"))
                return true;

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
            else if (fileName.Contains(".tga"))
            {
                return true;
            }

            return false;
        }
    }
}
