using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GaryKAuthProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                   new IdentityRole
                   {
                       Id = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                       Name = GaryKAuthProject.Models.Role.UserRole.Name,
                       NormalizedName = GaryKAuthProject.Models.Role.UserRole.Name.ToUpper(),
                   },
                 new IdentityRole
                 {
                     Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                     Name = GaryKAuthProject.Models.Role.AdministratorRole.Name,
                     NormalizedName = GaryKAuthProject.Models.Role.AdministratorRole.Name.ToUpper()
                 });
        }




        public DbSet<GaryKAuthProject.Models.MonsterRecipe> Recipes { get; set; }

        public DbSet<GaryKAuthProject.Models.Ingredient> Ingredients { get; set; }

    }
}
