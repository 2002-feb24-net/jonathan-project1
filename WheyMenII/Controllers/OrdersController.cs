using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WheyMen.DAL;
using WheyMenDAL.Library.Model;

namespace WheyMenII.UI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderDAL _context = new OrderDAL();

        // GET: Orders
        public IActionResult Index()
        {
            var wheyMenContext = _context.GetOrds();
            return View(wheyMenContext.ToList());
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.FindByID(Convert.ToInt32(id));
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustId"] = new SelectList(Customer, "Id", "Email");
            ViewData["LocId"] = new SelectList(Loc, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CustId,LocId,Total,Timestamp")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "Id", "Email", order.CustId);
            ViewData["LocId"] = new SelectList(_context.Loc, "Id", "Name", order.LocId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "Id", "Email", order.CustId);
            ViewData["LocId"] = new SelectList(_context.Loc, "Id", "Name", order.LocId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CustId,LocId,Total,Timestamp")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                     _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustId"] = new SelectList(_context.Customer, "Id", "Email", order.CustId);
            ViewData["LocId"] = new SelectList(_context.Loc, "Id", "Name", order.LocId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  _context.Order
                .Include(o => o.Cust)
                .Include(o => o.Loc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order =  _context.Order.FindAsync(id);
            _context.Order.Remove(order);
             _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
