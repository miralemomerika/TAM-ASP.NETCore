﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TAM.ViewModels
{
    public class UposleniRegistracijaVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Broj telefona")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Ime")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Prezime")]
        public string LastName { get; set; }

        public string TipUposlenog { get; set; }

        public UposleniRegistracijaVM()
        {
            Password = GenerisiLozinku(8);
        }

        private string GenerisiLozinku(int duzina)
        {
            string velikaSlova = "ASDFGHJKLERTZUIOPWQYXCVBNM";
            string malaSlova = velikaSlova.ToLower();
            string specijalniZnakovi = "?*!#%&";
            string brojevi = "0123456789";
            
            return RandomZnakovi(velikaSlova, 1) + RandomZnakovi(specijalniZnakovi, 1)
                + RandomZnakovi(brojevi,1) + RandomZnakovi(malaSlova, duzina - 3);
        }

        private string RandomZnakovi(string tekst, int duzina)
        {
            Random rnd = new Random();
            string rezultat = "";
            for(int i=0; i<duzina; i++)
            {
                rezultat += tekst[rnd.Next(tekst.Length)];
            }
            return rezultat;
        }
    }
}
