using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamenHanna_Cars.Models
{
    public class OwnerListViewModel
    {
        public List<OwnerDetailViewModel> Owners { get; set; }
        public DateTime GeneratedAt => DateTime.Now;
    }

    public class OwnerDetailViewModel
    {
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}