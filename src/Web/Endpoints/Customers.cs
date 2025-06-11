using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Application.Customers.Commands.CreateCustomer;
using KuyumcuStokTakip.Application.Customers.Commands.DeleteCustomer;
using KuyumcuStokTakip.Application.Customers.Queries.GetCustomers;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class Customers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetCustomers)
            .MapPost(CreateCustomer)
            .MapDelete(DeleteCustomer, "{id}");
    }

    public async Task<Ok<PaginatedList<CustomerDto>>> GetCustomers(ISender sender, [AsParameters] GetCustomersQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateCustomer(ISender sender, CreateCustomerCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Customers)}/{id}", id);
    }

    public async Task<NoContent> DeleteCustomer(ISender sender, int id)
    {
        await sender.Send(new DeleteCustomerCommand(id));
        return TypedResults.NoContent();
    }
}
