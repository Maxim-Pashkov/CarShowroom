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

            MessagingCenter.Subscribe<Car>(this, "SelectCarFromList", (Car) =>
            {
                TabbedPage tb = (TabbedPage)Parent;
                tb.CurrentPage = tb.Children.First(page => page == this);

                if(ListView.ItemsSource != null)
                {
                    IEnumerable<Grouping<string, Car>> CarsInList = ListView.ItemsSource.Cast<Grouping<string, Car>>();
                    Car selectedCar = CarsInList.SelectMany(g => g.ToList()).FirstOrDefault(c => c.Id == Car.Id);
                    ListView.SelectedItem = selectedCar;
                }                
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
