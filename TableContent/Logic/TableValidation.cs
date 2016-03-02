using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;

using TableConstruct;

namespace TableContent
{
    public class ContentValidationRule
    {
        public static bool IsValueValid(ConstructItem constructItem, object value)
        {
            string errorMsg = "";
            return IsValueValid(constructItem, value, out errorMsg);
        }

        public static bool IsValueValid(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";
            switch (constructItem.ItemType1)
            {
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_BASE:
                    isValid = IsBaseTypeValid(constructItem, value, out errorMsg);
                    break;
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM:
                    isValid = IsValueEnum(constructItem, value, out errorMsg);
                    break;
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID:
                    isValid = IsValueTableIdx(constructItem, value, out errorMsg);
                    break;
            }

            return isValid;
        }

        #region 基本类型
        
        private static bool IsBaseTypeValid(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";

            switch (constructItem.ItemType2[0].Name)
            {
                case ConstructConfig.ITEM_TYPE_BASE_STRING:
                    isValid = IsStringValid(constructItem, value, out errorMsg);
                    break;
                case ConstructConfig.ITEM_TYPE_BASE_INT:
                    isValid = IsIntValid(constructItem, value, out errorMsg);
                    break;
                case ConstructConfig.ITEM_TYPE_BASE_FLOAT:
                    isValid = IsFloatValid(constructItem, value, out errorMsg);
                    break;
                case ConstructConfig.ITEM_TYPE_BASE_BOOL:
                    isValid = IsBoolValid(constructItem, value, out errorMsg);
                    break;
                case ConstructConfig.ITEM_TYPE_BASE_VECTOR3:
                    isValid = IsVector3Valid(constructItem, value, out errorMsg);
                    break;
            }

            return isValid;
        }

        private static bool IsStringValid(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";

            return isValid;
        }

        private static bool IsIntValid(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";

            int valueInt;
            if (!Int32.TryParse((string)value, out valueInt))
            {
                isValid = false;
                errorMsg = "不是32位整数";
            }

            return isValid;
        }

        private static bool IsFloatValid(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";

            float valueType;
            if (!float.TryParse((string)value, out valueType))
            {
                isValid = false;
                errorMsg = "不是32位浮点数数";
            }

            return isValid;
        }

        private static bool IsBoolValid(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";

            bool valueType;
            if (!bool.TryParse((string)value, out valueType))
            {
                isValid = false;
                errorMsg = "不是布尔型";
            }

            return isValid;
        }

        private static bool IsVector3Valid(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";

            var valueList = ContentItem.GetSplitValue((string)value);
            if (valueList.Count != 3)
            {
                isValid = false;
                errorMsg = "类型错误";
            }

            foreach (var valueSingle in valueList)
            {
                isValid = IsFloatValid(constructItem, valueSingle, out errorMsg);
                if (!isValid)
                    break;
            }

            return isValid;
        }
        #endregion

        #region 枚举

        public static bool IsValueEnum(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";

            string valueType = (string)value;
            if (string.IsNullOrEmpty(valueType))
            {//空值直接返回
                return true;
            }

            EnumInfo enumInfo = EnumManager.Instance.GetEnum(constructItem.ItemType2[0].Name);
            if (enumInfo == null)
            {
                errorMsg = "枚举无效";
                return false;
            }

            EnumItem enumItem = enumInfo.GetEnumItemByValue(valueType);
            if (enumItem == null)
            {
                errorMsg = "枚举值无效";
                return false;
            }

            return isValid;
        }

        #endregion

        #region 表索引

        public static bool IsValueTableIdx(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";

            string valueType = (string)value;
            if (string.IsNullOrEmpty(valueType))
            {//空值直接返回
                return true;
            }

            if (constructItem.ItemType2.Count == 1)
            {
                isValid = IsValueTableSingle(constructItem, value, out errorMsg);
            }
            else
            {
                isValid = IsValueTableMulti(constructItem, value, out errorMsg);
            }

            return isValid;
        }

        public static bool IsValueTableSingle(ConstructItem constructItem, object value, out string errorMsg)
        {
            bool isValid = true;
            errorMsg = "";

            ContentFile contentFile = TableContentManager.Instance.GetFileByName(constructItem.ItemType2[0].Name);
            if (contentFile == null)
            {
                errorMsg = "表格无效";
                return false;
            }

            string valueType = (string)value;
            ContentRow contentRow = contentFile.ContentRow.GetRowByID(valueType);
            if (contentRow == null)
            {
                errorMsg = "表格ID无效";
                return false;
            }

            return isValid;
        }

        public static bool IsValueTableMulti(ConstructItem constructItem, object value, out string errorMsg)
        {
            errorMsg = "";

            var valueList = ContentItem.GetSplitValue((string)value);
            if (valueList.Count != 2)
            {
                errorMsg = "格式无效";
                return false;
            }

            if (string.IsNullOrEmpty(valueList[0]) || string.IsNullOrEmpty(valueList[1]))
            {
                errorMsg = "";
                return true;
            }

            if (constructItem.ItemType2.GetByName(valueList[0]) == null)
            {
                errorMsg = "不支持该表";
                return false;
            }

            ContentFile contentFile = TableContentManager.Instance.GetFileByName(valueList[0]);
            if (contentFile == null)
            {
                errorMsg = "表格无效";
                return false;
            }

            ContentRow contentRow = contentFile.ContentRow.GetRowByID(valueList[1]);
            if (contentRow == null)
            {
                errorMsg = "表格ID无效";
                return false;
            }

            return true;
        }
        #endregion
    }
}
