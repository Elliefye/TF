using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Android.App;
using SQLite;

namespace TF2.Entities
{
    //data preferably has to be bound to a class in sqlite, so for now we have classes for all tables
    [Table("LecturersAndSubjects")]
    class LecturersAndSubjects
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("UniqueId")]
        public int Id { get; set; }
        [NotNull, Column("LecturerId")]
        public int LecturerId { get; set; }
        [NotNull, Column("SubjectId")]
        public int SubjectId { get; set; }
    }

    //copies the DB from Assets to the local device and loads the entity lists from it
    public class EntityLoader
    {
        public static List<Lecturer> lecturers = new List<Lecturer>();
        public static List<Subject> subjects = new List<Subject>();
        public static List<Review> reviews = new List<Review>();
        //db connection can only be accessed through this class
        private static SQLiteConnection db;

        public static void ConnectToDatabase(string dbName)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);

            //copy DB to device
            if (!File.Exists(dbPath))
            {
                using (var br = new BinaryReader(Application.Context.Assets.Open(dbName)))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }

            db = new SQLiteConnection(dbPath);
        }

        public static void LoadLecturersAndSubjects()
        {
            lecturers = db.Table<Lecturer>().ToList();
            subjects = db.Table<Subject>().ToList();

            List<LecturersAndSubjects> group = db.Table<LecturersAndSubjects>().ToList();

            foreach (LecturersAndSubjects ls in group)
            {
                Lecturer currentLecturer = lecturers.Find(l => l.Id == ls.LecturerId);
                Subject currentSubject = subjects.Find(s => s.Id == ls.SubjectId);

                if(currentLecturer.Subjects == null)
                {
                    currentLecturer.Subjects = new List<Subject>();
                }

                if(currentSubject.Lecturers == null)
                {
                    currentSubject.Lecturers = new List<Lecturer>();
                }

                currentLecturer.Subjects.Add(currentSubject);
                currentSubject.Lecturers.Add(currentLecturer);
            }
        }

        public static void LoadReviews()
        {
            reviews = db.Table<Review>().ToList();

            foreach(Review r in reviews)
            {
                r.lecturer = lecturers.Find(l => l.Id == r.LecturerId);
                r.subject = subjects.Find(s => s.Id == r.SubjectId);
            }
        }

        public static void LogIn(string Username, string Password)
        {
            var match = from u in db.Table<User>()
                           where (u.Username == Username && u.Password == Password)
                           select u;

            List<User> userMatch = match.ToList();

            if (userMatch.Count == 1)
            {
                Encryption enc = new Encryption();

                ConstVars.currentUser = new User
                {
                    Id = match.ElementAt(0).Id,
                    Username = enc.Decrypt(match.ElementAt(0).Username),
                    //leave password encrypted for security reasons
                    Password = match.ElementAt(0).Password,
                    Email = enc.Decrypt(match.ElementAt(0).Email),
                    Role = match.ElementAt(0).Role
                };
            }
            //else return - log in failed
        }

        public static void UpdateUserData()
        {
            if(ConstVars.AuthStatus == 0)
            {
                throw new System.InvalidOperationException("No user is logged in");
            }
            else
            {
                //use ConstVars.current user to write to db
            }
        }

        public static void SignUp()
        {

        }

        public static void AddReview(Review newReview)
        {

        }
    }
}
