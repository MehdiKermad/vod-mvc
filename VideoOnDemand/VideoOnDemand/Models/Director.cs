using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Models
{
    public class Director
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String FirstName { get; set; }
        public DateTime Birth { get; set; }
    }
}