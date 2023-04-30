using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNTier.Common.ResponseObjects;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace TodoAppNTier.Bussiness.Extensions
{
    public static class ValidationResultExtensions
    {
        public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult validationResult)
        {
            List<CustomValidationError> errors = new();
            foreach (var item in validationResult.Errors)
            {
                errors.Add(new()
                {
                    ErrorMessage = item.ErrorMessage,
                    PropertyName = item.PropertyName
                });
            }
            return errors;
        }
    }
}
