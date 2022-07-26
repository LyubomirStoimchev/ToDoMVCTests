using System.IO;
using System.Reflection;
using System.Text.Json;
using BoDi;
using SpecFlowProject1.Configurations;
using SpecFlowProject1.Drivers;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Hooks;

[Binding]
public class Hooks
{
    private readonly IObjectContainer objectContainer;
    private static FeatureContext testRunContext;
    private readonly ScenarioContext scenarioContext;
    private static TestConfiguration _testConfiguration;
    
    public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioCtx, FeatureContext testRunCtx)
    {
        this.objectContainer = objectContainer;
        scenarioContext = scenarioCtx;
        testRunContext = testRunCtx;

        this.objectContainer.RegisterInstanceAs(_testConfiguration);
    }

    [BeforeFeature]
    public static void LoadConfigurations()
    {
        var configFilePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "Resources",
            "testConfiguration.json");

        if (!File.Exists(configFilePath)) throw new FileNotFoundException("Could not find test configuration.");

        _testConfiguration = JsonSerializer.Deserialize<TestConfiguration>(File.ReadAllText(configFilePath));
    }

    [BeforeScenario]
    public void InitializeDriver()
    {
        Driver.Init();
    }

    [AfterScenario]
    public void CloseDriver()
    {
        Driver.WebDriver.Quit();
        Driver.WebDriver.Dispose();
    }
}