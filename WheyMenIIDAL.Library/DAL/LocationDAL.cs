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

        public LocationDAL(IConfiguration iconfiguration)
        {
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