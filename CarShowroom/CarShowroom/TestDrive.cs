using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using SQLite;

namespace CarShowroom
{
    public class TestDrive
    {
        public TestDrive()
        {
            Date = DateTime.Today;
            Time = new TimeSpan(12, 0, 0);
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string SecondName { get; set; }

        [MaxLength(100)]
        public string ThirdName { get; set; }

        [Ignore]
        public string FullName { get => string.Join(" ", new List<string> { SecondName, FirstName, ThirdName }).Trim(); }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        public int CarId { get; set; }

        [Ignore]
        public Car Car {
            get => CarId != 0 ? App.Database.GetCar(CarId) : null;
            set => CarId = value != null ? value.Id : 0;
        }

        [MaxLength(200)]
        public string Showroom { get; set; }

        public DateTime DateTime { get; set; }

        [Ignore]
        public DateTime Date { get => DateTime.Date; set
            {
                DateTime = new DateTime(value.Year, value.Month, value.Day, Time.Hours, Time.Minutes, 0);
            }
        }

        [Ignore]
        public TimeSpan Time { get => DateTime.TimeOfDay; set
            {
                DateTime = new DateTime(Date.Year, Date.Month, Date.Day, value.Hours, value.Minutes, 0);
            }
        }

        [Ignore]
        public bool Accepted { get; set; }
    }
}
