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
        /// <summary>
        /// Делегат функции с двумя параметрами
        /// </summary>
        /// <param name="a"></param>
        /// <param name="xf"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Задание 1 -  Доработка программы вывода таблицы функции так, 
        /// чтобы можно было передавать функции типа double (double, double)
        /// </summary>
        static void Exercise1()
        {
            Console.WriteLine("Таблица функции a*Sin(x):");
            Table(delegate (double a, double x) { return a * Math.Sin(x); }, 3, -2, 2);

            Console.WriteLine();
            Console.WriteLine("Таблица функции a*x^2:");
            Table(delegate (double a, double x) { return a * x * x; }, 2, 0, 3);

            Console.WriteLine();
            Console.Write("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }

        /// <summary>
        /// Метод вывода таблицы на экран
        /// </summary>
        /// <param name="F">функция</param>
        /// <param name="a">коэффициент</param>
        /// <param name="x"></param>
        /// <param name="b"></param>
        static void Table(DoubleFunc F, double a, double x, double b)
        {
            Console.WriteLine("|--- X ---|--- a * Y ---|");
            while (x <= b)
            {
                Console.WriteLine("|{0,9:0.000}|{1,13:0.000}|", x, F(a, x));
                x += 1;
            }
            Console.WriteLine("|---------|-------------|");
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

        /// <summary>
        /// Задание 2 - Доработка программы нахождения минимума функции
        /// </summary>
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

                                var points = MinFunction.Load("data.bin", out double min);
                                foreach (var point in points)
                                {
                                    Console.WriteLine(point);
                                }
                                Console.WriteLine($"Минимум функции = {min}");
                                break;
                            }
                        case 0:
                            {
                                isWork = false;
                                break;
                            }
                    }
                    Console.WriteLine();
                    Console.Write("Для продолжения нажмите любую клавишу...");
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
        /// метод сравнения по возрасту
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <returns></returns>
        static int AgeCompare(Student st1, Student st2) => st1.age.CompareTo(st2.age);
        
        /// <summary>
        /// метод сравнения по курсу и возрасту
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <returns></returns>
        static int CourseAndAgeComapare(Student st1, Student st2)
        {
            return st1.course.CompareTo(st2.course) == 0 ? st1.age.CompareTo(st2.age) : st1.course.CompareTo(st2.course);
        }

        /// <summary>
        ///  Задание 3 - Доработка программы считывания информации 
        ///  о студентах из файла students.csv
        /// </summary>
        static void Exercise3()
        {
            List<Student> studentsList = new List<Student>();
            StreamReader sr = new StreamReader("students.csv");
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(';');
                    studentsList.Add(new Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[5]), int.Parse(s[6]), int.Parse(s[7]), s[8]));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Ошибка!ESC - выйти в главное меню");
                    if (Console.ReadKey().Key == ConsoleKey.Escape) return;
                }
            }
            sr.Close();
            bool isWork = true;
            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("----------------------");
                Console.WriteLine("Информация о студентах");
                Console.WriteLine("----------------------");
                Console.WriteLine("Всего студентов:" + studentsList.Count);
                Console.WriteLine("1) -> Число студентов учащихся на 5 и 6 курсах");
                Console.WriteLine("2) -> Количетсво студентов от 18 до 20 лет обучающихся на следующих курсах");
                Console.WriteLine("3) -> Список студентов отсортированых по возрасту");
                Console.WriteLine("4) -> Список студентов отсортированых по курсу и возрасту");
                Console.WriteLine("0) -> Назад");
                Console.Write("Введите номер меню: ");
                if (int.TryParse(Console.ReadLine(), out int ndx))
                {
                    Console.Clear();
                    switch (ndx)
                    {
                        default:
                            break;
                        case 1:
                            {
                                Console.WriteLine($"Число студентов учащихся на 5 и 6 курсах: {studentsList.FindAll(s => s.course > 4 && s.course <= 6).Count}");
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Количетсво студентов от 18 до 20 лет обучающихся на следующих курсах:");
                                for (int i = 0; i < studentsList.Max(s => s.course); i++)
                                {
                                    Console.WriteLine($"На {i + 1} курсе -> {studentsList.FindAll(s => s.age >= 18 && s.age <= 20 && s.course == i + 1).Count}");
                                }
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Список студентов отсортированых по возрасту");
                                studentsList.Sort(AgeCompare);
                                Console.WriteLine("|----Имя----|--Возраст--|");
                                foreach (var st in studentsList)
                                    Console.WriteLine("|{0,11}|{1,11}|", st.firstName, st.age);
                                Console.WriteLine("|-----------------------|");
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("Список студентов отсортированых по курсу и возрасту");
                                Console.WriteLine();
                                studentsList.Sort(CourseAndAgeComapare);
                                Console.WriteLine("|----Имя----|--Курс--|--Возраст--|");
                                foreach (var st in studentsList)
                                    Console.WriteLine("|{0,11}|{1,8}|{2,11}|", st.firstName, st.course, st.age);
                                Console.WriteLine("|--------------------------------|");
                                break;
                            }
                        case 0:
                            {
                                isWork = false;
                                break;
                            }
                    }
                    Console.WriteLine();
                    Console.Write("Для продолжения нажмите любую клавишу...");
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
    }
}

