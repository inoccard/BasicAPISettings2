using BasicAPISettings.Api.Domain.Models.WeatherForecastAggregate.Entities;
using BasicAPISettings.Api.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BasicAPISettings.Api.Controllers;

//[Authorize]
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
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var wf = await _repository.Query<WeatherForecast>().ToArrayAsync();

        if (wf is not null)
            return Ok(wf);
        else
            return NotFound("Nenhum dado encontrado!");
    }

    /// <summary>
    /// obtém um WeatherForecast
    /// </summary>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK, "", typeof(WeatherForecast))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var wf = await _repository.Query<WeatherForecast>().FirstOrDefaultAsync(w => w.Id == id);

        if (wf is not null)
            return Ok(wf);
        else
            return NotFound("Dado não encontrado!");
    }

    /// <summary>
    /// adicionar WeatherForecast
    /// </summary>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] WeatherForecast weatherForecast)
    {
        await _repository.Add(weatherForecast);

        if (await _repository.Commit())
            return Ok("Dados salvos com sucesso!");
        else
            return BadRequest("Não foi possível salvar os dados!");
    }

    /// <summary>
    /// adicionar WeatherForecast
    /// </summary>
    /// <returns></returns>
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] WeatherForecast weatherForecast)
    {
        var weatherForecastOld = await _repository.Query<WeatherForecast>().FirstOrDefaultAsync(w => w.Id == weatherForecast.Id);

        if (weatherForecastOld is null) await _repository.Add(weatherForecast);
        else
        {
            weatherForecastOld.Update(weatherForecast.Date, weatherForecast.TemperatureC, weatherForecast.Summary);

            _repository.Update(weatherForecastOld);
        }

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
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] WeatherForecast weatherForecast)
    {
        _repository.Remove(weatherForecast);

        if (await _repository.Commit())
            return Ok("Dados excluídos com sucesso!");
        else
            return BadRequest("Não foi possível excluir os dados!");
    }
}
