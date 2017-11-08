using System;
using System.Linq;
using System.Globalization;

namespace PrvaDomacaZadaca_Kalkulator
{
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
        private bool _appendMode;

        public Calculator()
        {
            _display = "0";
            _operator = EMPTY_OPERATOR;
            _lastResult = double.NaN;
            _appendMode = false;
            CultureInfo.CurrentCulture = new CultureInfo("hr-HR");
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
                _appendMode = true;
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
                _appendMode = false;
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

            if (!_appendMode)
            {
                _display = "";
                _appendMode = true;
            }

            int digitsCount = _display.Where(c => char.IsNumber(c)).Count();
            if (digitsCount < DISPLAY_SIZE)
            {
                _display += digit;
            }
        }

        private void HandleOperator(char op)
        {
            if (UnaryOperatorProvider.IsUnaryOperator(op))
            {
                var fun = UnaryOperatorProvider.GetOperator(op);
                Print(fun(ParseDisplay()));
            }
            else if (BinaryOperatorProvider.IsBinaryOperator(op))
            {
                if (_operator != EMPTY_OPERATOR && _appendMode)
                {
                    var fun = BinaryOperatorProvider.GetOperator(_operator);
                    double result = fun(_lastResult, ParseDisplay());
                    _lastResult = result;
                    Print(result);
                    _appendMode = false;
                    _operator = op;
                }
                else if (!_appendMode)
                {
                    _operator = op;
                }
                else
                {
                    _lastResult = ParseDisplay();
                }
                _operator = op;
            }
            else
            {
                throw new ArgumentException($"Unknown operator '{op}'");
            }
            _appendMode = false;
        }

        private void Print(double number)
        {
            _display = number.ToString();
            int digitsCount = _display.Where(c => char.IsNumber(c)).Count();
            if (digitsCount > DISPLAY_SIZE)
            {
                _display = new string(_display.Reverse().Skip(digitsCount - DISPLAY_SIZE).Reverse().ToArray());
            }
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
                var op = BinaryOperatorProvider.GetOperator(_operator);
                _lastResult = op(_lastResult, operand);
                Print(_lastResult);
            }
            _appendMode = false;
        }
    }
}
