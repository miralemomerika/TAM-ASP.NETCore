using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IKategorijaKursaService
    {
        public IEnumerable<KategorijaKursa> GetAll();
        public KategorijaKursa GetById(int id);
        public void Update(KategorijaKursa tipPolaznika);
        public void Delete(KategorijaKursa tipPolaznika);
        public void Add(KategorijaKursa tipPolaznika);

    }
}
