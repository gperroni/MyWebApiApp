using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model.Interfaces;

namespace Repository
{

    /// <summary>
    /// Classe base de acesso ao banco de dados, através da propriedade MyContext, contendo métodos para criação,
    /// atualização, exclusão e listagem. 
    /// </summary>
    /// <typeparam name="TEntity">Classe representando uma ENTIDADE e que, por isso, pode ser persistida, excluída, alterada ou listada</typeparam>
    public class BaseRepository<TEntity> where TEntity : class, IEntity
    {
        // Propriedade representando uma "abstração" do banco de dados
        public DbContext MyContext;

        public BaseRepository()
        {
            this.MyContext = new DataContext();
        }


        /// <summary>
        /// Cria uma nova ENTIDADE no banco, conforme classe definida em IENTITY
        /// </summary>
        /// <param name="entidade">Entidade a ser gravada</param>
        public virtual void Create(TEntity entidade)
        {
            var dbSet = this.MyContext.Set<TEntity>();
            dbSet.Add(entidade);
            this.MyContext.SaveChanges();
        }


        /// <summary>
        /// Lista todas as ENTIDADES definidas em IENTITY
        /// </summary>
        /// <returns>Lista de ENTIDADES definidas em IENTITY</returns>
        public IEnumerable<TEntity> GetAll()
        {
            var dbSet = this.MyContext.Set<TEntity>();
            return dbSet.ToList();
        }


        /// <summary>
        /// Atualiza tudo que está no contexto de MYCONTEXT, realizando cadastros ou atualizações do
        /// tipo definido em IENTITY
        /// </summary>
        /// <returns>Número de linhas afetadas</returns>
        public int SaveChanges()
        {
            return MyContext.SaveChanges();
        }


        /// <summary>
        /// Exclui a entidade passada, do tipo definido em IENTITY
        /// </summary>
        /// <param name="entidade">Entidade a ser excluída</param>
        public void Delete(TEntity entidade)
        {
            var dbSet = this.MyContext.Set<TEntity>();
            dbSet.Remove(entidade);
            this.MyContext.SaveChanges();
        }
    }
}