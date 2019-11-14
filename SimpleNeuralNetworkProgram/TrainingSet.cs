using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkLibrary;

namespace SimpleNeuralNetworkProgram
{
    public class TrainingSet
    {
        private int pointsNum;
        private int width;
        private int height;
        
        private List<double> points;
        private float[] labels;

        private Random random;

        public TrainingSet(int PointsNum, int Width, int Height)
        {
            pointsNum = PointsNum;
            width = Width;
            height = Height;
            random = new Random();
            points = new List<double>(PointsNum - 1 + 1);
            labels = new float[PointsNum - 1 + 1];
        }
        

        public List<double> Points
        {
            get
            {
                return points;
            }
        }

        public float[] Labels
        {
            get
            {
                return labels;
            }
        }

        /// <summary>
        ///     ''' Creates labels array by checking points against straight line 300 - 2 / 3 * X
        ///     ''' </summary>
        ///     ''' <param name="X">Point X coordinate</param>
        ///     ''' <param name="Y">Point Y coordinate</param>
        ///     ''' <returns></returns>
        private float Classify(float X, float Y)
        {
            double d = 300 - 2 / (double)3 * X;
            if (Y >= d)
                return +1;
            return -1;
        }
        
    }

}
