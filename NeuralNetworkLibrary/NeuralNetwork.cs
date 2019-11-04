using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public delegate void EndEpochEventHandler(object sender, EndEpochEventArgs e);
    public delegate void EndIterationEventHandler(object sender, EventArgs e);

    public class NeuralNetwork
    {
        private Neuron[,] outputLayer;

        public Neuron[,] OutputLayer
        {
            get { return outputLayer; }
            set { outputLayer = value; }
        }

        private bool isLearning = true;
        private int inputLayerDimension; // Размерность входного слоя
        private int outputLayerDimension; // Размерность выходного слоя
        private int numberOfPatterns;
        private List<List<double>> patterns; //Вектора 
        private List<string> classes;
        private SortedList<string, int> existentClasses;
        //private List<System.Drawing.Color> usedColors;
        private bool normalize;
        private int numberOfIterations;
        private int currentIteration;
        public Color[,] ColorMatrixNn { get; private set; }
        public Dictionary<Color,string> LegendaColors;
        private Functions function;
        private double valueSKO;
        private double currentSKO;

        private Dictionary<string,int> currentRatingClasses;

        //(3) этап Евклидова мера
        private double EuclideanCalculateVectors(List<double> Xi, List<double> Wi)
        {
            double value = 0;
            for (int i = 0; i < Xi.Count; i++)
                value += Math.Pow((Xi[i] - Wi[i]), 2);
            value = Math.Sqrt(value);
            return value;
        }

        /// <summary>
        /// Нормализация, как раз Xj = Xj / (Math.sqrt ( Math.Sum(Xj^2)))
        /// </summary>
        /// <param name="pattern"></param>
        private void NormalizeInputPattern(List<double> pattern)
        {
            double nn = 0;
            for (int i = 0; i < inputLayerDimension; i++)
            {
                nn += (pattern[i] * pattern[i]);
            }
            nn = Math.Sqrt(nn);
            for (int i = 0; i < inputLayerDimension; i++)
            {
                pattern[i] /= nn;
            }
        }

        private void StartEpoch(List<double> pattern,int iteration)
        {
            Neuron Winner = this.FindWinner(pattern);
            //Winner.UpdateSimilarMap(classes[iteration], currentRatingClasses);
            //currentRatingClasses[classes[iteration]]++;
            //Winner.ClassAfterLearning = Winner.GetMaxSimilarClass();
            currentSKO = 0;
            for (int i = 0; i < outputLayerDimension; i++)
                for (int j = 0; j < outputLayerDimension; j++)
                {
                    currentSKO += outputLayer[i, j].ModifyWeights(pattern, Winner.Coordinate, currentIteration, function, outputLayerDimension* outputLayerDimension);
                }
            currentIteration++;
            //currentSKO = Math.Abs(currentSKO / (outputLayerDimension * outputLayerDimension));// Деление в СКО
            currentSKO = Math.Abs(currentSKO / (outputLayerDimension * outputLayerDimension));
            //currentSKO = Math.Sqrt(1.0 / (Math.Pow(outputLayerDimension, 2) - 1)) * Math.Pow(currentSKO,2);
            EndEpochEventArgs e = new EndEpochEventArgs();
            OnEndEpochEvent(e);
        }

        public NeuralNetwork(int sqrtOfCountNeurons, int numberOfIterations, double valueSko, Functions f)
        {
            outputLayerDimension = sqrtOfCountNeurons;
            currentIteration = 1;
            this.numberOfIterations = numberOfIterations;
            function = f;
            this.valueSKO = valueSko;
            currentSKO = 100;
           
        }

        /// <summary>
        /// (3 и 4) Поиск минимума, основная функция
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public Neuron FindWinner(List<double> pattern)
        {
            double D = 0;
            Neuron Winner = outputLayer[0, 0];
            double min = EuclideanCalculateVectors(pattern, outputLayer[0, 0].Weights);
            for (int i = 0; i < outputLayerDimension; i++)
                for (int j = 0; j < outputLayerDimension; j++)
                {
                    D = EuclideanCalculateVectors(pattern, outputLayer[i, j].Weights);
                    if (D < min)
                    {
                        min = D;
                        Winner = outputLayer[i, j];
                    }
                }
            return Winner;
        }

        public void StartLearning()
        {
            int iterations = 0;
            if (numberOfIterations == 0)
            {
                while (currentSKO > valueSKO) //Пока  не станет меньше значения
                {
                    List<List<double>> patternsToLearn = new List<List<double>>(numberOfPatterns);
                    foreach (List<double> pArray in patterns)
                        patternsToLearn.Add(pArray);
                    Random randomPattern = new Random();
                    List<double> pattern = new List<double>(inputLayerDimension);
                    for (int i = 0; i < numberOfPatterns; i++)
                    {
                        pattern = patternsToLearn[randomPattern.Next(numberOfPatterns - i)];

                        StartEpoch(pattern,i);

                        patternsToLearn.Remove(pattern);
                    }
                    iterations++;
                    OnEndIterationEvent(new EventArgs());
                }
            }
        }

        #region MyRegion Второстепенные функции
        public bool Normalize
        {
            get { return normalize; }
            set { normalize = value; }
        }

        public List<List<double>> Patterns
        {
            get { return patterns; }
        }

        public List<string> Classes
        {
            get { return classes; }
        }

        public int InputLayerDimension
        {
            get { return inputLayerDimension; }
        }

        public int OutputLayerDimension
        {
            get { return outputLayerDimension; }
        }

        public double CurrentDelta
        {
            get { return currentSKO; }
        }

        public SortedList<string, int> ExistentClasses
        {
            get { return existentClasses; }
        }

        //public List<System.Drawing.Color> UsedColors
        //{
        //    get { return usedColors; }
        //}

        private int NumberOfClasses()
        {
            existentClasses = new SortedList<string, int>();
            existentClasses.Add(classes[0], 1);
            int k = 0;
            int d = 2;
            for (int i = 1; i < classes.Count; i++)
            {
                k = 0;
                for (int j = 0; j < existentClasses.Count; j++)
                    if (existentClasses.IndexOfKey(classes[i]) != -1) k++;
                if (k == 0)
                {
                    existentClasses.Add(classes[i], d);
                    d++;
                }
            }
            return existentClasses.Count;
        }

        public System.Drawing.Color[,] ColorSOFM()
        {
            System.Drawing.Color[,] colorMatrix = new System.Drawing.Color[outputLayerDimension, outputLayerDimension];
            int numOfClasses = NumberOfClasses();
            List<System.Drawing.Color> goodColors = new List<System.Drawing.Color>();
            goodColors.Add(System.Drawing.Color.Red);
            goodColors.Add(System.Drawing.Color.Navy);
            goodColors.Add(System.Drawing.Color.Green);
            goodColors.Add(System.Drawing.Color.Yellow);
            goodColors.Add(System.Drawing.Color.Cyan);
            goodColors.Add(System.Drawing.Color.CornflowerBlue);
            goodColors.Add(System.Drawing.Color.DarkOrange);
            goodColors.Add(System.Drawing.Color.GreenYellow);
            goodColors.Add(System.Drawing.Color.Bisque);
            Random rnd = new Random();
            for (int i = 0; i < numOfClasses - 7; i++)
            {
                goodColors.Add(Color.FromArgb(100, rnd.Next(255), rnd.Next(125) * rnd.Next(2), rnd.Next(255)));
            }
            //usedColors = new List<System.Drawing.Color>(numOfClasses);
            //usedColors.Add(goodColors[0]);
            LegendaColors = new Dictionary<Color, string>();
            //LegendaColors.Add(goodColors[0],classes[0]);
            int k = 0;
            int randomColor = 0;
            Random r = new Random();
            foreach (var classs in existentClasses)
            {
                randomColor = r.Next(goodColors.Count);
                if (!LegendaColors.ContainsKey(goodColors[randomColor]))
                {
                    LegendaColors.Add(goodColors[randomColor], classs.Key);
                }
                else
                {
                    LegendaColors.Add(Color.FromArgb(100, rnd.Next(255), rnd.Next(125) * rnd.Next(2), rnd.Next(255)), classs.Key);
                }
                
            }
            //while (LegendaColors.Count != numOfClasses)
            //{
            //    k = 0;
            //    randomColor = r.Next(goodColors.Count);
            //    //foreach (System.Drawing.Color cl in LegendaColors.Keys)
            //    //    if (cl == goodColors[randomColor]) k++;
            //    //if (k == 0)
            //    //{
            //    //    LegendaColors.Add(goodColors[randomColor],"");
            //    //}
            //        LegendaColors.Add(goodColors[randomColor], existentClasses.Key);
            //}
            for (int i = 0; i < outputLayerDimension; i++)
                for (int j = 0; j < outputLayerDimension; j++)
                    colorMatrix[i, j] = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ButtonFace);

            for (int i = 0; i < patterns.Count; i++)
            {
                Neuron n = FindWinner(patterns[i]);
                //if (isLearning)
                //{
                    //OutputLayer[n.Coordinate.X, n.Coordinate.Y].ClassAfterLearning =n.GetMaxSimilarClass();
                    //n.ClassAfterLearning = n.GetMaxSimilarClass();
                    //LegendaColors[LegendaColors.ElementAt(i).Key] = classes[i];
                    //LegendaColors[LegendaColors.ElementAt(existentClasses[classes[i]] - 1).Key] = n.GetMaxSimilarClass();
                //}
                colorMatrix[n.Coordinate.X, n.Coordinate.Y] = LegendaColors.ElementAt(existentClasses[classes[i]] - 1).Key;
                //colorMatrix[n.Coordinate.X, n.Coordinate.Y] = LegendaColors.FirstOrDefault(x => x.Value == n.ClassAfterLearning).Key; ;
            }

            this.ColorMatrixNn = colorMatrix;
            return colorMatrix;
        }


        /// <summary>
        /// Берет данные из файла и приводит к нужному формату с которого можно считать
        /// </summary>
        /// <param name="inputDataFileName"></param>
        public void ReadDataFromFile(string inputDataFileName)
        {
            StreamReader sr = new StreamReader(inputDataFileName);
            string line = sr.ReadLine();
            int k = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ') k++;
            }

            inputLayerDimension = k;
            int sigma0 = outputLayerDimension;

            k = 0;
            while (line != null)
            {
                line = sr.ReadLine();
                k++;
            }
            patterns = new List<List<double>>(k);
            classes = new List<string>(k);
            numberOfPatterns = k;

            List<double> pattern;

            sr = new StreamReader(inputDataFileName);
            line = sr.ReadLine();

            while (line != null)
            {
                int startPos = 0;
                int endPos = 0;
                int j = 0;
                pattern = new List<double>(inputLayerDimension); // Заполняет первый график двумя первыми числами
                for (int ind = 0; ind < line.Length; ind++)
                {
                    if (line[ind] == ' ' && j != inputLayerDimension)
                    {
                        endPos = ind;
                        pattern.Add(Convert.ToDouble(line.Substring(startPos, endPos - startPos).Replace('.', ',')));
                        startPos = ind + 1;
                        j++;
                    }
                    if (j > inputLayerDimension) throw new InvalidDataException("Wrong file format. Check input data file, and try again");
                }
                if (normalize) this.NormalizeInputPattern(pattern); // (1) Этап нормализация входных параметров
                patterns.Add(pattern);
                if (line.LastIndexOf(' ') != -1)
                {
                    startPos = line.LastIndexOf(' ');
                    classes.Add(line.Substring(startPos));
                    line = sr.ReadLine();
                }
            }
            sr.Close();

            //
            currentRatingClasses = new Dictionary<string, int>();
            foreach (var cl in classes)
            {
                if(!currentRatingClasses.ContainsKey(cl)) currentRatingClasses.Add(cl, 0);
            }

            //
            if (outputLayer == null)
            {
                outputLayer = new Neuron[outputLayerDimension, outputLayerDimension];

                // (2) Этап Заполнение случайными весами 
                Random r = new Random();
                for (int i = 0; i < outputLayerDimension; i++)
                for (int j = 0; j < outputLayerDimension; j++)
                {
                    outputLayer[i, j] = new Neuron(i, j, sigma0);
                    outputLayer[i, j].Weights = new List<double>(inputLayerDimension);
                    outputLayer[i, j].UpdateSimilarMap(classes[r.Next(classes.Count)], currentRatingClasses);
                    for (k = 0; k < inputLayerDimension; k++)
                    {
                        outputLayer[i, j].Weights.Add(r.NextDouble());
                    }
                }
            }

            
            
        }

        public event EndEpochEventHandler EndEpochEvent;
        public event EndIterationEventHandler EndIterationEvent;

        protected virtual void OnEndEpochEvent(EndEpochEventArgs e)
        {
            if (EndEpochEvent != null)
                EndEpochEvent(this, e);
        }

        protected virtual void OnEndIterationEvent(EventArgs e)
        {
            if (EndIterationEvent != null)
                EndIterationEvent(this, e);
        }

        #endregion
    }
}
