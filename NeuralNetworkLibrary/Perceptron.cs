using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    class SurroundingClass
    {
        private var Copy; 
        
        private int Size;

        private Matrix1D Weights;

        private float _LearnRate;

        public SurroundingClass(int PerceptronSize, float LRate)
        {
            this.Size = PerceptronSize;
            this.Weights = new Matrix1D(this.Size);
            this.Weights.RandomizeValues(-100, 100);
            this._LearnRate = LRate;
        }

        public Matrix1D HypothesisFunction(Matrix1D Input)
        {
            if (Input.Size != this.Size)
                throw new Exception("Input Matrix size shall match " + this.Size.ToString);
            Matrix1D HypothesisFun = new Matrix1D(this.Size);

            HypothesisFun = Input.Product(Weights);
            return HypothesisFun;
        }


        public float CalcOutput(Matrix1D Input, IActivationFunction ActivationFunction)
        {
            float Hypothesis_x = this.HypothesisFunction(Input).Sum;

            return ActivationFunction.Function(Hypothesis_x);
        }

        public void TrainPerceptron(Matrix1D[] Input, float[] Label, IActivationFunction ActivationFunction)
        {
            int m = Input.Count;  // training set size
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
                    for (int Index = 0; Index <= this.Size - 1; Index++)
                        this._Weights.Values(Index) = this._Weights.Values(Index) - this.LearnRate * IterateError * Input[I].GetValue(Index);
                    MSE += IterateError;
                    IterateError = 0;
                }
                // Calculate MSE
                MSE = 1 / (double)(2 * m) * MSE * MSE;
            }
            while (!MSE < 0.001 || Counter > 10000)// Reset error// iterate through training set
                ;
        }

    }
}
