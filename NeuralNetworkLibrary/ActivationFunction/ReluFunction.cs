using System;

namespace NeuralNetworkLibrary
{
    public class ReluFunction : IActivationFunction
    {
        public float Function(float x)
        {
            return Math.Max(x, 0);
        }

        public float Derivative(float x)
        {
            if (x >= 0)
                return 1;
            return 0;
        }
    }
}