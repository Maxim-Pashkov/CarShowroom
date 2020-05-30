using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CarShowroom
{
	public class ConfirmPage : ContentPage
	{
        private TestDrive TestDrive { get; set; }

        public ConfirmPage() : this(new TestDrive()) { }

		public ConfirmPage (TestDrive td)
		{
            TestDrive = td;

            Title = "Подтверждение записи на тест-драйв";

            Button button = new Button
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Подтвердить",
            };
            button.Clicked += Button_Clicked;

            Content = new StackLayout {                
                Children = {
                    new StackLayout
                    {
                        Padding = new Thickness(15, 10),
                        Orientation = StackOrientation.Horizontal,
                        IsVisible = !string.IsNullOrEmpty(TestDrive.FullName),
                        Children =
                        {
                            new Label {FontAttributes= FontAttributes.Bold, Text = "ФИО:"},
                            new Label {Text = TestDrive.FullName},
                        }
                    },
                    new StackLayout
                    {
                        Padding = new Thickness(15, 10),
                        Orientation = StackOrientation.Horizontal,
                        IsVisible = !string.IsNullOrEmpty(TestDrive.PhoneNumber),
                        Children =
                        {
                            new Label {FontAttributes= FontAttributes.Bold, Text = "Телефонный номер:"},
                            new Label {Text = TestDrive.PhoneNumber},
                        }
                    },
                    new StackLayout
                    {
                        Padding = new Thickness(15, 10),
                        Orientation = StackOrientation.Horizontal,
                        IsVisible = !string.IsNullOrEmpty(TestDrive.Email),
                        Children =
                        {
                            new Label {FontAttributes= FontAttributes.Bold, Text = "Электронный адрес:"},
                            new Label {Text = TestDrive.Email},
                        }
                    },
                    new StackLayout
                    {
                        Padding = new Thickness(15, 10),
                        Orientation = StackOrientation.Horizontal,
                        IsVisible = !string.IsNullOrEmpty(TestDrive.Showroom),
                        Children =
                        {
                            new Label {FontAttributes= FontAttributes.Bold, Text = "Адрес салона:"},
                            new Label {Text = TestDrive.Showroom},
                        }
                    },
                    new StackLayout
                    {
                        Padding = new Thickness(15, 10),
                        Orientation = StackOrientation.Horizontal,
                        IsVisible = TestDrive.Car != null,
                        Children =
                        {
                            new Label {FontAttributes= FontAttributes.Bold, Text = "Автомобиль:"},
                            new Label {Text = TestDrive.Car?.MainInfo ?? string.Empty},
                        }
                    },
                    new StackLayout
                    {
                        Padding = new Thickness(15, 10),
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label {FontAttributes= FontAttributes.Bold, Text = "Дата:"},
                            new Label {Text = TestDrive.DateTime.ToString("dd.MM.yyyy HH:mm")},
                        }
                    },
                    button,
                }
			};
		}

        async private void Button_Clicked(object sender, EventArgs e)
        {            
            await DisplayAlert("Уведомление", "Ваша запись оформлена!", "Ок");           
            MessagingCenter.Send<Page>(this, "TestDriveSaved");
            await Navigation.PopAsync();
        }
    }
}