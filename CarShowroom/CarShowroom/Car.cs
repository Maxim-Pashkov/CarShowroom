using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace CarShowroom
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public string Photo { get; set; }
        public ImageSource PhotoSource { get => ImageSource.FromResource("CarShowroom.images." + Photo, typeof(Car).GetTypeInfo().Assembly); }

        public int HorsePower { get; set; }

        public double Price { get; set; }

        public string MainInfo { get => string.Format("{0} {1}({2} л.с. {3} г.в.)", Brand, Model, HorsePower, Year); }

        public string PriceInfo { get => string.Format("Стоимость: {0:0.##} руб.", Price); }
    }
}
