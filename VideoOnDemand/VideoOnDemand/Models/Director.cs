using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoOnDemand.Models
{
    [Table("Realisateur")]
    public class Director : Personn
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nationality { get; set; }
        public virtual List<Actor> Actors { get; set; }
        public virtual List<Film> Films { get; set; }
    }
}