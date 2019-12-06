﻿using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NeuralNetworkLibrary;

namespace SimpleNeuralNetworkProgram
{
    public partial class NeuralNetworkForm : Form
    {
        private string fileName;
        private NeuralNetwork nn;

        public NeuralNetworkForm()
        {
            InitializeComponent();
            inputChart.Series.Add("InputVectors");
            inputChart.Series["InputVectors"].ChartType = SeriesChartType.Point;
            outputChart.Series.Add("OutputVectors");
            outputChart.Series["OutputVectors"].ChartType = SeriesChartType.Point;
            inputChart.ChartAreas[0].AxisX.Enabled = AxisEnabled.Auto;
            inputChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.Auto;
            outputChart.ChartAreas[0].AxisX.Enabled = AxisEnabled.Auto;
            outputChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.Auto;
        }

        private void ShowInputVectorsOnChart()
        {
            inputChart.Series["InputVectors"].Points.Clear();
            foreach (var pattern in nn.Patterns) inputChart.Series["InputVectors"].Points.AddXY(pattern[0], pattern[1]);
        }

        private void EndEpochEvent(object sender, EndEpochEventArgs e)
        {
            if (isVisibleOutputVectors.Checked)
            {
                outputChart.Series["OutputVectors"].Points.Clear();
                for (var i = 0; i < nn.OutputLayerDimensionInitialize; i++)
                for (var j = 0; j < nn.OutputLayerDimensionInitialize; j++)
                    outputChart.Series["OutputVectors"].Points
                        .AddXY(
                            (object) (double.TryParse(nn.OutputLayer[i][j].Weights[0].ToString(), out var d)
                                ? ValidateDataForChart(d)
                                : 0),
                            double.TryParse(nn.OutputLayer[i][j].Weights[1].ToString(), out var f)
                                ? ValidateDataForChart(f)
                                : 0);
                //.AddXY((object)((double.TryParse(nn.OutputLayer[i, j].Weights[0].ToString(), out double d)) ? (double.IsNaN(nn.OutputLayer[i, j].Weights[0])) ? 0 : (nn.OutputLayer[i, j].Weights[0] < Double.MinValue) ? Double.MinValue : d : 0),
                //    ((double.TryParse(nn.OutputLayer[i, j].Weights[1].ToString(), out double f)) ? (double.IsNaN(nn.OutputLayer[i, j].Weights[1])) ? 0 : (nn.OutputLayer[i, j].Weights[0] < Double.MinValue) ? Double.MinValue : f : 0));
            }

            Application.DoEvents();
        }

        private double ValidateDataForChart(double number)
        {
            if (!double.IsNaN(number))
                if (number < 0.000000000000000001E+10)
                    return 0.000000000000000001E+10;

            return 0;
        }

        private void ListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            inputChart.Series["InputVectors"].Points.Clear();
            outputChart.Series["OutputVectors"].Points.Clear();
            inputChart.Series["InputVectors"].Points.AddXY(nn.Patterns[lbVectors.SelectedIndex][0],
                nn.Patterns[lbVectors.SelectedIndex][1]);
            var Winner = nn.FindWinner(nn.Patterns[lbVectors.SelectedIndex]);
            outputChart.Series["OutputVectors"].Points.AddXY(Winner.Weights[0], Winner.Weights[1]);
            neuralNetworkVisualizer.LightUpThePixel(Winner.Coordinate.X, Winner.Coordinate.Y);
        }

        private void AddLegend()
        {
            panelLegend.Controls.Clear();
            var label = new Label();
            label.Name = "lblLegend";
            label.Top = 5;
            label.Left = 5;
            label.Text = "Legend";
            label.AutoSize = true;
            panelLegend.Controls.Add(label);
            int i = 0, ii = 0;
            foreach (var legendaColor in nn.LegendaColors)
            {
                var lbl = new Label();
                lbl.Name = "lbl" + legendaColor.Value;
                lbl.Text = " - " + legendaColor.Value;
                lbl.Top = 20 * (i + 1);
                lbl.AutoSize = true;
                lbl.Left = 15 + (int) lbl.Font.Size;
                panelLegend.Controls.Add(lbl);
                i++;

                var panel = new Panel();
                panel.Name = "panel" + legendaColor.Value;
                panel.Top = 20 * (ii + 1) + (int) lbl.Font.Size / 2;
                panel.Left = 15;
                panel.Width = (int) lbl.Font.Size;
                panel.Height = (int) lbl.Font.Size;
                panel.BackColor = legendaColor.Key;
                panelLegend.Controls.Add(panel);
                ii++;
            }
        }

