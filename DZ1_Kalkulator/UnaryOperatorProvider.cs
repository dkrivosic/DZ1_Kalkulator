using System;
using System.Collections.Generic;

namespace PrvaDomacaZadaca_Kalkulator
{
    public class UnaryOperatorProvider
    {
        private static readonly Dictionary<char, Func<double, double>> _unaryOperators =
            new Dictionary<char, Func<double, double>>()
        {
            { 'S', x => Math.Sin(x) },
            { 'K', x => Math.Cos(x) },
            { 'T', x => Math.Tan(x) },
            { 'Q', x => Math.Pow(x, 2) },
            { 'R', x => Math.Sqrt(x) },
            { 'I', x => 1.0 / x },

        };

        public static Func<double, double> GetOperator(char op)
        {
            return _unaryOperators[op];
        }

        public static bool IsUnaryOperator(char op)
        {
            return _unaryOperators.ContainsKey(op);
        }
    }
}
