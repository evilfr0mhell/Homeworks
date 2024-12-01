using System;

class XO
{
    static char[,] board = new char[3, 3];
    static bool isPlayer1Turn = true;

    static void Main(string[] args)
    {
        Console.WriteLine("Выберите режим игры:");
        Console.WriteLine("1 - Игрок против Игрока");
        Console.WriteLine("2 - Игрок против Компьютера");
        int mode = int.Parse(Console.ReadLine());

        InitializeBoard();
        bool gameEnded = false;

        while (!gameEnded)
        {
            PrintBoard();

            if (mode == 2 && !isPlayer1Turn)
            {
                ComputerMove();
            }
            else
            {
                PlayerMove();
            }

            gameEnded = CheckWinOrDraw();
            isPlayer1Turn = !isPlayer1Turn;
        }
    }

    static void InitializeBoard()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = ' ';
    }

    static void PrintBoard()
    {
        Console.Clear();
        Console.WriteLine("  0   1   2");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 'X')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X");
                    Console.ResetColor();
                }
                else if (board[i, j] == 'O')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("O");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" ");
                }

                if (j < 2) Console.Write(" | ");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine(" ---+---+---");
        }
    }

    static void PlayerMove()
    {
        int row, col;
        while (true)
        {
            Console.WriteLine($"Ход {(isPlayer1Turn ? "Игрока 1 (X)" : "Игрока 2 (O)")}:");
            Console.Write("Введите строку: ");
            row = int.Parse(Console.ReadLine());
            Console.Write("Введите столбец: ");
            col = int.Parse(Console.ReadLine());

            if (row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ')
            {
                board[row, col] = isPlayer1Turn ? 'X' : 'O';
                break;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }
        }
    }

    static void ComputerMove()
    {
        Console.WriteLine("Компьютер делает ход...");
        Random rand = new Random();
        int row, col;

        while (true)
        {
            row = rand.Next(0, 3);
            col = rand.Next(0, 3);

            if (board[row, col] == ' ')
            {
                board[row, col] = 'O';
                break;
            }
        }
    }

    static bool CheckWinOrDraw()
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != ' ' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] ||
                board[0, i] != ' ' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
            {
                AnnounceWinner();
                return true;
            }
        }

        if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] ||
            board[0, 2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            AnnounceWinner();
            return true;
        }

        foreach (char cell in board)
        {
            if (cell == ' ') return false;
        }

        Console.WriteLine("Ничья!");
        return true;
    }

    static void AnnounceWinner()
    {
        PrintBoard();
        Console.WriteLine($"Победил {(isPlayer1Turn ? "Игрок 1 (X)" : "Игрок 2/Компьютер (O)")}!");
    }
}
