using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableConstruct
{
    public class ConstructConfig
    {
        public static string CONSTUCT_FOLD_PATH = "./TableConstruct";

        //新建XML信息
        public const string INIT_VERSION_STR = "1.0";
        public const string INIT_ENCODING_STR = "utf-8";
        public const string DOC_ROOT_STR = "root";

        //表格信息
        public const string CONSTRUCT_TABLE_CLASS = "tableClass";
        public const string CONSTRUCT_TABLE_DESC = "tableDesc";
        public const string CONSTRUCT_TABLE_OLD_NAME = "tableOldName";

        //项信息
        public const string CONSTRUCT_ELEMENT_COLUMN = "constructColumn";
        public const string CONSTRUCT_ELEMENT_NAME = "constructName";
        public const string CONSTRUCT_ELEMENT_CODE = "constructCode";
        public const string CONSTRUCT_ELEMENT_TYPE1 = "constructType1";
        public const string CONSTRUCT_ELEMENT_TYPE2 = "constructType2";
        public const string CONSTRUCT_ELEMENT_DEFAULT = "constructDefault";
        public const string CONSTRUCT_ELEMENT_REPEAT = "constructRepeat";

        //类型信息
        public const string CONSTRUCT_ITEM_TYPE_BASE = "基本类型";
        public const string CONSTRUCT_ITEM_TYPE_ENUM = "自定义枚举";
        public const string CONSTRUCT_ITEM_TYPE_TABLE_ID = "表格索引";

        public const string ITEM_TYPE_BASE_INT = "整数";
        public const string ITEM_TYPE_BASE_FLOAT = "附点数";
        public const string ITEM_TYPE_BASE_STRING = "字符串";
        public const string ITEM_TYPE_BASE_BOOL = "布尔";
        public const string ITEM_TYPE_BASE_VECTOR3 = "三元组";

        //默认项
        public const string NEW_FILE_DEFAULT_ID_NAME = "ID";
        public const string NEW_FILE_DEFAULT_ID_CODE = "Id";
        public const string NEW_FILE_DEFAULT_ID_TYPE1 = CONSTRUCT_ITEM_TYPE_BASE;
        public const string NEW_FILE_DEFAULT_ID_TYPE2 = ITEM_TYPE_BASE_STRING;
        public const string NEW_FILE_DEFAULT_ID_DEFAULT = "-1";
        public const int NEW_FILE_DEFAULT_ID_REPEAT = 0;

        public const string NEW_FILE_DEFAULT_NAME_NAME = "名称";
        public const string NEW_FILE_DEFAULT_NAME_CODE = "Name";
        public const string NEW_FILE_DEFAULT_NAME_TYPE1 = CONSTRUCT_ITEM_TYPE_BASE;
        public const string NEW_FILE_DEFAULT_NAME_TYPE2 = ITEM_TYPE_BASE_STRING;
        public const string NEW_FILE_DEFAULT_NAME_DEFAULT = "";
        public const int NEW_FILE_DEFAULT_NAME_REPEAT = 0;

        public const string NEW_FILE_DEFAULT_DESC_NAME = "描述";
        public const string NEW_FILE_DEFAULT_DESC_CODE = "Desc";
        public const string NEW_FILE_DEFAULT_DESC_TYPE1 = CONSTRUCT_ITEM_TYPE_BASE;
        public const string NEW_FILE_DEFAULT_DESC_TYPE2 = ITEM_TYPE_BASE_STRING;
        public const string NEW_FILE_DEFAULT_DESC_DEFAULT = "";
        public const int NEW_FILE_DEFAULT_DESC_REPEAT = 0;

    }
}
