namespace KuyumcuStokTakip.Domain.Entities.Account;

public abstract  class AccountBase : BaseAuditableEntity
{
    public string FirstName { get; set; } = default!; // Müşteri adı
    public string LastName { get; set; } = default!;  // Müşteri soyadı
    public string? Phone { get; set; } // Telefon numarası (isteğe bağlı)
    public string? Email { get; set; } // E-posta (isteğe bağlı)
    public string? Address { get; set; } // Adres (isteğe bağlı)
    public string? Notes { get; set; } // Açıklama veya müşteri notu
}