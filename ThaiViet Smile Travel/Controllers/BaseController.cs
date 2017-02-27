using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThaiViet_Smile_Travel.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if(Session[Common.CommonConstants.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[Common.CommonConstants.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[Common.CommonConstants.CurrentCulture].ToString());
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi");
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

            Session[Common.CommonConstants.CurrentCulture] = valueCulture;
            return Redirect(returnUrl);
        }
    }
}