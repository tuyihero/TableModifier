using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TableConstruct
{
    public class WriteInnerStruct
    {
        public static void WriteAll()
        {
            //ConstructFold.Instance.RemoveFiles.ForEach((file) =>
            //{
            //    DeleteFile(file);
            //});

            InnerStructManager.Instance.InnerStructInfoCollection.ForEach((file) =>
            {
                WriteFile(file as InnerStructInfo);
            });
        }

        public static void DeleteFile(string name)
        {
            string configPath = InnerStructConfig.INNERSTRUCT_FOLD_PATH + "/" + name + ".xml";
            File.Delete(configPath);
        }

        //public static void WriteFile(string name)
        //{
        //    ConstructFile file = ConstructFold.Instance.ConstructFiles.GetByName(name);
        //    if (file != null)
        //    {
        //        WriteFile(file);
        //    }
        //}

        public static void WriteFile(InnerStructInfo file)
        {
            if (file == null)
                return;

            string configPath = InnerStructConfig.INNERSTRUCT_FOLD_PATH + "/" + file.Name + ".xml";
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(configPath))
            {
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(InnerStructConfig.INIT_VERSION_STR, InnerStructConfig.INIT_ENCODING_STR, null);
                xmlDoc.AppendChild(dec);
                XmlElement rootInit = xmlDoc.CreateElement(InnerStructConfig.DOC_ROOT_STR);
                xmlDoc.AppendChild(rootInit);
                xmlDoc.Save(configPath);
            }
            else
            {
                xmlDoc.Load(configPath);
            }
            XmlNode root = xmlDoc.SelectSingleNode(InnerStructConfig.DOC_ROOT_STR);
            root.RemoveAll();

            XmlElement xmlItem = null;
            foreach (InnerStructItem item in file._InnerStructItemCollection)
            {
                XmlElement xmlColumn = xmlDoc.CreateElement(InnerStructConfig.INNERSTRUCT_ELEMENT_COLUMN);

                xmlItem = xmlDoc.CreateElement(InnerStructConfig.INNERSTRUCT_ELEMENT_NAME);
                xmlItem.InnerText = item.Name;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(InnerStructConfig.INNERSTRUCT_ELEMENT_CODE);
                xmlItem.InnerText = item.ItemCode;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(InnerStructConfig.INNERSTRUCT_ELEMENT_TYPE1);
                xmlItem.InnerText = item.ItemType1;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(InnerStructConfig.INNERSTRUCT_ELEMENT_TYPE2);
                foreach (TableBaseItem type2s in item.ItemType2)
                {
                    XmlElement type2Ele = xmlDoc.CreateElement(InnerStructConfig.INNERSTRUCT_ELEMENT_TYPE2);
                    type2Ele.InnerText = type2s.Name;
                    xmlItem.AppendChild(type2Ele);
                }
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(InnerStructConfig.INNERSTRUCT_ELEMENT_DEFAULT);
                xmlItem.InnerText = item.ItemDefault;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(InnerStructConfig.INNERSTRUCT_ELEMENT_REPEAT);
                xmlItem.InnerText = item.ItemRepeat.ToString();
                xmlColumn.AppendChild(xmlItem);

                root.AppendChild(xmlColumn);
            }
            xmlDoc.Save(configPath);
        }
    }
}
