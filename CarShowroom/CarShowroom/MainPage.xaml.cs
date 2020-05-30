using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarShowroom
{
    public partial class MainPage : ContentPage
    {
        public TestDrive TestDrive { get; set; }

        public ObservableCollection<Car> Cars { get; set; }

        public int ListViewRowHeight { get; set; }

        public int ListViewHeightRequest { get; set; }

        public MainPage()
        {              
            TestDrive = new TestDrive();

            ListViewRowHeight = 50;

            BindingContext = this;
            InitializeComponent();

            MessagingCenter.Subscribe<Page>(this, "TestDriveSaved", (sender) =>
            {
                TestDrive = new TestDrive();
                OnPropertyChanged("TestDrive");                
            });
        }

        protected override void OnAppearing()
        {
            Cars = new ObservableCollection<Car>(App.Database.GetCars());
            OnPropertyChanged("Cars");
            
            ListViewHeightRequest = Cars.Count * ListViewRowHeight;
            OnPropertyChanged("ListViewHeightRequest");

            base.OnAppearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConfirmPage(TestDrive));
        }
    }
}
