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
	public partial class AboutUsPage : ContentPage
	{
		public AboutUsPage ()
		{
			InitializeComponent ();

            Image.Source = ImageSource.FromResource("CarShowroom.images.63254.pv6zoo.1280.jpg");
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:+7 927 1111111"));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            MessagingCenter.Send<Page>(this, "GoToSearchPage");
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            MessagingCenter.Send<Page>(this, "GoToTestDrivePage");
        }
    }
}