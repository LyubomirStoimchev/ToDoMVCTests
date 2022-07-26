using System.Linq;
using NUnit.Framework;
using SpecFlowProject1.Configurations;
using SpecFlowProject1.Drivers;
using SpecFlowProject1.PageObjects;
using TechTalk.SpecFlow;

[Binding]
public class ToDoStepDefinitions
{
    private readonly ScenarioContext _ctx;
    private readonly TodoPageObject _todoPageObject;

    public ToDoStepDefinitions(ScenarioContext ctx, TestConfiguration testConfiguration)
    {
        _ctx = ctx;
        _todoPageObject = new TodoPageObject(testConfiguration);
    }

    [Given(@"Home page is open")]
    public void Step_NavigateToSite()
    {
        _todoPageObject.GoToPage();
    }

    [When(@"go to ""([^""]+)"" link")]
    public void Step_GoToSelectLeaguesPopupAsian(string linkName)
    {
        _todoPageObject.GoToLink(linkName);
    }


    [Then(@"URL ""([^""]+)"" is opened")]
    public void Step_VerifyOpenedUrl(string url)
    {
        Assert.AreEqual(
            url,
            Driver.WebDriver.Url);
    }

    [When(@"add items successfully:")]
    [Then(@"add items successfully:")]
    public void Step_AddItemsSuccessfully(Table table)
    {
        var tableRowName = "items";

        Assert.IsTrue(
            table.Rows.Any(r => r.ContainsKey(tableRowName)),
            $@"Table must contain column with header ""{tableRowName}""");

        var items = table.Rows
            .Select(r => r[tableRowName])
            .ToList();

        foreach (var item in items) Step_AddItem(item);

        CollectionAssert.IsSupersetOf(
            _todoPageObject.GetAllAddedItems(),
            items);
    }

    [Then(@"verify items are:")]
    public void Step_VerifyItems(Table table)
    {
        var tableRowName = "items";

        Assert.IsTrue(
            table.Rows.Any(r => r.ContainsKey(tableRowName)),
            $@"Table must contain column with header ""{tableRowName}""");

        var items = table.Rows
            .Select(r => r[tableRowName])
            .ToList();

        CollectionAssert.AreEquivalent(
            _todoPageObject.GetAllAddedItems(),
            items);
    }

    [When(@"go to footer tab ""(.*)""")]
    public void Step_GoToFooterLink(string link)
    {
        _todoPageObject.GoToFooterLink(link);
    }

    [Then(@"verify footer tab ""(.*)"" is active")]
    public void Step_VerifyFooterLinkIsActive(string link)
    {
        Assert.IsTrue(_todoPageObject.IsFooterLinkSelected(link));
    }

    [When(@"add ""(.*)"" item")]
    public void Step_AddItem(string item)
    {
        _todoPageObject.AddItem(item);
    }

    [Then(@"item ""([^""]+)"" is (added|removed)")]
    public void Step_VerifyItemState(string item, string state)
    {
        Assert.AreEqual(
            state.Equals("added"),
            _todoPageObject.IsItemExists(item),
            $@"Item ""{item}"" was not ""{state}""");
    }

    [When(@"remove ""([^""]+)"" item")]
    public void Step_RemoveItem(string item)
    {
        _todoPageObject.Remove(item);
    }

    [When(@"select item ""(.*)""")]
    public void Step_SelectItem(string item)
    {
        _todoPageObject.CheckItem(item);
    }

    [Then(@"item ""(.*)"" is (checked|unchecked)")]
    public void Step_VerifyItemCheckState(string item, string expectedCheckState)
    {
        Assert.AreEqual(
            expectedCheckState.Equals("checked"),
            _todoPageObject.IsItemChecked(item),
            $@"Item ""{item}"" is not ""{expectedCheckState}""");
    }

    [Then(@"item ""(.*)"" text style is ""(.*)""")]
    public void Step_VerifyTextStyle(string item, string expectedTextStyle)
    {
        var actualTextStyle = _todoPageObject.GetItemTextStyle(item);

        Assert.IsFalse(
            string.IsNullOrEmpty(actualTextStyle),
            "Could not get item text style");

        Assert.AreEqual(
            expectedTextStyle,
            actualTextStyle,
            $@"Item ""{item}"" text style was expected to be ""{expectedTextStyle}"", but is ""{actualTextStyle}""");
    }

    [Then(@"item list (is|is not) empty")]
    public void Step_VerifyItemListEmptyState(string expectedState)
    {
        var itemsCount = _todoPageObject.GetItemCount();
        Assert.AreEqual(
            expectedState.Equals("is"),
            itemsCount == 0,
            $@"List capacity was ""{itemsCount}""");
    }

    [Then(@"item count is ""(\d+)""")]
    public void Step_VerifyItemCount(int expectedCount)
    {
        Assert.AreEqual(
            expectedCount,
            _todoPageObject.GetItemCount());
    }
}