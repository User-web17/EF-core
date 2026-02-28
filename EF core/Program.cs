using EF_core.Contexts;
using EF_core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Serialization;

namespace EF_core
{
    public class Program
    {
        static AppDbContext db = new AppDbContext();
        static void Main(string[] args)
        {
            try
            {
                Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("1. Create Student");
                Console.WriteLine("2. Create Teacher");
                Console.WriteLine("3. Show Students");
                Console.WriteLine("4. Show Teachers");
                Console.WriteLine("0. Exit");
                Console.Write("Enter: ");
                string choice = Console.ReadLine()!;

                if (string.IsNullOrEmpty(choice))
                {
                    throw new Exception();
                }

                switch (choice)
                {
                    case "1":
                        Student student = new Student()
                        {
                            FirstName = Console.ReadLine()!,
                            LastName = Console.ReadLine()!,
                            BirthDate = DateTime.Parse(Console.ReadLine()!),
                            Email = Console.ReadLine()!
                        };

                        db.Students.Add(student);
                        db.SaveChanges();
                        break;
                    case "2":
                        Teacher teacher = new Teacher()
                        {
                            FirstName = Console.ReadLine()!,
                            LastName = Console.ReadLine()!,
                            BirthDate = DateTime.Parse(Console.ReadLine()!),
                            Salary = Decimal.Parse(Console.ReadLine()!)
                        };

                        db.Teachers.Add(teacher);
                        db.SaveChanges();
                        break;
                    case "3":
                        var students = db.Students.ToList();

                        foreach (var student1 in students)
                        {
                            Console.WriteLine($"--- Student ---\n Id: {student1.Id}\n Name: {student1.FirstName}\n " +
                                $"Surname: {student1.LastName}\n Birth Date: {student1.BirthDate.ToString()}\n " +
                                $"Email: {student1.Email}\n");
                        }

                        break;
                    case "4":
                        var teachers = db.Teachers.ToList();

                        foreach (var teacher1 in teachers)
                        {
                            Console.WriteLine($"--- Teacher ---\n Id: {teacher1.Id}\n Name: {teacher1.FirstName}\n " +
                                $"Surname: {teacher1.LastName}\n Birth Date: {teacher1.BirthDate.ToString()}\n " +
                                $"Salary: {teacher1.Salary}\n");
                        }

                        break;
                }
            }
        }
    }
}
