using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TF2.Entities
{
    [Table("LecturerReviews")]
    public class LecturerReview
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [NotNull, Column("LecturerId")]
        public int LecturerId { get; set; }
        [Column("SubjectId")]
        public int SubjectId { get; set; }
        [NotNull, Column("Rating")]
        public int Rating { get; set; }
        [Column("Comment")]
        public string Comment { get; set; }
        [NotNull, Column("UserId")]
        public int UserId { get; set; }
        [NotNull, Column("Anonymous")]
        public int Anonymous { get; set; }
    }
}
