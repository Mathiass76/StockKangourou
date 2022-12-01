using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKangourou
{
    public partial class MyContext : DbContext
    {
        public virtual DbSet<Kangourou> Kangourous { get; set; }
        public MyContext() : base()
        { }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.UserID = "sa";
                builder.Password = "Info76240#";
                //builder.IntegratedSecurity = true; //Compte windows
                builder.InitialCatalog = "StockKangourou";
                builder.DataSource = "localhost,1434";

                optionsBuilder.UseSqlServer(builder.ConnectionString);
            }
        }
    }
}