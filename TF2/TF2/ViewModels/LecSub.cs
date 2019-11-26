using System;
using System.Collections.Generic;
using System.Text;

namespace TF2
{
    public class LecSub
    {
        public int lecturerId;
        public int subjectId;

        public String LecturerName { get; set; }
        public String SubjectName { get; set; }

        public LecSub(int lecturerId, int subjectId, String lecturerName, String subjectName)
        {
            this.lecturerId = lecturerId;
            this.subjectId = subjectId;
            LecturerName = lecturerName;
            SubjectName = subjectName;
        }
    }
}
