namespace KuyumcuStokTakip.Domain.Entities.Account;

public class AccountPartner :AccountBase
{
    public Guid? PartnerId { get; set; } // Eğer Partner altı bir kişi ise dolar
    public Partner? Partner { get; set; }
}