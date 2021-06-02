using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IRecenzijeService
    {
        public IEnumerable<Recenzija> GetAll();
        public IEnumerable<Recenzija> GetAllByOrganizacijaId(int Id);
        public Recenzija GetById(int Id);
        public void Update(Recenzija recenzija);
        public void Delete(Recenzija recenzija);
        public void Add(Recenzija recenzija);
        public IEnumerable<OrganizacijaKursa> GetAllAktivne();
    }
}
