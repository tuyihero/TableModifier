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
        public static void ReadAll()
        {
            string configPath = TableGlobalDefine.CONFIG_FOLD_PATH + "/" + TableGlobalDefine.CONFIG_FILE_NAME + ".xml";
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
                        TableGlobalConfig.Instance.CodeTablePath = childElement.InnerText;
                    }

                    if (childElement.Name == TableGlobalDefine.ELEMENT_RES_TABLE_PATH)
                    {
                        TableGlobalConfig.Instance.ResTablePath = childElement.InnerText;
                    }
                }

            }
            catch (Exception e)
            {

            }
        }

    }
}
