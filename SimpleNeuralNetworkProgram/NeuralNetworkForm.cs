using NeuralNetworkLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SimpleNeuralNetworkProgram
{
    public partial class NeuralNetworkForm : Form
    {
        private NeuralNetwork nn;

        public  NeuralNetworkForm()
        {
            InitializeComponent();
            inputChart.Series.Add("InputVectors");
            inputChart.Series["InputVectors"].ChartType = SeriesChartType.Point;
            outputChart.Series.Add("OutputVectors");
            outputChart.Series["OutputVectors"].ChartType = SeriesChartType.Point;
        }

        private void ShowInputVectorsOnChart()
        {
            inputChart.Series["InputVectors"].Points.Clear();
            foreach (List<double> pattern in nn.Patterns)
            {
                inputChart.Series["InputVectors"].Points.AddXY(pattern[0], pattern[1]);
            }
        }

        private void EndEpochEvent(object sender, EndEpochEventArgs e)
        {
            if (isVisibleOutputVectors.Checked)
            {
                outputChart.Series["OutputVectors"].Points.Clear();
                for (int i = 0; i < nn.OutputLayerDimension; i++)
                for (int j = 0; j < nn.OutputLayerDimension; j++)
                {
                    outputChart.Series["OutputVectors"].Points
                        .AddXY(nn.OutputLayer[i, j].Weights[0], nn.OutputLayer[i, j].Weights[1]);
                }
            }

            Application.DoEvents();
        }

        //private void nn_EndIterationEvent(object sender, EventArgs e)
        //{
        //    if (pbStatus.Value < pbStatus.Maximum) pbStatus.Value++;
        //}

        private void ListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            inputChart.Series["InputVectors"].Points.Clear();
            outputChart.Series["OutputVectors"].Points.Clear();
            inputChart.Series["InputVectors"].Points.AddXY(nn.Patterns[lbVectors.SelectedIndex][0], nn.Patterns[lbVectors.SelectedIndex][1]);
            Neuron Winner = nn.FindWinner(nn.Patterns[lbVectors.SelectedIndex]);
            outputChart.Series["OutputVectors"].Points.AddXY(Winner.Weights[0], Winner.Weights[1]);
            neuralNetworkVisualizer.LightUpThePixel(Winner.Coordinate.X, Winner.Coordinate.Y);
        }

        private void AddLegend()
        {
            panelLegend.Controls.Clear();
            Label label = new Label();
            label.Name = "lblLegend";
            label.Top = 5;
            label.Left = 5;
            label.Text = "Legend";
            label.AutoSize = true;
            panelLegend.Controls.Add(label);
            for (int i = 0; i < nn.ExistentClasses.Count; i++)
            {
                Label lbl = new Label();
                lbl.Name = "lbl" + nn.ExistentClasses.Keys[i];
                lbl.Text = " - " + nn.ExistentClasses.Keys[i];
                lbl.Top = 20 * (i + 1);
                lbl.AutoSize = true;
                lbl.Left = 15 + (int)lbl.Font.Size;
                this.panelLegend.Controls.Add(lbl);

                Panel panel = new Panel();
                panel.Name = "panel" + nn.ExistentClasses.Keys[i];
                panel.Top = 20 * (i + 1) + (int)lbl.Font.Size / 2;
                panel.Left = 15;
                panel.Width = (int)lbl.Font.Size;
                panel.Height = (int)lbl.Font.Size;
                panel.BackColor = nn.UsedColors[i];
                this.panelLegend.Controls.Add(panel);
            }
        }

        //private void lbVectors_Leave(object sender, EventArgs e)
        //{
        //    inputChart.Series["InputVectors"].Points.Clear();
        //    outputChart.Series["OutputVectors"].Points.Clear();
        //}

        private void AddPatternsToListBox()
        {
            lbVectors.Items.Clear();
            string patternString;
            for (int i = 0; i < nn.Patterns.Count; i++)
            {
                patternString = "";
                patternString += nn.Classes[i] + " ";
                for (int j = 0; j < nn.InputLayerDimension; j++)
                    patternString += nn.Patterns[i][j].ToString("g2") + " ";
                lbVectors.Items.Add(patternString);
            }
        }

        private void SwitchControls(bool switcher)
        {
            inputParams.Enabled = switcher;
            lbVectors.Enabled = switcher;
        }
        
        private void loadVectorsStart_Click(object sender, EventArgs e)
        {
            int numberOfNeurons = (int)Math.Sqrt(Int32.Parse(textBoxCountOfNeurons.Text));
            Functions f = Functions.Discrete;

            Double tbEpsilon2 = Double.Parse(textBoxEpsilon.Text.Replace('.', ','));
            nn = new NeuralNetwork(numberOfNeurons, 0, tbEpsilon2, f);
            nn.EndEpochEvent += new EndEpochEventHandler(EndEpochEvent);
            nn.Normalize = this.checkBoxNormalize.Checked;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
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
            }
        }
    }
}
