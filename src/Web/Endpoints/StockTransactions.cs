using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Commands.DeleteStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Commands.UpdateStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Queries.GetStockTransactions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class StockTransactions : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetStockTransactions)
            .MapPost(CreateStockTransaction)
            .MapPut(UpdateStockTransaction, "{id}")
            .MapDelete(DeleteStockTransaction, "{id}");
    }

    public async Task<Ok<PaginatedList<StockTransactionDto>>> GetStockTransactions(ISender sender, [AsParameters] GetStockTransactionsQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateStockTransaction(ISender sender, CreateStockTransactionCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(StockTransactions)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateStockTransaction(ISender sender, int id, UpdateStockTransactionCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();
        await sender.Send(command);
        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteStockTransaction(ISender sender, int id)
    {
        await sender.Send(new DeleteStockTransactionCommand(id));
        return TypedResults.NoContent();
    }
}

