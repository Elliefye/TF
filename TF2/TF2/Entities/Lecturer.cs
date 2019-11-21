using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2.Entities
{
    public class Lecturer
    {
        private string _id;
        public string Name { get; set; }
        public int NoOfSubjects { get; set; }
        public string Subjects { get; set; }
        public string Id { 
            get { return _id; } 
            set { _id = value; } 
        }
        public List<Subject> SubjectsNew { get; set; }
    }
}
