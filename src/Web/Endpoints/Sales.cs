using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Application.Sales.Commands.CreateSale;
using KuyumcuStokTakip.Application.Sales.Queries.GetSales;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class Sales : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetSales)
            .MapPost(CreateSale);
    }

    public async Task<Ok<PaginatedList<SaleDto>>> GetSales(ISender sender, [AsParameters] GetSalesQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateSale(ISender sender, CreateSaleCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Sales)}/{id}", id);
    }
}
