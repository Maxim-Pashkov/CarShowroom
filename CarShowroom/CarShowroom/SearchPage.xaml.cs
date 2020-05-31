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
		public SearchPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            SearchView_Search(string.Empty);
            
            base.OnAppearing();
        }

        private void SearchView_Search(string text)
        {
            ListView.ItemsSource = App.Database.GetCars().Where(Car => string.IsNullOrEmpty(text) || Car.Brand.Contains(text) || Car.Model.Contains(text)).ToList();
        }
    }
}