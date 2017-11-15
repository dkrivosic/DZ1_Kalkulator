using System;
using System.Linq;

namespace PrvaDomacaZadaca_Kalkulator
{
    public class Display
    {
        private const int DISPLAY_SIZE = 10;
        public string CurrentState { get; private set; }

        public Display()
        {
            CurrentState = "0";
        }

        public void Print(string message)
        {
            CurrentState = message;
        }

        public void Print(double number)
        {
            // Print error if the integer part of the number is too big
            int x = (int)number;
            if (x.ToString().Length > DISPLAY_SIZE)
            {
                CurrentState = "-E-";
                return;
            }

            CurrentState = number.ToString();

            // Round if there are too many digits after decimal point 
            int digitsCount = CurrentState.Where(c => char.IsNumber(c)).Count();
            if (digitsCount > DISPLAY_SIZE)
            {
                int digitsAfterComma = CurrentState.Reverse().TakeWhile(c => char.IsNumber(c)).Count();
                int digitsBeforeComma = digitsCount - digitsAfterComma;
                CurrentState = Math.Round(number, DISPLAY_SIZE - digitsBeforeComma).ToString();
            }

            // Remove trailing zeros after decimal point
            if (CurrentState.Contains(','))
            {
                CurrentState = new string(CurrentState.Reverse().SkipWhile(c => c == '0').Reverse().ToArray());
            }

            // Remove decimal point if it is the last character on the display
            if (CurrentState.Last() == ',')
            {
                CurrentState = new string(CurrentState.TakeWhile(c => c != ',').ToArray());
            }
        }

        public void Append(char digit)
        {
            int digitsCount = CurrentState.Where(c => char.IsNumber(c)).Count();
            if (digitsCount < DISPLAY_SIZE)
            {
                CurrentState += digit;
            }
        }

        public void Clear()
        {
            CurrentState = "0";
        }

        public void Delete()
        {
            CurrentState = "";
        }

        public double ParseDisplay()
        {
            double number = 0.0;
            bool success = double.TryParse(CurrentState, out number);
            return number;
        }
    }
}
