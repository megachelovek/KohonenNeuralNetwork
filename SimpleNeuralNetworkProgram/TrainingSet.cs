using System;
using System.Collections.Generic;

namespace SimpleNeuralNetworkProgram
{
    public class TrainingSet
    {
        private int height;

        private int pointsNum;

        private Random random;
        private int width;

        public TrainingSet(int PointsNum, int Width, int Height)
        {
            pointsNum = PointsNum;
            width = Width;
            height = Height;
            random = new Random();
            Points = new List<double>(PointsNum - 1 + 1);
            Labels = new float[PointsNum - 1 + 1];
        }

        public List<double> Points { get; }

        public float[] Labels { get; }

        /// <summary>
        ///     ''' Creates labels array by checking points against straight line 300 - 2 / 3 * X
        ///     '''
        /// </summary>
        /// '''
        /// <param name="X">Point X coordinate</param>
        /// '''
        /// <param name="Y">Point Y coordinate</param>
        /// '''
        /// <returns></returns>
        private float Classify(float X, float Y)
        {
            var d = 300 - 2 / (double) 3 * X;
            if (Y >= d)
                return +1;
            return -1;
        }
    }
}