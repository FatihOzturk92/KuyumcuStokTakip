namespace KuyumcuStokTakip.Web.AcceptanceTests.Pages;

public class StockBalancePage : BasePage
{
    public StockBalancePage(IBrowser browser, IPage page)
    {
        Browser = browser;
        Page = page;
    }

    public override string PagePath => $"{BaseUrl}/stock-balance";

    public override IBrowser Browser { get; }

    public override IPage Page { get; set; }

    public Task<bool> TableVisible() => Page.Locator("table").IsVisibleAsync();

    public Task SetFilter(string value) => Page.Locator("input[placeholder='Filter']").FillAsync(value);

    public Task<int> RowCount() => Page.Locator("tbody tr").CountAsync();
}
