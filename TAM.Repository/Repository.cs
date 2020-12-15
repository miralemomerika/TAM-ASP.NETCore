using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAM.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        ApplicationDbContext DbContext;
        DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            DbContext.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public TEntity GetById(int entityId)
        {
            return DbSet.Find(entityId);
        }
    }
}
