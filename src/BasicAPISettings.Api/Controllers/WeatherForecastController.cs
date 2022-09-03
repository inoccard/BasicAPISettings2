using BasicAPISettings.Api.Domain.Models.WeatherForecastAggregate.Entities;
using BasicAPISettings.Api.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BasicAPISettings.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/weather-forecast")]
public class WeatherForecastController : ControllerBase
{
    private readonly IRepository _repository;

    public WeatherForecastController(IRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// obtém um WeatherForecast
    /// </summary>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK, "", typeof(WeatherForecast[]))]
    [HttpGet("get-weather-forecasts")]
    public async Task<IActionResult> GetWeatherForecast()
    {
        var wf = await _repository.Query<WeatherForecast>().ToArrayAsync();

        if (wf is not null) 
            return Ok(wf);
        else 
            return BadRequest("Nenhum dado encontrado!");
    }

    /// <summary>
    /// adicionar WeatherForecast
    /// </summary>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [HttpGet("save-weather-forecasts")]
    public async Task<IActionResult> Save([FromBody] WeatherForecast weatherForecast)
    {
        await _repository.Add(weatherForecast);

        if (await _repository.Commit())
            return Ok("Dados salvos com sucesso!");
        else
            return BadRequest("Não foi possível salvar os dados!");
    }
    
    /// <summary>
    /// excluir WeatherForecast
    /// </summary>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [HttpGet("delete-weather-forecasts")]
    public async Task<IActionResult> Delete([FromBody] WeatherForecast weatherForecast)
    {
        _repository.Remove(weatherForecast);

        if (await _repository.Commit())
            return Ok("Dados excluídos com sucesso!");
        else
            return BadRequest("Não foi possível excluir os dados!");
    }
}
