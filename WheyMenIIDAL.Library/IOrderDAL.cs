using System;
using System.Collections.Generic;
using System.Text;

using WheyMen.DAL;
using WheyMenDAL.Library.Model;

namespace WheyMenDAL.Library
{
    public interface IOrderDAL
    {
        /// <summary>
        /// Calls getorders, list of orders searched for given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>-1 if order does not exist, order id otherwise</returns>
        int ValidateOrder(int id);
        /// <summary>
        /// Runs sp_create_ord and assisgns cid, lid params
        /// </summary>
        /// <param name="cid"> customer id</param>
        /// <param name="lid"> location id</param>
        /// <returns>id of created order</returns>
        int CreateOrder(int cid, int lid);
        /// <summary>
        /// Adds order item to database 
        /// </summary>
        /// <param name="oid">order id</param>
        /// <param name="pid">product id</param>
        /// <param name="qty">quantity </param>
        /// <returns>Price of added item</returns>
        Decimal AddOrderItem(int oid, int pid, int qty);

        /// <summary>
        /// Retrieves all orders from db by given search parameter and id
        /// </summary>
        /// <param name="search_param">location/order/customer id/name</param>
        /// <param name="mode">1: orders for a location, 2: orders for a given customer, 3: specific order, default: all orders
        /// </param>
        /// <returns></returns>
        List<Order> GetOrders(string search_param, int mode = 0);
    }
}
