using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Models
{
    public class Actor : Personn
    {
        [Required]
        public int Id { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public virtual List<Film> Films { get; set; }
    }
}