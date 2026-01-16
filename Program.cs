using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

record struct Player(string? Name, int Position, int Pieces);

namespace Ludo
{
    internal class Program
    {
        Random rnd = new Random();
        const int palya = 52;
        public Program()
        {
            
        }
        public void Bevezető()
        {
            Console.WriteLine("Köszöntünk a a ludo (vagy hazai fordításban a ki nevet a végén) nevű konzolos játékunkban");
            Console.WriteLine("Készítette: Levi, Norbi, Szabi");
            Console.Write("Add meg az első játékos nevét: ");
        }   
        public void Jatek()
        {
            List<Player> players = new List<Player>();
            Console.Write("Mennyi játékossal szertnél játszani? ");
            int.TryParse(Console.ReadLine(), out int playerCount);

            for (int i = 0; i < playerCount; i++)
            {
                Console.Write("Add meg a játékos nevét: ");
                string name = Console.ReadLine();
                if (name != players[i].Name)
                {
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        players.Add(new Player(name, 0, 4));
                    }
                    else
                    {
                        Console.WriteLine("Nem adtál meg nevet, próbáld újra.");
                        i--;
                    }
                }
                else
                    Console.WriteLine("Ez a név már foglalt"); i--;
            }

            for (int idx = 0; idx < players.Count; idx++)
            {
                Player p = players[idx];
                while (p.Pieces != 0)
                {
                    while (p.Position < palya)
                    {
                        int dobas = rnd.Next(1, 7);

                        bool hatvan = false;
                        if (hatvan is false)
                        {

                            if (dobas != 6)
                            {

                                Console.WriteLine($"{p.Name} még nem mehet ki mivel {dobas} dobot");
                            }
                            if (dobas == 6)
                            {

                                Console.WriteLine($"{p.Name} 6-ost dobot ki mehet");

                            }
                        }
                        if (hatvan is true)
                        {
                            Console.WriteLine($"{p.Name} {dobas} -est dobott, így {p.Position} -re lépett");
                            p.Position += dobas;
                        }

                        if (p.Position >= palya)
                        {
                            Console.WriteLine(p.Name + (p.Pieces - 1) + " bábuja van");
                            p.Pieces -= 1;
                            p.Position = 0;
                        }

                    }
                    if (p.Pieces == 0)
                    {
                        Console.WriteLine(p.Name + " nyert!");
                        Restart();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.ReadKey(intercept: true);
                    }
                }
            }
        }

        public void Palya()
        {
            char[,] board =
            {
                {'G','G','G','G','G','G','.','.','.','Y','Y','Y','Y','Y','Y'},
                {'G','G','G','G','G','G','.','.','.','Y','Y','Y','Y','Y','Y'},
                {'G','G','G','G','G','G','.','.','.','Y','Y','Y','Y','Y','Y'},
                {'G','G','G','G','G','G','.','.','.','Y','Y','Y','Y','Y','Y'},
                {'G','G','G','G','G','G','.','.','.','Y','Y','Y','Y','Y','Y'},
                {'G','G','G','G','G','G','.','.','.','Y','Y','Y','Y','Y','Y'},
                {'.','.','.','.','.','.','F','F','F','.','.','.','.','.','.'},
                {'.','.','.','.','.','.','F','F','F','.','.','.','.','.','.'},
                {'.','.','.','.','.','.','F','F','F','.','.','.','.','.','.'},
                {'R','R','R','R','R','R','.','.','.','B','B','B','B','B','B'},
                {'R','R','R','R','R','R','.','.','.','B','B','B','B','B','B'},
                {'R','R','R','R','R','R','.','.','.','B','B','B','B','B','B'},
                {'R','R','R','R','R','R','.','.','.','B','B','B','B','B','B'},
                {'R','R','R','R','R','R','.','.','.','B','B','B','B','B','B'},
                {'R','R','R','R','R','R','.','.','.','B','B','B','B','B','B'},
            };
            
            DrawBoard(board);

            static void DrawBoard(char[,] board)
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.Write(board[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
        public void Path()
        {
            List<(int x, int y)> mainPath = new List<(int x, int y)>
            {
                (0, 6), (1, 6), (2, 6), (3, 6), (4, 6), (5, 6),
                //befejezni
                (5, 6), (5, 8), (5, 9), (5, 10), (5, 11), (5, 12),
                (6, 12), (7, 12), (8, 12), (9, 12), (10, 12), (11, 12),
                (11, 11), (11, 10), (11, 9), (11, 8), (11, 7), (11, 6),
                (10, 6), (9, 6), (8, 6), (7, 6), (6, 6),
                (6, 5), (6, 4), (6, 3), (6, 2), (6, 1), (6, 0),
                (7, 0), (8, 0), (9, 0), (10, 0), (11, 0), (12, 0),
                (12, 1), (12, 2), (12, 3), (12, 4), (12, 5), (12, 6),
                (13, 6), (14, 6), (15, 6),
            };
            for (int i = 1; i < 6; i++)
            {
                path.Add((i, 6));
            }
            for (int i = 6; i < 11; i++)
            {
                path.Add((6, i));
            }

        }

        public void Restart()
        {
            Console.WriteLine("Nyomd meg az 'i' gombot ha újra szertnéd kezdeni");
            if (Console.ReadLine().ToLower() == "i")
            {
                Jatek();
            }
        }
        static void Main(string[] args)
        {
            new Program();
            Console.ReadKey();
        }
    }
}
