namespace SimpleNeuralNetworkProgram
{
    partial class NeuralNetworkForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.isVisibleOutputVectors = new System.Windows.Forms.CheckBox();
            this.outputChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.inputChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inputParams = new System.Windows.Forms.GroupBox();
            this.successLabel = new System.Windows.Forms.Label();
            this.lbVectors = new System.Windows.Forms.ListBox();
            this.loadVectorsStart = new System.Windows.Forms.Button();
            this.checkBoxNormalize = new System.Windows.Forms.CheckBox();
            this.textBoxEpsilon = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCountOfNeurons = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.neuralNetworkVisualizer = new SimpleNeuralNetworkProgram.NeuralNetworkVisualizer();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputChart)).BeginInit();
            this.inputParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.isVisibleOutputVectors);
            this.groupBox1.Controls.Add(this.outputChart);
            this.groupBox1.Controls.Add(this.inputChart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 717);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Графики нейронов и векторов";
            // 
            // isVisibleOutputVectors
            // 
            this.isVisibleOutputVectors.AutoSize = true;
            this.isVisibleOutputVectors.Location = new System.Drawing.Point(136, 371);
            this.isVisibleOutputVectors.Name = "isVisibleOutputVectors";
            this.isVisibleOutputVectors.Size = new System.Drawing.Size(217, 24);
            this.isVisibleOutputVectors.TabIndex = 6;
            this.isVisibleOutputVectors.Text = "Включить отображение";
            this.isVisibleOutputVectors.UseVisualStyleBackColor = true;
            // 
            // outputChart
            // 
            chartArea5.AxisX.LabelStyle.Format = "{0:0.000000}";
            chartArea5.AxisY.LabelStyle.Format = "{0:0.0000}";
            chartArea5.Name = "ChartArea1";
            this.outputChart.ChartAreas.Add(chartArea5);
            this.outputChart.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.outputChart.IsSoftShadows = false;
            this.outputChart.Location = new System.Drawing.Point(10, 398);
            this.outputChart.Name = "outputChart";
            this.outputChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series5.ChartArea = "ChartArea1";
            series5.Name = "Series1";
            this.outputChart.Series.Add(series5);
            this.outputChart.Size = new System.Drawing.Size(470, 300);
            this.outputChart.TabIndex = 5;
            this.outputChart.Text = "chart1";
            // 
            // inputChart
            // 
            chartArea6.AxisX.LabelStyle.Format = "{0:0.000000}";
            chartArea6.Name = "ChartArea1";
            this.inputChart.ChartAreas.Add(chartArea6);
            this.inputChart.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.inputChart.IsSoftShadows = false;
            this.inputChart.Location = new System.Drawing.Point(10, 60);
            this.inputChart.Name = "inputChart";
            this.inputChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series6.ChartArea = "ChartArea1";
            series6.Name = "Series1";
            this.inputChart.Series.Add(series6);
            this.inputChart.Size = new System.Drawing.Size(470, 300);
            this.inputChart.TabIndex = 3;
            this.inputChart.Text = "chart1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Входные вектора, обработанные";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выходной слой";
            // 
            // inputParams
            // 
            this.inputParams.Controls.Add(this.successLabel);
            this.inputParams.Controls.Add(this.lbVectors);
            this.inputParams.Controls.Add(this.loadVectorsStart);
            this.inputParams.Controls.Add(this.checkBoxNormalize);
            this.inputParams.Controls.Add(this.textBoxEpsilon);
            this.inputParams.Controls.Add(this.label4);
            this.inputParams.Controls.Add(this.textBoxCountOfNeurons);
            this.inputParams.Controls.Add(this.label3);
            this.inputParams.Location = new System.Drawing.Point(518, 12);
            this.inputParams.Name = "inputParams";
            this.inputParams.Size = new System.Drawing.Size(692, 717);
            this.inputParams.TabIndex = 3;
            this.inputParams.TabStop = false;
            this.inputParams.Text = "Входные параметры";
            // 
            // successLabel
            // 
            this.successLabel.AutoSize = true;
            this.successLabel.Location = new System.Drawing.Point(159, 78);
            this.successLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.successLabel.Name = "successLabel";
            this.successLabel.Size = new System.Drawing.Size(13, 20);
            this.successLabel.TabIndex = 7;
            this.successLabel.Text = " ";
            // 
            // lbVectors
            // 
            this.lbVectors.FormattingEnabled = true;
            this.lbVectors.ItemHeight = 20;
            this.lbVectors.Location = new System.Drawing.Point(6, 255);
            this.lbVectors.Name = "lbVectors";
            this.lbVectors.Size = new System.Drawing.Size(678, 444);
            this.lbVectors.TabIndex = 6;
            this.lbVectors.SelectedIndexChanged += new System.EventHandler(this.ListBoxSelectedIndexChanged);
            // 
            // loadVectorsStart
            // 
            this.loadVectorsStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loadVectorsStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.loadVectorsStart.Location = new System.Drawing.Point(6, 178);
            this.loadVectorsStart.Name = "loadVectorsStart";
            this.loadVectorsStart.Size = new System.Drawing.Size(680, 71);
            this.loadVectorsStart.TabIndex = 5;
            this.loadVectorsStart.Text = "Загрузить вектора и начать обучение сети";
            this.loadVectorsStart.UseVisualStyleBackColor = false;
            this.loadVectorsStart.Click += new System.EventHandler(this.loadVectorsStart_Click);
            // 
            // checkBoxNormalize
            // 
            this.checkBoxNormalize.AutoSize = true;
            this.checkBoxNormalize.Location = new System.Drawing.Point(6, 149);
            this.checkBoxNormalize.Name = "checkBoxNormalize";
            this.checkBoxNormalize.Size = new System.Drawing.Size(292, 24);
            this.checkBoxNormalize.TabIndex = 4;
            this.checkBoxNormalize.Text = "Нормализовать входные вектора";
            this.checkBoxNormalize.UseVisualStyleBackColor = true;
            // 
            // textBoxEpsilon
            // 
            this.textBoxEpsilon.Location = new System.Drawing.Point(6, 105);
            this.textBoxEpsilon.Name = "textBoxEpsilon";
            this.textBoxEpsilon.Size = new System.Drawing.Size(678, 26);
            this.textBoxEpsilon.TabIndex = 3;
            this.textBoxEpsilon.Text = "0,008";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Точность до, СКО";
            // 
            // textBoxCountOfNeurons
            // 
            this.textBoxCountOfNeurons.Location = new System.Drawing.Point(6, 45);
            this.textBoxCountOfNeurons.Name = "textBoxCountOfNeurons";
            this.textBoxCountOfNeurons.Size = new System.Drawing.Size(678, 26);
            this.textBoxCountOfNeurons.TabIndex = 1;
            this.textBoxCountOfNeurons.Text = "12";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(405, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Количество нейронов (лучше брать число-квадрат)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1462, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Нейроны - победители";
            // 
            // panelLegend
            // 
            this.panelLegend.AutoSize = true;
            this.panelLegend.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLegend.Location = new System.Drawing.Point(1216, 34);
            this.panelLegend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(2, 2);
            this.panelLegend.TabIndex = 6;
            this.panelLegend.Visible = false;
            // 
            // neuralNetworkVisualizer
            // 
            this.neuralNetworkVisualizer.Location = new System.Drawing.Point(1462, 37);
            this.neuralNetworkVisualizer.Matrix = null;
            this.neuralNetworkVisualizer.Name = "neuralNetworkVisualizer";
            this.neuralNetworkVisualizer.Size = new System.Drawing.Size(320, 260);
            this.neuralNetworkVisualizer.TabIndex = 4;
            this.neuralNetworkVisualizer.ZoomFactor = 25;
            // 
            // NeuralNetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1791, 762);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.neuralNetworkVisualizer);
            this.Controls.Add(this.inputParams);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(22, 791);
            this.Name = "NeuralNetworkForm";
            this.Text = "Нейронная сеть";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputChart)).EndInit();
            this.inputParams.ResumeLayout(false);
            this.inputParams.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart inputChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart outputChart;
        private System.Windows.Forms.GroupBox inputParams;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCountOfNeurons;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxEpsilon;
        private System.Windows.Forms.Button loadVectorsStart;
        private System.Windows.Forms.CheckBox checkBoxNormalize;
        private System.Windows.Forms.ListBox lbVectors;
        private NeuralNetworkVisualizer neuralNetworkVisualizer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.CheckBox isVisibleOutputVectors;
        private System.Windows.Forms.Label successLabel;
    }
}

