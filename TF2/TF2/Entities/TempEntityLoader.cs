using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2.Entities
{
    public class TempEntityLoader
    {
        //TEMP place for vars till we get DB
        public static dynamic lecturersSubjects;
        public static List<Lecturer> lecturers = new List<Lecturer>();
        public static List<Subject> subjects = new List<Subject>();
        public static List<User> users = new List<User>();
        public static List<Review> reviews = new List<Review>();

        public static void LoadLecturers()
        {
            var fileLines = File.ReadAllLines("Lecturers.txt");
            fileLines = fileLines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            for (int i = 0; i + 1 < fileLines.Length; i += 2)
            {
                lecturers.Add(new Lecturer
                {
                    Id = fileLines[i].Trim(),
                    Name = fileLines[i + 1].Trim(),
                });
            }
        }

        public static void LoadSubjects()
        {
            var fileLines = File.ReadAllLines("Subjects.txt");
            fileLines = fileLines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            for (int i = 0; i + 1 < fileLines.Length; i += 2)
            {
                subjects.Add(new Subject
                {
                    LecturerID = fileLines[i].Trim(),
                    SubjectName = fileLines[i + 1].Trim(),
                });
            }
        }

        public static dynamic groupJoinSubjectsAndLecturers()
        {
            return lecturers.GroupJoin(subjects,
            id => id.Id,
            lid => lid.LecturerID,
            (id, lecturerSubjects) => new
            {
                LecturerName = id.Name,
                Subjects = lecturerSubjects,
            }).ToList();
        }

        public static void LoadReviews()
        {
            using (var reviewsFile = new StreamReader("reviewsList.txt"))
            {
                string line;
                while ((line = reviewsFile.ReadLine()) != null)
                {
                    var parts = line.Split('@');
                    reviews.Add(new Review
                    {
                        Lecturer = parts[0],
                        Subject = parts[1],
                        LecturerScore = int.Parse(parts[2]),
                        SubjectScore = int.Parse(parts[3]),
                        Comment = parts[4]
                    });
                }
                reviewsFile.Close();
            }
        }

        public static void LoadUsers()
        {
            Encryption enc = new Encryption();

            string[] user;
            using (System.IO.StreamReader userFile = new System.IO.StreamReader(@Path.Combine(Environment.CurrentDirectory, "users.txt")))
            {
                //let's hope the file is never empty
                user = userFile.ReadLine().Split(' ');

                while (user[0] != null)
                {
                    users.Add(new User
                    {
                        Username = enc.Decrypt(user[2]),
                        Password = enc.Decrypt(user[1]),
                        Email = enc.Decrypt(user[0]),
                        Role = user[3]
                    });

                    //kinda wonky but works so ¯\_(ツ)_/¯
                    user[0] = userFile.ReadLine();

                    if (user[0] != null)
                    {
                        user = user[0].Split(' ');
                    }
                }
            }
        }

        public static void SaveUsers()
        {
            Encryption enc = new Encryption();

            using (System.IO.StreamWriter userFile = new StreamWriter(@Path.Combine(Environment.CurrentDirectory, "users.txt")))
            {
                foreach(User u in users)
                {
                    userFile.WriteLine(enc.Encrypt(u.Email) + " " + enc.Encrypt(u.Password) + " " + enc.Encrypt(u.Username)
                        + " " + u.Role);
                }                            
            }
        }

    }
}
