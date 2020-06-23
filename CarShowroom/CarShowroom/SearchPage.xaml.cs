using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarShowroom
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        public bool CarIsSelected { get; set; }

		public SearchPage ()
		{           
            BindingContext = this;
			InitializeComponent ();

            MessagingCenter.Subscribe<Page>(this, "GoToSearchPage", (source) =>
            {
                TabbedPage tb = (TabbedPage)Parent;
                tb.CurrentPage = tb.Children.First(page => page == this);
            });
        }

        protected override void OnAppearing()
        {
            SearchView_Search(string.Empty);
            
            base.OnAppearing();
        }

        private void SearchView_Search(string text)
        {
            ListView.ItemsSource = App.Database.GetCars().Where(Car => string.IsNullOrEmpty(text) || Car.Brand.Contains(text) || Car.Model.Contains(text)).ToList();
            ListView.SelectedItem = null;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CarIsSelected = ((ListView)sender).SelectedItem != null;
            OnPropertyChanged("CarIsSelected");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Car)ListView.SelectedItem, "SelectCarFromList");
            ListView.SelectedItem = null;
        }
    }
}