using UniMob;
using UniMob.UI;
using Samples.SimpleSpreadsheet.Domain;

namespace Samples.SimpleSpreadsheet.Presentation
{
    public class SpreadsheetCellWidget : StatefulWidget
    {
        public SpreadsheetCellWidget(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override State CreateState() => StateProvider.Of(this);
    }

    public class SpreadsheetCellState : ViewState<SpreadsheetCellWidget>
    {
        private readonly Spreadsheet _spreadsheet;

        public SpreadsheetCellState(WidgetViewReference view, Spreadsheet spreadsheet)
        {
            _spreadsheet = spreadsheet;
            View = view;
        }

        public override WidgetViewReference View { get; }

        [Atom] public string Name => Widget.Name;
        [Atom] public string Value => SpreadsheetCell.DisplayValue;
        [Atom] public string Formula => SpreadsheetCell.Formula;

        private SpreadsheetCell SpreadsheetCell => _spreadsheet.GetCell(Widget.Name);

        public void OnFormulaChanged(string formula)
        {
            SpreadsheetCell.Formula = formula;
        }
    }
}