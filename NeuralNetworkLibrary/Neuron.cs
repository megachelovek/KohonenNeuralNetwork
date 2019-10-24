using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class Neuron
    {
        private List<double> weights;
        private Point coordinate;
        private int iteration;
        private int weightsdimension;
        private int sigma0;
        private double alpha0 = 0.1;
        private double tau1;
        private int tau2 = 1000;

        private double h(Point winnerCoordinate,double countOfNeurons, Functions f)
        {
            double result = 0;
            double distance = 0;
            switch (f)
            {
                case Functions.Discrete:
                    {
                        distance = Math.Abs(this.Coordinate.X - winnerCoordinate.X) + Math.Abs(this.Coordinate.Y - winnerCoordinate.Y);
                        switch ((int)distance)
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
                        distance = Math.Sqrt(Math.Pow((winnerCoordinate.X - coordinate.X), 2) - Math.Pow((winnerCoordinate.Y - coordinate.Y), 2)); //Евклидова мера 
                        return distance;
                    }
                    break;
                //case Functions.Gaus:
                //    {
                //        distance = Math.Sqrt(Math.Pow((winnerCoordinate.X - coordinate.X), 2) + Math.Pow((winnerCoordinate.Y - coordinate.Y), 2));
                //        result = Math.Exp(-(distance * distance) / (Math.Pow(Sigma(iteration), 2)));
                //        return result;
                //        break;
                //    }
                    //case Functions.MexicanHat:
                    //    {
                    //        distance = Math.Sqrt(Math.Pow((winnerCoordinate.X - coordinate.X), 2) + Math.Pow((winnerCoordinate.Y - coordinate.Y), 2));
                    //        result = Math.Exp(-(distance * distance) / Math.Pow(Sigma(iteration), 2)) * (1 - (2 / Math.Pow(Sigma(iteration), 2)) * (distance * distance));                        
                    //        break;
                    //    }
                    //case Functions.FrenchHat:
                    //    {
                    //        int a = 2;
                    //        distance = Math.Abs(this.Coordinate.X - winnerCoordinate.X) + Math.Abs(this.Coordinate.Y - winnerCoordinate.Y);
                    //        if (distance <= a) result = 1;
                    //        else
                    //            if (distance < a && distance <= 3 * a) result = -1 / 3;
                    //            else
                    //                if (distance > 3 * a) result = 0;
                    //        break;
                    //    }
            }
            return result;
        }

        private void InitializeVariables(int sigma0)
        {
            iteration = 1;
            this.sigma0 = sigma0;
            tau1 = 1000 / Math.Log(sigma0);
        }

        private double Nyu(int t)
        {
            return alpha0 * Math.Exp(-t / tau2);
        }

        private double Sigma(int t)
        {
            double value = sigma0 * Math.Exp(-t / tau1);
            return value;
        }

        public List<double> Weights
        {
            get { return weights; }
            set
            {
                weights = value;
                weightsdimension = weights.Capacity;
            }
        }

        public Point Coordinate
        {
            get { return coordinate; }
            set { coordinate = value; }
        }

        public int Iteration
        {
            get { return iteration; }
        }

        public Neuron(int x, int y, int sigma0)
        {
            coordinate.X = x;
            coordinate.Y = y;
            InitializeVariables(sigma0);
        }

        public Neuron(Point coordinate, int sigma0)
        {
            this.coordinate = coordinate;
            InitializeVariables(sigma0);
        }

        /// <summary>
        /// Алгоритм WTA Модификация весов
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="winnerCoordinate"></param>
        /// <param name="iteration"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public double ModifyWeights(List<double> pattern, Point winnerCoordinate, int iteration, Functions f,double countOfNeurons)
        {
            double avgDelta = 0;
            double modificationValue = 0;
            for (int i = 0; i < weightsdimension; i++)
            {
                modificationValue = Nyu(iteration) * h(winnerCoordinate, countOfNeurons,f) * (pattern[i] - weights[i]);
                weights[i] += modificationValue;
                avgDelta += modificationValue;// Здесь сумма всех весов
            }
            avgDelta = avgDelta / weightsdimension;
            return avgDelta;
        }

        public double Norm
        {
            get
            {
                double norm = 0;
                foreach (double d in weights)
                {
                    norm += d;
                }
                norm = norm / this.weightsdimension;
                return norm;
            }
        }
    }
}
