namespace KuyumcuStokTakip.Web.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class StockTransactionsStepDefinitions
{
    private readonly StockTransactionsPage _page;

    public StockTransactionsStepDefinitions(StockTransactionsPage page)
    {
        _page = page;
    }

    [BeforeFeature("StockTransactions")]
    public static async Task BeforeScenario(IObjectContainer container)
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions());
        var page = await browser.NewPageAsync();
        var stockPage = new StockTransactionsPage(browser, page);
        container.RegisterInstanceAs(playwright);
        container.RegisterInstanceAs(browser);
        container.RegisterInstanceAs(stockPage);
    }

    [Given("the user is on the stock transactions page")]
    public async Task GivenTheUserIsOnTheStockTransactionsPage()
    {
        await _page.GotoAsync();
    }

    [When("the user creates a stock entry")]
    public async Task WhenTheUserCreatesAStockEntry()
    {
        await _page.ClickNewIn();
        await _page.FillInventoryProductId("1");
        await _page.FillQuantity("1");
        await _page.FillWeight("1");
        await _page.FillDescription("ui test");
        await _page.Save();
    }

    [Then("the stock entry is visible")]
    public async Task ThenTheStockEntryIsVisible()
    {
        var exists = await _page.TransactionExists("ui test");
        exists.Should().BeTrue();
    }

    [AfterFeature]
    public static async Task AfterScenario(IObjectContainer container)
    {
        var browser = container.Resolve<IBrowser>();
        var playwright = container.Resolve<IPlaywright>();
        await browser.CloseAsync();
        playwright.Dispose();
    }
}
