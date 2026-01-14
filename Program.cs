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
                        
                        p.Position += dobas;

                        Console.WriteLine($"{p.Name} {dobas} -est dobott, így {p.Position} -re lépett");

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
        public void Restart()
        {
            Console.WriteLine("Nyomd meg az 'i' gombot ha újra szertnéd kezdeni");
            ConsoleKeyInfo gomb = Console.ReadKey(intercept: true);
            if (gomb.Key == ConsoleKey.I)
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

