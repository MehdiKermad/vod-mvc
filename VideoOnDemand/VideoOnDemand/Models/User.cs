using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Models
{
    public class User : Personn
    {
        public int Id { get; set; }
        [Required]
        public String Pseudo { get; set; }
        [Required]
        public String Mdp { get; set; }
        public bool Admin { get; set; }
    }
}