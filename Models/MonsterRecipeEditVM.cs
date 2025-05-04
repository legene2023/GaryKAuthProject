using System.ComponentModel;

namespace GaryKAuthProject.Models
{
    public class MonsterRecipeEditVM
    {
        public class IngredientSelection
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public bool Selected { get; set; }

            public IngredientSelection()
            {
                Name = String.Empty;
                Id = String.Empty;
                Selected = false;
            }

            public IngredientSelection(Ingredient ingredient, bool pSelected = false)
            {
                Id = $"ing-{ingredient.Id}";
                Name = ingredient.Name;
                Selected = pSelected;
            }
        }

        public int? Id { get; set; }

        public required string Name { get; set; }

        [DisplayName("Ingredients")]
        public List<IngredientSelection>? IngredientSelections { get; set; }
    }
}
