using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TableConstruct
{
    public class WriteEnuminfos
    {
        public static void WriteProject()
        {
            EnumManager.Instance.RemoveFiles.ForEach((file) =>
            {
                DeleteFile(file);
            });

            EnumManager.Instance.EnumInfoCollection.ForEach((file) =>
            {
                WriteFile(file as EnumInfo);
            });
        }

        public static void DeleteFile(string name)
        {
            string configPath = TableGlobalConfig.Instance.SelectedProject + EnuminfoConfig.ENUMINFO_FOLD_PATH + "/" + name + ".xml";
            File.Delete(configPath);
        }


        public static void WriteFile(EnumInfo file)
        {
            if (file == null)
                return;

            if (!file.IsNeedWrite())
                return;

            string configPath = TableGlobalConfig.Instance.ConstructTablePath + EnuminfoConfig.ENUMINFO_FOLD_PATH + "/" + file.Name + ".xml";
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(configPath))
            {
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(EnuminfoConfig.INIT_VERSION_STR, EnuminfoConfig.INIT_ENCODING_STR, null);
                xmlDoc.AppendChild(dec);
                XmlElement rootInit = xmlDoc.CreateElement(EnuminfoConfig.DOC_ROOT_STR);
                xmlDoc.AppendChild(rootInit);
                xmlDoc.Save(configPath);
            }
            else
            {
                xmlDoc.Load(configPath);
            }
            XmlNode root = xmlDoc.SelectSingleNode(EnuminfoConfig.DOC_ROOT_STR);
            root.RemoveAll();

            XmlElement xmlItem = null;
            foreach (EnumItem item in file._EnumItemCollection)
            {
                XmlElement xmlColumn = xmlDoc.CreateElement(EnuminfoConfig.ENUMINFO_ELEMENT_COLUMN);

                xmlItem = xmlDoc.CreateElement(EnuminfoConfig.ENUMINFO_ELEMENT_NAME);
                xmlItem.InnerText = item.Name;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(EnuminfoConfig.ENUMINFO_ELEMENT_CODE);
                xmlItem.InnerText = item.ItemCode;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(EnuminfoConfig.ENUMINFO_ELEMENT_VALUE);
                xmlItem.InnerText = item.ItemValue;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(EnuminfoConfig.ENUMINFO_ELEMENT_DESC);
                xmlItem.InnerText = item.ItemDesc;
                xmlColumn.AppendChild(xmlItem);

                root.AppendChild(xmlColumn);
            }
            xmlDoc.Save(configPath);

            file.AlreadyWrite();
        }
    }
}
