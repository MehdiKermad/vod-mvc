using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoOnDemand.Models
{
    [Table("Utilisateur")]
    public class User : Personn
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public String Pseudo { get; set; }
        [Required]
        public String Mdp { get; set; }
        public bool Admin { get; set; }
    }
}