using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using VideoOnDemand.Models;

namespace VideoOnDemand.Entity
{
    public class VODContextInitializer : CreateDatabaseIfNotExists<VODContext>
    {
        protected override void Seed(VODContext context)
        {
            base.Seed(context);
            var users = new List<User>
            {
                new User { Pseudo = "Admin", Mdp = "pass", 
                    Admin = true, FirstName = "Mehdi", LastName = "Kermad", Birth = DateTime.Parse("1970-01-01") },
                new User { Pseudo = "Client", Mdp = "mdp", 
                    Admin = true, FirstName = "Jean", LastName = "Paul", Birth = DateTime.Parse("1981-12-30") }
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var films = new List<Film>
            {
                new Film { Name = "Fight Club", Theme = "Drame", Description = "Un homme un peu cinglé sur les bords ..."
                    , Nationality = "Etats-Unis", ReleaseDateFilm = DateTime.Parse("1970-01-01"), AddDateFilm = DateTime.Now },
                new Film { Name = "Americain Psycho", Theme = "Thriller",
                    Nationality = "Etats-Unis", ReleaseDateFilm = DateTime.Parse("2000-09-17"), AddDateFilm = DateTime.Now },
                new Film { Name = "Intouchables", Theme = "Comédie",
                    Nationality = "France", ReleaseDateFilm = DateTime.Parse("2011-03-07"), AddDateFilm = DateTime.Now },
                new Film { Name = "Old Boy", Theme = "Drame",
                    Nationality = "Corée du Sud", ReleaseDateFilm = DateTime.Parse("2009-06-22"), AddDateFilm = DateTime.Now },
                new Film { Name = "The Dark Knight Rises", Theme = "Action",
                    Nationality = "Etats-Unis", ReleaseDateFilm = DateTime.Parse("2012-09-17"), AddDateFilm = DateTime.Now },
                new Film { Name = "Je suis une légende", Theme = "Science-Fiction",
                    Nationality = "Etats-Unis", ReleaseDateFilm = DateTime.Parse("2008-06-02"), AddDateFilm = DateTime.Now },
                new Film { Name = "John Rambo", Theme = "Action",
                    Nationality = "Etats-Unis", ReleaseDateFilm = DateTime.Parse("2008-09-10"), AddDateFilm = DateTime.Now },
                new Film { Name = "Heat", Theme = "Policier",
                    Nationality = "Etats-Unis", ReleaseDateFilm = DateTime.Parse("1996-09-17"), AddDateFilm = DateTime.Now }
            };
            films.ForEach(s => context.Films.Add(s));
            context.SaveChanges();

        }
    } 
}