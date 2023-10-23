using System.ComponentModel.DataAnnotations;

namespace UL.Domain
{
    public class MathExpression
    {
        [Required]
        [RegularExpression(@"^(\d+[-+*\/]?)+\d+$|^\d+$", ErrorMessage = "Invalid expression format.")]
        public string? Expression { get; set; }
    }
}