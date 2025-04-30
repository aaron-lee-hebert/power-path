using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PowerPath.Web.Data.Entities;

namespace PowerPath.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Identity Tables
            builder.Entity<User>().ToTable("users");
            builder.Entity<IdentityRole<Guid>>().ToTable("roles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Lowercase table names
                entity.SetTableName(entity.GetTableName().ToLowerInvariant());

                // Lowercase and snake_case column names
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }

                // Lowercase key names
                foreach (var key in entity.GetKeys())
                {
                    key.SetName(ToSnakeCase(key.GetName().ToLowerInvariant()));
                }

                foreach (var fk in entity.GetForeignKeys())
                {
                    fk.SetConstraintName(ToSnakeCase(fk.GetConstraintName().ToLowerInvariant()));
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName().ToLowerInvariant()));
                }
            }
        }

        private static string ToSnakeCase(string input) =>
            string.Concat(input.Select((x, i) =>
                i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()))
                .ToLower();
    }
}
