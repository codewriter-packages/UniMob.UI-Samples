using System.Linq;
using UnityEngine;
using UniMob.UI;
using UniMob.UI.Widgets;
using Samples.SimpleSpreadsheet.Domain;
using Samples.SimpleSpreadsheet.Presentation;

namespace Samples.SimpleSpreadsheet
{
    public class SpreadsheetApp : UniMobUIApp
    {
        private readonly WidgetViewReference _cellView = WidgetViewReference.Resource("SpreadsheetCell");

        private Spreadsheet _spreadsheet;

        protected override void Initialize()
        {
            _spreadsheet = new Spreadsheet();

            _spreadsheet.GetCell("A").Formula = "2";
            _spreadsheet.GetCell("B").Formula = "3";
            _spreadsheet.GetCell("C").Formula = "5";
            _spreadsheet.GetCell("D").Formula = "A + B * C + 1";

            StateProvider.Register<SpreadsheetCellWidget>(() => new SpreadsheetCellState(_cellView, _spreadsheet));
        }

        protected override Widget Build(BuildContext context)
        {
            return new Container
            {
                BackgroundColor = Color.white,
                Size = WidgetSize.Stretched,
                Child = BuildContent(),
            };
        }

        private Widget BuildContent()
        {
            return new ScrollList
            {
                MainAxisAlignment = MainAxisAlignment.Center,
                CrossAxisAlignment = CrossAxisAlignment.Center,
                Children =
                {
                    _spreadsheet.GetAllCellNames().Select(BuildCell)
                }
            };
        }

        private Widget BuildCell(string cellName)
        {
            return new SpreadsheetCellWidget(cellName)
            {
                Key = Key.Of(cellName)
            };
        }
    }
}