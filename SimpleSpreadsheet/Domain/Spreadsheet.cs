using System;
using System.Collections.Generic;

namespace Samples.SimpleSpreadsheet.Domain
{
    public class Spreadsheet
    {
        private readonly Dictionary<string, SpreadsheetCell> _cells;

        public Spreadsheet()
        {
            _cells = new Dictionary<string, SpreadsheetCell>();

            InitializeDefaultCells();
        }

        private void InitializeDefaultCells()
        {
            const int cellCount = 26;
            for (int i = 0; i < cellCount; i++)
            {
                var cellName = (char) ('A' + i);
                _cells.Add(cellName.ToString(), new SpreadsheetCell(this));
            }
        }

        public IEnumerable<string> GetAllCellNames()
        {
            return _cells.Keys;
        }

        public SpreadsheetCell GetCell(string name)
        {
            if (TryGetCell(name, out var cell))
            {
                return cell;
            }

            throw new InvalidOperationException($"Cell '{name}' not exists");
        }

        public bool TryGetCell(string name, out SpreadsheetCell spreadsheetCell)
        {
            return _cells.TryGetValue(name, out spreadsheetCell);
        }
    }
}