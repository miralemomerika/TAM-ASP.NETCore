using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Repository
{
    public class ApplicationDbContext : IdentityDbContext<KorisnickiRacun>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<KorisnickiRacun> KorisnickiRacun { get; set; }
        public DbSet<SvrhaUplate> SvrhaUplate { get; set; }
        public DbSet<TipPolaznika> TipPolaznika { get; set; }
        public DbSet<TipDogadjaja> TipDogadjaja { get; set; }
        public DbSet<Prostorija> Prostorija { get; set; }
        public DbSet<KategorijaKursa> KategorijaKursa { get; set; }
        public DbSet<KategorijaObavijesti> KategorijaObavijesti { get; set; }
        public DbSet<ExceptionHandler> ExceptionHandler { get; set; }
        public DbSet<Kurs> Kurs { get; set; }
        public DbSet<Portir> Portir { get; set; }
        public DbSet<Predavac> Predavac { get; set; }
        public DbSet<Obavijest> Obavijest { get; set; }
        public DbSet<Organizator> Organizator { get; set; }
        public DbSet<Polaznik> Polaznik { get; set; }
        public DbSet<Prijava> Prijava { get; set; }
        public DbSet<Dogadjaj> Dogadjaj { get; set; }
        public DbSet<OrganizacijaKursa> OrganizacijaKursa { get; set; }
        public DbSet<Pohadjanje> Pohadjanje { get; set; }
        public DbSet<Uplata> Uplata { get; set; }
        //public DbSet<Ispit> Ispit { get; set; }
        //public DbSet<Rad> Rad { get; set; }
    }
}
