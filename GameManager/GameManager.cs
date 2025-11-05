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
        /// <summary>
        /// Attributes that a GameManager uses to "play" a game of othello
        /// </summary>
        public GameBoard Board { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set; }

        /// <summary>
        /// Events for MainViewModel
        /// </summary>
        public event Action<Square[,]> BoardUpdated;
        public event Action<Player> GameWon;
        public event Action GameDrawn;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p1"> The first player </param>
        /// <param name="p2"> The second player </param>
        public GameManager(Player p1, Player p2) 
        {
            Board = new GameBoard();
            Player1 = p1;
            Player2 = p2;
            CurrentPlayer = p1;
        }

        /// <summary>
        /// Logic for the GameManager to play a certain move.
        /// </summary>
        /// <param name="row"> The row "coordinate" that is to be played </param>
        /// <param name="col"> The column "coordinate" that is to be played</param>
        /// <returns> Returns false or true depedning on if the move was applied or not (if it was valid or not) </returns>
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

                Player oppositePlayer;

                if (CurrentPlayer == Player1)
                {
                    oppositePlayer = Player2;
                }
                else
                {
                    oppositePlayer = Player1;
                }

                //Dont swap players if the other player has no valid moves. (player skips their move if no valid moves)
                if (Board.GetValidMoves(oppositePlayer.Color).Count() != 0)
                {
                    SwapPlayers();
                }

                BoardUpdated?.Invoke(Board.Squares);
                IsGameOver();

                return true;
                
            }
            
            return false;
        }

        /// <summary>
        /// Tries to make a move, if move is made by computer then return it
        /// </summary>
        /// <returns> The move that is made </returns>
        public async Task<Square> TryComputerMoveAsync()
        {
            if (Player1 is HumanPlayer || Player2 is HumanPlayer)
            {
                await Task.Delay(500);
            }
            Square computerSquare = await CurrentPlayer.MakeMove(Board);
            if (computerSquare != null)
            {
                PlayMove(computerSquare.Row, computerSquare.Column);
                return computerSquare;
            }
            return new Square(-1,-1);
        }

        /// <summary>
        /// Swaps the players
        /// </summary>
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

        /// <summary>
        /// Checks if the game is over and if it is over then invoke the correct event
        /// </summary>
        private void IsGameOver()
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
                
            }
        }

    }
}
