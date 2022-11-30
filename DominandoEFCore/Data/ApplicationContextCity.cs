using System;
using DominandoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DominandoEFCore.Data
{
    public class ApplicationContextCity : DbContext
    {
        public DbSet<City> Cities { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=C002;Integrated Security=true;pooling=true;";
            optionsBuilder
               .UseSqlServer(strConnection)
               .EnableSensitiveDataLogging()
               .LogTo(Console.WriteLine, LogLevel.Information);
            //.UseLazyLoadingProxies()
        }
    }
}