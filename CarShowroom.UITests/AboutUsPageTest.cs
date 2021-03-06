﻿using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CarShowroom.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class AboutUsPageTest
    {
        IApp app;
        Platform platform;

        public AboutUsPageTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            app.Tap(c => c.Marked("О компании").Index(0));
        }

        //[Test]
        public void Repl()
        {
            app.Repl();
        }

        [Test]
        public void GoToSearchPage()
        {          
            Func<AppQuery, AppQuery> ButtonGoToSearchPage = c => c.Marked("ButtonGoToSearchPage").Index(0);
            app.ScrollDownTo(ButtonGoToSearchPage);
            app.Screenshot("Scrolling to button");

            app.Tap(ButtonGoToSearchPage);

            Assert.IsTrue(app.Query(c => c.Marked("SearchPageContainer")).Any());
        }

        [Test]
        public void GoToTestDrivePage()
        {
            Func<AppQuery, AppQuery> ButtonGoToTestDrivePage = c => c.Marked("ButtonGoToTestDrivePage").Index(0);
            app.ScrollDownTo(ButtonGoToTestDrivePage);
            app.Screenshot("Scrolling to button");

            app.Tap(ButtonGoToTestDrivePage);

            Assert.IsTrue(app.Query(c => c.Marked("TestDrivePageContainer")).Any());
        }
    }
}
