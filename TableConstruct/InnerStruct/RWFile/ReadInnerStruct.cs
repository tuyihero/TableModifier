using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TableConstruct
{
    public class ReadInnerStruct
    {
        public static void ReadAll()
        {
            if (!Directory.Exists(InnerStructConfig.INNERSTRUCT_FOLD_PATH))
            {
                Directory.CreateDirectory(InnerStructConfig.INNERSTRUCT_FOLD_PATH);
            }

            DirectoryInfo TheFolder = new DirectoryInfo(InnerStructConfig.INNERSTRUCT_FOLD_PATH);
            foreach (FileInfo fileInfo in TheFolder.GetFiles())
            {
                if (fileInfo.Extension == ".xml")
                    ReadFile(fileInfo);
            }
        }

        public static void ReadFile(FileInfo fileInfo)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(fileInfo.FullName);

                string fileName = fileInfo.Name.Remove(fileInfo.Name.LastIndexOf("."));
                InnerStructInfo enumInfo = new InnerStructInfo();
                enumInfo.Name = fileName;
                InnerStructManager.Instance.AddEnum(enumInfo);

                XmlNode root = xmlDoc.SelectSingleNode(InnerStructConfig.DOC_ROOT_STR);
                foreach (XmlElement childElement in root.ChildNodes.OfType<XmlElement>())
                {
                    if (childElement.Name == InnerStructConfig.INNERSTRUCT_ELEMENT_COLUMN)
                    {
                        InnerStructItem item = ReadColumn(childElement);
                        if (item != null)
                        {
                            enumInfo.AddItem(item);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static InnerStructItem ReadColumn(XmlElement columnEle)
        {
            InnerStructItem item = new InnerStructItem();
            foreach (XmlElement childElement in columnEle.ChildNodes.OfType<XmlElement>())
            {
                if (childElement.Name == InnerStructConfig.INNERSTRUCT_ELEMENT_NAME)
                {
                    item.Name = childElement.InnerText;
                }

                if (childElement.Name == InnerStructConfig.INNERSTRUCT_ELEMENT_CODE)
                    item.ItemCode = childElement.InnerText;

                if (childElement.Name == InnerStructConfig.INNERSTRUCT_ELEMENT_TYPE1)
                    item.ItemType1 = childElement.InnerText;

                if (childElement.Name == InnerStructConfig.INNERSTRUCT_ELEMENT_TYPE2)
                {
                    foreach (XmlElement type2Ele in childElement.ChildNodes.OfType<XmlElement>())
                    {
                        item.ItemType2.AddNewItem(new TableBaseItem() { Name = type2Ele.InnerText });
                    }
                }

                if (childElement.Name == InnerStructConfig.INNERSTRUCT_ELEMENT_DEFAULT)
                    item.ItemDefault = childElement.InnerText;

                if (childElement.Name == InnerStructConfig.INNERSTRUCT_ELEMENT_REPEAT)
                    item.ItemRepeat = int.Parse(childElement.InnerText);
            }

            if (string.IsNullOrEmpty(item.Name))
                return null;

            return item;
        }
    }


}
