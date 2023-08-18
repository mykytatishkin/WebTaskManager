using System.Linq;
using AspNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Действие для отображения списка проектов
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }

        // Действие для создания нового проекта
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // Действие для просмотра деталей проекта
        public IActionResult Details(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // Действие для редактирования проекта
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(int id, Project updatedProject)
        {
            if (ModelState.IsValid)
            {
                var project = _context.Projects.FirstOrDefault(p => p.Id == id);
                if (project == null)
                {
                    return NotFound();
                }

                project.Name = updatedProject.Name;
                project.Description = updatedProject.Description;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(updatedProject);
        }

        // Действие для удаления проекта
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
