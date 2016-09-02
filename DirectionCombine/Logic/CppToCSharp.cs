using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirectionCombine
{
    class CppToCSharp
    {
        #region 单例

        private static CppToCSharp _Instance = null;
        public static CppToCSharp Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CppToCSharp();
                return _Instance;
            }
        }

        private CppToCSharp()
        {

        }

        #endregion

        #region dbstruct
        private string _InputPath;
        private string _OutPutPath;

        public void ChangeFoldToCs(string path)
        {
            if (!Directory.Exists(path))
                return;

            _InputPath = path;
            var files = Directory.GetFiles(path);
            _OutPutPath = Path.GetDirectoryName(path) + "/CSharp";
            foreach (var file in files)
            {
                ChangeFileToCs(file);
            }
        }

        private void ChangeFileToCs(string path)
        {
            //if (Path.GetExtension(path).Contains ("cpp"))
            //    return;

            string cppFilePath = FindCppFile(path);
            //if (File.Exists(cppFilePath))
            //{
                ChangeHAndCppFile(path, cppFilePath);
            //}
            //else
            //{
            //    ChangeHFileOnly(path);
            //}
        }

        private string FindCppFile(string hFilePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(hFilePath);
            fileName = hFilePath.Replace(".h", ".cpp");
            return fileName;
        }

        private void ChangeHFileOnly(string path)
        { }

        private void ChangeHAndCppFile(string hPath, string cppPath)
        {
            StreamReader hReader = new StreamReader(hPath, Encoding.UTF8);
            //StreamReader cppReader = new StreamReader(cppPath, Encoding.UTF8);

            if (!Directory.Exists(_OutPutPath))
            {
                Directory.CreateDirectory(_OutPutPath);
            }
            StreamWriter csWriter = new StreamWriter(hPath.Replace(_InputPath, _OutPutPath).Replace(".h", ".cs"), true, Encoding.UTF8);

            //string sqlPath = _OutPutPath + "dbcreatetable.sql";
            //StreamWriter sqlWriter = new StreamWriter(sqlPath, true, Encoding.UTF8);

            //********************** write cs title
            //csWriter.Write("using System;\nusing ProtoBuf;\nusing ZyGames.Framework.Event;\nusing ZyGames.Framework.Model;\nusing ZyGames.Framework.Cache.Generic;\n");
            //csWriter.Write("namespace GameServer.Model\n{\n[Serializable, ProtoContract]\n[EntityTable(CacheType.Entity, \"ConnData\")]\n");

            //********************** Read Head
            string className;
            string funName;
            string attrType;
            string attrName;
            string length;
            int attrNum = 0;
            Dictionary<string, string> sqlValues = new Dictionary<string, string>();
            
            while (!hReader.EndOfStream)
            {
                string line = hReader.ReadLine();
                

                line = ChangeType(line);
                if (IsCanDropLine(line))
                    continue;

                if (TryGetClassName(line, out className))
                {
                    attrNum = 0;
                    csWriter.Write("using System;\nusing System.Collections.Generic;\nusing ProtoBuf;\nusing ZyGames.Framework.Event;\nusing ZyGames.Framework.Model;\nusing ZyGames.Framework.Cache.Generic;\n");
                    csWriter.Write("\nnamespace GameServer.Model\n{\n\t[Serializable, ProtoContract]\n\t[EntityTable(CacheType.Entity, \"ConnData\")]\n");

                    csWriter.Write("\tpublic class " + className + " : BaseEntity\n\t{\n\t\tpublic " + className + "(): base(){}\n");
                }
                else if (TryGetFunName(line, out funName))
                {

                }
                else if (TryGetAttribute(line, out attrType, out attrName))
                {
                    if (attrType.Contains("List"))
                    {
                        csWriter.Write("\t\t[ProtoMember(" + (++attrNum).ToString() + ")]\n\t\t[EntityField(true, ColumnDbType.Text)]\n");
                    }
                    else
                    {
                        csWriter.Write("\t\t[ProtoMember(" + (++attrNum).ToString() + ")]\n\t\t[EntityField(true)]\n");
                    }

                    csWriter.Write("\t\tpublic " + attrType + " " + attrName + "\n\t\t{\n\t\t\tget;\n\t\t\tset;\n\t\t}\n");
                }
                else
                {
                    csWriter.Write(line + "\n");
                }
            }

            csWriter.Write("\t}\n}");
            csWriter.Close();
        }

        private bool TryGetClassName(string line, out string className)
        {
            if (line.Contains("struct ") || line.Contains("class "))
            {
                var strs = line.Split(' ');
                className = strs[1];
                return true;
            }
            className = "";
            return false;
        }

        private bool TryGetAttribute(string line, out string type, out string attrName)
        {
            type = "";
            attrName = "";
            int idx = line.IndexOf(';');
            if (idx < 0)
                return false;
            string strLine = line.Remove(idx);
            string[] strs;
            strLine = strLine.Trim();
            if (strLine.Contains(' '))
            {
                strs = strLine.Split(' ');
            }
            else
            {
                strs = strLine.Split('\t');
            }
            bool isGetType = false;
            foreach (var str in strs)
            {
                if (!isGetType)
                {
                    type = str.Trim();
                    if (string.IsNullOrEmpty(type))
                        continue;

                    isGetType = true;
                }
                else
                {
                    attrName = str.Trim();
                    if (string.IsNullOrEmpty(attrName))
                        continue;

                    isGetType = true;
                }
            }
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(attrName))
                return true;
            
            return false;
        }

        private bool TryGetArrayLength(string line, out string length)
        {
            if (line.Contains("[") && line.Contains("]"))
            {
                int left = line.IndexOf('[');
                int right = line.IndexOf(']');
                length = line.Substring(left, right - left);
                return true;
            }
            length = "";
            return false;
        }

        private bool TryGetFunName(string line, out string funName)
        {
            if (line.Contains('('))
            {
                int idx = line.IndexOf('(');
                int funNameStart = 0;
                for (int i = idx; i >= 0; --i)
                {
                    if (line[i] == ' ')
                    {
                        funNameStart = i;
                        break;
                    }
                }
                funName = line.Substring(funNameStart, idx - funNameStart);
                funName = funName.Trim();
                return true;
            }
            funName = "";
            return false;
        }

        private string ChangeType(string inline)
        {
            string outLine = inline;
            outLine = outLine.Replace("const tchar*", "string");
            outLine = outLine.Replace("tint32", "int");
            outLine = outLine.Replace("tint16", "short");
            outLine = outLine.Replace("tint8", "byte");
            outLine = outLine.Replace("tuint32", "uint");
            outLine = outLine.Replace("tuint16", "ushort");
            outLine = outLine.Replace("tuint8", "byte");
            outLine = outLine.Replace("tfloat32", "float");
            outLine = outLine.Replace("Guid64", "ulong");
            outLine = outLine.Replace("time_t", "TimeSpan");
            outLine = outLine.Replace("::", ".");


            if (IsArray(outLine))
            {
                if (outLine.Contains("tchar"))
                {
                    outLine = ChangeArrayToString(outLine, "tchar");
                }
                else if (outLine.Contains("char"))
                {
                    outLine = ChangeArrayToString(outLine, "char");
                }
                else
                {
                    outLine = ChangeArrayToList(outLine);
                }
            }
            return outLine;
        }

        private bool IsArray(string line)
        {
            if (line.Contains("[") && line.Contains("]"))
                return true;
            if (line.Contains("BitSet<"))
                return true;
            return false;
        }

        private string ChangeArrayToString(string arrayLine, string charStr)
        {
            int left = arrayLine.IndexOf('[');
            int right = arrayLine.IndexOf(']');
            string strStr = arrayLine.Substring(0, left);
            strStr.Replace("char", "string");
            strStr += ";";
            return strStr;
        }

        private string ChangeArrayToList(string arrayLine)
        {
            string type;
            string attrName;
            string listStr = "";
            if (TryGetAttribute(arrayLine, out type, out attrName))
            {
                
                if (type.Contains("BitSet"))
                {
                    listStr = "List<bool> " + attrName + ";";
                }
                else
                {
                    int left = attrName.IndexOf('[');
                    int right = attrName.IndexOf(']');
                    if (left < 0)
                    {
                        return arrayLine;
                    }
                    string name = attrName.Substring(0, left);
                    listStr = "List<" + type + "> " + name + ";";
                }
            }
            return listStr;
        }

        private bool IsCanDropLine(string line)
        {
            if (line.StartsWith("//"))
                return true;

            if (line.StartsWith("#include"))
                return true;

            return false;
        }

        //private string ChangeArray(string arrayLine)
        //{
        //    int left = arrayLine.IndexOf('[');
        //    int right = arrayLine.IndexOf(']');
        //    string Num = arrayLine.Substring(left, right);
        //    strStr.Replace("char", "string");
        //    return strStr;
        //}
        #endregion

        #region tables
        string _TabInPath = "";
        string _TabOutPath = "";

        public void ChangeTabFold(string path)
        {
            if (!Directory.Exists(path))
                return;

            _TabInPath = path;
            _TabOutPath = Path.GetDirectoryName(path) + "/TableReader";

            if (!Directory.Exists(_TabOutPath))
            {
                Directory.CreateDirectory(_TabOutPath);
            }

            var files = Directory.GetFiles(_TabInPath);
            List<string> classNames = new List<string>();
            foreach (var file in files)
            {
                if (Path.GetExtension(file) == ".txt")
                {
                    string className = ChangeTab(file);
                    classNames.Add(className);
                }
            }

            WriteTabReader(classNames);
        }

        public string ChangeTab(string filePath)
        {
            StreamReader tabReader = new StreamReader(filePath, Encoding.UTF8);
            string[] propteries = tabReader.ReadLine().Split('\t');
            string[] propTypes = tabReader.ReadLine().Split('\t');
            tabReader.Close();

            return WriteTabCs(filePath, propteries, propTypes);
        }

        public string WriteTabCs(string filePath, string[] propteries, string[] propTypes)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string className = "Tab" + fileName;
            string outFilePath = _TabOutPath + "/" + className + ".cs";

            StreamWriter tabWriter = new StreamWriter(outFilePath);
            tabWriter.Write("using System;\nusing System.Collections.Generic;\nusing System.Collections;\n\n ");
            tabWriter.Write("namespace GameServer.Logic\n{\n");
            tabWriter.Write("\t[Serializable]");
            tabWriter.Write("\n\tpublic class " + className);
            tabWriter.Write("\n\t{\n");
            tabWriter.Write("\n\t\tprivate const string TAB_FILE_DATA = \"./Config/" + fileName + ".txt\";");
            tabWriter.Write("\n\t\tpublic static string GetInstanceFile(){return TAB_FILE_DATA; }\n");
            for (int i = 0; i < propteries.Length; ++i)
            {
                tabWriter.Write("\n\t\tprivate " + GetClassType(propTypes[i]) + " m_" + propteries[i] + ";");
                tabWriter.Write("\n\t\tpublic " + GetClassType(propTypes[i]) + " " + propteries[i] + "{ get{ return m_" + propteries[i]  + ";}}\n");
            }
            tabWriter.Write("\n\t\tpublic static bool LoadTable(Dictionary<int, List<object> > _tab)");
            tabWriter.Write("\n\t\t{");
            tabWriter.Write("\n\t\t\tif(!TableManager.ReaderPList(GetInstanceFile(),SerializableTable,_tab))");
            tabWriter.Write("\n\t\t\t{");
            tabWriter.Write("\n\t\t\t\tthrow TableException.ErrorReader(\"Load File{ 0}Fail!!!\",GetInstanceFile());");
            tabWriter.Write("\n\t\t\t}");
            tabWriter.Write("\n\t\t\treturn true;");
            tabWriter.Write("\n\t\t}");

            tabWriter.Write("\n\t\tpublic static void SerializableTable(string[] valuesList,int skey,Dictionary<int, List<object> > _hash)");
            tabWriter.Write("\n\t\t{");
            //tabWriter.Write("\n\t\t\tif ((int)_ID.MAX_RECORD!=valuesList.Length)");
            //tabWriter.Write("\n\t\t\t{");
            //tabWriter.Write("\n\t\t\t\tthrow TableException.ErrorReader(\"Load { 0}error as CodeSize:{ 1}not Equal DataSize: { 2}\", GetInstanceFile(),_ID.MAX_RECORD,valuesList.Length);");
            //tabWriter.Write("\n\t\t\t}");
            tabWriter.Write("\n\t\t\t" + className + " _values = new " + className + "();");
            for (int i = 0; i < propteries.Length; ++i)
            {
                tabWriter.Write("\n\t\t\t_values.m_" + propteries[i] + " = " + GetConvertType(propTypes[i]) + "(valuesList[" + i + "]);");
            }
            tabWriter.Write("\n\t\t\t_Values.Add(_values." + propteries[0] + ", _values);");
            tabWriter.Write("\n\t\t}");

            tabWriter.Write("\n\n\t\tprivate static Dictionary<int, " + className + "> _Values;");
            tabWriter.Write("\n\t\tpublic static Dictionary<int, " + className + "> Values");
            tabWriter.Write("\n\t\t{");
            tabWriter.Write("\n\t\t\tget");
            tabWriter.Write("\n\t\t\t{");
            tabWriter.Write("\n\t\t\t\tif (_Values == null)");
            tabWriter.Write("\n\t\t\t\t{");
            tabWriter.Write("\n\t\t\t\t\t_Values = new Dictionary<int, " + className + ">();");
            tabWriter.Write("\n\t\t\t\t\tLoadTable(new Dictionary<int, List<object>>());");
            tabWriter.Write("\n\t\t\t\t}");
            tabWriter.Write("\n\t\t\t\treturn _Values;");
            tabWriter.Write("\n\t\t\t}");
            tabWriter.Write("\n\t\t}");
            tabWriter.Write("\n\t\tpublic static " + className + " GetRecordByID(int id)");
            tabWriter.Write("\n\t\t{");
            tabWriter.Write("\n\t\t\tif (id < _Values.Count)");
            tabWriter.Write("\n\t\t\t{");
            tabWriter.Write("\n\t\t\treturn _Values[id];");
            tabWriter.Write("\n\t\t\t}");
            tabWriter.Write("\n\t\t\treturn null;");
            tabWriter.Write("\n\t\t}");
            tabWriter.Write("\n\t}");
            tabWriter.Write("\n}");

            tabWriter.Close();

            return className;
        }

        public void WriteTabReader(List<string> tabNamelist)
        {
            string outFilePath = _TabOutPath + "/_TableReader.cs";

            StreamWriter tabWriter = new StreamWriter(outFilePath);
            tabWriter.Write("using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nusing System.Threading.Tasks;");
            tabWriter.Write("\nnamespace GameServer.Logic\n{");
            tabWriter.Write("\n\tclass _TableReader\n\t{");
            tabWriter.Write("\n\t\tpublic static void InitTables()\n\t\t{");
            tabWriter.Write("\n\t\t\tint count = 0;");
            tabWriter.Write("\n\t\t\tConsole.WriteLine(\"InitTables start\");\n");

            foreach (var className in tabNamelist)
            {
                tabWriter.Write("\n\t\t\tcount = " + className + ".Values.Count;");
            }
            tabWriter.Write("\n\t\t}");
            tabWriter.Write("\n\t}");
            tabWriter.Write("\n}");
            tabWriter.Close();
        }

        public string GetClassType(string tabType)
        {
            if (tabType == "STRING")
                return "string";
            if (tabType == "INT")
                return "int";
            if (tabType == "FLOAT")
                return "float";

            return tabType;
        }

        public string GetConvertType(string tabType)
        {
            if (tabType == "STRING")
                return "Convert.ToString";
            if (tabType == "INT")
                return "Convert.ToInt32";
            if (tabType == "FLOAT")
                return "(float)Convert.ToDouble";

            return tabType;
        }

        #endregion

        #region funcs

        string _FunInPath = "";
        string _FunOutPath = "";

        public void ChangeFunFold(string path)
        {
            if (!Directory.Exists(path))
                return;

            _FunInPath = path;
            _FunOutPath = Path.GetDirectoryName(path) + "/FunCs";

            if (!Directory.Exists(_FunOutPath))
            {
                Directory.CreateDirectory(_FunOutPath);
            }

            var files = Directory.GetFiles(_FunInPath);
            List<string> classNames = new List<string>();
            foreach (var file in files)
            {
                if (Path.GetExtension(file) == ".cpp")
                {
                    string className = ChangeFun(file);
                    classNames.Add(className);
                }
            }

        }

        public string ChangeFun(string filePath)
        {
            StreamReader FunReader = new StreamReader(filePath, Encoding.UTF8);
            string[] propteries = FunReader.ReadLine().Split('\t');
            string[] propTypes = FunReader.ReadLine().Split('\t');
            FunReader.Close();

            return WriteFunCs(filePath, propteries, propTypes);
        }

        public string WriteFunCs(string filePath, string[] propteries, string[] propTypes)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string className = "Fun" + fileName;
            string outFilePath = _FunOutPath + "/" + className + ".cs";

            StreamWriter FunWriter = new StreamWriter(outFilePath);
            FunWriter.Write("using System;\nusing System.Collections.Generic;\nusing System.Collections;\n\n ");
            FunWriter.Write("namespace GameServer.Logic\n{\n");
            FunWriter.Write("\t[Serializable]");
            FunWriter.Write("\n\tpublic class " + className);
            FunWriter.Write("\n\t{\n");
            FunWriter.Write("\n\t\tprivate const string Fun_FILE_DATA = \"./Config/" + fileName + ".txt\";");
            FunWriter.Write("\n\t\tpublic static string GetInstanceFile(){return Fun_FILE_DATA; }\n");
            for (int i = 0; i < propteries.Length; ++i)
            {
                FunWriter.Write("\n\t\tprivate " + GetClassType(propTypes[i]) + " m_" + propteries[i] + ";");
                FunWriter.Write("\n\t\tpublic " + GetClassType(propTypes[i]) + " " + propteries[i] + "{ get{ return m_" + propteries[i] + ";}}\n");
            }
            FunWriter.Write("\n\t\tpublic static bool LoadFunle(Dictionary<int, List<object> > _Fun)");
            FunWriter.Write("\n\t\t{");
            FunWriter.Write("\n\t\t\tif(!FunleManager.ReaderPList(GetInstanceFile(),SerializableFunle,_Fun))");
            FunWriter.Write("\n\t\t\t{");
            FunWriter.Write("\n\t\t\t\tthrow FunleException.ErrorReader(\"Load File{ 0}Fail!!!\",GetInstanceFile());");
            FunWriter.Write("\n\t\t\t}");
            FunWriter.Write("\n\t\t\treturn true;");
            FunWriter.Write("\n\t\t}");

            FunWriter.Write("\n\t\tpublic static void SerializableFunle(string[] valuesList,int skey,Dictionary<int, List<object> > _hash)");
            FunWriter.Write("\n\t\t{");
            //FunWriter.Write("\n\t\t\tif ((int)_ID.MAX_RECORD!=valuesList.Length)");
            //FunWriter.Write("\n\t\t\t{");
            //FunWriter.Write("\n\t\t\t\tthrow FunleException.ErrorReader(\"Load { 0}error as CodeSize:{ 1}not Equal DataSize: { 2}\", GetInstanceFile(),_ID.MAX_RECORD,valuesList.Length);");
            //FunWriter.Write("\n\t\t\t}");
            FunWriter.Write("\n\t\t\t" + className + " _values = new " + className + "();");
            for (int i = 0; i < propteries.Length; ++i)
            {
                FunWriter.Write("\n\t\t\t_values.m_" + propteries[i] + " = " + GetConvertType(propTypes[i]) + "(valuesList[" + i + "]);");
            }
            FunWriter.Write("\n\t\t\t_Values.Add(_values." + propteries[0] + ", _values);");
            FunWriter.Write("\n\t\t}");

            FunWriter.Write("\n\n\t\tprivate static Dictionary<int, " + className + "> _Values;");
            FunWriter.Write("\n\t\tpublic static Dictionary<int, " + className + "> Values");
            FunWriter.Write("\n\t\t{");
            FunWriter.Write("\n\t\t\tget");
            FunWriter.Write("\n\t\t\t{");
            FunWriter.Write("\n\t\t\t\tif (_Values == null)");
            FunWriter.Write("\n\t\t\t\t{");
            FunWriter.Write("\n\t\t\t\t\t_Values = new Dictionary<int, " + className + ">();");
            FunWriter.Write("\n\t\t\t\t\tLoadFunle(new Dictionary<int, List<object>>());");
            FunWriter.Write("\n\t\t\t\t}");
            FunWriter.Write("\n\t\t\t\treturn _Values;");
            FunWriter.Write("\n\t\t\t}");
            FunWriter.Write("\n\t\t}");
            FunWriter.Write("\n\t\tpublic static " + className + " GetRecordByID(int id)");
            FunWriter.Write("\n\t\t{");
            FunWriter.Write("\n\t\t\tif (id < _Values.Count)");
            FunWriter.Write("\n\t\t\t{");
            FunWriter.Write("\n\t\t\treturn _Values[id];");
            FunWriter.Write("\n\t\t\t}");
            FunWriter.Write("\n\t\t\treturn null;");
            FunWriter.Write("\n\t\t}");
            FunWriter.Write("\n\t}");
            FunWriter.Write("\n}");

            FunWriter.Close();

            return className;
        }

        public void WriteFunReader(List<string> FunNamelist)
        {
            string outFilePath = _FunOutPath + "/_FunleReader.cs";

            StreamWriter FunWriter = new StreamWriter(outFilePath);
            FunWriter.Write("using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nusing System.Threading.Tasks;");
            FunWriter.Write("\nnamespace GameServer.Logic\n{");
            FunWriter.Write("\n\tclass _FunleReader\n\t{");
            FunWriter.Write("\n\t\tpublic static void InitFunles()\n\t\t{");
            FunWriter.Write("\n\t\t\tint count = 0;");
            FunWriter.Write("\n\t\t\tConsole.WriteLine(\"InitFunles start\");\n");

            foreach (var className in FunNamelist)
            {
                FunWriter.Write("\n\t\t\tcount = " + className + ".Values.Count;");
            }
            FunWriter.Write("\n\t\t}");
            FunWriter.Write("\n\t}");
            FunWriter.Write("\n}");
            FunWriter.Close();
        }

        #endregion
    }
}
