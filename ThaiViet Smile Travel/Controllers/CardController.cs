using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using ThaiVietSmileTravel.Common;
using ThaiVietSmileTravel.Globalization;
using ThaiVietSmileTravel.Models.Framework;

using ThaiViet_Smile_Travel.Common;
using ThaiViet_Smile_Travel.Models;
using System.Globalization;

namespace ThaiViet_Smile_Travel.Controllers
{
    public class CardController : BaseController
    {
        private TVSTravelDbContext db = new TVSTravelDbContext();

        // GET: Card
        public ActionResult Index()
        {
            var card = Session[CommonConstants.CardSession];
            var list = new List<CardItemModel>();
            if (card != null)
            {
                list = (List<CardItemModel>)card;
            }

            return View(list);
        }

        public ActionResult AddItem(int tourId, int soLuong)
        {
            var tour = db.tbl_Tour.Find(tourId);
            var card = Session[CommonConstants.CardSession];
            if (card != null)
            {
                var list = (List<CardItemModel>)card;
                if (list.Exists(x => x.Tour.Id != tourId))
                {
                    foreach (var temp in list)
                    {
                        if (temp.Tour.Id == tourId)
                        {
                            return RedirectToAction("index");
                        }
                    }
                    var item = new CardItemModel();
                    item.Tour = tour;
                    item.SoNguoi = soLuong;

                    list.Add(item);
                }
                Session[CommonConstants.CardSession] = list;
            }
            else
            {
                var item = new CardItemModel();
                item.Tour = tour;
                item.SoNguoi = soLuong;
                var list = new List<CardItemModel>();
                list.Add(item);
                Session[CommonConstants.CardSession] = list;
            }
            return RedirectToAction("index");
        }

        public JsonResult Update(string cardModel)
        {
            var jsoncard = new JavaScriptSerializer().Deserialize<List<CardItemModel>>(cardModel);
            var sessionCard = (List<CardItemModel>)Session[CommonConstants.CardSession];

            foreach (var item in sessionCard)
            {
                var jsonitem = jsoncard.SingleOrDefault(x => x.Tour.Id == item.Tour.Id);
                if (jsonitem != null)
                {
                    item.SoNguoi = jsonitem.SoNguoi;
                }
            }
            Session[CommonConstants.CardSession] = sessionCard;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            var sessionCard = (List<CardItemModel>)Session[CommonConstants.CardSession];
            sessionCard.RemoveAll(x => x.Tour.Id == id);
            if (sessionCard.Count == 0)
            {
                Session[CommonConstants.CardSession] = null;
            }
            else
            {
                Session[CommonConstants.CardSession] = sessionCard;
            }
            return Json(new
            {
                status = true
            });
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderTour(string tenKH, string diaChi, string soDT, string email, string datetimepickerOrder, string ghiChu)
        {
            var cart = (List<CardItemModel>)Session[CommonConstants.CardSession];
            var admin = db.tbl_Account.FirstOrDefault();
            if (!string.IsNullOrEmpty(tenKH) && !string.IsNullOrEmpty(diaChi) && !string.IsNullOrEmpty(soDT))
            {
                var order = new tbl_Orders();
                order.TenKH = tenKH;
                order.DiaChi = diaChi;
                order.SoDT = soDT;
                order.Email = email;
                order.NgayDat = DateTime.Now;
                order.NgayCanDi = datetimepickerOrder == null ? DateTime.Now : DateTime.ParseExact(datetimepickerOrder, "yyyy-MM-dd",
                                            new CultureInfo("en-US"),
                                            DateTimeStyles.None);
                order.GhiChu = ghiChu;

                try
                {
                    var id = Insert(order);

                    string tentour = null;
                    int songuoi = 0;
                    foreach (var item in cart)
                    {
                        var orderDetail = new tbl_OrderDetail();
                        orderDetail.TourID = item.Tour.Id;
                        orderDetail.OrderID = id;
                        orderDetail.DonGia = item.Tour.DonGia;
                        orderDetail.SoNguoi = item.SoNguoi;
                        tentour += item.Tour.TenTourTL;
                        songuoi += item.SoNguoi;
                        InsertOrderDetail(orderDetail);
                    }

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/OrderMail/NewOrderTourSendAdmin.cshtml"));
                    content = content.Replace("{{TenKH}}", tenKH);
                    content = content.Replace("{{SoDT}}", soDT);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{DiaChi}}", diaChi);
                    content = content.Replace("{{TenTour}}", tentour);
                    content = content.Replace("{{SoNguoi}}", songuoi.ToString("N0"));

                    if (admin.Email != null)
                    {
                        string contentCusstom = null;
                        if (CommonConstants.CurrentCulture == null)
                        {
                            contentCusstom = System.IO.File.ReadAllText(Server.MapPath("~/Views/OrderMail/NewOrderTourSendCustom.cshtml"));
                            contentCusstom = contentCusstom.Replace("{{TenTour}}", tentour);
                            new MailHelper().SendMail(order.Email, Resource.lblConfigOderTour, null, contentCusstom, false, false);
                        }
                        else if (CommonConstants.CurrentCulture.Equals("vi"))
                        {
                            contentCusstom = System.IO.File.ReadAllText(Server.MapPath("~/Views/OrderMail/NewOrderTourSendCustom_vi.cshtml"));
                            contentCusstom = contentCusstom.Replace("{{TenTour}}", tentour);
                            new MailHelper().SendMail(order.Email, Resource.lblConfigOderTour, null, contentCusstom, false, false);
                        }
                        else
                        {
                            contentCusstom = System.IO.File.ReadAllText(Server.MapPath("~/Views/OrderMail/NewOrderTourSendCustom_en.cshtml"));
                            contentCusstom = contentCusstom.Replace("{{TenTour}}", tentour);
                            new MailHelper().SendMail(order.Email, Resource.lblConfigOderTour, null, contentCusstom, false, false);
                        }
                        var toEmail = admin.Email;

                        new MailHelper().SendMail(toEmail, "Khách hàng đặt tour mới", tenKH, content, false, true);
                        Session[CommonConstants.CardSession] = null;
                        return RedirectToAction("Success");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("OrderTourError");
                }
                return RedirectToAction("OrderTourError");
            }
            else
            {
                ViewBag.ValidateOrder = Resource.vbErrorOderTour;
                return View("Index", cart);
            }
        }

        private int Insert(tbl_Orders order)
        {
            try
            {
                db.tbl_Orders.Add(order);
                db.SaveChanges();
                return order.Id;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
            
        }

        private bool InsertOrderDetail(tbl_OrderDetail orderdetail)
        {
            try
            {
                db.tbl_OrderDetail.Add(orderdetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult OrderTourError()
        {
            return View();
        }
    }
}