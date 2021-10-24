using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp2020.Public
{
    // Inheritance   currentClassName : baseClassName
    public class MyAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["UserID"] == null)
            {
                return false;
            }
            else
                return true;
        }
        ///Login failed or NOT logined
        ///
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("~/Admin/Login");
        }
    }
}