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
	public partial class SubLectProfile : ContentPage
	{
        private List<string> list1Empty = new List<string>();
        private List<string> list1Full = new List<string>();
        private bool list1Collapsed = false;

		public SubLectProfile ()
		{
			InitializeComponent ();
		}

        public SubLectProfile(Subject subject)
        {
            InitializeComponent();

            Title = TitleLabel.Text = subject.SubjectName;
            SubtitleLabel.Text = "Subject at Vilnius University";
            RatingLabel.Text = "Rating: " + EntityLoader.GetAvgRating(subject);
            List1Label.Text = "All lecturers: ▼";

            foreach(Lecturer l in subject.Lecturers.Value)
            {
                list1Full.Add(l.FirstName + " " + l.LastName);
            }

            SubOrLectList.ItemsSource = list1Full;
            var list1LabelTapGestureRecognizer = new TapGestureRecognizer();

            list1LabelTapGestureRecognizer.Tapped += (s, e) => {
                if(list1Collapsed)
                {
                    SubOrLectList.ItemsSource = list1Full;
                    List1Label.Text = "All lecturers: ▼";
                    list1Collapsed = false;
                }
                else
                {
                    SubOrLectList.ItemsSource = list1Empty;
                    List1Label.Text = "All lecturers: ▷";
                    list1Collapsed = true;
                }
            };

            List1Label.GestureRecognizers.Add(list1LabelTapGestureRecognizer);
        }

        public SubLectProfile(Lecturer lecturer)
        {
            InitializeComponent();

            Title = TitleLabel.Text = lecturer.FirstName + " " + lecturer.LastName;
            SubtitleLabel.Text = "Lecturer at Vilnius University";
            RatingLabel.Text = "Rating: " + EntityLoader.GetAvgRating(lecturer);
            List1Label.Text = "All subjects: ▼";

            foreach(Subject s in lecturer.Subjects.Value)
            {
                list1Full.Add(s.SubjectName);
            }

            SubOrLectList.ItemsSource = list1Full;
            var list1LabelTapGestureRecognizer = new TapGestureRecognizer();

            list1LabelTapGestureRecognizer.Tapped += (s, e) => {
                if (list1Collapsed)
                {
                    SubOrLectList.ItemsSource = list1Full;
                    List1Label.Text = "All subjects: ▼";
                    list1Collapsed = false;
                }
                else
                {
                    SubOrLectList.ItemsSource = list1Empty;
                    List1Label.Text = "All subjects: ▷";
                    list1Collapsed = true;
                }
            };

            List1Label.GestureRecognizers.Add(list1LabelTapGestureRecognizer);
        }

        private async void ItemFromList1Tapped(object sender, ItemTappedEventArgs e)
        {
            string item = e.Item.ToString();
            bool answer = await DisplayAlert("", "Would you like to visit the profile page for " + item + "?", "Yes please!", "No thanks");

            if(answer)
            {
                Lecturer lect = EntityLoader.lecturers.FirstOrDefault(l =>
                {
                    string name = l.FirstName + " " + l.LastName;
                    return name == item;
                });

                if (lect != null)
                {
                    await Navigation.PushAsync(new SubLectProfile(lect));
                    //await Navigation.PushAsync(new NavigationPage(new SubLectProfile(lect)));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                    return;
                }

                Subject sub = EntityLoader.subjects.FirstOrDefault(s => s.SubjectName == item);

                if (sub != null)
                {
                    await Navigation.PushAsync(new SubLectProfile(sub));
                    //await Navigation.PushAsync(new NavigationPage(new SubLectProfile(sub)));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                    return;
                }

                await DisplayAlert("Oops", "Something went wrong.", "Oh no!");
            }
        }

    }
}