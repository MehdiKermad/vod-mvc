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
    }
}