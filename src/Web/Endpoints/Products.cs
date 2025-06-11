using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Application.Products.Commands.CreateProduct;
using KuyumcuStokTakip.Application.Products.Commands.DeleteProduct;
using KuyumcuStokTakip.Application.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetProducts)
            .MapPost(CreateProduct)
            .MapDelete(DeleteProduct, "{id}");
    }

    public async Task<Ok<PaginatedList<ProductDto>>> GetProducts(ISender sender, [AsParameters] GetProductsQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateProduct(ISender sender, CreateProductCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Products)}/{id}", id);
    }

    public async Task<NoContent> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return TypedResults.NoContent();
    }
}
