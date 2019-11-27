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
    public class LecturersAndSubjects
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
        public static List<LecturersAndSubjects> lecAndSub = new List<LecturersAndSubjects>();
        public static List<Review> reviews = new List<Review>();
        //db connection can only be accessed through this class
        private static SQLiteConnection db;
        private static Encryption enc = new Encryption();

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

            lecAndSub = db.Table<LecturersAndSubjects>().ToList();

            foreach (LecturersAndSubjects ls in lecAndSub)
            {
                Lecturer currentLecturer = lecturers.Find(l => l.Id == ls.LecturerId);
                Subject currentSubject = subjects.Find(s => s.Id == ls.SubjectId);

                if (currentLecturer.Subjects == null)
                {
                    currentLecturer.Subjects = new List<Subject>();
                }

                if (currentSubject.Lecturers == null)
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

            foreach (Review r in reviews)
            {
                r.lecturer = lecturers.Find(l => l.Id == r.LecturerId);
                r.subject = subjects.Find(s => s.Id == r.SubjectId);
            }
        }

        public static User LogIn(string Username, string Password)
        {
            Username = enc.Encrypt(Username);
            Password = enc.Encrypt(Password);

            /*var match = from u in db.Table<User>()
                           where ((u.Username == Username && u.Password == Password) || (u.Email == Username && u.Password == Password))
                           select u;
                           
             List<User> userMatch = match.ToList();*/

            //supports logging in with either email or username

            User match = db.Table<User>().FirstOrDefault(u => (u.Username == Username || u.Email == Username) && u.Password == Password);

            if (match != null)
            {
                return new User
                {
                    Id = match.Id,
                    Username = enc.Decrypt(match.Username),
                    //leave password encrypted for security reasons
                    Password = match.Password,
                    Email = enc.Decrypt(match.Email),
                    Role = match.Role
                };
            }
            else return null;
        }

        public static void UpdateUserData()
        {
            if (ConstVars.AuthStatus == 0)
            {
                throw new System.InvalidOperationException("No user is logged in");
            }
            else
            {
                //use ConstVars.currentUser to write to db
                User current = ConstVars.currentUser;

                current.Username = enc.Encrypt(current.Username);
                current.Email = enc.Encrypt(current.Email);

                db.Update(current);
            }
        }

        public static void SignUp(User newUser)
        {
            newUser.Username = enc.Encrypt(newUser.Username);
            newUser.Password = enc.Encrypt(newUser.Password);
            newUser.Email = enc.Encrypt(newUser.Email);

            db.Insert(newUser);
        }

        public static void AddReview(Review newReview)
        {
            db.Insert(newReview);
            reviews.Add(newReview);
        }
    }
}