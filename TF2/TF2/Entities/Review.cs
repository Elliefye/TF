using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TF2.Entities
{
    [Table("Reviews")]
    public class Review
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("UniqueId")]
        public int Id { get; set; }
        [NotNull, Column("LecturerId")]
        public int LecturerId { get; set; }
        [NotNull, Column("SubjectId")]
        public int SubjectId { get; set; }
        [NotNull, Column("LecturerReview")]
        public int LecturerScore { get; set; }
        [NotNull, Column("SubjectReview")]
        public int SubjectScore { get; set; }
        [Column("Comment")]
        public string Comment { get; set; }
        [NotNull, Column("UserId")]
        public int UserId { get; set; }
        public Lecturer lecturer;
        public Subject subject;
    }
}
