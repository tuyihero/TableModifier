using System.Collections;

namespace Tables
{

    //技能优先级
    public enum SKILL_PRIOR_TYPE
    {
        SKILL_PRIOR_NONE = 0,
        SKILL_PRIOR_COVER = 1, //Cover,中断同优先级的技能
        SKILL_PRIOR_NOT_COVER = 2, //NotCover,不中断同优先级技能
    }


}
