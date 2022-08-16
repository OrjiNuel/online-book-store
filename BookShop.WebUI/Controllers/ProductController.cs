using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Abstract;
using BookShop.Domain.Entities;
using BookShop.WebUI.Models;

namespace BookShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 5;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        // GET: Product
        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),


                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? 
                        repository.Products.Count() :
                        repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(viewModel);
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            } else
            {
                return null;
            }
        }
    }
}