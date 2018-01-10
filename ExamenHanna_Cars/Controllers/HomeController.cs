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

namespace ExamenHanna_Cars.Controllers
{

    public class HomeController : Controller
    {
        private readonly EntityContext _entityContext;

        public HomeController(EntityContext entityContext)
        {

            _entityContext = entityContext;

        }

        // Get all cars

        [HttpGet("/")]

        public IActionResult Index()
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
                OwnerId = car.Owner?.Select(x => x.OwnerId).FirstOrDefault(),
                Brand = car.Brand?.Model,
                BrandId = car.Brand?.Id
            };
            return vm;
        }



        // Get details from specific car

        [HttpGet("/Home/{id}")]

        public IActionResult Detail([FromRoute]int id)
        {
            var car = GetFullGraph()
                .FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = ConvertCar(car);
            vm.Brands = _entityContext.Brand.Select(x => new SelectListItem
            {
                Text = x.Model,
                Value = x.Id.ToString(),
            }).ToList();

            vm.Owners = _entityContext.Owners.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString(),
            }).ToList();

            return View(vm);
        }


        // Save edit

        [HttpPost("/cars")]
        public IActionResult Persist([FromForm] CarDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var car = vm.Id == 0 ? new Car() : GetFullGraph().FirstOrDefault(x => x.Id == vm.Id);
                car.Plate = vm.Plate;
                car.Date = vm.Date;
                car.Color = vm.Color;
                car.Brand = vm.BrandId.HasValue ? _entityContext.Brand.FirstOrDefault(x => x.Id == vm.BrandId) : null;

                List<CarOwner> OwnersList = new List<CarOwner>();
                OwnersList.Add(new CarOwner()
                {
                    OwnerId = vm.OwnerId,
                });
                car.Owner = OwnersList;


                if (vm.Id == 0)
                    _entityContext.Cars.Add(car);
                else
                    _entityContext.Cars.Update(car);
                _entityContext.SaveChanges();

                return Redirect("/");
            }

            return View("Detail", vm);
        }

        private IIncludableQueryable<Car, Owner> GetFullGraph()
        {
            return _entityContext.Cars.Include(x => x.Brand).Include(x => x.Owner).ThenInclude(x => x.Owner);
        }



        // Delete car from list

        [HttpPost("/Home/delete/{id}")]

        public IActionResult Delete([FromRoute] int id)
        {
            var toDelete = _entityContext.Cars.FirstOrDefault(x => x.Id == id);
            if (toDelete != null)
            {
                _entityContext.Cars.Remove(toDelete);
                _entityContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


        // Add a new car

        [HttpGet("/Home/New")]
        public IActionResult New()
        {
            return View("Detail", new CarDetailViewModel()
            {

                Plate = null,
                Owner = null,
                Color = null,
                Brand = null,


                Brands = _entityContext.Brand.Select(x => new SelectListItem
                {
                    Text = x.Model,
                    Value = x.Id.ToString(),
                }
                ).ToList(),

                Owners = _entityContext.Owners.Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id.ToString(),
                }).ToList(),


            });


        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}