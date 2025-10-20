using HNG_Stage0_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace HNG_Stage0_Api.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<StringEntity> Strings { get; set; }
    }
}
