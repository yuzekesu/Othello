using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Board = new GameBoard();
            Player1 = p1;
            Player2 = p2;
            CurrentPlayer = p1;
        }

        public bool PlayMove(int row, int col)
        {
            List<Square> valMoves = Board.GetValidMoves(CurrentPlayer.Color);

            if (valMoves.Count() != 0 && valMoves.Contains(Board.Squares[row, col]))
            {
                Board.ApplyMove(row, col, CurrentPlayer.Color);
           
                List<Square> discToFlip = Board.GetFlippableDiscs(row, col, CurrentPlayer.Color);

                foreach (Square square in discToFlip)
                {
                    square.Color = CurrentPlayer.Color;
                }


                SwapPlayers();
                BoardUpdated.Invoke(Board.Squares);
                if (IsGameOver())
                {
                    EndGame();
                }

                return true;
                
            }
            
            return false;
        }

        public void SwapPlayers()
        {
            if (CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
            }
            else
            {
                CurrentPlayer = Player1;
            }
        }

        private Player GetPlayer(string color)
        {
            if (Player1.Color == color)
            {
                return Player1;
            }
            else
            {
                return Player2;
            }
        }

        private void EndGame()
        {
            //Something
        }

        private bool IsGameOver()
        {
            if (Board.IsFull() || Board.GetValidMoves(Player1.Color).Count() == 0 && Board.GetValidMoves(Player2.Color).Count() == 0)
            {
                int score1 = Board.CountDiscs(Player1.Color);
                int score2 = Board.CountDiscs(Player2.Color);
                if (score1 > score2)
                {
                    GameWon.Invoke(Player1);
                }
                else if (score2 > score1)
                {
                    GameWon.Invoke(Player2);
                }
                else
                {
                    GameDrawn.Invoke();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
