using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverJewelry_BOs.Validations
{
    public class YearRangeAttribute : ValidationAttribute
    {
        private readonly int _minYear;

        public YearRangeAttribute(int minYear)
        {
            _minYear = minYear;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                int maxYear = DateTime.Now.Year;
                if (year >= _minYear && year <= maxYear)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"The year must be between {_minYear} and {maxYear}.");
                }
            }

            return new ValidationResult("Invalid year format.");
        }
    }
}
