using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Attributes
{
    public class MaxValue : ValidationAttribute
    {
        private readonly int _value;
        public MaxValue(int value)
        {
            _value = value;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("This filed required not null");
            }
            try
            {
                var a = (int)value;
                if (a > _value)
                {
                    return new ValidationResult($"This filed must smaller {_value}");
                }
            }
            catch
            {
                return new ValidationResult("This filed required integer");
            }
            return null;
        }
    }
}
