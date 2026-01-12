using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

record struct Player(string Name, int Position, int Pieces);

namespace Ludo
{
    internal class Program
    {
        Random rnd = new Random();
        const int palya = 52;
        int p1_hely = 0;
        int p2_hely = 0;
        int p3_hely = 0;
        int p4_hely = 0;
        int p1_babu = 4;
        int p2_babu = 4;
        int p3_babu = 4;
        int p4_babu = 4;
        string p1_nev = "";
        string p2_nev = "p2";
        string p3_nev = "p3";
        string p4_nev = "p4";
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
            /*
            p1_nev = Console.ReadLine();
            while (p1_nev == null)
                Console.WriteLine("Nem adtál meg nevet, próbáld újra: ");
            */
            
            List<Player> players = new List<Player>();
            while (true)
            {
                Console.Write("Mennyi játékossal szertnél játszani? ");
                /*if (int.TryParse(Console.ReadLine(), out int numPlayers) && numPlayers >= 2 && numPlayers <= 4)
                {
                    for (int i = 0; i < numPlayers; i++)
                    {
                        Console.Write("Add meg a játékos nevét: ");
                        string name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name) && !players.Any(p => p.Name == name))
                        {
                            players.Add(new Player(name, 0, 4));
                        }
                        else
                        {
                            Console.WriteLine("Érvénytelen vagy foglalt név, próbáld újra.");
                            i--; // Ismételje meg a ciklust a név újragenerálásához
                        }
                    }
                    break; // Kilép a ciklusból, ha sikeresen hozzáadtuk a játékosokat
                }
                else
                {
                    Console.WriteLine("Kérlek adj meg egy érvényes számot 2 és 4 között.");
                }*/

                for (int i = 0; i < players.Count; i++)
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
                        }
                    }
                    else
                        Console.WriteLine("Ez a név már foglalt");
                }
            }


            while (p1_babu == 0 || p2_babu == 0 || p3_babu == 0 || p4_babu == 0)
            {
                while (p1_hely < palya && p2_hely < palya && p3_hely < palya && p4_hely < palya)
                {
                    int dobas1 = rnd.Next(1, 7);
                    int dobas2 = rnd.Next(1, 7);
                    int dobas3 = rnd.Next(1, 7);
                    int dobas4 = rnd.Next(1, 7);
                    p1_hely += dobas1;
                    p2_hely += dobas2;
                    p2_hely += dobas3;
                    p2_hely += dobas4;

                    Console.WriteLine($"{p1_nev} {dobas1} -est dobott, így {p1_hely} -re lépett");

                    if (p1_hely >= palya)
                    {
                        Console.WriteLine(p1_nev + (p1_babu-1) + " bábuja van");
                        p1_hely = 0;
                    }
                    if (p2_hely >= palya)
                    {
                        Console.WriteLine(p2_nev + (p2_babu - 1) + " bábuja van");
                        p2_hely = 0;
                    }
                    if (p3_hely >= palya)
                    {
                        Console.WriteLine(p3_nev + (p3_babu - 1) + " bábuja van");
                        p3_hely = 0;
                    }
                    if (p4_hely >= palya)
                    {
                        Console.WriteLine(p4_nev + (p4_babu - 1) + " bábuja van");
                        p4_hely = 0;
                    }
                    if (p1_hely == 0)
                    {
                        Console.WriteLine(p1_nev + " nyert!");
                        Restart();
                    }
                    else if (p2_hely == 0)
                    {
                        Console.WriteLine(p2_nev + " nyert!");
                        Restart();
                    }
                    else if (p3_hely == 0)
                    {
                        Console.WriteLine(p3_nev + " nyert!");
                        Restart();
                    }
                    else if (p4_hely == 0)
                    {
                        Console.WriteLine(p4_nev + " nyert!");
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
