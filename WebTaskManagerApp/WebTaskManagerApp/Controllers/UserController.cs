using Microsoft.AspNetCore.Mvc;
using WebTaskManagerApp.Models;

namespace WebTaskManagerApp.Controllers
{
    public class UserController : Controller
    {
        private readonly TaskManagerContext _context;
        public UserController(TaskManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Registration(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            User userInDb = _context.Users.Where(u => u.Name == u.Name &&
                u.Password == user.Password).FirstOrDefault();
            if(userInDb == null)
            {
                return RedirectToAction("Registration");
            }
            else
            {

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
