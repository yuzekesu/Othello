using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoard
{
    public class GameBoard
    {
        public Square[,] Squares { get; set; }

        public GameBoard()
        {
            Squares = new Square[8, 8];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Squares[row, col] = new Square(row, col);
                }
            }

            Squares[3, 3].Color = "White";
            Squares[4, 4].Color = "White";
            Squares[3, 4].Color = "Black";
            Squares[4, 3].Color = "Black";
        }

        public List<Square> GetValidMoves(string color)
        {
            var moves = new List<Square>();


            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (Squares[row, col].Color == null)
                    {
                        int r = row;
                        int c = col;
                        bool opponentFound = false;
                        for (int dirRow = -1; dirRow <= 1; dirRow++)
                        {
                            for (int dirCol = -1; dirCol <= 1; dirCol++)
                            {

                            }
                        }




                    }
                }
            }


            return moves;
        }

        public List<Square> GetFlippableDiscs(int row, int col, string color)
        {
            var flippable = new List<Square>();

            return flippable;
        }


        public bool ApplyMove(int row, int col, string color)
        {
            return true;
        }

        public bool IsFull()
        {
            return false;
        }

        public int CountDiscs(string color)
        {
            return 0;
        }
    }
}
