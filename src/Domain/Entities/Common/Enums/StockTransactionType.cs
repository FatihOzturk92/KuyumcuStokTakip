namespace KuyumcuStokTakip.Domain.Entities;
public enum ProductType
{
    ModelBased,
    CategoryBased,
    PurityBased
}

public enum EStockTransactionType
{
    In = 0,
    Out = 1
}
public enum EUnitPriceType
{
    Milyem = 0,
    Dolar = 1,
    TL = 2
}
public enum PartnerType
{
    Wholesaler = 0,
    Workshop = 1,
    Branch = 2
}
public enum AccountCurrencyType
{
    Gold = 0, // Gram bazlı
    Dolar = 1,
    TL = 2
}

public enum AccountTransactionType
{
    Debit = 0,  // Borç
    Credit = 1  // Alacak
}public enum ETransactionSourceType
{
    Customer = 0,
    Partner = 1,
    Anonymous = 2
}
public enum AccountOwnerType
{
    Customer = 0,
    Partner = 1
}

 
public enum CashTransactionType
{
    In = 0,   // Kasa Girişi
    Out = 1   // Kasa Çıkışı
}

public enum CashTransactionSource
{
    Cash = 0,     // Nakit
    Bank = 1,     // Banka transferi
    Pos = 2       // Kredi kartı (POS)
}

 public enum EWeightUnit
{
    Gram = 0,
    Adet = 1,
    Dolar = 2
}



