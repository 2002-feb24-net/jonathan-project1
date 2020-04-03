﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WheyMen.Domain;
using WheyMen.Domain.Model;
using Microsoft.Extensions.Logging;

namespace WheyMenII.UI
{
    public class CustomersController : Controller
    {
        private readonly ICustomerDAL _context;
        private readonly ILogger<CustomersController> logger;
        public CustomersController(ICustomerDAL cDAL,ILogger<CustomersController> logger)
        {
            _context = cDAL;
            this.logger = logger;
        }
        public IActionResult Search(string SearchFirstName, string SearchLastName)
        {
            logger.LogInformation($"Searching for cust {1} {2}", SearchFirstName, SearchLastName);
            return View(_context.SearchCust(1, SearchFirstName, SearchLastName));
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetCusts());
        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.FindByID(Convert.ToInt32(id));
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            if(TempData["CustAddError"]!=null && (bool)TempData["CustAddError"]==true)
            {
                ModelState.AddModelError("CustAddError", "Creating the customer failed change username/email.");
            }
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Email,Username,Pwd,LastName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(customer);
                }
                catch (Exception)
                {
                    TempData["CustAddError"] = true;
                    return View(customer);
                }
                logger.LogInformation($"Successfully created customer");
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.FindByID(Convert.ToInt32(id));
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Email,Username,Pwd,LastName")] Customer customer)
        {
           
            if (_context.FindByID(id).Username != customer.Username) //username cant be updated
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Edit(customer);
                }
                catch (Exception)
                {
                    logger.LogError("Customer edit failed");
                    TempData["EditFailed"] = true;
                    ModelState.AddModelError("EditFailed", "Updating the customer failed please try again later.");
                    return View(customer);
                }
                logger.LogInformation("Successfully edited customer");
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.FindByID(Convert.ToInt32(id));
            if (customer == null)
            {
                return NotFound();
            }
           

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            logger.LogInformation($"Successfully deleted customer {1}", id);
            _context.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
