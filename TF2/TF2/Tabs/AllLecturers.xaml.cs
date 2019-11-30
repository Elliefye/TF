using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2.Tabs
{
    public class LecturerView : Tuple<string, string>
    {
        public LecturerView(string item1, string item2) : base(item1, item2 ?? CreateEmptyModel()) { }

        private static string CreateEmptyModel()
        {
            return "";
        }
    }

    public class LecturerViewModel
    {
        public ObservableCollection<LecturerView> LecturerViewList { get; set; }

        public LecturerViewModel()
        {
            LecturerViewList = new ObservableCollection<LecturerView>();
            
            foreach(Lecturer lect in EntityLoader.lecturers)
            {
                LecturerViewList.Add(new LecturerView(lect.FirstName + " " + lect.LastName, Math.Round(EntityLoader.GetAvgRating(lect), 2).ToString()));
            }
        }
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllLecturers : ContentPage
	{
		public AllLecturers ()
		{
			InitializeComponent ();
            LecturerList.BindingContext = new LecturerViewModel();            
            //LecturerList.BindingContext = lc;
        }

        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {

        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}