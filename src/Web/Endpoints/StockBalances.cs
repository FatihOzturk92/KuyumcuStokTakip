using KuyumcuStokTakip.Application.StockBalances.Queries.GetStockBalance;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class StockBalances : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetStockBalances);
    }

    public async Task<Ok<List<StockBalanceDto>>> GetStockBalances(ISender sender, [AsParameters] GetStockBalanceQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }
}

