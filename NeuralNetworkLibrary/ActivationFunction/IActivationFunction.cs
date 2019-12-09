namespace NeuralNetworkLibrary
{
    public interface IActivationFunction
    {
        float Function(float x);
        float Derivative(float x);
    }
}