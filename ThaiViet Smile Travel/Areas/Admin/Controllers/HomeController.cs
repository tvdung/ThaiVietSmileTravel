using System;
using System.Linq;
using System.Web.Mvc;
using ThaiVietSmileTravel.Models.Framework;

using ThaiViet_Smile_Travel.Areas.Admin.Code;
using ThaiViet_Smile_Travel.Areas.Admin.Models;
using System.Net;
using System.Data.Entity;
using System.Web.Security;
using System.Web;

namespace ThaiVietSmileTravel.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        private TVSTravelDbContext db = new TVSTravelDbContext();

        // GET: Admin/Home
        
        public ActionResult Index()
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            if (Request.Cookies["Login"] != null)
            {
              return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var pass = Common.CommonHelper.Encrypt(model.Password, true);
                var result = db.tbl_Account
                    .Where(x => x.UserName.Equals(model.UserName) && x.Password.Equals(pass))
                    .FirstOrDefault();

                if (result != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    Session["UserId"] = result.UserId;
                    Session.Timeout = 50;
                    HttpCookie cookie = new HttpCookie("Login");
                        cookie.Values.Add("UserName", model.UserName);
                        cookie.Values.Add("Password", model.Password);
                        cookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu sai");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập tên người dùng và mật khẩu");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Login", "Home");
        }

        
        public ActionResult Tour()
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            var model = db.tbl_Tour.ToList();
            return View(model);
        }


        //Tạo mới tour
        
        public ActionResult CreateTour()
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTour([Bind(Include = "Id,TenTourEN,TenTourTL,TenTourVN,DonGia,DonViTinh,NgayKhoiHanh,NgayKetThuc,SoNgay,SoDem,SoCho,NoiDungEN,NoiDungTL,NoiDungVN,HinhAnh,KhuyenMai,TourHot,NgayTao,Language,CategoryId")] tbl_Tour tbl_Tour)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Tour.Add(tbl_Tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Tour);
        }

        //Edit tour
        
        public ActionResult EditTour(int? id)
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tour tbl_Tour = db.tbl_Tour.Find(id);
            if (tbl_Tour == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Tour);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTour([Bind(Include = "Id,TenTourEN,TenTourTL,TenTourVN,DonGia,DonViTinh,NgayDuKien,SoNgay,SoDem,SoCho,NoiDungEN,NoiDungTL,NoiDungVN,HinhAnh,KhuyenMai,TourHot,NgayTao,CategoryId,IsActive")] tbl_Tour tbl_Tour)
        {
            if (ModelState.IsValid)
            {

                db.Entry(tbl_Tour).State = EntityState.Modified;
                db.SaveChanges();
                var model = db.tbl_Tour.ToList();
                return RedirectToAction("Tour", model);
            }
            return View(tbl_Tour);
        }

        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            tbl_Tour tbl_Tour = db.tbl_Tour.Find(id);
            if (tbl_Tour.IsActive)
                tbl_Tour.IsActive = false;
            else
                tbl_Tour.IsActive = true;
            db.SaveChanges();
            var model = db.tbl_Tour.ToList();
            return View("Tour", model);
        }

        //Edit tour
        
        public ActionResult EditAbout(int? id)
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_About tbl_About = db.tbl_About.Find(id);
            if (tbl_About == null)
            {
                return HttpNotFound();
            }
            return View(tbl_About);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAbout([Bind(Include = "Id,NoiDungVN,NoiDungEN,NoiDungTL")] tbl_About tbl_About)
        {
            if (ModelState.IsValid)
            {

                db.Entry(tbl_About).State = EntityState.Modified;
                db.SaveChanges();
                var model = db.tbl_About;
                return RedirectToAction("EditAbout", model);
            }
            return View(tbl_About);
        }

        
        public ActionResult SettingAccount()
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(db.tbl_Account.ToList());
        }

        
        public ActionResult EditAccount(int? id)
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Account tbl_Account = db.tbl_Account.Find(id);
            if (tbl_Account == null)
            {
                return HttpNotFound();
            }
            tbl_Account.Password = null;
            tbl_Account.PasswordEmail = null;
            return View(tbl_Account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount([Bind(Include = "UserId,UserName,Password,RememberMe,Email,PasswordEmail,IsAdmin")] tbl_Account tbl_Account)
        {
            if (ModelState.IsValid)
            {
                tbl_Account.Password = Common.CommonHelper.Encrypt(tbl_Account.Password, true);
                tbl_Account.PasswordEmail = Common.CommonHelper.Encrypt(tbl_Account.PasswordEmail, true);
                tbl_Account.IsAdmin = true;
                db.Entry(tbl_Account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Account);
        }

        //public static string CreateMD5(string input)
        //{
        //    // Use input string to calculate MD5 hash
        //    MD5 md5 = System.Security.Cryptography.MD5.Create();
        //    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        //    byte[] hashBytes = md5.ComputeHash(inputBytes);

        //    // Convert the byte array to hexadecimal string
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < hashBytes.Length; i++)
        //    {
        //        sb.Append(hashBytes[i].ToString("X2"));
        //    }
        //    return sb.ToString();
        //}

        //giỏ hàng
        
        public ActionResult OrderList()
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(db.tbl_Orders.ToList().OrderByDescending(x => x.NgayDat));
        }

        
        public ActionResult OrderDetails(int? id)
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Orders tbl_Orders = db.tbl_Orders.Find(id);
            if (tbl_Orders == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Orders);
        }

        public ActionResult UpdateStatusOrder(int? id)
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Orders tbl_Orders = db.tbl_Orders.Find(id);
            if (tbl_Orders == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (tbl_Orders.TrangThai)
                    tbl_Orders.TrangThai = false;
                else
                    tbl_Orders.TrangThai = true;
                db.SaveChanges();
            }
            var model = db.tbl_Orders.ToList().OrderByDescending(x => x.NgayDat);
            return View("OrderList", model);
        }


        //danh sach lien he
        
        public ActionResult ListContact()
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(db.tbl_Contact.ToList());
        }

        public ActionResult UpdateStatusContact(int? id)
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Contact tblContact = db.tbl_Contact.Find(id);
            if (tblContact == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (tblContact.IsReply)
                    tblContact.IsReply = false;
                else
                    tblContact.IsReply = true;
                db.SaveChanges();
            }
            var model = db.tbl_Contact.ToList().OrderByDescending(x => x.NgayGui);
            return View("ListContact", model);
        }


        //Banner
        
        public ActionResult Banner()
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(db.tbl_Banner.ToList());
        }

        public ActionResult UpdateStatusBanner(int? id)
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Banner tbl_Banner = db.tbl_Banner.Find(id);
            if (tbl_Banner == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (tbl_Banner.IsActive)
                    tbl_Banner.IsActive = false;
                else
                    tbl_Banner.IsActive = true;
                db.SaveChanges();
            }
            var model = db.tbl_Banner.ToList();
            return View("Banner", model);
        }

        
        public ActionResult EditBanner(int? id)
        {
            if (Request.Cookies["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Banner tbl_Banner = db.tbl_Banner.Find(id);
            if (tbl_Banner == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBanner([Bind(Include = "Id,HinhAnh,UrlTour,IsActive")] tbl_Banner tbl_Banner)
        {
            if (ModelState.IsValid)
            {
                tbl_Banner.IsActive = true;
                db.Entry(tbl_Banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Banner");
            }
            return View(tbl_Banner);
        }
    }
}