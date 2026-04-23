using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiPetshop.Infra.Data
{
    public class MeuDbContextFactory : IDesignTimeDbContextFactory<MeuDbContext>
    {
        public MeuDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MeuDbContext>();

            // Coloque sua string de conexão aqui apenas para a migração
            var connectionString = "Server=localhost;Database=db_patas_felizes;Uid=root;Pwd=123;";

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));

            return new MeuDbContext(optionsBuilder.Options);
        }
    }
}