using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            AssetManager assets = Android.App.Application.Context.Assets;
            using (StreamReader reader = new StreamReader(assets.Open("Lecturers.txt")))
            {
                string text = reader.ReadToEnd();
                string[] fileLines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                fileLines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                for (int i = 0; i + 1 < fileLines.Length; i += 2)
                {
                    lecturers.Add(new Lecturer
                    {
                        Id = Int32.Parse(fileLines[i].Trim()),
                        Name = fileLines[i + 1].Trim(),
                    });
                }
            }          
        }

        public static void LoadSubjects()
        {
            using (StreamReader reader = new StreamReader(Android.App.Application.Context.Assets.Open("Subjects.txt")))
            {
                string text = reader.ReadToEnd();
                string[] fileLines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                fileLines = fileLines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                for (int i = 0; i + 1 < fileLines.Length; i += 2)
                {
                    subjects.Add(new Subject
                    {
                        Id = Int32.Parse(fileLines[i].Trim()),
                        SubjectName = fileLines[i + 1].Trim(),
                    });
                }
            }
        }

        public static dynamic groupJoinSubjectsAndLecturers()
        {
            return lecturers.GroupJoin(subjects,
            id => id.Id,
            lid => lid.Id,
            (id, lecturerSubjects) => new
            {
                LecturerName = id.Name,
                Subjects = lecturerSubjects,
            }).ToList();
        }

        public static void LoadReviews()
        {
            using (StreamReader reader = new StreamReader(Android.App.Application.Context.Assets.Open("reviewsList.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
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
            }
        }

        public static void LoadUsers()
        {
            Encryption enc = new Encryption();

            string[] user;
            using (StreamReader reader = new StreamReader(Android.App.Application.Context.Assets.Open("users.txt")))
            {
                //let's hope the file is never empty
                user = reader.ReadLine().Split(' ');

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
                    user[0] = reader.ReadLine();

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

            var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "users.txt");
            using (var writer = new System.IO.StreamWriter(filename))
            {
                foreach (User u in users)
                {
                    writer.WriteLine(enc.Encrypt(u.Email) + " " + enc.Encrypt(u.Password) + " " + enc.Encrypt(u.Username)
                        + " " + u.Role);
                }
            }
        }
    }
}
