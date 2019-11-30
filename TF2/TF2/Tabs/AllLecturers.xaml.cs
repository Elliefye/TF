using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            LecturerViewList.Add(new LecturerView("Kasuba", "5"));
            LecturerViewList.Add(new LecturerView("Skersys", "4.76"));
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
    }
}