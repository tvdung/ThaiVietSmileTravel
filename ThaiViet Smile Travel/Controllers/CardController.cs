using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.ApplicationInsights.Channel;

using ThaiViet_Smile_Travel.Models;
using ThaiVietSmileTravel.Models.Framework;
using ThaiViet_Smile_Travel.Common;
using ThaiVietSmileTravel.Common;
using System.Configuration;

namespace ThaiViet_Smile_Travel.Controllers
{
    public class CardController : BaseController
    {
        TVSTravelDbContext db = new TVSTravelDbContext();
        
        // GET: Card
        public ActionResult Index()
        {
            var card = Session[CommonConstants.CardSession];
            var list = new List<CardItemModel>();
            if(card != null)
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
                if (list.Exists(x => x.Tour.Id != tourId)){
                    foreach (var temp in list)
                    {
                        if (temp.Tour.Id == tourId)
                            return RedirectToAction("index");
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
                    item.SoNguoi = jsonitem.SoNguoi;
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
            if(sessionCard.Count == 0)
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult OrderTour([Bind(Include = "Id,TenKH,DiaChi,SoDT,Email,NgayDat,NgayCanDi,GhiChu,TrangThai")] tbl_Orders tbl_Orders)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var id = Insert(tbl_Orders);
        //            var cart = (List<CardItemModel>)Session[CommonConstants.CardSession];
        //            string tentour = null;
        //            int songuoi = 0;
        //            foreach (var item in cart)
        //            {
        //                var orderDetail = new tbl_OrderDetail();
        //                orderDetail.TourID = item.Tour.Id;
        //                orderDetail.OrderID = id;
        //                orderDetail.DonGia = item.Tour.DonGia;
        //                orderDetail.SoNguoi = item.SoNguoi;
        //                tentour += item.Tour.TenTourVN;
        //                songuoi += item.SoNguoi;
        //                InsertOrderDetail(orderDetail);
        //            }

        //            string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/NewOrderTour.cshtml"));
        //            content = content.Replace("{{TenKH}}", tbl_Orders.TenKH);
        //            content = content.Replace("{{SoDT}}", tbl_Orders.SoDT);
        //            content = content.Replace("{{Email}}", tbl_Orders.Email);
        //            content = content.Replace("{{DiaChi}}", tbl_Orders.DiaChi);
        //            content = content.Replace("{{TenTour}}", tentour);
        //            content = content.Replace("{{SoNguoi}}", songuoi.ToString("N0"));

        //            var toEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
        //            new MailHelper().SendMail(tbl_Orders.Email, "Đơn hàng mới từ website thaivietsmile", tbl_Orders.TenKH, content, 0, 1);
        //            new MailHelper().SendMail(toEmail, "Đơn hàng mới từ website thaivietsmile", tbl_Orders.TenKH, content, 0, 0);
        //        }
        //        catch (Exception ex)
        //        {
        //            return RedirectToAction("OrderTourError");
        //        }
        //        Session[CommonConstants.CardSession] = null;
        //    }

        //    return View("Index");
        //}

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderTour(string tenKH, string diaChi, string soDT, string email, DateTime? datetimepickerOrder, string ghiChu)
        {
            var cart = (List<CardItemModel>)Session[CommonConstants.CardSession];
            var admin = new TVSTravelDbContext().tbl_Administrator.Where(x => x.IsAdmin);
            if (!string.IsNullOrEmpty(tenKH) && !string.IsNullOrEmpty(diaChi) && !string.IsNullOrEmpty(soDT))
            {
                var order = new tbl_Orders();
                order.TenKH = tenKH;
                order.DiaChi = diaChi;
                order.SoDT = soDT;
                order.Email = email;
                order.NgayDat = DateTime.Now;
                order.NgayCanDi = (DateTime)(datetimepickerOrder == null ? DateTime.Now : datetimepickerOrder);
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

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/NewOrderTour.cshtml"));
                    content = content.Replace("{{TenKH}}", tenKH);
                    content = content.Replace("{{SoDT}}", soDT);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{DiaChi}}", diaChi);
                    content = content.Replace("{{TenTour}}", tentour);
                    content = content.Replace("{{SoNguoi}}", songuoi.ToString("N0"));

                    var tblAdministrator = admin.FirstOrDefault();
                    if (tblAdministrator != null)
                    {
                        var toEmail = tblAdministrator.Email;
                        //new MailHelper().SendMail(email, @ThaiVietSmileTravel.Globalization.Resource.lblConfigOderTour, tenKH, content, 0, 1);
                        new MailHelper().SendMail(toEmail, @ThaiVietSmileTravel.Globalization.Resource.lblSubOderTour, tenKH, content, 0, 0);
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("OrderTourError");
                }
                Session[CommonConstants.CardSession] = null;
            }
            else
            {
                ViewBag.ValidateOrder = ThaiVietSmileTravel.Globalization.Resource.vbErrorOderTour;
                return View("Index", cart);
            }
            return RedirectToAction("Success");
        }

        private int Insert(tbl_Orders order)
        {
            db.tbl_Orders.Add(order);
            db.SaveChanges();
            return order.Id;
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