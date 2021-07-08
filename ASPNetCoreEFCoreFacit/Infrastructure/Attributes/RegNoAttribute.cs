using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreEFCoreFacit.Infrastructure.Attributes
{
    public class RegNoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var regno = value.ToString();
            if (regno.Length != 6)
                return new ValidationResult("Måste vara 6 tecken");

            for(int i = 0; i < 3; i++)
                if(!Char.IsLetter(regno[i]))
                    return new ValidationResult("Felaktigt regno");

            for (int i = 3; i < 5; i++)
                if (!Char.IsDigit(regno[i]))
                    return new ValidationResult("Felaktigt regno");

            if (!Char.IsLetterOrDigit(regno[5]))
                return new ValidationResult("Felaktigt regno");

            return ValidationResult.Success;
        }
    }
}