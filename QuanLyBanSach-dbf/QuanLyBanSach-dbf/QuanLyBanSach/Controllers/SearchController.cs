using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using QuanLyBanSach.Data;

namespace QuanLyBanSach.Controllers
{
    public class SearchController : Controller
    {
        BookStore db = new BookStore();

        // GET: Search
        public ActionResult Index(string key, int? costBegin, int? costEnd)
        {
            IQueryable<Product> products;
            if (!string.IsNullOrEmpty(key))
            {
                products = db.Products.Where(p => p.Name.Contains(key));
            }
            else
            {
                products = db.Products;
            }

            if (costBegin != null)
            {
                products = products.Where(p => p.CostPrice >= costBegin);
            }
            if (costEnd != null)
            {
                products = products.Where(p => p.CostPrice <= costEnd);
            }

            ViewData["Categories"] = db.Categories.ToList();
            ViewData["Authors"] = db.Authors.ToList();
            return View(products.ToList());
        }

        //    /// <summary>
        //    /// As same as the view above, but this only return json
        //    /// </summary>
        [HttpGet]
        public ActionResult Search(
            string key
            , int? authorId, int? categoryId
            , int? costBegin, int? costEnd)
        {
            if (costBegin != null)
            {
                costBegin *= 1000;
            }
            if (costEnd != null)
            {
                costEnd *= 1000;
            }
            List<Product> products;
            if (!string.IsNullOrEmpty(key))
            {
                products = db.Products.Where(p => p.Name.Contains(key)).ToList();
            }
            else
            {
                products = db.Products.ToList();
            }

            if (authorId != null)
            {
                //List<Product> tmp = products.ToList();
                //List<Product> tmp2 = new List<Product>(tmp);
                //foreach (var singleProduct in tmp2)
                //{
                //    var details = singleProduct.ProductDetails;
                //    if (details != null)
                //    {
                //        var found = details.Where(pd => pd.AuthorId == authorId);
                //        if (found == null || found.Count() == 0)
                //        {
                //            tmp.Remove(singleProduct);
                //        }
                //    }
                //}
                //if (tmp.Count == 0)
                //    return Json(tmp, JsonRequestBehavior.AllowGet);

                try
                {
                    //products = tmp.AsQueryable();
                    products = db.sp_FilterByAuthorID(authorId).ToList();
                }
                catch (Exception)
                {
                    return Json("Error at tmp.AsQueryable()", JsonRequestBehavior.AllowGet);
                }

            }

            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId).ToList();

                if (products.Count() == 0)
                    return Json(new List<Product>(), JsonRequestBehavior.AllowGet);
            }

            if (costBegin != null)
            {
                products = products.Where(p => p.Price >= costBegin).ToList();
                if (products.Count() == 0)
                    return Json(new List<Product>(), JsonRequestBehavior.AllowGet);
            }
            if (costEnd != null)
            {
                products = products.Where(p => p.Price <= costEnd).ToList();
                if (products.Count() == 0)
                    return Json(new List<Product>(), JsonRequestBehavior.AllowGet);
            }

            var result = new JsonNetResult
            {
                Data = products.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Settings = {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 3// product, productdetail, author
                    }
            };
            return result;
        }
    }


}