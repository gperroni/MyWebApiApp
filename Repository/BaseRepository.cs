using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model.Interfaces;

namespace Repository
{
    public class BaseRepository<TEntity> where TEntity : class, IEntity
    {
        public DbContext MyContext;

        public BaseRepository()
        {
            this.MyContext = new DataContext();
        }

        public virtual void Create(TEntity entidade)
        {
            var dbSet = this.MyContext.Set<TEntity>();
            dbSet.Add(entidade);
            this.MyContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            var dbSet = this.MyContext.Set<TEntity>();
            return dbSet.ToList();
        }

        public int SaveChanges()
        {
            return MyContext.SaveChanges();
        }

        public void Delete(TEntity entidade)
        {
            var dbSet = this.MyContext.Set<TEntity>();
            dbSet.Remove(entidade);
            this.MyContext.SaveChanges();
        }
    }
}