using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GeekBrainsLib;


namespace HomeWork6
{


    class Program
    {
        delegate double DoubleFunc(double a, double xf);

        static void Main(string[] args)
        {
            ConsoleUI consoleUI = new ConsoleUI("Мельников Александр", "Практическое задание 6", 3);
            bool isWork = true;
            while (isWork)
            {
                Console.Clear();
                consoleUI.PrintUserInfo();
                consoleUI.PrintMenu();
                if (int.TryParse(Console.ReadLine(), out int ndx))
                {
                    Console.Clear();
                    switch (ndx)
                    {
                        default:
                            break;
                        case 1:
                            {
                                Exercise1();
                                break;
                            }
                        case 2:
                            {
                                Exercise2();
                                break;
                            }
                        case 3:
                            {
                                Exercise3();
                                break;
                            }
                        case 0:
                            {
                                isWork = false;
                                break;
                            }
                    }
                }
                else
                {
                    Console.Clear();
                    ModifiedConsole.Print("Формат ввода неверен!");
                    ModifiedConsole.Pause();
                }
            }
        }

        static void Exercise1()
        {
            Console.WriteLine("Таблица функций:");
            Console.WriteLine("Таблица функции a*Sin(x):");
            Table(delegate (double a, double x) { return a * Math.Sin(x); }, 3, -2, 2);
            Console.WriteLine();
            Console.WriteLine("Таблица функции a*x^2:");
            Table(delegate (double a, double x) { return a * x * x; }, 2, 0, 3);
            Console.ReadKey();
        }

        static void Table(Function F, double x, double b)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x));
                x += 1;
            }
            Console.WriteLine("---------------------");
        }

        static void Table(DoubleFunc F, double a, double x, double b)
        {
            Console.WriteLine("------- X ------ a * Y ---");
            while (x <= b)
            {
                Console.WriteLine("|  {0,8:0.000}  | {1,8:0.000}  |", x, F(a, x));
                x += 1;
            }
            Console.WriteLine("--------------------------");
        }

        static void Exercise2()
        {
            Function[] functions = new Function[] { F, X2, X3, Math.Sqrt, Math.Sin, Math.Cos, Math.Tan };
            bool isWork = true;
            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("Нахождение минимума функции");
                Console.WriteLine("1) -> y = x^2 - 50x + 10");
                Console.WriteLine("2) -> y = x^2");
                Console.WriteLine("3) -> y = x^3");
                Console.WriteLine("4) -> y = sqrt(x)");
                Console.WriteLine("5) -> y = sin(x)");
                Console.WriteLine("6) -> y = cos(x)");
                Console.WriteLine("7) -> y = tan(x)");
                Console.WriteLine("0) -> Назад");
                Console.Write("Введите номер функции: ");
                if (int.TryParse(Console.ReadLine(), out int ndx))
                {
                    Console.Clear();
                    switch (ndx)
                    {
                        default:
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            {
                                Console.WriteLine("Введите отрезок на котором нужно найти минимум");
                                Console.Write("От -> ");
                                double l = double.Parse(Console.ReadLine());
                                Console.Write("До -> ");
                                double r = double.Parse(Console.ReadLine());
                                MinFunction.SaveFunc("data.bin", functions[ndx - 1], l, r, 0.5);
                                Console.Clear();
                                break;
                            }
                        case 0:
                            {
                                isWork = false;
                                break;
                            }
                    }
                    var points = MinFunction.Load("data.bin", out double min);
                    foreach (var point in points)
                    {
                        Console.WriteLine(point);
                    }
                    Console.WriteLine($"Минимум функции = {min}");
                    Console.WriteLine();
                    Console.WriteLine("Для продолжения нажмите любую клавишу...");
                    Console.ReadKey();

                }
                else
                {
                    Console.Clear();
                    ModifiedConsole.Print("Формат ввода неверен!");
                    ModifiedConsole.Pause();
                }
            }
        }

        /// <summary>
        /// Функция x^2 - 50x + 10
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double F(double x) => x * x - 50 * x + 10;

        /// <summary>
        /// Квадратичная функция
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double X2(double x) => x * x;

        /// <summary>
        /// Кубическая функция
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double X3(double x) => x * x * x;


        static int AgeCompare(Student st1, Student st2) => st1.age.CompareTo(st2.age);

        static int CourseAndAgeComapare(Student st1, Student st2) 
        {
            int rest = st1.course.CompareTo(st2.course);
            if(rest == 0) rest = st1.age.CompareTo(st2.age);
            return rest;
        }

        static void Exercise3()
        {
            List<Student> list = new List<Student>();
            // Создаем список студентов
            DateTime dt = DateTime.Now;
            StreamReader sr = new StreamReader("students.csv");
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(';');
                    // Добавляем в список новый экземпляр класса Student
                    list.Add(new Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[5]), int.Parse(s[6]), int.Parse(s[7]), s[8]));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Ошибка!ESC - прекратить выполнение программы");
                    if (Console.ReadKey().Key == ConsoleKey.Escape) return;
                }
            }
            sr.Close();
            Console.WriteLine("Всего студентов:" + list.Count);
            Console.WriteLine($"Число студентов учащихся на 5 и 6 курсах: {list.FindAll(s => s.course > 4 && s.course <= 6).Count}");
            Console.WriteLine();
            Console.WriteLine("Количетсво студентов от 18 до 20 лет обучающихся на следующих курсах:");
            for(int i =0; i < list.Max(s=>s.course); i++) 
            {
                Console.WriteLine($"На {i+1} курсе -> {list.FindAll(s => s.age >= 18 && s.age <= 20 && s.course == i + 1).Count}");
            }
            Console.WriteLine();
            Console.WriteLine("Список студентов отсортированых по возрасту");
            list.Sort(AgeCompare);
            //list = list.OrderBy(s => s.age).ToList();
            foreach (var v in list) Console.WriteLine(v.firstName);
            Console.WriteLine();
            Console.WriteLine("Список студентов отсортированых по курсу и возрасту");
            list.Sort(CourseAndAgeComapare);
            // list = list.OrderBy(s => s.course).ThenBy(s=>s.age).ToList();
            foreach (var v in list) Console.WriteLine($"{v.firstName} ; {v.course} ; {v.age}");

            Console.WriteLine(DateTime.Now - dt);
            Console.ReadKey();
        }
    }
}

