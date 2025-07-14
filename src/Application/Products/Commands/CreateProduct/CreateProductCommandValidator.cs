using FluentValidation;

namespace KuyumcuStokTakip.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
        RuleFor(v => v.PhotoUrl).MaximumLength(300);
        RuleFor(v => v.CertificateNumber).MaximumLength(100);
        RuleFor(v => v.Notes).MaximumLength(1000);
    }
}
