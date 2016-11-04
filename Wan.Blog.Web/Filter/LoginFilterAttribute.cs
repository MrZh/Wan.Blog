using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wan.Blog.Web.Filter
{
    public class LoginFilterAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)

        {
            if (HttpContext.Current.Session["userId"] == null)
            {
                httpContext.Response.Redirect("/Home/Index");
                return false;

            }

            return true;
        }
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    if (HttpContext.Current.Session["userId"] == null)
        //    {
        //        filterContext.HttpContext.Response.Redirect("/Home/Index");
        //    }

        //    base.OnAuthorization(filterContext);
        //}
    }
}