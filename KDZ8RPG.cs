using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        const int width = 31;
        const int height = 21;
        char[,] maze = GenerateMaze(width, height);

        Player player = new Player();
        List<Character> characters = new List<Character>
        {
            new Character("ГОБЛИН", 30, true, 5, 5, @"
        .      .
       /(.-""- (\
   |\  \/      \/  /|
   | \ / =.  .= \ / |
    \( \  o\/o   / )/
     \_,'-/  \-' ,_/
       /  \__/   \   
       \\__/\__/ /"),
            new Character("Толмачёв Никита", 100, false, 3, 7),
            new Character("Злой ДИНО", 50, true, 9, 9, @"
       __
      /oo\
     |    |
 ^^  (vvvv)   ^^
 \\  /\__/\  //
  \\/      \//
   /        \        
  |          |    ^  
  /          \___/ | 
 (            )     |
  \----------/     /
    //    \\_____/
   W       W
")
        };

        int playerX = 1, playerY = 1;

        while (true)
        {
            Console.Clear();
            DisplayMaze(maze, playerX, playerY, characters);

            Console.WriteLine($"Здоровье: {player.Health} | Сила: {player.Strength}");
            Console.WriteLine("Инвентарь: " + string.Join(", ", player.Inventory));

            char move = Console.ReadKey(true).KeyChar;
            int newX = playerX, newY = playerY;

            switch (move)
            {
                case 'w': newY--; break;
                case 's': newY++; break;
                case 'a': newX--; break;
                case 'd': newX++; break;
                default:
                    Console.WriteLine("Нажми W, A, S или D для перемещения!");
                    continue;
            }


            if (newX >= 0 && newX < width && newY >= 0 && newY < height && maze[newY, newX] != '#')
            {
                playerX = newX;
                playerY = newY;
            }

            Character character = characters.Find(c => c.Health > 0 && c.X == playerX && c.Y == playerY);
            if (character != null)
            {
                InteractWithCharacter(player, character);
            }

            if (maze[playerY, playerX] == 'E')
            {
                Console.WriteLine("Ты нашел выход! Победа!");
                break;
            }

            if (player.Health <= 0)
            {
                Console.WriteLine("Ты погиб. Игра окончена.");
                break;
            }
        }
    }

    static char[,] GenerateMaze(int width, int height)
    {
        char[,] maze = new char[height, width];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                maze[y, x] = '#';

        Random rand = new Random();
        int[][] directions = new int[][]
        {
            new int[] { 0, -2 },
            new int[] { 0, 2 },
            new int[] { -2, 0 },
            new int[] { 2, 0 }
        };

        Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
        int startX = 1, startY = 1;
        maze[startY, startX] = '.';
        stack.Push((startX, startY));

        while (stack.Count > 0)
        {
            var (x, y) = stack.Pop();

            for (int i = directions.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (directions[i], directions[j]) = (directions[j], directions[i]);
            }

            foreach (var dir in directions)
            {
                int nx = x + dir[0];
                int ny = y + dir[1];

                if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && maze[ny, nx] == '#')
                {
                    maze[ny, nx] = '.';
                    maze[y + dir[1] / 2, x + dir[0] / 2] = '.';
                    stack.Push((nx, ny));
                }
            }
        }

        maze[height - 2, width - 2] = 'E';
        return maze;
    }

    static void DisplayMaze(char[,] maze, int playerX, int playerY, List<Character> characters)
    {
        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                if (x == playerX && y == playerY)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('@');
                }
                else if (characters.Exists(c => c.Health > 0 && c.IsEnemy && x == c.X && y == c.Y))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('@');
                }
                else if (characters.Exists(c => c.Health > 0 && !c.IsEnemy && x == c.X && y == c.Y))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write('N');
                }
                else if (maze[y, x] == '#')
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(maze[y, x]);
                }
                else if (maze[y, x] == 'E')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('E');
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(maze[y, x]);
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    static void InteractWithCharacter(Player player, Character character)
    {
        if (character.IsEnemy)
        {
            Console.WriteLine("Ты встретил врага!");
            Console.WriteLine(character.ASCIIArt ?? "Враг перед тобой!");
            Console.WriteLine("1. Атаковать\n2. Убежать");

            char choice = Console.ReadKey(true).KeyChar;
            if (choice == '1')
            {
                int playerDamage = new Random().Next(player.Strength - 2, player.Strength + 2);
                int enemyDamage = new Random().Next(5, 15);

                character.Health -= playerDamage;
                Console.WriteLine($"Ты нанес {playerDamage} урона врагу!");
                if (character.Health > 0)
                {
                    player.Health -= enemyDamage;
                    Console.WriteLine($"Враг нанес тебе {enemyDamage} урона! У тебя осталось {player.Health} здоровья.");
                }

                if (character.Health <= 0)
                {
                    Console.WriteLine("Ты победил врага!");
                    string loot = "Зелье здоровья";
                    player.Inventory.Add(loot);
                    Console.WriteLine($"Ты получил: {loot}");
                }
            }
            else if (choice == '2')
            {
                Console.WriteLine("Ты убежал!");
            }
        }
        else
        {
            Console.WriteLine($"Ты встретил {character.Name}.");
            Console.WriteLine("1. Поговорить\n2. Игнорировать");

            char choice = Console.ReadKey(true).KeyChar;
            if (choice == '1')
            {
                Console.WriteLine($"{character.Name}: Привет! Возьми это в помощь.");
                string gift = "Эликсир силы";
                player.Inventory.Add(gift);
                player.Strength += 5;
                Console.WriteLine($"Ты получил: {gift}. Сила увеличена на 5!");
            }
        }
    }
}

class Player
{
    public int Health { get; set; }
    public int Strength { get; set; }
    public List<string> Inventory { get; private set; }

    public Player()
    {
        Health = 100;
        Strength = 10;
        Inventory = new List<string>();
    }
}

class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public bool IsEnemy { get; set; }
    public string ASCIIArt { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Character(string name, int health, bool isEnemy, int x, int y, string asciiArt = null)
    {
        Name = name;
        Health = health;
        IsEnemy = isEnemy;
        X = x;
        Y = y;
        ASCIIArt = asciiArt;
    }
}
