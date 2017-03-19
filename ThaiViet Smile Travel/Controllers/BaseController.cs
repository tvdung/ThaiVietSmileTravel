using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

using ThaiViet_Smile_Travel.Common;

namespace ThaiViet_Smile_Travel.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (CommonConstants.CurrentCulture != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(CommonConstants.CurrentCulture.ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(CommonConstants.CurrentCulture.ToString());
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("th");
            }
            double khach = 0;
            TextReader tr = new StreamReader(Server.MapPath("~/luottruycap.txt"));
            khach = Convert.ToDouble(tr.ReadLine());
            tr.Close();
            tr.Dispose();
            try
            {
                khach++;
                TextWriter tw = new StreamWriter(Server.MapPath("~/luottruycap.txt"));
                tw.Write(khach);
                tw.Close();
                tw.Dispose();
            }
            catch (Exception)
            {
            }
            ViewBag.LuotTruyCap = khach.ToString();
        }

        //change culture
        public ActionResult ChangeCulture(string valueCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(valueCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(valueCulture);

            CommonConstants.CurrentCulture = valueCulture;
            return Redirect(returnUrl);
        }
    }
}