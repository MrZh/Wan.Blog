using System;
using System.Web.Mvc;
using Wan.Blog.Domain.Entities;

namespace Wan.Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Blogs.TestString(new Blogs { CreateTime = DateTime.Now, UserId = Guid.NewGuid().ToString() });
            // Blogs.DeleteBlog("1759b276-e693-46b0-bfd2-91053e1f6747");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}