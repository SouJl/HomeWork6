using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6
{
    /// <summary>
    /// Класс Студента
    /// </summary>
    class Student
    {
        public string lastName;
        public string firstName;
        public string university;
        public string faculty;
        public int course;
        public string department;
        public int group;
        public string city;
        public int age;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="university">Университет</param>
        /// <param name="faculty">Факультет</param>
        /// <param name="department">Направление</param>
        /// <param name="age">Возраст</param>
        /// <param name="course">Курс</param>
        /// <param name="group">Группа</param>
        /// <param name="city">Город</param>
        public Student(string firstName, string lastName, string university, string faculty, string department, int age, int course, int group, string city)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.university = university;
            this.faculty = faculty;
            this.department = department;
            this.age = age;
            this.course = course;
            this.group = group;
            this.city = city;
        }
    }

}
