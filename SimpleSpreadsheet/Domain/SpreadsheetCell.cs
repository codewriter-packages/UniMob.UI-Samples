using System;
using System.Globalization;
using UniMob;

namespace Samples.SimpleSpreadsheet.Domain
{
    public class SpreadsheetCell : ILifetimeScope
    {
        private readonly Spreadsheet _spreadsheet;

        public SpreadsheetCell(Spreadsheet spreadsheet, Lifetime lifetime)
        {
            _spreadsheet = spreadsheet;
            Lifetime = lifetime;
        }

        public Lifetime Lifetime { get; }

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