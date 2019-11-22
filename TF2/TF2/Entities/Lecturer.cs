using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2.Entities
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NoOfSubjects { get; set; }       
        public List<Subject> Subjects { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
