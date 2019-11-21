using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2.Entities
{
    public class Review
    {
        public string Lecturer { get; set; }
        public string Subject { get; set; }
        public int LecturerScore { get; set; }
        public int SubjectScore { get; set; }
        public string Comment { get; set; }
    }
}
