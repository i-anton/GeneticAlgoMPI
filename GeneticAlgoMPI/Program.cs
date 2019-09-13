using GeneticAlgo;
using System;
using System.Globalization;
using System.Threading;

namespace GeneticAlgoMPI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                var result = new GeneticWorldMPI(100, 16, 500).Run();
                Console.WriteLine($"Fitness: {result.Item1}");
                Console.WriteLine($"Best individuo: {result.Item2.fitness}");
                Console.WriteLine(StringUtil.ArrayToString(result.Item2.genes));
            }
        }
    }
}
