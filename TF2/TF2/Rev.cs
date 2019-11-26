using System;
using System.Collections.Generic;
using System.Text;

namespace TF2
{
    class Rev
    {
        public String LecturerName { get; set; }
        public String SubjectName { get; set; }
        public int LecturerScore { get; set; }
        public int SubjectScore { get; set; }
        public String Comment { get; set; }

        public Rev(String lecturerName, String subjectName, int lecturerScore, int subjectScore, String comment)
        {
            LecturerName = lecturerName;
            SubjectName = subjectName;
            LecturerScore = lecturerScore;
            SubjectScore = subjectScore;
            Comment = comment;
        }
    }
}
