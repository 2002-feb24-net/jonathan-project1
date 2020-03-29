using System;
using System.Collections.Generic;

using WheyMenDAL.Library.Model;

namespace WheyMenDAL.Library
{
    public interface ICustomerDAL
    {
        /// <summary>
        /// Returns number of customers
        /// </summary>
        /// <returns></returns>
        int NumberOfCustomers();
        /// <summary>
        /// Checks if a given string is unique among customers
        /// </summary>
        /// <param name="mode">1: checks strings against all usernames, 2: against all emails</param>
        /// <param name="check">string to be checked</param>
        /// <returns>true if check is unique</returns>
        bool CheckUnique(int mode, string check);
        /// <summary>
        /// Adds customer to databse
        /// </summary>
        /// <param name="fn">First name</param>
        /// <param name="ln">Last name</param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="pwd">password</param>
        int AddCust(string fn, string ln, string username, string email, string pwd);
       
        /// <summary>
        /// Searches customers by given parameter/search mode
        /// </summary>
        /// <param name="mode">Search mode: 1 - By name, 2 - By username</param>
        /// <param name="search_param">name/username to search by</param>
        /// <returns></returns>
        Customer SearchCust(int mode = 0,params string[] search_param);
       
        //Retrieves actual pwd of customer returns id of matching username
       /// <summary>
       /// Verifies that username exists, assigns id if it does
       /// </summary>
       /// <param name="username"></param>
       /// <param name="id">id holder for invoker</param>
       /// <returns>actual password of customer</returns>
        string VerifyCustomer(string username,out int id);
        //returns -1 if customer name or id does not exist
        //returns the matching ID other wise
        /// <summary>
        /// Verifies if customer with given name or id exists
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns>id corresponding to customer matching search param or -1 if not exists</returns>
        int ValidateCustomer(int id = -1, params string[] name);
        /// <summary>
        /// Retrieves list of customers from db
        /// </summary>
        /// <returns></returns>
        List<Customer> GetList();
    }
}
