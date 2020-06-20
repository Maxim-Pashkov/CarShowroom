using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CarShowroom
{
    public class CarShowroomRepositoryMock : CarShowroomRepositoryInterface
    {
        public IEnumerable<Car> GetCars()
        {
            return new List<Car>{};
        }

        public IEnumerable<TestDrive> GetTestDrives()
        {
            return new List<TestDrive>{};
        }

        public Car GetCar(int id)
        {
            return null;
        }

        public TestDrive GetTestDrive(int id)
        {
            return null;
        }

        public int DeleteCar(int id)
        {
            return id;
        }

        public int DeleteTestDrive(int id)
        {
            return id;
        }

        public int SaveCar(Car item)
        {
            return item.Id;
        }

        public int SaveTestDrive(TestDrive item)
        {
            return item.Id;
        }

        public int GetLastInsertedId()
        {
            return 0;
        }
    }
}
