using Microsoft.Playwright;
using NUnit.Framework.Legacy;

namespace PlaywrightNunitKT
{
    public class Tests
    {
        public IBrowser? Browser { get; set; }
        public IPage Page { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var playwright =await Playwright.CreateAsync();
            Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
           
        }
        [SetUp]
        public async Task SetUp()
        {
            Page = await Browser.NewPageAsync();
        }
        [TearDown]
        public  async Task TearDown()
        {
             await Page.CloseAsync();
        }

        

        [OneTimeTearDown]
        public async Task OneTimeTeardown()
        {
            await Browser.CloseAsync();
        }

        [Test]

        public async Task GoToInitialTest ()
        {
            await Page.GotoAsync("https://www.google.co.uk");
            var title = await Page.TitleAsync();

            ClassicAssert.AreEqual("Google", title);
        }

        
    }
}