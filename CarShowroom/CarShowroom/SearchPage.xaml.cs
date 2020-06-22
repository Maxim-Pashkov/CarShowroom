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
        private string lastSearched;
        public bool CarIsSelected { get; set; }

		public SearchPage ()
		{
            lastSearched = string.Empty;
            BindingContext = this;
			InitializeComponent ();            
		}

        protected override void OnAppearing()
        {
            SearchView_Search(lastSearched);
            
            base.OnAppearing();
        }

        private void SearchView_Search(string text)
        {
            ListView.ItemsSource = App.Database.GetCars().Where(Car => string.IsNullOrEmpty(text) || Car.Brand.Contains(text) || Car.Model.Contains(text)).ToList();
            ListView.SelectedItem = null;
            lastSearched = text;
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