﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public Film Film { get; set; }
        public string Message { get; set; }
        public int Marks { get; set; }
        public DateTime Date { get; set; }
        public bool Validated {get; set;}
    }
}