using System;
using System.Text;
using System.Threading.Tasks;

namespace GameOfFifteen
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int size;
                while (true)
                {
                    Console.Clear();
                    Console.Write("Enter the size of the grid: ");
                    var sizeText = Console.ReadLine();
                    if (!string.IsNullOrEmpty(sizeText) && int.TryParse(sizeText, out size))
                        break;
                }
                
                var game = Game.NewGame(size);

                while (true)
                {
                    Console.Clear();
                    PrintGrid(game);

                    Console.WriteLine();
                    Console.WriteLine("Moved {0}", game.MoveCount == 1 ? "1 time" : game.MoveCount + " times.");
                    Console.WriteLine();
                    Console.Write("Enter your move(or q to quit): ");

                    var key = Console.ReadKey();

                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            game.MoveBy(Game.Left);
                            break;
                        case ConsoleKey.RightArrow:
                            game.MoveBy(Game.Right);
                            break;
                        case ConsoleKey.UpArrow:
                            game.MoveBy(Game.Up);
                            break;
                        case ConsoleKey.DownArrow:
                            game.MoveBy(Game.Down);
                            break;
                        default:
                            if (key.KeyChar == 'q')
                                return;
                            break;
                    }

                    if (game.Won())
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.Clear();
                    PrintGrid(game);

                    Console.WriteLine();
                    Console.WriteLine("You won in {0} moves!!", game.MoveCount);
                    Console.WriteLine();
                    Console.Write("Press 'q' to quit or 'n' key for a new game: ");

                    var key2 = Console.ReadKey(); ;
                    if (key2.KeyChar == 'q')
                        return;

                    if (key2.KeyChar == 'n')
                        break;
                }
            }
        }


        private static void PrintGrid(Game game)
        {
            for (int y = 0; y < game.Height; y++)
            {
                PrintRow(game, y, "      ", "┌────┐");
                PrintRow(game, y, "      ", "│ {0,2} │");
                PrintRow(game, y, "      ", "└────┘");
            }
        }

        private static void PrintRow(Game game, int y, string blankFormat, string numberFormat)
        {
            for (int x = 0; x < game.Width; x++)
            {
                var n = game[x, y].ToString();
                var correct = game.IsPositionCorrect(x, y);
                
                using (new ConsoleColorSetter(correct ? ConsoleColor.Green : ConsoleColor.White))
                {
                    if (n == "-1")
                    {
                        Console.Write(blankFormat);
                    }
                    else
                    {
                        Console.Write(numberFormat, n);
                    }
                }

            }
            Console.WriteLine();
        }
    }
}
