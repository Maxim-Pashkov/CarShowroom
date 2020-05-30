using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace CarShowroom
{
    [Table("Cars")]
    public class Car
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Brand { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }


        public int Year { get; set; }

        [MaxLength(100)]
        public string Photo { get; set; }

        [Ignore]
        public ImageSource PhotoSource { get => ImageSource.FromResource("CarShowroom.images." + Photo, typeof(Car).GetTypeInfo().Assembly); }

        public int HorsePower { get; set; }

        public double Price { get; set; }

        [Ignore]
        public string MainInfo { get => string.Format("{0} {1}({2} л.с. {3} г.в.)", Brand, Model, HorsePower, Year); }

        [Ignore]
        public string PriceInfo { get => string.Format("Стоимость: {0:0.##} руб.", Price); }
    }
}
