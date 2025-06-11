namespace KuyumcuStokTakip.Web.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class StockBalanceStepDefinitions
{
    private readonly StockBalancePage _page;
    private int _initialRowCount;

    public StockBalanceStepDefinitions(StockBalancePage page)
    {
        _page = page;
    }

    [BeforeFeature("StockBalance")]
    public static async Task BeforeScenario(IObjectContainer container)
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions());
        var page = await browser.NewPageAsync();
        var balancePage = new StockBalancePage(browser, page);
        container.RegisterInstanceAs(playwright);
        container.RegisterInstanceAs(browser);
        container.RegisterInstanceAs(balancePage);
    }

    [Given("the user is on the stock balance page")]
    public async Task GivenTheUserIsOnTheStockBalancePage()
    {
        await _page.GotoAsync();
        _initialRowCount = await _page.RowCount();
    }

    [Then("the stock balance table is visible")]
    public async Task ThenTheStockBalanceTableIsVisible()
    {
        var visible = await _page.TableVisible();
        visible.Should().BeTrue();
    }

    [When("the user filters by product name")]
    public async Task WhenTheUserFiltersByProductName()
    {
        await _page.SetFilter("Altın");
        await _page.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Then("only filtered rows are shown")]
    public async Task ThenOnlyFilteredRowsAreShown()
    {
        var rows = await _page.RowCount();
        rows.Should().BeLessOrEqualTo(_initialRowCount);
        var texts = await _page.Page.Locator("tbody tr td:first-child").AllTextContentsAsync();
        texts.Should().OnlyContain(t => t.Contains("Altın", StringComparison.OrdinalIgnoreCase));
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
