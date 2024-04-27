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
using CateringManagement.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HaverProject.Utilities;
using HaverProject.ViewModel;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore; 

namespace HaverProject.Controllers
{
    public class NCRController : ElephantController
    {
        private readonly HaverContext _context;
        private readonly IMyEmailSender _emailSender;

        public NCRController(HaverContext context, IMyEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Dashboard()
        {


            DateTime fiveYearsAgo = DateTime.Today.AddYears(-5);

            // Query records older than five years
            var records = _context.Ncrs
                .Where(x => x.Date < fiveYearsAgo)
                .ToList();
            foreach (var record in records)
            {
                // Assuming you want to update a field named 'FieldToUpdate'
                record.Status = "Archieve";

                // Mark the entity as modified (if necessary)
                _context.Entry(record).State = EntityState.Modified;
            }

            // Save changes to the database
            _context.SaveChanges();
      

            var ncrLogsQuerya = (from e in _context.Ncrs
                                 where e.Status != "Closed"
                                 where e.Status != "Archieve"
                                 where e.Status != "Void"
                                 select e
                                  );
            ViewData["AllNcrCount"] = (string)ncrLogsQuerya.Count().ToString();
            var ncrLogsQuery = (from e in _context.Ncrs
                                where e.Status == "Engineering"
                                select e
                                      );
            ViewData["EngineeringNcrCount"] = (string)ncrLogsQuery.Count().ToString();
            var ncrLogsQueryp = (from e in _context.Ncrs
                                where e.Status == "Purchase"
                                select e
                                );
            ViewData["PurchaseNcrCount"] = (string)ncrLogsQueryp.Count().ToString();
            var ncrLogsQueryc = (from e in _context.Ncrs
                                 where e.Status == "Closed"
                                 select e
                             );
            ViewData["ClosedNcrCount"] = (string)ncrLogsQueryc.Count().ToString();
            var ncrLogsQueryr = (from e in _context.Ncrs
                                 where e.Status == "Review"
                                 select e
                           );
            ViewData["ReviewNcrCount"] = (string)ncrLogsQueryr.Count().ToString();
            var ncrLogsQueryar = (from e in _context.Ncrs
                                 where e.Status == "Archieve"
                                 select e
                         );
            ViewData["ArchievedNcrCount"] = (string)ncrLogsQueryar.Count().ToString();
            var ncrLogsQueryv = (from e in _context.Ncrs
                                  where e.Status == "Void"
                                  select e
                        );
            ViewData["VoidNcrCount"] = (string)ncrLogsQueryv.Count().ToString();


            return View("Dashboard");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Archieve(int? id)
        {
            if (id == null || _context.Ncrs == null)
            {
                return NotFound();
            }

            var nCR = await _context.Ncrs
                 .Include(p => p.NCRPhotos)
                 .Include(n => n.Supplier)
                 .FirstOrDefaultAsync(m => m.ID == id);
            if (nCR == null)
            {
                return NotFound();
            }
            nCR.Status = "Archieve";
            _context.Attach(nCR).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            Dashboard();
            
            return View("Dashboard");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Void(int? id, string reason, string VoidReason)
        {
            var nCR = await _context.Ncrs.FindAsync(id);

            if (nCR == null)
            {
                return NotFound();
            }

            // Set the status to "Void"
            nCR.Status = "Void";

            // Set the void reason
            nCR.VoidReason = VoidReason;
            _context.Entry(nCR).State = EntityState.Modified;
            try
            {
                // Save changes to mark the NCR as void
                await _context.SaveChangesAsync();

                // If a void comment is provided, add it to the NCR's comments
                if (!string.IsNullOrEmpty(VoidReason))
                {
                    var newComment = new NCRComment
                    {
                        NCRId = nCR.ID,
                        CommentText = VoidReason,
                        CreatedAt = DateTime.Now
                    };

                    // Add the comment to the NCR's comment list
                    if (nCR.Comments == null)
                    {
                        nCR.Comments = new List<NCRComment>();
                    }
                    nCR.Comments.Add(newComment);

                    // Save changes to the database again to include the comment
                    await _context.SaveChangesAsync();
                }

                ViewData["VoidMessage"] = "NCR voided successfully."; // Set success message
            }
            catch (DbUpdateException ex)
            {
                // Handle potential database errors gracefully (e.g., logging, error messages)
                return View("Error"); // Or redirect to an error page
            }

            return RedirectToAction("Index"); // Redirect back to the Index action
        }

        [Authorize]
        public async Task<IActionResult> VoidNcr(string searchString, int? id, string answer, DateTime? date, long? poNumber, string status, int supplier, int? page, int pageSizeID, string actionButton, string sortOrder, string sortDirection = "asc", string sortField = "NCRNumber")
        {
            if (page == null)
            {
                page = 1;
            }

            var ncrLogsQuery = (from e in _context.Ncrs
                                where e.Status == "Void"
                                select e
                            )
                            .Include(f => f.Supplier)
                            .AsNoTracking();

            ViewData["EngineeringNcrCount"] = (string)ncrLogsQuery.Count().ToString();
            ViewData["NCRNumberSortParam"] = string.IsNullOrEmpty(sortOrder) ? "ncr_number_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PONumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ponumber_desc" : "";
            ViewData["SupplierSortParm"] = sortOrder == "Supplier" ? "supplier_desc" : "Supplier";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "status_desc" : "Status";


            // Apply filters based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.NCRNumber.Contains(searchString)

                                             || n.DescriptionItemID.Contains(searchString));
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

            // Apply sorting based on the selected sortField and sortDirection
            switch (sortField)
            {
                case "NCRNumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.NCRNumber) : ncrLogsQuery.OrderBy(n => n.NCRNumber);
                    break;
                case "Date":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Date) : ncrLogsQuery.OrderBy(n => n.Date);
                    break;
                case "PONumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.PONumber) : ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
                case "Supplier":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Supplier.Name) : ncrLogsQuery.OrderBy(n => n.Supplier.Name);
                    break;
                case "Status":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Status) : ncrLogsQuery.OrderBy(n => n.Status);
                    break;

                default:
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
            }
            var ncrs = await ncrLogsQuery.AsNoTracking().ToListAsync();
            if (actionButton == "Void") // Check if the "Void" button was clicked
            {
                try
                {
                    // Existing voiding logic...

                    // Add comment logic (assuming a "VoidComment" textarea exists in the form):
                    var voidComment = HttpContext.Request.Form["VoidComment"];
                    if (!string.IsNullOrEmpty(voidComment))
                    {
                        var ncr = await _context.Ncrs.FindAsync(id);
                        if (ncr != null)
                        {
                            // Create a new NCRComment object
                            var newComment = new NCRComment();
                            newComment.NCRId = id.Value;
                            newComment.CommentText = voidComment;
                            newComment.CreatedAt = DateTime.Now;

                            // Add the comment to the NCR's comment list (if applicable)
                            ncr.Comments.Add(newComment);

                            // Save changes to the database
                            await _context.SaveChangesAsync();
                        }
                    }

                    return RedirectToAction("Void", new { id = id, reason = HttpContext.Request.Form["VoidReason"] });
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as per your application's requirements
                    // For example, you can log the exception and then display a friendly error message to the user
                    // Alternatively, you can redirect the user to an error page
                    Console.WriteLine($"An error occurred while processing the voiding logic: {ex.Message}");
                    return View("Error"); // Or redirect to an error page
                }
            }

