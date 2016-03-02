using System.Collections;

namespace Tables
{
    public class TableReader
    {

        #region 唯一实例

        private TableReader() { }

        private static TableReader _Instance = null;
        public static TableReader Instance 
        {
            get
            {
                if (_Instance == null)
                    _Instance = new TableReader();

                return _Instance;
            }
            
        }

        #endregion

        #region Logic

        //技能信息
        public static SkillInfoTable SkillInfoTable { get; internal set; }
        public static SkillStepTable SkillStepTable { get; internal set; }

        public static void ReadTables()
        {
            //读取所有表
            SkillInfoTable = new SkillInfoTable("SkillInfoTable");
            SkillStepTable = new SkillStepTable("SkillStepTable");

            //初始化所有表
            SkillInfoTable.CoverTableContent();
        }

        #endregion
    }
}
