using CarDealershipAssistant.API.Models;
using CarDealershipAssistant.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipAssistant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly CarService _carService;

    public CarsController(CarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Car>>> GetAllCars()
    {
        var cars = await _carService.GetAllCarsAsync();

        return Ok(cars);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Car>> GetCarById(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);

        if (car is null)
        {
            return NotFound();
        }

        return Ok(car);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<Car>>> SearchCars(
        [FromQuery] string? brand,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int? minYear,
        [FromQuery] string? fuelType,
        [FromQuery] string? transmission)
    {
        var cars = await _carService.SearchCarsAsync(
            brand,
            maxPrice,
            minYear,
            fuelType,
            transmission);

        return Ok(cars);
    }
}