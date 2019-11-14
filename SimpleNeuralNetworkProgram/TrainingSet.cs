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
        private int _PointsNum;
        private int _Width;
        private int _Height;
        
        private Matrix1D[] _Points;
        private float[] _Labels;

        private Random _Gen;

        public TrainingSet(int PointsNum, int Width, int Height)
        {
            _PointsNum = PointsNum;
            _Width = Width;
            _Height = Height;
            _Gen = new Random();
            _Points = new Matrix1D[PointsNum - 1 + 1];
            _Labels = new float[PointsNum - 1 + 1];
        }
        

        public Matrix1D[] Points
        {
            get
            {
                return _Points;
            }
        }

        public float[] Labels
        {
            get
            {
                return _Labels;
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
