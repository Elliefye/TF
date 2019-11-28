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

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                User u = (User)obj;
                return (u.Id == Id && u.Username == Username && u.Email == Email);
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -13300799;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Role);
            return hashCode;
        }
    }
}
