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
        public ComputerPlayer(string name, string color) { Name = name; Color = color;}
        public override async Task<Square> MakeMove(GameBoard board)
        {
            List<Square> validComputerMoves = board.GetValidMoves(this.Color);
            await Task.Delay(500);
            if (validComputerMoves.Count() > 0)
            {
                Random randomGen = new Random();

                return validComputerMoves[randomGen.Next(validComputerMoves.Count())];
            }
            return null;
        }
    }
}
