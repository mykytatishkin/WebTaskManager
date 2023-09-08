using Microsoft.AspNetCore.Mvc;
using WebTaskManagerApp.Models;

namespace WebTaskManagerApp.Controllers
{
    public class UserController : Controller
    {
        private readonly TaskManagerContext _context;
        private readonly ILogger<UserController> _logger;
        public UserController(TaskManagerContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
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
        public IActionResult Registration(User user, IFormFile Avatar)
        {
            string base64 = string.Empty;
            if (Avatar != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Avatar.OpenReadStream().CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    byte[] data = ms.ToArray();
                    base64 = Convert.ToBase64String(data);
                    user.Avatar = base64;
                }
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            // Csookie
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
            _logger.LogInformation(string.Format("&&& Login() starded with params id:{0}, name:{1}, email: {2}, password: {3}", user.Id, user.Name, user.Email, user.Password));


#pragma warning disable CS8600

            User userInDb = _context.Users.Where(
                   u => u.Name == user.Name
                || u.Email == user.Email
                && u.Password == user.Password
            ).FirstOrDefault();

#pragma warning restore CS8600

            if (userInDb == null)
            {
                _logger.LogWarning(string.Format("&&& Failed login for email {0}[name {1}] with pass {2}", user.Email, user.Name, user.Password));
                return RedirectToAction("Registration");
            }
            else
            {
                HttpContext.Session.SetString("LoggedName", userInDb.Name);
                HttpContext.Session.SetInt32("LoggedId", userInDb.Id);
                HttpContext.Session.SetInt32("RoleId", userInDb.RoleId);
                _logger.LogInformation(string.Format("&&& Succedeed login for email {0}[name {1}] with pass {2}", user.Email, user.Name, user.Password));
                return RedirectToAction("Index");
            }
        }

        public IActionResult SetCompleted(int Id, int Delta)
        {
            _logger.LogInformation(string.Format("&&& SetCompleted() starded with params id:{0}, Delta:{1}", Id, Delta));
            
            var task = _context.Tasks.Find(Id);
            if(task != null)
            {
                _logger.LogInformation(string.Format("&&& Task {0} status changed from {1} to {2} by {3}", task.Name, task.Competed, Delta, task.AsigneeId));
                task.Competed = Delta;
                _context.Update(task);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult MyPage()
        {
            // Partial Views

            return View();
        }
    }
}
