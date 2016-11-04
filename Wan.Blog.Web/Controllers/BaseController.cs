using System.Web.Mvc;
using Wan.Blog.Domain.Common;
using Wan.Blog.Domain.Entities;

namespace Wan.Blog.Web.Controllers
{
    public class BaseController : Controller
    {
        protected User CurrentUser => GetUser();

        protected CommonRedis Redis = new CommonRedis();

        private User GetUser()
        {
            return Redis.Get<User>(CurrentUserId);
        }

        public string CurrentUserId => GetUserId();

        private string GetUserId()
        {
            return HttpContext.Session?["userId"].ToString();
        }
    }
}