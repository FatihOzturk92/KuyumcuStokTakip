using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Application.Expenses.Commands.CreateExpense;
using KuyumcuStokTakip.Application.Expenses.Commands.DeleteExpense;
using KuyumcuStokTakip.Application.Expenses.Commands.UpdateExpense;
using KuyumcuStokTakip.Application.Expenses.Queries.GetExpenses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class Expenses : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetExpenses)
            .MapPost(CreateExpense)
            .MapPut(UpdateExpense, "{id}")
            .MapDelete(DeleteExpense, "{id}");
    }

    public async Task<Ok<PaginatedList<ExpenseDto>>> GetExpenses(ISender sender, [AsParameters] GetExpensesQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateExpense(ISender sender, CreateExpenseCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Expenses)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateExpense(ISender sender, int id, UpdateExpenseCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();
        await sender.Send(command);
        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteExpense(ISender sender, int id)
    {
        await sender.Send(new DeleteExpenseCommand(id));
        return TypedResults.NoContent();
    }
}
