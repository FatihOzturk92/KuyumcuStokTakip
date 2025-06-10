using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Application.InventoryProducts.Commands.CreateInventoryProduct;
using KuyumcuStokTakip.Application.InventoryProducts.Commands.DeleteInventoryProduct;
using KuyumcuStokTakip.Application.InventoryProducts.Commands.UpdateInventoryProduct;
using KuyumcuStokTakip.Application.InventoryProducts.Queries.GetInventoryProducts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class InventoryProducts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetInventoryProducts)
            .MapPost(CreateInventoryProduct)
            .MapPut(UpdateInventoryProduct, "{id}")
            .MapDelete(DeleteInventoryProduct, "{id}");
    }

    public async Task<Ok<PaginatedList<InventoryProductDto>>> GetInventoryProducts(ISender sender, [AsParameters] GetInventoryProductsQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateInventoryProduct(ISender sender, CreateInventoryProductCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(InventoryProducts)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateInventoryProduct(ISender sender, int id, UpdateInventoryProductCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();
        await sender.Send(command);
        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteInventoryProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteInventoryProductCommand(id));
        return TypedResults.NoContent();
    }
}
