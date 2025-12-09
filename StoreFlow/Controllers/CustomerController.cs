using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

namespace StoreFlow.Controllers
{
    public class CustomerController : Controller
    {
        private readonly StoreContext _context;

        public CustomerController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult CustomerListOrderByCustomerName()
        {
            var values = _context.Customers.OrderBy(x => x.CustomerName + x.CustomerSurname).ToList(); // Müşteri adı ve soyadına göre sıralanmış müşteri listesi
            return View(values);
        }

        public IActionResult CustomerListOrderByDescBalance()
        {
            var values = _context.Customers.OrderByDescending(x=>x.CustomerBalance).ToList(); // Müşteri bakiyesine göre azalan sıralanmış müşteri listesi
            return View(values);
        }

        public IActionResult CustomerGetByCity(string city)
        {
            var exist = _context.Customers.Any(x=>x.CustomerCity==city); // Belirtilen şehirde en az 1 tane müşteri var mı?
            if (exist)
            {
                ViewBag.message = $"{city} şehrinde en az 1 tane müşteri var";
            }
            else
            {
                ViewBag.message = $"{city} şehrinde hiç müşteri yok";
            }
           return View();
        }


        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
           
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerList");
        }

        public IActionResult DeleteCustomer(int id)
        {
            var Customer = _context.Customers.Find(id);
            if (Customer != null)
            {
                _context.Customers.Remove(Customer);
                _context.SaveChanges();
            }
            return RedirectToAction("CustomerList");
        }

        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            var Customer = _context.Customers.Find(id);
            return View(Customer);
        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer Customer)
        {
            _context.Customers.Update(Customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerList");
        }

        public IActionResult CustomerListByCity()
        {
            var groupedCustomers = _context.Customers.ToList().GroupBy(c => c.CustomerCity)
                .ToList();
            return View(groupedCustomers);
        }

        public IActionResult CustomersByCityCount()
        {
            var query = 
                from c in _context.Customers
                group c by c.CustomerCity into cityGroup
                select new CustomerCityGroup
                {
                    City = cityGroup.Key,
                    CustomerCount = cityGroup.Count()
                };
            var result = query.ToList();
            return View(result);
        }

        public IActionResult CustomerCityList()
        { 
            var values = _context.Customers.Select(x => x.CustomerCity).Distinct().ToList(); // Müşteri şehirlerinin tekil listesi
            return View(values);
        }

        public IActionResult ParallelCustomers()
        {
            var customers = _context.Customers.ToList();

            var result = customers.AsParallel().Where(c => c.CustomerCity.StartsWith("A",StringComparison.OrdinalIgnoreCase)).ToList(); // Şehir adı "A" harfi ile başlayan müşteriler (büyük/küçük harf duyarsız)

            return View(result);
        }

        public IActionResult CustomerListExceptCityIstanbul()
        {
            var allCustomers = _context.Customers.ToList();
            var customersListInIstanbul = _context.Customers.Where(x => x.CustomerCity == "Istanbul")
                .Select(c => c.CustomerCity)
                .ToList();
            var result = allCustomers.ExceptBy(customersListInIstanbul, c=>c.CustomerCity).ToList(); // İstanbul şehri dışındaki müşteriler

            return View(result);
        }

        public IActionResult CustomerListWithDefaultIfEmpty()
        {
            var customers = _context.Customers.Where(x=>x.CustomerCity=="Balıkesir").ToList().DefaultIfEmpty(new Customer
            {
                CustomerId= 0,
                CustomerName = "Kayıt Yok",
                CustomerSurname = "------",
                CustomerCity = "Şehir Yok"
            }).ToList(); // Ankara şehrinde müşteri yoksa varsayılan müşteri bilgisi
            return View(customers);
        }

        public IActionResult CustomerIntersectByCity()
        {
         var cityValues1 = _context.Customers.Where(x =>x.CustomerCity == "İstanbul").Select(y => y.CustomerName + " " + y.CustomerSurname).ToList();

            var cityValues2 = _context.Customers.Where(x => x.CustomerCity == "Ankara").Select(y => y.CustomerName + " " + y.CustomerSurname).ToList();

            var intersectValues = cityValues1.Intersect(cityValues2).ToList(); // Hem İstanbul'da hem de Ankara'da bulunan müşteriler
            return View(intersectValues);
        }

        public IActionResult CustomerCastExample()
        {
            //Cast() metodu, bir koleksiyondaki öğeleri belirtilen türe dönüştürmek için kullanılır. View sayfasında örnek gösterim yapılacaktır.
            var values = _context.Customers.ToList();
            ViewBag.v = values;
            return View();
        }

        public IActionResult CustomerListWithIndex()
        {
            var customers = _context.Customers
                .ToList()
                .Select((c,index) => new
                {
                    SiraNo= index + 1,
                     c.CustomerName,
                     c.CustomerSurname,
                     c.CustomerCity
                }).ToList(); // Müşteri listesi ile birlikte indeks numarası
            return View(customers);
        }

    }
}
