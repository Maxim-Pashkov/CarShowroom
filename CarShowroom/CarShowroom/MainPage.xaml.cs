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

        public MainPage()
        {              
            TestDrive = new TestDrive();

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
            Car carSelected = (Car)ListView.SelectedItem;
            List<Car> CarsList = App.Database.GetCars().ToList();
            ObservableCollection<Grouping<string, Car>> Cars = new ObservableCollection<Grouping<string, Car>>(CarsList.GroupBy(Car => Car.Brand).Select(g => new Grouping<string, Car>(g.Key, g)));
            ListView.ItemsSource = Cars;
            ListView.SelectedItem = CarsList.FirstOrDefault(car => car.Id == carSelected?.Id);
            ListView.RowHeight = 50;
            ListView.HeightRequest = Cars.Sum(g => g.Count()) * ListView.RowHeight + Cars.Count() * ListView.RowHeight;

            base.OnAppearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConfirmPage(TestDrive));
        }
    }
}
