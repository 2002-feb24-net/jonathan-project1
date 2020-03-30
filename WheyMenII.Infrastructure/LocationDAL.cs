using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

using WheyMenDAL.Library;
using WheyMenDAL.Library.Model;

namespace WheyMen.DAL
{
    public class LocationDAL : ILocationDAL
    {
        public IEnumerable<Loc> GetLocs()
        {
            using var context = new WheyMenContext();
            return context.Loc;
        }

        /// <summary>
        /// Adds a customer to database
        /// </summary>
        /// <param name="cust"></param>
        public void Add(Loc l)
        {
            using var context = new WheyMenContext();
            context.Loc.Add(l);
        }

        /// <summary>
        /// Sets location's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Loc l)
        {
            using var context = new WheyMenContext();
            context.Entry(l).State = EntityState.Modified;
        }
        public void UpdateInventory(int id, int qty)
        {
            using (var context = new WheyMenContext())
            {
                var to_update = context.Inventory.Find(id);
                to_update.Qty -= qty;
                context.SaveChanges();
            }
        }
        public List<Inventory> GetInventory(int id)
        {
            var listInventoryModel = new List<Inventory>();
            using (var context = new WheyMenContext())
            {
                listInventoryModel = context.Inventory
                                                .Include("P")
                                                .Where(i => i.StoreId == id)
                                                .ToList();

            }
            return listInventoryModel;
        }

        public List<Loc> GetList()
        {


            var listLocationModel = new List<Loc>();
            using (var context = new WheyMenContext())
            {
                listLocationModel = context.Loc.ToList();
            }

            return listLocationModel;
        }
    }
}