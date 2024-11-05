using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SilverJewelry_BOs.Validations
{
    public class NameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                // Regular expression to match each word starting with a capital letter
                string pattern = @"^([A-Z][a-z0-9]*)(\s[A-Z][a-z0-9]*)*$";

                if (Regex.IsMatch(name, pattern))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The name must only contain letters, digits, and spaces, and each word must start with a capital letter.");
                }
            }

            return new ValidationResult("Invalid name format.");
        }
    }
}
