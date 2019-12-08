using System;
using System.Collections.Generic;
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

            LecturerViewModel lecturerViewModel = new LecturerViewModel();
            lecturerViewModel = new LecturerViewModel(lecturerViewModel.Sort50());

            list1row0.Text = lecturerViewModel.LecturerViewList.ElementAt(0).Item1;
            list1row0score.Text = lecturerViewModel.LecturerViewList.ElementAt(0).Item2;

            list1row1.Text = lecturerViewModel.LecturerViewList.ElementAt(1).Item1;
            list1row1score.Text = lecturerViewModel.LecturerViewList.ElementAt(1).Item2;

            list1row2.Text = lecturerViewModel.LecturerViewList.ElementAt(2).Item1;
            list1row2score.Text = lecturerViewModel.LecturerViewList.ElementAt(2).Item2;

            list1row3.Text = lecturerViewModel.LecturerViewList.ElementAt(3).Item1;
            list1row3score.Text = lecturerViewModel.LecturerViewList.ElementAt(3).Item2;

            list1row4.Text = lecturerViewModel.LecturerViewList.ElementAt(4).Item1;
            list1row4score.Text = lecturerViewModel.LecturerViewList.ElementAt(4).Item2;

            SubjectViewModel subjectViewModel = new SubjectViewModel();
            subjectViewModel = new SubjectViewModel(subjectViewModel.Sort50());

            list2row0.Text = subjectViewModel.SubjectViewList.ElementAt(0).Item1;
            list2row0score.Text = subjectViewModel.SubjectViewList.ElementAt(0).Item2;

            list2row1.Text = subjectViewModel.SubjectViewList.ElementAt(1).Item1;
            list2row1score.Text = subjectViewModel.SubjectViewList.ElementAt(1).Item2;

            list2row2.Text = subjectViewModel.SubjectViewList.ElementAt(2).Item1;
            list2row2score.Text = subjectViewModel.SubjectViewList.ElementAt(2).Item2;

            list2row3.Text = subjectViewModel.SubjectViewList.ElementAt(3).Item1;
            list2row3score.Text = subjectViewModel.SubjectViewList.ElementAt(3).Item2;

            list2row4.Text = subjectViewModel.SubjectViewList.ElementAt(4).Item1;
            list2row4score.Text = subjectViewModel.SubjectViewList.ElementAt(4).Item2;
        }
	}
}