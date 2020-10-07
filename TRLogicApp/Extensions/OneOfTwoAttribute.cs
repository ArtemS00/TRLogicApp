using System.ComponentModel.DataAnnotations;

namespace TRLogicApp.Extensions
{
    public class OneOfTwoAttribute : ValidationAttribute
    {
        private readonly string _other;
        public OneOfTwoAttribute(string other)
        {
            _other = other;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_other);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", _other)
                );
            }
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);

            if (value == null ^ otherValue == null)
                return null;

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
