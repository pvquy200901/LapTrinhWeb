using LapTrinhWeb.DbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LapTrinhWeb.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        WebBanHangEntities1 objWebBanHangEntities = new WebBanHangEntities1();
        // GET: Admin/User
        public ActionResult Index()
        {
            var lstUser = objWebBanHangEntities.Users.ToList();
            return View(lstUser);
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public ActionResult Create(User user)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objWebBanHangEntities.Users.Add(user);
                    objWebBanHangEntities.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var webBanHangEntities = objWebBanHangEntities.Users.Find(id);
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
            var webBanHangEntities = objWebBanHangEntities.Users.Find(id);
            objWebBanHangEntities.Users.Remove(webBanHangEntities);
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            User user = objWebBanHangEntities.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Password,IsAdmin,Adress,Phone,UserName")] User user)
        {

            if (ModelState.IsValid)
            {
                try
                {  
                    objWebBanHangEntities.Entry(user).State = EntityState.Modified;
                    objWebBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}