using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableConstruct;

namespace TableContent
{
    public class ContentItem : TableBaseItem
    {
        #region 构造

        public ContentItem(ContentRow row, ConstructItem construct, int constructIdx)
        {
            _ItemConstruct = construct;
            _ContructRepeatIndex = constructIdx;

            ContentRow = row;
        }

        #endregion

        #region 展示

        private ConstructItem _ItemConstruct = null;
        public ConstructItem ItemConstruct
        {
            get 
            {
                return _ItemConstruct;
            }
        }

        private int _ContructRepeatIndex = 0;
        public int ConstructRepeatIndex
        {
            get { return _ContructRepeatIndex; }
            set { _ContructRepeatIndex = value; }
        }

        public string ItemConstructName
        {
            get 
            {
                if (ItemConstruct.ItemRepeat > 1)
                    return _ItemConstruct.Name + ConstructRepeatIndex.ToString();
                else
                    return _ItemConstruct.Name;
            }
        }

        private string _Value = null;
        //[ValidConstruct]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                string oldValue = _Value;
                _Value = value;
                OnPropertyChanged("Value");

                if (!ContentRow.IsInit)
                {
                    SpecilValueNotify(oldValue, _Value);
                    CheckValidate();
                }
            }
        }

        private ITableDisplay _DisplayInfo = null;
        public ITableDisplay DisplayInfo
        {
            get
            {
                SetDisaplyInfo();
                return _DisplayInfo;
            }
            set
            {
                _DisplayInfo = value;
                Value = _DisplayInfo.WriteValue;
                OnPropertyChanged("DisplayInfo");
            }
        }

        #endregion

        #region 逻辑
        public readonly ContentRow ContentRow;

        public void SetTableReference()
        {
            SetTableReference("", _Value);
        }

        public void SetTableReference(string oldValue, string newValue)
        {
            if (_ItemConstruct.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID)
            {
                if (_ItemConstruct.ItemType2.Count == 1)
                {
                    var contentFile = TableContentManager.Instance.GetFileByName(_ItemConstruct.ItemType2[0].Name);
                    if (contentFile == null)
                        return;

                    var row = contentFile.GetRowByID(newValue);
                    if (row == null)
                    {
                        return;
                    }

                    row.AddReferenceItem(this);
                }
                else
                {
                    //先添加新关系，然后删除旧关系
                    var newRefTable = GetSplitValue(newValue);
                    if (newRefTable.Count != 2)
                        return;

                    var newContentFile = TableContentManager.Instance.GetFileByName(newRefTable[0]);
                    if (newContentFile == null)
                        return;

                    var newRow = newContentFile.GetRowByID(newRefTable[1]);
                    if (newRow == null)
                    {
                        return;
                    }

                    newRow.AddReferenceItem(this);

                    var oldRefTable = GetSplitValue(oldValue);
                    if (oldRefTable.Count != 2)
                        return;

                    var oldContentFile = TableContentManager.Instance.GetFileByName(oldRefTable[0]);
                    if (oldContentFile == null)
                        return;

                    var oldRow = oldContentFile.GetRowByID(oldRefTable[1]);
                    if (oldRow == null)
                    {
                        return;
                    }

                    oldRow.DelReferenceItem(this);
                }
            }
        }

        //分割值
        public static List<string> GetSplitValue(string value)
        {
            return new List<string>(value.Split(';'));
        }

        //合并值
        public static string GetComboValue(params string[] splitValue)
        {
            string comboStr = "";
            for (int i = 0; i < splitValue.Length; ++i)
            {
                if (i == 0)
                {
                    comboStr = splitValue[i];
                }
                else
                {
                    comboStr += ";" + splitValue[i];
                }
            }
            return comboStr;
        }

        //有效值检测
        public void CheckValidate()
        {
            string errMsg = "";
            bool isValid = ContentValidationRule.IsValueValid(ItemConstruct, _Value, out errMsg);
            ErrorMsg = errMsg;
            IsShowError = !isValid;
            ContentRow.SetItemShowError(IsShowError);
        }
        #endregion

        #region 私有

        private void SetDisaplyInfo()
        {
            if (_DisplayInfo != null)
                return;

            if (_ItemConstruct.ItemType2.Count == 0)
                return;

            if (string.IsNullOrEmpty(_Value))
                return;

            if (_ItemConstruct.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM)
            {
                EnumInfo enumInfo = EnumManager.Instance.GetEnum(_ItemConstruct.ItemType2[0].Name) as EnumInfo;
                if (enumInfo == null)
                    return;

                _DisplayInfo = enumInfo.GetEnumItemByValue(_Value) as ITableDisplay;
            }
            else if (_ItemConstruct.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID)
            {

            }
        }

        //ID和名称的改变需要通知row
        private void SpecilValueNotify(string oldValue, string newValue)
        {
            if (ItemConstruct.ItemCode == ConstructConfig.NEW_FILE_DEFAULT_ID_CODE && ContentRow != null)
            {
                //id修正
                if (ContentRow != null && ContentRow.ContentFile != null && oldValue != null)
                {
                    ContentRow.ContentFile.ContentRow.ModifyKey(oldValue, newValue);
                }

                if (ItemConstruct.ItemType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID
                    && ItemConstruct.ItemType2.Count == 2)
                {
                    SetTableReference();
                }

                //反向索引记录
                ContentRow.ModifyRowID(oldValue, newValue);
            }
            else if (ItemConstruct.ItemCode == ConstructConfig.NEW_FILE_DEFAULT_NAME_CODE && ContentRow != null)
            {
                ContentRow.ModifyRowName(oldValue, newValue);
            }
        }
        #endregion
    }
}
