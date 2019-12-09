using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Android.App;
using SQLite;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public static List<LecturersAndSubjects> lecAndSub;
        public static List<Review> reviews = new List<Review>();
        //db connection can only be accessed through this class
        private static SQLiteConnection db;
        private static readonly string dbName = "teaching_feedback.db";
        private static readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);        
        private static Encryption enc = new Encryption();

        public static void CopyDatabase()
        {
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
        }

        public static async void LoadLecturersAndSubjects()
        {
            using (var db = new Context(dbPath))
            {
                db.Database.Migrate();

                lecturers = await db.Lecturers.ToListAsync();
                subjects = await db.Subjects.ToListAsync();
            }
        }

        //not used anymore
        public static List<LecturersAndSubjects> GetLecturersAndSubjects()
        {
            return db.Table<LecturersAndSubjects>().ToList();
        }

        public static List<Lecturer> GetLectListForSubject(Subject sub) 
        {
            List<Lecturer> lecturerList = new List<Lecturer>();

            using (var db = new Context(dbPath))
            {
                List<int> lecturerIds = db.LecturersAndSubjects.Where(ls => ls.SubjectId == sub.Id).Select(ls => ls.LecturerId).ToList();

                foreach (int lec in lecturerIds)
                {
                    lecturerList.Add(lecturers.Find(l => l.Id == lec));
                }
            }

            return lecturerList;
        }

        public static List<Subject> GetSubListForLecturer(Lecturer lect)
        {
            List<Subject> subjectList = new List<Subject>();

            using (var db = new Context(dbPath))
            {
                List<int> subjectIds = db.LecturersAndSubjects.Where(ls => ls.LecturerId == lect.Id).Select(ls => ls.SubjectId).ToList();

                foreach(int sub in subjectIds)
                {
                    subjectList.Add(subjects.Find(s => s.Id == sub));
                }
            }

            return subjectList;
        }

        public static User LogIn(string Username, string Password)
        {
            Username = enc.Encrypt(Username);
            Password = enc.Encrypt(Password);

            using (var db = new Context(dbPath))
            {
                User match = db.Users.FirstOrDefault(u => (u.Username == Username || u.Email == Username)
                    && u.Password == Password);

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
        }

        public static void UpdateUserData()
        {
            if (ConstVars.AuthStatus == 0 || ConstVars.currentUser == null)
            {
                throw new System.InvalidOperationException("No user is logged in");
            }
            else
            {
                User current = new User
                {
                    Id = ConstVars.currentUser.Id,
                    Username = enc.Encrypt(ConstVars.currentUser.Username),
                    Email = enc.Encrypt(ConstVars.currentUser.Email),
                    Password = ConstVars.currentUser.Password,
                    Role = ConstVars.currentUser.Role
                };

                using (var db = new Context(dbPath))
                {
                    db.Update(current);
                    db.SaveChanges();
                }
            }
        }

        public static void SignUp(User newUser)
        {
            newUser.Username = enc.Encrypt(newUser.Username);
            newUser.Password = enc.Encrypt(newUser.Password);
            newUser.Email = enc.Encrypt(newUser.Email);

            using (var db = new Context(dbPath))
            {
                newUser.Id = db.Users.Max(u => u.Id);
                db.Add(newUser);
                db.SaveChanges();
            }
        }

        //finds users with the same username or email
        public static bool LookForExistingUser(string EmailOrUsername)
        {
            EmailOrUsername = enc.Encrypt(EmailOrUsername);

            using (var db = new Context(dbPath))
            {
                User match = db.Users.FirstOrDefault(u => u.Username == EmailOrUsername || u.Email == EmailOrUsername);
                return match != null;
            }    
        }

        public static bool LookForExistingUser(string email, string username)
        {
            using (var db = new Context(dbPath))
            {
                User match;

                if (ConstVars.currentUser.Email == email && ConstVars.currentUser.Username == username) //neither changed, only password
                {
                    return false;
                }
                else if (ConstVars.currentUser.Email == email) //only username changed
                {
                    username = enc.Encrypt(username);
                    match = db.Users.FirstOrDefault(u => u.Username == username);
                }
                else if (ConstVars.currentUser.Username == username) //only email changed
                {
                    email = enc.Encrypt(email);
                    match = db.Users.FirstOrDefault(u => u.Email == email);
                }
                else //both changed
                {
                    username = enc.Encrypt(username);
                    email = enc.Encrypt(email);
                    match = db.Users.FirstOrDefault(u => u.Username == username || u.Email == email);
                }

                return match != null;
            }   
        }

        //not needed anymore
        public static void AddReview(Review newReview)
        {
            db.Insert(newReview);
            reviews.Add(newReview);
        }

        public static List<LecturerReview> GetUserReviewsL()
        {
            if(ConstVars.AuthStatus == 0 || ConstVars.currentUser == null)
            {
                throw new InvalidOperationException("No user is logged in.");
            }

            using (var db = new Context(dbPath))
            {
                return db.LecturerReviews.Where(r => r.UserId == ConstVars.currentUser.Id).ToList();
            }     
        }

        public static List<SubjectReview> GetUserReviewsS()
        {
            if (ConstVars.AuthStatus == 0 || ConstVars.currentUser == null)
            {
                throw new InvalidOperationException("No user is logged in.");
            }

            using (var db = new Context(dbPath))
            {
                return db.SubjectReviews.Where(r => r.UserId == ConstVars.currentUser.Id).ToList();
            }                
        }

        public static double GetAvgRating(Subject subject)
        {
            using (var db = new Context(dbPath))
            {
                List<SubjectReview> allReviews = db.SubjectReviews.Where(sr => sr.SubjectId == subject.Id).ToList();
                if(allReviews.Count == 0)
                {
                    return 0;
                }
                else return allReviews.Select(sr => sr.Rating).Average();
            }
        }

        public static double GetAvgRating(Lecturer lecturer)
        {
            using (var db = new Context(dbPath))
            {
                List<LecturerReview> allreviews = db.LecturerReviews.Where(lr => lr.LecturerId == lecturer.Id).ToList();
                if (allreviews.Count == 0)
                {
                    return 0;
                }
                else return allreviews.Select(lr => lr.Rating).Average();
            }
        }

        public static List<SubjectReview> GetSubjectReviews(Subject subject)
        {
            using (var db = new Context(dbPath))
            {
                return db.SubjectReviews.Where(sr => sr.SubjectId == subject.Id).ToList();
            }                
        }

        public static List<LecturerReview> GetLecturerReviews(Lecturer lecturer)
        {
            using (var db = new Context(dbPath))
            {
                return db.LecturerReviews.Where(lr => lr.LecturerId == lecturer.Id).ToList();
            }
        }

        public static void AddReview(LecturerReview review)
        {
            using (var db = new Context(dbPath))
            {
                db.LecturerReviews.Add(review);
                db.SaveChanges();
            }
        }

        public static void AddReview(SubjectReview review)
        {
            using (var db = new Context(dbPath))
            {
                db.SubjectReviews.Add(review);
                db.SaveChanges();
            }
        }

        public static void EditReview(SubjectReview review)
        {
            using (var db = new Context(dbPath))
            {
                db.Update(review);
                db.SaveChanges();
            }
        }

        public static void EditReview(LecturerReview review)
        {
            using (var db = new Context(dbPath))
            {
                db.Update(review);
                db.SaveChanges();
            }
        }

        public static void DeleteReview(LecturerReview review)
        {
            using (var db = new Context(dbPath))
            {
                db.Remove(review);
                db.SaveChanges();
            }
        }

        public static void DeleteReview(SubjectReview review)
        {
            using (var db = new Context(dbPath))
            {
                db.Remove(review);
                db.SaveChanges();
            }
        }

        public static string GetReviewerUsername(LecturerReview lecturerReview)
        {
            using (var db = new Context(dbPath))
            {
                string reviewerUsername = db.Users.FirstOrDefault(u => u.Id == lecturerReview.UserId).Username;
                return enc.Decrypt(reviewerUsername);
            }
        }

        public static string GetReviewerUsername(SubjectReview subjectReview)
        {
            using (var db = new Context(dbPath))
            {
                string reviewerUsername = db.Users.FirstOrDefault(u => u.Id == subjectReview.UserId).Username;
                return enc.Decrypt(reviewerUsername);
            }
        }

        public static User GetUserFromId(int id)
        {
            using (var db = new Context(dbPath))
            {
                User user = db.Users.FirstOrDefault(u => u.Id == id);
                user.Username = enc.Decrypt(user.Username);
                user.Email = enc.Decrypt(user.Email);
                return user;
            }
        }
    }
}