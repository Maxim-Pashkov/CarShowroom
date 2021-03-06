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
    public class TestDrivePageTest
    {
        IApp app;
        Platform platform;

        public TestDrivePageTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            app.Tap(c => c.Marked("Запись на тест-драйв").Index(0));
        }

        //[Test]
        public void Repl()
        {
            app.Repl();
        }

        [Test]
        public void ButtonSubmitTestDriveIsDisabled()
        {
            app.ScrollTo("ButtonSubmitTestDrive", "TestDrivePageContainer");

            app.Screenshot("Scrolling to button");

            AppResult button = app.Query("ButtonSubmitTestDrive").First();

            Assert.IsFalse(button.Enabled);
        }

        [Test]
        public void ButtonSubmitTestDriveIsEnabledAfterToggleSwitchAccept()
        {
            app.ScrollTo("ButtonSubmitTestDrive", "TestDrivePageContainer");

            app.Screenshot("Scrolling to button");

            app.Tap("SwitchAccept");

            app.Screenshot("Switch tap");

            AppResult button = app.Query("ButtonSubmitTestDrive").First();

            Assert.IsTrue(button.Enabled);
        }

        [Test]
        public void ConfirmPageIsVisible()
        {
            app.ScrollTo("ButtonSubmitTestDrive", "TestDrivePageContainer");

            app.Screenshot("Scrolling to button");

            app.Tap("SwitchAccept");

            app.Screenshot("Switch tap");

            app.Tap("ButtonSubmitTestDrive");

            AppResult[] ConfirmPage = app.Query("ConfirmPageContainer");

            Assert.IsTrue(ConfirmPage.Any());
        }

        [Test]
        public void RevertToTestDrivePageAfterConfirm()
        {
            app.ScrollTo("ButtonSubmitTestDrive", "TestDrivePageContainer");

            app.Screenshot("Scrolling to button");

            app.Tap("SwitchAccept");

            app.Screenshot("Switch tap");

            app.Tap("ButtonSubmitTestDrive");

            app.Screenshot("Button submit was clicked");

            app.Tap("ButtonConfirmSubmit");

            app.Screenshot("Button confirm was clicked");

            app.Tap("Ок");

            Assert.IsTrue(app.Query(c => c.Marked("TestDrivePageContainer")).Any());
        }
    }
}
