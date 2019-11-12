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
            Randomize();
        }
        
        public void Randomize()
        {
            for (int I = 0; I <= _PointsNum - 1; I++)
            {
                Points[I] = new Matrix1D(3);
                Points[I].SetValue(0, 1);
                Points[I].SetValue(1, _Gen.Next(0, _Width));
                Points[I].SetValue(2, _Gen.Next(0, _Height));
                Labels[I] = Classify(Points[I].GetValue(1), Points[I].GetValue(2));
            }
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
            float d = 300 - 2 / (double)3 * X;
            if (Y >= d)
                return +1;
            return -1;
        }

        /// <summary>
        ///     ''' Draws points within passed canvas object
        ///     ''' </summary>
        ///     ''' <param name="MyCanv"></param>
        public void Draw(Canvas MyCanv)
        {
            for (int I = 0; I <= _PointsNum - 1; I++)
            {
                if (_Labels[I] == 1)
                    MyCanv.DrawBox(5, Points[I].GetValue(1), Points[I].GetValue(2), Color.Blue);
                else
                    MyCanv.DrawCircle(5, Points[I].GetValue(1), Points[I].GetValue(2), Color.Green);
            }
        }
    }

}
