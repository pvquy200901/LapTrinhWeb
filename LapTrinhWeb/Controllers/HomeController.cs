using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LapTrinhWeb.DbContext;
using LapTrinhWeb.Models;


namespace LapTrinhWeb.Controllers
{
    public class HomeController : Controller
    {
        WebBanHangEntities1 objWebBanHangEntities = new WebBanHangEntities1();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objWebBanHangEntities.Categories.ToList();
            objHomeModel.ListProduct = objWebBanHangEntities.Products.ToList();
            return View(objHomeModel);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objWebBanHangEntities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().Name;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    Session["isAdmin"] = data.FirstOrDefault().IsAdmin;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
       
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWebBanHangEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objWebBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                    objWebBanHangEntities.Users.Add(_user);
                    objWebBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();

        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        public ActionResult ShoppingCart()
        {
            return View((List<CardModel>)Session["cart"]);
        }
        public ActionResult CheckOut()
        {
            return View();
        }
        public ActionResult Shop()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objWebBanHangEntities.Categories.ToList();
            objHomeModel.ListProduct = objWebBanHangEntities.Products.ToList();
            return View(objHomeModel);
        }
        
        public ActionResult Hot()
        {

            return View();
        }

        public ActionResult ChinhSach()
        {

            return View();
        }

        public ActionResult shopMen()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objWebBanHangEntities.Categories.ToList();
            objHomeModel.ListProduct = objWebBanHangEntities.Products.ToList();
            return View(objHomeModel);
        }
        public ActionResult shopWomen()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objWebBanHangEntities.Categories.ToList();
            objHomeModel.ListProduct = objWebBanHangEntities.Products.ToList();
            return View(objHomeModel);
        }
        public ActionResult shopKid()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objWebBanHangEntities.Categories.ToList();
            objHomeModel.ListProduct = objWebBanHangEntities.Products.ToList();
            return View(objHomeModel);
        }



        public ActionResult AddToCard(int id, int quantity)
        {

            if (Session["cart"] == null)
            {
                List<CardModel> cart = new List<CardModel>();
                cart.Add(new CardModel { Product = objWebBanHangEntities.Products.Find(id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CardModel> cart = (List<CardModel>)Session["cart"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CardModel { Product = objWebBanHangEntities.Products.Find(id), Quantity = quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        private int isExist(int id)
        {
            List<CardModel> cart = (List<CardModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(id))
                    return i;
            return -1;
        }


        public ActionResult Remove(int Id)
        {
            List<CardModel> li = (List<CardModel>)Session["cart"];
            li.RemoveAll(x => x.Product.Id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        public int TotalQuanity()
        {
            int total = 0;
            List<CardModel> listCart = Session["cart"] as List<CardModel>;
            if(listCart != null)
            {
                total = listCart.Sum(n => n.Quantity);
            }
            return total;
        }

        public double TotalMoney()
        {
            double totalMoney = 0;
            List<CardModel> listCart = Session["cart"] as List<CardModel>;
            if (listCart != null)
            {
                totalMoney = (double)listCart.Sum(n => (n.Quantity * n.Product.PriceDiscount));
            }
            return totalMoney;
        }
    }
}