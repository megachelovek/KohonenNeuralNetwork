using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleNeuralNetworkProgram
{
    public partial class NeuralNetworkVisualizer : Control
    {
        private Color[,] matrix;
        private Color previousColor;
        private Point previousPoint;

        public NeuralNetworkVisualizer()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        public NeuralNetworkVisualizer(Color[,] matrix)
        {
            this.matrix = matrix;
            InitializeComponent();
        }

        public int ZoomFactor { get; set; } = 1;

        public Color[,] Matrix
        {
            get => matrix;
            set
            {
                matrix = value;
                previousColor = Color.YellowGreen;
            }
        }

        private void DrawBorder(Graphics g)
        {
            g.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1),
                new Rectangle(0, 0, Width - 1, Height - 1));
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (matrix != null)
            {
                Width = (int) Math.Sqrt(matrix.Length) * ZoomFactor;
                Height = (int) Math.Sqrt(matrix.Length) * ZoomFactor;
                var currentTop = 0;
                var currentLeft = 0;
                for (var i = 0; i < (int) Math.Sqrt(matrix.Length); i++)
                {
                    currentTop = i * ZoomFactor;
                    for (var j = 0; j < (int) Math.Sqrt(matrix.Length); j++)
                    {
                        currentLeft = j * ZoomFactor;
                        pe.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.WhiteSmoke), 2),
                            new Rectangle(currentTop, currentLeft, ZoomFactor, ZoomFactor));
                        pe.Graphics.FillRectangle(new SolidBrush(matrix[i, j]),
                            new RectangleF(currentTop, currentLeft, ZoomFactor, ZoomFactor));
                    }
                }

                DrawBorder(pe.Graphics);
            }
            else
            {
                DrawBorder(pe.Graphics);
                pe.Graphics.DrawString("Map does not formed", new Font("Verdana", 10), new SolidBrush(Color.Black),
                    new PointF(Width / 2 - 70, Height / 2));
            }
        }

        public void LightUpThePixel(int i, int j)
        {
            var g = CreateGraphics();
            // Delete previous mark
            if (previousColor == Color.YellowGreen)
            {
                previousPoint = new Point(i, j);
                previousColor = matrix[i, j];
            }

            var top = previousPoint.X * ZoomFactor;
            var left = previousPoint.Y * ZoomFactor;
            g.DrawRectangle(new Pen(new SolidBrush(Color.WhiteSmoke), 2),
                new Rectangle(top, left, ZoomFactor, ZoomFactor));
            g.FillRectangle(new SolidBrush(previousColor), new RectangleF(top, left, ZoomFactor, ZoomFactor));
            DrawBorder(g);
            // Place new mark
            left = j * ZoomFactor;
            top = i * ZoomFactor;
            var rect = new Rectangle(top + ZoomFactor / 2 - ZoomFactor / 8,
                left + ZoomFactor / 2 - ZoomFactor / 8, ZoomFactor / 4, ZoomFactor / 4);
            g.DrawEllipse(new Pen(new SolidBrush(Color.Gray), 2), rect);
            g.FillEllipse(new SolidBrush(Color.Orange), rect);
            previousPoint.X = i;
            previousPoint.Y = j;
            previousColor = matrix[i, j];
        }
    }
}