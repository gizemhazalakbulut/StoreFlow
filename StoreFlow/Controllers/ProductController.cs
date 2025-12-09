using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

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
            var values = _context.Products.Include(x=>x.Category).Take(5).ToList(); //İlk 5 ürünü getir
            return View(values);
        }

        public IActionResult Skip4ProductList()
        {
            var values = _context.Products.Include(x=>x.Category).Skip(4).Take(10).ToList(); //4 ürünü atla 10 ürün getir
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProductWithAttach()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProductWithAttach(Product product) //ürün ekleme attach metodu ile
        {
            var category = new Category { CategoryId=1 }; //var olan bir kategoriyi kullanarak ürün ekleme
            _context.Categories.Attach(category);
            var productValue = new Product
            {
                ProductName= product.ProductName,
                ProductPrice= product.ProductPrice,
                ProductStock= product.ProductStock,
                Category= category
            };
            _context.Products.Add(productValue);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }
        
        public IActionResult ProductCount()
        {
            var value = _context.Products.LongCount(); //ürün sayısını long olarak getirir
            var lastProduct  =_context.Products.OrderBy(x => x.ProductId).Last(); //son ürünü getirir
            ViewBag.v2 = lastProduct.ProductName;
            ViewBag.v = value;
            return View();
        }

        public IActionResult ProductListWithCategory()
        {
            var result = from c in _context.Categories
                         join p in _context.Products
                         on c.CategoryId equals p.CategoryId
                         select new ProductWithCategoryViewModel
                         {
                           ProductName= p.ProductName,
                           ProductStock=  p.ProductStock,
                           CategoryName=  c.CategoryName
                         };
         
            return View(result.ToList());
        }

    }
}
