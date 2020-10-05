using System;
using System.Collections.Generic;
using System.Globalization;

namespace Samples.SimpleSpreadsheet.Domain
{
    public static class Calculator
    {
        private delegate double OperatorDelegate(double a, double b);
        
        private static readonly Dictionary<char, OperatorDelegate> Operators;

        static Calculator()
        {
            Operators = new Dictionary<char, OperatorDelegate>
            {
                ['+'] = (a, b) => a + b,
                ['-'] = (a, b) => a - b,
                ['*'] = (a, b) => a * b,
                ['/'] = (a, b) => a / b,
            };
        }

        public static double? CalculateValue(Spreadsheet spreadsheet, string formula)
        {
            formula = formula.Trim();

            if (string.IsNullOrWhiteSpace(formula))
            {
                return null;
            }

            if (double.TryParse(formula, NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
            {
                return number;
            }

            if (spreadsheet.TryGetCell(formula, out var cell))
            {
                return cell.RawValue;
            }

            foreach (var op in Operators)
            {
                var index = formula.IndexOf(op.Key);
                if (index == -1) continue;

                var leftFormula = formula.Substring(0, index);
                var rightFormula = formula.Substring(index + 1);

                var leftNumber = CalculateValue(spreadsheet, leftFormula);
                var rightNumber = CalculateValue(spreadsheet, rightFormula);
                if (!leftNumber.HasValue || !rightNumber.HasValue) continue;

                return op.Value.Invoke(leftNumber.Value, rightNumber.Value);
            }

            throw new Exception("Invalid formula");
        }
    }
}