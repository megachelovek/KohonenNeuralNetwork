namespace NeuralNetworkLibrary
{
    public class SignFunction : IActivationFunction
    {
        public float Function(float x)
        {
            if (x >= 0)
                return 1;
            return -1;
        }

        public float Derivative(float x)
        {
            return 0;
        }
    }
}