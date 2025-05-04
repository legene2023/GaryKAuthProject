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

        public DbSet<GaryKAuthProject.Models.MonsterRecipe> Recipes { get; set; }

        public DbSet<GaryKAuthProject.Models.Ingredient> Ingredients { get; set; }

    }
}
