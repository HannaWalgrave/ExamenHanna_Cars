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

    public class OwnerController : Controller
    {

        private readonly EntityContext _entityContext;

        public OwnerController(EntityContext entityContext)
        {

            _entityContext = entityContext;

        }

        // All owners and their cars

        [HttpGet("/Home/Contact")]
        public IActionResult Contact()
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



        // Delete car from list

        [HttpPost("/Home/About/delete/{id}")]

        public IActionResult Delete([FromRoute] int id)
        {
            var toDelete = _entityContext.Cars.FirstOrDefault(x => x.Id == id);
            if (toDelete != null)
            {
                _entityContext.Cars.Remove(toDelete);
                _entityContext.SaveChanges();
            }
            return RedirectToAction(nameof(Contact));
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
