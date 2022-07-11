using System;
namespace genetic
{
    public class Egyed
    {
        Random r = new Random();

        public double[] Chromosome { get; set; }
        public double Fitness { get; set; }
        double actual;

        const double minimum = -10;
        const double maximum = 10;

        public Egyed()
        {
            Chromosome = new double[4];
        }

        public double GetRandomNumber()
        {
            return r.NextDouble() * (maximum - minimum) + minimum;
        }
        public void CountFitness(double goal)
        {
            actual = 0;
            Fitness = 0;
            for (int i = 0; i < Chromosome.Length; i++)
            {
                actual += Math.Pow(Chromosome[i], 2);
            }
            double distance = Math.Abs(goal - actual);
            Fitness = distance;
        }


        public void Crossover(Egyed parent1, Egyed parent2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (r.NextDouble() < 0.5) Chromosome[i] = parent1.Chromosome[i];
                else Chromosome[i] = parent2.Chromosome[i];
            }
        }

        public void Mutation(double mutationRate)
        {
            for (int i = 0; i < 4; i++)
            {
                if (r.NextDouble() <= mutationRate) Chromosome[i] = GetRandomNumber();
            }

        }

        public override string ToString()
        {
            return $"Kromoszómák: {Chromosome[0]} , {Chromosome[1]} , {Chromosome[2]} , {Chromosome[3]} ,Eredmény: {actual} , Fitness: {Fitness}";
        }
    }
}
