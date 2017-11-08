using System;
using System.Collections.Generic;

namespace PrvaDomacaZadaca_Kalkulator
{
    public class BinaryOperatorProvider
    {
        private static readonly Dictionary<char, Func<double, double, double>> _binaryOperators = 
            new Dictionary<char, Func<double, double, double>>()
        {
            { '+', (a, b) => a + b },
            { '-', (a, b) => a - b },
            { '*', (a, b) => a * b },
            { '/', (a, b) => a / b }
        };

        public static Func<double, double, double> GetOperator(char op)
        {
            return _binaryOperators[op];
        }

        public static bool IsBinaryOperator(char op)
        {
            return _binaryOperators.ContainsKey(op);
        }
    }
}
