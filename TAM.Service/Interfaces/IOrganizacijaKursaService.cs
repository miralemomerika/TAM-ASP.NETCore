using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IOrganizacijaKursaService
    {
        public IEnumerable<OrganizacijaKursa> GetAll();
        public OrganizacijaKursa GetById(int id);
        public void Update(OrganizacijaKursa kurs);
        public void Delete(OrganizacijaKursa kurs);
        public void Add(OrganizacijaKursa kurs);
    }
}
