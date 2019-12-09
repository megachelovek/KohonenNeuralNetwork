namespace NeuralNetworkLibrary.PerceptronLibrary
{
    public class Dendrite
    {
        public double InputPulse { get; set; }

        public double SynapticWeight { get; set; }

        public bool Learnable { get; set; } = true;
    }
}