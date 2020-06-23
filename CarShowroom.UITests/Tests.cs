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
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        //[Test]
        public void WelcomeTextIsDisplayed()
        {
            app.Repl();
            /*AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());*/
        }

        [Test]
        public void GoToSearchPageFromAboutUs()
        {
            app.Tap(c => c.Marked("О компании").Index(0));

            Func<AppQuery, AppQuery> ButtonGoToSearchPage = c => c.Marked("ButtonGoToSearchPage").Index(0);
            app.ScrollDownTo(ButtonGoToSearchPage);
            app.Tap(ButtonGoToSearchPage);

            Assert.IsTrue(app.Query(c => c.Marked("SearchPageContainer")).Any());
        }

        [Test]
        public void GoToTestDrivePageFromAboutUs()
        {
            app.Tap(c => c.Marked("О компании").Index(0));

            Func<AppQuery, AppQuery> ButtonGoToTestDrivePage = c => c.Marked("ButtonGoToTestDrivePage").Index(0);
            app.ScrollDownTo(ButtonGoToTestDrivePage);
            app.Tap(ButtonGoToTestDrivePage);

            Assert.IsTrue(app.Query(c => c.Marked("TestDrivePageContainer")).Any());
        }
    }
}
