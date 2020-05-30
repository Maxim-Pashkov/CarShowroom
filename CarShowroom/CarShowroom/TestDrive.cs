using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CarShowroom
{
    public class TestDrive
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }

        public string FullName { get => string.Join(" ", new List<string> { SecondName, FirstName, ThirdName }).Trim(); }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Car Car { get; set; }

        public string Showroom { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime Date { get => DateTime.Equals(DateTime.MinValue) ? DateTime.Today : DateTime; set
            {
                DateTime = new DateTime(value.Year, value.Month, value.Day, Time.Hours, Time.Minutes, 0);
            }
        }

        public TimeSpan Time { get => DateTime.Equals(DateTime.MinValue) ? new TimeSpan(12, 0, 0) : DateTime.TimeOfDay; set
            {
                DateTime = new DateTime(Date.Year, Date.Month, Date.Day, value.Hours, value.Minutes, 0);
            }
        }

        public bool Accepted { get; set; }
    }
}
