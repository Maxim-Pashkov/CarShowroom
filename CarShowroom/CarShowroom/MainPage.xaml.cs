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

            Cars = new ObservableCollection<Car>
            {
                new Car { Brand = "ВАЗ", Model = "2107", Year = 2003, HorsePower = 72, Price = 80000, Photo = "vaz2107.jpg" },
                new Car { Brand = "LADA", Model = "Granta", Year = 2015, HorsePower = 105, Price = 350000, Photo = "Granta.png" },
                new Car { Brand = "LADA", Model = "XRAY", Year = 2018, HorsePower = 108, Price = 800000, Photo = "xray.png" },
                new Car { Brand = "Daewoo", Model = "Nexia", Year = 2010, HorsePower = 80, Price = 320000, Photo = "daewoo_nexia.jpg" },
                new Car { Brand = "Daewoo", Model = "Matiz", Year = 2008, HorsePower = 70, Price = 200000, Photo = "daewoo-matiz.jpg" },
            };

            ListViewRowHeight = 50;
            ListViewHeightRequest = Cars.Count * ListViewRowHeight;

            BindingContext = this;
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConfirmPage(TestDrive));
        }
    }
}
