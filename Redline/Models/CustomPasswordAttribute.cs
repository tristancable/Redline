using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Redline.Models
{
    public class CustomPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
                return new ValidationResult("Password is required.", new[] { validationContext.MemberName! });

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult("Password must contain at least one uppercase letter.", new[] { validationContext.MemberName! });

            if (!Regex.IsMatch(password, @"[a-z]"))
                return new ValidationResult("Password must contain at least one lowercase letter.", new[] { validationContext.MemberName! });

            if (!Regex.IsMatch(password, @"[0-9]"))
                return new ValidationResult("Password must contain at least one number.", new[] { validationContext.MemberName! });

            if (!Regex.IsMatch(password, @"[\W_]"))
                return new ValidationResult("Password must contain at least one special character.", new[] { validationContext.MemberName! });

            if (password.Length < 8)
                return new ValidationResult("Password must be at least 8 characters long.", new[] { validationContext.MemberName! });

            return ValidationResult.Success;
        }
    }
}