using System;
using System.Collections.Generic;
using System.Text;

namespace EF_core.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<Teacher> Teachers { get; set; } = null!;
        public override string ToString()
        {
            return $"{Id} | {Name} | {Description} | Teacher count: {Teachers.Count}";
        }
    }
}
