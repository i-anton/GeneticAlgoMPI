using System;
using System.Globalization;
using System.Threading;

namespace GeneticAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new GeneticWorldSerial(100, 6, 500).Run();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Console.WriteLine($"Fitness: {result.Item1}");
            Console.WriteLine($"Best individuo: {result.Item2.fitness}");
            Console.WriteLine(StringUtil.ArrayToString(result.Item2.genes));
        }
    }
}
