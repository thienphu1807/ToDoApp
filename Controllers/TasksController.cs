using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly SignInManager<User> _signInManager;

        public TasksController(UserManager<User> userManager, AppDbContext appDbContext, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _signInManager = signInManager;
        }
        // GET: TasksController
        [HttpGet("Tasks/TaskList")]
        public async Task<IActionResult> GetAllTasks()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }
            var taskItem = await _appDbContext.TaskItems.Where(u => u.UserId == user.Id).Include(c => c.Categories).ToListAsync();
            return View(taskItem);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_appDbContext.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: TasksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItemViewModel taskItem)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_appDbContext.Categories, "Id", "CategoryName");
                return View(taskItem);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }

            var task = new TaskItem
            {
                Title = taskItem.Title,
                Description = taskItem.Description,
                Status = taskItem.Status,
                Priority = taskItem.Priority,
                DueDate = taskItem.DueDate,
                CategoriesId = taskItem.CategoriesId,
                UserId = user.Id
            };
            _appDbContext.TaskItems.Add(task);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("TaskList");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_appDbContext.Categories, "Id", "CategoryName");
            var taskItem = _appDbContext.TaskItems.FirstOrDefault(t => t.Id == id);
            return View(taskItem);
        }

        // POST: TasksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskItemViewModel taskItem)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_appDbContext.Categories, "Id", "CategoryName");
                return View(taskItem);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }

            var existingTask = await _appDbContext.TaskItems.FindAsync(taskItem.Id);
            if (existingTask == null)
            {
                return View();
            }

            existingTask.Title = taskItem.Title;
            existingTask.Description = taskItem.Description;
            existingTask.Status = taskItem.Status;
            existingTask.Priority = taskItem.Priority;
            existingTask.DueDate = taskItem.DueDate;
            existingTask.CategoriesId = taskItem.CategoriesId;
            existingTask.UserId = user.Id;

            _appDbContext.TaskItems.Update(existingTask);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("TaskList");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }

            var existingTask = await _appDbContext.TaskItems.FindAsync(id);
            if (existingTask == null)
            {
                return View();
            }

            _appDbContext.TaskItems.Remove(existingTask);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("TaskList");
        }
    }
}
