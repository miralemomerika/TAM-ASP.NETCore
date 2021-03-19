using Microsoft.EntityFrameworkCore;
using TAM.Repository;

namespace TAM.Test
{
    class TestHelper
    {
        public static ApplicationDbContext GetTAMDbContext()
        {
            DbContextOptionsBuilder<ApplicationDbContext> builder
                = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=app.fit.ba,1431; Database=p2062_TamDb;Trusted_Connection=false;User ID=p2062;Password=mV3Rgt!;MultipleActiveResultSets=true;");
                return new ApplicationDbContext(builder.Options);
        }
    }
}
