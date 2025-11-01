using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public abstract class Player
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public abstract Task<Square> MakeMove(GameBoard board);
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, string color) { Name = name; Color = color; }
        public override Task<Square> MakeMove(GameBoard board)
        {
            return null;
        }
    }

    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name, string color) { Name = name; Color = color; }
        public override Task<Square> MakeMove(GameBoard board)
        {
            //Some logic and return the square that the logic makes instead of null
            return null;
        }
    }
}
