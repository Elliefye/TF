using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;
using TF2.ViewModels;
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

            List<LecOrSubView> top5Lecturers = new LecturerViewModel(new LecturerViewModel().Sort50()).LecturerViewList.Take(5).ToList();

            list1row0.Text = top5Lecturers.ElementAt(0).Item1;
            list1row0score.Text = top5Lecturers.ElementAt(0).Item2;

            list1row1.Text = top5Lecturers.ElementAt(1).Item1;
            list1row1score.Text = top5Lecturers.ElementAt(1).Item2;

            list1row2.Text = top5Lecturers.ElementAt(2).Item1;
            list1row2score.Text = top5Lecturers.ElementAt(2).Item2;

            list1row3.Text = top5Lecturers.ElementAt(3).Item1;
            list1row3score.Text = top5Lecturers.ElementAt(3).Item2;

            list1row4.Text = top5Lecturers.ElementAt(4).Item1;
            list1row4score.Text = top5Lecturers.ElementAt(4).Item2;

            ObservableCollection<LecOrSubView> top5SubjectsTemp = new SubjectViewModel().Sort05();
            List<LecOrSubView> top5Subjects = new SubjectViewModel(top5SubjectsTemp).SubjectViewList.Skip(Math.Max(0, top5SubjectsTemp.Count - 5)).ToList();

            list2row0.Text = top5Subjects.ElementAt(4).Item1;
            list2row0score.Text = top5Subjects.ElementAt(4).Item2;

            list2row1.Text = top5Subjects.ElementAt(3).Item1;
            list2row1score.Text = top5Subjects.ElementAt(3).Item2;

            list2row2.Text = top5Subjects.ElementAt(2).Item1;
            list2row2score.Text = top5Subjects.ElementAt(2).Item2;

            list2row3.Text = top5Subjects.ElementAt(1).Item1;
            list2row3score.Text = top5Subjects.ElementAt(1).Item2;

            list2row4.Text = top5Subjects.ElementAt(0).Item1;
            list2row4score.Text = top5Subjects.ElementAt(0).Item2;
        }
	}
}