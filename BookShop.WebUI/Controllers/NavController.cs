using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Abstract;

namespace BookShop.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }
        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Products
                                    .Select(x => x.Category)
                                    .Distinct()
                                    .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}