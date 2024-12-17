//1.2 Толмачёв Никита
Console.Write("Введите число N:");
int l = Convert.ToInt32(Console.ReadLine());
Console.Write("Введите число K:");
int k = Convert.ToInt32(Console.ReadLine());
if (k == 0)
{
    Console.WriteLine("Делить на ноль нельзя!");
    return;
}
int n = 0;
bool minuse = (l < 0 && k > 0) || (l > 0 && k < 0);
l = Math.Abs(l);
k = Math.Abs(k);
while (l >= k)
{
    l -= k;
    n++;
}
if (minuse)
{
    n = -n;
}
Console.WriteLine($"Целая часть: {n}, остаток: {l}");