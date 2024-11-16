
using System;
using System.Collections.Generic;

namespace Task
{
    //Определение абстрактного класса Person 
    //Определяет базовые свойства и методы, общие для всех персон учебного центра (например, Фамилия, Дата рождения, методполучения возраста).
    //Метод ToString позволяет, как базовый, выводить информацию о человеке.
    public abstract class Person 
    {
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }

        protected Person (string lastName, DateTime birthDate)
        {
            LastName = lastName;
            BirthDate = birthDate;
        }

        public int GetAge()
        {
            return DateTime.Now.Year - BirthDate.Year - (DateTime.Now.DayOfYear < BirthDate.DayOfYear ? 1 : 0);
        }

        public override string ToString()
        {
            return $"{LastName}, Возраст: {GetAge()}";
        }
    }

    //Определение интерфейса IEmployee
    //Определяет методы, которые должны реализовывать классы сотрудников. Например, расчет заработной платы и информация о должности.
    public interface IEmployee 
    {
        decimal CalcuLateSalary();
        string GetPositionInfo();
    }

    //Классы Administrator, Student, Teacher, Manager: все они наследуют Person.
    //Каждый класс реализует свои специфические свойства и методы.
    //Например, у преподавателя есть опыт работы, который влияет на заработную плату.
    public class Administrator : Person, IEmployee //Реализация класса Administrator
    {
        public string Laboratory { get; private set; }
        public Administrator(string lastName, DateTime birthDate, string Laboratory) : base(lastName, birthDate)
        {
            Laboratory = Laboratory;
        }
        public decimal CalcuLateSalary()
        {
            return 50000; //Задаем фиксированную зарплату для администратора
        }

        public string GetPositionInfo()
        {
            return "Администратор лаборатории: " + Laboratory;
        }

        public override string ToString()
        {
            return base.ToString() + $", Лаборатория: {Laboratory}, Зарплата: {CalcuLateSalary()}, Должность: {GetPositionInfo()}";
        }
    }

    public class Student : Person
    {
        public string Faculty { get; private set; }
        public string Group { get; private set; }

        public Student(string lastName, DateTime birthDate, string faculty, string group) : base (lastName, birthDate)
        {
            Faculty = faculty;
            Group = group;
        }

        public override string ToString()
        {
            return base.ToString() + $", Факультет: {Faculty}, Группа: {Group}";
        }
    }
    public class Teacher : Person, IEmployee
    {
        public string Faculty { get; private set; }
        public string Position { get; private set; }
        public int Experience { get; private set; } // В годах

        public Teacher(String lastName, DateTime birthDate, string faculty, string position, int experience) : base(lastName, birthDate)
        {
            Faculty = faculty;
            Position = position;
            Experience = experience;
        }
        
        public decimal CalcuLateSalary()
        {
            return 30000 + (Experience * 2000);  // Базовая зарплата + надбавка за стаж
        }

        public string GetPositionInfo()
        {
            return $"Преподователь, Должность: {Position}, Стаж: {Experience} лет";
        }

        public override string ToString()
        {
            return base.ToString() + $", Факультет: {Faculty}," + $"Зарплата: {CalcuLateSalary()}, {GetPositionInfo()}";
        }
    }
    public class Manager : Person, IEmployee
    {
        public string Faculty { get; private set; }
        public string Position { get; private set; }
        public Manager(string lastName, DateTime birthDate, string faculty, string position) : base(lastName, birthDate)
        {
            Faculty = faculty;
            Position = position;
        }
        
        public decimal CalcuLateSalary()
        {
            return 70000; // Задаем фиксированную зарплату для менеджера
        }

        public string GetPositionInfo()
        {
            return $"Менеджер, Должность: {Position}";
        }

        public override string ToString()
        {
            return base.ToString() + $", Факультет: {Faculty}," + $"Зарплата: {CalcuLateSalary()}, {GetPositionInfo()}";
        }
    }
    class Program
    {
        static void Main()
        {
            // Мы используем список (List) для хранения различных объектов, основанных на классе Person. Это гибкая коллекция, которая позволяет динамически добавлять и удалять элементы.
            List<Person> people = new List<Person> 
            {
                new Administrator("Иванов", new DateTime(1985, 5, 1), "Физика"),
                new Student("Петров", new DateTime(1999, 8, 20), "Математика", "Группа 101"),
                new Teacher("Сидоров", new DateTime(1975, 3, 10), "Химия", "Профессор", 10),
                new Manager("Алексеева", new DateTime(1990, 1, 15), "Бизнес", "Старший менеджер")
            };

            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
        }
    }
}