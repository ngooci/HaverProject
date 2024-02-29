using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HaverProject.Data;
using HaverProject.Models;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace HaverProject.Controllers
{
    public class NCRController : Controller
    {
        private readonly HaverContext _context;

        public NCRController(HaverContext context)
        {
            _context = context;
        }

        // GET: NCR
        public async Task<IActionResult> Index(string searchString, int? id, DateTime? date, long? poNumber, string status, string supplier, int? page, string actionButton, string sortOrder, string sortDirection = "asc", string sortField = "NCRNumber")
        {
            IQueryable<NCR> ncrLogsQuery = _context.Ncrs.AsQueryable();
            ViewData["NCRNumberSortParam"] = string.IsNullOrEmpty(sortOrder) ? "ncr_number_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PONumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ponumber_desc" : "";
            ViewData["SupplierSortParm"] = sortOrder == "Supplier" ? "supplier_desc" : "Supplier";

            // Apply filters based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.NCRNumber.Contains(searchString)
                                                    || n.Supplier.Contains(searchString)
                                                    || n.DescriptionItem.Contains(searchString));
            }

            if (id != null)
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.ID == id);
            }

            if (poNumber != null)
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.PONumber == poNumber);
            }

            if (!string.IsNullOrEmpty(status))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.Status == status);
            }

            if (date != null)
            {
                // Filter from the start of the provided date to the end of the day
                DateTime startDate = date.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                ncrLogsQuery = ncrLogsQuery.Where(n => n.Date >= startDate && n.Date < endDate);
            }

            if (!string.IsNullOrEmpty(supplier))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.Supplier.Contains(supplier));
            }

            // Apply sorting based on the selected sortField and sortDirection
            switch (sortOrder)
            {
                case "ncr_number_desc":
                    ncrLogsQuery = ncrLogsQuery.OrderByDescending(n => n.NCRNumber);
                    break;
                case "Date":
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.Date);
                    break;
                case "date_desc":
                    ncrLogsQuery = ncrLogsQuery.OrderByDescending(n => n.Date);
                    break;
                case "\"ncr_number_asc":
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.NCRNumber);
                    break;
                case "ponumber_desc":
                    ncrLogsQuery = ncrLogsQuery.OrderByDescending(n => n.PONumber);
                    break;
                case "Supplier":
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.Supplier);
                    break;
                case "supplier_desc":
                    ncrLogsQuery = ncrLogsQuery.OrderByDescending(n => n.Supplier);
                    break;
                default:
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
            }

            var ncrs = await ncrLogsQuery.AsNoTracking().ToListAsync();

            // Retrieve distinct suppliers and pass them to the view
            var distinctSuppliers = await _context.Ncrs.Select(n => n.Supplier).Distinct().ToListAsync();
            ViewData["DistinctSuppliers"] = new SelectList(distinctSuppliers);

            return View(ncrs);
        }

        // GET: NCR/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ncrs == null)
            {
                return NotFound();
            }

            var nCR = await _context.Ncrs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nCR == null)
            {
                return NotFound();
            }

            return View(nCR);
        }

        // GET: NCR/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NCR/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NCRNumber,PONumber,SupplierOrRecInsp,WIP,Supplier,QuantityReceived,QuantityDefected,DescriptionItem,DescriptionDefect,Yes,No,Date,RepresentativesName")] NCR nCR)
        {
            if (ModelState.IsValid)
            {

                _context.Add(nCR);
                nCR.Status = "Engineering";
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                // Retrieve all model state errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                // Iterate over each error and log or handle it
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(nCR);
        }

        // GET: NCR/Edit/5
        public async Task<IActionResult> QualityEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCR = await _context.Ncrs.FindAsync(id);
            if (nCR == null)
            {
                return NotFound();
            }

            // Retrieve distinct supplier names
            var distinctSuppliers = await _context.Ncrs.Select(n => n.Supplier).Distinct().ToListAsync();

            // Pass the distinct supplier names to the view
            ViewData["SupplierList"] = new SelectList(distinctSuppliers);

            return View("QualityEdit",nCR);
        }

        // GET: NCR/Edit/5
        public async Task<IActionResult> EngineerEdit(int? id)
        {
            if (id == null || _context.Ncrs == null)
            {
                return NotFound();
            }

            var nCR = await _context.Ncrs.FindAsync(id);
            if (nCR == null)
            {
                return NotFound();
            }
            return View("EngineerEdit", nCR);
        }

        public async Task<IActionResult> PurchaseEdit(int? id)
        {
            if (id == null || _context.Ncrs == null)
            {
                return NotFound();
            }

            var nCR = await _context.Ncrs.FindAsync(id);
            if (nCR == null)
            {
                return NotFound();
            }
            return View("PurchaseEdit", nCR);
        }

        // POST: NCR/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QualityEdit(int id, [Bind("ID,NCRNumber,PONumber,SupplierOrRecInsp,WIP,Supplier," +
            "QuantityReceived,QuantityDefected,DescriptionItem,DescriptionDefect,Yes,No,Date,RepresentativesName")] NCR nCR)
            {
            if (id != nCR.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Attach(nCR);
                    nCR.Status = "Engineering";
                    _context.Entry(nCR).Property("NCRNumber").IsModified = false;
                    _context.Entry(nCR).Property("PONumber").IsModified = true;
                    _context.Entry(nCR).Property("SupplierOrRecInsp").IsModified = true;
                    _context.Entry(nCR).Property("WIP").IsModified = true;
                    _context.Entry(nCR).Property("Supplier").IsModified = true;
                    _context.Entry(nCR).Property("QuantityReceived").IsModified = true;
                    _context.Entry(nCR).Property("QuantityDefected").IsModified = true;
                    _context.Entry(nCR).Property("DescriptionItem").IsModified = true;
                    _context.Entry(nCR).Property("DescriptionDefect").IsModified = true;
                    _context.Entry(nCR).Property("Yes").IsModified = true;
                    _context.Entry(nCR).Property("No").IsModified = true;
                    _context.Entry(nCR).Property("Date").IsModified = true;
                    _context.Entry(nCR).Property("RepresentativesName").IsModified = true;
                    _context.Update(nCR);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NCRExists(nCR.ID))
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
            return View(nCR);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EngineerEdit(int id, [Bind("ID,Status,UseAsIs,Repair,Rework,Scrap,CustomerYes,CustomerNO,Disposition," +
            "DrawingYes,DrawingNo,OriginalRev,UpdatedRev,NameOfEngineer,RevisingDate,Engineer,EngineerDate")] NCR nCR)
        {
            if (id != nCR.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Attach(nCR);
                     nCR.Status = "Purchase";
                    _context.Entry(nCR).Property("UseAsIs").IsModified = true;
                    _context.Entry(nCR).Property("Repair").IsModified = true;
                    _context.Entry(nCR).Property("Rework").IsModified = true;
                    _context.Entry(nCR).Property("Scrap").IsModified = true;
                    _context.Entry(nCR).Property("CustomerYes").IsModified = true;
                    _context.Entry(nCR).Property("CustomerNO").IsModified = true;
                    _context.Entry(nCR).Property("Disposition").IsModified = true;
                    _context.Entry(nCR).Property("DrawingYes").IsModified = true;
                    _context.Entry(nCR).Property("DrawingNo").IsModified = true;
                    _context.Entry(nCR).Property("OriginalRev").IsModified = true;
                    _context.Entry(nCR).Property("UpdatedRev").IsModified = true;
                    _context.Entry(nCR).Property("NameOfEngineer").IsModified = true;
                    _context.Entry(nCR).Property("RevisingDate").IsModified = true;
                    _context.Entry(nCR).Property("Engineer").IsModified = true;
                    _context.Entry(nCR).Property("EngineerDate").IsModified = true;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NCRExists(nCR.ID))
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
            if (!ModelState.IsValid)
            {
                // Retrieve all model state errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                // Iterate over each error and log or handle it
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(nCR);
        }

        

         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseEdit(int id, [Bind("ID,ReturntoSupplier,ReworkInHouse,ScrapinHouse,DeferforHBC," +
             "PreliminaryDecision,CARYes,CARNo,FollowupYes,FollowupNo,PurchaseDate,OperatingManagerName")] NCR nCR)
        {
            if (id != nCR.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Attach(nCR);
                    nCR.Status = "Closed";
                    _context.Entry(nCR).Property("ReturntoSupplier").IsModified = true;
                    _context.Entry(nCR).Property("ReworkInHouse").IsModified = true;
                    _context.Entry(nCR).Property("ScrapinHouse").IsModified = true;
                    _context.Entry(nCR).Property("DeferforHBC").IsModified = true;
                    _context.Entry(nCR).Property("PreliminaryDecision").IsModified = true;
                    _context.Entry(nCR).Property("CARYes").IsModified = true;
                    _context.Entry(nCR).Property("CARNo").IsModified = true;
                    _context.Entry(nCR).Property("FollowupYes").IsModified = true;
                    _context.Entry(nCR).Property("FollowupNo").IsModified = true;
                    _context.Entry(nCR).Property("PurchaseDate").IsModified = true;
                    _context.Entry(nCR).Property("OperatingManagerName").IsModified = true;
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NCRExists(nCR.ID))
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
            if (!ModelState.IsValid)
            {
                // Retrieve all model state errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                // Iterate over each error and log or handle it
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(nCR);
        }

        // GET: NCR/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ncrs == null)
            {
                return NotFound();
            }

            var nCR = await _context.Ncrs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nCR == null)
            {
                return NotFound();
            }

            return View(nCR);
        }

        // POST: NCR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ncrs == null)
            {
                return Problem("Entity set 'HaverContext.Ncrs'  is null.");
            }
            var nCR = await _context.Ncrs.FindAsync(id);
            if (nCR != null)
            {
                _context.Ncrs.Remove(nCR);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NCRExists(int id)
        {
          return (_context.Ncrs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
