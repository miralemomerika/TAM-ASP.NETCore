using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.Core
{
    public class KorisnickiRacun : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}