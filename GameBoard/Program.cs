namespace GameBoard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameBoard gameBoard = new GameBoard();
            bool gameActive = true;


            while (gameActive)
            {
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
                                Console.Write(" _");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
                Console.ReadLine();
            }
        }
    }
}
