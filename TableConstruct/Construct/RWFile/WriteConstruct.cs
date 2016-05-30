using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TableConstruct
{
    public class WriteConstruct
    {
        private static string _ConfigPath = TableGlobalConfig.Instance.SelectedProject + ConstructConfig.CONSTUCT_FOLD_PATH;

        public static void WriteProject()
        {
            ConstructFold.Instance.RemoveFiles.ForEach((file) =>
            {
                DeleteFile(file);
            });

            ConstructFold.Instance.ConstructFiles.ForEach((file) =>
            {
                {
                    WriteFile(file as ConstructFile);
                }
            });
        }

        public static void DeleteFile(string name)
        {
            string configPath = _ConfigPath  + "\\" + name + ".xml";
            if(File.Exists(configPath))
                File.Delete(configPath);
        }

        public static void WriteFile(string name)
        {
            ConstructFile file = ConstructFold.Instance.ConstructFiles.GetByName(name) as ConstructFile;
            if (file != null)
            {
                WriteFile(file);
            }
        }

        public static void WriteDirectroy(ConstructFile file)
        {
            string[] pathNames = file.Path.Split('\\');

            for (int i = 0; i < pathNames.Length - 1; ++i)
            {
                if (string.IsNullOrEmpty(pathNames[i]))
                {
                    continue;
                }
            }
        }

        public static void WriteFile(ConstructFile file)
        {
            if (file == null)
                return;

            if (!file.IsNeedWrite())
                return;

            string configPath = TableGlobalConfig.Instance.ConstructTablePath + ConstructConfig.CONSTUCT_FOLD_PATH + file.Path + "\\" + file.Name + ".xml";
            string diretPath = TableGlobalConfig.Instance.ConstructTablePath + ConstructConfig.CONSTUCT_FOLD_PATH + file.Path;
            if (!Directory.Exists(diretPath))
            {
                Directory.CreateDirectory(diretPath);
            }

            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(configPath))
            {
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(ConstructConfig.INIT_VERSION_STR, ConstructConfig.INIT_ENCODING_STR, null);
                xmlDoc.AppendChild(dec);
                XmlElement rootInit = xmlDoc.CreateElement(ConstructConfig.DOC_ROOT_STR);
                xmlDoc.AppendChild(rootInit);
                xmlDoc.Save(configPath);
            }
            else
            {
                xmlDoc.Load(configPath);
            }
            XmlNode root = xmlDoc.SelectSingleNode(ConstructConfig.DOC_ROOT_STR);
            root.RemoveAll();

            //表格信息
            XmlElement xmlTableClass = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_TABLE_CLASS);
            xmlTableClass.InnerText = file.Class;
            root.AppendChild(xmlTableClass);

            XmlElement xmlTableDesc = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_TABLE_DESC);
            xmlTableDesc.InnerText = file.Desc;
            root.AppendChild(xmlTableDesc);

            XmlElement xmlTableOldName = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_TABLE_OLD_NAME);
            xmlTableOldName.InnerText = file.OldName;
            root.AppendChild(xmlTableOldName);

            //项信息
            XmlElement xmlItem = null;
            foreach (ConstructItem item in file.ConstructItems)
            {
                XmlElement xmlColumn = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_ELEMENT_COLUMN);

                xmlItem = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_ELEMENT_NAME);
                xmlItem.InnerText = item.Name;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_ELEMENT_CODE);
                xmlItem.InnerText = item.ItemCode;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_ELEMENT_TYPE1);
                xmlItem.InnerText = item.ItemType1;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_ELEMENT_TYPE2);
                foreach (TableBaseItem type2s in item.ItemType2)
                {
                    XmlElement type2Ele = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_ELEMENT_TYPE2);
                    type2Ele.InnerText = type2s.Name;
                    xmlItem.AppendChild(type2Ele);
                }
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_ELEMENT_DEFAULT);
                xmlItem.InnerText = item.ItemDefault;
                xmlColumn.AppendChild(xmlItem);

                xmlItem = xmlDoc.CreateElement(ConstructConfig.CONSTRUCT_ELEMENT_REPEAT);
                xmlItem.InnerText = item.ItemRepeat.ToString();
                xmlColumn.AppendChild(xmlItem);

                root.AppendChild(xmlColumn);
            }
            xmlDoc.Save(configPath);

            file.AlreadyWrite();
            file.NeedReloadContent = true;
        }

        public static void WriteFileOldName(ConstructFile file, string oldName)
        {
            if (file == null)
                return;

            string configPath = TableGlobalConfig.Instance.SelectedProject + ConstructConfig.CONSTUCT_FOLD_PATH + "/" + file.Name + ".xml";
            if (!File.Exists(configPath))
            {
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configPath);
            XmlNode root = xmlDoc.SelectSingleNode(ConstructConfig.DOC_ROOT_STR);

            //
            foreach (XmlElement childElement in root.ChildNodes.OfType<XmlElement>())
            {
                if (childElement.Name == ConstructConfig.CONSTRUCT_TABLE_OLD_NAME)
                {
                    childElement.InnerText = oldName;
                    break;
                }
            }

            xmlDoc.Save(configPath);
        }

    }
}
