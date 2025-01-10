using Microsoft.AspNetCore.Mvc;

namespace BigBlog.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
