using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAM.Repository
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity); 
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int entityId);
        TEntity GetById(string entityId);
    }
}
