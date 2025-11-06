using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    public class GameBoard
    {
        /// <summary>
        /// The two-dimensional array that the GameBoard is made up of 
        /// </summary>
        public Square[,] Squares { get; set; }

        /// <summary>
        /// Constructor to make a new Square array and to initialice squares for the board
        /// </summary>
        public GameBoard()
        {
            Squares = new Square[8, 8];
            InitializeBoard();
        }

        /// <summary>
        /// Creates the board. Gives each square their row/column value and sets the startingposition of the 4 discs in the middle of the gameboard
        /// </summary>
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

        /// <summary>
        /// Gets all the valid moves that a player can make
        /// </summary>
        /// <param name="color"> The color of the player</param>
        /// <returns> A list of all the squares that are valid moves </returns>
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
                    //Only checks valid moves if the square has no color (no disc already placed on that location)
                    if (Squares[row, col].Color == null)
                    {
                        //If the square has no color check each direction 
                        for (int dirRow = -1; dirRow <= 1; dirRow++)
                        {
                            for (int dirCol = -1; dirCol <= 1; dirCol++)
                            {
                                int r = row + dirRow;
                                int c = col + dirCol;
                                bool captureFound = false;

                                //While loop to stop the loops from going outside the gameboard
                                while (r >= 0 && r < 8 && c >= 0 && c < 8)
                                {
                                    
                                    if (Squares[r, c].Color == color)
                                    {
                                        //If the loop has found one/many discs of opponent color and then one of their own then it is a valid move
                                        if (captureFound)
                                        {
                                            moves.Add(Squares[row, col]);
                                        }
                                        break;
                                    }
                                    else if (Squares[r, c].Color == opponentColor)
                                    {
                                        //Opponent found in the current direction
                                        captureFound = true;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    //Move one square away in the current direction
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

        /// <summary>
        /// Gets all the flippable discs after a move is made
        /// </summary>
        /// <param name="row"> The row of the move that was made </param>
        /// <param name="col"> The column of the move that was made </param>
        /// <param name="color"> The color of the player that made the move </param>
        /// <returns> Returns all the squares(discs) of enemy player that can be flipped </returns>
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

            //Loops trough the directions (similar to GetValidMoves)
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
                            //Add square to a temporary list if the square is of opponent color
                            tempFlippableDiscs.Add(Squares[r, c]);
                        }
                        else if (Squares[r,c].Color == color) 
                        {
                            //Add all the squares in the temporary list if a square of same color is found
                            flippableDiscs.AddRange(tempFlippableDiscs);
                            break;
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

            return flippableDiscs;
        }


        //GameBoard "applies a move" (Swaps the color of the square)
        public void ApplyMove(int row, int col, string color)
        {
            Squares[row, col].Color = color;
        }

        /// <summary>
        /// Checks if the board is full
        /// </summary>
        /// <returns> True if the board is full </returns>
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

        /// <summary>
        /// Counts the discs (squares) of a player
        /// </summary>
        /// <param name="color"> Color of the player </param>
        /// <returns> Count of discs(squares) as int </returns>
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
