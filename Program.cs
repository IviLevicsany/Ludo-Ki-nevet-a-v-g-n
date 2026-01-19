using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Random rnd = new Random();

    static char[,] baseBoard =
    {
        {'G','G','G','G','G','G','.','.','.','Y','Y','Y','Y','Y','Y'},
        {'G','G','G','G','G','G','.','H','.','Y','Y','Y','Y','Y','Y'},
        {'G','G','G','G','G','G','.','H','.','Y','Y','Y','Y','Y','Y'},
        {'G','G','G','G','G','G','.','H','.','Y','Y','Y','Y','Y','Y'},
        {'G','G','G','G','G','G','.','H','.','Y','Y','Y','Y','Y','Y'},
        {'G','G','G','G','G','G','.','H','.','Y','Y','Y','Y','Y','Y'},
        {'.','.','.','.','.','.','H','H','H','.','.','.','.','.','.'},
        {'.','H','H','H','H','H','H','H','H','H','H','H','H','H','.'},
        {'.','.','.','.','.','.','H','H','H','.','.','.','.','.','.'},
        {'R','R','R','R','R','R','.','H','.','B','B','B','B','B','B'},
        {'R','R','R','R','R','R','.','H','.','B','B','B','B','B','B'},
        {'R','R','R','R','R','R','.','H','.','B','B','B','B','B','B'},
        {'R','R','R','R','R','R','.','H','.','B','B','B','B','B','B'},
        {'R','R','R','R','R','R','.','H','.','B','B','B','B','B','B'},
        {'R','R','R','R','R','R','.','.','.','B','B','B','B','B','B'},
    };

    static List<(int x, int y)> mainPath = new()
    {
        (0,6),(1,6),(2,6),(3,6),(4,6),(5,6),
        (6,5),(6,4),(6,3),(6,2),(6,1),(6,0),(7,0),
        (8,0),(8,1),(8,2),(8,3),(8,4),(8,5),
        (9,6),(10,6),(11,6),(12,6),(13,6),(14,6),(14,7),
        (14,8),(13,8),(12,8),(11,8),(10,8),(9,8),
        (8,9),(8,10),(8,11),(8,12),(8,13),(8,14),(7,14),
        (6,14),(6,13),(6,12),(6,11),(6,10),(6,9),
        (5,8),(4,8),(3,8),(2,8),(1,8),(0,8),(0,7),
    };

    static Dictionary<char, List<(int x, int y)>> homePaths = new()
    {
        ['G'] = new() { (1, 7), (2, 7), (3, 7), (4, 7), (5, 7), (6, 7) },
        ['Y'] = new() { (7, 1), (7, 2), (7, 3), (7, 4), (7, 5), (7, 6) },
        ['B'] = new() { (13, 7), (12, 7), (11, 7), (10, 7), (9, 7), (8, 7) },
        ['R'] = new() { (7, 13), (7, 12), (7, 11), (7, 10), (7, 9), (7, 8) }
    };
    

    static Dictionary<char, int> startPosition = new()
    {
        ['G'] = 1,
        ['Y'] = 14,
        ['B'] = 27,
        ['R'] = 40
    };

    class Player
    {
        public char Color;
        public List<Pawn> Pawns = new();
    }

    class Pawn
    {
        public int MainPosition = -1;
        public int GoalPosition = -1;
        public bool Finished = false;
    }

    static void Main()
    {
        var players = new List<Player>
        {
            CreatePlayer('G'),
            CreatePlayer('Y'),
            CreatePlayer('B'),
            CreatePlayer('R')
        };

        int current = 0;

        while (true)
        {
            Draw(players);
            var p = players[current];

            int roll = rnd.Next(1, 7);
            Console.WriteLine($"\n{p.Color} játékos {roll}-t dobott (ENTER)");
            Console.ReadLine();

            Move(p, players, roll);

            if (p.Pawns.All(x => x.Finished))
            {
                Draw(players);
                Console.WriteLine($"\n{p.Color} Nyert!");
                break;
            }

            if (roll != 6)
                current = (current + 1) % players.Count;
        }
    }

    static Player CreatePlayer(char c)
    {
        var p = new Player { Color = c };
        for (int i = 0; i < 4; i++) p.Pawns.Add(new Pawn());
        return p;
    }

    static void Move(Player p, List<Player> players, int roll)
    {
        var newPawn = p.Pawns.FirstOrDefault(i => i.MainPosition == -1 && !i.Finished);
        var activePawn = p.Pawns.FirstOrDefault(i => (i.MainPosition >= 0 || i.GoalPosition >= 0) && !i.Finished);

        if (roll == 6 && newPawn != null && activePawn == null)
        {
            newPawn.MainPosition = startPosition[p.Color];
            return;
        }
        else if (roll == 6 && newPawn != null && activePawn != null)
        {
            Console.Write("Szeretnél új bábut a pályára hozni? (I/N): ");
            if (Console.ReadKey().Key == ConsoleKey.I)
            {
                newPawn.MainPosition = startPosition[p.Color];
                return;
            }
            Console.WriteLine();
        }
        if (activePawn == null) return;

        int entryIndex = (startPosition[p.Color] + mainPath.Count - 1) % mainPath.Count;

        if (activePawn.MainPosition >= 0)
        {
            int target = activePawn.MainPosition + roll;

            if (activePawn.MainPosition <= entryIndex && target > entryIndex)
            {
                int stepsIntoHome = target - entryIndex - 1;

                if (stepsIntoHome < homePaths[p.Color].Count)
                {
                    activePawn.MainPosition = -1;
                    activePawn.GoalPosition = 0;
                    for (int i = 1; i <= stepsIntoHome; i++)
                        activePawn.GoalPosition++;
                    return;
                }
                else return;
            }

            activePawn.MainPosition = target % mainPath.Count;
        }
        else if (activePawn.GoalPosition >= 0)
        {
            int remaining = homePaths[p.Color].Count - 1 - activePawn.GoalPosition;

            if (roll == remaining)
            {
                activePawn.Finished = true;
                activePawn.GoalPosition = -1;
            }
            else if (roll < remaining)
            {
                activePawn.GoalPosition += roll;
            }
            else
            {
                Console.WriteLine($"Túl nagy dobás, a bábu nem lép.");
            }
        }

        foreach (var other in players.Where(pl => pl != p))
            foreach (var pawn in other.Pawns)
                if (pawn.MainPosition == activePawn.MainPosition && pawn.MainPosition >= 0)
                    pawn.MainPosition = -1;
    }

    static void Draw(List<Player> players)
    {
        Console.Clear();
        char[,] board = (char[,])baseBoard.Clone();

        foreach (var p in players)
            foreach (var pawn in p.Pawns)
            {
                if (pawn.MainPosition >= 0)
                {
                    var pos = mainPath[pawn.MainPosition];
                    board[pos.y, pos.x] = p.Color;
                }
                else if (pawn.GoalPosition >= 0)
                {
                    var pos = homePaths[p.Color][pawn.GoalPosition];
                    board[pos.y, pos.x] = p.Color;
                }
            }

        for (int y = 0; y < 15; y++)
        {
            for (int x = 0; x < 15; x++)
            {
                char c = board[y, x];
                Console.ForegroundColor = c switch
                {
                    'G' => ConsoleColor.Green,
                    'Y' => ConsoleColor.Yellow,
                    'B' => ConsoleColor.Blue,
                    'R' => ConsoleColor.Red,
                    'H' => ConsoleColor.Gray,
                    '.' => ConsoleColor.DarkGray,
                };
                Console.Write(c);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nCélba ért bábuk:");
        foreach (var p in players)
            Console.Write($"{p.Color}: {p.Pawns.Count(i => i.Finished)} / 4   ");
        Console.WriteLine();
    }
}
