using System;

class Game2048
{
    const int Size = 4;
    static int[,] board = new int[Size, Size];
    static Random random = new Random();

    static void Main()
    {
        InitializeBoard();
        while (true)
        {
            PrintBoard();
            Console.WriteLine("Используйте W (вверх), A (влево), S (вниз), D (вправо) для движения. Нажмите Q, чтобы выйти.");
            var key = Console.ReadKey().Key;

            if (key == ConsoleKey.Q) break;

            bool moved = false;
            switch (key)
            {
                case ConsoleKey.W: moved = MoveUp(); break;
                case ConsoleKey.S: moved = MoveDown(); break;
                case ConsoleKey.A: moved = MoveLeft(); break;
                case ConsoleKey.D: moved = MoveRight(); break;
                default: continue;
            }

            if (moved)
            {
                AddRandomTile();
                if (IsGameOver())
                {
                    PrintBoard();
                    Console.WriteLine("Игра закончена!");
                    break;
                }
            }
        }
    }

    static void InitializeBoard()
    {
        AddRandomTile();
        AddRandomTile();
    }

    static void AddRandomTile()
    {
        while (true)
        {
            int x = random.Next(Size);
            int y = random.Next(Size);
            if (board[x, y] == 0)
            {
                board[x, y] = random.Next(10) < 9 ? 2 : 4;
                break;
            }
        }
    }

    static void PrintBoard()
    {
        Console.Clear();
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write(board[i, j] == 0 ? ".\t" : $"{board[i, j]}\t");
            }
            Console.WriteLine("\n");
        }
    }

    static bool MoveLeft()
    {
        bool moved = false;
        for (int i = 0; i < Size; i++)
        {
            int[] row = new int[Size];
            int index = 0;
            for (int j = 0; j < Size; j++)
            {
                if (board[i, j] != 0) row[index++] = board[i, j];
            }

            for (int j = 0; j < Size - 1; j++)
            {
                if (row[j] != 0 && row[j] == row[j + 1])
                {
                    row[j] *= 2;
                    row[j + 1] = 0;
                }
            }

            int[] newRow = new int[Size];
            index = 0;
            for (int j = 0; j < Size; j++)
            {
                if (row[j] != 0) newRow[index++] = row[j];
            }

            for (int j = 0; j < Size; j++)
            {
                if (board[i, j] != newRow[j])
                {
                    moved = true;
                    board[i, j] = newRow[j];
                }
            }
        }
        return moved;
    }

    static bool MoveRight()
    {
        ReverseBoardHorizontally();
        bool moved = MoveLeft();
        ReverseBoardHorizontally();
        return moved;
    }

    static bool MoveUp()
    {
        TransposeBoard();
        bool moved = MoveLeft();
        TransposeBoard();
        return moved;
    }

    static bool MoveDown()
    {
        TransposeBoard();
        bool moved = MoveRight();
        TransposeBoard();
        return moved;
    }

    static void ReverseBoardHorizontally()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size / 2; j++)
            {
                int temp = board[i, j];
                board[i, j] = board[i, Size - j - 1];
                board[i, Size - j - 1] = temp;
            }
        }
    }

    static void TransposeBoard()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = i + 1; j < Size; j++)
            {
                int temp = board[i, j];
                board[i, j] = board[j, i];
                board[j, i] = temp;
            }
        }
    }

    static bool IsGameOver()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (board[i, j] == 0) return false;
                if (i > 0 && board[i, j] == board[i - 1, j]) return false;
                if (j > 0 && board[i, j] == board[i, j - 1]) return false;
            }
        }
        return true;
    }
}
