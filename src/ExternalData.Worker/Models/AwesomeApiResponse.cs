using System.Text.Json.Serialization;

namespace ExternalData.Worker.Models;

public class AwesomeApiResponse
{
    [JsonPropertyName("USDBRL")]
    public UsdBrlData? UsdBrl { get; set; }
}

public class UsdBrlData
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("codein")]
    public string? CodeIn { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("bid")]
    public string? Bid { get; set; }

    [JsonPropertyName("ask")]
    public string? Ask { get; set; }
}