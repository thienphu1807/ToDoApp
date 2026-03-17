using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllTask()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var taskItem = await _appDbContext.TaskItems.Where(u => u.UserId == user.Id).ToListAsync();
            return View(taskItem);
        }

        // GET: TasksController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var taskItem = await _appDbContext.TaskItems.Where(u => u.UserId == user.Id && u.Id == id).FirstOrDefaultAsync();
            return View(taskItem);
        }
        // POST: TasksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] TaskItem taskItem)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
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

            return View(task);
        }
    }
}
