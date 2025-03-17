using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.DatabaseContext
{
    public class PetDatabaseContextFactory : IDesignTimeDbContextFactory<PetDatabaseContext>
    {
        public PetDatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PetDatabaseContext>();
            var connectionString = "Server=DESKTOP-JJTIOH3\\SQLEXPRESS;User ID=sa;Password=12345;Database=PetMall;Trusted_Connection=False;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);

            return new PetDatabaseContext(optionsBuilder.Options);
        }
    }
}
