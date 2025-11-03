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
        List<int> numbersR = new List<int> { 2, 5 };
        List<int> numbersC = new List<int> { 4, 6 };
        int pListLoc = 0;
        public ComputerPlayer(string name, string color) { Name = name; Color = color; pListLoc = 0; }
        public override async Task<Square> MakeMove(GameBoard board)
        {
            List<Square> validComputerMoves = board.GetValidMoves(this.Color);
            await Task.Delay(1000);
            if (validComputerMoves.Count() > 0)
            {
                Random randomGen = new Random();

                if (pListLoc < 2)
                {
                    Square sq = new Square(numbersR[pListLoc], numbersC[pListLoc]);
                    pListLoc++;
                    return sq;
                }

                return validComputerMoves[randomGen.Next(validComputerMoves.Count())];
            }
            return null;
        }
    }
}
