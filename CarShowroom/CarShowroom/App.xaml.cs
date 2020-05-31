using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CarShowroom
{
    public partial class App : Application
    {
        public const string DBFILENAME = "CarShowroom.db";
        public static CarShowroomRepository database;
        public static CarShowroomRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new CarShowroomRepository(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBFILENAME));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            if(Database.GetCars().Count() == 0)
            {
                Database.SaveCar(new Car { Brand = "ВАЗ", Model = "2107", Year = 2003, HorsePower = 72, Price = 80000, Photo = "vaz2107.jpg" });
                Database.SaveCar(new Car { Brand = "LADA", Model = "Granta", Year = 2015, HorsePower = 105, Price = 350000, Photo = "Granta.png" });
                Database.SaveCar(new Car { Brand = "LADA", Model = "XRAY", Year = 2018, HorsePower = 108, Price = 800000, Photo = "xray.png" });
                Database.SaveCar(new Car { Brand = "Daewoo", Model = "Nexia", Year = 2010, HorsePower = 80, Price = 320000, Photo = "daewoo_nexia.jpg" });
                Database.SaveCar(new Car { Brand = "Daewoo", Model = "Matiz", Year = 2008, HorsePower = 70, Price = 200000, Photo = "daewoo-matiz.jpg" });
            }

            MainPage = new NavigationPage(new TabbedMainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