            var s = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(s, "Id", "Name");

            var distinctSuppliers = await _context.Suppliers.Select(s => s.Name).Distinct().ToListAsync();
            ViewData["DistinctSuppliers"] = new SelectList(distinctSuppliers);
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<NCR>.CreateAsync(ncrLogsQuery.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> VoidDetails(int? id, string reason)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCR = await _context.Ncrs
                .Include(p => p.NCRPhotos)
                .Include(n => n.Supplier)
                .Include(a => a.SapNo)
                .Include(b => b.ItemProblem)
                .Include(o => o.ItemMarked)
                .Include(o => o.UseAsIs)
                .Include(k => k.SupplierOrRecInsp)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (nCR == null)
            {
                return NotFound();
            }

            // Update the VoidReason property and save changes to the database
            nCR.VoidReason = reason;
            await _context.SaveChangesAsync();

            return View("Details", nCR);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> VoidNcrPost(int? id, string reason, string voidComment)
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

            nCR.Status = "Void"; // Set the status to "Void"
            nCR.VoidReason = reason; // Set the void reason

            try
            {
                // If a void comment is provided, add it to the NCR's comments
                if (!string.IsNullOrEmpty(voidComment))
                {
                    var newComment = new NCRComment
                    {
                        NCRId = nCR.ID,
                        CommentText = voidComment,
                        CreatedAt = DateTime.Now
                    };

                    // Add the comment to the NCR's comment list
                    if (nCR.Comments == null)
                    {
                        nCR.Comments = new List<NCRComment>();
                    }
                    nCR.Comments.Add(newComment);
                }

                // Save changes to mark the NCR as void and include the comment if provided
                await _context.SaveChangesAsync();

                ViewData["VoidMessage"] = "NCR voided successfully."; // Set success message
            }
            catch (DbUpdateException ex)
            {
                // Handle potential database errors gracefully (e.g., logging, error messages)
                return View("Error"); // Or redirect to an error page
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> EngineeringNcr(string searchString, int? id, DateTime? date, long? poNumber, string status, int supplier, int? page, int pageSizeID, string actionButton, string sortOrder, string sortDirection = "asc", string sortField = "NCRNumber")
        {
            


            if (page == null)
            {
                page = 1;
            }

            var ncrLogsQuery = (from e in _context.Ncrs
                                where e.Status == "Engineering"
                                select e
                                      ).Include(f => f.Supplier)
                                  .AsNoTracking();

            ViewData["EngineeringNcrCount"] = (string)ncrLogsQuery.Count().ToString();
            ViewData["NCRNumberSortParam"] = string.IsNullOrEmpty(sortOrder) ? "ncr_number_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PONumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ponumber_desc" : "";
            ViewData["SupplierSortParm"] = sortOrder == "Supplier" ? "supplier_desc" : "Supplier";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "status_desc" : "Status";


            // Apply filters based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.NCRNumber.Contains(searchString)

                                                    || n.DescriptionItemID.Contains(searchString));
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


            // Apply sorting based on the selected sortField and sortDirection
            switch (sortField)
            {
                case "NCRNumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.NCRNumber) : ncrLogsQuery.OrderBy(n => n.NCRNumber);
                    break;
                case "Date":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Date) : ncrLogsQuery.OrderBy(n => n.Date);
                    break;
                case "PONumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.PONumber) : ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
                case "Supplier":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Supplier.Name) : ncrLogsQuery.OrderBy(n => n.Supplier.Name);
                    break;
                case "Status":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Status) : ncrLogsQuery.OrderBy(n => n.Status);
                    break;

