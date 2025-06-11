namespace KuyumcuStokTakip.Web.AcceptanceTests.Pages;

public class StockTransactionsPage : BasePage
{
    public StockTransactionsPage(IBrowser browser, IPage page)
    {
        Browser = browser;
        Page = page;
    }

    public override string PagePath => $"{BaseUrl}/stock-transactions";

    public override IBrowser Browser { get; }

    public override IPage Page { get; set; }

    public Task ClickNewIn() => Page.GetByText("Giriş Yap").ClickAsync();
    public Task FillInventoryProductId(string value) => Page.Locator("input[ngmodel='editor.inventoryProductId']").FillAsync(value);
    public Task FillQuantity(string value) => Page.Locator("input[ngmodel='editor.quantity']").FillAsync(value);
    public Task FillWeight(string value) => Page.Locator("input[ngmodel='editor.weight']").FillAsync(value);
    public Task FillDescription(string value) => Page.Locator("input[ngmodel='editor.description']").FillAsync(value);
    public Task Save() => Page.GetByText("Save").ClickAsync();
    public Task<bool> TransactionExists(string description) => Page.Locator($"text={description}").IsVisibleAsync();
}
