using System;

namespace GeneticAlgo
{
    public abstract class WorldIndividuo
    {
        protected readonly int maxGenerations;
        protected readonly int geneCount;
        protected readonly Individuo[] individuos;
        protected readonly Individuo[] bestIndividuos;

        public WorldIndividuo(int individuosCount, int geneCount, int maxGenerations)
        {
            Individuo.rnd = new Random();
            this.geneCount = geneCount;
            this.maxGenerations = maxGenerations;
            individuos = new Individuo[individuosCount];
            bestIndividuos = new Individuo[individuosCount];
            for (int i = 0; i < individuos.Length; i++)
                bestIndividuos[i] = new Individuo(geneCount);
        }

        public virtual Tuple<double, Individuo> Run() => new Tuple<double, Individuo>(0.0, individuos[0]);

        protected void BackupBest()
        {
            for (int i = 0; i < individuos.Length; i++)
                bestIndividuos[i].Set(individuos[i]);
        }

        protected void Restore()
        {
            for (int i = 0; i < individuos.Length; i++)
                individuos[i].Set(bestIndividuos[i]);
        }

        protected void Initialize()
        {
            for (int i = 0; i < individuos.Length; i++)
                individuos[i] = new Individuo(geneCount);
        }

        protected void Selection()
        {
            for (int i = 0; i < individuos.Length; i++)
            {
                var ind1 = individuos[i];
                for (int j = 0; j < individuos.Length; j++)
                {
                    if (i == j) continue;
                    var ind2 = individuos[j];
                    if (ind1.fitness > ind2.fitness)
                    {
                        ind2.Set(ind1);
                    }
                    else
                    {
                        ind1.Set(ind2);
                    }
                }
            }
        }

        protected void Crossover()
        {
            for (int i = 0; i < individuos.Length; i++)
            {
                var ind1 = individuos[i];
                for (int j = 0; j < individuos.Length; j++)
                {
                    if (i == j) continue;
                    var ind2 = individuos[j];
                    ind1.Crossover(ind2);
                }
            }
        }

        protected void Mutate()
        {
            for (int i = 0; i < individuos.Length; i++)
                individuos[i].Mutate();
        }

        protected double Average()
        {
            double result = 0.0;
            for (int i = 0; i < individuos.Length; i++)
                result += individuos[i].fitness;
            return result / individuos.Length;
        }

        protected Individuo FindFittestIndividuo()
        {
            var max = individuos[0];
            double maxFit = max.fitness;
            for (int i = 1; i < individuos.Length; i++)
            {
                var item = individuos[i];
                if (item.fitness > maxFit)
                {
                    max = item;
                    maxFit = max.fitness;
                }
            }
            return max;
        }
    }
}
