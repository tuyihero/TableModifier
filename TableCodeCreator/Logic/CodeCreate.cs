using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableConstruct;
using System.IO;

namespace TableCodeCreator
{
    class CodeCreate
    {
        #region 唯一实例
        private static CodeCreate _Instance = null;
        public static CodeCreate Instance 
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new CodeCreate();
                return _Instance;
            }
        }

        private CodeCreate()
        {
 
        }
        #endregion

        public const string CODE_DIRECTORY = "TableReader";
        public const string CODE_CSV_DIRECTORY = "KBCsv";
        public const string CODE_READ_BASE_FILE = "TableReadBase.cs";
        public const string CODE_ENUM_FILE = "TableEnum.cs";
        public const string CODE_TABLE_BASE_DIRECTORY = "TableBase";
        public const string CODE_TABLE_READ_FILE = "TableReader.cs";

        public const string CODE_WRITE_DIRECTORY = "TableWriter";
        public const string CODE_WRITE_BASE_FILE = "TableWriteBase.cs";
        public const string CODE_WRITE_FILE = "TableWriter.cs";

        public void CreateCode()
        {
            //先初始化表结构
            ConstructFold.Instance.InitFold();

            //1.生成目录
            string codeDirectory = System.IO.Path.Combine(TableGlobalConfig.Instance.CodePath, CODE_DIRECTORY);
            System.IO.Directory.CreateDirectory(codeDirectory);

            //2.复制csv读取（如果不存在）
            string codeCsvDirectory = System.IO.Path.Combine(codeDirectory, CODE_CSV_DIRECTORY);
            if (!System.IO.Directory.Exists(codeCsvDirectory))
            {
                string tempCsvPath = System.IO.Path.Combine(TableGlobalConfig.Instance.TemplatePath, CODE_CSV_DIRECTORY);
                CopyDirectory(tempCsvPath, codeCsvDirectory);
            }

            //3.复制TableReadBase
            {
                string readBasePath = System.IO.Path.Combine(codeDirectory, CODE_READ_BASE_FILE);
                if (File.Exists(readBasePath))
                {
                    File.SetAttributes(readBasePath, FileAttributes.Normal);
                    File.Delete(readBasePath);
                }
                string tempReadBasePath = System.IO.Path.Combine(TableGlobalConfig.Instance.TemplatePath, CODE_READ_BASE_FILE);
                File.Copy(tempReadBasePath, readBasePath);
            }

            //4.生成Enum
            {
                string enumPath = System.IO.Path.Combine(codeDirectory, CODE_ENUM_FILE);
                if (File.Exists(enumPath))
                {
                    File.SetAttributes(enumPath, FileAttributes.Normal);
                    File.Delete(enumPath);
                }
                CSCreateEnumFile(enumPath);
            }

            //5.生成表格base
            {
                string tableBasePath = System.IO.Path.Combine(codeDirectory, CODE_TABLE_BASE_DIRECTORY);
                if (Directory.Exists(tableBasePath))
                {
                    Directory.Delete(tableBasePath, true);
                }

                System.IO.Directory.CreateDirectory(tableBasePath);
                CSCreateTableBase(tableBasePath);

            }

            //6.生成TableRead
            {
                string tableReaderPath = System.IO.Path.Combine(codeDirectory, CODE_TABLE_READ_FILE);
                if (File.Exists(tableReaderPath))
                {
                    File.SetAttributes(tableReaderPath, FileAttributes.Normal);
                    File.Delete(tableReaderPath);
                }
                CSCreateTableReaderFile(tableReaderPath);
            }

            //7.复制WriteBase
            {
                string writeDirectory = System.IO.Path.Combine(TableGlobalConfig.Instance.CodePath, CODE_WRITE_DIRECTORY);
                System.IO.Directory.CreateDirectory(writeDirectory);

                string writeBasePath = System.IO.Path.Combine(writeDirectory, CODE_WRITE_BASE_FILE);
                if (File.Exists(writeBasePath))
                {
                    File.SetAttributes(writeBasePath, FileAttributes.Normal);
                    File.Delete(writeBasePath);
                }
                string tempReadBasePath = System.IO.Path.Combine(TableGlobalConfig.Instance.TemplatePath, CODE_WRITE_BASE_FILE);
                File.Copy(tempReadBasePath, writeBasePath);

                string writePath = System.IO.Path.Combine(writeDirectory, CODE_WRITE_FILE);
                if (File.Exists(writePath))
                {
                    File.SetAttributes(writePath, FileAttributes.Normal);
                    File.Delete(writePath);
                }
                tempReadBasePath = System.IO.Path.Combine(TableGlobalConfig.Instance.TemplatePath, CODE_WRITE_FILE);
                File.Copy(tempReadBasePath, writePath);
            }
        }

        public static void CopyDirectory(string sourceDirectory, string destDirectory)
        {
            //判断源目录和目标目录是否存在，如果不存在，则创建一个目录
            if (!Directory.Exists(sourceDirectory))
            {
                Directory.CreateDirectory(sourceDirectory);
            }
            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            //拷贝文件
            CopyFile(sourceDirectory, destDirectory);

            //拷贝子目录       
            //获取所有子目录名称
            string[] directionName = Directory.GetDirectories(sourceDirectory);

            foreach (string directionPath in directionName)
            {
                //根据每个子目录名称生成对应的目标子目录名称
                string directionPathTemp = destDirectory + "\\" + directionPath.Substring(sourceDirectory.Length + 1);

                //递归下去
                CopyDirectory(directionPath, directionPathTemp);
            }
        }
        public static void CopyFile(string sourceDirectory, string destDirectory)
        {
            //获取所有文件名称
            string[] fileName = Directory.GetFiles(sourceDirectory);

            foreach (string filePath in fileName)
            {
                //根据每个文件名称生成对应的目标文件名称
                string filePathTemp = destDirectory + "\\" + filePath.Substring(sourceDirectory.Length + 1);

                //若不存在，直接复制文件；若存在，覆盖复制
                if (File.Exists(filePathTemp))
                {
                    File.Copy(filePath, filePathTemp, true);
                }
                else
                {
                    File.Copy(filePath, filePathTemp);
                }
            }
        }

        #region create CSharp file
        
        private void CSCreateEnumFile(string enumPath)
        {
            var builder = new StringBuilder();
            builder.Append("using System.Collections;\n");
            builder.Append("\n");
            builder.Append("namespace Tables\n");
            builder.Append("{\n");
            builder.Append("\n");

            foreach (EnumInfo enumInfo in EnumManager.Instance.EnumInfoCollection)
            {
                builder.Append("    //" + enumInfo.Desc + "\n");
                builder.Append("    public enum " + enumInfo.Name + "\n");
                builder.Append("    {\n");

                foreach (EnumItem enumItem in enumInfo._EnumItemCollection)
                {
                    builder.Append("        " + enumItem.ItemCode 
                        + " = " + enumItem.ItemValue 
                        + "," + " //" + enumItem.Name + "," + enumItem.ItemDesc + "\n");
                }

                builder.Append("    }\n");
                builder.Append("\n");
            }

            builder.Append("\n");
            builder.Append("}");

            File.WriteAllText(enumPath, builder.ToString(), Encoding.UTF8);

        }

        private void CSCreateTableBase(string tableBasePath)
        {
            foreach (ConstructFile tableFile in ConstructFold.Instance.ConstructFiles)
            {
                string filePath = tableBasePath + "/" + tableFile.Name + ".cs";
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }

                CSCreateTableFile(tableFile, filePath);
            }
        }

        private void CSCreateTableFile(ConstructFile constructFile, string filePath)
        {
            var builder = new StringBuilder();
            var itemInit = new StringBuilder();

            //record
            builder.Append("using System;\n");
            builder.Append("using System.IO;\n");
            builder.Append("using System.Text;\n");
            builder.Append("using System.Collections.Generic;\n");
            builder.Append("using Kent.Boogaart.KBCsv;\n\n");
            builder.Append("using UnityEngine;\n\n");
            builder.Append("namespace Tables\n");
            builder.Append("{\n");

            builder.Append("    public partial class " + constructFile.Name + "Record  : TableRecordBase\n");
            builder.Append("    {\n");
            builder.Append("        public DataRecord ValueStr;\n\n");
            foreach(ConstructItem constructItem in constructFile.ConstructItems)
            {
                string typeCode = CSTableItemTypeCode(constructItem);
                builder.Append("        public " + typeCode + " " + constructItem.ItemCode + " { get; set; }\n");
                if (typeCode.Contains("List"))
                {
                    itemInit.Append("            " + constructItem.ItemCode + 
                        " = new " + typeCode + "();\n");
                }
            }

            builder.Append("        public " + constructFile.Name + "Record(DataRecord dataRecord)\n");
            builder.Append("        {\n");
            builder.Append("            if (dataRecord != null)\n");
            builder.Append("            {\n");
            builder.Append("                ValueStr = dataRecord;\n");
            builder.Append("                Id = ValueStr[0];\n\n");
            builder.Append("            }\n");
            builder.Append(itemInit);

            builder.Append("        }\n");
            builder.Append("        public string[] GetRecordStr()\n");
            builder.Append("        {\n");
            builder.Append("            List<string> recordStrList = new List<string>();\n");
            for (int i = 0; i < constructFile.ConstructItems.Count; ++i)
            {
                var constructItem = constructFile.ConstructItems[i] as ConstructItem;
                if (constructItem.ItemRepeat > 1)
                {
                    builder.Append("            foreach (var testTableItem in " + constructItem.ItemCode + ")\n");
                    builder.Append("            {\n");
                    if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID
                        && constructItem.ItemType2.Count == 1)
                    {
                        builder.Append("                if (testTableItem != null)\n");
                        builder.Append("                {\n");
                        builder.Append("                    recordStrList.Add(testTableItem.Id);\n");
                        builder.Append("                }\n");
                        builder.Append("                else\n");
                        builder.Append("                {\n");
                        builder.Append("                    recordStrList.Add(\"\");\n");
                        builder.Append("                }\n");
                    }
                    else if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM)
                    {
                        builder.Append("                recordStrList.Add(((int)testTableItem).ToString());\n");
                    }
                    else
                    {
                        builder.Append("                recordStrList.Add(TableWriteBase.GetWriteStr(testTableItem));\n");
                    }
                    builder.Append("            }\n");
                }
                else
                {
                    if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID
                        && constructItem.ItemType2.Count == 1)
                    {
                        builder.Append("            if (" + constructItem.ItemCode + " != null)\n");
                        builder.Append("            {\n");
                        builder.Append("                recordStrList.Add(" + constructItem.ItemCode + ".Id);\n");
                        builder.Append("            }\n");
                        builder.Append("            else\n");
                        builder.Append("            {\n");
                        builder.Append("                recordStrList.Add(\"\");\n");
                        builder.Append("            }\n");
                    }
                    else if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM)
                    {
                        builder.Append("            recordStrList.Add(((int)" + constructItem.ItemCode + ").ToString());\n");
                    }
                    else
                    {
                        builder.Append("            recordStrList.Add(TableWriteBase.GetWriteStr(" + constructItem.ItemCode + "));\n");
                    }
                }

            }
            builder.Append("\n            return recordStrList.ToArray();\n");
            builder.Append("        }\n");
            builder.Append("    }\n\n");

            //for info
            builder.Append("    public partial class " + constructFile.Name + " : TableFileBase\n");
            builder.Append("    {\n");
            builder.Append("        public Dictionary<string, " + constructFile.Name + "Record> Records { get; internal set; }\n\n");
            builder.Append("        public bool ContainsKey(string key)\n");
            builder.Append("        {\n");
            builder.Append("             return Records.ContainsKey(key);\n");
            builder.Append("        }\n\n");
            builder.Append("        public " + constructFile.Name + "Record GetRecord(string id)\n");
            builder.Append("        {\n");
            builder.Append("            try\n");
            builder.Append("            {\n");
            builder.Append("                return Records[id];\n");
            builder.Append("            }\n");
            builder.Append("            catch (Exception ex)\n");
            builder.Append("            {\n");
            builder.Append("                throw new Exception(\"" + constructFile.Name + "\" + \": \" + id, ex);\n");
            builder.Append("            }\n");
            builder.Append("        }\n\n");
            builder.Append("        public " + constructFile.Name + "(string pathOrContent,bool isPath = true)\n");
            builder.Append("        {\n");
            builder.Append("            Records = new Dictionary<string, " + constructFile.Name + "Record>();\n");
            builder.Append("            if(isPath)\n");
            builder.Append("            {\n");
            builder.Append("                string[] lines = File.ReadAllLines(pathOrContent);\n");
            builder.Append("                lines[0] = lines[0].Replace(\"\\r\\n\", \"\\n\");\n");
            builder.Append("                ParserTableStr(string.Join(\"\\n\", lines));\n");
            builder.Append("            }\n");
            builder.Append("            else\n");
            builder.Append("            {\n");
            builder.Append("                ParserTableStr(pathOrContent.Replace(\"\\r\\n\", \"\\n\"));\n");
            builder.Append("            }\n");
            builder.Append("        }\n\n");
            builder.Append("        private void ParserTableStr(string content)\n");
            builder.Append("        {\n");
            builder.Append("            StringReader rdr = new StringReader(content);\n");
            builder.Append("            using (var reader = new CsvReader(rdr))\n");
            builder.Append("            {\n");
            builder.Append("                HeaderRecord header = reader.ReadHeaderRecord();\n");
            builder.Append("                while (reader.HasMoreRecords)\n");
            builder.Append("                {\n");
            builder.Append("                    DataRecord data = reader.ReadDataRecord();\n");
            builder.Append("                    if (data[0].StartsWith(\"#\"))\n");
            builder.Append("                        continue;\n\n");
            builder.Append("                    " + constructFile.Name + "Record record = new " + constructFile.Name + "Record(data);\n");
            builder.Append("                    Records.Add(record.Id, record);\n");
            builder.Append("                }\n");
            builder.Append("            }\n");
            builder.Append("        }\n\n");
            builder.Append("        public void CoverTableContent()\n");
            builder.Append("        {\n");
            builder.Append("            foreach (var pair in Records)\n");
            builder.Append("            {\n");

            int dataIdx = 0;
            for (int i = 1; i < constructFile.ConstructItems.Count; ++i)
            {
                ++dataIdx;
                var constructItem = constructFile.ConstructItems[i] as ConstructItem;
                if (constructItem.ItemRepeat > 1)
                {
                    for (int j = 0; j < constructItem.ItemRepeat; ++j)
                    {
                        builder.Append(CSTableItemParseCode(constructItem, dataIdx, j));
                        if (j != constructItem.ItemRepeat - 1)
                        {
                            ++dataIdx;
                        }
                    }
                }
                else
                {
                    builder.Append(CSTableItemParseCode(constructItem, dataIdx, -1));
                }

            }

            builder.Append("            }\n");
            builder.Append("        }\n");
            builder.Append("    }\n");

            builder.Append("\n");
            builder.Append("}");

            File.WriteAllText(filePath, builder.ToString(), Encoding.UTF8);
        }

        private void CSCreateTableItem(ConstructItem constructItem, ref StringBuilder builder)
        {
 
        }

        private string CSTableItemTypeCode(ConstructItem constructItem)
        {
            string typeCode = "";
            if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_BASE)
            {
                switch (constructItem.ItemType2[0].Name)
                {
                    case ConstructConfig.ITEM_TYPE_BASE_INT:
                        typeCode = "int";
                        break;
                    case ConstructConfig.ITEM_TYPE_BASE_FLOAT:
                        typeCode = "float";
                        break;
                    case ConstructConfig.ITEM_TYPE_BASE_STRING:
                        typeCode = "string";
                        break;
                    case ConstructConfig.ITEM_TYPE_BASE_BOOL:
                        typeCode = "bool";
                        break;
                    case ConstructConfig.ITEM_TYPE_BASE_VECTOR3:
                        typeCode = "Vector3";
                        break;
                }
            }
            else if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM)
            {
                typeCode = constructItem.ItemType2[0].Name;
            }
            else if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID)
            {
                if (constructItem.ItemType2.Count == 1)
                {
                    typeCode = constructItem.ItemType2[0].Name + "Record";
                }
                else
                {
                    typeCode = "MultiTable";
                }
            }

            if (constructItem.ItemRepeat > 1)
            {
                typeCode = "List<" + typeCode + ">";
            }

            return typeCode;
        }

        private StringBuilder CSTableItemParseCode(ConstructItem constructItem, int dataIdx, int repeatIdx)
        {
            var parseBuilder = new StringBuilder();
            string parseValueStart = " = ";
            string parseValueEnd = "";
            if (repeatIdx > -1)
            {
                parseValueStart = ".Add(";
                parseValueEnd = ")";
            }
            
            
            if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_BASE)
            {
                switch (constructItem.ItemType2[0].Name)
                {
                    case ConstructConfig.ITEM_TYPE_BASE_INT:
                        parseBuilder.Append("                pair.Value." 
                            + constructItem.ItemCode + parseValueStart + "TableReadBase.ParseInt(pair.Value.ValueStr[" 
                            + dataIdx + "])" + parseValueEnd  + ";");
                        break;
                    case ConstructConfig.ITEM_TYPE_BASE_FLOAT:
                        parseBuilder.Append("                pair.Value."
                            + constructItem.ItemCode + parseValueStart + "TableReadBase.ParseFloat(pair.Value.ValueStr["
                            + dataIdx + "])" + parseValueEnd + ";");
                        break;
                    case ConstructConfig.ITEM_TYPE_BASE_STRING:
                        parseBuilder.Append("                pair.Value."
                            + constructItem.ItemCode + parseValueStart + "TableReadBase.ParseString(pair.Value.ValueStr["
                            + dataIdx + "])" + parseValueEnd + ";");
                        break;
                    case ConstructConfig.ITEM_TYPE_BASE_BOOL:
                        parseBuilder.Append("                pair.Value."
                            + constructItem.ItemCode + parseValueStart + "TableReadBase.ParseBool(pair.Value.ValueStr["
                            + dataIdx + "])" + parseValueEnd + ";");
                        break;
                    case ConstructConfig.ITEM_TYPE_BASE_VECTOR3:
                        parseBuilder.Append("                pair.Value."
                            + constructItem.ItemCode + parseValueStart + "TableReadBase.ParseVector3(pair.Value.ValueStr["
                            + dataIdx + "])" + parseValueEnd + ";");
                        break;
                }
            }
            else if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM)
            {
                parseBuilder.Append("                pair.Value." + constructItem.ItemCode
                    + parseValueStart + " (" + constructItem.ItemType2[0].Name + ")TableReadBase.ParseInt(pair.Value.ValueStr["
                    + dataIdx + "])" + parseValueEnd + ";");
            }
            else if (constructItem.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID)
            {
                if (constructItem.ItemType2.Count == 1)
                {
                    parseBuilder.Append("                if (!string.IsNullOrEmpty(pair.Value.ValueStr[" + dataIdx + "]))\n");
                    parseBuilder.Append("                {\n");
                    //parseBuilder.Append("                    pair.Value.Step.Add(TableReader." + constructItem.ItemType2[0].Name + ".GetRecord(pair.Value.ValueStr[" + dataIdx + "]));\n");
                    parseBuilder.Append("                    pair.Value." + constructItem.ItemCode
                        + parseValueStart + " TableReader." + constructItem.ItemType2[0].Name + ".GetRecord(pair.Value.ValueStr["
                        + dataIdx + "])" + parseValueEnd + ";\n");
                    parseBuilder.Append("                }\n");
                    //parseBuilder.Append("                else\n");
                    //parseBuilder.Append("                {\n");
                    //parseBuilder.Append("                    pair.Value." + constructItem.ItemCode
                    //    + parseValueStart + "null" + parseValueEnd + ";\n");
                    //parseBuilder.Append("                }");
                }
                else
                {
                    parseBuilder.Append("                pair.Value." + constructItem.ItemCode
                        + parseValueStart + " TableReadBase.ParseMultiTable(pair.Value.ValueStr["
                        + dataIdx + "])" + parseValueEnd + ";");
                }
            }

            if (constructItem.ItemRepeat > 1)
            {
                //parseBuilder.Append("List<" + parseCode + ">");
            }
            parseBuilder.Append("\n");
            //parseBuilder.Append(parseCode);
            return parseBuilder;
        }

        private void CSCreateTableReaderFile(string enumPath)
        {
            var builder = new StringBuilder();
            builder.Append("using System.Collections;\n\n");
            builder.Append("namespace Tables\n");
            builder.Append("{\n");
            builder.Append("    public class TableReader\n");
            builder.Append("    {\n\n");
            builder.Append("        #region 唯一实例\n\n");
            builder.Append("        private TableReader() { }\n\n");
            builder.Append("        private TableReader _Instance = null;\n");
            builder.Append("        public TableReader Instance\n");
            builder.Append("        {\n");
            builder.Append("            get\n");
            builder.Append("            {\n");
            builder.Append("                if (_Instance == null)\n");
            builder.Append("                    _Instance = new TableReader();\n\n");
            builder.Append("                return _Instance;\n");
            builder.Append("            }\n");
            builder.Append("        }\n\n");
            builder.Append("        #endregion\n");

            builder.Append("        #region Logic\n\n");

            var readBuilder = new StringBuilder();
            var initBuilder = new StringBuilder();
            foreach (ConstructFile constructFile in ConstructFold.Instance.ConstructFiles)
            {
                builder.Append("//" + constructFile.Desc + "\n");
                builder.Append("        public static " + constructFile.Name
                    + " " + constructFile.Name + " { get; internal set; }\n");

                //readBuilder.Append("            " + constructFile.Name
                //    + " = new " + constructFile.Name + "(\"" + TableGlobalConfig.Instance.CodeTablePath + constructFile.Name + ".csv\");\n");
                string filePath = constructFile.Path.Replace("\\", "/") + constructFile.Name;
                readBuilder.Append("            " + constructFile.Name
                    + " = new " + constructFile.Name + "(TableReadBase.GetTableText(\"" + filePath + "\"), false);\n");

                initBuilder.Append("            " + constructFile.Name + ".CoverTableContent();\n");
            }
            builder.Append("\n");
            builder.Append("        public static void ReadTables()\n");
            builder.Append("        {\n");
            builder.Append("            //读取所有表\n");
            builder.Append(readBuilder);
            builder.Append("\n");
            builder.Append("            //初始化所有表\n");
            builder.Append(initBuilder);
            builder.Append("        }\n\n");
            builder.Append("        #endregion\n");
            builder.Append("    }\n");
            builder.Append("}\n");

            File.WriteAllText(enumPath, builder.ToString(), Encoding.UTF8);
        }

        #endregion
    }
}

