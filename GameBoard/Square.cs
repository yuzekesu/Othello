using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
    public class Square
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public string Color { get; set; }

        public Square() { }
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            Color = null;
        }

        //ADD PropertyChangedEventHandler
    }
}
