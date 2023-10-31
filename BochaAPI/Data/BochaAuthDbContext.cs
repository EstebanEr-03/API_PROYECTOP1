using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BochaAPI.Data
{
    public class BochaAuthDbContext : IdentityDbContext
    {
        public BochaAuthDbContext(DbContextOptions<BochaAuthDbContext> options) : base(options)
        {

        }

        //Reglas de la DTBASE

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerrRoleId = "ba8a3480-4466-4309-b399-faf1ffa7786f";
            var writerrRoleId = "07cc3b5b-ec45-4a53-835c-fa2bf53f0653";

            var roles = new List<IdentityRole>
            {
                 new IdentityRole
                 {
                    Id=readerrRoleId,
                    ConcurrencyStamp=readerrRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                 },
                 new IdentityRole
                 {
                    Id=writerrRoleId,
                    ConcurrencyStamp=writerrRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                 }


            };

            //Seeding informacion

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
