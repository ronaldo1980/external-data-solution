using ExternalData.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExternalData.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRateController : ControllerBase
{
    private readonly IExchangeRateRepository _repository;

    public ExchangeRateController(IExchangeRateRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatest()
    {
        var data = await _repository.GetLatestAsync();

        if (data is null)
            return NotFound(new { message = "Nenhum dado coletado até o momento." });

        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Ok(data);
    }
}