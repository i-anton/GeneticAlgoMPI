using System;

namespace GeneticAlgo
{
    [Serializable]
    public class Individuo
    {
        [NonSerialized]
        private static readonly double GENE_MUT_PROBABILITY = 0.15;
        public readonly double[] genes;
        public double fitness;
        [NonSerialized]
        public static Random rnd;

        public Individuo(int geneCount)
        {
            genes = new double[geneCount];
            for (int i = 0; i < geneCount; i++)
            {
                genes[i] = (rnd.NextDouble() - 0.5) * 50;
            }
            UpdateFitness();
        }
        public void Mutate()
        {
            for (int i = 0; i < genes.Length; i++)
            {
                if (rnd.NextDouble() <= GENE_MUT_PROBABILITY)
                    genes[i] += (rnd.NextDouble() - 0.5);
            }
            UpdateFitness();
        }
        public void Crossover(Individuo other)
        {
            for (int i = 0; i < genes.Length; i++)
            {
                if (rnd.NextDouble() <= 0.3)
                {
                    var temp = genes[i];
                    genes[i] = other.genes[i];
                    other.genes[i] = temp;
                }
            }
            UpdateFitness();
            other.UpdateFitness();
        }
        public void Set(Individuo other)
        {
            other.genes.CopyTo(genes, 0);
            fitness = other.fitness;
        }
        public void UpdateFitness() => fitness = 1.0 / (1.0 + Rosenbrock());
        private double Sphere()
        {
            double result = 0.0;
            for (int i = 0; i < genes.Length; i++)
            {
                var subresult = (genes[i] - 1.0);
                result += subresult * subresult;
            }
            return result;
        }
        private double Rosenbrock()
        {
            double result = 0.0;
            for (int i = 0; i < genes.Length - 1; i++)
            {
                var item = genes[i];
                var subresult = (item * item) - genes[i + 1];
                subresult = subresult * subresult * 100.0 + (item - 1.0) * (item - 1.0);
                result += subresult;
            }
            return result;
        }
    }
}
