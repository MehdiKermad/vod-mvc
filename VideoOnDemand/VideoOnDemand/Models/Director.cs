using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Models
{
    public class Director : Personn
    {
        [Required]
        public int Id { get; set; }
        public string Nationality { get; set; }
        public virtual List<Actor> Actors { get; set; }
        public virtual List<Film> Films { get; set; }
    }
}