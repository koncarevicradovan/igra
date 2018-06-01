using System;
using System.Net.Http;
using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Igra.Controllers
{

    internal class CheckLoginFilterAttribute : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.Filters.IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            try
            {
                string username = (string)filterContext.HttpContext.Session["username"];
                if (username == null || username.Length == 0)
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            try
            {
                string username = (string)filterContext.HttpContext.Session["username"];
                if (username == null || username.Length == 0)
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return;
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                string username = (string)filterContext.HttpContext.Session["username"];
                if (username == null || username.Length == 0)
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}