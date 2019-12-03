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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubLectProfile : ContentPage
	{
        private List<string> list1Empty = new List<string>();
        private List<string> list1Full = new List<string>();
        private ObservableCollection<Rev> list2Empty = new ObservableCollection<Rev>();
        private ObservableCollection<Rev> list2Full = new ObservableCollection<Rev>();
        private bool list1Collapsed = false;
        private bool list2Collapsed = false;
        private bool subprof = true;
        private int id;

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

            foreach (Lecturer l in subject.Lecturers.Value)
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
            id = subject.Id;
            
            ReviewViewModel reviewViewModel = new ReviewViewModel(subject);
            ReviewList.BindingContext = reviewViewModel;
            list2Full = reviewViewModel.ReviewViewList;
            ReviewListChange();

            if (EntityLoader.GetUserReviewsS().Any(sr => sr.SubjectId == id))
            {
                AddReviewBtn.Text = "Change review";
                AddReviewBtn.Clicked -= AddReviewBtn_Clicked;
                AddReviewBtn.Clicked += EditReviewBtn_Clicked;
            }
        }

        public SubLectProfile(Lecturer lecturer)
        {
            InitializeComponent();

            Title = TitleLabel.Text = lecturer.FirstName + " " + lecturer.LastName;
            SubtitleLabel.Text = "Lecturer at Vilnius University";
            RatingLabel.Text = "Rating: " + EntityLoader.GetAvgRating(lecturer);
            List1Label.Text = "All subjects: ▼";

            foreach (Subject s in lecturer.Subjects.Value)
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
            subprof = false;
            id = lecturer.Id;

            ReviewViewModel reviewViewModel = new ReviewViewModel(lecturer);
            ReviewList.BindingContext = reviewViewModel;
            list2Full = reviewViewModel.ReviewViewList;
            ReviewListChange();

            if (EntityLoader.GetUserReviewsL().Any(lr => lr.LecturerId == id))
            {
                AddReviewBtn.Text = "Change review";
                AddReviewBtn.Clicked -= AddReviewBtn_Clicked;
                AddReviewBtn.Clicked += EditReviewBtn_Clicked;
            }
        }

        private void ReviewListChange()
        {
            List2Label.Text = "All reviews: ▼";

            var list2LabelTapGestureRecognizer = new TapGestureRecognizer();

            list2LabelTapGestureRecognizer.Tapped += (s, e) => {
                if (list2Collapsed)
                {
                    ReviewList.ItemsSource = list2Full;
                    List2Label.Text = "All reviews: ▼";
                    list2Collapsed = false;
                }
                else
                {
                    ReviewList.ItemsSource = list2Empty;
                    List2Label.Text = "All reviews: ▷";
                    list2Collapsed = true;
                }
            };

            List2Label.GestureRecognizers.Add(list2LabelTapGestureRecognizer);
        }

        private async void ItemFromList1Tapped(object sender, ItemTappedEventArgs e)
        {
            string item = e.Item.ToString();
            bool answer = await DisplayAlert("", "Would you like to visit the profile page for " + item + "?", "Yes!", "No thanks");

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
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                    return;
                }

                Subject sub = EntityLoader.subjects.FirstOrDefault(s => s.SubjectName == item);

                if (sub != null)
                {
                    await Navigation.PushAsync(new SubLectProfile(sub));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                    return;
                }

                await DisplayAlert("Oops", "Something went wrong.", "Oh no!");
            }
        }

        async void AddReviewBtn_Clicked(object sender, EventArgs e)
        {
            if(subprof)
            {
                Subject subject = EntityLoader.subjects.Find(s => s.Id == id);
                var nextPage = new AddReview(subject);
                nextPage.AddedReview += UpdateReviewList;
                await Navigation.PushAsync(nextPage);
            }
            else
            {
                Lecturer lecturer = EntityLoader.lecturers.Find(l => l.Id == id);
                var nextPage = new AddReview(lecturer);
                nextPage.AddedReview += UpdateReviewList;
                await Navigation.PushAsync(nextPage);
            }
        }

        async void EditReviewBtn_Clicked(object sender, EventArgs e)
        {
            if (subprof)
            {
                SubjectReview review = EntityLoader.GetUserReviewsS().Find(sr => sr.SubjectId == id);
                Subject subject = EntityLoader.subjects.Find(s => s.Id == id);
                var nextPage = new AddReview(subject, review);
                nextPage.AddedReview += UpdateReviewList;
                await Navigation.PushAsync(nextPage);
            }
            else
            {
                LecturerReview review = EntityLoader.GetUserReviewsL().Find(lr => lr.LecturerId == id);
                Lecturer lecturer = EntityLoader.lecturers.Find(l => l.Id == id);
                var nextPage = new AddReview(lecturer);
                nextPage.AddedReview += UpdateReviewList;
                await Navigation.PushAsync(nextPage);
            }
        }

        private void UpdateReviewList(object sender, EventArgs e)
        {
            if(subprof) //subject profile page
            {
                Subject current = EntityLoader.subjects.FirstOrDefault(s => s.Id == id);
                RatingLabel.Text = "Rating: " + EntityLoader.GetAvgRating(current);

                ReviewViewModel reviewViewModel = new ReviewViewModel(current);
                ReviewList.BindingContext = reviewViewModel;
                list2Full = reviewViewModel.ReviewViewList;

                if (EntityLoader.GetUserReviewsS().Any(sr => sr.SubjectId == id))
                {
                    AddReviewBtn.Text = "Change review";
                    AddReviewBtn.Clicked -= AddReviewBtn_Clicked;
                    AddReviewBtn.Clicked += EditReviewBtn_Clicked;
                }
            }
            else
            {
                Lecturer current = EntityLoader.lecturers.FirstOrDefault(l => l.Id == id);
                RatingLabel.Text = "Rating: " + EntityLoader.GetAvgRating(current);

                ReviewViewModel reviewViewModel = new ReviewViewModel(current);
                ReviewList.BindingContext = reviewViewModel;
                list2Full = reviewViewModel.ReviewViewList;

                if (EntityLoader.GetUserReviewsL().Any(lr => lr.LecturerId == id))
                {
                    AddReviewBtn.Text = "Change review";
                    AddReviewBtn.Clicked -= AddReviewBtn_Clicked;
                    AddReviewBtn.Clicked += EditReviewBtn_Clicked;
                }
            }
        }
    }
}