                default:
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
            }
            var ncrs = await ncrLogsQuery.AsNoTracking().ToListAsync();


            var s = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(s, "Id", "Name");
           
            var distinctSuppliers = await _context.Suppliers.Select(s => s.Name).Distinct().ToListAsync();
            ViewData["DistinctSuppliers"] = new SelectList(distinctSuppliers);
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<NCR>.CreateAsync(ncrLogsQuery.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);


        }

        [Authorize]
        public async Task<IActionResult> PurchaseNcr(string searchString, int? id, DateTime? date, long? poNumber, string status, int supplier, int? page,int pageSizeID, string actionButton, string sortOrder, string sortDirection = "asc", string sortField = "NCRNumber")
        {



            if (page == null)
            {
                page = 1;
            }

            var ncrLogsQuery = (from e in _context.Ncrs
                                where e.Status == "Purchase"
                                select e
                                      ).Include(f => f.Supplier)
                                  .AsNoTracking();


            ViewData["NCRNumberSortParam"] = string.IsNullOrEmpty(sortOrder) ? "ncr_number_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PONumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ponumber_desc" : "";
            ViewData["SupplierSortParm"] = sortOrder == "Supplier" ? "supplier_desc" : "Supplier";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "status_desc" : "Status";


            // Apply filters based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.NCRNumber.Contains(searchString)

                                                    || n.DescriptionItemID.Contains(searchString));
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


            // Apply sorting based on the selected sortField and sortDirection
            switch (sortField)
            {
                case "NCRNumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.NCRNumber) : ncrLogsQuery.OrderBy(n => n.NCRNumber);
                    break;
                case "Date":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Date) : ncrLogsQuery.OrderBy(n => n.Date);
                    break;
                case "PONumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.PONumber) : ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
                case "Supplier":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Supplier.Name) : ncrLogsQuery.OrderBy(n => n.Supplier.Name);
                    break;
                case "Status":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Status) : ncrLogsQuery.OrderBy(n => n.Status);
                    break;

                default:
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
            }
            var ncrs = await ncrLogsQuery.AsNoTracking().ToListAsync();


            var s = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(s, "Id", "Name");
            
            var distinctSuppliers = await _context.Suppliers.Select(s => s.Name).Distinct().ToListAsync();
            ViewData["DistinctSuppliers"] = new SelectList(distinctSuppliers);
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<NCR>.CreateAsync(ncrLogsQuery.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);


        }

        [Authorize]
        public async Task<IActionResult> ReviewNcr(string searchString, int? id, DateTime? date, long? poNumber, string status, int supplier, int? page,int pageSizeID, string actionButton, string sortOrder, string sortDirection = "asc", string sortField = "NCRNumber")
        {



            if (page == null)
            {
                page = 1;
            }

            var ncrLogsQuery = (from e in _context.Ncrs
                                where e.Status == "Review"
                                select e
                                      ).Include(f => f.Supplier)
                                  .AsNoTracking();


            ViewData["NCRNumberSortParam"] = string.IsNullOrEmpty(sortOrder) ? "ncr_number_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PONumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ponumber_desc" : "";
            ViewData["SupplierSortParm"] = sortOrder == "Supplier" ? "supplier_desc" : "Supplier";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "status_desc" : "Status";

            // Apply filters based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.NCRNumber.Contains(searchString)

                                                    || n.DescriptionItemID.Contains(searchString));
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


            // Apply sorting based on the selected sortField and sortDirection
            switch (sortField)
            {
                case "NCRNumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.NCRNumber) : ncrLogsQuery.OrderBy(n => n.NCRNumber);
                    break;
                case "Date":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Date) : ncrLogsQuery.OrderBy(n => n.Date);
                    break;
                case "PONumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.PONumber) : ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
                case "Supplier":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Supplier.Name) : ncrLogsQuery.OrderBy(n => n.Supplier.Name);
                    break;
                case "Status":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Status) : ncrLogsQuery.OrderBy(n => n.Status);
                    break;

                default:
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
            }
            var ncrs = await ncrLogsQuery.AsNoTracking().ToListAsync();


            var s = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(s, "Id", "Name");
       
            var distinctSuppliers = await _context.Suppliers.Select(s => s.Name).Distinct().ToListAsync();
            ViewData["DistinctSuppliers"] = new SelectList(distinctSuppliers);
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<NCR>.CreateAsync(ncrLogsQuery.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);


        }

        [Authorize]
        public async Task<IActionResult> ClosedNcr(string searchString, int? id, DateTime? date, long? poNumber, string status, int supplier, int? page,int pageSizeID, string actionButton, string sortOrder, string sortDirection = "asc", string sortField = "NCRNumber")
        {
            


            if (page == null)
            {
                page = 1;
            }

            var ncrLogsQuery = (from e in _context.Ncrs
                                where e.Status == "Closed"
                                select e
                                 ).Include(f => f.Supplier)
                                  .AsNoTracking();


            ViewData["NCRNumberSortParam"] = string.IsNullOrEmpty(sortOrder) ? "ncr_number_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PONumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ponumber_desc" : "";
            ViewData["SupplierSortParm"] = sortOrder == "Supplier" ? "supplier_desc" : "Supplier";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "status_desc" : "Status";


            // Apply filters based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.NCRNumber.Contains(searchString)

                                                    || n.DescriptionItemID.Contains(searchString));
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


            // Apply sorting based on the selected sortField and sortDirection
            switch (sortField)
            {
                case "NCRNumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.NCRNumber) : ncrLogsQuery.OrderBy(n => n.NCRNumber);
                    break;
                case "Date":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Date) : ncrLogsQuery.OrderBy(n => n.Date);
                    break;
                case "PONumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.PONumber) : ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
                case "Supplier":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Supplier.Name) : ncrLogsQuery.OrderBy(n => n.Supplier.Name);
                    break;
                case "Status":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Status) : ncrLogsQuery.OrderBy(n => n.Status);
                    break;

                default:
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
            }
            var ncrs = await ncrLogsQuery.AsNoTracking().ToListAsync();


            var s = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(s, "Id", "Name");
            var distinctSuppliers = await _context.Suppliers.Select(s => s.Name).Distinct().ToListAsync();
            ViewData["DistinctSuppliers"] = new SelectList(distinctSuppliers);
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<NCR>.CreateAsync(ncrLogsQuery.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);


        }

        [Authorize]
        public async Task<IActionResult> ArchievedNcr(string searchString, int? id, DateTime? date, long? poNumber, string status, int supplier, int? page,int pageSizeID, string actionButton, string sortOrder, string sortDirection = "asc", string sortField = "NCRNumber")
        {



            if (page == null)
            {
                page = 1;
            }

            var ncrLogsQuery = (from e in _context.Ncrs
                                where e.Status == "Archieve"
                                select e
                                      ).Include(f => f.Supplier)
                                  .AsNoTracking();

            ViewData["EngineeringNcrCount"] = (string)ncrLogsQuery.Count().ToString();
            ViewData["NCRNumberSortParam"] = string.IsNullOrEmpty(sortOrder) ? "ncr_number_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PONumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ponumber_desc" : "";
            ViewData["SupplierSortParm"] = sortOrder == "Supplier" ? "supplier_desc" : "Supplier";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "status_desc" : "Status";


            // Apply filters based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.NCRNumber.Contains(searchString)

                                                    || n.DescriptionItemID.Contains(searchString));
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


            // Apply sorting based on the selected sortField and sortDirection
            switch (sortField)
            {
                case "NCRNumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.NCRNumber) : ncrLogsQuery.OrderBy(n => n.NCRNumber);
                    break;
                case "Date":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Date) : ncrLogsQuery.OrderBy(n => n.Date);
                    break;
                case "PONumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.PONumber) : ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
                case "Supplier":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Supplier.Name) : ncrLogsQuery.OrderBy(n => n.Supplier.Name);
                    break;
                case "Status":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Status) : ncrLogsQuery.OrderBy(n => n.Status);
                    break;

                default:
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
            }
            var ncrs = await ncrLogsQuery.AsNoTracking().ToListAsync();


            var s = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(s, "Id", "Name");
           
            var distinctSuppliers = await _context.Suppliers.Select(s => s.Name).Distinct().ToListAsync();
            ViewData["DistinctSuppliers"] = new SelectList(distinctSuppliers);
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<NCR>.CreateAsync(ncrLogsQuery.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);


        }

        [Authorize]
        public async Task<IActionResult> Index(string searchString, int? id, DateTime? date, long? poNumber, string status, string supplier,string sapId, string itemdescription, int? page, int pageSizeID, string actionButton, string sortOrder, string sortDirection = "asc", string sortField = "NCRNumber")
        {
            if (page == null)
            {
                page = 1;
            }

            var ncrLogsQuery = _context.Ncrs
                .Include(f => f.Supplier)
                .AsNoTracking();

            ViewData["NCRNumberSortParam"] = string.IsNullOrEmpty(sortOrder) ? "ncr_number_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PONumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ponumber_desc" : "";
            ViewData["SupplierSortParm"] = sortOrder == "Supplier" ? "supplier_desc" : "Supplier";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "status_desc" : "Status";


            // Apply filters based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.NCRNumber.Contains(searchString) || n.DescriptionItemID.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(supplier))
            {
                ncrLogsQuery = ncrLogsQuery.Where(n => n.Supplier.Name == supplier);
            }


            // Your existing code...

            var ncrs = await ncrLogsQuery.AsNoTracking().ToListAsync();



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


            // Apply sorting based on the selected sortField and sortDirection
            switch (sortField)
            {
                case "NCRNumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.NCRNumber) : ncrLogsQuery.OrderBy(n => n.NCRNumber);
                    break;
                case "Date":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Date) : ncrLogsQuery.OrderBy(n => n.Date);
                    break;
                case "PONumber":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.PONumber) : ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
                case "Supplier":
                    ncrLogsQuery = sortDirection == "desc" ? ncrLogsQuery.OrderByDescending(n => n.Supplier.Name) : ncrLogsQuery.OrderBy(n => n.Supplier.Name);
                    break;
                case "Status":
                    ncrLogsQuery = sortDirection == "desc"
                        ? ncrLogsQuery.OrderByDescending(n => n.Status)
                        : ncrLogsQuery.OrderBy(n => n.Status);
                    break;

                default:
                    ncrLogsQuery = ncrLogsQuery.OrderBy(n => n.PONumber);
                    break;
            }

 

            DateTime fiveYearsAgo = DateTime.Today.AddYears(-5);

            // Query records older than five years
            var records = _context.Ncrs
                .Where(x => x.Date < fiveYearsAgo)
                .ToList();
            foreach (var record in records)
            {
                // Assuming you want to update a field named 'FieldToUpdate'
                record.Status = "Archieve";

                // Mark the entity as modified (if necessary)
                _context.Entry(record).State = EntityState.Modified;
            }

            // Save changes to the database
            _context.SaveChanges();
            var s = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(s, "Id", "Name");
           

            // Retrieve distinct suppliers and pass them to the view
            var distinctSuppliers = await _context.Suppliers.Select(s => s.Name).Distinct().ToListAsync();
            ViewData["DistinctSuppliers"] = new SelectList(distinctSuppliers);

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<NCR>.CreateAsync(ncrLogsQuery.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);
        }

        // GET: NCR/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCR = await _context.Ncrs
                .Include(p => p.NCRPhotos)
                .Include(n => n.Supplier)
                .Include(a => a.SapNo)
                .Include(b => b.ItemProblem)
                .Include(o => o.ItemMarked)
               
             
                .Include(o => o.UseAsIs)
                .Include(k => k.SupplierOrRecInsp)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (nCR == null)
            {
                return NotFound();
            }

            return View(nCR);
        }
        // GET: NCR/Create
        [Authorize(Roles = "Admin, QualityInspector")]
        public IActionResult Create()
        {

            var emailList = _context.emailAddresses.ToList();
            ViewBag.EmailList = new SelectList(emailList, "id", "Name");

            var supplier = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(supplier, "Id", "Name");

            var itemdescription = _context.ItemProblems.ToList();
            ViewBag.descList = new SelectList(itemdescription, "Id", "Name");

            var sapId = _context.SapNos.ToList();
            ViewBag.SapId = new SelectList(sapId, "Id", "Name");
            var rescp = _context.SupplierOrRecInsps.ToList();
            ViewBag.rescpList = new SelectList(rescp, "Id", "Name");

            ViewBag.ItemList = GetItemMarkedList();

            List<int> ncrno = new List<int>(); 
            foreach (NCR n in _context.Ncrs)
            {
               int y = int.Parse(n.NCRNumber.Substring(n.NCRNumber.Length - 2));
                ncrno.Add(y);
            }
            int biggest = ncrno.Max();
            int newncr = biggest + 1;
            ViewData["NewNCR"] = newncr.ToString();

          
            return View();
        }
        private List<SelectListItem> GetItemMarkedList()
        {
            return _context.ItemMarkeds
                           .Select(item => new SelectListItem
                           {
                               Value = item.Id.ToString(),
                               Text = item.Name
                           })
                           .ToList();
           
        }
        [Authorize]
        public ActionResult Detailspdf(int id)
        {
            var model = GetYourData(id);
            return new Rotativa.AspNetCore.ViewAsPdf(model);
        }
        public NCR GetYourData(int id)
        {

            return _context.Ncrs.FirstOrDefault(item => item.ID == id);

        }
        // POST: NCR/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, QualityInspector")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NCRNumber,PONumber,SupplierOrRecInspID,SupplierId,SapId,QuantityReceived,QuantityDefected," +
    "DescriptionItemID,DescriptionDefect,ItemMarkedID,Date,RepresentativesName ,ncrEmail")] NCR nCR, string chkRemoveImage, List<IFormFile> pictures, int? page)
        {
            if (ModelState.IsValid)
            {
                // Set the Date property to today's date
       

                // Add or update pictures await AddPictures(customer, Pictures);
                await AddPictures(nCR, pictures);

                // Set the default status
                nCR.Status = "Engineering";

                // Add the NCR to the context
                _context.Add(nCR);

                // Save changes to the database
                await _context.SaveChangesAsync();

                Notification(nCR.ncrEmail, null, null);

                // Redirect to the index action
                return View("Notification", nCR);
                  
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

        public async Task<IActionResult> Notification(int? id, string Subject, string emailContent)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmailAddress t = await _context.emailAddresses.FindAsync(id);

            ViewData["id"] = id;
            ViewData["Name"] = t.Name;

            if (string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(emailContent))
            {
                ViewData["Message"] = "You must enter both a Subject and some message Content before sending the message.";
            }
            else
            {
                int folksCount = 0;
                try
                {
                    //Send a Notice.
                    List<EmailAddress> folks = (from e in _context.emailAddresses
                                                where e.id == id
                                                select e
                                              ).ToList();
                    folksCount = folks.Count;
                    string name = folks[0].Name;
                    if (folksCount > 0)
                    {
                        var msg = new EmailMessage()
                        {
                            ToAddresses = folks,
                            Subject = Subject,
                            Content = "<p>" + emailContent + "</p>"

                        };
                        await _emailSender.SendToManyAsync(msg);
                        ViewData["Message"] = "Message sent to " + name 
                            + ((folksCount == 1) ? "." : "s.");
                    }
                    else
                    {
                        ViewData["Message"] = "Message NOT sent!";
                    }
                }
                catch (Exception ex)
                {
                    string errMsg = ex.GetBaseException().Message;

                    ViewData["Message"] = "Error: Could not send email message";
                }
            }

            return View();
        }


        // GET: NCR/Edit/5
        [Authorize(Roles = "Admin, QualityInspector")]
        public async Task<IActionResult> QualityEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCR = await _context.Ncrs
                .Include(n => n.NCRPhotos)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (nCR == null)
            {
                return NotFound();
            }

            // Detach the retrieved entity from the DbContext
            _context.Entry(nCR).State = EntityState.Detached;

            var supplier = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(supplier, "Id", "Name");
            var emailList = _context.emailAddresses.ToList();
            ViewBag.EmailList = new SelectList(emailList, "id", "Name");
            var itemdescription = _context.ItemProblems.ToList();
            ViewBag.descList = new SelectList(itemdescription, "Id", "Name");
            var rescp = _context.SupplierOrRecInsps.ToList();
            ViewBag.rescpList = new SelectList(rescp, "Id", "Name");


            var sapId = _context.SapNos.ToList();
            ViewBag.SapId = new SelectList(sapId, "Id", "Name");
         

            ViewBag.ItemList = GetItemMarkedList();

            return View("QualityEdit", nCR);
        }

        // GET: NCR/Edit/5
        [Authorize(Roles = "Admin,Engineer")]
        public async Task<IActionResult> EngineerEdit(int? id)
        {
            try
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

                var useAs = _context.UseAsIss.ToList();
                ViewBag.UseAsList = new SelectList(useAs, "Id", "Name");
                var emailList = _context.emailAddresses.ToList();
                ViewBag.EmailList = new SelectList(emailList, "id", "Name");

                // Fetch the Review list
                ViewBag.ReviewList = GetReviewList();

                return View("EngineerEdit", nCR);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine("An error occurred in EngineerEdit action: " + ex.Message);
                throw; // Rethrow the exception to maintain standard exception handling
            }
        }


        private List<SelectListItem> GetReviewList()
        {
            // Retrieve the list of reviews from your database or any other source
            var reviewList = _context.Reviews
                                    .Select(item => new SelectListItem
                                    {
                                        Value = item.Id.ToString(),
                                        Text = item.Name
                                    })
                                    .ToList();
            return reviewList;
        }

        [Authorize(Roles = "Admin,Purchase")]
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
             if (nCR.Engineer == null) { ModelState.AddModelError("Error", "Check ID"); };
            var emailList = _context.emailAddresses.ToList();
            ViewBag.EmailList = new SelectList(emailList, "id", "Name");
            return View("PurchaseEdit", nCR);
        }

        [Authorize(Roles = "Admin,Review")]
        public async Task<IActionResult> ReviewEdit(int? id)
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
            nCR.finalDate = DateTime.Today;

            ViewBag.YesList = GetYesReviewList();
            var emailList = _context.emailAddresses.ToList();
            ViewBag.EmailList = new SelectList(emailList, "id", "Name");
            return View("ReviewEdit", nCR);
        }
        private List<SelectListItem> GetYesReviewList()
        {
            // Retrieve the list of reviews from your database or any other source
            var reviewList = _context.Reviews
                                    .Select(item => new SelectListItem
                                    {
                                        Value = item.Id.ToString(),
                                        Text = item.Name
                                    })
                                    .ToList();
            return reviewList;
        }


        // POST: NCR/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, QualityInspector")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QualityEdit(int id, [Bind("ID,NCRNumber,PONumber,SupplierOrRecInspID,SupplierId,SapId," +
    "QuantityReceived,QuantityDefected,DescriptionItemID,DescriptionDefect,ItemMarkedID,Date,RepresentativesName,ncrEmail,NCRPhotos,Video")] NCR nCR, string chkRemoveImage, List<IFormFile> pictures)
        {
            var phototoupdate = await _context.Ncrs.Include(c => c.NCRPhotos).FirstOrDefaultAsync(c => c.ID == id);

            if (phototoupdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check if the picture file is provided and not null
                //if (thePicture == null || thePicture.Length == 0)
                //{
                //    // Add a model error if the picture is not provided
                //    ModelState.AddModelError("thePicture", "Please select a picture.");
                //    return View(nCR);
                //}

                // Add the picture to the NCR
                try
                {
                    if (chkRemoveImage != null)
                    {
                        // Remove the image if the checkbox is checked
                        phototoupdate.NCRPhotos = null;
                    }

                    // Detach the existing customer before updating
                    _context.Entry(phototoupdate).State = EntityState.Detached;

                    // Attach the modified customer


                    _context.Attach(nCR);
                    nCR.Status = "Engineering";

                    _context.Entry(nCR).Property("PONumber").IsModified = true;
                    _context.Entry(nCR).Property("SupplierOrRecInspID").IsModified = true;
                    _context.Entry(nCR).Property("SupplierId").IsModified = true;
                    _context.Entry(nCR).Property("SapId").IsModified = true;
                    _context.Entry(nCR).Property("QuantityReceived").IsModified = true;
                    _context.Entry(nCR).Property("QuantityDefected").IsModified = true;
                    _context.Entry(nCR).Property("DescriptionItemID").IsModified = true;
                    _context.Entry(nCR).Property("DescriptionDefect").IsModified = true;
                    _context.Entry(nCR).Property("ItemMarkedID").IsModified = true;
                    _context.Entry(nCR).Property("RepresentativesName").IsModified = true;
                    _context.Entry(nCR).Property("Date").IsModified = true;

                    await _context.SaveChangesAsync();
                   

                    // Add or update pictures
                    await AddPictures(nCR, pictures);

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

                Notification(nCR.ncrEmail, null, null);

                // Redirect to the index action
                return View("Notification", nCR);

            }

            return View(nCR);
        }

        [Authorize(Roles = "Admin,Engineer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EngineerEdit(int id, [Bind("ID,Status,UseAsIsId,CustomerYes,CustomerNO,Disposition," +
    "DrawingYes,DrawingNo,OriginalRev,UpdatedRev,NameOfEngineer,RevisingDate,Engineer,EngineerDate,ncrEmail")] NCR nCR)
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
                       
                        _context.Entry(nCR).Property("UseAsIsId").IsModified = true;
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
                    
                    _context.Attach(nCR);
                    // Your code for modifying NCR properties
                    nCR.Status = "Purchase";
                    // Modify other properties as needed
                    // ...

                    await _context.SaveChangesAsync();

                    // Send notification
                    Notification(nCR.ncrEmail, null, null);

                    return View("Notification", nCR);
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
            }

            // Handle ModelState errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(nCR);
        }



        [Authorize(Roles = "Admin,Purchase")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseEdit(int id, [Bind("ID,reviewyes,reviewno," +
             "PreliminaryDecision,CARYes,CARNo,FollowupYes,FollowupNo,PurchaseDate,OperatingManagerName,ncrEmail")] NCR nCR)
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
                    nCR.Status = "Review";
                    _context.Entry(nCR).Property("reviewyes").IsModified = true;
                    _context.Entry(nCR).Property("reviewno").IsModified = true;
                    _context.Entry(nCR).Property("PreliminaryDecision").IsModified = true;
                    _context.Entry(nCR).Property("CARYes").IsModified = true;
                    _context.Entry(nCR).Property("CARNo").IsModified = true;
                    _context.Entry(nCR).Property("FollowupYes").IsModified = true;
                    _context.Entry(nCR).Property("FollowupNo").IsModified = true;
                    _context.Entry(nCR).Property("PurchaseDate").IsModified = true;
                    _context.Entry(nCR).Property("OperatingManagerName").IsModified = true;
                    nCR.FollowupYes = Request.Form["FollowupYes"] == "true";

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

                Notification(nCR.ncrEmail, null, null);

                // Redirect to the index action
                return View("Notification", nCR);
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


        [Authorize(Roles = "Admin,Review")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewEdit(int id, [Bind("ID,reinyes,reino,ncrclosenyes,ncrcloseno,newNcrnno,InspectorName," +"Qualitydepartment",
             "finalDate,ncrEmail")] NCR nCR)
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
                    _context.Entry(nCR).Property("reinyes").IsModified = true;
                    _context.Entry(nCR).Property("reino").IsModified = true;
                    _context.Entry(nCR).Property("ncrclosenyes").IsModified = true;
                    _context.Entry(nCR).Property("ncrcloseno").IsModified = true;
                    _context.Entry(nCR).Property("Qualitydepartment").IsModified = true;
                    _context.Entry(nCR).Property("newNcrnno").IsModified = true;
                    _context.Entry(nCR).Property("InspectorName").IsModified = true;
                    _context.Entry(nCR).Property("finalDate").IsModified = true;
                    nCR.finalDate = DateTime.Today;

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

                Notification(nCR.ncrEmail, null, null);

                // Redirect to the index action
                return View("Notification", nCR);
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
        [Authorize]
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

        private async Task AddPictures(NCR nCR, List<IFormFile> pictures)
        {
            // Get the pictures and save them with the Customer (1 size)
            if (pictures != null && pictures.Any())
            {
                foreach (var picture in pictures)
                {
                    string mimeType = picture.ContentType;
                    long fileLength = picture.Length;

                    if (!(mimeType == "" || fileLength == 0)) // Looks like we have a file!!!
                    {
                        if (mimeType.Contains("image"))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await picture.CopyToAsync(memoryStream);
                                var pictureArray = memoryStream.ToArray(); // Gives us the Byte[]

                                // Create a new CustomerPhoto instance for each image
                                var nCRPhoto = new NCRPhoto
                                {
                                    Content = pictureArray,
                                    MimeType = mimeType
                                };

                                // Check if the CustomerImages list is null
                                if (nCR.NCRPhotos == null)
                                {
                                    // If it's null, create a new list and add the first image
                                    nCR.NCRPhotos = new List<NCRPhoto> { nCRPhoto };
                                }
                                else
                                {
                                    // If it's not null, add the image to the existing list
                                    nCR.NCRPhotos.Add(nCRPhoto);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
