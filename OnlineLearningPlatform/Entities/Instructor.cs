using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.Entities
{
    public class Instructor
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string? Bio { get; set; }

        public DateTime JoinDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
