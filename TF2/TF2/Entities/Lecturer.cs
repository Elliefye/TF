using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TF2.Entities
{
    [Table("Lecturers")]
    public class Lecturer
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [NotNull, Column("FirstName")]
        public string FirstName { get; set; }
        [NotNull, Column("LastName")]
        public string LastName { get; set; }     
        public List<Subject> Subjects { get; set; }
    }
}
