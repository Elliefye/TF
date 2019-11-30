using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TF2.Entities;
using TF2.Tabs;

namespace TF2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProRevNavigation : TabbedPage
    {
        public ProRevNavigation(Lecturer lecturer)
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            SwitchPage(lecturer);
        }

        public ProRevNavigation(Subject subject)
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            SwitchPage(subject);
        }

        private async void SwitchPage(Lecturer lecturer) {
            await Navigation.PushAsync(new SubLectProfile(lecturer));
        }

        private async void SwitchPage(Subject subject)
        {
            await Navigation.PushAsync(new SubLectProfile(subject));
        }
    }
}