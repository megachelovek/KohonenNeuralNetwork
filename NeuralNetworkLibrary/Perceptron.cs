using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class SurroundingClass
    {
        private List<double> weights;

        private float learnRate;

        public SurroundingClass(int PerceptronSize, float LRate)
        {
            this.weights = new List<double>(PerceptronSize);
            Random rnd = new Random();
            for (int i = 0; i < this.weights.Capacity; i++)
            {
                this.weights.Add(rnd.NextDouble());
            }
            this.learnRate = LRate;
        }

        public List<double> HypothesisFunction(List<double> Input)
        {
            if (Input.Count != this.weights.Count)
                throw new Exception($"Input Matrix size shall match " + this.weights.Count.ToString());
            List<double> HypothesisFun = Input;
            
            return HypothesisFun;
        }

        public float CalcOutput(List<double> Input, IActivationFunction ActivationFunction)
        {
            float Hypothesis_x = (float) SumAllElements(this.HypothesisFunction(Input));

            return ActivationFunction.Function(Hypothesis_x);
        }

        public float CalcOutput(double Input, IActivationFunction ActivationFunction)
        {
            float Hypothesis_x = (float)SumAllElements(this.HypothesisFunction( new List<double>{Input}));

            return ActivationFunction.Function(Hypothesis_x);
        }

        public void TrainPerceptron(List<List<double>> Input, float[] Label, IActivationFunction ActivationFunction)
        {
            int countOfVectors = Input.Count();  // training set size
            int iterationCounter = 0;      // number of iterations
            float MSE = 0;           // To track error MSE
            float IterateError = 0;  // To Track error in each iteration

            do
            {
                iterationCounter += 1;
                MSE = 0;
                for (int I = 0; I <= countOfVectors - 1; I++)
                {
                    float Out = this.CalcOutput(Input[I], ActivationFunction);
                    IterateError = Out - Label[I];
                    for (int Index = 0; Index <= this.weights.Count - 1; Index++)
                    {
                        this.weights[Index] = this.weights[Index] - this.learnRate * IterateError * Index;
                    }

                    MSE += IterateError;
                    IterateError = 0;
                }

                // Calculate MSE
                MSE = (float) (1 / (double) (2 * countOfVectors) * MSE * MSE);
            } while (MSE < 0.001 || iterationCounter > 10000); // Reset error// iterate through training set

        }

        public double SumAllElements(List<double> elements)
        {
            double result = 0;
            foreach (var elemList in elements)
            {
                result += elemList;
            }

            return result;
        }

    }
}
