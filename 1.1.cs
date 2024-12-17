//1.1 Толмачёв Никита
using System;
Console.Write("Введите число: ");
int n = Convert.ToInt32(Console.ReadLine());
int j = 0;
for (int i = 2; i < n; i++)
{
    for (j = 2; j <= Math.Sqrt(i); j++)
    {

        if (i % j == 0)
        {
            break;
        }
    }
    if (j > Math.Sqrt(i))
        Console.WriteLine(i);
}