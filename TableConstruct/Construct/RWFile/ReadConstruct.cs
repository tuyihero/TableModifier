﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TableConstruct
{
    public class ReadConstruct
    {
        private static string _ConstructPath = TableGlobalConfig.Instance.ConstructTablePath + ConstructConfig.CONSTUCT_FOLD_PATH;

        public static void ReadProject()
        {
            //string constructPath = TableGlobalConfig.Instance.ConstructTablePath + ConstructConfig.CONSTUCT_FOLD_PATH;
            if (!Directory.Exists(_ConstructPath))
            {
                Directory.CreateDirectory(_ConstructPath);
            }

            DirectoryInfo TheFolder = new DirectoryInfo(_ConstructPath);
            foreach (FileInfo fileInfo in TheFolder.GetFiles())
            {
                if (fileInfo.Extension == ".xml")
                    ReadFile(fileInfo);
            }

            foreach (DirectoryInfo directInfo in TheFolder.GetDirectories())
            {
                ReadDirect(directInfo);
            }
        }

        public static void ReadDirect(DirectoryInfo directInfo)
        {
            ConstructFile file = new ConstructFile(directInfo.Name, false);

            foreach (FileInfo fileInfo in directInfo.GetFiles())
            {
                if (fileInfo.Extension == ".xml")
                    ReadFile(fileInfo);
            }

            foreach (DirectoryInfo direct in directInfo.GetDirectories())
            {
                ReadDirect(direct);
            }
        }

        public static void ReadFile(FileInfo fileInfo)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(fileInfo.FullName);

                string fileName = fileInfo.Name.Remove(fileInfo.Name.LastIndexOf("."));
                ConstructFile file = new ConstructFile(fileName, false);
                ConstructFold.Instance.AddFile(file);

                file.Path = fileInfo.FullName.Replace(_ConstructPath, "").Replace(fileInfo.Name, "");

                XmlNode root = xmlDoc.SelectSingleNode(ConstructConfig.DOC_ROOT_STR);
                foreach (XmlElement childElement in root.ChildNodes.OfType<XmlElement>())
                {
                    if (childElement.Name == ConstructConfig.CONSTRUCT_TABLE_CLASS)
                    {
                        file.Class = childElement.InnerText;
                    }
                    else if (childElement.Name == ConstructConfig.CONSTRUCT_TABLE_DESC)
                    {
                        file.Desc = childElement.InnerText;
                    }
                    else if (childElement.Name == ConstructConfig.CONSTRUCT_TABLE_OLD_NAME)
                    {
                        file.OldName = childElement.InnerText;
                    }
                    else if(childElement.Name == ConstructConfig.CONSTRUCT_ELEMENT_COLUMN)
                    {
                        ConstructItem item = ReadColumn(childElement);
                        if (item != null)
                        {
                            file.ConstructItems.AddNewItem(item);

                            //重置写标记
                            item.WriteFlag = false;
                        }
                        
                    }
                }

                //重置写标记
                file.ConstructItems.WriteFlag = false;
                file.WriteFlag = false;

            }
            catch (Exception e)
            {
                
            }
        }

        public static ConstructItem ReadColumn(XmlElement columnEle)
        {
            ConstructItem item = new ConstructItem("");
            foreach (XmlElement childElement in columnEle.ChildNodes.OfType<XmlElement>())
            {
                if (childElement.Name == ConstructConfig.CONSTRUCT_ELEMENT_NAME)
                {
                    item.Name = childElement.InnerText;

                    if (item.Name == ConstructConfig.NEW_FILE_DEFAULT_ID_NAME
                        || item.Name == ConstructConfig.NEW_FILE_DEFAULT_NAME_NAME
                        || item.Name == ConstructConfig.NEW_FILE_DEFAULT_DESC_NAME)
                    {
                        item.ItemReadOnly = true;
                    }
                }

                if (childElement.Name == ConstructConfig.CONSTRUCT_ELEMENT_CODE)
                    item.ItemCode = childElement.InnerText;

                if (childElement.Name == ConstructConfig.CONSTRUCT_ELEMENT_TYPE1)
                    item.ItemType1 = childElement.InnerText;

                if (childElement.Name == ConstructConfig.CONSTRUCT_ELEMENT_TYPE2)
                {
                    foreach (XmlElement type2Ele in childElement.ChildNodes.OfType<XmlElement>())
                    {
                        item.ItemType2.AddNewItem(new TableBaseItem() { Name = type2Ele.InnerText });
                    }
                }

                if (childElement.Name == ConstructConfig.CONSTRUCT_ELEMENT_DEFAULT)
                    item.ItemDefault = childElement.InnerText;

                if (childElement.Name == ConstructConfig.CONSTRUCT_ELEMENT_REPEAT)
                    item.ItemRepeat = int.Parse(childElement.InnerText);
            }

            if (string.IsNullOrEmpty(item.Name))
                return null;

            return item;
        }
    }
}
