using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GaryKAuthProject.Data;
using GaryKAuthProject.Models;
using Humanizer.Localisation.NumberToWords;

namespace GaryKAuthProject.Controllers
{
    public class MonsterRecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonsterRecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MonsterRecipes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipes.ToListAsync());
        }

        // GET: MonsterRecipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monsterRecipe = await _context.Recipes.Include(mr => mr.Ingredients)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monsterRecipe == null)
            {
                return NotFound();
            }

            return View(monsterRecipe);
        }

        // GET: MonsterRecipes/Create
        public async Task<IActionResult> Create()
        {
            List<Ingredient> allIngredients = await _context.Ingredients.ToListAsync();
            MonsterRecipeEditVM viewModel = new MonsterRecipeEditVM() { Name = String.Empty, IngredientSelections = new List<MonsterRecipeEditVM.IngredientSelection>() };
            viewModel.IngredientSelections = new List<MonsterRecipeEditVM.IngredientSelection>();
            foreach (Ingredient ing in allIngredients)
            {
                viewModel.IngredientSelections.Add(new MonsterRecipeEditVM.IngredientSelection(ing));
            }


            ViewBag.AllIngredients = allIngredients;
            return View(viewModel);
        }

        // POST: MonsterRecipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MonsterRecipeEditVM viewModel)
        {
            if (ModelState.IsValid)
            {
                MonsterRecipe recipe = new MonsterRecipe() { Name = viewModel.Name, Ingredients = new() }; 
                List<Ingredient> allIngredients = await _context.Ingredients.ToListAsync();
                foreach (Ingredient ing in allIngredients)
                {
                    if (Request.Form.ContainsKey($"ing-{ing.Id}"))
                    {
                        recipe.Ingredients.Add(ing);
                    }
                }
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: MonsterRecipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monsterRecipe = await _context.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(m => m.Id == id);
            if (monsterRecipe == null)
            {
                return NotFound();
            }

            MonsterRecipeEditVM viewModel = new MonsterRecipeEditVM() { Name = monsterRecipe.Name, IngredientSelections = new() };

            List<Ingredient> allIngredients = await _context.Ingredients.ToListAsync();
            foreach(Ingredient ingredient in allIngredients)
            {
                MonsterRecipeEditVM.IngredientSelection selection = new MonsterRecipeEditVM.IngredientSelection (ingredient);
                if (monsterRecipe.Ingredients.Contains(ingredient))
                {
                    selection.Selected = true;
                }

                viewModel.IngredientSelections.Add(selection);
            }

            return View(viewModel);
        }

        // POST: MonsterRecipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MonsterRecipeEditVM viewModel)
        {
            if (!viewModel.Id.HasValue)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    MonsterRecipe oldRecipe = await _context.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(m => m.Id == id);
                    if (oldRecipe == null)
                    {
                        return NotFound();
                    }

                    MonsterRecipe newRecipe = new MonsterRecipe() { Id = viewModel.Id.Value, Name = viewModel.Name };
                    oldRecipe.Name = newRecipe.Name;


                    List<Ingredient> allIngredients = await _context.Ingredients.ToListAsync();
                    foreach (Ingredient ing in allIngredients)
                    {
                        string ingredientFormKey = $"ing-{ing.Id}";

                        if (Request.Form.ContainsKey(ingredientFormKey) && !oldRecipe.Ingredients.Contains(ing))
                        {
                            oldRecipe.Ingredients.Add(ing);
                        }

                        if (oldRecipe.Ingredients.Contains(ing) && !Request.Form.ContainsKey(ingredientFormKey))
                        {
                            oldRecipe.Ingredients.Remove(ing);
                        }
               
                    }

                    _context.Update(oldRecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonsterRecipeExists(viewModel.Id.Value))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: MonsterRecipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monsterRecipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monsterRecipe == null)
            {
                return NotFound();
            }

            return View(monsterRecipe);
        }

        // POST: MonsterRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monsterRecipe = await _context.Recipes.FindAsync(id);
            if (monsterRecipe != null)
            {
                _context.Recipes.Remove(monsterRecipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonsterRecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
