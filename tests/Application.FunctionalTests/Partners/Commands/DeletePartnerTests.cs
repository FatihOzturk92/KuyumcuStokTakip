using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Partners.Commands.CreatePartner;
using KuyumcuStokTakip.Application.Partners.Commands.DeletePartner;
using KuyumcuStokTakip.Domain.Entities.Account;

namespace KuyumcuStokTakip.Application.FunctionalTests.Partners.Commands;

using static Testing;

public class DeletePartnerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidId()
    {
        var command = new DeletePartnerCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeletePartner()
    {
        var id = await SendAsync(new CreatePartnerCommand { Name = "Test", Type = "Supplier" });
        await SendAsync(new DeletePartnerCommand(id));
        var entity = await FindAsync<Partner>(id);
        entity.Should().BeNull();
    }
}
