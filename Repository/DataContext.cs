using Model;
using System.Data.Entity;

namespace Repository
{
    /// <summary>
    /// Classe de "contexto", representando um banco de dados
    /// Se comportará como uma abstração do mesmo, contendo propriedades representando TABELAS
    /// </summary>
    public partial class DataContext : DbContext
    {

        public DataContext() : base("DataContext")
        {
            //Disable initializer
            Database.SetInitializer<DataContext>(null);
        }

        // Representa a tablea CLIENTES
        public DbSet<Cliente> Clientes { get; set; }

    }
}