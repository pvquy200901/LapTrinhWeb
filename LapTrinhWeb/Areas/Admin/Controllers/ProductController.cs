using LapTrinhWeb.DbContext;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LapTrinhWeb.Areas.Admin.Controllers
{
   
    public class ProductController : Controller
    {
        WebBanHangEntities1 objWebBanHangEntities = new WebBanHangEntities1();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var lstProduct = objWebBanHangEntities.Products.ToList();
            return View(lstProduct);
        }
        public ActionResult Details(int Id)
        {
            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }

        public ActionResult Create()
        {
            //get list category 
           
            Product objCourse = new Product();
            objCourse.ListCategory = objWebBanHangEntities.Categories.ToList();
            return View(objCourse);
        }

        
        [HttpPost]
      
        public ActionResult Create(Product objProduct)
        {

          
                try
                {
                    if (objProduct.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                        //ten hinh
                        string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                        //png
                        fileName = fileName  + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddmmss")) + extension;
                        //tenhinh.png
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));
                    }

                    objProduct.ListCategory = objWebBanHangEntities.Categories.ToList();
                    objProduct.CreateOnUtc = DateTime.Now;
                    objWebBanHangEntities.Products.Add(objProduct);
                    objWebBanHangEntities.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            
           
        }
    
        public ActionResult Edit(int id)
        {
            
            Product objCourse = objWebBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            objCourse.ListCategory = objWebBanHangEntities.Categories.ToList();
            
            return View(objCourse);
           
            
          
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product objproduct)
        {


            if (objproduct.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpLoad.FileName);
                //ten hinh
                string extension = Path.GetExtension(objproduct.ImageUpLoad.FileName);
                //png
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddmmss")) + extension;
                //tenhinh.png
                objproduct.Avatar = fileName;
                objproduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));

            }

            objWebBanHangEntities.Entry(objproduct).State = EntityState.Modified;
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");

        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var webBanHangEntities = objWebBanHangEntities.Products.Find(id);
            if (webBanHangEntities == null)
            {
                return HttpNotFound();
            }
            return View(webBanHangEntities);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var webBanHangEntities = objWebBanHangEntities.Products.Find(id);
            objWebBanHangEntities.Products.Remove(webBanHangEntities);
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}