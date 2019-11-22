using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TF2.Entities
{
    [Table ("Users")]
    public class User
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [NotNull, Unique, Column("Username")]
        public string Username { get; set; }
        [NotNull, Unique, Column("Password")]
        public string Password { get; set; }
        [NotNull, Column("Email")]
        public string Email { get; set; }
        [Column("Role")]
        public string Role { get; set; }
    }
}
