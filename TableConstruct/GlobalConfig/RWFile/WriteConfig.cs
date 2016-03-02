using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TableConstruct
{
    public class WriteConfig
    {
        public static void WriteAll()
        {
            string configPath = TableGlobalDefine.CONFIG_FOLD_PATH + "/" + TableGlobalDefine.CONFIG_FILE_NAME + ".xml";
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(configPath))
            {
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(TableGlobalDefine.INIT_VERSION_STR, TableGlobalDefine.INIT_ENCODING_STR, null);
                xmlDoc.AppendChild(dec);
                XmlElement rootInit = xmlDoc.CreateElement(TableGlobalDefine.DOC_ROOT_STR);
                xmlDoc.AppendChild(rootInit);
                xmlDoc.Save(configPath);
            }
            else
            {
                xmlDoc.Load(configPath);
            }
            XmlNode root = xmlDoc.SelectSingleNode(TableGlobalDefine.DOC_ROOT_STR);
            root.RemoveAll();

            XmlElement xmlText = xmlDoc.CreateElement(TableGlobalDefine.ELEMENT_TEMPLATE_PATH);
            xmlText.InnerText = TableGlobalConfig.Instance.TemplatePath;
            root.AppendChild(xmlText);

            xmlText = xmlDoc.CreateElement(TableGlobalDefine.ELEMENT_CODE_PATH);
            xmlText.InnerText = TableGlobalConfig.Instance.CodePath;
            root.AppendChild(xmlText);

            xmlText = xmlDoc.CreateElement(TableGlobalDefine.ELEMENT_CODE_TABLE_PATH);
            xmlText.InnerText = TableGlobalConfig.Instance.CodeTablePath;
            root.AppendChild(xmlText);

            xmlText = xmlDoc.CreateElement(TableGlobalDefine.ELEMENT_RES_TABLE_PATH);
            xmlText.InnerText = TableGlobalConfig.Instance.ResTablePath;
            root.AppendChild(xmlText);

            xmlDoc.Save(configPath);
        }


    }
}
