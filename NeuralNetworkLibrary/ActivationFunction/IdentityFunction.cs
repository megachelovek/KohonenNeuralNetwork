namespace NeuralNetworkLibrary
{
    public class IdentityFunction : IActivationFunction
    {
        public float Function(float x)
        {
            return x;
        }

        public float Derivative(float x)
        {
            return 1;
        }
    }
}