using SpecFlowProject1.Configurations;
using SpecFlowProject1.Drivers;

namespace SpecFlowProject1.PageObjects;

public abstract class PageObject
{
    protected readonly TestConfiguration _testConfiguration;

    protected PageObject(TestConfiguration testConfiguration)
    {
        _testConfiguration = testConfiguration;
    }

    protected void Navigate(string url)
    {
        Driver.WebDriver.Navigate().GoToUrl(url);
    }
}