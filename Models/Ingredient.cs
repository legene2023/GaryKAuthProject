using System.ComponentModel.DataAnnotations;

namespace GaryKAuthProject.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public List<MonsterRecipe> Recipes { get; set; } = [];

        public Ingredient()
        {
            Name = String.Empty;
        }
    }
}
