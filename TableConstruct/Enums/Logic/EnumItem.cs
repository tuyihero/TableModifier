using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TableConstruct
{
    public class EnumItem : TableBaseItem, ITableDisplay
    {
        public EnumItem()
        {
        }

        #region 显示属性接口
        private string _ItemCode;
        private string _ItemValue;
        private string _ItemDesc;

        [StringEmpty]
        [StringNotDefault]
        public String ItemCode
        {
            get { return _ItemCode; }
            set
            {
                _ItemCode = value;
                OnPropertyChanged("ItemCode");
            }
        }

        [StringEmpty]
        public String ItemValue
        {
            get { return _ItemValue; }
            set
            {
                var enumCollection = ParentCollection as EnumItemCollection;
                if (enumCollection != null)
                {
                    enumCollection.ModifyKey(_ItemCode, value);
                }

                _ItemValue = value;
                OnPropertyChanged("ItemValue");
            }
        }

        public String ItemDesc
        {
            get { return _ItemDesc; }
            set
            {
                _ItemDesc = value;
                OnPropertyChanged("ItemDesc");
            }
        }

        #endregion

        #region 特殊属性

        public bool ItemReadOnly = false;

        public EnumInfo _BelongEnumInfo = null;
        public EnumInfo BelongEnumInfo 
        {
            get
            {
                return _BelongEnumInfo;
            }
              
        }

        #endregion

        #region 接口ITableDisplay

        public string DisplayName 
        {
            get
            {
                return Name;
            }
        }

        public string DisplayTips 
        {
            get
            {
                return _ItemDesc;
            }
        }

        public string WriteValue 
        {
            get
            {
                return _ItemValue;
            }
        }

        #endregion
    }
}
