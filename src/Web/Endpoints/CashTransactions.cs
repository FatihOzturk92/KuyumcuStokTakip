using KuyumcuStokTakip.Application.CashTransactions.Commands.CreateCashTransaction;
using KuyumcuStokTakip.Application.CashTransactions.Commands.UpdateCashTransaction;
using KuyumcuStokTakip.Application.CashTransactions.Commands.DeleteCashTransaction;
using KuyumcuStokTakip.Application.CashTransactions.Queries.GetCashTransactions;
using KuyumcuStokTakip.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class CashTransactions : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetCashTransactions)
            .MapPost(CreateCashTransaction)
            .MapPut(UpdateCashTransaction, "{id}")
            .MapDelete(DeleteCashTransaction, "{id}");
    }

    public async Task<Ok<PaginatedList<CashTransactionDto>>> GetCashTransactions(ISender sender, [AsParameters] GetCashTransactionsQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateCashTransaction(ISender sender, CreateCashTransactionCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(CashTransactions)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateCashTransaction(ISender sender, int id, UpdateCashTransactionCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();
        await sender.Send(command);
        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteCashTransaction(ISender sender, int id)
    {
        await sender.Send(new DeleteCashTransactionCommand(id));
        return TypedResults.NoContent();
    }
}
