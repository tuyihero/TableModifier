using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TableConstruct
{
    public class ReadConfig
    {
        public static void ReadAllFiles()
        {
            string configPath = TableGlobalDefine.CONFIG_FOLD_PATH;
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
                TableGlobalConfig.Instance.ProjectFileNames.Clear();
                return;
            }

            DirectoryInfo TheFolder = new DirectoryInfo(configPath);
            foreach (FileInfo fileInfo in TheFolder.GetFiles())
            {
                string[] fileNames = fileInfo.Name.Split('.');
                TableGlobalConfig.Instance.ProjectFileNames.Add(fileNames[0]);
            }
        }

        public static void ReadProjectConfig(string projectName)
        {
            string configPath = TableGlobalDefine.CONFIG_FOLD_PATH + "/" + projectName + ".xml";
            if (!File.Exists(configPath))
            {
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(configPath);

                XmlNode root = xmlDoc.SelectSingleNode(TableGlobalDefine.DOC_ROOT_STR);
                foreach (XmlElement childElement in root.ChildNodes.OfType<XmlElement>())
                {
                    if (childElement.Name == TableGlobalDefine.ELEMENT_TEMPLATE_PATH)
                    {
                        TableGlobalConfig.Instance.TemplatePath = childElement.InnerText;
                    }

                    if (childElement.Name == TableGlobalDefine.ELEMENT_CODE_PATH)
                    {
                        TableGlobalConfig.Instance.CodePath = childElement.InnerText;
                    }

                    if (childElement.Name == TableGlobalDefine.ELEMENT_CODE_TABLE_PATH)
                    {
                        TableGlobalConfig.Instance.ConstructTablePath = childElement.InnerText;
                    }

                    if (childElement.Name == TableGlobalDefine.ELEMENT_RES_TABLE_PATH)
                    {
                        TableGlobalConfig.Instance.ResTablePath = childElement.InnerText;
                    }

                    if (childElement.Name == TableGlobalDefine.ELEMENT_PROJECT_PATH)
                    {
                        TableGlobalConfig.Instance.ProjectPath = childElement.InnerText;
                    }
                }

            }
            catch (Exception e)
            {

            }
        }

    }
}
