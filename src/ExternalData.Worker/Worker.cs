using System.Globalization;
using System.Text.Json;
using ExternalData.Domain.Entities;
using ExternalData.Domain.Interfaces;
using ExternalData.Worker.Models;

namespace ExternalData.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;

    public Worker(
        ILogger<Worker> logger,
        IServiceScopeFactory scopeFactory,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker iniciado em: {time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IExchangeRateRepository>();
                var client = _httpClientFactory.CreateClient("awesome-api");

                var response = await client.GetAsync("json/last/USD-BRL", stoppingToken);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(stoppingToken);
                var data = JsonSerializer.Deserialize<AwesomeApiResponse>(json);

                if (data?.UsdBrl is null)
                {
                    _logger.LogWarning("Resposta inválida da API externa.");
                }
                else
                {
                    var record = new ExchangeRateRecord
                    {
                        Code = data.UsdBrl.Code ?? "USD",
                        CodeIn = data.UsdBrl.CodeIn ?? "BRL",
                        Name = data.UsdBrl.Name ?? "Dólar Comercial/Real Brasileiro",
                        Bid = decimal.TryParse(data.UsdBrl.Bid, CultureInfo.InvariantCulture, out var bid) ? bid : 0,
                        Ask = decimal.TryParse(data.UsdBrl.Ask, CultureInfo.InvariantCulture, out var ask) ? ask : 0,
                        CreatedAt = DateTime.UtcNow
                    };

                    await repository.AddAsync(record);

                    _logger.LogInformation(
                        "Cotação salva com sucesso. Bid: {bid}, Ask: {ask}, Data: {date}",
                        record.Bid, record.Ask, record.CreatedAt);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de rede ao consultar API externa.");
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout ao consultar API externa.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado durante coleta.");
            }

            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
        }
    }
}