using System.Drawing;

namespace Othello.Model
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameBoard gameBoard = new GameBoard();
            Player p1 = new Player("p1", "Black");
            Player p2 = new Player("p2", "White");
            Player currentPlayer = p1;

            while (!gameBoard.IsFull())
            {
                List<Square> validMoves = gameBoard.GetValidMoves(currentPlayer.Color);

                Console.WriteLine($"Current player: {currentPlayer.Name}, Color: {currentPlayer.Color}");
                foreach (Square square in validMoves)
                {
                    Console.Write($"({square.Column+1}, {square.Row + 1}) ");
                }
                Console.WriteLine();

                for (int r = 0; r < 8; r++) 
                {
                    for (int c = 0; c < 8; c++)
                    {
                        switch (gameBoard.Squares[r, c].Color)
                        {
                            case "White":
                                Console.Write(" W");
                                break;
                            case "Black":
                                Console.Write(" B");
                                break;
                            default:
                                if (validMoves.Contains(gameBoard.Squares[r, c]))
                                { 
                                    Console.Write(" O"); 
                                }
                                else 
                                { 
                                    Console.Write(" _"); 
                                }
                                break;
                        }
                                
                    }
                    Console.WriteLine();
                }
                bool moveApplied = false;
                
                while (moveApplied == false)
                {
                    
                    var coordinates = Console.ReadLine().Split(' ');

                    //out of bounds if other then number
                    int rowCoord = Convert.ToInt16(coordinates[1]) - 1;
                    int colCoord = Convert.ToInt16(coordinates[0]) - 1;

                    
                    if (validMoves.Contains(gameBoard.Squares[rowCoord, colCoord]))
                    {
                        gameBoard.ApplyMove(rowCoord, colCoord, currentPlayer.Color);
                        

                        if (currentPlayer == p1)
                        {
                            currentPlayer = p2;
                        } 
                        else
                        {
                            currentPlayer = p1;
                        }

                            moveApplied = true;

                    }

                }

            }
        }
    }
}
