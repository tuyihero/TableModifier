using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace KeyChanger
{
    public class RWFile
    {
        public const string KEY_CHANGER_FOLD = "KeyChanger";
        public const string KEY_CHANGER_CONFIG_FILE = "KeyChangerConfig";

        //新建XML信息
        public const string INIT_VERSION_STR = "1.0";
        public const string INIT_ENCODING_STR = "utf-8";
        public const string DOC_ROOT_STR = "root";

        public const string KEY_GROUP = "KeyGroup";
        public const string KEY_CHANGE_FROM = "KeyChangeFrom";
        public const string KEY_CHANGE_TO = "KeyChangeTo";
        public const string KEY_STORE_INFO = "KeyStoreInfo";
        public const string KEY_STORE_TYPE = "KeyStoreType";
        public const string KEY_STORE_VALUE = "KeyStoreValue";

        public enum KEY_TYPE
        {
            KEYBOARD = 1,
            JOYSTICK = 2
        }

        public static void WriteFile()
        {
            string configPath = KEY_CHANGER_FOLD + "/" + KEY_CHANGER_CONFIG_FILE + ".xml";
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(configPath))
            {
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(INIT_VERSION_STR, INIT_ENCODING_STR, null);
                xmlDoc.AppendChild(dec);
                XmlElement rootInit = xmlDoc.CreateElement(DOC_ROOT_STR);
                xmlDoc.AppendChild(rootInit);
                xmlDoc.Save(configPath);
            }
            else
            {
                xmlDoc.Load(configPath);
            }
            XmlNode root = xmlDoc.SelectSingleNode(DOC_ROOT_STR);
            root.RemoveAll();

            foreach (KeyChangeItem item in KeyChangeManager.Instance.KeyChangeItemCollection)
            {
                WriteItem(item, ref xmlDoc, ref root);
            }
            xmlDoc.Save(configPath);
        }

        public static void WriteItem(KeyChangeItem keyItem, ref XmlDocument xmlDoc, ref XmlNode xmlNode)
        {
            XmlElement xmlGroup = xmlDoc.CreateElement(KEY_GROUP);

            XmlElement xmlFrom = xmlDoc.CreateElement(KEY_CHANGE_FROM);
            WriteStore(keyItem.FromStores, ref xmlDoc, ref xmlFrom);
            xmlGroup.AppendChild(xmlFrom);

            XmlElement xmlTo = xmlDoc.CreateElement(KEY_CHANGE_TO);
            WriteStore(keyItem.ToStores, ref xmlDoc, ref xmlTo);
            xmlGroup.AppendChild(xmlTo);

            xmlNode.AppendChild(xmlGroup);
        }

        public static void WriteStore(KeyStoreInfoCollection storeCollection, ref XmlDocument xmlDoc, ref XmlElement xmlItem)
        {
            foreach (KeyStoreInfo keyStore in storeCollection)
            {
                XmlElement xmlStore = xmlDoc.CreateElement(KEY_STORE_INFO);
                if (keyStore.IsJoystick() && keyStore._Joystick!= JoystickButtons.None)
                {
                    xmlStore.SetAttribute(KEY_STORE_TYPE, ((int)KEY_TYPE.JOYSTICK).ToString());
                    xmlStore.SetAttribute(KEY_STORE_VALUE, ((int)keyStore._Joystick).ToString());
                }
                else if (keyStore._Keyboard != Keys.NoName)
                {
                    xmlStore.SetAttribute(KEY_STORE_TYPE, ((int)KEY_TYPE.KEYBOARD).ToString());
                    xmlStore.SetAttribute(KEY_STORE_VALUE, ((int)keyStore._Keyboard).ToString());
                }
                xmlItem.AppendChild(xmlStore);
            }
        }

        public static void ReadFile()
        {
            if (!Directory.Exists(KEY_CHANGER_FOLD))
            {
                Directory.CreateDirectory(KEY_CHANGER_FOLD);
            }

            KeyChangeManager.Instance.KeyChangeItemCollection.Clear();

            string configPath = KEY_CHANGER_FOLD + "/" + KEY_CHANGER_CONFIG_FILE + ".xml";
            if (!File.Exists(configPath))
            {
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configPath);

            XmlNode root = xmlDoc.SelectSingleNode(DOC_ROOT_STR);
            foreach (XmlElement childElement in root.ChildNodes.OfType<XmlElement>())
            {
                if (childElement.Name == KEY_GROUP)
                {
                    ReadGroup(childElement);
                }
            }
        }

        public static void ReadGroup(XmlElement xmlElement)
        {
            KeyChangeItem newItem = KeyChangeManager.Instance.NewChange();
            foreach (XmlElement childElement in xmlElement.ChildNodes.OfType<XmlElement>())
            {
                if (childElement.Name == KEY_CHANGE_FROM)
                {
                    newItem.FromStores = ReadStore(childElement);
                }
                else if (childElement.Name == KEY_CHANGE_TO)
                {
                    newItem.ToStores = ReadStore(childElement);
                }
            }
        }

        public static KeyStoreInfoCollection ReadStore(XmlElement xmlElement)
        {
            KeyStoreInfoCollection storeCollection = new KeyStoreInfoCollection();
            foreach (XmlElement childElement in xmlElement.ChildNodes.OfType<XmlElement>())
            {
                if (childElement.Name == KEY_STORE_INFO)
                {
                    try
                    {
                        KeyStoreInfo keyStore = new KeyStoreInfo();
                        switch((KEY_TYPE)int.Parse(childElement.GetAttribute(KEY_STORE_TYPE)))
                        {
                            case KEY_TYPE.KEYBOARD:
                                keyStore.SetStoreValue((Keys)int.Parse(childElement.GetAttribute(KEY_STORE_VALUE)));
                                break;
                            case KEY_TYPE.JOYSTICK:
                                keyStore.SetStoreValue((JoystickButtons)int.Parse(childElement.GetAttribute(KEY_STORE_VALUE)));
                                break;
                        }
                        storeCollection.Add(keyStore);
                    }
                    catch(Exception e)
                    {

                    }
                }
            }
            return storeCollection;
        }

    }
}
