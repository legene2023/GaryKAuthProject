using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaryKAuthProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class IngredientsToRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_MonsterRecipeId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_MonsterRecipeId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "MonsterRecipeId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "IngredientMonsterRecipe",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    RecipesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientMonsterRecipe", x => new { x.IngredientsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_IngredientMonsterRecipe_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientMonsterRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMonsterRecipe_RecipesId",
                table: "IngredientMonsterRecipe",
                column: "RecipesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientMonsterRecipe");

            migrationBuilder.AddColumn<int>(
                name: "MonsterRecipeId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_MonsterRecipeId",
                table: "Ingredients",
                column: "MonsterRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_MonsterRecipeId",
                table: "Ingredients",
                column: "MonsterRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
