using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Models
{
    public class Film
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public Director Director { get; set; }
        public String Theme { get; set; }
        public String Description { get; set; }
        public DateTime Sortie { get; set; }

    }
}