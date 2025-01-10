using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PROPERTY_MANAGER.Infrastructure.EntitiesConfiguration;

namespace PROPERTY_MANAGER.Infrastructure.Context
{
    public class PersistenceContext : DbContext
    {
        private readonly IConfiguration _config;

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }

            modelBuilder.HasDefaultSchema(_config.GetValue<string>("SchemaName"));

            #region Models
            modelBuilder.ConfigureModel();
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
