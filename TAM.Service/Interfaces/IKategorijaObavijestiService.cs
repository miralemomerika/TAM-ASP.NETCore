using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IKategorijaObavijestiService
    {
        public IEnumerable<KategorijaObavijesti> GetAll();
        public KategorijaObavijesti GetById(int id);
        public void Update(KategorijaObavijesti tipPolaznika);
        public void Delete(KategorijaObavijesti tipPolaznika);
        public void Add(KategorijaObavijesti tipPolaznika);
    }
}
