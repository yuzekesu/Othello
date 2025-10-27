using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
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



    //TEMP Player since it is not done yet
    public class Player
    {
        public string Name { get; set; }
        public string Color { set; get; }
        public Player() { }
        public Player(string name, string color) { Name = name; Color = color; }
    }
}
