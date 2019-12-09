namespace NeuralNetworkLibrary
{
    public class StepFunction : IActivationFunction
    {
        public float Function(float x)
        {
            if (x >= 0)
                return 1;
            return 0;
        }

        public float Derivative(float x)
        {
            return Function(x);
        }

        public float Function(float x, float Theta)
        {
            if (x >= Theta)
                return 1;
            return 0;
        }
    }
}