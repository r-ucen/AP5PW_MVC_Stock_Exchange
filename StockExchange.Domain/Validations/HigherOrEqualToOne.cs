using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Domain.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]

    public class HigherOrEqualToOneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else if (value is int num)
            {
                if (num >= 1)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The number of shares bust be equal or higher than 1.");
                }
            }
            else
            {
                throw new NotImplementedException($"The {nameof(HigherOrEqualToOneAttribute)} is not implemented for the type: {value.GetType()}");
            }
        }
    }
}
