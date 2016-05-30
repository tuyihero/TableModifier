using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableConstruct
{
    public class TableGlobalDefine
    {
        public static string CONFIG_FOLD_PATH = "./TableCodeCreator";

        public static string CONFIG_FILE_NAME = "config";

        //新建XML信息
        public const string INIT_VERSION_STR = "1.0";
        public const string INIT_ENCODING_STR = "utf-8";
        public const string DOC_ROOT_STR = "root";

        //项信息
        public const string ELEMENT_TEMPLATE_PATH = "TemplatePath";
        public const string ELEMENT_CODE_PATH = "CodePath";
        public const string ELEMENT_CODE_TABLE_PATH = "CodeTablePath";
        public const string ELEMENT_RES_TABLE_PATH = "ResTablePath";
        public const string ELEMENT_PROJECT_PATH = "ProjectPath";
        

        public const string ELEMENT_DEFAULT_TEMPLATE_PATH = "./TableCodeTemplate";
        public const string ELEMENT_DEFAULT_CONSTRUCT_PATH = "\\Tables\\Construct";
        public const string ELEMENT_DEFAULT_CODE_PATH = "\\Tables\\Code";
        public const string ELEMENT_DEFAULT_RESOURCE_PATH = "\\Resources\\Tables"; 

    }
}
