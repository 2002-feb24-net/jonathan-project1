using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheyMen.Domain;
using WheyMen.Domain.Model;

namespace WheyMen.Infrastructure
{
    public class CustomerDAL : ICustomerDAL
    {
        readonly WheyMenContext context;
        
        public CustomerDAL(WheyMenContext context)
        {
            this.context = context;
        }

        public CustomerDAL()
        {
            context = new WheyMenContext();
        }

        public void Remove(int id)
        {
            var toRemove = context.Customer.Find(id);
            context.Customer.Remove(toRemove);
            context.SaveChanges();
          
        }
        
        public Customer FindByID(int id)
        {
            return context.Customer.Find(id);
        }
        
        public async Task<IEnumerable<Customer>> GetCusts()
        {
            return await context.Customer.ToListAsync();
        }
        
        /// <summary>
        /// Adds a customer to database
        /// </summary>
        /// <param name="cust"></param>
        public int Add(Customer cust)
        {
            context.Customer.Add(cust);
            context.SaveChanges();
            context.Entry(cust).Reload();
            return cust.Id;
        }

        /// <summary>
        /// Sets customer's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Customer cust)
        {
            var old = FindByID(cust.Id);
            context.Entry(old).State = EntityState.Detached;
            context.Set<Customer>().Attach(cust);
            context.Entry(cust).State = EntityState.Modified;
            context.SaveChanges();
        }

        //Searches customers by either name or username
        //modes:
        //  1: Search by name
        //  2: username
        //  default: email
        public IEnumerable<Customer> SearchCust(int mode=0,params string[] search_param)
        {
            if (mode == 1)
            {
                return context.Customer.Where(cust => cust.Name == search_param[0] && cust.LastName == search_param[1]);
            }
            else if(mode == 2)
            {
                return context.Customer.Where(cust => cust.Username == search_param[0]);
            }
            else
            {
                return context.Customer.Where(cust => cust.Email == search_param[0]);   
            }
        }

        //assigns id of verified customer -1 if does not exist/invalid etc.
        //returns actual pwd of customer
        public string VerifyCustomer(string username,out int id)
        {
            var cust = context.Customer.FirstOrDefault(cust => cust.Username == username);
            if(cust==null)
            {
                id = -1;
            }
            else
            {
                id = cust.Id;
                return cust.Pwd;
            }
            return "";
            
        }
    }
}