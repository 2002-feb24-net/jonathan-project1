using System.Collections.Generic;
using System.Threading.Tasks;
using WheyMen.Domain.Model;

namespace WheyMen.Domain
{
    public interface ICustomerDAL
    {
        /// <summary>
        /// Removes custs by id
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id);

        /// <summary>
        /// Searches custs by id, returns null if not foudn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer FindByID(int id);
      
        /// <summary>
        /// Returns ienumerable of custs
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Customer>> GetCusts();

        /// <summary>
        /// Adds a customer to database
        /// </summary>
        /// <param name="cust"></param>
        public int Add(Customer cust);

        /// <summary>
        /// Sets customer's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Customer cust);

        /// <summary>
        /// Searches customers by given parameter/search mode
        /// </summary>
        /// <param name="mode">Search mode: 1 - By name, 2 - By username</param>
        /// <param name="search_param">name/username to search by</param>
        /// <returns></returns>
        IEnumerable<Customer> SearchCust(int mode = 0,params string[] search_param);
       
        //Retrieves actual pwd of customer returns id of matching username
       /// <summary>
       /// Verifies that username exists, assigns id if it does
       /// </summary>
       /// <param name="username"></param>
       /// <param name="id">id holder for invoker</param>
       /// <returns>actual password of customer</returns>
        string VerifyCustomer(string username,out int id);
    }
}
