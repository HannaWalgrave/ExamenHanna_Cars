using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ExamenHanna_Cars.Data;
using ExamenHanna_Cars.Models;
using ExamenHanna_Cars.Entities;
using System;

namespace ExamenHanna_Cars.Controllers
{

    public class BrandController : Controller
    {

        private readonly EntityContext _entityContext;

        public BrandController(EntityContext entityContext)
        {

            _entityContext = entityContext;

        }

        [HttpGet("/Home/About")]
        public IActionResult About()
        {
            var model = new CarListViewModel { Cars = new List<CarDetailViewModel>() };

            var allCars = GetFullGraph().OrderBy(x => x.Plate)
                .ToList();
            model.Cars.AddRange(allCars.Select(ConvertCar));


            return View(model);
        }

        private static CarDetailViewModel ConvertCar(Car car)
        {
            var vm = new CarDetailViewModel
            {
                Id = car.Id,
                Plate = car.Plate,
                Color = car.Color,
                Date = car.Date,
                Owner = string.Join(";", car.Owner.Select(x => x.Owner.FullName)),
                Brand = car.Brand?.Model,
                BrandId = car.Brand?.Id
            };
            return vm;
        }




        private IIncludableQueryable<Car, Owner> GetFullGraph()
        {
            return _entityContext.Cars.Include(x => x.Brand).Include(x => x.Owner).ThenInclude(x => x.Owner);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}