using System.Collections.ObjectModel;

namespace MatrixLib
{
    public class Matrix
    {

        public double[,] x; 
        public int n;
        public int m;

        //operator
        // Dot
        public static Matrix operator *(Matrix lhs, Matrix rhs) => new Matrix(lhs.Dot(rhs));
        public static Matrix operator *(double lhs, Matrix rhs) => new Matrix(rhs.Dot(lhs));
        public static Matrix operator *(Matrix lhs, double rhs) => new Matrix(lhs.Dot(rhs));
        // Plus
        public static Matrix operator +(Matrix lhs, Matrix rhs) => new Matrix(lhs.Plus(rhs));
        public static Matrix operator +(double lhs, Matrix rhs) => new Matrix(rhs.Plus(lhs));
        public static Matrix operator +(Matrix lhs, double rhs) => new Matrix(lhs.Plus(rhs));
        // Minus
        public static Matrix operator -(Matrix lhs, Matrix rhs) => new Matrix(lhs.Minus(rhs));
        public static Matrix operator -(double lhs, Matrix rhs) => new Matrix(rhs.Minus(lhs));
        public static Matrix operator -(Matrix lhs, double rhs) => new Matrix(lhs.Minus(rhs));


        public Matrix()
        {
            this.x = Eye(3);
            this.n = 3;
            this.m = 3;
        }

        public Matrix(int n, int m) 
        {
            this.x = new double[n, m];
            this.n = n;
            this.m = m;
        }
        public Matrix(double[,] x) 
        {
            this.x = x;
            this.n = x.GetLength(0);
            this.m = x.GetLength(1);
        }
        public void SetMatrix(double[,] x) 
        {
            this.x = x; 
            this.n = x.GetLength(0);
            this.m = x.GetLength(1);
        }
        public double[,] Eye(int n)
        {
            double[,] mat = new double[n, n];
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    mat[j, i] = (i == j) ? 1 : 0;
                }
            }
            return mat;
        }

        public double[,] Plus(Matrix mat)
        {
            if (this.n != mat.n || this.m != mat.m)
            {
                Console.WriteLine("Matrix size error in Plus.");
                Console.WriteLine(String.Format("[{0}, {1}] * [{3}, {4}] is not calculable.", this.n, this.m, mat.n, mat.m));
            }

            double[,] y = mat.x;
            double[,] x2 = new double[this.n, this.m];

            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    x2[i, j] = this.x[i, j] + y[i, j];
                }
            }
            return x2;
        }
        public double[,] Plus(double x)
        {
            double[,] x2 = new double[this.n, this.m];

            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    x2[i, j] = this.x[i, j] + x;
                }
            }
            return x2;
        }
        public double[,] Minus(Matrix mat)
        {
            if (this.n != mat.n || this.m != mat.m)
            {
                Console.WriteLine("Matrix size error in Minus.");
                Console.WriteLine($"[{this.n}, {this.m}] * [{mat.n}, {mat.m}] is not calculable.");
            }
            double[,] y = mat.x;
            double[,] x2 = new double[this.n, this.m];

            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    x2[i, j] = this.x[i, j] - y[i, j];
                }
            }
            return x2;
        }
        public double[,] Minus(double x)
        {
            double[,] x2 = new double[this.n, this.m];

            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    x2[i, j] = this.x[i, j] - x;
                }
            }
            return x2;
        }

        public double[,] Dot(Matrix mat)
        {
            if (this.m != mat.n) 
            {
                Console.WriteLine("Matrix size error in Dot.");
                Console.WriteLine(String.Format("[{0}, {1}] * [{3}, {4}] is not calculable.", this.n, this.m, mat.n, mat.m));
            }
            double[,] y = mat.x;
            double[,] product = new double[this.x.GetLength(0), y.GetLength(1)];

            for (int i = 0; i < this.x.GetLength(0); i++)
            {
                for (int j = 0; j < y.GetLength(1); j++)
                {
                    for (int k = 0; k < this.x.GetLength(1); k++)
                    {
                        product[i, j] += this.x[i, k] * y[k, j];
                    }
                }
            }
            return product;
        }
        public double[,] Dot(double x)
        {
            double[,] product = new double[this.n, this.m];

            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    product[i, j] = this.x[i, j] * x;
                }
            }
            return product;
        }
        public Matrix Transpose()
        {
            double[,] xT = new double[this.m, this.n];

            for (int i = 0; i < this.x.GetLength(1); i++)
            {
                for (int j = 0; j < this.x.GetLength(0); j++)
                {
                    xT[i, j] = this.x[j, i];
                }
            }
            Matrix mat = new Matrix(xT);
            return mat;
        }

        public Matrix Inverse()
        {

            int n = this.n;
            int m = this.m;

            double[,] x = new double[this.n, this.m];
            Array.Copy(this.x, x, this.x.Length);

            double[,] invx = new double[n, m];

            if (n == m)
            {

                int max;
                double tmp;

                for (int j = 0; j < n; j++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        invx[j, i] = (i == j) ? 1 : 0;
                    }
                }

                for (int k = 0; k < n; k++)
                {
                    max = k;
                    for (int j = k + 1; j < n; j++)
                    {
                        if (Math.Abs(x[j, k]) > Math.Abs(x[max, k]))
                        {
                            max = j;
                        }
                    }

                    if (max != k)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            // 入力行列側
                            tmp = x[max, i];
                            x[max, i] = x[k, i];
                            x[k, i] = tmp;

                            // 単位行列側
                            tmp = invx[max, i];
                            invx[max, i] = invx[k, i];
                            invx[k, i] = tmp;
                        }
                    }

                    tmp = x[k, k];

                    for (int i = 0; i < n; i++)
                    {
                        x[k, i] /= tmp;
                        invx[k, i] /= tmp;
                    }

                    for (int j = 0; j < n; j++)
                    {
                        if (j != k)
                        {
                            tmp = x[j, k] / x[k, k];
                            for (int i = 0; i < n; i++)
                            {
                                x[j, i] = x[j, i] - x[k, i] * tmp;
                                invx[j, i] = invx[j, i] - invx[k, i] * tmp;
                            }
                        }
                    }

                }

                //逆行列が計算できなかった時の措置
                for (int j = 0; j < n; j++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (double.IsNaN(invx[j, i]))
                        {
                            Console.WriteLine("Error : Unable to compute inverse matrix");
                            invx[j, i] = 0;//ここでは，とりあえずゼロに置き換えることにする
                        }
                    }
                }
                Matrix mat = new Matrix(invx);
                return mat;
            }
            else
            {
                Console.WriteLine("Error : It is not a square matrix");
                Matrix mat = new Matrix(invx);
                return mat;
            }
        }

        public void Print()
        {
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    Console.Write(this.x[i, j]);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }
    }
}