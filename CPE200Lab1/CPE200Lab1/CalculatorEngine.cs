using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    class CalculatorEngine
    {
        
        public string calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            double result;
            switch (operate)
            {
                case "+":
                    result = (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand));
                 /*   MessageBox.Show(result.ToString(), secondOperand);
                    MessageBox.Show(DecimalManage(result), secondOperand);*/
                    return DecimalManage(result);
                case "-":
                    result = (Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand));
                    return DecimalManage(result);
                case "X":

                    result = (Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand));
                    // split between integer part and fractional part
                    return DecimalManage(result);
                case "÷":
                    // Not allow devide be zero
                    if (secondOperand != "0")
                    {

                        result = (Convert.ToDouble(firstOperand) / Convert.ToDouble(secondOperand));
                        return DecimalManage(result);
                    }
                    break;
            }
            return "E";
        }

        public string DecimalManage(double result)
        {
            string[] parts;
            int remainLength;
            string returnTo= String.Format("{0:0.######}", result);
            parts = returnTo.Split('.');
            // if integer part length is already break max output, return error
            if (parts[0].Length > 8)
            {
                return "Error";
            }
            // calculate remaining space for fractional part.
           remainLength = 8 - parts[0].Length - 1;
            if (remainLength < 0) remainLength = 0;
             //trim the fractional part gracefully. =
            returnTo=Convert.ToDouble(returnTo).ToString("N"+remainLength);
            if(returnTo.IndexOf(".")!=-1)
            {
                for (; ; )
                {
                    if (returnTo[(returnTo.Length) - 1] == '0')
                    {
                        returnTo = returnTo.Substring(0, returnTo.Length - 1);
                    }
                    else if (returnTo[(returnTo.Length) - 1] == '.')
                    {
                        returnTo = returnTo.Substring(0, returnTo.Length - 1);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(returnTo.Length>8)
            {
                return "Error";
            }
            return returnTo;
        }
    }
}
