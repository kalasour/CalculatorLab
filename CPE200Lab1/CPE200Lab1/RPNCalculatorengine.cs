using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class RPNCalculatorEngine:CalculatorEngine
    {
        public string Process(string str)
        {
            string result="Zero.";
            Stack NumList=new Stack();
            string[] parts= str.Split(' ');
            if (parts[(parts.Length) - 1] == "") return "E";
            for (int i = parts.Length-1; i >=0; i--)
            {
                if (isNumber(parts[i]))
                {
                    NumList.Push(parts[i]);
                }
            }
            for (int i = 0; i <parts.Length; i++)
            {
                if (isOperator(parts[i]))
                {
                    result = calculate(parts[i], NumList.Pop().ToString(), NumList.Pop().ToString());
                    NumList.Push(result);
                }
            }
            return NumList.Pop().ToString();
        }
    }
}
