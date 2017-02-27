using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Web.Security;

using ThaiVietSmileTravel.Models;
using ThaiVietSmileTravel.Models.Framework;

using ThaiViet_Smile_Travel.Areas.Admin.Code;
using ThaiViet_Smile_Travel.Areas.Admin.Models;
using System.Net;
using System.Data.Entity;

namespace ThaiVietSmileTravel.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private TVSTravelDbContext db = new TVSTravelDbContext();

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = db.tbl_Administrator
                    .Where(x => x.UserName.Equals(model.UserName) && x.Password.Equals(model.Password))
                    .FirstOrDefault();
                if (result != null)
                {
                    SessionHelper.SetSession(new UserSession()
                    {
                        UserName = model.UserName
                    });
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
            SessionHelper.SetSession(null);
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Tour()
        {
            var model = db.tbl_Tour.ToList();
            return View(model);
        }


        //Tạo mới tour
        public ActionResult CreateTour()
        {
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
            tbl_Tour tbl_Tour = db.tbl_Tour.Find(id);
            tbl_Tour.IsActive = false;
            db.SaveChanges();
            var model = db.tbl_Tour.ToList();
            return View("Tour", model);
        }

        //Edit tour
        public ActionResult EditAbout(int? id)
        {
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
    }
}