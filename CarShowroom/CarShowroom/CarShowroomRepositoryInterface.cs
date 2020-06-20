using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom
{
    public interface CarShowroomRepositoryInterface
    {
        IEnumerable<Car> GetCars();

        IEnumerable<TestDrive> GetTestDrives();

        Car GetCar(int id);

        TestDrive GetTestDrive(int id);

        int DeleteCar(int id);

        int DeleteTestDrive(int id);

        int SaveCar(Car item);

        int SaveTestDrive(TestDrive item);

        int GetLastInsertedId();
    }
}
