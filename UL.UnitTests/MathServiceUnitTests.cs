using System;
using UL.Application;
using UL.Application.Math;
using Xunit;

namespace UL.UnitTests
{
    public class MathServiceUnitTests
    {
        [Theory]
        [InlineData("4+5*2", 14)]
        [InlineData("4+5/2", 6.5)]
        [InlineData("4+5/2-1", 5.5)]
        public void EvaluateExpression_ValidExpression_ReturnsExpectedResult(string expression, double expectedResult)
        {
            // Arrange
            IMathService mathService = new MathService();

            // Act
            var result = mathService.EvaluateExpression(expression);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EvaluateExpression_DivideByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            var mathService = new MathService();
            var expression = "4/0"; // Division by zero

            // Act and Assert
            Assert.Throws<DivideByZeroException>(() => mathService.EvaluateExpression(expression));
        }
        [Fact]
        public void EvaluateExpression_InvalidInput_ThrowsFormatException()
        {
            // Arrange
            IMathService mathService = new MathService();
            var expression = "Invalid Input"; // Invalid Input

            // Act and Assert
            Assert.Throws<FormatException>(() => mathService.EvaluateExpression(expression));
        }
    }
}