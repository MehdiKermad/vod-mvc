using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoOnDemand.Models
{
    [Table("Film")]
    public class Film
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public Director Director { get; set; }
        public String Theme { get; set; }
        public String Description { get; set; }
        public string Nationality { get; set; }
        public DateTime ReleaseDateFilm { get; set; }
        public DateTime AddDateFilm { get; set; }
        public virtual List<Actor> Actors { get; set; }

    }
}