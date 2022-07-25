using LapTrinhWeb.DbContext;
using LapTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapTrinhWeb.Controllers
{
    public class ProductController : Controller
    {
        WebBanHangEntities1 objWebBanHangEntities = new WebBanHangEntities1();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebBanHangEntities.Products.Where(p => p.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}