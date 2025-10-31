using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    public class GameBoard
    {
        public Square[,] Squares { get; set; }

        public GameBoard()
        {
            Squares = new Square[8, 8];
            InitializeBoard();
        }

        private void InitializeBoard()
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
            List<Square> moves = new List<Square>();
            string opponentColor;
            if (color == "White")
            {
                opponentColor = "Black";
            }
            else
            {
                opponentColor = "White";
            }

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (Squares[row, col].Color == null)
                    {
                        for (int dirRow = -1; dirRow <= 1; dirRow++)
                        {
                            for (int dirCol = -1; dirCol <= 1; dirCol++)
                            {
                                int r = row + dirRow;
                                int c = col + dirCol;
                                bool captureFound = false;

                                while (r >= 0 && r < 8 && c >= 0 && c < 8)
                                {
                                    if (Squares[r, c].Color == color)
                                    {
                                        if (captureFound)
                                        {
                                            moves.Add(Squares[row, col]);
                                        }
                                        break;
                                    }
                                    else if (Squares[r, c].Color == opponentColor)
                                    {
                                        captureFound = true;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    r += dirRow;
                                    c += dirCol;
                                }
                            }
                        }
                    }
                }
            }


            return moves;
        }

        public List<Square> GetFlippableDiscs(int row, int col, string color)
        {
            List<Square> flippableDiscs = new List<Square>();
            string opponentColor;
            if (color == "White")
            {
                opponentColor = "Black";
            }
            else
            {
                opponentColor = "White";
            }

            for (int dirRow = -1; dirRow <= 1; dirRow++)
            {
                for (int dirCol = -1; dirCol <= 1; dirCol++)
                {
                    int r = row + dirRow;
                    int c = col + dirCol;
                    List<Square> tempFlippableDiscs = new List<Square>();

                    while (r >= 0 && r < 8 && c >= 0 && c < 8)
                    {
                        if (Squares[r, c].Color == opponentColor)
                        {
                            tempFlippableDiscs.Add(Squares[r, c]);
                        }
                        else if (Squares[r,c].Color == color) 
                        {
                            flippableDiscs.AddRange(tempFlippableDiscs);
                            break;
                        }
                        else 
                        {
                            break; 
                        }
                        r = r + dirRow;
                        c = c + dirCol;
                    }
                }
            }

            return flippableDiscs;
        }


        public bool ApplyMove(int row, int col, string color)
        {
            List<Square> 
            List<Square> discToFlip = GetFlippableDiscs(row, col, color);

            if (discToFlip.Count == 0)
            {
                return false;
            }

            Squares[row, col].Color = color;

            foreach (Square disc in discToFlip)
            {
                disc.Color = color;
            }
            
            return true;
        }

        public bool IsFull()
        {
            foreach (Square square in Squares)
            {
                if (square.Color == null)
                {
                    return false;
                }
            }
            return true;
        }

        public int CountDiscs(string color)
        {
            int discCount = 0;

            foreach (Square square in Squares)
            {
                if (square.Color == color)
                {
                    discCount++;
                }
            }

            return discCount;
        }
    }
}
