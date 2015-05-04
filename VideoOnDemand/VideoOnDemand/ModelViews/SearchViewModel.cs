﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entity;
using VideoOnDemand.Models;

namespace VideoOnDemand.ModelViews
{
    public class SearchViewModel
    {
        public string Theme { get; set; }
        public string Nationality { get; set; }

        public SearchViewModel() { }

        public SearchViewModel(string Theme, string Nationality)
        {
            this.Theme = Theme;
            this.Nationality = Nationality;
        }

    }
}