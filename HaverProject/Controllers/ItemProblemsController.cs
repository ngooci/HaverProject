using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaverProject.Data;
using HaverProject.ViewModel;
using HaverProject.Models;

namespace HaverProject.Controllers
{
    public class ItemProblemsController : Controller
    {
        private readonly HaverContext _context;
        public ItemProblemsController(HaverContext context)
        {
            _context = context;
        }
        // GET: ItemProblems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemProblems.ToListAsync());
        }
        // GET: ItemProblems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ItemProblems == null)
            {
                return NotFound();
            }

            var problem = await _context.ItemProblems
                .FirstOrDefaultAsync(m => m.Name == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // GET: ItemProblems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemProblems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ItemProblem problem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(problem);
        }

        // GET: ItemProblems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ItemProblems == null)
            {
                return NotFound();
            }

            var problem = await _context.ItemProblems.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }
            return View(problem);
        }

        // POST: ItemProblems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] ItemProblem problem)
        {
            if (id != problem.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemProblemsExists(problem.Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(problem);
        }

        // GET: ItemProblems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.ItemProblems == null)
            {
                return NotFound();
            }

            var problem = await _context.ItemProblems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // POST: ItemProblems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
                if (_context.ItemProblems == null)
                {
                    return Problem("Entity set 'HaverContext.ItemProblems'  is null.");
                }
                var problem = await _context.ItemProblems.FindAsync(id);
                if (problem != null)
                {
                    _context.ItemProblems.Remove(problem);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
        }

        private bool ItemProblemsExists(string id)
        {
            return _context.ItemProblems.Any(e => e.Name == id);
        }

    }
}
