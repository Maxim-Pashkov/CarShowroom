using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CarShowroom
{
    public delegate void SearchViewEventHandler(string text);

    public partial class SearchView : ContentView
	{
        public event SearchViewEventHandler Search;

        public SearchView ()
		{
            Entry Entry = new Entry {
                Placeholder = "Введите текст",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                AutomationId = "SearchEntry"
            };

            Button Button = new Button { Text = "Искать", AutomationId = "SearchButton" };

            Button.Clicked += (sender, e) => Search?.Invoke(Entry.Text); ;

            BindingContext = this;

            Content = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                Children = {
                    Entry,
                    Button,
				}
			};
		}
    }
}