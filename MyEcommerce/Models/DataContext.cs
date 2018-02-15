using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyEcommerce.Models.Repositories
{
    public partial class DataContext : DbContext
    {

        public DataContext() : base("DataContext")
        {
            //Disable initializer
            Database.SetInitializer<DataContext>(null);
        }

        public DbSet<Cliente> Clientes { get; set; }

    }
}