using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers
{
    public class TodoController : Controller
    {
        private readonly StoreContext _context;

        public TodoController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            _context.Todos.AddRange();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateToDo()
        {
            var todos = new List<Todo>
            {
                new Todo { Description = "Mail gönder", Status = true, Priority = "Birincil" },
                new Todo { Description = "Rapor hazırla", Status = true, Priority = "İkincil" },
                new Todo { Description = "Toplantıya katıl", Status = false, Priority = "Birincil" }
            };

            await _context.Todos.AddRangeAsync(todos);
            await _context.SaveChangesAsync();

            return View();
        }


        public IActionResult TodoAggreagatePriority()
        {
            var priorityFirstlyTodo = _context.Todos
                 .Where(x => x.Priority == "Birincil")
                .Select(y => y.Description)
                .ToList();

            string result = priorityFirstlyTodo.Aggregate((acc, desc) => acc + ", " + desc); // Birleştirme işlemi yapılıyor. 
            ViewBag.results = result;
            return View();
        }


        public IActionResult IncompleteTask() // Tamamlanmamış görevleri listeleme
        {
            var values = _context.Todos
                .Where(x => !x.Status)
                .Select(y => y.Description)
                .ToList()
                .Prepend("Gün başında tüm görevleri kontrol etmeyi unutmayın!")
                .ToList(); // Başına ekleme yapılıyor.
            return View(values);
        }

        public IActionResult TodoChunk()
        {
            var values = _context.Todos
                .Where(x => !x.Status)
                .ToList()
                .Chunk(2)
                .ToList(); // İki'şerli gruplara ayırma işlemi yapılıyor.
            return View(values);
        }

        public IActionResult TodoConcat()
        {
            var values = _context.Todos
                .Where(x => x.Priority == "Birincil")
                .ToList()
                .Concat(
                        _context.Todos.Where(y => y.Priority == "İkincil").ToList()
                )
                .ToList();
            return View(values);
        }

        public IActionResult TodoUnion() // Birleştirme işlemi (Union)
        {
            var values = _context.Todos.Where(x => x.Priority == "Birincil").ToList();
            var values2 = _context.Todos.Where(x => x.Priority == "İkincil").ToList();
            var result = values.UnionBy(values2, x => x.Description).ToList();
            return View(result);
        }

        
    }
}
