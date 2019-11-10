using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NeuralNetworkLibrary
{
    public class Neuron
    {
        private readonly double alpha0 = 0.1;
        public string ClassAfterLearning;
        private Point coordinate;
        private double Nyu2 = 0.5;
        private int sigma0;
        private readonly Dictionary<string, int> similarityClassForLearning;
        private double tau1;
        private readonly int tau2 = 1000;
        private List<double> weights;
        private int weightsdimension;

        public double Norm
        {
            get
            {
                double norm = 0;
                foreach (var d in weights) norm += d;
                norm = norm / weightsdimension;
                return norm;
            }
        }

        /// <summary>
        ///     (5) ЭТАП Алгоритм WTA Модификация весов + Возвращение текущего СКО
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="winnerCoordinate"></param>
        /// <param name="iteration"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public double ModifyWeights(List<double> pattern, Point winnerCoordinate, int iteration, Functions f,
            double countOfNeurons)
        {
            double avgDelta = 0;
            double modificationValue = 0;
            for (var i = 0; i < weightsdimension; i++)
            {
                modificationValue =
                    NyuFormula(iteration) * h(winnerCoordinate, f) * (pattern[i] - weights[i]); // WTA формула
                //modificationValue = Nyu2 * (pattern[i] - weights[i]) + weights[i];
                //h(winnerCoordinate, countOfNeurons,f) * (pattern[i] - weights[i]);
                weights[i] += modificationValue;
                avgDelta += modificationValue; // Здесь сумма всех весов
            }

            avgDelta = avgDelta / weightsdimension;
            return avgDelta;
        }

        //СПОРНАЯ ШТУКА, ЛУЧШЕ ПОКА НЕ ИСПОЛЬЗОВАТЬ
        public void UpdateSimilarMap(string nameClass, Dictionary<string, int> currentClasses)
        {
            //Dictionary<string, int> sortedDict = currentClasses.OrderByDescending(pair => pair.Value)
            //    .Take(currentClasses.Count).ToDictionary(pair => pair.Key, pair => pair.Value);
            //int index = Array.IndexOf(sortedDict.Keys.ToArray(), nameClass)-1;
            //double differenceBetweenClasses = (Math.Round((double)sortedDict.Values.Max() / currentClasses[nameClass])/5)-3;
            if (!similarityClassForLearning.ContainsKey(nameClass))
                similarityClassForLearning.Add(nameClass, 1);
            else
                similarityClassForLearning[nameClass]++;
        }

        //СПОРНАЯ ШТУКА, ЛУЧШЕ ПОКА НЕ ИСПОЛЬЗОВАТЬ
        public string GetMaxSimilarClass()
        {
            return similarityClassForLearning.Aggregate((x, y) => x.Value > y.Value ? x : y).Key ?? null;
        }

        #region Второстепенные функции

        private void InitializeVariables(int sigma0)
        {
            Iteration = 1;
            this.sigma0 = sigma0;
            tau1 = 1000 / Math.Log(sigma0);
        }

        private double NyuFormula(int t)
        {
            return alpha0 * Math.Exp(-t / tau2);
        }

        //private double Sigma(int t)
        //{
        //    double value = sigma0 * Math.Exp(-t / tau1);
        //    return value;
        //}

        public List<double> Weights
        {
            get => weights;
            set
            {
                weights = value;
                weightsdimension = weights.Capacity;
            }
        }

        public Point Coordinate
        {
            get => coordinate;
            set => coordinate = value;
        }

        public int Iteration { get; private set; }

        public Neuron(int x, int y, int sigma0)
        {
            coordinate.X = x;
            coordinate.Y = y;
            InitializeVariables(sigma0);
            similarityClassForLearning = new Dictionary<string, int>();
        }

        public Neuron(Point coordinate, int sigma0)
        {
            this.coordinate = coordinate;
            InitializeVariables(sigma0);
        }

        private double Alpha(int t)
        {
            var value = alpha0 * Math.Exp(-t / tau2);
            return value;
        }

        private double h(Point winnerCoordinate, Functions f)
        {
            double result = 0;
            double distance = 0;
            switch (f)
            {
                case Functions.Discrete:
                {
                    distance = Math.Abs(Coordinate.X - winnerCoordinate.X) +
                               Math.Abs(Coordinate.Y - winnerCoordinate.Y);
                    switch ((int) distance)
                    {
                        case 0:
                            result = 1;
                            break;
                        case 1:
                            result = 0.5f;
                            break;
                        case 2:
                            result = 0.25f;
                            break;
                        case 3:
                            result = 0.125f;
                            break;
                    }

                    break;
                }
                case Functions.EuclideanMeasure:
                {
                    distance = Math.Sqrt(Math.Pow(winnerCoordinate.X - coordinate.X, 2) +
                                         Math.Pow(winnerCoordinate.Y - coordinate.Y, 2));
                    result = Math.Exp(-(distance * distance) / Math.Pow(NyuFormula(Iteration), 2));

                    //distance = Math.Sqrt(Math.Pow((winnerCoordinate.X - coordinate.X), 2) - Math.Pow((winnerCoordinate.Y - coordinate.Y), 2)); //Евклидова мера 
                    return result;
                }
                    break;
            }

            return result;
        }

        #endregion
    }
}