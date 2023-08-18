using System.Linq;
using AspNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Действие для отображения списка задач
        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }

        // Действие для создания новой задачи
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // Действие для просмотра деталей задачи
        public IActionResult Details(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // Действие для редактирования задачи
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(int id, Models.Task updatedTask)
        {
            if (ModelState.IsValid)
            {
                var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                {
                    return NotFound();
                }

                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(updatedTask);
        }

        // Действие для удаления задачи
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
