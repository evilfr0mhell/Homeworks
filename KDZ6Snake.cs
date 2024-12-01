using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Game game = new Game();
        game.Start();
    }
}

class Game
{
    private const int Width = 30;   
    private const int Height = 20;  
    private Snake snake;
    private Food food;
    private Obstacle obstacle;
    private bool isRunning;

    public void Start()
    {
        Console.CursorVisible = false;
        snake = new Snake(Width / 2, Height / 2);
        food = new Food(Width, Height);
        obstacle = new Obstacle(Width, Height);
        isRunning = true;

        while (isRunning)
        {
            Draw();
            Input();
            Update();
            Thread.Sleep(150); 
        }

        Console.Clear();
        Console.WriteLine("Игра окончена!");
    }

    private void Draw()
    {
        Console.Clear();


        for (int x = 0; x <= Width; x++)
        {
            Console.SetCursorPosition(x, 0);
            Console.Write("#");
            Console.SetCursorPosition(x, Height);
            Console.Write("#");
        }
        for (int y = 0; y <= Height; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write("#");
            Console.SetCursorPosition(Width, y);
            Console.Write("#");
        }


        snake.Draw();


        food.Draw();

        obstacle.Draw();
    }

    private void Input()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            snake.ChangeDirection(key);
        }
    }

    private void Update()
    {
        snake.Move();


        if (snake.HeadX <= 0 || snake.HeadX >= Width || snake.HeadY <= 0 || snake.HeadY >= Height)
        {
            isRunning = false;
        }

        if (snake.IsCollidingWithSelf())
        {
            isRunning = false;
        }

        if (obstacle.IsColliding(snake.HeadX, snake.HeadY))
        {
            isRunning = false;
        }

        if (snake.HeadX == food.X && snake.HeadY == food.Y)
        {
            snake.Grow();
            food.Generate(snake, obstacle);
        }
    }
}

class Snake
{
    private List<(int X, int Y)> body;
    private int directionX, directionY;

    public int HeadX => body.First().X;
    public int HeadY => body.First().Y;

    public Snake(int startX, int startY)
    {
        body = new List<(int X, int Y)> { (startX, startY) };
        directionX = 1;
        directionY = 0;
    }

    public void Move()
    {
        (int X, int Y) newHead = (HeadX + directionX, HeadY + directionY);
        body.Insert(0, newHead);
        body.RemoveAt(body.Count - 1);
    }

    public void Grow()
    {
        body.Add(body.Last());
    }

    public void ChangeDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.W when directionY == 0: directionX = 0; directionY = -1; break;
            case ConsoleKey.S when directionY == 0: directionX = 0; directionY = 1; break;
            case ConsoleKey.A when directionX == 0: directionX = -1; directionY = 0; break;
            case ConsoleKey.D when directionX == 0: directionX = 1; directionY = 0; break;
        }
    }

    public bool IsCollidingWithSelf()
    {
        return body.Skip(1).Any(part => part.X == HeadX && part.Y == HeadY);
    }

    public bool IsColliding(int x, int y)
    {
        return body.Any(part => part.X == x && part.Y == y);
    }

    public void Draw()
    {
        foreach (var part in body)
        {
            Console.SetCursorPosition(part.X, part.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("s");
        }
        Console.ResetColor();
    }
}

class Food
{
    private Random random = new Random();
    public int X { get; private set; }
    public int Y { get; private set; }

    private ConsoleColor color;

    public Food(int width, int height)
    {
        Generate(null, null);
    }

    public void Generate(Snake snake, Obstacle obstacle)
    {
        do
        {
            X = random.Next(1, 29);
            Y = random.Next(1, 19);
        }
        while ((snake != null && snake.IsColliding(X, Y)) || (obstacle != null && obstacle.IsColliding(X, Y)));

        color = (ConsoleColor)random.Next(9, 15);
    }

    public void Draw()
    {
        Console.SetCursorPosition(X, Y);
        Console.ForegroundColor = color;
        Console.Write("@");
        Console.ResetColor();
    }
}

class Obstacle
{
    private List<(int X, int Y)> blocks;

    public Obstacle(int width, int height)
    {
        blocks = new List<(int X, int Y)>
        {
            (10, 5), (15, 10), (20, 15)
        };
    }

    public bool IsColliding(int x, int y)
    {
        return blocks.Any(block => block.X == x && block.Y == y);
    }

    public void Draw()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        foreach (var block in blocks)
        {
            Console.SetCursorPosition(block.X, block.Y);
            Console.Write("X");
        }
        Console.ResetColor();
    }
}
