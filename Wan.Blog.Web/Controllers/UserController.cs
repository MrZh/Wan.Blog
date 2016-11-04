using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using Wan.Blog.Domain.Entities;
using Wan.Blog.Web.Filter;

namespace Wan.Blog.Web.Controllers
{


    public class UserController : BaseController
    {

        [LoginFilter]
        // GET: Blog
        public string Index()
        {
            User user = this.CurrentUser;
            return JsonConvert.SerializeObject(user);
        }

        [LoginFilter]
        public ActionResult Login()
        {
            Session["userId"] = "c25aa88e-1426-42e8-8278-363d19a0ac23";
            this.Redis.Insert<User>("c25aa88e-1426-42e8-8278-363d19a0ac23", new User { Id = "c25aa88e-1426-42e8-8278-363d19a0ac23", UserName = "" });
            return RedirectToAction("Index", "Home");
        }
    }
}