using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableConstruct
{
    public class EnuminfoConfig
    {
        public static string ENUMINFO_FOLD_PATH = "/EnumInfo";

        //新建XML信息
        public const string INIT_VERSION_STR = "1.0";
        public const string INIT_ENCODING_STR = "utf-8";
        public const string DOC_ROOT_STR = "root";

        //项信息
        public const string ENUMINFO_ELEMENT_COLUMN = "enumColumn";
        public const string ENUMINFO_ELEMENT_NAME = "enumName";
        public const string ENUMINFO_ELEMENT_CODE = "enumCode";
        public const string ENUMINFO_ELEMENT_VALUE = "enumValue";
        public const string ENUMINFO_ELEMENT_DESC = "enumDesc";

    }
}
