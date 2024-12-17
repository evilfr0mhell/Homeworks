//1.5 Толмачёв Никита 
Console.WriteLine("Введите длины сторон первого треугольника:");
double a1 = Convert.ToDouble(Console.ReadLine());
double b1 = Convert.ToDouble(Console.ReadLine());
double c1 = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Введите длины сторон второго треугольника:");
double a2 = Convert.ToDouble(Console.ReadLine());
double b2 = Convert.ToDouble(Console.ReadLine());
double c2 = Convert.ToDouble(Console.ReadLine());
double[] triangle1 = { a1, b1, c1 };
double[] triangle2 = { a2, b2, c2 };
Array.Sort(triangle1);
Array.Sort(triangle2);
if (triangle1[0] / triangle2[0] == triangle1[1] / triangle2[1] && triangle1[1] / triangle2[1] == triangle1[2] / triangle2[2])
{
    Console.WriteLine("Треугольники подобны");
}
else
{
    Console.WriteLine("Треугольники не подобны");
}