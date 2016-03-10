using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace TableConstruct
{
    public class ConstructItem : TableBaseItem
    {
        public ConstructItem(string name)
        {
            Name = name;
        }

        #region 显示属性接口
        private string _ItemCode;
        private string _ItemDefault;
        private int _ItemRepeat = 1;

        [StringEmpty]
        public String ItemCode
        {
            get { return _ItemCode; }
            set
            {
                _ItemCode = value;
                OnPropertyChanged("ItemCode");
            }
        }

        public String ItemDefault
        {
            get { return _ItemDefault; }
            set
            {
                _ItemDefault = value;
                OnPropertyChanged("ItemDefault");
            }
        }

        public int ItemRepeat
        {
            get { return _ItemRepeat; }
            set
            {
                _ItemRepeat = value;
                if (_ItemRepeat < 1)
                    _ItemRepeat = 1;
                OnPropertyChanged("ItemReapet");
            }
        }

        //类型
        private string _ItemType1;
        private TableBaseCollection _ItemType2 = new TableBaseCollection();

        [StringEmpty]
        public String ItemType1
        {
            get { return _ItemType1; }
            set
            {
                _ItemType1 = value;
                OnPropertyChanged("ItemType1");

                //ModifyItemType2Items();
            }
        }

        public TableBaseCollection ItemType2
        {
            get { return _ItemType2; }
            set
            {
                _ItemType2 = value;
                OnPropertyChanged("ItemType2");
            }
        }

        #endregion

        #region 特殊属性

        public bool ItemReadOnly = false;

        #endregion
    }
}
