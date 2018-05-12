using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanSach.Controllers
{
    public class HomeController : Controller
    {
        private BookStore_RecoveredEntities1 db = new BookStore_RecoveredEntities1();
        public ActionResult Index()
        {
            ViewData["Authors"] = db.Authors.ToList();
            ViewData["Categories"] = db.Authors.ToList();
            return View();
        }

        
    }
}