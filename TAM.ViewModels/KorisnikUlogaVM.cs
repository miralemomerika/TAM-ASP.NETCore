using System;
using System.Collections.Generic;
using System.Text;

namespace TAM.ViewModels
{
    public class KorisnikUlogaVM
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public string CVUrl { get; set; }
        public string Titula { get; set; }
    }
}
