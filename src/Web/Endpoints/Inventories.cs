using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Application.Inventories.Commands.CreateInventory;
using KuyumcuStokTakip.Application.Inventories.Commands.DeleteInventory;
using KuyumcuStokTakip.Application.Inventories.Commands.UpdateInventory;
using KuyumcuStokTakip.Application.Inventories.Queries.GetInventories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class Inventories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetInventories)
            .MapPost(CreateInventory)
            .MapPut(UpdateInventory, "{id}")
            .MapDelete(DeleteInventory, "{id}");
    }

    public async Task<Ok<PaginatedList<InventoryDto>>> GetInventories(ISender sender, [AsParameters] GetInventoriesQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateInventory(ISender sender, CreateInventoryCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Inventories)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateInventory(ISender sender, int id, UpdateInventoryCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();
        await sender.Send(command);
        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteInventory(ISender sender, int id)
    {
        await sender.Send(new DeleteInventoryCommand(id));
        return TypedResults.NoContent();
    }
}
