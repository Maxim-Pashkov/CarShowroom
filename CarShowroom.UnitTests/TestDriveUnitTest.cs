using System;
using NUnit.Framework;
using CarShowroom;

namespace CarShowroom.UnitTests
{
    [TestFixture]
    public class TestDriveUnitTest
    {
        CarShowroomRepositoryMock Database;

        public TestDriveUnitTest()
        {
            Database = new CarShowroomRepositoryMock();
        }

        [Test]
        public void TestFullNameByFirstName()
        {
            TestDrive td = new TestDrive(Database);

            td.FirstName = "1";

            Assert.AreEqual(td.FullName, "1");
        }

        [Test]
        public void TestFullNameByFirstNameAndSecondName()
        {
            TestDrive td = new TestDrive(Database);

            td.FirstName = "1";
            td.SecondName = "2";

            Assert.AreEqual(td.FullName, "2 1");
        }

        [Test]
        public void TestFullNameByFirstNameAndThirdName()
        {
            TestDrive td = new TestDrive(Database);

            td.FirstName = "1";
            td.ThirdName = "3";

            Assert.AreEqual(td.FullName, "1 3");
        }

        [Test]
        public void TestFullNameBySecondNameAndThirdName()
        {
            TestDrive td = new TestDrive(Database);

            td.SecondName = "2";
            td.ThirdName = "3";

            Assert.AreEqual(td.FullName, "2 3");
        }

        [Test]
        public void TestFullNameByFirstNameAndSecondNameAndThirdName()
        {
            TestDrive td = new TestDrive(Database);

            td.FirstName = "1";
            td.SecondName = "2";
            td.ThirdName = "3";

            Assert.AreEqual(td.FullName, "2 1 3");
        }

        [Test]
        public void TestDateDefaultValue()
        {
            TestDrive td = new TestDrive(Database);

            Assert.AreEqual(td.Date, DateTime.Today);
        }

        [Test]
        public void TestTimeDefaultValue()
        {
            TestDrive td = new TestDrive(Database);

            Assert.AreEqual(td.Time, new TimeSpan(12, 0, 0));
        }

        [Test]
        public void TestDateTimeByDate()
        {
            TestDrive td = new TestDrive(Database);

            td.Date = new DateTime(2019, 12, 10);

            Assert.AreEqual(td.DateTime, new DateTime(2019, 12, 10, 12, 0, 0));
        }

        [Test]
        public void TestDateTimeByTime()
        {
            TestDrive td = new TestDrive(Database);

            td.Time = new TimeSpan(10, 10, 10);

            Assert.AreEqual(td.DateTime, DateTime.Today.AddHours(10).AddMinutes(10));
        }

        [Test]
        public void TestDateTimeByDateAndTime()
        {
            TestDrive td = new TestDrive(Database);

            td.Date = new DateTime(2019, 12, 10);

            td.Time = new TimeSpan(10, 10, 10);

            Assert.AreEqual(td.DateTime, new DateTime(2019, 12, 10, 10, 10, 0));
        }

        [Test]
        public void TestDateTimeByTimeAndDate()
        {
            TestDrive td = new TestDrive(Database);

            td.Time = new TimeSpan(10, 10, 10);

            td.Date = new DateTime(2019, 12, 10);            

            Assert.AreEqual(td.DateTime, new DateTime(2019, 12, 10, 10, 10, 0));
        }
    }
}
