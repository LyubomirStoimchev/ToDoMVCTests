using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SpecFlowProject1.Configurations;
using SpecFlowProject1.Drivers;

namespace SpecFlowProject1.PageObjects
{
    public class TodoPageObject : PageObject
    {
        private readonly string _footerFilterElementLocator = @"//*[@class=""filters""]//a[normalize-space()=""{0}""]";
        private readonly string _inputElementLocator = "new-todo";
        private readonly string _linkElementLocator = @"//a[normalize-space()=""{0}""]";
        private readonly string _listItemBlockElementWithTextLocator = @"//li[contains(@class,""todo"")][//label[normalize-space()=""{0}""]]";
        private readonly string _listItemCheckElementLocator = @"//li[contains(@class,""todo"")]//label[normalize-space()=""{0}""]/../*[@class=""toggle""]";
        private readonly string _listItemElementsLocator = @"//li[contains(@class,""todo"")]//label";
        private readonly string _listItemElementWithTextLocator = @"//li[contains(@class,""todo"")]//label[normalize-space()=""{0}""]";
        private readonly string _listItemRemoveElementLocator = @"//li[@class=""todo""]//label[normalize-space()=""{0}""]/../*[@class=""destroy""]";

        public TodoPageObject(TestConfiguration testConfiguration) : base(testConfiguration)
        {
        }

        public void GoToPage()
        {
            Navigate(_testConfiguration.SiteUrl);
        }

        public void GoToLink(string linkText)
        {
            var elementLocator = By.XPath(string.Format(_linkElementLocator, linkText));

            Driver.WebDriver
                .WaitVisible(elementLocator, _testConfiguration.Timeouts.Visible)
                .FindElement(elementLocator)
                .Click();
        }

        public void GoToFooterLink(string linkText)
        {
            var elementLocator = By.XPath(string.Format(_footerFilterElementLocator, linkText));

            Driver.WebDriver
                .WaitVisible(elementLocator, _testConfiguration.Timeouts.Visible)
                .FindElement(elementLocator)
                .Click();
        }

        public bool IsFooterLinkSelected(string linkText)
        {
            var elementLocator = By.XPath(string.Format(_footerFilterElementLocator, linkText));

            return Driver.WebDriver
                .WaitVisible(elementLocator, _testConfiguration.Timeouts.Visible)
                .FindElement(elementLocator)
                .GetAttribute("class")
                .Equals("selected");
        }

        public void AddItem(string itemText)
        {
            var element = Driver.WebDriver
                .WaitVisible(By.ClassName(_inputElementLocator), _testConfiguration.Timeouts.Visible)
                .FindElement(By.ClassName(_inputElementLocator));

            element.SendKeys(itemText);
            element.SendKeys(Keys.Enter);
        }

        public int GetItemCount()
        {
            return Driver.WebDriver
                .FindElements(By.XPath(_listItemElementsLocator))
                .Count;
        }

        public void Remove(string itemText)
        {
            var elementLocator = By.XPath(string.Format(_listItemRemoveElementLocator, itemText));

            ((IJavaScriptExecutor) Driver.WebDriver).ExecuteScript(
                "arguments[0].click();",
                Driver.WebDriver
                    .WaitExists(elementLocator, _testConfiguration.Timeouts.Exist)
                    .FindElement(elementLocator));
        }

        public void CheckItem(string itemText)
        {
            var elementLocator = By.XPath(string.Format(_listItemCheckElementLocator, itemText));

            Driver.WebDriver
                .WaitExists(elementLocator, _testConfiguration.Timeouts.Exist)
                .FindElement(elementLocator)
                .Click();
        }

        public List<string> GetAllAddedItems()
        {
            var elementLocator = By.XPath(_listItemElementsLocator);

            return Driver.WebDriver
                .WaitVisible(elementLocator, _testConfiguration.Timeouts.Visible)
                .FindElements(elementLocator)
                .Select(e => e.Text)
                .ToList();
        }

        public bool IsItemExists(string itemText)
        {
            return WaitUtils.IsElementExists(By.XPath(string.Format(_listItemElementWithTextLocator, itemText)));
        }

        public bool IsItemChecked(string itemText)
        {
            var elementLocator = By.XPath(string.Format(_listItemBlockElementWithTextLocator, itemText));

            return Driver.WebDriver
                .WaitVisible(elementLocator, _testConfiguration.Timeouts.Visible)
                .FindElement(elementLocator)
                .GetAttribute("class")
                .Contains("completed");
        }

        public string GetItemTextStyle(string itemText)
        {
            var elementLocator = By.XPath(string.Format(_listItemElementWithTextLocator, itemText));

            return Driver.WebDriver
                .WaitVisible(elementLocator, _testConfiguration.Timeouts.Visible)
                .FindElement(elementLocator)
                .GetCssValue("text-decoration-line");
        }
    }
}