
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UberTrashInterface.Entities
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string phoneNumber = value.ToString();
                if (!Regex.IsMatch(phoneNumber, @"^\d+$"))
                {
                    return new ValidationResult("Phone number must contain only digits.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
