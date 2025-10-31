namespace Othello.Model
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player p1 = new Player("p1", "Black");
            Player p2 = new Player("p2", "White");

            GameManager gameManager = new GameManager(p1, p2);

            while (true)
            {
                //Needs a valid moves to be called in the start of a round if we want to paint out the valid moves
                List<Square> validMoves = gameManager.Board.GetValidMoves(gameManager.CurrentPlayer.Color);


                Console.WriteLine($"Current player: {gameManager.CurrentPlayer.Name}, Color: {gameManager.CurrentPlayer.Color}");
                foreach (Square square in validMoves)
                {
                    Console.Write($"({square.Column + 1}, {square.Row + 1}) ");
                }
                Console.WriteLine();


                for (int r = 0; r < 8; r++)
                {
                    for (int c = 0; c < 8; c++)
                    {
                        switch (gameManager.Board.Squares[r, c].Color)
                        {
                            case "White":
                                Console.Write(" W");
                                break;
                            case "Black":
                                Console.Write(" B");
                                break;
                            default:
                                if (validMoves.Contains(gameManager.Board.Squares[r, c]))
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


                    if (gameManager.PlayMove(rowCoord, colCoord))
                    {
                        moveApplied = true;
                    }

                }

            }
        }
    }
}
