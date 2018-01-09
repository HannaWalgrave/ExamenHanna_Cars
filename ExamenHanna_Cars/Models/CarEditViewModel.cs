using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenHanna_Cars.Models
{
    public class CarEditViewModel
    {

        public int Id { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public DateTime Date { get; set; }
        public List<SelectListItem> Owners { get; set; }
        public int? BrandId { get; set; }
        public List<SelectListItem> Brands { get; set; }

    }
}
