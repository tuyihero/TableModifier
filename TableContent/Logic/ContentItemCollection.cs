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
    public class ContentItemCollection : TableBaseCollection
    {
        //维护一个索引
        private Dictionary<string, ContentItem> _ContentItemsDic = new Dictionary<string, ContentItem>();

        new public void AddNewItem(ContentItem contentItem)
        {
            base.AddNewItem(contentItem);

            _ContentItemsDic.Add(contentItem.ItemConstructName, contentItem);
        }

        public ContentItem GetItemByName(string name)
        {
            if (_ContentItemsDic.ContainsKey(name))
            {
                return _ContentItemsDic[name];
            }
            return null;
        }
    }
}
