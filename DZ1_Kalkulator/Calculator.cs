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
        private const char EMPTY_OPERATOR = 'N';
        private Display _display;
        private char _operator;
        private double _lastResult;
        private ButtonCategory _lastPressed;
        private string _memory;

        public Calculator()
        {
            _display = new Display();
            _operator = EMPTY_OPERATOR;
            _lastResult = double.NaN;
            CultureInfo.CurrentCulture = new CultureInfo("hr-HR");
            _lastPressed = ButtonCategory.Nothing;
            _memory = "0";
        }

        public string GetCurrentDisplayState()
        {
            return _display.CurrentState;
        }

        public void Press(char inPressedDigit)
        {
            if (char.IsDigit(inPressedDigit))
            {
                HandleDigit(inPressedDigit);
            }
            else if (inPressedDigit == ',')
            {
                _lastPressed = ButtonCategory.Comma;
                _display.Append(inPressedDigit);    
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
                _display.Clear();
            }
            else if (inPressedDigit == 'O')
            {
                _display.Clear();
                _lastResult = double.NaN;
                _lastPressed = ButtonCategory.Nothing;
            }
            else if (inPressedDigit == 'P')
            {
                _memory = _display.CurrentState;
            }
            else if (inPressedDigit == 'G')
            {
                _display.Print(_memory);
            }
            else
            {
                HandleOperator(inPressedDigit);
            }
        }

        private void ChangeSign()
        {
            string currentDisplay = GetCurrentDisplayState(); 
            if (currentDisplay.First() == '-')
            {
                currentDisplay = currentDisplay.Substring(1);
            }
            else
            {
                currentDisplay = "-" + currentDisplay;
            }
            _display.Print(currentDisplay);
        }

        private void HandleDigit(char digit)
        {
            if (_display.CurrentState == "0" && digit == '0')
            {
                return;
            }

            if (_lastPressed == ButtonCategory.BinaryOperator && double.IsNaN(_lastResult))
            {
                _lastResult = _display.ParseDisplay();
            }

            if (_lastPressed != ButtonCategory.Comma && _lastPressed != ButtonCategory.Digit)
            {
                _display.Delete();
            }

            _display.Append(digit);
            _lastPressed = ButtonCategory.Digit;
        }

        private void HandleOperator(char op)
        {
            if (UnaryOperatorProvider.IsUnaryOperator(op))
            {
                var fun = UnaryOperatorProvider.GetOperator(op);
                _display.Print(fun(_display.ParseDisplay()));
                _lastPressed = ButtonCategory.UnaryOperator;
            }
            else if (BinaryOperatorProvider.IsBinaryOperator(op))
            {
                if (double.IsNaN(_lastResult))
                {
                    _lastResult = _display.ParseDisplay();    
                }

                if (_operator != EMPTY_OPERATOR && _lastPressed != ButtonCategory.BinaryOperator)
                {
                    double number = _display.ParseDisplay();
                    var fun = BinaryOperatorProvider.GetOperator(_operator);
                    _lastResult = fun(_lastResult, number);
                    _display.Print(_lastResult);
                }
                _operator = op;
                _lastPressed = ButtonCategory.BinaryOperator;
            }
            else
            {
                throw new ArgumentException($"Unknown operator '{op}'");
            }
        }

        private void EqualsSign()
        {
            if (_operator != EMPTY_OPERATOR)
            {
                double operand = _lastResult;
                if (!string.IsNullOrEmpty(_display.CurrentState))
                {
                    operand = _display.ParseDisplay();
                }
                if (double.IsNaN(_lastResult))
                {
                    _lastResult = _display.ParseDisplay();
                }
                    
                var op = BinaryOperatorProvider.GetOperator(_operator);
                _lastResult = op(_lastResult, operand);
                _display.Print(_lastResult);
            }
            else
            {
                _display.Print(_display.ParseDisplay());  
            }
        }

    }
}
