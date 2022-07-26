using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowProject1.Drivers;

namespace SpecFlowProject1;

public static class WaitUtils
{
    public static IWebDriver WaitVisible(this IWebDriver driver, By elementLocator, int waitTime = 20)
    {
        new WebDriverWait(Driver.WebDriver, TimeSpan.FromSeconds(waitTime))
            .Until(ExpectedConditions.ElementIsVisible(elementLocator));

        return driver;
    }

    public static IWebDriver WaitExists(this IWebDriver driver, By elementLocator, int waitTime = 5)
    {
        new WebDriverWait(Driver.WebDriver, TimeSpan.FromSeconds(waitTime))
            .Until(ExpectedConditions.ElementExists(elementLocator));

        return driver;
    }

    public static bool IsElementExists(By elementLocator)
    {
        try
        {
            Driver.WebDriver.FindElement(elementLocator);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}