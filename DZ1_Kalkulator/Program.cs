using System;
using System.Linq;

namespace PrvaDomacaZadaca_Kalkulator
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ICalculator calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('M');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('M');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
        }
    }
}
