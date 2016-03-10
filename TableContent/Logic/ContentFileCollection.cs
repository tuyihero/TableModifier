using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

using TableConstruct;

namespace TableContent
{
    public class ContentFileCollection : TableBaseCollection
    {
        //维护一个索引
        private Dictionary<string, ContentFile> _ContentFilesDic = new Dictionary<string, ContentFile>();

        new public void AddNewItem(ContentFile contentFile)
        {
            base.RemoveByName(contentFile.Name);
            base.AddNewItem(contentFile);

            if (_ContentFilesDic.ContainsKey(contentFile.Name))
                _ContentFilesDic.Remove(contentFile.Name);

            _ContentFilesDic.Add(contentFile.Name, contentFile);
        }

        public ContentFile GetFileByName(string name)
        {
            if (_ContentFilesDic.ContainsKey(name))
            {
                return _ContentFilesDic[name];
            }
            return null;
        }

        public void ModifyKey(string oldValue, string newValue)
        {
            if (_ContentFilesDic.ContainsKey(oldValue))
            {
                var contentFile = _ContentFilesDic[oldValue];
                _ContentFilesDic.Remove(oldValue);
                _ContentFilesDic.Add(newValue, contentFile);
            }
        }
    }
}
