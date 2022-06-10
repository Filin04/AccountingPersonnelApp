using System.Collections.Generic;

namespace AccountingPersonnelApp.Models
{
    public class PositionList
    {
        private static IEnumerable<Position> positions;

        public static IEnumerable<Position> Positions { get => positions; set => positions = value; }
    }
}
