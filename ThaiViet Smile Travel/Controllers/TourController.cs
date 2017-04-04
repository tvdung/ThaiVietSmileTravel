using System;
using System.Linq;
using System.Web.Mvc;

using PagedList;

using ThaiVietSmileTravel.Models.Framework;

namespace ThaiViet_Smile_Travel.Controllers
{
    public class TourController : BaseController
    {
        private TVSTravelDbContext db = new TVSTravelDbContext();

        // GET: Tour

        public ActionResult Index(int? categoryId, int page = 1, int pageSize = 6)
        {
            try
            {
                var category = db.tbl_Categories
                    .Find(categoryId);
                if (category != null)
                {
                    ViewBag.TitleCategory = category.TenVN;
                    var query = db.tbl_Tour
                        .Where(x => x.CategoryId == categoryId && x.IsActive)
                        .OrderByDescending(x => x.NgayTao)
                        .ToList();
                    var results = query.ToPagedList(page, pageSize);
                    return View(results);
                }
                else
                {
                    return RedirectToAction("NotFound", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View();
        }

        public ActionResult Detail(int? id)
        {
            if (id != 0)
            {
                var result = db.tbl_Tour
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
                return View(result);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View();
        }
    }
}