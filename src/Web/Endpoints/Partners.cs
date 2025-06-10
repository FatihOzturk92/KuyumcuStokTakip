using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Application.Partners.Commands.CreatePartner;
using KuyumcuStokTakip.Application.Partners.Commands.DeletePartner;
using KuyumcuStokTakip.Application.Partners.Commands.UpdatePartner;
using KuyumcuStokTakip.Application.Partners.Queries.GetPartners;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KuyumcuStokTakip.Web.Endpoints;

public class Partners : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetPartners)
            .MapPost(CreatePartner)
            .MapPut(UpdatePartner, "{id}")
            .MapDelete(DeletePartner, "{id}");
    }

    public async Task<Ok<PaginatedList<PartnerDto>>> GetPartners(ISender sender, [AsParameters] GetPartnersQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreatePartner(ISender sender, CreatePartnerCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Partners)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdatePartner(ISender sender, int id, UpdatePartnerCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();
        await sender.Send(command);
        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeletePartner(ISender sender, int id)
    {
        await sender.Send(new DeletePartnerCommand(id));
        return TypedResults.NoContent();
    }
}
