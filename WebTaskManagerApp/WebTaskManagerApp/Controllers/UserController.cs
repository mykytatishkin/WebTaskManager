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
            string loggedName = HttpContext.Session.GetString("LoggedName");
            ViewBag.LoggedName = loggedName;
            int? loggedId = HttpContext.Session.GetInt32("LoggedId");
            var tasks = _context.Tasks.Where(t => t.AsigneeId == loggedId).ToList();

            var ids = tasks.Select(t => new { Pid = t.ProjectId }).Distinct().ToList();
            List<TaskViewModel> list = new List<TaskViewModel>();
            foreach(var item in ids)
            {
                TaskViewModel tvm = new TaskViewModel()
                {
                    CurrentProject = _context.Project.Find(item.Pid),
                    LinkedTasks = _context.Tasks.Where(t => t.ProjectId == item.Pid && t.AsigneeId == loggedId).ToList()
                };
                list.Add(tvm);
            }
            return View(list);
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
            HttpContext.Session.SetString("LoggedName", user.Name);
            HttpContext.Session.SetInt32("LoggedId", user.Id);
            HttpContext.Session.SetInt32("RoleId", user.RoleId);

            return RedirectToAction("Index");
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
            User userInDb = _context.Users.Where(u => u.Name == user.Name &&
                u.Password == user.Password).FirstOrDefault();
            if(userInDb == null)
            {
                return RedirectToAction("Registration");
            }
            else
            {
                HttpContext.Session.SetString("LoggedName", userInDb.Name);
                HttpContext.Session.SetInt32("LoggedId", userInDb.Id);
                HttpContext.Session.SetInt32("RoleId", userInDb.RoleId);

                return RedirectToAction("Index");
            }
        }

        public IActionResult SetCompleted(int Id, int Delta)
        {
            var task = _context.Tasks.Find(Id);
            if(task != null)
            {
                task.Competed = Delta;
                _context.Update(task);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
