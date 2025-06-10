namespace KuyumcuStokTakip.Domain.Entities.Inventory;
public class Purity
{
    public Guid Id { get; set; }
    public required string Name { get; set; } // 14K, 18K, 22K, 24K
    public decimal Rate { get; set; } // Saflık oranı (örneğin 14K için 0.583)
}
