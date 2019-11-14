using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    class SurroundingClass
    {

        private List<double> weights;

        private float learnRate;

        public SurroundingClass(int PerceptronSize, float LRate)
        {
            this.weights = new List<double>(PerceptronSize);
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

        public void TrainPerceptron(List<double> Input, float[] Label, IActivationFunction ActivationFunction)
        {
            int m = Input.Count();  // training set size
            int Counter = 0;      // number of iterations
            float MSE = 0;           // To track error MSE
            float IterateError = 0;  // To Track error in each iteration

            do
            {
                Counter += 1;
                MSE = 0;
                for (int I = 0; I <= m - 1; I++)
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
                MSE = (float) (1 / (double) (2 * m) * MSE * MSE);
            } while (MSE < 0.001 || Counter > 10000); // Reset error// iterate through training set

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
