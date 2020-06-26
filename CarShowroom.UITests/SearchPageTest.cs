using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CarShowroom.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class SearchPageTest
    {
        IApp app;
        Platform platform;

        private Func<AppQuery, AppQuery> ButtonGoToTestDrivePage = c => c.Marked("ButtonGoToTestDrivePage").Index(0);  

        public SearchPageTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        //[Test]
        public void Repl()
        {
            app.Repl();
        }

        [Test]
        public void ButtonGoToTestDrivePageIsDisabledDefault()
        {
            AppResult[] button = app.Query(ButtonGoToTestDrivePage);

            Assert.IsFalse(button.First().Enabled);
        }

        [Test]
        public void ButtonGoToTestDrivePageIsEnabledAfterSelectCar()
        {
            Func<AppQuery, AppQuery> CarItem = c => c.Marked("CarItem").Index(0);

            app.ScrollDownTo(CarItem);
            app.Screenshot("Scrolling to car row");

            app.Tap(CarItem);
            app.Screenshot("Car item click");

            AppResult[] button = app.Query(ButtonGoToTestDrivePage);

            Assert.IsTrue(button.First().Enabled);
        }

        [Test]
        public void GoToTestDrivePage()
        {
            Func<AppQuery, AppQuery> CarItem = c => c.Marked("CarItem").Index(0);

            app.ScrollDownTo(CarItem);
            app.Screenshot("Scrolling to car row");

            app.Tap(CarItem);
            app.Screenshot("Car item click");

            app.Tap(ButtonGoToTestDrivePage);

            Assert.IsTrue(app.Query(c => c.Marked("TestDrivePageContainer").Index(0)).Any());
        }
    }
}
