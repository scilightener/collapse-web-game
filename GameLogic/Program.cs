using System.Drawing;

namespace GameLogic
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const int x = 3; const int y = 3;
            // TODO: test game
            var p1 = new Player(1, "test1", Color.Blue);
            var p2 = new Player(2, "test2", Color.Green);
            var board = new Board(x, y, p1, p2);
            var move = 1;
            Console.WriteLine("Commands: 'exit' - exit, 'x y' - make move");
            Console.WriteLine("Please, push your enter button.");
            Console.ReadLine();
            while (true)
            {
                Console.Clear();
                for (var i = 0; i < x; i++)
                {
                    for (var j = 0; j < y; j++)
                    {
                        var cell = board[i, j];
                        // shitty code ughh
                        // but i found out that there is no clear conversion between ConsoleColor and System.Drawing.Color :(
                        Console.ForegroundColor = cell.Owner == p1 ? ConsoleColor.Blue :
                            cell.Owner == p2 ? ConsoleColor.Green : ConsoleColor.White;
                        Console.Write(board[i, j].CountPoints + "\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine();
                }

                Console.Write($"make your move, ");
                Console.ForegroundColor = move % 2 == 1 ? ConsoleColor.Blue : ConsoleColor.Green;
                Console.WriteLine($"Player{(move - 1) % 2 + 1}");
                Console.ForegroundColor = ConsoleColor.White;
                
                var cmd = Console.ReadLine()!;
                if (cmd == "exit") break;
                var moveX = int.Parse(cmd.Split()[0]);
                var moveY = int.Parse(cmd.Split()[1]);
                var proceeded = board.MakeMove(move % 2 == 1 ? p1 : p2, moveX, moveY);
                if (!proceeded)
                {
                    Console.WriteLine("Error. Try again. Push your enter button to continue");
                    Console.ReadLine();
                    continue;
                }
                move++;
            }
        }
    }
}

