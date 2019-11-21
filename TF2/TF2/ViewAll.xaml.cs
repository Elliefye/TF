using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewAll : ContentPage
	{
		public ViewAll ()
		{
			InitializeComponent ();
            App.Current.MainPage = new MainPage();
		}
	}
}