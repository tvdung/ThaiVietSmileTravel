using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

using PagedList;

using ThaiVietSmileTravel.Common;
using ThaiVietSmileTravel.Models.Framework;

using ThaiViet_Smile_Travel.Common;
using ThaiViet_Smile_Travel.Models;
using ThaiVietSmileTravel.Globalization;

namespace ThaiViet_Smile_Travel.Controllers
{
    public class HomeController : BaseController
    {
        private TVSTravelDbContext db = new TVSTravelDbContext();

        public ActionResult Index(string searchtour, int page = 1, int pageSize = 6)
        {
            try
            {
                IPagedList<tbl_Tour> result;
                if (!string.IsNullOrEmpty(searchtour))
                {
                    result = db.tbl_Tour
                        .Where(x => x.IsActive && (x.TenTourTL.Contains(searchtour) || x.TenTourVN.Contains(searchtour) || x.TenTourEN.Contains(searchtour)))
                        .OrderByDescending(x => x.NgayTao)
                        .ToPagedList(page, pageSize);
                }
                else
                {
                    result = db.tbl_Tour
                        .Where(x => x.IsActive)
                        .OrderByDescending(x => x.NgayTao)
                        .ToPagedList(page, pageSize);
                }
                if (result.Count == 0)
                {
                    ViewBag.SearchTourNotFound = ThaiVietSmileTravel.Globalization.Resource.SearchTourNotFound;
                }
                return View(result);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Home");
            }
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
                var admin = db.tbl_Account.Where(x => x.IsAdmin).ToList().FirstOrDefault();
                db.tbl_Contact.Add(tbl_Contact);
                db.SaveChanges();
                try
                {
                    //send admin
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/ContactMail/ContactCustomSendAdmin.cshtml"));
                    content = content.Replace("{{TenKH}}", tbl_Contact.Ho + " " + tbl_Contact.Ten);
                    content = content.Replace("{{SoDT}}", tbl_Contact.SoDT);
                    content = content.Replace("{{Email}}", tbl_Contact.Email);
                    content = content.Replace("{{DiaChi}}", tbl_Contact.DiaChi);
                    content = content.Replace("{{TenCompany}}", tbl_Contact.TenCongTy);
                    content = content.Replace("{{Comment}}", tbl_Contact.GhiChu);

                    if (admin != null)
                    {

                        string contentCusstom = null;
                        if (CommonConstants.CurrentCulture == null)
                        {
                            contentCusstom = System.IO.File.ReadAllText(Server.MapPath("~/Views/ContactMail/ContactCustom.cshtml"));
                            new MailHelper().SendMail(tbl_Contact.Email, Resource.lblConfigFromEmailDisplayNameContact, null, contentCusstom, true, true);
                        }
                       else if (CommonConstants.CurrentCulture.Equals("vi"))
                        {
                            contentCusstom = System.IO.File.ReadAllText(Server.MapPath("~/Views/ContactMail/ContactCustom_vi.cshtml"));
                            new MailHelper().SendMail(tbl_Contact.Email, Resource.lblConfigFromEmailDisplayNameContact, null, contentCusstom, true, true);
                        }
                        else
                        {
                            contentCusstom = System.IO.File.ReadAllText(Server.MapPath("~/Views/ContactMail/ContactCustom_en.cshtml"));
                            new MailHelper().SendMail(tbl_Contact.Email, Resource.lblConfigFromEmailDisplayNameContact, null, contentCusstom, true, true);
                        }
                        
                        new MailHelper().SendMail(admin.Email, "Liên hệ từ khách hàng ", tbl_Contact.Ho + " " + tbl_Contact.Ten, content, true, false);
                        Session[CommonConstants.CardSession] = null;
                        return RedirectToAction("SendContactSuccess");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("SendContactError");
                }
                return RedirectToAction("SendContactError");
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

        public ActionResult NotFound()
        {
            return View();
        }
    }
}