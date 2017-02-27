using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ThaiViet_Smile_Travel.Common;
using ThaiViet_Smile_Travel.Models;
using ThaiVietSmileTravel.Models.Framework;
using System.IO;
using System.Net.Mime;
using System.Windows.Forms;

using PagedList;

using ThaiVietSmileTravel.Common;

namespace ThaiViet_Smile_Travel.Controllers
{
    public class HomeController : BaseController
    {
        private TVSTravelDbContext db = new TVSTravelDbContext();

        public ActionResult Index(int page = 1, int pageSize = 6)
        {
            var result = db.tbl_Tour
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.NgayTao)
                .ToPagedList(page, pageSize);
            return View(result);
        }

        public object Application { get; set; }

        public ActionResult About()
        {
            var result = db.tbl_About.ToList();
            return View(result);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Category()
        {
            var model = db.tbl_Categories;

            return PartialView("_Category", model);
        }

        [ChildActionOnly]
        public PartialViewResult CartInfo()
        {
            var card = Session[CommonConstants.CardSession];
            var list = new List<CardItemModel>();
            if (card != null)
            {
                list = (List<CardItemModel>)card;
            }
            return PartialView(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactResult([Bind(Include = "Id,Ho,Ten,Email,TenCongTy,DiaChi,SoDT,GhiChu")] tbl_Contact tbl_Contact)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Contact.Add(tbl_Contact);
                db.SaveChanges();
                try
                {

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/ContactCustom.cshtml"));
                    content = content.Replace("{{TenKH}}", tbl_Contact.Ho + " " + tbl_Contact.Ten);
                    content = content.Replace("{{SoDT}}", tbl_Contact.SoDT);
                    content = content.Replace("{{Email}}", tbl_Contact.Email);
                    content = content.Replace("{{DiaChi}}", tbl_Contact.DiaChi);
                    content = content.Replace("{{TenCompany}}", tbl_Contact.TenCongTy);
                    content = content.Replace("{{Comment}}", tbl_Contact.GhiChu);

                    var toEmail = ConfigurationManager.AppSettings["FromEmailAddress"];
                    //new MailHelper().SendMail(tbl_Contact.Email, "Xác nhận liên hệ tới website thaivietsmile", tbl_Contact.Ho + " " + tbl_Contact.Ten, content, 1, 1);
                    new MailHelper().SendMail(toEmail, "Liên hệ từ khách hàng ", tbl_Contact.Ho + " " + tbl_Contact.Ten, content, 1, 0);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("SendContactError");
                }
                Session[CommonConstants.CardSession] = null;
                return RedirectToAction("SendContactSuccess");
            }

            return View("Contact");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ContactResult(string firstName, string lastName, string email, string company, string phoneNumber, string adress, string comments)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var contact = new tbl_Contact();
        //        contact.Ho = firstName;
        //        contact.Ten = lastName;
        //        contact.Email = email;
        //        contact.SoDT = phoneNumber;
        //        contact.DiaChi = adress;
        //        contact.TenCongTy = company;
        //        contact.GhiChu = comments;

        //        try
        //        {

        //            db.SaveChanges();
        //            string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/ContactCustom.cshtml"));
        //            content = content.Replace("{{TenKH}}", firstName + " " + lastName);
        //            content = content.Replace("{{SoDT}}", phoneNumber);
        //            content = content.Replace("{{Email}}", email);
        //            content = content.Replace("{{DiaChi}}", adress);
        //            content = content.Replace("{{TenCompany}}", company);
        //            content = content.Replace("{{Comment}}", comments);

        //            var toEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
        //            new MailHelper().SendMail(email, "Xác nhận liên hệ tới website thaivietsmile", firstName + " " + lastName, content, 1, 1);
        //            new MailHelper().SendMail(toEmail, "Liên hệ khách hàng từ website thaivietsmile", firstName + " " + lastName, content, 1, 0);
        //        }
        //        catch (Exception ex)
        //        {
        //            return RedirectToAction("SendContactError");
        //        }
        //        Session[CommonConstants.CardSession] = null;
        //        return RedirectToAction("SendContactSuccess");
        //    }
        //    return View();
        //}

        public ActionResult SendContactSuccess()
        {
            return View();
        }

        public ActionResult SendContactError()
        {
            return View();
        }

        public PartialViewResult _AdBanner()
        {
            var result = db.tbl_Banner
                .Where(x => x.IsActive)
                .ToList();
            return PartialView(result);
        }
    }
}