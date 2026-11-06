using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipAssistant.API.Data;
using CarDealershipAssistant.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAssistant.API.Services
{
    public class CarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Car>> SearchCarsAsync(
      string? brand,
      decimal? maxPrice,
      int? minYear,
      string? fuelType,
      string? transmission)
        {
            Console.WriteLine("===== SEARCH CARS =====");
            Console.WriteLine($"Brand: {brand}");
            Console.WriteLine($"MaxPrice: {maxPrice}");
            Console.WriteLine($"MinYear: {minYear}");
            Console.WriteLine($"FuelType: {fuelType}");
            Console.WriteLine($"Transmission: {transmission}");

            var query = _context.Cars
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                query = query.Where(c =>
                    c.Brand.ToLower() == brand.ToLower());
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(c =>
                    c.Price <= maxPrice.Value);
            }

            if (minYear.HasValue && minYear.Value > 0)
            {
                query = query.Where(c =>
                    c.Year >= minYear.Value);
            }

            if (!string.IsNullOrWhiteSpace(fuelType))
            {
                query = query.Where(c =>
                    c.FuelType.ToLower() == fuelType.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(transmission))
            {
                query = query.Where(c =>
                    c.Transmission.ToLower() == transmission.ToLower());
            }

            var result = await query.ToListAsync();

            Console.WriteLine("===== SEARCH RESULTS =====");

            foreach (var car in result)
            {
                Console.WriteLine(
                    $"{car.Brand} {car.Model} - {car.Year} - {car.Price}");
            }

            return result;


        }


    }
}