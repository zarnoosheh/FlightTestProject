using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.SeedData;

namespace RepositoryLayer
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<ApplicationContext>
        {
            ApplicationContext IDesignTimeDbContextFactory<ApplicationContext>.CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
               .Build();

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

                optionsBuilder.UseLazyLoadingProxies().UseSqlServer<ApplicationContext>(configuration.GetConnectionString("DefaultConnection"), options => options.CommandTimeout(180));

                return new ApplicationContext(optionsBuilder.Options);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Seed

            modelBuilder.Seed();

            #endregion

            #region Entity

            modelBuilder.Entity<TblFlights>();
            modelBuilder.Entity<TblRoutes>();
            modelBuilder.Entity<TblSubscriptions>();

            #endregion

        }

    }
}