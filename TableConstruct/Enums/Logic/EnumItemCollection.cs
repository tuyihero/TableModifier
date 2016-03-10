using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace TableConstruct
{
    public class EnumItemCollection : TableBaseCollection
    {

        //维护一个索引
        private Dictionary<string, EnumItem> _ContentFilesDic = new Dictionary<string, EnumItem>();

        new public void AddNewItem(EnumItem contentFile)
        {
            base.AddNewItem(contentFile);

            _ContentFilesDic.Add(contentFile.ItemValue, contentFile);
        }

        public EnumItem GetItemByValue(string itemValue)
        {
            if (_ContentFilesDic.ContainsKey(itemValue))
            {
                return _ContentFilesDic[itemValue];
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

        public int GetNewItemValue()
        {
            int maxValue = 0;
            int itemValue = 0;
            foreach (var pair in _ContentFilesDic)
            {
                if (int.TryParse(pair.Value.ItemValue, out itemValue))
                {
                    if (itemValue > maxValue)
                    {
                        maxValue = itemValue;
                    }
                }
            }

            return maxValue + 1;
        }
    }
}
