using ASP.NET_Core_Identity_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_Identity_Demo.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }
                
        
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;

            var products = _repo.GetAllProducts();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }

            products = sortOrder switch
            {
                "" => products.OrderBy(s => s.Name),
                "name_desc" => products.OrderByDescending(s => s.Name),
                "Price" => products.OrderBy(s => s.Price),
                "price_desc" => products.OrderByDescending(s => s.Price),
                _ => products.OrderBy(s => s.ProductID),
            };
            return View(products);
        }

        [Authorize]
        public IActionResult ViewProduct(int id)
        {
            var product = _repo.GetProduct(id);

            return View(product);
        }

        [Authorize]
        public IActionResult UpdateProduct(int id)
        {
            Product prod = _repo.GetProduct(id);

            _repo.UpdateProduct(prod);

            if (prod == null)
            {
                return View("ProductNotFound");
            }

            return View(prod);
        }

        [Authorize]
        public IActionResult UpdateProductToDatabase(Product product)
        {
            _repo.UpdateProduct(product);

            return RedirectToAction("ViewProduct", new { id = product.ProductID });
        }

        [Authorize]
        public IActionResult InsertProduct()
        {
            var prod = _repo.AssignCategory();

            return View(prod);
        }

        [Authorize]
        public IActionResult InsertProductToDatabase(Product productToInsert)
        {
            _repo.InsertProduct(productToInsert);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult DeleteProduct(Product product)
        {
            _repo.DeleteProduct(product);

            return RedirectToAction("Index");
        }


    }
}
