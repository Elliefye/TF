using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2.Tabs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();

            List<Lecturer> top5 = EntityLoader.GetTop5Lecturers();

            Lecturer current = top5.ElementAt(0);
            list1row0.Text = current.FirstName + " " + current.LastName;
            list1row0score.Text = EntityLoader.GetAvgRating(current).ToString();

            current = top5.ElementAt(1);
            list1row1.Text = current.FirstName + " " + current.LastName;
            list1row1score.Text = EntityLoader.GetAvgRating(current).ToString();

            current = top5.ElementAt(2);
            list1row2.Text = current.FirstName + " " + current.LastName;
            list1row2score.Text = EntityLoader.GetAvgRating(current).ToString();

            current = top5.ElementAt(3);
            list1row3.Text = current.FirstName + " " + current.LastName;
            list1row3score.Text = EntityLoader.GetAvgRating(current).ToString();

            current = top5.ElementAt(4);
            list1row4.Text = current.FirstName + " " + current.LastName;
            list1row4score.Text = EntityLoader.GetAvgRating(current).ToString();

            List<Subject> top5s = EntityLoader.GetTop5Subjects();

            Subject currents = top5s.ElementAt(0);
            list2row0.Text = currents.SubjectName;
            list2row0score.Text = EntityLoader.GetAvgRating(currents).ToString();

            currents = top5s.ElementAt(1);
            list2row1.Text = currents.SubjectName;
            list2row1score.Text = EntityLoader.GetAvgRating(currents).ToString();

            currents = top5s.ElementAt(2);
            list2row2.Text = currents.SubjectName;
            list2row2score.Text = EntityLoader.GetAvgRating(currents).ToString();

            currents = top5s.ElementAt(3);
            list2row3.Text = currents.SubjectName;
            list2row3score.Text = EntityLoader.GetAvgRating(currents).ToString();

            currents = top5s.ElementAt(4);
            list2row4.Text = currents.SubjectName;
            list2row4score.Text = EntityLoader.GetAvgRating(currents).ToString();
        }
	}
}