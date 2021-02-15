using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReferenceOfPerson.Core.Utilities
{
    public class Min18years : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var birthDate = Convert.ToDateTime(value);
            var age = DateTime.Today.Year - birthDate.Year;
            return(age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Person should be at least 18 years old");
        }
    }
}