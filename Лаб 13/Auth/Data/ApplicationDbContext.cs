using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");
            builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
            builder.Entity<IdentityRole>().ToTable("AspNetRoles");
            builder.Entity<IdentityUser>().ToTable("AspNetUsers");
        }
    }
}