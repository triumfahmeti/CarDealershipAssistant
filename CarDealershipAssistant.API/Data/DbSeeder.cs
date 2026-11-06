using CarDealershipAssistant.API.Models;

namespace CarDealershipAssistant.API.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // If there are already cars, do not seed again
        if (context.Cars.Any())
        {
            return;
        }

        var cars = new List<Car>
        {
            new Car
            {
                Brand = "BMW",
                Model = "320d",
                Year = 2021,
                Price = 23500,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 85000,
                Color = "Black"
            },

            new Car
            {
                Brand = "BMW",
                Model = "X3",
                Year = 2020,
                Price = 24900,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 92000,
                Color = "White"
            },

            new Car
            {
                Brand = "BMW",
                Model = "520d",
                Year = 2022,
                Price = 31500,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 65000,
                Color = "Grey"
            },

            new Car
            {
                Brand = "BMW",
                Model = "330i",
                Year = 2021,
                Price = 28900,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Mileage = 55000,
                Color = "Blue"
            },

            new Car
            {
                Brand = "Mercedes-Benz",
                Model = "C220",
                Year = 2021,
                Price = 26500,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 78000,
                Color = "Black"
            },

            new Car
            {
                Brand = "Mercedes-Benz",
                Model = "E220",
                Year = 2022,
                Price = 34500,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 62000,
                Color = "Silver"
            },

            new Car
            {
                Brand = "Mercedes-Benz",
                Model = "A200",
                Year = 2020,
                Price = 21500,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Mileage = 70000,
                Color = "White"
            },

            new Car
            {
                Brand = "Audi",
                Model = "A4",
                Year = 2021,
                Price = 24500,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 88000,
                Color = "Grey"
            },

            new Car
            {
                Brand = "Audi",
                Model = "Q5",
                Year = 2020,
                Price = 29900,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 95000,
                Color = "Black"
            },

            new Car
            {
                Brand = "Audi",
                Model = "A3",
                Year = 2022,
                Price = 23500,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Mileage = 45000,
                Color = "Red"
            },

            new Car
            {
                Brand = "Volkswagen",
                Model = "Golf 8",
                Year = 2021,
                Price = 19500,
                FuelType = "Petrol",
                Transmission = "Manual",
                Mileage = 60000,
                Color = "White"
            },

            new Car
            {
                Brand = "Volkswagen",
                Model = "Passat",
                Year = 2020,
                Price = 18500,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 110000,
                Color = "Black"
            },

            new Car
            {
                Brand = "Volkswagen",
                Model = "Tiguan",
                Year = 2022,
                Price = 27500,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 55000,
                Color = "Blue"
            },

            new Car
            {
                Brand = "Toyota",
                Model = "RAV4",
                Year = 2021,
                Price = 28500,
                FuelType = "Hybrid",
                Transmission = "Automatic",
                Mileage = 50000,
                Color = "White"
            },

            new Car
            {
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2022,
                Price = 22500,
                FuelType = "Hybrid",
                Transmission = "Automatic",
                Mileage = 35000,
                Color = "Silver"
            },

            new Car
            {
                Brand = "Volvo",
                Model = "XC60",
                Year = 2021,
                Price = 31000,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 72000,
                Color = "Grey"
            },

            new Car
            {
                Brand = "Ford",
                Model = "Focus",
                Year = 2021,
                Price = 16500,
                FuelType = "Petrol",
                Transmission = "Manual",
                Mileage = 65000,
                Color = "Blue"
            },

            new Car
            {
                Brand = "Ford",
                Model = "Kuga",
                Year = 2022,
                Price = 24500,
                FuelType = "Hybrid",
                Transmission = "Automatic",
                Mileage = 40000,
                Color = "Black"
            },

            new Car
            {
                Brand = "Porsche",
                Model = "Macan",
                Year = 2021,
                Price = 52000,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Mileage = 48000,
                Color = "Black"
            },

            new Car
            {
                Brand = "Skoda",
                Model = "Octavia",
                Year = 2021,
                Price = 18500,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Mileage = 90000,
                Color = "White"
            }
        };

        await context.Cars.AddRangeAsync(cars);

        await context.SaveChangesAsync();
    }
}