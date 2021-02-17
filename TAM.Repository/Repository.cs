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
            try
            {               
                DbSet.Add(entity);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {              
                DbSet.Remove(entity);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        public void Update(TEntity entity)
        {
            try
            {
                DbContext.Entry(entity).State = EntityState.Modified;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public TEntity GetById(int entityId)
        {
            try
            {
                if (DbSet.Find(entityId) == null)
                    throw new Exception("Entitet ne postoji u bazi");
                return DbSet.Find(entityId);
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        public TEntity GetById(string entityId)
        {
            try
            {
                if (DbSet.Find(entityId) == null)
                    throw new Exception("Entitet ne postoji u bazi");
                return DbSet.Find(entityId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
