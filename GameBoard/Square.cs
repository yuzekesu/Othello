using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    /// <summary>
    /// Class for the squares that the GameBoard is made up of. 
    /// </summary>
    public class Square : INotifyPropertyChanged
    {
        /// <summary>
        /// Attributes that a square has
        /// </summary>
        public int Row { get; set; }
        public int Column { get; set; }

        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                if (color == value) return;
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public Square() { }
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            Color = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// Abstract class of a Player
    /// </summary>
    public abstract class Player
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public abstract Task<Square> MakeMove(GameBoard board);
    }

    /// <summary>
    /// Human player which moves with clicks
    /// </summary>
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, string color) { Name = name; Color = color; }
        public override async Task<Square> MakeMove(GameBoard board)
        {
            return null;
        }
    }

    /// <summary>
    /// Computer player that moves by taking a random square from valid moves.
    /// </summary>
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name, string color) { Name = name; Color = color; }

        /// <summary>
        /// ComputerPlayer makes ther random move
        /// </summary>
        /// <param name="board"> Copy of the current gameboard</param>
        /// <returns></returns>
        public override async Task<Square> MakeMove(GameBoard board)
        {
            List<Square> validComputerMoves = board.GetValidMoves(this.Color);
            await Task.Delay(100);

            //If valid move exist then return a random selected move. Else return null if no move can be made
            if (validComputerMoves.Count() > 0)
            {
                Random randomGen = new Random();

                return validComputerMoves[randomGen.Next(validComputerMoves.Count())];
            }
            return null;
        }
    }

    // DEBUG COMPUTERPLAYER

    //public class ComputerPlayer : Player
    //{
    //    List<int> numbersR = new List<int> { 2, 5 };
    //    List<int> numbersC = new List<int> { 4, 6 };
    //    int pListLoc = 0;
    //    public ComputerPlayer(string name, string color) { Name = name; Color = color; pListLoc = 0; }
    //    public override async Task<Square> MakeMove(GameBoard board)
    //    {
    //        List<Square> validComputerMoves = board.GetValidMoves(this.Color);

    //        if (validComputerMoves.Count() > 0)
    //        {
    //            Random randomGen = new Random();

    //            if (pListLoc < 2)
    //            {
    //                Square sq = new Square(numbersR[pListLoc], numbersC[pListLoc]);
    //                pListLoc++;
    //                return sq;
    //            }

    //            return validComputerMoves[randomGen.Next(validComputerMoves.Count())];
    //        }
    //        return null;
    //    }
    //}
}
