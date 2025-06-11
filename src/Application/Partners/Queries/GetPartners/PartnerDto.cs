using KuyumcuStokTakip.Domain.Entities.Account;

namespace KuyumcuStokTakip.Application.Partners.Queries.GetPartners;

public class PartnerDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string? PartnerPhone { get; init; }
    public string? PartnerEmail { get; init; }
    public string? PartnerAddress { get; init; }
    public string? Note { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Partner, PartnerDto>();
        }
    }
}
