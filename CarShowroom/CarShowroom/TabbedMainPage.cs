using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CarShowroom
{
	public class TabbedMainPage : TabbedPage
	{
		public TabbedMainPage ()
		{
            Title = "Автосалон";

            Children.Add(new SearchPage());
            Children.Add(new MainPage());           
		}
	}
}