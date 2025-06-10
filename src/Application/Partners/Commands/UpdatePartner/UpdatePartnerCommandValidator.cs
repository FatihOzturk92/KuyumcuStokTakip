namespace KuyumcuStokTakip.Application.Partners.Commands.UpdatePartner;

public class UpdatePartnerCommandValidator : AbstractValidator<UpdatePartnerCommand>
{
    public UpdatePartnerCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
        RuleFor(v => v.Name).NotEmpty();
        RuleFor(v => v.Type).NotEmpty();
    }
}