        private void AddPatternsToListBox()
        {
            lbVectors.Items.Clear();
            string patternString;
            for (var i = 0; i < nn.Patterns.Count; i++)
            {
                patternString = "";
                patternString += nn.Classes[i] + " ";
                for (var j = 0; j < nn.InputLayerDimension; j++)
                    patternString += nn.Patterns[i][j].ToString("g2") + " ";
                lbVectors.Items.Add(patternString);
            }
        }

        private void SwitchControls(bool switcher)
        {
            inputParams.Enabled = switcher;
            lbVectors.Enabled = switcher;
        }

        private void CheckSuccessPercentage()
        {
            var success = 0;
            var fail = 0;
            for (var i = 0; i < nn.Patterns.Count; i++)
            {
                var Winner = nn.FindWinner(nn.Patterns[i]);
                //int indexColor = nn.LegendaColors..IndexOf(nn.ColorMatrixNn[Winner.Coordinate.X, Winner.Coordinate.Y]);
                //if (nn.Classes[i] == Winner.GetMaxSimilarClass())
                //if (nn.Classes[i] == nn.OutputLayer[Winner.Coordinate.X, Winner.Coordinate.Y].ClassAfterLearning)
                if (nn.LegendaColors.FirstOrDefault(x => x.Value == nn.Classes[i]).Key ==
                    nn.ColorMatrixNn[Winner.Coordinate.X, Winner.Coordinate.Y])
                    success++;
                else
                    fail++;

                lbVectors.Items.Cast<string>();
                var itemText = lbVectors.Items[i].ToString();
                lbVectors.Items.RemoveAt(i);
                //lbVectors.Items.Insert(i, itemText+$"[{nn.ExistentClasses.Keys[indexColor]}]");
                lbVectors.Items.Insert(i,
                    itemText + $"[{nn.LegendaColors[nn.ColorMatrixNn[Winner.Coordinate.X, Winner.Coordinate.Y]]}]");
                successLabel.Text = $"Успешно={success}/{nn.Patterns.Count}";
            }


            //SurroundingClass perceptron = new SurroundingClass(nn.Patterns[0].Count,0.1f);
            //perceptron.TrainPerceptron(nn.Patterns, answers, new IdentityFunction());
        }

        /// <summary>
        ///     Заполняет массив лейбл для передачи правильных ответов классов в виде цифр
        /// </summary>
        /// <param name="patterns"></param>
        /// <returns></returns>
        private double[] CreateLabelForPerceptron()
        {
            var result = new double[nn.Patterns.Count];
            for (var i = 0; i < nn.Patterns.Count; i++) result[i] = nn.ExistentClasses[nn.Classes[i]];

            return result;
        }


        private void loadVectorsStart_Click(object sender, EventArgs e)
        {
            var numberOfNeurons = (int) Math.Sqrt(int.Parse(textBoxCountOfNeurons.Text));
            var f = Functions.Discrete;

            var tbEpsilon2 = double.Parse(textBoxEpsilon.Text.Replace('.', ','));
            if (nn == null)
            {
                nn = new NeuralNetwork(numberOfNeurons, 0, tbEpsilon2, f);
                nn.EndEpochEvent += EndEpochEvent;
                nn.Normalize = checkBoxNormalize.Checked;
            }

            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName = ofd.FileName;
                nn.ReadDataFromFile(ofd.FileName);
                neuralNetworkVisualizer.Matrix = null;
                panelLegend.Visible = false;
                ShowInputVectorsOnChart();
                AddPatternsToListBox();
                SwitchControls(false);
                nn.StartLearning();
                neuralNetworkVisualizer.Matrix = nn.ColorSOFM();
                neuralNetworkVisualizer.Invalidate();
                panelLegend.Visible = true;
                AddLegend();
                SwitchControls(true);
                CheckSuccessPercentage();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var success = 0;
            var fail = 0;
            var answers = CreateLabelForPerceptron();
            var perceptron = new NeuralNetwork(5, 0, 1);
            perceptron.Patterns = nn.Patterns;
            perceptron.AddNeuralLayer(6, 0.1); //Количество параметров
            perceptron.AddNeuralLayer(5, 0.1);
            perceptron.Build();
            perceptron.ReadDataFromFile(fileName);
            perceptron.TrainPerceptron(nn.Patterns, answers, 200000,1.1);
            perceptron.CreateClassNamesForEachNeuron();

            for (var i = 0; i < nn.Patterns.Count; i++)
            {
                var Winner = perceptron.FindWinner(nn.Patterns[i]); //PERSEPTRON
                if (Winner.className == nn.Classes[i])
                    success++;
                else
                    fail++;

                lbVectors.Items.Cast<string>();
                var itemText = lbVectors.Items[i].ToString();
                lbVectors.Items.RemoveAt(i);
                lbVectors.Items.Insert(i,
                    itemText + $"  <{Winner.className}>");
                successLabel2.Text = $"Успешно={success}/{nn.Patterns.Count}";
            }
        }
    }
}