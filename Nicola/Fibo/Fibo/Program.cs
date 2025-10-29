
using System.Reflection.Metadata.Ecma335;

Console.WriteLine(Fibonacci(-1));
static int Fibonacci(int n)
{
    if (n < 0)
    {
        Console.WriteLine("pas de nombre négatif");
        return n;
    }
    if (n < 2)
        return n;
    return Fibonacci(n - 1) + Fibonacci(n - 2);
    
}

for (int i = 0; i < 13; i++)
{
    Console.WriteLine(Fibonacci(i));
}