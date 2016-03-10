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
    public class ContentRowCollection : TableBaseCollection
    {
        //维护一个索引
        private Dictionary<string, ContentRow> _ContentFilesDic = new Dictionary<string, ContentRow>();

        public void AddNewItem(ContentRow contentRow)
        {
            base.AddNewItem(contentRow);

            if (ContentConfig.IsContentIDInvalid(contentRow.ID))
            {
                _ContentFilesDic.Add(contentRow.ID, contentRow);
            }
        }

        public ContentRow GetRowByID(string id)
        {
            if (_ContentFilesDic.ContainsKey(id))
            {
                return _ContentFilesDic[id];
            }
            return null;
        }

        public bool ContainsID(string id)
        {
            return _ContentFilesDic.ContainsKey(id);
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

        new public void Clear()
        {
            base.Clear();
            _ContentFilesDic.Clear();
        }
    }
}
