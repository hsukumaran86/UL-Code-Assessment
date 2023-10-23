using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace UL.Application.Math
{

    public class MathService : IMathService
    {
        public double EvaluateExpression(string? expression)
        {
            var pattern = @"^(\d+[-+*\/]?)+\d+$|^\d+$";
            if (!string.IsNullOrEmpty(expression) && Regex.IsMatch(expression, pattern))
            {

                return EvaluateTokens(GetTokens(expression, new List<char> { '+', '-' }));

            }
            else
            {
                throw new FormatException("Invalid expression format.");
            }


        }
        private List<string> GetTokens(string expression, List<char> operators)
        {
            List<string> elments = new List<string>();
            StringBuilder currentTerm = new StringBuilder();
            foreach (char c in expression)
            {


                if (operators.Contains(c))
                {
                    elments.Add(currentTerm.ToString());
                    currentTerm.Clear();
                    elments.Add(c.ToString());

                }
                else
                {
                    currentTerm.Append(c);
                }
            }
            elments.Add(currentTerm.ToString());
            return elments;
        }

        private double EvaluateTokens(List<string> elements)
        {
            double accumulator = 0;
            List<char> multandiv = new List<char> { '*', '/' };
            for (int i = 0; i < elements.Count; i++)
            {

                switch (elements[i])
                {
                    case "*":
                        accumulator *= Convert.ToDouble(elements[i + 1]);
                        break;
                    case "/":
                        if (elements[i + 1] == "0")
                        {
                            throw new DivideByZeroException("Division by zero not allowed");
                        }
                        accumulator /= Convert.ToDouble(elements[i + 1]);
                        break;
                    case "+":
                        accumulator += EvaluateTokens(GetTokens(elements[i + 1], multandiv));
                        break;
                    case "-":
                        accumulator -= EvaluateTokens(GetTokens(elements[i + 1], multandiv));
                        break;
                    default:
                        if (i == 0)
                        {
                            if (IsNotANumber(elements[i]))
                            {
                                accumulator = EvaluateTokens(GetTokens(elements[i], multandiv));
                            }
                            else
                            {
                                accumulator = Convert.ToDouble(elements[i]);
                            }


                        }
                        break;
                }

            }
            return accumulator;
        }

        private bool IsNotANumber(string input)
        {
            if (int.TryParse(input, out int result))
            {
                // The input is a valid integer
                return false;
            }
            else
            {
                // The input is not a valid integer
                return true;
            }
        }
    }
}