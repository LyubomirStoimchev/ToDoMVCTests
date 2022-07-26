using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SpecFlowProject1.Drivers
{
    public static class Driver
    {
        private static readonly AsyncLocal<IWebDriver> _asyncWebDriver = new();
        
        public static IWebDriver WebDriver
        {
            get => _asyncWebDriver.Value;

            private set => _asyncWebDriver.Value = value;
        }

        public static void Init()
        {
            var option = new ChromeOptions();
            //option.AddArgument("--headless");
            WebDriver = new ChromeDriver(option);
        }
    }
}