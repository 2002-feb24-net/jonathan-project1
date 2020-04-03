using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using WheyMen.Domain;
using WheyMen.Domain.Model;
using System.Threading.Tasks;

namespace WheyMen.Infrastructure
{
    public class OrderDAL : IOrderDAL
    {
        readonly WheyMenContext context;

        public OrderDAL()
        {
            context = new WheyMenContext();
        }
        
        public void RemoveOrderItem(OrderItem item)
        {
            context.OrderItem.Remove(item);
            context.SaveChanges();
        }

        public Order FindByID(int id)
        {
            var res = context.Order
                    .Include(o => o.Cust)
                    .Include(o => o.Loc)
                    .Include("OrderItem.P")
                    .Include("OrderItem.P.P")
                    .FirstOrDefault(m=>m.Id==id);
            context.Entry(res).Reload();
            return res;
        }

        public void Remove(int id)
        {
            try
            {
                var toRemove = context.Order.Include(x=>x.OrderItem).FirstOrDefault(x=>x.Id==id);
                context.OrderItem.RemoveRange(toRemove.OrderItem);
                context.Remove(toRemove);
                context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return;
            }
        }

        public IEnumerable<Loc> GetLocs()
        {
            return context.Loc;
        }

        public async Task<IEnumerable<Order>> GetOrds()
        {
            return await context.Order.Include(o => o.Cust).Include(o => o.Loc).ToListAsync();
        }

        /// <summary>
        /// Adds an order to database
        /// </summary>
        /// <param name="cust"></param>
        public int Add(Order o)
        {
            context.Order.Add(o);
            context.SaveChanges();
            context.Entry(o).Reload();
            return o.Id;
        }

        /// <summary>
        /// Sets order's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Order o)
        {
            context.Entry(o).State = EntityState.Modified;
            context.SaveChanges();
        }

        //Returns price of added item
        public void AddOrderItem(OrderItem item)
        {
            context.OrderItem.Add(item);
            context.SaveChanges();
        }

        //Searches orders by given param, param is checked against Order columns according to mode
        //Mode Codes:
        //  1: Get orders by location
        //  2: By customer
        //  3: Get details of 1 specific order
        public async Task<List<Order>> GetOrders(int mode=0, params string[] search_param)
        {
            var orderList = context.Order.Include("Loc").Include("Cust").AsQueryable();
            using(var context = new WheyMenContext())
            {
                switch (mode)
                {
                    case 1:
                        orderList = orderList
                        .Where(o => o.Loc.Name == search_param[0]);
                        break;
                    case 2:
                        orderList = orderList
                        .Where(o => o.Cust.Name == search_param[0] && o.Cust.LastName == search_param[1] );
                        break;
                    case 3:
                        orderList = orderList
                        .Where(o => o.Id == Convert.ToInt32(search_param[0]));
                        break;
                    default:
                        break;
                }
            }
            return await orderList
                        .Include("OrderItem")
                        .Include("OrderItem.P")
                        .Include("OrderItem.P.P")
                        .ToListAsync();
        }
    }
}
