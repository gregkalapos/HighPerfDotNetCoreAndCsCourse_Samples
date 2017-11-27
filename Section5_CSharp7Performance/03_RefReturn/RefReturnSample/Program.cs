using System;
using System.Diagnostics;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace RefReturnSample
{
    /// <summary>
    /// This sample app from Section 5.3 compares the performance of ref return and normal return with a relatively big struct.
    /// </summary>
    [MemoryDiagnoser]
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }
                 
        [Benchmark]
        public void TestWithRef()
        {
            Random rnd = new Random();
            Matrix m1 = Matrix.CreateRandomMatrix();
            Matrix m2 = Matrix.CreateRandomMatrix();
            Matrix m = m1;
            for (int i = 0; i < 1000000; i++)
            {
               ref Matrix mmm = ref GetMatrixRef(rnd.Next(2), ref m1, ref m2);
            }

            Debug.WriteLine(m.ToString());
        }

        [Benchmark]
        public void TestWithoutRef()
        {
            Random rnd = new Random();
            Matrix m1 = Matrix.CreateRandomMatrix();
            Matrix m2 = Matrix.CreateRandomMatrix();
            Matrix m = m1;
            for (int i = 0; i < 1000000; i++)
            {
                m = GetMatrix(rnd.Next(2), ref m1, ref m2);
            }

            Debug.WriteLine(m.ToString());
        }

        public static ref Matrix GetMatrixRef(int a, ref Matrix left, ref Matrix right)
        {
            if (a > 0)
                return ref left;
            else
                return ref right;
        }

        public static Matrix GetMatrix(int a, ref Matrix left, ref Matrix right)
        {
            if (a > 0)
                return left;
            else
                return right;
        }
    }

    /// <summary>
    /// A struct which is a relatively big Value Type
    /// </summary>
    public struct Matrix
    {
        int item00, item01, item02;
        int item10, item11, item12;
        int item20, item21, item22;

        public static Matrix CreateRandomMatrix()
        {
            Random rnd = new Random();
            var retVal = new Matrix();
            retVal.item00 = rnd.Next();
            retVal.item01 = rnd.Next();
            retVal.item02 = rnd.Next();

            retVal.item10 = rnd.Next();
            retVal.item11 = rnd.Next();
            retVal.item12 = rnd.Next();

            retVal.item10 = rnd.Next();
            retVal.item11 = rnd.Next();
            retVal.item12 = rnd.Next();

            return retVal;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{item00} {item01} {item02}");
            sb.Append($"{item10} {item11} {item12}");
            sb.Append($"{item20} {item21} {item22}");

            return sb.ToString();
        }

    }
}
