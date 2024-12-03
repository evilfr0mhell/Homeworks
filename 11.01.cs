using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "f.txt";

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {

                writer.WriteLine("x sin(x)");


                for (double x = 0; x <= 1.0; x += 0.1)
                {

                    writer.WriteLine($"{x:F1} {Math.Sin(x):F4}");
                }
            }

            Console.WriteLine($"Таблица значений sin(x) успешно записана в файл \"{filePath}\".");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
