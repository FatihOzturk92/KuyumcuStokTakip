using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Partners.Commands.CreatePartner;
using KuyumcuStokTakip.Domain.Entities.Account;

namespace KuyumcuStokTakip.Application.FunctionalTests.Partners.Commands;

using static Testing;

public class CreatePartnerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireFields()
    {
        var command = new CreatePartnerCommand { Name = "", Type = "" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreatePartner()
    {
        var command = new CreatePartnerCommand
        {
            Name = "Test Partner",
            Type = "Supplier"
        };

        var id = await SendAsync(command);
        var entity = await FindAsync<Partner>(id);
        entity.Should().NotBeNull();
        entity!.Name.Should().Be(command.Name);
    }
}
