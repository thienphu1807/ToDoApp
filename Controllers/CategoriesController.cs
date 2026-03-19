using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly SignInManager<User> _signInManager;

        public CategoriesController(UserManager<User> userManager, AppDbContext appDbContext, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _signInManager = signInManager;
        }
        // GET: TasksController
        public async Task<IActionResult> GetAllCategories()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }
            var categories = await _appDbContext.Categories.ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TasksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }

            var categoriesItem = new Categories
            {
                CategoryName = categories.CategoryName
            };
            _appDbContext.Categories.Add(categoriesItem);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(t => t.Id == id);
            return View(category);
        }

        // POST: TasksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categories categories)
        {

            var existingCategories = await _appDbContext.Categories.FindAsync(categories.Id);
            if (existingCategories == null)
            {
                return View();
            }

            existingCategories.CategoryName = categories.CategoryName;

            _appDbContext.Categories.Update(existingCategories);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("GetAllCategories");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewBag.Categories = new SelectList(_appDbContext.Categories, "Id", "CategoryName");
            //    return View(taskItem);
            //}

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }

            var existingCategories = await _appDbContext.Categories.FindAsync(id);
            if (existingCategories == null)
            {
                return View();
            }

            _appDbContext.Categories.Remove(existingCategories);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("GetAllCategories");
        }
    }
}
