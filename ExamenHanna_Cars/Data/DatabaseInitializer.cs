using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ExamenHanna_Cars.Entities;
using System.Drawing;

namespace ExamenHanna_Cars.Data
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(EntityContext entityContext)
        {


            var brands = new List<Brand>
            {

                new Brand() { Model = "Millenium Falcon"},
                new Brand() { Model = "Ecto-1"},
                new Brand() { Model = "Delorean"},
                new Brand() { Model = "Batmobile"},

            };


            var owners = new List<Owner>() {

                new Owner() { FirstName = "Han", LastName = "Solo"},
                new Owner() { FirstName = "Bruce", LastName= "Wayne"},
                new Owner() { FirstName = "Marty", LastName = "McFly"},
                new Owner() { FirstName = "Peter", LastName = "Venkman"},
            };



            var cars = new List<Car>();

            for (var i = 0; i < 4; i++)
            {
                var carOwner = new CarOwner()
                {
                    Owner = owners[i]
                };

                Brand brand = brands[3];
                String color = "Black";


                if (i % 4 == 0)
                {
                    brand = brands[0];
                    color = "White";

                }
                else if (i % 3 == 0)
                {
                    brand = brands[1];
                    color = "Blue";


                }
                else if (i % 2 == 0)
                {
                    brand = brands[2];
                    color = "Green";

                }

                cars.Add(new Car
                {

                    Plate = $"1 - NRD - 85{i}",
                    Owner = new List<CarOwner> { carOwner },
                    Brand = brand,
                    Color = color

                });
            }

            entityContext.Database.EnsureCreated();
            entityContext.Brand.AddRange(brands);
            entityContext.Owners.AddRange(owners);
            entityContext.Cars.AddRange(cars);
            entityContext.SaveChanges();
        }
    }
}