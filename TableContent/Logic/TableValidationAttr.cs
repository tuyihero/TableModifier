using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using TableConstruct;

namespace TableContent
{
    public class ValidConstruct : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            ValidationResult result = ValidationResult.Success;

            ContentItem constructItem = context.ObjectInstance as ContentItem;
            if (constructItem != null)
            {
                string errorMsg = "";
                bool isValid = ContentValidationRule.IsValueValid(constructItem.ItemConstruct, value, out errorMsg);
                if (!isValid)
                {
                    result = new ValidationResult(errorMsg);
                }
            }

            return result;
        }

    }
}
