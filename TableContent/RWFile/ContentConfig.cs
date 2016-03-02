using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableContent
{
    class ContentConfig
    {
        //public static string CONTENT_FOLD_PATH = "./TableContent";

        public static string CONTENT_INVALID_ID = "-1";
        public static int CONTENT_INVALID_INT_ID = -1;
        public static string CONTENT_INIT_DEFAULT_ID = "#empty";

        public static bool IsContentIDInvalid(string id)
        {
            int intID = CONTENT_INVALID_INT_ID;
            if (!int.TryParse(id, out intID))
            {
                return false;
            }

            return IsContentIDInvalid(intID);
        }

        public static bool IsContentIDInvalid(int id)
        {
            return id != CONTENT_INVALID_INT_ID;
        }
    }
}
