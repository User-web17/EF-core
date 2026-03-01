using EF_core.Contexts;
using EF_core.Entities;
using EF_core.Migrations;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Serialization;

namespace EF_core
{
    public class Program
    {
        static AppDbContext db = new AppDbContext();
        static void Main(string[] args)
        {
            
        }

        static void ChangeStudent(AppDbContext context)
        {
            Console.Write("Enter student id: ");
            int id = int.Parse(Console.ReadLine()!);

            Student? student = context.Students.FirstOrDefault(x => x.Id == id);

            if (student != null)
            {
                Console.WriteLine("Enter new first name: ");

                string? temp = Console.ReadLine();
                student.FirstName = (temp.IsNullOrEmpty() ? student.FirstName : temp)!;

                Console.WriteLine("Enter new last name: ");
                temp = Console.ReadLine();
                student.LastName = (temp.IsNullOrEmpty() ? student.LastName : temp)!;

                Console.WriteLine("Enter new email: ");
                temp = Console.ReadLine();
                student.Email = (temp.IsNullOrEmpty() ? student.Email : temp)!;

                Console.WriteLine("Enter new birth date: ");
                string? newBirthDate = Console.ReadLine();
                if (!newBirthDate.IsNullOrEmpty())
                {
                    student.BirthDate = DateTime.Parse(newBirthDate!);
                }
                context.SaveChanges();
            }
        }

        static void DeleteSudent(AppDbContext context)
        {
            Console.WriteLine("Enter Student Id: ");
            int id = Int32.Parse(Console.ReadLine()!);
            Student st = db.Students.FirstOrDefault(x => x.Id == id)!;

            if (st != null)
            {
                db.Students.Remove(st);
            }

            context.SaveChanges();
        }

        static void AddStudent(AppDbContext context)
        {

        }
    }
}
