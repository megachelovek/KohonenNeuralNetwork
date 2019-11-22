using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    // Decompiled with JetBrains decompiler
    // Type: CommonLib.CommonLib.Matrix1D
    // Assembly: CommonLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
    // MVID: 235DFFEE-F7B0-4B61-A5F2-0A3610A2789C
    // Assembly location: C:\Users\2regv\source\repos\CommonLib\CommonLib\bin\Debug\CommonLib.dll

    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace CommonLib.CommonLib
    {
        /// <summary>
        /// Wrap methods and properities of 1D matrix object
        /// Store its vales as single dimensional array
        /// </summary>
        public class Matrix1D 
        {
            /// <summary>Array to hold matrix values - 1D array</summary>
            private float[] _Values;
            /// <summary>
            /// Size or capacity of matrix - Simply number of stored elements in Values arrary
            /// </summary>
            private int _Size;
            private Random _Rnd;

            /// <summary>Constructor - Redim Array to only 1 element</summary>
            public Matrix1D()
            {
                this._Rnd = new Random();
                this._Values = new float[2];
            }

            /// <summary>Constructor that receives values array</summary>
            /// <param name="_InitialValues">Initial Elements array</param>
            public Matrix1D(float[] _InitialValues)
            {
                this._Rnd = new Random();
                this._Values = new float[checked(((IEnumerable<float>)_InitialValues).Count<float>() - 1 + 1)];
                this.Values = _InitialValues;
                this._Size = ((IEnumerable<float>)this._Values).Count<float>();
            }

            /// <summary>
            /// Constructor that receives values integer array
            /// All elements are converted to Single type
            /// </summary>
            /// <param name="_InitialValues">Initial Elements array</param>
            public Matrix1D(int[] _InitialValues)
            {
                this._Rnd = new Random();
                this._Values = new float[checked(((IEnumerable<int>)_InitialValues).Count<int>() - 1 + 1)];
                int num = checked(((IEnumerable<float>)this._Values).Count<float>() - 1);
                int index = 0;
                while (index <= num)
                {
                    this.Values[index] = (float)_InitialValues[index];
                    checked { ++index; }
                }
                this._Size = ((IEnumerable<float>)this._Values).Count<float>();
            }

            /// <summary>
            /// Constructor, receives size of matrix
            /// All elements will be randomized for values between 1 and -1
            /// </summary>
            /// <param name="_ReqCapacity">Matrix size</param>
            public Matrix1D(int _ReqCapacity)
            {
                this._Rnd = new Random();
                this._Values = new float[checked(_ReqCapacity - 1 + 1)];
                this.RandomizeValues(-1.0, 1.0);
                this._Size = ((IEnumerable<float>)this._Values).Count<float>();
            }

            /// <summary>
            /// Size or capacity of matrix - Simply number of stored elements in Values arrary
            /// </summary>
            /// <returns></returns>
            public int Size
            {
                get
                {
                    return ((IEnumerable<float>)this._Values).Count<float>();
                }
            }

            /// <summary>Array to hold matrix values - 1D array</summary>
            /// <returns></returns>
            public float[] Values
            {
                get
                {
                    return this._Values;
                }
                set
                {
                    this._Values = new float[checked(((IEnumerable<float>)value).Count<float>() - 1 + 1)];
                    this._Values = value;
                }
            }

            /// <summary>Return min value within matrix elements</summary>
            /// <returns></returns>
            public float GetMin
            {
                get
                {
                    if (this._Values == null)
                        throw new Exception("Matrix Values has not been set");
                    float num1 = 0.0f;
                    int num2 = checked(this.Size - 1);
                    int Index = 0;
                    while (Index <= num2)
                    {
                        if (Index == 0)
                            num1 = this.GetValue(Index);
                        if ((double)this.GetValue(Index) < (double)num1)
                            num1 = this.GetValue(Index);
                        checked { ++Index; }
                    }
                    return num1;
                }
            }

            /// <summary>Return max value within matrix elements</summary>
            /// <returns></returns>
            public float GetMax
            {
                get
                {
                    if (this._Values == null)
                        throw new Exception("Matrix Values has not been set");
                    float num1 = 0.0f;
                    int num2 = checked(this.Size - 1);
                    int Index = 0;
                    while (Index <= num2)
                    {
                        if (Index == 0)
                            num1 = this.GetValue(Index);
                        if ((double)this.GetValue(Index) > (double)num1)
                            num1 = this.GetValue(Index);
                        checked { ++Index; }
                    }
                    return num1;
                }
            }

            /// <summary>Implement Matrix Product method between 2 metrices m1 and m2</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="m2">2nd matrix</param>
            /// <returns>New matrix of same size of min size between m1 and m2</returns>
            public Matrix1D Product(Matrix1D m1, Matrix1D m2)
            {
                Matrix1D matrix1D = new Matrix1D(Math.Min(m1.Size, m2.Size));
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] * m2.Values[index];
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix Product method between current object and m2</summary>
            /// <param name="m2">2nd matrix</param>
            /// <returns>New matrix of same size of min size between m1 and m2</returns>
            public Matrix1D Product(Matrix1D m2)
            {
                return this.Product(this, m2);
            }

            /// <summary>Implement Matrix Product method between matrix m1 and scalar value</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar product of matrix elements</returns>
            public Matrix1D Product(Matrix1D m1, int Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] * (float)Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix Product method between matrix m1 and scalar value</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar product of matrix elements</returns>
            public Matrix1D Product(Matrix1D m1, float Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] * Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix Product method between matrix m1 and scalar value</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar product of matrix elements</returns>
            public Matrix1D Product(Matrix1D m1, double Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] * (float)Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix Product method between matrix m1 and scalar value</summary>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar product of matrix elements</returns>
            public Matrix1D Product(int Scalar)
            {
                return this.Product(this, Scalar);
            }

            /// <summary>Implement Matrix Product method between matrix m1 and scalar value</summary>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar product of matrix elements</returns>
            public Matrix1D Product(float Scalar)
            {
                return this.Product(this, Scalar);
            }

            /// <summary>Implement Matrix Product method between matrix m1 and scalar value</summary>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar product of matrix elements</returns>
            public Matrix1D Product(double Scalar)
            {
                return this.Product(this, Scalar);
            }

            /// <summary>Implement Matrix addition method between 2 metrices m1 and m2</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="m2">2nd matrix</param>
            /// <returns>New matrix of same size of min size between m1 and m2</returns>
            public Matrix1D Add(Matrix1D m1, Matrix1D m2)
            {
                Matrix1D matrix1D = new Matrix1D(Math.Min(m1.Size, m2.Size));
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] + m2.Values[index];
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix addition method between current object and m2</summary>
            /// <param name="m2">2nd matrix</param>
            /// <returns>New matrix of same size of min size between m1 and m2</returns>
            public Matrix1D Add(Matrix1D m2)
            {
                return this.Add(this, m2);
            }

            /// <summary>Implement Matrix addition method between matrix m1 and scalar value</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar addition of matrix elements</returns>
            public Matrix1D Add(Matrix1D m1, int Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] + (float)Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix addition method between matrix m1 and scalar value</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar addition of matrix elements</returns>
            public Matrix1D Add(Matrix1D m1, float Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] + Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix addition method between matrix m1 and scalar value</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar addition of matrix elements</returns>
            public Matrix1D Add(Matrix1D m1, double Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] + (float)Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix addition method between matrix m1 and scalar value</summary>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar addition of matrix elements</returns>
            public Matrix1D Add(int Scalar)
            {
                return this.Add(this, Scalar);
            }

            /// <summary>Implement Matrix addition method between matrix m1 and scalar value</summary>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar addition of matrix elements</returns>
            public Matrix1D Add(float Scalar)
            {
                return this.Add(this, Scalar);
            }

            /// <summary>Implement Matrix addition method between matrix m1 and scalar value</summary>
            /// <param name="Scalar">scalar (single) value</param>
            /// <returns>Scalar product of matrix elements</returns>
            public Matrix1D Add(double Scalar)
            {
                return this.Add(this, Scalar);
            }

            /// <summary>Implement Matrix subtraction method between 2 metrices m1 and m2</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="m2">2nd matrix</param>
            /// <returns>New matrix of same size of min size between m1 and m2</returns>
            public Matrix1D Sub(Matrix1D m1, Matrix1D m2)
            {
                Matrix1D matrix1D = new Matrix1D(Math.Min(m1.Size, m2.Size));
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] - m2.Values[index];
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix subtraction method between object and m2</summary>
            /// <param name="m2">2nd matrix</param>
            /// <returns>New matrix of same size of min size between m1 and m2</returns>
            public Matrix1D Sub(Matrix1D m2)
            {
                return this.Sub(this, m2);
            }

            /// <summary>Implement Matrix subtraction method between matrix and scalar</summary>
            /// <param name="m1">matrix</param>
            /// <param name="Scalar">scalar value</param>
            /// <returns>Scalar subtract of matrix elements</returns>
            public Matrix1D Sub(Matrix1D m1, int Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] - (float)Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix subtraction method between matrix and scalar</summary>
            /// <param name="m1">matrix</param>
            /// <param name="Scalar">scalar value</param>
            /// <returns>Scalar subtract of matrix elements</returns>
            public Matrix1D Sub(Matrix1D m1, float Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] - Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix subtraction method between matrix and scalar</summary>
            /// <param name="m1">matrix</param>
            /// <param name="Scalar">scalar value</param>
            /// <returns>Scalar subtract of matrix elements</returns>
            public Matrix1D Sub(Matrix1D m1, double Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] - (float)Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix subtraction method between matrix and scalar</summary>
            /// <param name="Scalar">scalar value</param>
            /// <returns>Scalar subtract of matrix elements</returns>
            public Matrix1D Sub(int Scalar)
            {
                return this.Sub(this, Scalar);
            }

            /// <summary>Implement Matrix subtraction method between matrix and scalar</summary>
            /// <param name="Scalar">scalar value</param>
            /// <returns>Scalar subtract of matrix elements</returns>
            public Matrix1D Sub(float Scalar)
            {
                return this.Sub(this, Scalar);
            }

            /// <summary>Implement Matrix subtraction method between matrix and scalar</summary>
            /// <param name="Scalar">scalar value</param>
            /// <returns>Scalar subtract of matrix elements</returns>
            public Matrix1D Sub(double Scalar)
            {
                return this.Sub(this, Scalar);
            }

            /// <summary>Sum of all matrix elements</summary>
            /// <returns>sum of all matrix elements</returns>
            public float Sum()
            {
                float num1 = 0.0f;
                int num2 = checked(this.Size - 1);
                int Index = 0;
                while (Index <= num2)
                {
                    num1 += this.GetValue(Index);
                    checked { ++Index; }
                }
                return num1;
            }

            /// <summary>Implement Matrix divide method between 2 metrices m1 and m2</summary>
            /// <param name="m1">1st matrix</param>
            /// <param name="m2">2nd matrix</param>
            /// <returns>New matrix of same size of min size between m1 and m2</returns>
            public Matrix1D Divide(Matrix1D m1, Matrix1D m2)
            {
                Matrix1D matrix1D = new Matrix1D(Math.Min(m1.Size, m2.Size));
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] / m2.Values[index];
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix divide method between object and m2</summary>
            /// <param name="m2">2nd matrix</param>
            /// <returns>New matrix of same size of min size between m1 and m2</returns>
            public Matrix1D Divide(Matrix1D m2)
            {
                return this.Divide(this, m2);
            }

            /// <summary>Implement Matrix divide method between matrix and scalar value</summary>
            /// <param name="m1">matrix</param>
            /// <param name="Scalar">scalar value</param>
            /// <returns>new matrix same size as m1</returns>
            public Matrix1D Divide(Matrix1D m1, int Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] / (float)Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix divide method between matrix and scalar value</summary>
            /// <param name="m1">matrix</param>
            /// <param name="Scalar">scalar value</param>
            /// <returns>new matrix same size as m1</returns>
            public Matrix1D Divide(Matrix1D m1, float Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] / Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix divide method between matrix and scalar value</summary>
            /// <param name="m1">matrix</param>
            /// <param name="Scalar">scalar value</param>
            /// <returns>new matrix same size as m1</returns>
            public Matrix1D Divide(Matrix1D m1, double Scalar)
            {
                Matrix1D matrix1D = new Matrix1D(m1.Size);
                int num = checked(matrix1D.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    matrix1D.Values[index] = m1.Values[index] / (float)Scalar;
                    checked { ++index; }
                }
                return matrix1D;
            }

            /// <summary>Implement Matrix divide method between matrix and scalar value</summary>
            /// <param name="Scalar">scalar value</param>
            /// <returns>new matrix same size as m1</returns>
            public Matrix1D Divide(int Scalar)
            {
                return this.Divide(this, Scalar);
            }

            /// <summary>Implement Matrix divide method between matrix and scalar value</summary>
            /// <param name="Scalar">scalar value</param>
            /// <returns>new matrix same size as m1</returns>
            public Matrix1D Divide(float Scalar)
            {
                return this.Divide(this, Scalar);
            }

            /// <summary>Implement Matrix divide method between matrix and scalar value</summary>
            /// <param name="Scalar">scalar value</param>
            /// <returns>new matrix same size as m1</returns>
            public Matrix1D Divide(double Scalar)
            {
                return this.Divide(this, Scalar);
            }

            /// <summary>Overrides ToString function of base object</summary>
            /// <returns>String represents matric contents [v1,v2,v3,....,vn]</returns>
            public override string ToString()
            {
                string str = "Values[";
                int num = checked(this.Size - 1);
                int index = 0;
                while (index <= num)
                {
                    str += this.Values[index].ToString();
                    if (index != checked(this.Size - 1))
                        str += ",";
                    checked { ++index; }
                }
                return str + "]";
            }

            /// <summary>Randomize matrix elements between min and max values</summary>
            /// <param name="min"></param>
            /// <param name="max"></param>
            public void RandomizeValues(double min, double max)
            {
                int num = checked(((IEnumerable<float>)this._Values).Count<float>() - 1);
                int index = 0;
                while (index <= num)
                {
                    this._Values[index] = (float) ((float)this._Rnd.NextDouble()-min);
                    checked { ++index; }
                }
            }

            /// <summary>Copy contents of m1 into object starting from starting index</summary>
            /// <param name="m1">matrix to be copied</param>
            /// <param name="StartingIndex">starting index of copy</param>
            /// <returns>new instance of object</returns>
            public Matrix1D Copy(Matrix1D m1, int StartingIndex)
            {
                int num1 = StartingIndex;
                int num2 = checked(((IEnumerable<float>)this._Values).Count<float>() - 1);
                int index = num1;
                while (index <= num2 && checked(index - StartingIndex) <= checked(((IEnumerable<float>)m1.Values).Count<float>() - 1))
                {
                    this._Values[index] = m1.Values[checked(index - StartingIndex)];
                    checked { ++index; }
                }
                return this;
            }

            /// <summary>Copy contents of m1 into object starting from starting index = 0</summary>
            /// <param name="m1">matrix to be copied</param>
            /// <returns>new instance of object</returns>
            public Matrix1D Copy(Matrix1D m1)
            {
                return this.Copy(m1, 0);
            }

            /// <summary>Copy contents of object into itself starting from starting index = 0</summary>
            /// <returns>new instance of object</returns>
            public Matrix1D Copy(int StartingIndex)
            {
                return this.Copy(this, StartingIndex);
            }

            /// <summary>Forces all elements of matrix to ForcedValue</summary>
            /// <param name="ForcedValue"></param>
            public void ForceValues(float ForcedValue)
            {
                int num = checked(((IEnumerable<float>)this._Values).Count<float>() - 1);
                int index = 0;
                while (index <= num)
                {
                    this._Values[index] = ForcedValue;
                    checked { ++index; }
                }
            }

            /// <summary>
            /// Return index element of matrix
            /// Index starts with 0
            /// </summary>
            /// <param name="Index">0 indexed element</param>
            /// <returns>Element value at index of Index</returns>
            public float GetValue(int Index)
            {
                if (Index > ((IEnumerable<float>)this.Values).Count<float>())
                    return 0.0f;
                return this._Values[Index];
            }

            /// <summary>
            /// Set value of matrix element at position Index
            /// 0 indexed positions
            /// </summary>
            /// <param name="Index">Index (Poistion) starting from 0</param>
            /// <param name="Value">New value</param>
            public void SetValue(int Index, float Value)
            {
                if (Index > ((IEnumerable<float>)this.Values).Count<float>())
                    return;
                this._Values[Index] = Value;
            }
        }
    }

}
