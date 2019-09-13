using System;

namespace GeneticAlgo
{
    public class GeneticWorldSerial : WorldIndividuo
    {
        public GeneticWorldSerial(int individuosCount, int geneCount, int maxGenerations) :
            base(individuosCount, geneCount, maxGenerations)
        { }
        public override Tuple<double, Individuo> Run()
        {
            Initialize();
            int generation = 1;
            var quality = 0.0;
            do
            {
                Selection();
                Crossover();
                Mutate();
                var genQuality = Average();
                if (genQuality < quality)
                {
                    Restore();
                }
                else
                {
                    BackupBest();
                    quality = genQuality;
                }
                Console.WriteLine($"Generation: {generation}; quality: {quality}");
            } while (generation++ < maxGenerations);
            return new Tuple<double, Individuo>(quality, FindFittestIndividuo());
        }
    }
}
