using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreContext _context;

        public ProductController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult ProductList()
        {
            var values = _context.Products.Include(x=>x.Category).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var categories = _context.Categories
                                     .Select(c => new SelectListItem
                                     {
                                       Value= c.CategoryId.ToString(),
                                       Text=  c.CategoryName
                                     }).ToList();
            ViewBag.categories = categories;
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteProduct(int id)
        {
            var value = _context.Products.Find(id);
            
                _context.Products.Remove(value);
                _context.SaveChanges();
            
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var categories = _context.Categories
                                 .Select(c => new SelectListItem
                                 {
                                     Value = c.CategoryId.ToString(),
                                     Text = c.CategoryName
                                 }).ToList();
            ViewBag.categories = categories;

            var product = _context.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public IActionResult First5ProductList()
        {
            var values = _context.Products.Include(x=>x.Category).Take(5).ToList();
            return View(values);
        }
    }
}
