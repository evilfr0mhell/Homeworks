using System;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Игра Быки и Коровы!");
            Console.WriteLine("Выбери режим игры:");
            Console.WriteLine("1. Игрок1 vs Игрок2");
            Console.WriteLine("2. Игрок vs Компьютер");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                PlayerVsPlayer();
            }
            else if (choice == 2)
            {
                PlayerVsComputer();
            }
            else
            {
                Console.WriteLine("Ошибка. Выберите вариант 1 или 2.");
            }
        }

        static void PlayerVsPlayer()
        {
            Console.WriteLine("Игрок 1, введите ваше секретное число (4 уникальных числа):");
            string secretNumber1 = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Игрок 2, введите ваше секретное число (4 уникальных числа):");
            string secretNumber2 = Console.ReadLine();
            Console.Clear();

            PlayGame(secretNumber1, secretNumber2);
        }

        static void PlayerVsComputer()
        {
            string secretNumber = GenerateSecretNumber();
            Console.WriteLine("Компьютер загадал число! Игра началась!");

            PlayGame(secretNumber, null);
        }

        static void PlayGame(string secretNumber1, string secretNumber2)
        {
            bool gameWon = false;
            int attempts = 0;

            while (!gameWon)
            {
                attempts++;
                if (secretNumber2 == null)
                {
                    Console.WriteLine("Введите ваш ответ:");
                    string guess = Console.ReadLine();
                    (int bulls, int cows) = GetBullsAndCows(secretNumber1, guess);

                    Console.WriteLine($"Быков: {bulls}, Коров: {cows}");
                    if (bulls == 4)
                    {
                        Console.WriteLine($"Поздравляем! Вы отгадали число за данное число ({attempts}) попыток.");
                        gameWon = true;
                    }
                }
                else
                {
                    Console.WriteLine("Игрок 1, введите ваш ответ:");
                    string guess1 = Console.ReadLine();
                    (int bulls1, int cows1) = GetBullsAndCows(secretNumber2, guess1);
                    Console.WriteLine($"Игрок 1 - Быков: {bulls1}, Коров: {cows1}");

                    if (bulls1 == 4)
                    {
                        Console.WriteLine($"Игрок 1 ПОБЕДИЛ! Вы отгадали число за данное число ({attempts}) попыток.");
                        gameWon = true;
                        break;
                    }

                    Console.WriteLine("Игрок 2, введите ваш ответ:");
                    string guess2 = Console.ReadLine();
                    (int bulls2, int cows2) = GetBullsAndCows(secretNumber1, guess2);
                    Console.WriteLine($"Игрок 2 - Быков: {bulls2}, Коров: {cows2}");

                    if (bulls2 == 4)
                    {
                        Console.WriteLine($"Игрок 2 ПОБЕДИЛ! Вы отгадали число за данное число ({attempts}) попыток.");
                        gameWon = true;
                    }
                }
            }
        }

        static (int, int) GetBullsAndCows(string secret, string guess)
        {
            int bulls = 0, cows = 0;

            for (int i = 0; i < secret.Length; i++)
            {
                if (guess[i] == secret[i])
                {
                    bulls++;
                }
                else if (secret.Contains(guess[i]))
                {
                    cows++;
                }
            }

            return (bulls, cows);
        }

        static string GenerateSecretNumber()
        {
            Random rand = new Random();
            string secretNumber = "";
            while (secretNumber.Length < 4)
            {
                char digit = (char)('0' + rand.Next(0, 10));
                if (!secretNumber.Contains(digit))
                {
                    secretNumber += digit;
                }
            }
            return secretNumber;
        }
    }
}
