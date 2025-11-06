using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StockExchange.Domain.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    internal class AllLettersCapitalizedAttribute : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else if (value is string text)
            {
                if (text == String.Empty)
                {
                    return ValidationResult.Success;
                }

                if (text.ToUpper() == text)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("All letters must be capitalized.");
                }
            } 
            else
            {
                throw new NotImplementedException($"The {nameof(AllLettersCapitalizedAttribute)} is not implemented for the type: {value.GetType()}");
            }

        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context.Attributes.ContainsKey("data-val") == false)
            {
                context.Attributes.Add("data-val", "true");
            }

            context.Attributes.Add("data-val-allletterscapitalized", "All letters must be capitalized.");
        }
    }
}
