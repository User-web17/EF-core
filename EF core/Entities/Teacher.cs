using System;
using System.Collections.Generic;
using System.Text;

namespace EF_core.Entities
{
    public class Teacher
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
    }
}
