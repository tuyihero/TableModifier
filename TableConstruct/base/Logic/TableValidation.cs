using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TableConstruct
{
    public class StringEmpty : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var name = value as string;

            if (String.IsNullOrEmpty(name))
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "不能为空";
        }
    }

    public class StringNotDefault : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var name = value as string;

            if (name == "新建项")
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "请输入新建项的名字";
        }
    }
}
