using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TF2.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public List<Lecturer> Lecturers { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
