using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using PagedList;

using ThaiVietSmileTravel.Models.Framework;

namespace ThaiViet_Smile_Travel.Controllers
{
    public class TourController : BaseController
    {
        TVSTravelDbContext db = new TVSTravelDbContext();
        // GET: Tour

        public ActionResult Index(int categoryId, int page = 1, int pageSize = 6)
        {
            var category = db.tbl_Categories
                .Find(categoryId);
            if(category != null)
            {
                ViewBag.TitleCategory = category.TenVN;
            var results = db.tbl_Tour
                .Where(x => x.CategoryId == categoryId && x.IsActive)
                .OrderByDescending(x => x.NgayTao)
                .ToPagedList(page, pageSize);
                return View(results);
            }
            return View();
        }

        public ActionResult Detail(int id)
        {
            if (id != 0)
            {
                var result = db.tbl_Tour
                .Where(x => x.Id == id)
                .FirstOrDefault();
                return View(result);
            }
            return View();
        }
    }
}