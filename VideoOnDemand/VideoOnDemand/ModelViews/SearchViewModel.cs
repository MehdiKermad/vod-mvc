using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.DAL;
using VideoOnDemand.Models;

namespace VideoOnDemand.ModelViews
{
    public class SearchViewModel
    {
        public string Theme { get; set; }

        public SearchViewModel() { }

        public SearchViewModel(string Theme)
        {
            this.Theme = Theme;
        }

    }
}