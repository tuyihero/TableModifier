using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TableConstruct
{
    public class InnerStructInfo : TableBaseItem
    {
        #region Logic

        public TableBaseCollection _InnerStructItemCollection = new TableBaseCollection();

        public InnerStructItem CreateItem()
        {
            return CreateItem("newItem");
        }

        public InnerStructItem CreateItem(string name)
        {
            InnerStructItem item = new InnerStructItem();
            item.Name = name;
            item.ItemType2.Add(new TableBaseItem());
            _InnerStructItemCollection.AddNewItem(item);

            return item;
        }

        public void AddItem(InnerStructItem item)
        {
            _InnerStructItemCollection.AddNewItem(item);
        }
        #endregion
    }
}
