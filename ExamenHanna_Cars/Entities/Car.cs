using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace ExamenHanna_Cars.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Color { get; set; }
        public DateTime Date { get; set; }
        public virtual List<CarOwner> Owner { get; set; }
        public virtual Brand Brand { get; set; }


    }
}