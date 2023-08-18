using System.Linq;
using AspNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Действие для отображения списка пользователей
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // Действие для создания нового пользователя
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Действие для просмотра деталей пользователя
        public IActionResult Details(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Действие для редактирования пользователя
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(int id, User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = updatedUser.UserName;
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(updatedUser);
        }

        // Действие для удаления пользователя
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
