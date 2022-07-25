using LapTrinhWeb.DbContext;
using LapTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapTrinhWeb.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        WebBanHangEntities1 objWebBanHangEntities = new WebBanHangEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductCategory(int Id)
        {
            var ListProduct = objWebBanHangEntities.Products.Where(p => p.CategoryId == Id).ToList();
            return View(ListProduct);

        }
    }
}