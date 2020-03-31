using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Configuration;

using WheyMenDAL.Library;
using WheyMenDAL.Library.Model;


namespace WheyMen.DAL
{
    public class OrderDAL : IOrderDAL
    {
        readonly WheyMenContext context = new WheyMenContext();
        
        public Order FindByID(int id)
        {
            return context.Order
                    .Include(o => o.Cust)
                    .Include(o => o.Loc)
                    .Include("OrderItem.P")
                    .Include("OrderItem.P.P")
                    .FirstOrDefault(m=>m.Id==id);
        }

        public void Remove(int id)
        {
            var toRemove = context.Order.Find(id);
            context.Remove(toRemove);
            context.SaveChanges();
        }

        public IEnumerable<Loc> GetLocs()
        {
            return context.Loc;
        }

        public IEnumerable<Order> GetOrds()
        {
            return context.Order.Include(o => o.Cust).Include(o => o.Loc);
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

        public int ValidateOrder(int id)
        {
            var ordList = GetOrders("");
            foreach(var order in ordList)
            {
                if(id>0&&order.Id == id)
                {
                    return id;
                }
                
            }
            return -1;
        }

        public int CreateOrder(int cid, int lid)
        {
            var new_order = new Order
            {
                CustId = cid,
                LocId = lid,
                Total = 0,
                Timestamp = DateTime.Now,
            };
            context.Order.Add(new_order);
            context.SaveChanges();
            return new_order.Id;
        }

        //Searches orders by given param, param is checked against Order columns according to mode
        //Mode Codes:
        //  1: Get orders by location
        //  2: By customer
        //  3: Get details of 1 specific order
        public List<Order> GetOrders(string search_param,int mode=0)
        {
            
            var ordersList = new List<Order>();
            int id = int.Parse(search_param);
            using(var context = new WheyMenContext())
            {
                switch (mode)
                {
                    case 1:
                        ordersList = context.Order
                                        .Where(o => o.LocId == id)
                                        .Include("OrderItem")
                                        .Include("OrderItem.P")
                                        .Include("OrderItem.P.P")
                                        .Include("Loc")
                                        .Include("Cust")
                                        .ToList();
                            
                            
                        break;
                    case 2:
                        ordersList = context.Order
                                       .Where(o => o.CustId == id)
                                       .Include("OrderItem")
                                       .Include("OrderItem.P")
                                       .Include("OrderItem.P.P")
                                       .Include("Loc")
                                       .Include("Cust")
                                       .ToList();
                        break;
                    case 3:
                        ordersList = context.Order
                                       .Where(o => o.Id == id)
                                       .Include("OrderItem")
                                       .Include("OrderItem.P")
                                       .Include("OrderItem.P.P")
                                       .Include("Loc")
                                       .Include("Cust")
                                       .ToList();
                        break;
                    default:
                        ordersList = context.Order.Include("OrderItem")
                                       .Include("OrderItem.P")
                                       .Include("OrderItem.P.P")
                                       .Include("Loc")
                                       .Include("Cust")
                                       .ToList();
                        break;
                }
            }
            return ordersList;
        }
    }
}
