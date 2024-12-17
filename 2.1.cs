//2.1 Толмачёв Никита
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите строку S:");
        string s = Console.ReadLine();

        Console.WriteLine("Введите строку S1:");
        string s1 = Console.ReadLine();

        int count = 0;
        int index = s.IndexOf(s1);
        while (index != -1)
        {
            count++;
            index = s.IndexOf(s1, index + s1.Length);
        }
        Console.WriteLine("Количество вхождений S1 в S: " + count);
    }
}