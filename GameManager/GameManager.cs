using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    public class GameManager
    {
        public GameBoard Board { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set; }

        public event Action<Square[,]> BoardUpdated;
        public event Action<Player> GameWon;
        public event Action GameDrawn;

        public GameManager(Player p1, Player p2) 
        { 
            Player1 = p1;
            Player2 = p2;
            CurrentPlayer = p1;
        }

        public bool PlayMove(int row, int col)
        {
            return true;
        }
        

    }
}
