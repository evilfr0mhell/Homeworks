using System;

class Game
{
    static char[,] field;
    static int playerX, playerY;
    static int level = 0;

    static readonly ConsoleColor grassColor = ConsoleColor.Green;
    static readonly ConsoleColor stoneColor = ConsoleColor.Gray;
    static readonly ConsoleColor treeColor = ConsoleColor.DarkYellow;
    static readonly ConsoleColor tileColor = ConsoleColor.Yellow;
    static readonly ConsoleColor activatedTileColor = ConsoleColor.Cyan;
    static readonly ConsoleColor playerColor = ConsoleColor.Red;
    static readonly ConsoleColor wallColor = ConsoleColor.DarkGray;

    static readonly string[] levels = new string[]
    {
        @"
IIIIIIIIII
I########I
I###R#T##I
I###T####I
I####O###I
I######T#I
I##T#####I
I#O######I
I########I
I###C####I
IIIIIIIIII",
        @"
IIIIIIIIII
I##T#OT##I
I####TT##I
I####O###I
I########I
I######R#I
I####T###I
I####C###I
I##T#####I
IIIIIIIIII"
    };

    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать в игру!");
        while (level < levels.Length)
        {
            LoadLevel(levels[level]);
            while (!IsLevelCompleted())
            {
                PrintField();
                ProcessInput();
            }
            Console.WriteLine($"Уровень {level + 1} завершен!");
            level++;
        }
        Console.WriteLine("Поздравляем, вы прошли все уровни!");
    }

    static void LoadLevel(string levelData)
    {
        string[] rows = levelData.Trim().Split('\n');
        int height = rows.Length;
        int width = rows[0].Trim().Length;
        field = new char[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                char symbol = rows[i].Trim()[j];
                field[i, j] = symbol;

                if (symbol == 'C')
                {
                    playerX = i;
                    playerY = j;
                }
            }
        }
    }

    static void PrintField()
    {
        Console.Clear();
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                char symbol = field[i, j];
                switch (symbol)
                {
                    case '#':
                        Console.ForegroundColor = grassColor;
                        break;
                    case 'R':
                    case 'r':
                        Console.ForegroundColor = stoneColor;
                        break;
                    case 'T':
                        Console.ForegroundColor = treeColor;
                        break;
                    case 'O':
                        Console.ForegroundColor = tileColor;
                        break;
                    case 'c':
                        Console.ForegroundColor = activatedTileColor;
                        break;
                    case 'C':
                        Console.ForegroundColor = playerColor;
                        break;
                    case 'I':
                        Console.ForegroundColor = wallColor;
                        break;
                    default:
                        Console.ResetColor();
                        break;
                }
                Console.Write(symbol);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    static void ProcessInput()
    {
        ConsoleKey key = Console.ReadKey(true).Key;
        int newX = playerX, newY = playerY;

        switch (key)
        {
            case ConsoleKey.W: newX--; break;
            case ConsoleKey.S: newX++; break;
            case ConsoleKey.A: newY--; break;
            case ConsoleKey.D: newY++; break;
            default: return;
        }

        if (CanMove(newX, newY))
        {
            MovePlayer(newX, newY);
        }
    }

    static bool CanMove(int x, int y)
    {
        if (x < 0 || y < 0 || x >= field.GetLength(0) || y >= field.GetLength(1))
            return false;

        char target = field[x, y];

        if (target == 'I' || target == 'T')
            return false;

        if (target == 'R' || target == 'r') 
        {
            int pushX = x + (x - playerX);
            int pushY = y + (y - playerY);

            if (pushX < 0 || pushY < 0 || pushX >= field.GetLength(0) || pushY >= field.GetLength(1))
                return false;

            char pushTarget = field[pushX, pushY];
            if (pushTarget == '#' || pushTarget == 'O') 
            {
                field[pushX, pushY] = (pushTarget == 'O') ? 'r' : 'R'; 
                field[x, y] = (target == 'r') ? 'O' : '#'; 
                return true;
            }
            return false;
        }

        return true;
    }

    static void MovePlayer(int x, int y)
    {
        char currentTile = field[playerX, playerY];
        char targetTile = field[x, y];

       
        field[playerX, playerY] = (currentTile == 'c') ? 'O' : '#';

        
        field[x, y] = (targetTile == 'O') ? 'c' : 'C';

        playerX = x;
        playerY = y;
    }


    static bool IsLevelCompleted()
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j] == 'O') 
                    return false;
            }
        }
        return true;
    }
}