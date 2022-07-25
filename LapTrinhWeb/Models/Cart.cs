using LapTrinhWeb.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapTrinhWeb.Models
{
    public class Cart
    {
        WebBanHangEntities1 objWebBanHangEntities = new WebBanHangEntities1();
        public int idProduct { get; set; }
        public string nameProduct { get; set; }

        public string avatarProduct { get; set; }

        public Double dongia { get; set; }
        public int soluong { get; set; }

        public Double Thanhtien
        {
            get { return soluong * dongia; }
        }
    }
}