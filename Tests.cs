using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            var configurationBuilder = new ConfigurationBuilder()
            
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",reloadOnChange:true,optional:false)
                .Build();
           
           
            var playwright = await Playwright.CreateAsync();
            Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = configurationBuilder.GetValue("Headless",defaultValue:false),
                Channel = configurationBuilder.GetValue("Channel", defaultValue: "chrome"),
                Timeout = configurationBuilder.GetValue("Timeout", defaultValue: 0),

            });
        }

        [SetUp]
        public async Task SetUp() => Page = await Browser!.NewPageAsync();

        [TearDown]
        public async Task TearDown() => await Page.CloseAsync();

        [OneTimeTearDown]
        public async Task OneTimeTeardown() => await Browser!.CloseAsync();

        [Test]
        public async Task GoToInitialTest()
        {
            await Page.GotoAsync("https://www.google.co.uk");
            var title = await Page.TitleAsync();

            Assert.That(title, Is.EqualTo("Google"));
        }

        [Test]
        public async Task SearchForSomething ()
        {
            await Page.GotoAsync("https://www.google.co.uk");
            await Page.FillAsync(".gLFyf", "Playwright.net docs");
            await Page.ClickAsync(".gNO89b");
            Thread.Sleep(4000);
        }

        
    }
}