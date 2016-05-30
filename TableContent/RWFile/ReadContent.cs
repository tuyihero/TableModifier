using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Kent.Boogaart.KBCsv;
using TableConstruct;

namespace TableContent
{
    public class ReadContent
    {
        public static void ReadAll()
        {
            //先保证初始化了表结构
            ConstructFold.Instance.InitFold();

            foreach (ConstructFile file in ConstructFold.Instance.ConstructFiles)
            {
                ReadContentFile(file);
            }
        }

        public static void ReLoadNeed()
        {
            //先保证初始化了表结构
            ConstructFold.Instance.InitFold();

            foreach (var file in ConstructFold.Instance.RemoveFiles)
            {
                TableContentManager.Instance.ContentFiles.RemoveByName(file);
            }

            bool ifAnyReload = false;
            foreach (ConstructFile file in ConstructFold.Instance.ConstructFiles)
            {
                if (file.NeedReloadContent)
                {
                    ReadContentFile(file);
                    file.NeedReloadContent = false;
                    ifAnyReload = true;
                }
            }

            if (ifAnyReload)
            {
                if (ContentFileList.Instance != null)
                {
                    ContentFileList.Instance.Refresh();
                }
            }
        }

        public static void ReadContentFile(ConstructFile construct)
        {
             ContentFile contentFile = new ContentFile(construct);
            contentFile.Name = construct.Name;
            contentFile.IsInit = true;
            contentFile.Path = construct.Path;

            ReadContentFile(construct, ref contentFile);
        }

        public static void ReadContentFile(ConstructFile construct, ref ContentFile contentFile)
        {
            string path = TableGlobalConfig.Instance.ResTablePath + "\\" + construct.Path + construct.Name + ".csv";
            if (!string.IsNullOrEmpty(construct.OldName))
            {
                path = TableGlobalConfig.Instance.ResTablePath + "\\" + construct.Path + construct.OldName + ".csv";
            }
            
            if (!File.Exists(path))
            {
                CreateNewFile(construct);
                return;
            }

            string[] lines = File.ReadAllLines(path, Encoding.Default);
            if (lines.Length == 0)
                return;

            contentFile.ContentRow.Clear();
            lines[0] = lines[0].Replace("\r\n", "\n");
            StringReader rdr = new StringReader(string.Join("\n", lines));
            using (var reader = new CsvReader(rdr))
            {
                HeaderRecord header = reader.ReadHeaderRecord();
                //string curGourpName = "";
                //string curClassName = "";

               

                var columnTitles = construct.GetColumnTitles();
                while (reader.HasMoreRecords)
                {
                    DataRecord data = reader.ReadDataRecord();
                    //if (data[0].StartsWith("###"))
                    //{
                    //    curGourpName = data[0].TrimStart('#');
                    //    continue;
                    //}

                    //if (data[0].StartsWith("##"))
                    //{
                    //    curClassName = data[0].TrimStart('#');
                    //    continue;
                    //}

                    ContentRow clumn = ReadRow(construct, data, contentFile);
                    contentFile.AddContentRow(clumn);
                }

                if (IsHeaderMatch(construct, header))
                {
                    contentFile.WriteFlag = false;
                }
                else
                {
                    contentFile.WriteFlag = true;
                }
                contentFile.IsInit = false;
                TableContentManager.Instance.AddContentFile(contentFile);
            }
        }

        public static ContentRow ReadRow(ConstructFile construct, DataRecord data, ContentFile contentFile)
        {
            ContentRow row = new ContentRow(contentFile);
            try
            {
                row.IsInit = true;
                foreach (ConstructItem constructItem in construct.ConstructItems)
                {
                    ReadItem(constructItem, data, ref row);
                }
                row.IsInit = false;
                //if (row.ContentItems.Count != 0)
                //{
                //    row.Name = row.ContentItems[0].Value.ToString();
                //}
            }
            catch (Exception e)
            {
                throw new Exception("read exception:" + e);
            }
            row.WriteFlag = false;
            row._ContentItems.WriteFlag = false;
            return row;
        }

        public static void ReadItem(ConstructItem constructItem, DataRecord data, ref ContentRow row)
        {
            for (int i = 0; i < constructItem.ItemRepeat; ++i)
            {
                ContentItem contentItem = new ContentItem(row, constructItem, i + 1);
                string columnName = constructItem.Name;
                if (constructItem.ItemRepeat > 1)
                {
                    columnName = (constructItem.Name + (i + 1).ToString());
                }
                columnName = "\"" + columnName + "\"";
                if (data.HeaderRecord.Contains(columnName))
                {
                    contentItem.Value = data[columnName];
                }
                else
                {
                    contentItem.Value = "";
                }

                row.AddContentItem(contentItem);
            }
        }

        public static void CreateNewFile(ConstructFile construct)
        {
            ContentFile contentFile = new ContentFile(construct);
            contentFile.Name = construct.Name;
            contentFile.Path = construct.Path;

            //ContentRow row = new ContentRow(contentFile);
            //foreach (ConstructItem constructItem in construct.ConstructItems)
            //{
            //    for (int i = 0; i < constructItem.ItemRepeat; ++i)
            //    {
            //        ContentItem contentItem = new ContentItem(row, constructItem, i + 1);
            //        if (constructItem.ItemCode == ConstructConfig.NEW_FILE_DEFAULT_ID_CODE)
            //        {
            //            contentItem.Value = ContentConfig.CONTENT_INIT_DEFAULT_ID;
            //        }
            //        else
            //        {
            //            contentItem.Value = "";
            //        }
            //        row.AddContentItem(contentItem);
            //    }
            //}

            //contentFile.AddContentRow(row);

            TableContentManager.Instance.AddContentFile(contentFile);
        }

        private static bool IsHeaderMatch(ConstructFile constructFile, HeaderRecord fileHeader)
        {
            var titles = constructFile.GetColumnTitles();
            for (int i = 0; i < fileHeader.Count; ++i)
            {
                if (i < titles.Length)
                {
                    if (titles[i] != fileHeader[i])
                        return false;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
