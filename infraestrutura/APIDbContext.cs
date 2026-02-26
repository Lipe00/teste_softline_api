using domain.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace infraestrutura
{
    public class APIDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<User> User { get; set; }

        public APIDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var typeDatabase = _configuration["TypeDatabase"];
            var connectionString = _configuration.GetConnectionString(typeDatabase);
            optionsBuilder.UseMySQL(connectionString);
        }
    }
}
