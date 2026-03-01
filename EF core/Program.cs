using EF_core.Contexts;
using EF_core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Xml.Serialization;

namespace EF_core
{
    public static class Program
    {
        static AppDbContext db = new AppDbContext();
        static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Initialize Database");
                Console.WriteLine("2. Show Info");
                Console.WriteLine("3. Update Info");
                Console.WriteLine("4. Delete Info");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        InitializeDB(db);
                        break;
                    case "2":
                        ShowInfo(db);
                        break;
                    case "3":
                        UpdateInfo(db);
                        break;
                    case "4":
                        DeleteInfo(db);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void InitializeDB(AppDbContext db)
        {
            if (db.Departments.Any())
            {
                Console.WriteLine("Database already initialized.");
                return;
            }

            var itDepartment = new Department
            {
                Name = "IT Department",
                Description = "Programming and Software Engineering",
                Teachers = new List<Teacher>(),
                Subjects = new List<Subject>()
            };

            var mathDepartment = new Department
            {
                Name = "Mathematics Department",
                Description = "Pure and Applied Mathematics",
                Teachers = new List<Teacher>(),
                Subjects = new List<Subject>()
            };

            var linguistDepartment = new Department
            {
                Name = "Linguist Department",
                Description = "Linguistics",
                Teachers = new List<Teacher>(),
                Subjects = new List<Subject>()
            };

            var csharp = new Subject
            {
                Name = "C#",
                Description = ".NET and WinForms",
                Department = itDepartment,
                Teachers = new List<Teacher>()
            };

            var databases = new Subject
            {
                Name = "Databases",
                Description = "SQL Server and EF Core",
                Department = itDepartment,
                Teachers = new List<Teacher>()
            };

            var algebra = new Subject
            {
                Name = "Algebra",
                Description = "Linear Algebra",
                Department = mathDepartment,
                Teachers = new List<Teacher>()
            };

            var ukrainian = new Subject
            {
                Name = "Ukrainian",
                Description = "In-depth Ukrainian",
                Department = linguistDepartment,
                Teachers = new List<Teacher>()
            };

            var english = new Subject
            {
                Name = "English",
                Description = "In-depth English",
                Department = linguistDepartment,
                Teachers = new List<Teacher>()
            };

            var groupA = new Group
            {
                Name = "IT-101",
                Students = new List<Student>(),
                Teachers = new List<Teacher>()
            };

            var groupB = new Group
            {
                Name = "MATH-202",
                Students = new List<Student>(),
                Teachers = new List<Teacher>()
            };

            var groupC = new Group
            {
                Name = "DB-303",
                Students = new List<Student>(),
                Teachers = new List<Teacher>()
            };

            var groupD = new Group
            {
                Name = "UKR-404",
                Students = new List<Student>(),
                Teachers = new List<Teacher>()
            };

            var groupE = new Group
            {
                Name = "UKR-505",
                Students = new List<Student>(),
                Teachers = new List<Teacher>()
            };

            var teacher1 = new Teacher
            {
                FirstName = "John",
                LastName = "Smith",
                BirthDate = new DateTime(1985, 4, 10),
                Salary = 3000,
                Department = itDepartment,
                Subjects = new List<Subject> { csharp, databases },
                Groups = new List<Group> { groupA }
            };

            var teacher2 = new Teacher
            {
                FirstName = "Anna",
                LastName = "Myhailivna",
                BirthDate = new DateTime(1990, 6, 22),
                Salary = 2800,
                Department = mathDepartment,
                Subjects = new List<Subject> { algebra },
                Groups = new List<Group> { groupB }
            };

            var teacher3 = new Teacher
            {
                FirstName = "Michael",
                LastName = "Clear",
                BirthDate = new DateTime(1990, 6, 22),
                Salary = 2800,
                Department = linguistDepartment,
                Subjects = new List<Subject> { algebra },
                Groups = new List<Group> { groupB }
            };

            var teacher4 = new Teacher
            {
                FirstName = "May",
                LastName = "Brown",
                BirthDate = new DateTime(1990, 6, 22),
                Salary = 2800,
                Department = linguistDepartment,
                Subjects = new List<Subject> { algebra },
                Groups = new List<Group> { groupB }
            };

            var student1 = new Student
            {
                FirstName = "Alex",
                LastName = "Shevchenko",
                Email = "alex@gmail.com",
                BirthDate = new DateTime(2004, 2, 15),
                Group = groupA
            };

            var student2 = new Student
            {
                FirstName = "Maria",
                LastName = "Hvylova",
                Email = "maria@gmail.com",
                BirthDate = new DateTime(2005, 8, 5),
                Group = groupA
            };

            var student3 = new Student
            {
                FirstName = "David",
                LastName = "Kozak",
                Email = "david@gmail.com",
                BirthDate = new DateTime(2003, 11, 20),
                Group = groupB
            };

            var student4 = new Student
            {
                FirstName = "Sofia",
                LastName = "Zvychaina",
                Email = "sofia@gmail.com",
                BirthDate = new DateTime(2004, 2, 15),
                Group = groupC
            };

            var student5 = new Student
            {
                FirstName = "Kyrylo",
                LastName = "Mefodiyovich",
                Email = "kyrylo@gmail.com",
                BirthDate = new DateTime(2005, 8, 5),
                Group = groupD
            };

            var student6 = new Student
            {
                FirstName = "Myhailo",
                LastName = "Melnyk",
                Email = "myhailo@gmail.com",
                BirthDate = new DateTime(2003, 11, 20),
                Group = groupE
            };

            db.Departments.AddRange(itDepartment, mathDepartment, linguistDepartment);
            db.Subjects.AddRange(csharp, databases, algebra, ukrainian, english);
            db.Groups.AddRange(groupA, groupB, groupC, groupD, groupE);
            db.Teachers.AddRange(teacher1, teacher2, teacher3, teacher4);
            db.Students.AddRange(student1, student2, student3, student4, student5, student6);

            db.SaveChanges();

            Console.WriteLine("Database initialized successfully.");
        }

