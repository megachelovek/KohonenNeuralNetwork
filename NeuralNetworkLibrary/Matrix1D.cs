using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    class Matrix1D
    {
        public int Size { get; }

        //private double[] weigths { get; }

        public double[] Values { get; set; }

        public Matrix1D(double[] arr)
        {
            this.Values = arr;
        }

        public Matrix1D(int size)
        {
            this.Size = size;
            this.Values = new double[size];
        }

        public void RandomizeValues(int min,int max)
        {
            Random rnd = new Random();
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = rnd.Next(200) - 100;
            }
        }
    }
}
