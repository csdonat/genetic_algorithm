using System;
using System.Collections.Generic;
using System.Linq;

namespace genetic
{
    public class GeneticAlgorithm
    {
        Random r = new Random();

        int population_size = 200;
        double mutation_rate = 0.1;


        int generation = 1;

        const double goal = 49.0;
        public List<Egyed> Population { get; set; }

        public GeneticAlgorithm()
        {

        }

        public void Generate_Initial_Population()
        {
            generation = 1;
            Population = Enumerable.Range(1, population_size).Select(x => new Egyed()).ToList();
            foreach (var item in Population)
            {
                for (int i = 0; i < 4; i++)
                {
                    item.Chromosome[i] = item.GetRandomNumber();
                }
            }
        }
        public void CountFitnesses()
        {
            foreach (var item in Population)
            {
                item.CountFitness(goal);
            }
        }
        public void Selection()
        {
            //2 legjobb fitness (legkisebb fitness számu a legjobb)
            Egyed szulo1 = Population.OrderBy(x => x.Fitness).ToList()[0];
            Egyed szulo2 = Population.OrderBy(x => x.Fitness).ToList()[1];

            foreach (var item in Population)
            {
                item.Crossover(szulo1, szulo2);
                item.Mutation(mutation_rate);
            }

            generation++;
        }

        public void Eredmeny()
        {
            Console.Clear();
            foreach (var item in Population)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nGeneráció: " + generation);
        }
    }
}
