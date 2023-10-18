using System.ComponentModel.DataAnnotations;

namespace UL.Domain
{
    public class MathExpression
    {
        [Required]
        [RegularExpression(@"^[\d\+\-\*\/\(\)]+$", ErrorMessage = "Invalid expression format.")]
        public string? Expression { get; set; }
    }
}