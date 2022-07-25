using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapTrinhWeb.DbContext;
using LapTrinhWeb.Models;

namespace LapTrinhWeb.Controllers
{
    public class PaymentController : Controller
    {
        WebBanHangEntities1 objWebBanHangEntities = new WebBanHangEntities1();
        // GET: Payment
        public ActionResult Index()
        {
            if(Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CardModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreateOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWebBanHangEntities.Orders.Add(objOrder);

                objWebBanHangEntities.SaveChanges();
                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach(var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    obj.Price = item.Product.Price;
                    
                    lstOrderDetail.Add(obj);
                }
                objWebBanHangEntities.OrderDetails.AddRange(lstOrderDetail);
                objWebBanHangEntities.SaveChanges();
            }
            return View();
        }
    }
}