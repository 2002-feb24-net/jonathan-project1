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
        readonly WheyMenContext context = new WheyMenContext();
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
        public void Add(Customer cust)
        {
            context.Customer.Add(cust);
            context.SaveChanges();

        }

        /// <summary>
        /// Sets customer's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Customer cust)
        {
            context.Entry(cust).State = EntityState.Modified;
        }

        public int NumberOfCustomers()
        {
            return context.Customer.ToList().Count;

        }
        public bool CheckUnique(int mode, string check)
        {
            if (mode == 1)
            {
                if (context.Customer.Where(c => c.Username==check).ToList().Count==0)
                {
                    return true;
                }
            }
            else if (mode == 2)
            {
                if(context.Customer.Where(c=>c.Email == check).ToList().Count==0)
                {
                    return true;
                }
            }
            return false;
        }
        public int AddCust(string fn, string ln, string username, string email, string pwd)
        {
            var new_cust = new Customer
            {
                Name = fn,
                Email = email,
                Username = username,
                Pwd = pwd,
                LastName = ln
            };
            try
            {
                context.Customer.Add(new_cust);
                context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                Console.WriteLine("Email/username already exists");
            }
            return new_cust.Id;
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
        //returns -1 if customer name or id does not exist
        //returns the matching ID other wise
        public int ValidateCustomer(int id = -1, params string[] name)
        {
            var listCustomers = GetList();
            foreach (var cust in listCustomers)
            {
                if (id > 0 && cust.Id == id)
                {
                    return id;
                }
                else if (id<0 && cust.Name == name[0] && cust.LastName == name[1])
                {
                    return cust.Id;
                }
            }
            return -1;
        }

        public List<Customer> GetList()
        {
            var listCustomerModel = context.Customer.ToList();
            return listCustomerModel;
        }
    }
}