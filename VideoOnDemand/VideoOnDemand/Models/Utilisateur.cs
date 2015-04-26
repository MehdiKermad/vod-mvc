using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public String Pseudo { get; set; }
        public String Mdp { get; set; }
        public bool Admin { get; set; }
    }
}