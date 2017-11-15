using System;
using System.Linq;
using System.Globalization;

namespace PrvaDomacaZadaca_Kalkulator
{
    enum ButtonCategory { Nothing, Number, Digit, Comma, BinaryOperator, UnaryOperator }

    public class Factory
    {
        public static ICalculator CreateCalculator()
        {
            return new Calculator();
        }
    }

    public class Calculator : ICalculator
    {
        private const int DISPLAY_SIZE = 10;
        private const char EMPTY_OPERATOR = 'N';
        private string _display;
        private char _operator;
        private double _lastResult;
        private ButtonCategory _lastPressed;
        private string _memory;

        public Calculator()
        {
            _display = "0";
            _operator = EMPTY_OPERATOR;
            _lastResult = double.NaN;
            CultureInfo.CurrentCulture = new CultureInfo("hr-HR");
            _lastPressed = ButtonCategory.Nothing;
            _memory = "0";
        }

        public string GetCurrentDisplayState()
        {
            return _display;
        }

        public void Press(char inPressedDigit)
        {
            Console.WriteLine($"Pressed: {inPressedDigit}");
            Console.WriteLine($"Last result: {_lastResult}");
            Console.WriteLine($"Operator: {_operator}");
            Console.WriteLine("--------------------------------");

            if (char.IsDigit(inPressedDigit))
            {
                HandleDigit(inPressedDigit);
            }
            else if (inPressedDigit == ',')
            {
                _lastPressed = ButtonCategory.Comma;
                _display += inPressedDigit;    
            }
            else if (inPressedDigit == '=')
            {
                EqualsSign();
            }

            else if (inPressedDigit == 'M')
            {
                ChangeSign();
            }
            else if (inPressedDigit == 'C')
            {
                _display = "0";
            }
            else if (inPressedDigit == 'O')
            {
                _display = "0";
                _lastResult = double.NaN;
                _lastPressed = ButtonCategory.Nothing;
            }
            else if (inPressedDigit == 'P')
            {
                _memory = _display;
            }
            else if (inPressedDigit == 'G')
            {
                Print(_memory);
            }
            else
            {
                HandleOperator(inPressedDigit);
            }
        }

        private void ChangeSign()
        {
            if (_display.First() == '-')
            {
                _display = _display.Substring(1);
            }
            _display = "-" + _display;
        }

        private void HandleDigit(char digit)
        {
            if (_display == "0" && digit == '0')
            {
                return;
            }

            if (_lastPressed == ButtonCategory.BinaryOperator && double.IsNaN(_lastResult))
            {
                _lastResult = ParseDisplay();
            }

            if (_lastPressed != ButtonCategory.Comma && _lastPressed != ButtonCategory.Digit)
            {
                _display = "";
            }

            int digitsCount = _display.Where(c => char.IsNumber(c)).Count();
            if (digitsCount < DISPLAY_SIZE)
            {
                _display += digit;
            }

            _lastPressed = ButtonCategory.Digit;
        }

        private void HandleOperator(char op)
        {
            if (UnaryOperatorProvider.IsUnaryOperator(op))
            {
                var fun = UnaryOperatorProvider.GetOperator(op);
                Print(fun(ParseDisplay()));
                _lastPressed = ButtonCategory.UnaryOperator;
            }
            else if (BinaryOperatorProvider.IsBinaryOperator(op))
            {
                if (double.IsNaN(_lastResult))
                {
                    _lastResult = ParseDisplay();    
                }

                if (_operator != EMPTY_OPERATOR && _lastPressed != ButtonCategory.BinaryOperator)
                {
                    double number = ParseDisplay();
                    var fun = BinaryOperatorProvider.GetOperator(_operator);
                    _lastResult = fun(_lastResult, number);
                    Print(_lastResult);
                }
                _operator = op;
                _lastPressed = ButtonCategory.BinaryOperator;
            }
            else
            {
                throw new ArgumentException($"Unknown operator '{op}'");
            }
        }

        private void Print(double number)
        {
            // Check if the integer part of number is too big
            int x = (int) number;
            if (x.ToString().Length > DISPLAY_SIZE)
            {
                Print("-E-");
                return;
            }

            _display = number.ToString();

            // Check if there are too many decimal places 
            int digitsCount = _display.Where(c => char.IsNumber(c)).Count();
            if (digitsCount > DISPLAY_SIZE)
            {
                int digitsAfterComma = _display.Reverse().TakeWhile(c => char.IsNumber(c)).Count();
                int digitsBeforeComma = digitsCount - digitsAfterComma;
                _display = Math.Round(number, DISPLAY_SIZE - digitsBeforeComma).ToString();
            }

            // Check if the last decimal is zero
            if (_display.Contains(','))
            {
                _display = new string(_display.Reverse().SkipWhile(c => c == '0').Reverse().ToArray());
            }

            if (_display.Last() == ',')
            {
                _display = new string(_display.TakeWhile(c => c != ',').ToArray());
            }
        }

        private void Print(string message)
        {
            _display = message;
        }

        private double ParseDisplay()
        {
            double number = 0.0;
            bool success = double.TryParse(_display, out number);
            return number;
        }

        private void EqualsSign()
        {
            if (_operator != EMPTY_OPERATOR)
            {
                double operand = _lastResult;
                if (!string.IsNullOrEmpty(_display))
                {
                    operand = ParseDisplay();
                }
                if (double.IsNaN(_lastResult))
                {
                    _lastResult = ParseDisplay();
                }
                    
                var op = BinaryOperatorProvider.GetOperator(_operator);
                _lastResult = op(_lastResult, operand);
                Print(_lastResult);
            }
            else
            {
                Print(ParseDisplay());  
            }
        }

    }
}
