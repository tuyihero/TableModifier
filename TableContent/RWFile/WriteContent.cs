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
    public class WriteContent
    {
        public static void WriteAll()
        { 
            foreach(ContentFile contentFile in TableContentManager.Instance.ContentFiles)
            {
                WriteFile(contentFile);
            }
        }

        public static void WriteFile(ContentFile file)
        {
            if (!string.IsNullOrEmpty(file.ConstructFile.OldName))
            {
                string oldPath = TableGlobalConfig.Instance.ResTablePath + "/" + file.ConstructFile.OldName + ".csv";
                if (!string.IsNullOrEmpty(oldPath))
                {
                    File.Delete(oldPath);
                }
                file.WriteFlag = true;

                WriteConstruct.WriteFileOldName(file.ConstructFile, "");
            }

            if(!file.IsNeedWrite())
                return;

            string path = TableGlobalConfig.Instance.ResTablePath + "/" + file.ConstructFile.Name + ".csv";
            if (string.IsNullOrEmpty(path))
                return;

            File.Delete(path);

            using (StreamWriter SourceStream = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                CsvWriter writer = new CsvWriter(SourceStream);
                writer.WriteRecord(file.ConstructFile.GetColumnTitles());
                foreach (ContentRow row in file.ContentRow)
                {
                    writer.WriteRecord(row.GetItemsStr());
                }
            }

            file.AlreadyWrite();

        }

        public static void CreateFileNotExist()
        {
            foreach (ConstructFile constructFile in ConstructFold.Instance.ConstructFiles)
            {
                string path = TableGlobalConfig.Instance.ResTablePath + "/" + constructFile.Name + ".csv";
                if (File.Exists(path))
                    continue;

                ContentFile contentFile = new ContentFile(constructFile);
                contentFile.Name = constructFile.Name;

                contentFile.GreateRow("empty");

                TableContentManager.Instance.AddContentFile(contentFile);
            }
        }
    }
}
