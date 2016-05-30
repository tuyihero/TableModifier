using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TableConstruct
{
    public class ReadEnuminfos
    {
        public static void ReadProject()
        {
            string foldPath = TableGlobalConfig.Instance.ConstructTablePath + EnuminfoConfig.ENUMINFO_FOLD_PATH;
            if (!Directory.Exists(foldPath))
            {
                Directory.CreateDirectory(foldPath);
            }

            DirectoryInfo TheFolder = new DirectoryInfo(foldPath);
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
                EnumInfo enumInfo = new EnumInfo();
                enumInfo.Name = fileName;
                EnumManager.Instance.AddEnum(enumInfo);

                XmlNode root = xmlDoc.SelectSingleNode(EnuminfoConfig.DOC_ROOT_STR);
                foreach (XmlElement childElement in root.ChildNodes.OfType<XmlElement>())
                {
                    if (childElement.Name == EnuminfoConfig.ENUMINFO_ELEMENT_COLUMN)
                    {
                        EnumItem item = ReadColumn(childElement);
                        if (item != null)
                        {
                            enumInfo.AddEnumItem(item);
                        }

                        //重置写标记
                        item.WriteFlag = false;
                    }
                    
                }

                //重置写标记
                enumInfo.WriteFlag = false;

            }
            catch (Exception e)
            {

            }
        }

        public static EnumItem ReadColumn(XmlElement columnEle)
        {
            EnumItem item = new EnumItem();
            foreach (XmlElement childElement in columnEle.ChildNodes.OfType<XmlElement>())
            {
                if (childElement.Name == EnuminfoConfig.ENUMINFO_ELEMENT_CODE)
                    item.ItemCode = childElement.InnerText;

                if (childElement.Name == EnuminfoConfig.ENUMINFO_ELEMENT_NAME)
                    item.Name = childElement.InnerText;

                if (childElement.Name == EnuminfoConfig.ENUMINFO_ELEMENT_VALUE)
                    item.ItemValue = childElement.InnerText;

                if (childElement.Name == EnuminfoConfig.ENUMINFO_ELEMENT_DESC)
                    item.ItemDesc = childElement.InnerText;
            }

            if (string.IsNullOrEmpty(item.Name))
                return null;

            return item;
        }
    }


}
