using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    public class MatrixMultiplication
    {
        private readonly int[,] _matrix1;
        private readonly int[,] _matrix2;

        public MatrixMultiplication(int[,] matrix1, int[,] matrix2)
        {
            _matrix1 = matrix1;
            _matrix2 = matrix2;
        }

        public int[,] Multiplication()
        {
            if (_matrix1.GetLength(1) != _matrix2.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            int[,] result = new int[_matrix1.GetLength(0), _matrix2.GetLength(1)];

            Parallel.For(0, _matrix1.GetLength(0), i =>
            {
                for (int j = 0; j < _matrix2.GetLength(1); j++)
                {
                    for (int k = 0; k < _matrix2.GetLength(0); k++)
                    {
                        result[i, j] += _matrix1[i, k] * _matrix2[k, j];
                    }
                }
            });

            return result;
        }
    }

}
