using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TF2.Entities
{
    [Table("Subjects")]
    public class Subject
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [NotNull, Unique, Column("SubjectName")]
        public string SubjectName { get; set; }
        public List<Lecturer> Lecturers { get; set; }
    }
}
