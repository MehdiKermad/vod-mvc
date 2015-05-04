using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoOnDemand.Models
{
    [Table("Acteur")]
    public class Actor : Personn
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public virtual List<Film> Films { get; set; }
    }
}