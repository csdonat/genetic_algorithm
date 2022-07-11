using System;
using System.Collections.Generic;
using System.Linq;

namespace genetic
{
    class Program
    {


        static double pontossag = 0.01;
        static int maxIterations = 200;
        static GeneticAlgorithm ga;
        static List<double[]> eredmenyek;

        static void Main(string[] args)
        {

            //menü
            eredmenyek = new List<double[]>();
            int menu;
            do
            {
                menu = MenuBeker();

                switch (menu)
                {
                    case 1: DeleteResults(); break;
                    case 2: if (eredmenyek.Count() != 0) { StartGenetic(); break; } else { DeleteResults(); break; }
                    default: break;
                }


            } while (menu != 3);

        }
        static int MenuBeker()
        {
            bool valid = false;
            int menu;
            do
            {
                Console.Clear();
                if (eredmenyek.Count > 0)
                {
                    Console.WriteLine("\nEredmények: ");

                    for (int i = 0; i < eredmenyek.Count; i++)
                    {
                        double score = 0;
                        for (int j = 0; j < eredmenyek[i].Length; j++)
                        {
                            score += Math.Pow(eredmenyek[i][j], 2);
                            Console.Write(eredmenyek[i][j].ToString() + " , ");
                        }
                        Console.Write(" , eredmény= " + score);
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("\nEredmények törlése és keresés: 1 \nEredmények meghagyása és keresésének folytatása: 2\nKilépés: 3");
                Console.Write("\nÍrja be a választását: ");




                string beker = Console.ReadLine();
                valid = int.TryParse(beker, out menu);


            } while (!valid);

            return menu;
        }

        static void DeleteResults()
        {
            eredmenyek = new List<double[]>();
            StartGenetic();
        }

        static void StartGenetic()
        {
            ga = new GeneticAlgorithm();
            ga.Generate_Initial_Population();
            ga.CountFitnesses();

            Egyed best;
            bool nemtalal = false;
            bool valid = false;
            int iterations = 0;
            do
            {
                iterations++;

                ga.Eredmeny();
                ga.Selection();
                ga.CountFitnesses();
                best = ga.Population.OrderBy(x => x.Fitness).First();

                if (best.Fitness < pontossag) valid = true;
                for (int i = 0; i < eredmenyek.Count; i++)
                {
                    if (eredmenyek[i] == best.Chromosome) valid = false;
                }

                if (iterations == maxIterations)
                {
                    nemtalal = true;
                    break;
                }

            } while (!valid);
            if (nemtalal)
            {
                Console.WriteLine("\n Nem talált eredményt\nNyomjon meg egy gombot...");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Eredményt talált: \n" + best + "\n\nNyomjon meg egy gombot...");
            eredmenyek.Add(best.Chromosome);
            Console.ReadKey(true);


        }
    }
}
