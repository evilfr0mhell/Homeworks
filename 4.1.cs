using System;

class Program
{
    static void Main()
    {
        int N = 5;
        int[,] array = new int[N, N];

        int value = 1;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                array[i, j] = value++;
            }
        }

        Console.WriteLine("Массив:");
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write(array[i, j].ToString("D2") + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nОбход по спирали:");
        PrintSpiral(array, N);
    }

    static void PrintSpiral(int[,] array, int N)
    {
        int centerX = N / 2;
        int centerY = N / 2;

        int[] dx = { 0, -1, 0, 1 };
        int[] dy = { 1, 0, -1, 0 };
        int x = centerX, y = centerY;

        Console.Write(array[x, y] + " ");
        int step = 1;
        int direction = 0;

        while (step < N)
        {
            for (int repeat = 0; repeat < 2; repeat++)
            {
                for (int i = 0; i < step; i++)
                {
                    x += dx[direction];
                    y += dy[direction];

                    if (x >= 0 && x < N && y >= 0 && y < N)
                    {
                        Console.Write(array[x, y] + " ");
                    }
                }

                direction = (direction + 1) % 4;
            }

            step++;
        }

        for (int i = 0; i < step - 1; i++)
        {
            x += dx[direction];
            y += dy[direction];

            if (x >= 0 && x < N && y >= 0 && y < N)
            {
                Console.Write(array[x, y] + " ");
            }
        }
    }
}
