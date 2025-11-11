using System.Web.Mvc;
using RecipeBook.DataAccess;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeRepository _recipeRepository = new RecipeRepository();

        // GET: Recipe
        public ActionResult Index()
        {
            var recipes = _recipeRepository.GetAllRecipes();
            return View(recipes);
        }

        // GET: Recipe/Details/5
        public ActionResult Details(int id)
        {
            var recipe = _recipeRepository.GetRecipeById(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _recipeRepository.AddRecipe(recipe);
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(int id)
        {
            var recipe = _recipeRepository.GetRecipeById(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _recipeRepository.UpdateRecipe(recipe);
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(int id)
        {
            var recipe = _recipeRepository.GetRecipeById(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _recipeRepository.DeleteRecipe(id);
            return RedirectToAction("Index");
        }
    }
}
