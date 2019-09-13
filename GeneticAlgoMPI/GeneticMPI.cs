using MPI;
using System;

namespace GeneticAlgo
{
    public class GeneticWorldMPI : WorldIndividuo
    {
        private Individuo[] migratedIn;
        private Individuo[] migratedOut;
        private readonly Intracommunicator comm;
        private readonly int processTo;
        private readonly int processFrom;
        const int migrateBarrier = 100;

        public GeneticWorldMPI(int individuosCount, int geneCount, int maxGenerations) :
            base(individuosCount, geneCount, maxGenerations)
        {
            migratedIn = new Individuo[individuosCount / 2];
            migratedOut = new Individuo[individuosCount / 2];
            comm = Communicator.world;
            processTo = (comm.Rank + 1) % comm.Size;
            processFrom = (comm.Rank + comm.Size - 1) % comm.Size;
        }
        public override Tuple<double, Individuo> Run()
        {
            Individuo.rnd = new Random(unchecked(
                (int)(new DateTime().Ticks + comm.Rank)));
            Initialize();
            int generation = 1;
            int migCounter = 0;
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
                Console.WriteLine($"P:{comm.Rank}; Generation: {generation}; quality: {quality}");
                if (migCounter++ >= migrateBarrier) {
                    Migrate();
                    migCounter = 0;
                }
            } while (generation++ < maxGenerations);
            return new Tuple<double, Individuo>(quality, FindFittestIndividuo());
        }
        private void Migrate()
        {
            Array.Copy(individuos, migratedOut, migratedOut.Length);
            comm.Send(migratedOut, processTo, 0);
            comm.Receive(processFrom, 0, out migratedIn);
            Array.Copy(migratedIn, individuos, migratedIn.Length);
        }
    }
}
