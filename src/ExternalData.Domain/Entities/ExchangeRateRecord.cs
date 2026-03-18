namespace ExternalData.Domain.Entities;

public class ExchangeRateRecord
{
    public int Id { get; set; }

    public string Code { get; set; } = "";

    public string CodeIn { get; set; } = "";

    public string Name { get; set; } = "";

    public decimal Bid { get; set; }

    public decimal Ask { get; set; }

    public DateTime CreatedAt { get; set; }
}