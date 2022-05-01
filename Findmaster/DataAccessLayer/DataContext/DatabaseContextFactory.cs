using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Findmaster.DataAccessLayer.DataContext
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = usersdb; Username = postgres; Password = 1");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
