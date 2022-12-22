using System.Drawing;

namespace GameLogic
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const int x = 3; const int y = 3;
            // TODO: test game
            var p1 = new Player(1, "Andrew", Color.Blue);
            var p2 = new Player(2, "Nikita", Color.Green);
            var players = new[] { p1, p2 };
            var board = new Board(x, y, players);
            var move = 1;
            Console.WriteLine("Commands: 'exit' - exit, 'x y' - make move");
            Console.WriteLine("Please, push your enter button.");
            Console.ReadLine();
            while (true)
            {
                Console.Clear();
                var currentPlayer = move % 2 == 1 ? p1 : p2;
                for (var i = 0; i < x; i++)
                {
                    for (var j = 0; j < y; j++)
                    {
                        var cell = board[i, j];
                        // shitty code ughh
                        // but i found out there is no clear conversion between ConsoleColor and System.Drawing.Color :(
                        WriteColorfully(board[i, j].CountPoints + "\t", cell.Owner == p1 ? ConsoleColor.Blue :
                            cell.Owner == p2 ? ConsoleColor.Green : ConsoleColor.White);
                    }
                    Console.WriteLine();
                }
                if (board.GetPlayerStatus(currentPlayer) == PlayerStatus.Looser)
                {
                    WriteColorfully(currentPlayer.Name, ConsoleColor.Yellow);
                    Console.WriteLine(" is a looser. GG.");
                }
                if (board.Status == GameStatus.Ended)
                    break;

                Console.Write($"make your move, ");
                WriteColorfully(currentPlayer.Name, currentPlayer == p1 ? ConsoleColor.Blue : ConsoleColor.Green);
                Console.WriteLine();
                
                var cmd = Console.ReadLine()!;
                if (cmd == "exit") break;
                var splited = cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (splited.Length != 2 ||
                    !int.TryParse(splited[0], out var moveX) || !int.TryParse(splited[1], out var moveY) ||
                    !board.MakeMove(currentPlayer, moveX, moveY))
                {
                    WriteColorfully("Error. Try again. Push your enter button to continue\n", ConsoleColor.Red);
                    Console.ReadLine();
                    continue;
                }
                move++;
            }

            if (board.Status != GameStatus.Ended) return;
            var winner = players.First(p => board.GetPlayerStatus(p) == PlayerStatus.Winner);
            WriteColorfully(winner.Name, ConsoleColor.Magenta);
            Console.WriteLine(" is a winner! Congrats!");
        }

        private static void WriteColorfully(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

