using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorNamespace;

public partial class MainWindow : Window
{
    private double? _firstOperand;
    private string? _pendingOperator;
    private bool _replaceDisplay = true;

    /// <summary>
    /// Initializes the calculator window.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        UpdateDisplay("0");
    }

    private void DigitButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not string digit)
        {
            return;
        }

        if (_replaceDisplay || DisplayTextBox.Text == "0")
        {
            UpdateDisplay(digit);
        }
        else
        {
            UpdateDisplay(DisplayTextBox.Text + digit);
        }

        _replaceDisplay = false;
        ClearMessage();
    }

    private void DecimalButton_Click(object sender, RoutedEventArgs e)
    {
        if (_replaceDisplay)
        {
            UpdateDisplay("0" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
            _replaceDisplay = false;
            ClearMessage();
            return;
        }

        if (!DisplayTextBox.Text.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
        {
            UpdateDisplay(DisplayTextBox.Text + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
        }

        ClearMessage();
    }

    private void OperatorButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not string operation)
        {
            return;
        }

        if (_firstOperand.HasValue && _replaceDisplay)
        {
            _pendingOperator = operation;
            UpdateExpression();
            ClearMessage();
            return;
        }

        if (!TryGetDisplayValue(out var currentValue))
        {
            ShowMessage("Please enter a valid number.");
            return;
        }

        try
        {
            if (_firstOperand.HasValue && _pendingOperator is not null)
            {
                var result = ApplyOperation(_firstOperand.Value, currentValue, _pendingOperator);
                _firstOperand = result;
                UpdateDisplay(FormatValue(result));
                ResultTextBlock.Text = $"Result: {FormatValue(result)}";
            }
            else
            {
                _firstOperand = currentValue;
                ResultTextBlock.Text = string.Empty;
            }

            _pendingOperator = operation;
            _replaceDisplay = true;
            UpdateExpression();
            ClearMessage();
        }
        catch (DivideByZeroException)
        {
            ShowMessage("You cannot divide by zero. Please enter a non-zero second number.");
        }
        catch (InvalidOperationException)
        {
            ShowMessage("That operator is not supported. Please use +, -, *, /, or %.");
        }
    }

    private void EqualsButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_firstOperand.HasValue || _pendingOperator is null)
        {
            return;
        }

        if (!TryGetDisplayValue(out var currentValue))
        {
            ShowMessage("Please enter a valid number.");
            return;
        }

        try
        {
            var expression = $"{FormatValue(_firstOperand.Value)} {GetOperatorText(_pendingOperator)} {FormatValue(currentValue)} =";
            var result = ApplyOperation(_firstOperand.Value, currentValue, _pendingOperator);
            UpdateDisplay(FormatValue(result));
            ExpressionTextBlock.Text = expression;
            ResultTextBlock.Text = $"Result: {FormatValue(result)}";
            _firstOperand = null;
            _pendingOperator = null;
            _replaceDisplay = true;
            ClearMessage();
        }
        catch (DivideByZeroException)
        {
            ShowMessage("You cannot divide by zero. Please enter a non-zero second number.");
        }
        catch (InvalidOperationException)
        {
            ShowMessage("That operator is not supported. Please use +, -, *, /, or %.");
        }
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        UpdateDisplay("0");
        _replaceDisplay = true;
        ClearMessage();
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        _firstOperand = null;
        _pendingOperator = null;
        ResultTextBlock.Text = string.Empty;
        ExpressionTextBlock.Text = string.Empty;
        ClearButton_Click(sender, e);
    }

    private void ToggleSignButton_Click(object sender, RoutedEventArgs e)
    {
        if (!TryGetDisplayValue(out var currentValue))
        {
            ShowMessage("Please enter a valid number.");
            return;
        }

        UpdateDisplay(FormatValue(-currentValue));
        _replaceDisplay = false;
        ClearMessage();
    }

    private static double ApplyOperation(double firstNumber, double secondNumber, string operation) => operation switch
    {
        "+" => Calculator.Add(firstNumber, secondNumber),
        "-" => Calculator.Subtract(firstNumber, secondNumber),
        "*" => Calculator.Multiply(firstNumber, secondNumber),
        "/" => Calculator.Divide(firstNumber, secondNumber),
        "%" => Calculator.Percentage(firstNumber, secondNumber),
        _ => throw new InvalidOperationException("Unsupported operator.")
    };

    private bool TryGetDisplayValue(out double value) => double.TryParse(DisplayTextBox.Text, out value);

    private void UpdateDisplay(string value)
    {
        DisplayTextBox.Text = value;
    }

    private void UpdateExpression()
    {
        ExpressionTextBlock.Text = _firstOperand.HasValue && _pendingOperator is not null
            ? $"{FormatValue(_firstOperand.Value)} {GetOperatorText(_pendingOperator)}"
            : string.Empty;
    }

    private static string FormatValue(double value) => value.ToString("G", CultureInfo.CurrentCulture);

    private static string GetOperatorText(string operation) => operation switch
    {
        "/" => "÷",
        "*" => "×",
        _ => operation
    };

    private void ClearMessage()
    {
        MessageTextBlock.Text = string.Empty;
    }

    private void ShowMessage(string message)
    {
        ResultTextBlock.Text = string.Empty;
        MessageTextBlock.Text = message;
    }
}