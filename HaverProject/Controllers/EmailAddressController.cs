using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HaverProject.Data;
using HaverProject.ViewModel;

namespace HaverProject.Controllers
{
    public class EmailAddressController : Controller
    {
        private readonly HaverContext _context;

        public EmailAddressController(HaverContext context)
        {
            _context = context;
        }

        // GET: EmailAddress
        public async Task<IActionResult> Index()
        {
              return View(await _context.emailAddresses.ToListAsync());
        }

        // GET: EmailAddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.emailAddresses == null)
            {
                return NotFound();
            }

            var emailAddress = await _context.emailAddresses
                .FirstOrDefaultAsync(m => m.id == id);
            if (emailAddress == null)
            {
                return NotFound();
            }

            return View(emailAddress);
        }

        // GET: EmailAddress/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmailAddress/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Address")] EmailAddress emailAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emailAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailAddress);
        }

        // GET: EmailAddress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.emailAddresses == null)
            {
                return NotFound();
            }

            var emailAddress = await _context.emailAddresses.FindAsync(id);
            if (emailAddress == null)
            {
                return NotFound();
            }
            return View(emailAddress);
        }

        // POST: EmailAddress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Address")] EmailAddress emailAddress)
        {
            if (id != emailAddress.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailAddressExists(emailAddress.id))
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
            return View(emailAddress);
        }

        // GET: EmailAddress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.emailAddresses == null)
            {
                return NotFound();
            }

            var emailAddress = await _context.emailAddresses
                .FirstOrDefaultAsync(m => m.id == id);
            if (emailAddress == null)
            {
                return NotFound();
            }

            return View(emailAddress);
        }

        // POST: EmailAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.emailAddresses == null)
            {
                return Problem("Entity set 'HaverContext.emailAddresses'  is null.");
            }
            var emailAddress = await _context.emailAddresses.FindAsync(id);
            if (emailAddress != null)
            {
                _context.emailAddresses.Remove(emailAddress);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailAddressExists(int id)
        {
          return _context.emailAddresses.Any(e => e.id == id);
        }
    }
}
