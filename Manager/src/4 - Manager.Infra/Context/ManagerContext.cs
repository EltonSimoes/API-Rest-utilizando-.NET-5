using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Context
{
    public class ManagerContext : DbContext
    {
        public ManagerContext()
        { }

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }
    }

    //Pode retirar foi mapeado no Startup
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseMySql(@"server=192.168.0.5; port=3306; user=root;password=;Database=bonita;", MySqlServerVersion.LatestSupportedServerVersion);
    //}
}
