using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace TableConstruct
{
    public class TableBaseCollection : ObservableCollection<TableBaseItem>
    {
        #region 属性

        public bool _WriteFlag = false;
        public bool WriteFlag
        {
            get
            {
                return _WriteFlag;
            }
            set
            {
                _WriteFlag = value;

                if (!_WriteFlag)
                {
                    this.ForEach((item) => { item._WriteFlag = false; });
                }
            }
        }

        #endregion
        #region 私有
        private bool _IsInitField = false;

        private FieldInfo _NameField = null;
        private FieldInfo _IdField = null;
        #endregion

        #region

        public void AddNewItem(TableBaseItem item)
        {
            Add(item);
            item.ParentCollection = this;

            if (!_IsInitField)
            {
                Type type = Items[0].GetType();
                FieldInfo[] fieldInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fieldInfos)
                {
                    if (field.Name == "_Name")
                        _NameField = field;

                    if (field.Name == "_Id")
                        _IdField = field;
                }
                _IsInitField = true;
            }

            WriteFlag = true;
        }

        public void ForEach(Action<TableBaseItem> action)
        {
            foreach (var link in Items)
            {
                action(link);
            }
        }

        public TableBaseItem GetByName(string name)
        {
            if (_NameField == null)
                return null;
                
            foreach (var item in Items)
            {
                string itemName = _NameField.GetValue(item) as string;
                if (itemName == name)
                {
                    return item;
                }
            }
            
            return null;
        }

        public void RemoveByName(string name)
        {
            if (_NameField == null)
                return;

            foreach (var item in Items)
            {
                string itemName = _NameField.GetValue(item) as string;

                if (itemName == name)
                {
                    Remove(item);
                    break;
                }
            }

            WriteFlag = true;
        }

        public void MoveItemToPos(TableBaseItem item, int pos)
        {
            if (item != null)
            {
                if (Items.Remove(item))
                {
                    Items.Insert(pos, item);
                }
            }

            WriteFlag = true;
        }

        public void MovePosToPos(int orgPos, int destPos)
        {
            Move(orgPos, destPos);
            WriteFlag = true;
        }

        public void RemoveLast()
        {
            Items.Remove(Items.Last<TableBaseItem>());

            WriteFlag = true;
        }
        #endregion
    }
}
