using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace NeuralNetworkLibrary
{
    public delegate void EndEpochEventHandler(object sender, EndEpochEventArgs e);

    public delegate void EndIterationEventHandler(object sender, EventArgs e);

    public class NeuralNetwork
    {
        private int currentIteration;

        private Dictionary<string, int> currentRatingClasses;
        private readonly Functions function;

        private bool isLearning = true;

        public Dictionary<Color, string> LegendaColors;

        //private List<System.Drawing.Color> usedColors;
        private readonly int numberOfIterations;
        private int numberOfPatterns;
        private readonly double valueSKO;

        public NeuralNetwork(int sqrtOfCountNeurons, int numberOfIterations, double valueSko, Functions f)
        {
            OutputLayerDimension = sqrtOfCountNeurons;
            currentIteration = 1;
            this.numberOfIterations = numberOfIterations;
            function = f;
            valueSKO = valueSko;
            CurrentDelta = 100;
        }

        public Neuron[,] OutputLayer { get; set; }

        public Color[,] ColorMatrixNn { get; private set; }

        //(3) этап Евклидова мера
        private double EuclideanCalculateVectors(List<double> Xi, List<double> Wi)
        {
            double value = 0;
            for (var i = 0; i < Xi.Count; i++)
                value += Math.Pow(Xi[i] - Wi[i], 2);
            value = Math.Sqrt(value);
            return value;
        }

        /// <summary>
        ///     Нормализация, как раз Xj = Xj / (Math.sqrt ( Math.Sum(Xj^2)))
        /// </summary>
        /// <param name="pattern"></param>
        private void NormalizeInputPattern(List<double> pattern)
        {
            double nn = 0;
            for (var i = 0; i < InputLayerDimension; i++) nn += pattern[i] * pattern[i];
            nn = Math.Sqrt(nn);
            for (var i = 0; i < InputLayerDimension; i++) pattern[i] /= nn;
        }

        /// <summary>
        ///     1 эпоха = однократная подача всех обучающих примеров
        ///     Этапы 4 и 5 67
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="iteration"></param>
        private void StartEpoch(List<double> pattern, int iteration)
        {
            var Winner = FindWinner(pattern);
            //Winner.UpdateSimilarMap(classes[iteration], currentRatingClasses);
            //currentRatingClasses[classes[iteration]]++;
            //Winner.ClassAfterLearning = Winner.GetMaxSimilarClass();
            CurrentDelta = 0;
            for (var i = 0; i < OutputLayerDimension; i++)
            for (var j = 0; j < OutputLayerDimension; j++)
                CurrentDelta += OutputLayer[i, j].ModifyWeights(pattern, Winner.Coordinate, currentIteration, function,
                    OutputLayerDimension * OutputLayerDimension);
            currentIteration++;
            //currentSKO = Math.Abs(currentSKO / (outputLayerDimension * outputLayerDimension));// Деление в СКО
            CurrentDelta = Math.Abs(CurrentDelta / (OutputLayerDimension * OutputLayerDimension));
            //currentSKO = Math.Sqrt(1.0 / (Math.Pow(outputLayerDimension, 2) - 1)) * Math.Pow(currentSKO,2);
            var e = new EndEpochEventArgs();
            OnEndEpochEvent(e);
        }

        /// <summary>
        ///     (3 и 4) Поиск минимума, основная функция
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public Neuron FindWinner(List<double> pattern)
        {
            double D = 0;
            var Winner = OutputLayer[0, 0];
            var min = EuclideanCalculateVectors(pattern, OutputLayer[0, 0].Weights);
            for (var i = 0; i < OutputLayerDimension; i++)
            for (var j = 0; j < OutputLayerDimension; j++)
            {
                D = EuclideanCalculateVectors(pattern, OutputLayer[i, j].Weights);
                if (D < min)
                {
                    min = D;
                    Winner = OutputLayer[i, j];
                }
            }

            return Winner;
        }

        public void StartLearning()
        {
            var iterations = 0;
            if (numberOfIterations == 0)
                while (CurrentDelta > valueSKO) //Пока не станет меньше значения
                {
                    var patternsToLearn = new List<List<double>>(numberOfPatterns);
                    foreach (var pArray in Patterns)
                        patternsToLearn.Add(pArray);
                    var randomPattern = new Random();
                    var pattern = new List<double>(InputLayerDimension);
                    for (var i = 0; i < numberOfPatterns; i++)
                    {
                        pattern = patternsToLearn[randomPattern.Next(numberOfPatterns - i)];

                        StartEpoch(pattern, i);

                        patternsToLearn.Remove(pattern);
                    }

                    iterations++;
                    OnEndIterationEvent(new EventArgs());
                }
        }

        #region MyRegion Второстепенные функции

        public bool Normalize { get; set; }

        public List<List<double>> Patterns { get; private set; }

        public List<string> Classes { get; private set; }

        public int InputLayerDimension { get; private set; }

        public int OutputLayerDimension { get; }

        public double CurrentDelta { get; private set; }

        public SortedList<string, int> ExistentClasses { get; private set; }

        //public List<System.Drawing.Color> UsedColors
        //{
        //    get { return usedColors; }
        //}

        private int NumberOfClasses()
        {
            ExistentClasses = new SortedList<string, int>();
            ExistentClasses.Add(Classes[0], 1);
            var k = 0;
            var d = 2;
            for (var i = 1; i < Classes.Count; i++)
            {
                k = 0;
                for (var j = 0; j < ExistentClasses.Count; j++)
                    if (ExistentClasses.IndexOfKey(Classes[i]) != -1)
                        k++;
                if (k == 0)
                {
                    ExistentClasses.Add(Classes[i], d);
                    d++;
                }
            }

            return ExistentClasses.Count;
        }

        public Color[,] ColorSOFM()
        {
            var colorMatrix = new Color[OutputLayerDimension, OutputLayerDimension];
            var numOfClasses = NumberOfClasses();
            var goodColors = new List<Color>();
            goodColors.Add(Color.Red);
            goodColors.Add(Color.Navy);
            goodColors.Add(Color.Green);
            goodColors.Add(Color.Yellow);
            goodColors.Add(Color.Cyan);
            goodColors.Add(Color.CornflowerBlue);
            goodColors.Add(Color.DarkOrange);
            goodColors.Add(Color.GreenYellow);
            goodColors.Add(Color.Bisque);
            var rnd = new Random();
            for (var i = 0; i < numOfClasses - 7; i++)
                goodColors.Add(Color.FromArgb(100, rnd.Next(255), rnd.Next(125) * rnd.Next(2), rnd.Next(255)));
            //usedColors = new List<System.Drawing.Color>(numOfClasses);
            //usedColors.Add(goodColors[0]);
            LegendaColors = new Dictionary<Color, string>();
            //LegendaColors.Add(goodColors[0],classes[0]);
            var k = 0;
            var randomColor = 0;
            var r = new Random();
            foreach (var classs in ExistentClasses)
            {
                randomColor = r.Next(goodColors.Count);
                if (!LegendaColors.ContainsKey(goodColors[randomColor]))
                    LegendaColors.Add(goodColors[randomColor], classs.Key);
                else
                    LegendaColors.Add(Color.FromArgb(100, rnd.Next(255), rnd.Next(125) * rnd.Next(2), rnd.Next(255)),
                        classs.Key);
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
            for (var i = 0; i < OutputLayerDimension; i++)
            for (var j = 0; j < OutputLayerDimension; j++)
                colorMatrix[i, j] = Color.FromKnownColor(KnownColor.ButtonFace);

            for (var i = 0; i < Patterns.Count; i++)
            {
                var n = FindWinner(Patterns[i]);
                //if (isLearning)
                //{
                //OutputLayer[n.Coordinate.X, n.Coordinate.Y].ClassAfterLearning =n.GetMaxSimilarClass();
                //n.ClassAfterLearning = n.GetMaxSimilarClass();
                //LegendaColors[LegendaColors.ElementAt(i).Key] = classes[i];
                //LegendaColors[LegendaColors.ElementAt(existentClasses[classes[i]] - 1).Key] = n.GetMaxSimilarClass();
                //}
                colorMatrix[n.Coordinate.X, n.Coordinate.Y] =
                    LegendaColors.ElementAt(ExistentClasses[Classes[i]] - 1).Key;
                //colorMatrix[n.Coordinate.X, n.Coordinate.Y] = LegendaColors.FirstOrDefault(x => x.Value == n.ClassAfterLearning).Key; ;
            }

            ColorMatrixNn = colorMatrix;
            return colorMatrix;
        }


        /// <summary>
        ///     Берет данные из файла и приводит к нужному формату с которого можно считать
        /// </summary>
        /// <param name="inputDataFileName"></param>
        public void ReadDataFromFile(string inputDataFileName)
        {
            var sr = new StreamReader(inputDataFileName);
            var line = sr.ReadLine();
            var k = 0;
            for (var i = 0; i < line.Length; i++)
                if (line[i] == ' ')
                    k++;

            InputLayerDimension = k;
            var sigma0 = OutputLayerDimension;

            k = 0;
            while (line != null)
            {
                line = sr.ReadLine();
                k++;
            }

            Patterns = new List<List<double>>(k);
            Classes = new List<string>(k);
            numberOfPatterns = k;

            List<double> pattern;

            sr = new StreamReader(inputDataFileName);
            line = sr.ReadLine();

            while (line != null)
            {
                var startPos = 0;
                var endPos = 0;
                var j = 0;
                pattern = new List<double>(InputLayerDimension); // Заполняет первый график двумя первыми числами
                for (var ind = 0; ind < line.Length; ind++)
                {
                    if (line[ind] == ' ' && j != InputLayerDimension)
                    {
                        endPos = ind;
                        pattern.Add(Convert.ToDouble(line.Substring(startPos, endPos - startPos).Replace('.', ',')));
                        startPos = ind + 1;
                        j++;
                    }

                    if (j > InputLayerDimension)
                        throw new InvalidDataException("Wrong file format. Check input data file, and try again");
                }

                if (Normalize) NormalizeInputPattern(pattern); // (1) Этап нормализация входных параметров
                Patterns.Add(pattern);
                if (line.LastIndexOf(' ') != -1)
                {
                    startPos = line.LastIndexOf(' ');
                    Classes.Add(line.Substring(startPos));
                    line = sr.ReadLine();
                }
            }

            sr.Close();

            //
            currentRatingClasses = new Dictionary<string, int>();
            foreach (var cl in Classes)
                if (!currentRatingClasses.ContainsKey(cl))
                    currentRatingClasses.Add(cl, 0);

            //
            if (OutputLayer == null)
            {
                OutputLayer = new Neuron[OutputLayerDimension, OutputLayerDimension];

                // (2) Этап Заполнение случайными весами 
                var r = new Random();
                for (var i = 0; i < OutputLayerDimension; i++)
                for (var j = 0; j < OutputLayerDimension; j++)
                {
                    OutputLayer[i, j] = new Neuron(i, j, sigma0);
                    OutputLayer[i, j].Weights = new List<double>(InputLayerDimension);
                    OutputLayer[i, j].UpdateSimilarMap(Classes[r.Next(Classes.Count)], currentRatingClasses);
                    for (k = 0; k < InputLayerDimension; k++) OutputLayer[i, j].Weights.Add(r.NextDouble());
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