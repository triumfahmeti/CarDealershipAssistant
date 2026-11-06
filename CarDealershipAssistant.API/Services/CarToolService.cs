using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CarDealershipAssistant.API.DTOs;

namespace CarDealershipAssistant.API.Services
{
    public class CarToolService
    {
        private readonly CarService _carService;

        public CarToolService(CarService carService)
        {
            _carService = carService;
        }

        public async Task<string> SearchCarsAsync(
            SearchCarsRequest request)
        {
            decimal? maxPrice = request.MaxPrice;
            int? minYear = request.MinYear is > 0 ? request.MinYear : null;

            var cars = await _carService.SearchCarsAsync(
                request.Brand,
                maxPrice,
                minYear,
                request.FuelType,
                request.Transmission);

            if (cars.Count == 0)
            {
                return "No cars were found matching the requested criteria.";
            }

            return JsonSerializer.Serialize(cars);
        }
    }
}