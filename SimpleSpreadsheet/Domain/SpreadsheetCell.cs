using System;
using System.Globalization;
using UniMob;

namespace Samples.SimpleSpreadsheet.Domain
{
    public class SpreadsheetCell
    {
        private readonly Spreadsheet _spreadsheet;

        public SpreadsheetCell(Spreadsheet spreadsheet)
        {
            _spreadsheet = spreadsheet;
        }

        [Atom] public string Formula { get; set; } = string.Empty;

        [Atom] public double? RawValue => Calculator.CalculateValue(_spreadsheet, Formula);

        [Atom] public string DisplayValue
        {
            get
            {
                try
                {
                    var number = RawValue;
                    if (!number.HasValue)
                    {
                        return string.Empty;
                    }

                    return number.Value.ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    var error = e is CyclicAtomDependencyException
                        ? "Cyclic dependency"
                        : e.Message;

                    return $"#ERR ({error})";
                }
            }
        }
    }
}