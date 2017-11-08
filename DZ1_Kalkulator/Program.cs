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
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press(',');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('*'); //provjera uzastopnog unosa različitih binarnih operatora (zadnji se pamti)
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('-');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('+');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('3');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('-'); //provjera uzastopnog unosa istog binarnog operatora
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('-');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('-');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('2');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('Q');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('Q'); //provjera uzastopnog unosa unarnih operatora (svi se izračunavaju)
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('*');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('2');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('-');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('3');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('C');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('1');
            Console.WriteLine($"Screen: {calculator.GetCurrentDisplayState()}");
            calculator.Press('=');

            string displayState = calculator.GetCurrentDisplayState();
            Console.WriteLine("-23 : " + displayState);
        }
    }
}
