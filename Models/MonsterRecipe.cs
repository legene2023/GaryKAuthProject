using System.ComponentModel.DataAnnotations;

namespace GaryKAuthProject.Models
{
    public class MonsterRecipe
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public System.Collections.Generic.List<Ingredient> Ingredients { get; set; } = [];

        public MonsterRecipe()
        {
            Name = String.Empty;
            Ingredients = new List<Ingredient>();
        }
        
    }
}
