using System;
using System.Collections.Generic;
using UniMob;

namespace Samples.SimpleSpreadsheet.Domain
{
    public class Spreadsheet : ILifetimeScope
    {
        private readonly Dictionary<string, SpreadsheetCell> _cells;

        public Spreadsheet(Lifetime lifetime)
        {
            Lifetime = lifetime;
            _cells = new Dictionary<string, SpreadsheetCell>();

            InitializeDefaultCells();
        }

        public Lifetime Lifetime { get; }

        private void InitializeDefaultCells()
        {
            const int cellCount = 26;
            for (int i = 0; i < cellCount; i++)
            {
                var cellName = (char) ('A' + i);
                _cells.Add(cellName.ToString(), new SpreadsheetCell(this, Lifetime));
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