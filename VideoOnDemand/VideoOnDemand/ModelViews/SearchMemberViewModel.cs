using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entity;
using VideoOnDemand.Models;

namespace VideoOnDemand.ModelViews
{
    public class SearchMemberViewModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public string AddNaissDate { get; set; }

        public SearchMemberViewModel() { }

        public SearchMemberViewModel(string FirstName, string LastName, string Nationality, string AddNaissDate, string Biography)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Nationality = Nationality;
            this.AddNaissDate = AddNaissDate;
            this.Biography = Biography;
        }

    }
}