        static void ShowInfo(AppDbContext db)
        {
            Console.WriteLine("\n--- Departments ---");
            var departments = db.Departments
                .Include(d => d.Teachers)
                .Include(d => d.Subjects)
                .ToList();

            foreach (var d in departments)
                Console.WriteLine(d);

            Console.WriteLine("\n--- Teachers ---");
            var teachers = db.Teachers
                .Include(t => t.Subjects)
                .Include(t => t.Groups)
                .ToList();

            foreach (var t in teachers)
                Console.WriteLine(t);

            Console.WriteLine("\n--- Students ---");
            var students = db.Students
                .Include(s => s.Group)
                .ToList();

            foreach (var s in students)
                Console.WriteLine(s);

            Console.WriteLine("\n--- Groups ---");
            var groups = db.Groups
                .Include(g => g.Students)
                .Include(g => g.Teachers)
                .ToList();

            foreach (var g in groups)
                Console.WriteLine(g);
        }

        static void UpdateInfo(AppDbContext db)
        {
            Console.WriteLine("\n1 - Update Teacher Salary");
            Console.WriteLine("2 - Update Student Email");
            Console.Write("Select: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Enter Teacher Id: ");
                    int tId = int.Parse(Console.ReadLine()!);

                    var teacher = db.Teachers.FirstOrDefault(t => t.Id == tId);
                    if (teacher == null)
                    {
                        Console.WriteLine("Teacher not found.");
                        return;
                    }

                    Console.Write("Enter new salary: ");
                    teacher.Salary = decimal.Parse(Console.ReadLine()!);

                    db.SaveChanges();
                    Console.WriteLine("Salary updated.");
                    break;

                case "2":
                    Console.Write("Enter Student Id: ");
                    int sId = int.Parse(Console.ReadLine()!);

                    var student = db.Students.FirstOrDefault(s => s.Id == sId);
                    if (student == null)
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }

                    Console.Write("Enter new email: ");
                    student.Email = Console.ReadLine()!;

                    db.SaveChanges();
                    Console.WriteLine("Email updated.");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void DeleteInfo(AppDbContext db)
        {
            Console.WriteLine("\n1 - Delete Student");
            Console.WriteLine("2 - Delete Teacher");
            Console.Write("Select: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Enter Student Id: ");
                    int sId = int.Parse(Console.ReadLine()!);

                    var student = db.Students.FirstOrDefault(s => s.Id == sId);
                    if (student == null)
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }

                    db.Students.Remove(student);
                    db.SaveChanges();
                    Console.WriteLine("Student deleted.");
                    break;

                case "2":
                    Console.Write("Enter Teacher Id: ");
                    int tId = int.Parse(Console.ReadLine()!);

                    var teacher = db.Teachers
                        .Include(t => t.Subjects)
                        .Include(t => t.Groups)
                        .FirstOrDefault(t => t.Id == tId);

                    if (teacher == null)
                    {
                        Console.WriteLine("Teacher not found.");
                        return;
                    }

                    db.Teachers.Remove(teacher);
                    db.SaveChanges();
                    Console.WriteLine("Teacher deleted.");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
