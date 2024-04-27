using HaverProject.Data;
using HaverProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaverProject.Controllers
{
    public class SapController : Controller
    {
        private readonly HaverContext _context;

        public SapController(HaverContext context)
        {
            _context = context;
        }

        // GET: Sap
        public async Task<IActionResult> Index()
        {
            return View(await _context.SapNos.ToListAsync());
        }
        // GET: Sap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.SapNos == null)
            {
                return NotFound();
            }

            var sap = await _context.SapNos
                .FirstOrDefaultAsync(m => m.Name == id);
            if (sap == null)
            {
                return NotFound();
            }

            return View(sap);
        }
        // GET: Sap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemProblems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SapNo sap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sap);
        }
        // GET: Sap/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SapNos == null)
            {
                return NotFound();
            }

            var sap = await _context.SapNos.FindAsync(id);
            if (sap == null)
            {
                return NotFound();
            }
            return View(sap);
        }

        // POST: Sap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] SapNo sap)
        {
            if (id != sap.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SapExists(sap.Name))
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
            return View(sap);
        }

        // GET: Sap/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.SapNos == null)
            {
                return NotFound();
            }

            var sap = await _context.SapNos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sap == null)
            {
                return NotFound();
            }

            return View(sap);
        }

        // POST: Sap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SapNos == null)
            {
                return Problem("Entity set 'HaverContext.Sap'  is null.");
            }
            var sap = await _context.SapNos.FindAsync(id);
            if (sap != null)
            {
                _context.SapNos.Remove(sap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SapExists(string id)
        {
            return _context.SapNos.Any(e => e.Name == id);
        }

    }
}
