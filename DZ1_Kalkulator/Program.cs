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
            calculator.Press('P');
            calculator.Press('3');
            calculator.Press('P');
            calculator.Press('4');
            calculator.Press('P');
            calculator.Press('5');
            calculator.Press('G');
            //234
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
        }
    }
}
