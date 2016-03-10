using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TableConstruct
{
    public class EnumInfo : TableBaseItem
    {
        #region 属性

        private string _Desc;
        public string Desc
        {
            get
            {
                return _Desc;
            }
            set
            {
                _Desc = value;
            }
        }

        #endregion

        #region Logic

        public EnumItemCollection _EnumItemCollection = new EnumItemCollection();

        public void CreateEnumItem()
        {
            _EnumItemCollection.AddNewItem(new EnumItem() { ItemValue = _EnumItemCollection.GetNewItemValue().ToString() });
        }

        public void AddEnumItem(EnumItem enumItem)
        {
            _EnumItemCollection.AddNewItem(enumItem);
        }

        public EnumItem GetEnumItemByValue(string itemValue)
        {
            return _EnumItemCollection.GetItemByValue(itemValue);
        }

        public bool IsNeedWrite()
        {
            return _EnumItemCollection.WriteFlag | WriteFlag;
        }

        public void AlreadyWrite()
        {
            WriteFlag = false;
            _EnumItemCollection.WriteFlag = false;
        }
        #endregion
    }
}
