using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        string[] questions = {
            "1. Какое из следующих слов написано правильно?\n a) обращение\n b) абращение\n c) обрещение\n d) абращение",
            "2. Что такое орфоэпия?\n a) Наука о словах\n b) Правила произношения слов\n c) Искусство письма\n d) Стиль изложения",
            "3. Как называется предложение, состоящее из одной грамматической основы?\n a) Простое\n b) Сложное\n c) Сложноподчинённое\n d) Сложносочинённое",
            "4. Что из этого является глаголом?\n a) ложь\n b) стоп\n c) возьмешь\n d) танцы",
            "5. Как называется нарушение норм литературного языка?\n a) Диалектизм\n b) Лексический повтор\n c) Лексическая ошибка\n d) Отклонение"
        };

        char[] correctAnswers = { 'a', 'b', 'a', 'c', 'c' };

        int correctCount = 0;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Console.WriteLine("Тест по русскому языку начался! Ответьте на вопросы:");

        for (int i = 0; i < questions.Length; i++)
        {
            Console.WriteLine(questions[i]);
            Console.Write("Ваш ответ: ");
            char answer = Char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if (answer == correctAnswers[i])
            {
                correctCount++;
            }
        }

        stopwatch.Stop();

        Console.WriteLine("\nТест завершён!");
        Console.WriteLine($"Количество правильных ответов: {correctCount}/{questions.Length}");

        if (correctCount >= 3)
        {
            Console.WriteLine("Результат: Тест пройден!");
        }
        else
        {
            Console.WriteLine("Результат: Тест не пройден.");
        }

        Console.WriteLine($"Время выполнения теста: {stopwatch.Elapsed.TotalSeconds:F2} секунд.");
    }
}
