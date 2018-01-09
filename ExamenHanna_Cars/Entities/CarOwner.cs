using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenHanna_Cars.Entities
{
    public class CarOwner
    {

        public int CarId { get; set; }
        public Car Car { get; set; }
        public int? OwnerId { get; set; }
        public Owner Owner { get; set; }
    }

}
