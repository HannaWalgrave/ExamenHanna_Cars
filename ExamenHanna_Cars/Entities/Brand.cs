using System;
using System.Collections.Generic;

namespace ExamenHanna_Cars.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public virtual IEnumerable<Car> Cars { get; set; }
    }
}