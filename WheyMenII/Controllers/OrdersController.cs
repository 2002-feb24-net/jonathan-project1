﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using WheyMen.Infrastructure;
using WheyMen.Domain;
using WheyMen.Domain.Model;
using System.Dynamic;
using WheyMenII.UI.Models;
using Newtonsoft.Json;

namespace WheyMenII.UI.Controllers
{
    public class OrdersController : Controller
    {

        private readonly IOrderDAL _context;
        private readonly ICustomerDAL _custContext;
        private readonly ILocationDAL _locContext;

        public OrdersController(IOrderDAL oDAL,ICustomerDAL cDAL, ILocationDAL lDAL)
        {
            _context = oDAL;
            _custContext = cDAL;
            _locContext = lDAL;
        }

        public async Task<IActionResult> SearchLocOrders(string locName)
        {
            return View("Index", await _context.GetOrders(1, locName));
        }
        public async Task<IActionResult> SearchCustOrders(string firstName, string lastName)
        {
            return View("Index", await _context.GetOrders(2,firstName,lastName));
        }
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var wheyMenContext = await _context.GetOrds();
            return View(wheyMenContext.ToList());
        }

        private CreateOrderViewModel InitCOVM(int storeID)
        {
            var inventoryItemModel = new CreateOrderViewModel
            {
                Inventory = _locContext.GetInventory(storeID)
            };
            ViewData["Pid"] = new SelectList(_locContext.GetInventory(storeID), "Id", "P.Name");
            return inventoryItemModel;
        }

        public IActionResult CreateOrderItem()
        {
            int storeID = Convert.ToInt32(TempData["StoreID"]);
            TempData["StoreID"] = storeID;
            
            return View(InitCOVM(storeID));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrderItem([Bind("Oid","Qty","Id","Pid")] OrderItem item)
        {
            if (ModelState.IsValid && item.ValidateQuantity( _locContext.GetQty(item.Pid)))
            {
                int qty = item.Qty;
                if(TempData["Item"]!=null)
                {
                    item = JsonConvert.DeserializeObject<OrderItem>((string)TempData["Item"]);
                }
                _locContext.UpdateInventory(item.Pid, qty);
                item.Oid = Convert.ToInt32(TempData["OrderID"]);
                _context.AddOrderItem(item);
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("QuantityError", "Invalid quantity entered, please try again");
            int storeID = Convert.ToInt32(TempData["StoreID"]);
            TempData["StoreID"] = storeID;
            TempData["Item"] = JsonConvert.SerializeObject(item);
            return View(InitCOVM(storeID));
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
        public async Task<IActionResult> Create()
        {
            IEnumerable<Customer> custsEnum = await _custContext.GetCusts();
            ViewData["CustId"] = new SelectList(await _custContext.GetCusts(), "Id", "Email");
            ViewData["LocId"] = new SelectList(_context.GetLocs(), "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CustId,LocId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Total = 0;
                order.Timestamp = DateTime.Now;
                TempData["OrderID"] = _context.Add(order);
                TempData["StoreID"] = order.LocId;
                return RedirectToAction("CreateOrderItem",new { store_id = 1});
            }
            
            return View();
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  _context.FindByID(Convert.ToInt32(id));
            if (order == null)
            {
                return NotFound();
            }
            IEnumerable<Customer> custEnum = await _custContext.GetCusts();
            ViewData["CustId"] = new SelectList(custEnum, "Id", "Email", order.CustId);
            ViewData["LocId"] = new SelectList(_context.GetLocs(), "Id", "Name", order.LocId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustId,LocId,Total,Timestamp")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Edit(order);
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
            IEnumerable<Customer> custEnum = await _custContext.GetCusts();
            ViewData["CustId"] = new SelectList(custEnum, "Id", "Email", order.CustId);
            ViewData["LocId"] = new SelectList(_context.GetLocs(), "Id", "Name", order.LocId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public IActionResult Delete(int? id)
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

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order =  _context.FindByID(id);
            _context.Remove(order.Id);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return !(_context.FindByID(id) == null);
        }
    }
}
