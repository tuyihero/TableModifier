using System.Collections;

namespace Tables
{
    public class TableReadBase
    {
        public static int ParseInt(string value)
        {
            return int.Parse(value);
        }

        public static float ParseFloat(string value)
        {
            return float.Parse(value);
        }

        public static string ParseString(string value)
        {
            return value;
        }
    }
}
