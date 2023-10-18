using System.ComponentModel.DataAnnotations;

namespace UL.Domain
{
    public class MathExpression
    {
        [Required]
        [RegularExpression(@"^[\d\+\-\*\/\(\)]+$", ErrorMessage = "Invalid characters in expression.")]
        public string? Expression { get; set; }
    }
}