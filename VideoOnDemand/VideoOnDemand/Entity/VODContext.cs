using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VideoOnDemand.Models;

namespace VideoOnDemand.Entity
{
    public class VODContext : DbContext
    {
        public VODContext()
            : base("name=VODContext")
        {
        Database.SetInitializer(new VODContextInitializer());
        }
        
        
        public DbSet<User> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }

}