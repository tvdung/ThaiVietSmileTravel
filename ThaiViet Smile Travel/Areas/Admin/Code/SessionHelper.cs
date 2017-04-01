using System.Web;
using System.Web.Mvc;

namespace ThaiViet_Smile_Travel.Areas.Admin.Code
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserId"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}