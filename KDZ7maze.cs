using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static char[,] GenerateMaze(int width, int height)
    {
        char[,] maze = new char[height, width];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                maze[y, x] = '#';
        int startX = 1, startY = 1;
        maze[startY, startX] = '.'; 
        int[][] directions = new int[][]
        {
            new int[] { 0, -2 }, 
            new int[] { 0, 2 }, 
            new int[] { -2, 0 }, 
            new int[] { 2, 0 }   
        };

        Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
        stack.Push((startX, startY));
        Random rand = new Random();

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

    static void DisplayMaze(char[,] maze, int playerX, int playerY)
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
                else if (maze[y, x] == '#')
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray; 
                    Console.Write(maze[y, x]);
                }
                else if (maze[y, x] == 'E')
                {
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.Write(maze[y, x]);
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

    static void Main(string[] args)
    {
        const int width = 31; 
        const int height = 21;
        char[,] maze = GenerateMaze(width, height);

        int playerX = 1;
        int playerY = 1;

        Stopwatch timer = new Stopwatch();
        timer.Start();
        TimeSpan timeLimit = TimeSpan.FromSeconds(60); 

        while (true)
        {
            Console.Clear();
            DisplayMaze(maze, playerX, playerY);

          
            TimeSpan remainingTime = timeLimit - timer.Elapsed;
            if (remainingTime <= TimeSpan.Zero)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Время вышло! Ты проиграл!");
                Console.ResetColor();
                break;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Найди выход (E)! Управление: W/A/S/D. Осталось времени: {remainingTime:ss} секунд");
            Console.ResetColor();

          
            char move = Console.ReadKey(true).KeyChar;
            int newX = playerX, newY = playerY;

          
            switch (move)
            {
                case 'w': newY--; break;
                case 's': newY++; break;
                case 'a': newX--; break;
                case 'd': newX++; break;
                default: continue;
            }

           
            if (newX >= 0 && newX < width && newY >= 0 && newY < height && maze[newY, newX] != '#')
            {
                playerX = newX;
                playerY = newY;
            }

          
            if (maze[playerY, playerX] == 'E')
            {
                Console.Clear();
                DisplayMaze(maze, playerX, playerY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ты нашел выход! Победа!");
                Console.ResetColor();
                break;
            }
        }
    }
}
