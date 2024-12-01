using System;
using System.Linq;
using System.Threading;

class MemoryGame
{
    static int gridSize = 4;
    static char[,] grid;    
    static bool[,] revealed;
    static Random random = new Random();

    static void Main()
    {
        Console.CursorVisible = false;

        grid = GenerateGrid(gridSize);
        revealed = new bool[gridSize, gridSize];

  
        Console.Clear();
        Console.WriteLine("Добро пожаловать в игру Memory!");
        Console.WriteLine("Нажмите любую клавишу, чтобы начать...");
        Console.ReadKey();

        DateTime startTime = DateTime.Now;


        while (true)
        {
            DrawGrid();
            Console.WriteLine("Выберите карту с помощью стрелок и Enter.");


            (int row1, int col1) = SelectCard();
            revealed[row1, col1] = true;
            DrawGrid();


            (int row2, int col2) = SelectCard();
            revealed[row2, col2] = true;
            DrawGrid();


            if (grid[row1, col1] == grid[row2, col2])
            {
                Console.WriteLine("Совпадение найдено!");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("Нет совпадения.");
                Thread.Sleep(1000);
                revealed[row1, col1] = false;
                revealed[row2, col2] = false;
            }

            if (revealed.Cast<bool>().All(x => x))
            {
                DateTime endTime = DateTime.Now;
                Console.Clear();
                Console.WriteLine("Поздравляем! Вы нашли все совпадения!");
                Console.WriteLine($"Игра завершена за {endTime - startTime:mm\\:ss}.");
                break;
            }
        }
    }

    static char[,] GenerateGrid(int size)
    {
        char[] symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] selectedSymbols = symbols.Take(size * size / 2).ToArray();
        char[] shuffledSymbols = selectedSymbols.Concat(selectedSymbols).OrderBy(_ => random.Next()).ToArray();

        char[,] grid = new char[size, size];
        for (int i = 0; i < size * size; i++)
            grid[i / size, i % size] = shuffledSymbols[i];

        return grid;
    }

    static void DrawGrid()
    {
        Console.Clear();
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                if (revealed[row, col])
                    Console.Write($" {grid[row, col]} ");
                else
                    Console.Write(" # ");
            }
            Console.WriteLine();
        }
    }

    static (int, int) SelectCard()
    {
        int currentRow = 0, currentCol = 0;

        while (true)
        {
            DrawGrid();
            Console.SetCursorPosition(currentCol * 3, currentRow);
            Console.Write("[ ]");

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    currentRow = (currentRow > 0) ? currentRow - 1 : gridSize - 1;
                    break;
                case ConsoleKey.DownArrow:
                    currentRow = (currentRow < gridSize - 1) ? currentRow + 1 : 0;
                    break;
                case ConsoleKey.LeftArrow:
                    currentCol = (currentCol > 0) ? currentCol - 1 : gridSize - 1;
                    break;
                case ConsoleKey.RightArrow:
                    currentCol = (currentCol < gridSize - 1) ? currentCol + 1 : 0;
                    break;
                case ConsoleKey.Enter:
                    if (!revealed[currentRow, currentCol])
                        return (currentRow, currentCol);
                    break;
            }
        }
    }
}
