using System.Data;

namespace UL.Application
{

    public class MathService :IMathService
    {
        public double EvaluateExpression(string expression)
        {
            
                DataTable table = new DataTable();
                
                object result ;
                try 
                {
                    result = table.Compute(expression, "");
                }
                catch (Exception )
                {
                    throw new FormatException("Invalid input expression");
                }

                if (result == null || result == DBNull.Value)
                {
                    throw new FormatException("Invalid input expression");
                }
                else if (double.IsInfinity(Convert.ToDouble(result))) {

                    throw new DivideByZeroException("Division by zero not allowed");

                }
                else
                {
                    return Convert.ToDouble(result);
                }
           

        }
    }
    
}
