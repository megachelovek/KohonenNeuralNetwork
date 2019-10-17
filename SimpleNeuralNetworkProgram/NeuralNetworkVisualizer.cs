using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleNeuralNetworkProgram
{
    public partial class NeuralNetworkVisualizer :Control
    {
        private Color[,] matrix;
        private int zoomFactor = 1;
        private Point previousPoint;
        private Color previousColor;

        private void DrawBorder(Graphics g)
        {
            g.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1),
                new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        public int ZoomFactor
        {
            get { return zoomFactor; }
            set { zoomFactor = value; }
        }

        public Color[,] Matrix
        {
            get { return matrix; }
            set
            {
                matrix = value;
                previousColor = Color.YellowGreen;
            }
        }

        public NeuralNetworkVisualizer()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        public NeuralNetworkVisualizer(Color[,] matrix)
        {
            this.matrix = matrix;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (matrix != null)
            {
                this.Width = (int) Math.Sqrt(matrix.Length) * zoomFactor;
                this.Height = (int) Math.Sqrt(matrix.Length) * zoomFactor;
                int currentTop = 0;
                int currentLeft = 0;
                for (int i = 0; i < (int) Math.Sqrt(matrix.Length); i++)
                {
                    currentTop = i * zoomFactor;
                    for (int j = 0; j < (int) Math.Sqrt(matrix.Length); j++)
                    {
                        currentLeft = j * zoomFactor;
                        pe.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.WhiteSmoke), 2),
                            new Rectangle(currentTop, currentLeft, zoomFactor, zoomFactor));
                        pe.Graphics.FillRectangle(new SolidBrush(matrix[i, j]),
                            new RectangleF(currentTop, currentLeft, zoomFactor, zoomFactor));
                    }
                }

                DrawBorder(pe.Graphics);
            }
            else
            {
                DrawBorder(pe.Graphics);
                pe.Graphics.DrawString("Map does not formed", new Font("Verdana", 10), new SolidBrush(Color.Black),
                    new PointF(this.Width / 2 - 70, this.Height / 2));
            }
        }

        public void LightUpThePixel(int i, int j)
        {
            Graphics g = this.CreateGraphics();
            // Delete previous mark
            if (previousColor == Color.YellowGreen)
            {
                previousPoint = new Point(i, j);
                previousColor = matrix[i, j];
            }

            int top = (previousPoint.X * zoomFactor);
            int left = (previousPoint.Y * zoomFactor);
            g.DrawRectangle(new Pen(new SolidBrush(Color.WhiteSmoke), 2),
                new Rectangle(top, left, zoomFactor, zoomFactor));
            g.FillRectangle(new SolidBrush(previousColor), new RectangleF(top, left, zoomFactor, zoomFactor));
            DrawBorder(g);
            // Place new mark
            left = (j * zoomFactor);
            top = (i * zoomFactor);
            Rectangle rect = new Rectangle(top + (int) zoomFactor / 2 - (int) zoomFactor / 8,
                left + (int) zoomFactor / 2 - (int) zoomFactor / 8, (int) zoomFactor / 4, (int) zoomFactor / 4);
            g.DrawEllipse(new Pen(new SolidBrush(Color.Gray), 2), rect);
            g.FillEllipse(new SolidBrush(Color.Orange), rect);
            previousPoint.X = i;
            previousPoint.Y = j;
            previousColor = matrix[i, j];
        }

    }


}
