using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamenHanna_Cars.Models
{
    public class CarListViewModel
    {
        public List<CarDetailViewModel> Cars { get; set; }
        public List<CarEditViewModel> NewCars { get; set; }
        public DateTime GeneratedAt => DateTime.Now;
    }

    public class CarDetailViewModel
    {
        //Een auto heeft de volgende eigenschappen: kleur (string), datum gekocht (datetime), nummerplaat (string), eigenaar (object) en auto-type (object). 
        [Required]
        public int Id { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public DateTime Date { get; set; }

        //Een eigenaar heeft de volgende eigenschappen: naam (string), voornaam (string). 
        public int? OwnerId { get; set; }
        public string Owner { get; set; }
        public List<SelectListItem> Owners { get; set; }

        //Een auto-type heeft de volgende eigenschappen: merk (string), model (string).
        public string Brand { get; set; }
        public string Model { get; set; }

        public int? BrandId { get; set; }
        public List<SelectListItem> Brands { get; set; }
    }

    public class CarEditViewModel
    {

        public int Id { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public DateTime Date { get; set; }
        public int? OwnerId { get; set; }
        public List<SelectListItem> Owners { get; set; }
        public int? BrandId { get; set; }
        public List<SelectListItem> Brands { get; set; }

    }
}