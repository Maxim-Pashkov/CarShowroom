using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;

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

            List<string> containerStyle = new List<string> { "container" };
            List<string> labelBigStyle = new List<string> { "label-big" };

            Content = new StackLayout {                
                Children = {
                    new StackLayout
                    {
                        StyleClass= containerStyle,                        
                        Orientation = StackOrientation.Horizontal,
                        IsVisible = !string.IsNullOrEmpty(TestDrive.FullName),
                        Children =
                        {
                            new Label {StyleClass = labelBigStyle, Text = "ФИО:", VerticalTextAlignment = TextAlignment.Center},
                            new Label {Text = TestDrive.FullName, VerticalTextAlignment = TextAlignment.Center},
                        }
                    },
                    new BoxView {StyleClass = new List<string> {"hr"}, HorizontalOptions=LayoutOptions.FillAndExpand, IsVisible=!string.IsNullOrEmpty(TestDrive.FullName)},
                    new StackLayout
                    {
                        StyleClass= containerStyle,
                        Orientation = StackOrientation.Horizontal,
                        IsVisible = !string.IsNullOrEmpty(TestDrive.PhoneNumber),
                        Children =
                        {
                            new Label {StyleClass = labelBigStyle, Text = "Телефонный номер:", VerticalTextAlignment = TextAlignment.Center},
                            new Label {Text = TestDrive.PhoneNumber, VerticalTextAlignment= TextAlignment.Center},
                        }
                    },
                    new BoxView {StyleClass = new List<string> {"hr"}, HorizontalOptions=LayoutOptions.FillAndExpand, IsVisible=!string.IsNullOrEmpty(TestDrive.PhoneNumber)},
                    new StackLayout
                    {
                        StyleClass= containerStyle,
                        Orientation = StackOrientation.Horizontal,
                        IsVisible = !string.IsNullOrEmpty(TestDrive.Email),
                        Children =
                        {
                            new Label {StyleClass = labelBigStyle, Text = "Электронный адрес:", VerticalTextAlignment = TextAlignment.Center},
                            new Label {Text = TestDrive.Email, VerticalTextAlignment = TextAlignment.Center},
                        }
                    },
                    new BoxView {StyleClass = new List<string> {"hr"}, HorizontalOptions=LayoutOptions.FillAndExpand, IsVisible=!string.IsNullOrEmpty(TestDrive.Email)},
                    new StackLayout
                    {
                        StyleClass= containerStyle,
                        IsVisible = !string.IsNullOrEmpty(TestDrive.Showroom),
                        Children =
                        {
                            new Label {StyleClass = labelBigStyle, Text = "Адрес салона"},
                            new Label {Text = TestDrive.Showroom},
                        }
                    },
                    new BoxView {StyleClass = new List<string> {"hr"}, HorizontalOptions=LayoutOptions.FillAndExpand, IsVisible=!string.IsNullOrEmpty(TestDrive.Showroom)},
                    new StackLayout
                    {
                        StyleClass= containerStyle,
                        Orientation = StackOrientation.Horizontal,
                        IsVisible = TestDrive.Car != null,
                        Children =
                        {
                            new Label {StyleClass = labelBigStyle, Text = "Автомобиль:", VerticalTextAlignment = TextAlignment.Center},
                            new Label {Text = TestDrive.Car?.MainInfo ?? string.Empty, VerticalTextAlignment = TextAlignment.Center},
                        }
                    },
                    new BoxView {StyleClass = new List<string> {"hr"}, HorizontalOptions=LayoutOptions.FillAndExpand, IsVisible=TestDrive.Car != null},
                    new StackLayout
                    {
                        StyleClass= containerStyle,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label {StyleClass = labelBigStyle, Text = "Дата:", VerticalTextAlignment = TextAlignment.Center},
                            new Label {Text = TestDrive.DateTime.ToString("dd.MM.yyyy HH:mm"), VerticalTextAlignment = TextAlignment.Center},
                        }
                    },
                    new BoxView {StyleClass = new List<string> {"hr"}, HorizontalOptions=LayoutOptions.FillAndExpand},
                    button,
                }
			};

            Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly, "CarShowroom.styles.css"));

        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            App.Database.SaveTestDrive(TestDrive);

            int id = App.Database.GetLastInsertedId();

            TestDrive td = App.Database.GetTestDrive(id);
            System.Diagnostics.Debug
                    .WriteLine("Test Drive ID: " + td.Id + " ; CarId: " + td.CarId + " ; FullName: " + td.FullName + " ;Phone:" + td.PhoneNumber + "; Email: " + td.Email + "; Showroom: " + td.Showroom);

            await DisplayAlert("Уведомление", "Ваша запись оформлена!", "Ок");           
            MessagingCenter.Send<Page>(this, "TestDriveSaved");
            await Navigation.PopAsync();
        }
    }
}