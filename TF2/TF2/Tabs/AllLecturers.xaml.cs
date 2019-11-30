using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TF2.Entities;

namespace TF2.Tabs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllLecturers : ContentPage
	{
        LecturerViewModel lecturerViewModel = new LecturerViewModel();

        public AllLecturers ()
		{
			InitializeComponent ();
            LecturerList.BindingContext = lecturerViewModel;            
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as LecOrSubView;
            Lecturer lec = EntityLoader.lecturers.FirstOrDefault(s => s.FirstName + " " + s.LastName == content.Item1);
            await Navigation.PushAsync(new SubLectProfile(lec));
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Searchbar.Text))
            {
                List<LecOrSubView> currentItems = new List<LecOrSubView>();

                foreach (LecOrSubView item in lecturerViewModel.LecturerViewList)
                {
                    if (item.Item1.ToLower().Contains(Searchbar.Text.ToLower()))
                    {
                        currentItems.Add(item);
                        System.Diagnostics.Debug.WriteLine(item.Item1);
                    }
                }

                lecturerViewModel = new LecturerViewModel(currentItems);
                LecturerList.BindingContext = lecturerViewModel;
            }
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Searchbar.Text))
            {
                lecturerViewModel = new LecturerViewModel();
                LecturerList.BindingContext = lecturerViewModel;
            }
        }
    }
}