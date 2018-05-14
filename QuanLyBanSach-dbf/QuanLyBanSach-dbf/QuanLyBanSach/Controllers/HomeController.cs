using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanSach.Controllers
{
    public class HomeController : Controller
    {
        private BookStore db = new BookStore();
        public ActionResult Index()
        {
            ViewData["Authors"] = db.Authors.ToList();
            ViewData["Categories"] = db.Categories.ToList();
            return View();
        }

        
    }
}