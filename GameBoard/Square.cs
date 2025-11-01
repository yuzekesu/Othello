using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    public class Square : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public abstract class Player
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public abstract Task<Square> MakeMove(GameBoard board);
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, string color) { Name = name; Color = color; }
        public override async Task<Square> MakeMove(GameBoard board)
        {
            return null;
        }
    }

    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name, string color) { Name = name; Color = color; }
        public override async Task<Square> MakeMove(GameBoard board)
        {
            List<Square> validComputerMoves = board.GetValidMoves(this.Color);
            await Task.Delay(100);
            if (validComputerMoves.Count() > 0)
            {
                Random randomGen = new Random();
                
                return validComputerMoves[randomGen.Next(validComputerMoves.Count())];
            }
            return null;
        }
    }
}
