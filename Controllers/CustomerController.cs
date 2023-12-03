using atelier3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace atelier3.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationdbContext _context;

        public CustomerController(ApplicationdbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.customers.Include(c => c.membershipType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.customers == null)
            {
                return NotFound();
            }

            var customer = await _context.customers
                .Include(c => c.membershipType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["membershipTypeId"] = new SelectList(_context.Set<MembershipType>(), "Id", "Id");
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,membershipTypeId")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            ViewData["membershipTypeId"] = new SelectList(_context.Set<MembershipType>(), "Id", "Id", customer.MembershipTypeId);
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.customers == null)
            {
                return NotFound();
            }

            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["membershipTypeId"] = new SelectList(_context.Set<MembershipType>(), "Id", "Id", customer.MembershipTypeId);
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,membershipTypeId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return Content("no customer found ! ");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return Content("no customer found ! ");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["membershipTypeId"] = new SelectList(_context.Set<MembershipType>(), "Id", "Id", customer.MembershipTypeId);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.customers == null)
            {
                return Content("no customer found ! ");
            }

            var customer = await _context.customers
                .Include(c => c.membershipType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return Content("no customer found ! ");
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.customers == null)
            {
                return Content("no customer !");
            }
            var customer = await _context.customers.FindAsync(id);
            if (customer != null)
            {
                _context.customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return (_context.customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
