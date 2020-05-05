using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Pacman.Ai.Reinforced
{
    public class GeneticLearning : ArtificialMovement
    {
        private Food food;
        private Pacman pacman;
        private Action restartGame;

        private int targetFoodEaten = 0;
        private int geneticItteration = 0;
        private List<float> geneticSnapshots = new List<float>();

        public GeneticLearning(Food food, Pacman pacman, Action restart)
        {
            this.food = food;
            this.pacman = pacman;
            this.restartGame = restart;

            targetFoodEaten = 512;
        }

        int populationSize = 25;
        float mutationRate = 0.01f;
        //Elitism refers to a method for improving the GA performance; the basic idea is to transfer the best individuals of the current generation to the next generation.
        int elitism = 5;

        private GeneticAlgorithm<int> GeneticAlgorithm;
        private Random random;

        public void Start()
        {
            random = new System.Random();
            GeneticAlgorithm = new GeneticAlgorithm<int>(populationSize, targetFoodEaten, random, GetRandomMovement, FitnessFunction, elitism, mutationRate);
        }

        public void Update()
        {
           
                GeneticAlgorithm.NewGeneration();
                geneticItteration += 1;

                var bestGene = GeneticAlgorithm.BestGenes; //Best genome
                var bestfitness = GeneticAlgorithm.BestFitness;

                geneticSnapshots.Add(bestfitness);

                var generation = GeneticAlgorithm.Generation;
                var popCount = GeneticAlgorithm.Population.Count;

                if (GeneticAlgorithm.BestFitness == 1)
                {
                    MessageBox.Show("100% accuracy.");
                }
             
        }

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs, Pacman p, Food f)
        {
            Update();
            
        }

        public override void Start(Pacman pacman, FormElements formElements, GameBoard gameBoard, Food food)
        {
            Start();

            myTimer.Tick += (sender, e) => TimerEventProcessor(sender, e, pacman, food);
            myTimer.Interval = 100;
            myTimer.Start();
        }

        private int GetRandomMovement()
        {
            int i = random.Next(4) + 1;
            return i;
        }

        /// <summary>
        /// How much a gene pool eats given the parameters
        /// </summary>
        /// <param name="genes"></param>
        /// <returns></returns>
        public int HowMuchEaten(int[] genes)
        {
            for (int i = 0; i < genes.Length; i++)
            {
                pacman.nextDirection = genes[i];
                pacman.MovePacman(genes[i]);
                
                //if (Form1.ActiveForm != null && i % 10 == 0)
                //{
                //    foreach (Control a in Form1.ActiveForm.Controls)
                //    {
                //        //if (!a.Name.StartsWith("Food"))
                //            a.Refresh();
                //    }
                //}
            }

            int totalForGene = food.TotalAmount - food.Amount;
            restartGame(); //restart when calculated
            return totalForGene;
        }

        private float FitnessFunction(int index)
        {
            float score = 0;
            DNA<int> dna = GeneticAlgorithm.Population[index];

            var genePoolAte = HowMuchEaten(dna.Genes);

            float tempAte = genePoolAte;
            tempAte /= 244;
            tempAte = ((float)Math.Pow(2, tempAte) - 1) / (2 - 1);

            float avg = 0;

            if (geneticSnapshots.Count > 0)
                avg = geneticSnapshots.Average();

            // Performance string
            var perforamnce = $"Ate {genePoolAte} out of 244 on genetic evolution {geneticItteration} ";

            //If the current gene is performing beter than average, we can return its score which makes it possible to evolve.
            bool isScoreAboveAverage = genePoolAte >= geneticItteration && tempAte > avg;

            if (isScoreAboveAverage)
            {
                score += genePoolAte;
            }

            // final score
            score /= 244;
            score = ((float)Math.Pow(2, score) - 1) / (2 - 1);

            if (isScoreAboveAverage)
            {
                if (geneticSnapshots.Count > 2)
                    geneticSnapshots.RemoveAt(0);

                geneticSnapshots.Add(score);
                System.Diagnostics.Debug.WriteLine(perforamnce + $"; snapshot taken for reference, it ran at a score of {score}");
            }
            else
            { 
                System.Diagnostics.Debug.WriteLine(perforamnce);
            }

            return score;
        }
    }
}
