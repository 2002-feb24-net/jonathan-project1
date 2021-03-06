﻿using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using WheyMen.Domain;
using WheyMen.Domain.Model;

namespace WheyMen.Infrastructure
{
    public class LocationDAL : ILocationDAL
    {
        private readonly WheyMenContext context;

        public LocationDAL(WheyMenContext cont)
        {
            context = cont;
        }
        public LocationDAL()
        {
            context = new WheyMenContext();
        }

        public IEnumerable<Loc> GetLocs()
        {
            return context.Loc.Include("P.P");
        }

        /// <summary>
        /// Adds a customer to database
        /// </summary>
        /// <param name="cust"></param>
        public void Add(Loc l)
        {
            context.Loc.Add(l);
        }

        /// <summary>
        /// Sets location's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Loc l)
        {
            context.Entry(l).State = EntityState.Modified;
        }

        public void UpdateInventory(int id, int qty)
        {
            var to_update = context.Inventory.Find(id);
            to_update.Qty -= qty;
            context.SaveChanges();
        }

        public int GetQty(int id)
        {
            return context.Inventory.Find(id).Qty;
        }

        public List<Inventory> GetInventory(int id)
        {
            var listInventoryModel = context.Inventory
                                            .Include("P")
                                            .Where(i => i.StoreId == id)
                                            .ToList();

            return listInventoryModel;
        }
    }
}