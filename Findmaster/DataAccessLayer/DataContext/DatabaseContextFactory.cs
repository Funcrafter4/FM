using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Findmaster.DataAccessLayer.DataContext
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql("Host = abul.db.elephantsql.com; Port = 5432; Database = asbroxrk; Username = asbroxrk; Password = 3bED_msH9cnUf1UP1YxkBoRrpYGLqtDf");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
