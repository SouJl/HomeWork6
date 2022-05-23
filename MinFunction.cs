using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6
{
    /// <summary>
    /// Делегат функции по одному параметру
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public delegate double Function(double x);
    
    /// <summary>
    /// Статический класс минимума фкнции
    /// </summary>
    static class MinFunction
    {
        /// <summary>
        /// Метод сохранения точек функции в файл
        /// </summary>
        /// <param name="fileName">путь</param>
        /// <param name="Func">функция</param>
        /// <param name="x">начальная точка</param>
        /// <param name="b">конечная точка</param>
        /// <param name="h">сдвиг</param>
        public static void SaveFunc(string fileName, Function Func, double x, double b, double h)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            while (x <= b)
            {
                bw.Write(Func(x));
                x += h;
            }
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// Метод загрузки и нахождения минимума функции
        /// </summary>
        /// <param name="fileName">путь</param>
        /// <returns>минимум функции</returns>
        public static double Load(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return min;
        }

        /// <summary>
        /// Метод загрузки и нахождения минимума функции
        /// </summary>
        /// <param name="fileName">путь</param>
        /// <param name="min">минимум функции</param>
        /// <returns>точки</returns>
        public static double[] Load(string fileName, out double min)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            min = double.MaxValue;
            List<double> d = new List<double>();
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                d.Add(bw.ReadDouble());
                if (d.Last() < min) min = d.Last();
            }
            bw.Close();
            fs.Close();
            return d.ToArray();
        }
    }
}
