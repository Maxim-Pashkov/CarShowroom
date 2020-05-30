using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom
{
    public class CarShowroomRepository
    {
        SQLiteConnection database;
        public CarShowroomRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<TestDrive>();
            database.CreateTable<Car>();
        }

        public IEnumerable<Car> GetCars()
        {
            return database.Table<Car>().ToList();
        }

        public IEnumerable<TestDrive> GetTestDrives()
        {
            return database.Table<TestDrive>().ToList();
        }

        public Car GetCar(int id)
        {
            return database.Get<Car>(id);
        }

        public TestDrive GetTestDrive(int id)
        {
            return database.Get<TestDrive>(id);
        }

        public int DeleteCar(int id)
        {
            return database.Delete<Car>(id);
        }

        public int DeleteTestDrive(int id)
        {
            return database.Delete<TestDrive>(id);
        }

        public int SaveCar(Car item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }

        public int SaveTestDrive(TestDrive item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }

        public int GetLastInsertedId()
        {
            SQLiteCommand Command = new SQLiteCommand(database);
            Command.CommandText = "select last_insert_rowid()";
            return Command.ExecuteScalar<int>();
        }

    }
}
