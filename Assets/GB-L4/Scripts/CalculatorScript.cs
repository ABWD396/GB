using System;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorScript : MonoBehaviour
{
    [SerializeField]
    private Text outputText;

    private InputState inputState = 0;
    private string inputNumber = "";
    private string savedNumber = "";
    private Command commandPressed = 0;
    private string commandPressedString = "";

    public void ButtonPressed(Button button)
    {
        HandleText(button.GetComponentInChildren<Text>().text);
    }

    private void HandleText(String inputText)
    {
        bool isNumber = byte.TryParse(inputText, out _);

        if (isNumber)
        {
            HandleNumber(inputText);
        }
        else
        {
            Command command = GetCommand(inputText);
            if (command == Command.Dot)
            {
                HandleDot();
            }
            else
            {
                HandleCommand(command, inputText);
            }
        }

        Output();
    }

    private void Output()
    {
        if (HasInputState(InputState.COMMAND_PRESSED))
        {
            PrintText(savedNumber + commandPressedString + inputNumber);
        }
        else
        {
            PrintText(inputNumber);
        }
    }

    private void HandleDot()
    {
        if (HasInputState(InputState.DOT_PRESSED))
        {
            return;
        }

        if (inputNumber.Length == 0)
        {
            inputNumber = "0.";
        }
        else
        {
            inputNumber += ".";
        }

        SetInputState(InputState.DOT_PRESSED);

    }
    private void HandleCommand(Command command, String commandText)
    {
        switch (command)
        {
            case Command.Cancel:
                Reset();
                break;
            case Command.Backspace:
                Backspace();
                break;
            case Command.Invert:
                Invert();
                break;
            case Command.Equal:
                Calculate();
                break;
            default:
                HandleAriphmeticCommand(command, commandText);
                break;
        }
    }

    private void Invert()
    {
        if (inputNumber.Length > 0)
        {
            string firstSymbol = inputNumber[..1];
            inputNumber = firstSymbol == "-" ? inputNumber[1..] : $"-{inputNumber}";
        }
    }

    private void Reset()
    {
        inputState = 0;
        inputNumber = "";
        savedNumber = "";
        commandPressed = 0;
        commandPressedString = "";
    }
    private void Backspace()
    {
        if (inputNumber.Length > 0)
        {
            if (inputNumber.EndsWith('.'))
            {
                UnsetInputState(InputState.DOT_PRESSED);
            }

            inputNumber = inputNumber[..^1];
        }
    }

    private void HandleAriphmeticCommand(Command command, string commandText)
    {
        if (!HasInputState(InputState.COMMAND_PRESSED))
        {
            savedNumber = inputNumber;
            inputNumber = "";
            UnsetInputState(InputState.DOT_PRESSED);
            SetInputState(InputState.COMMAND_PRESSED);
        }

        commandPressed = command;
        commandPressedString = commandText;
    }

    private void HandleNumber(string number)
    {
        inputNumber += number;
    }

    private void Calculate()
    {
        if (!HasInputState(InputState.COMMAND_PRESSED))
        {
            return;
        }

        double secondNumber = Math.Round(double.Parse(inputNumber), 4);
        double firstNumber = Math.Round(double.Parse(savedNumber), 4);

        double result = 0;

        switch (commandPressed)
        {
            case Command.Plus:
                result = firstNumber + secondNumber;
                break;
            case Command.Minus:
                result = firstNumber - secondNumber;
                break;
            case Command.Multiply:
                result = firstNumber * secondNumber;
                break;
            case Command.Divide:
                if (secondNumber == 0)
                {
                    return;
                }
                result = firstNumber / secondNumber;
                break;
        }

        Reset();
        inputNumber = result.ToString();
        if (inputNumber.Contains('.'))
        {
            SetInputState(InputState.DOT_PRESSED);
        }
    }

    private Command GetCommand(String command)
    {
        switch (command)
        {
            case "+/-":
                return Command.Invert;
            case "+":
                return Command.Plus;
            case "-":
                return Command.Minus;
            case "*":
                return Command.Multiply;
            case "/":
                return Command.Divide;
            case "=":
                return Command.Equal;
            case "<-":
                return Command.Backspace;
            case "C":
                return Command.Cancel;
            case ".":
                return Command.Dot;
            default:
                return Command.NotCommand;
        }
    }
    private void PrintText(String text)
    {
        outputText.text = text;
    }

    private bool HasInputState(InputState searchState)
    {
        return (inputState & searchState) == searchState;
    }
    private void SetInputState(InputState setState)
    {
        inputState |= setState;
    }
    private void UnsetInputState(InputState unsetState)
    {
        inputState &= ~unsetState;
    }

}

enum Command : byte
{
    Invert = 0,
    Plus = 1,
    Minus = 2,
    Multiply = 3,
    Divide = 4,
    Equal = 5,
    Backspace = 6,
    Cancel = 7,
    Dot = 8,
    NotCommand = 127,
}

[Flags]
enum InputState : byte
{
    DOT_PRESSED = 1,
    COMMAND_PRESSED = 2,
}