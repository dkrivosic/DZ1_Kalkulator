using System;
using System.Linq;

namespace PrvaDomacaZadaca_Kalkulator
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ICalculator calculator = Factory.CreateCalculator();
            calculator.Press('3');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('+');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('2');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('M');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('*');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('4');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('=');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('Q');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('S');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('M');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('P');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('C');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('5');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('I');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('+');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('G');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('=');

            //Assert.AreEqual("0,487903317", displayState);

            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
        }
    }
}
