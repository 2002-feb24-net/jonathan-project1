using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using WheyMenDAL.Library;
using WheyMenDAL.Library.Model;

namespace WheyMen.DAL
{
    public class CustomerDAL : ICustomerDAL
    {
        public int NumberOfCustomers()
        {
            using(var context = new WheyMenContext())
            {
                return context.Customer.ToList().Count;
            }
            
        }
        public bool CheckUnique(int mode, string check)
        {
            using (var context = new WheyMenContext())
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
            using (var context = new WheyMenContext())
            {
                try
                {
                    context.Customer.Add(new_cust);
                    context.SaveChanges();
                }
                catch(DbUpdateException)
                {
                    Console.WriteLine("Email/username already exists");
                }
            }
            return new_cust.Id;
        }

        //Searches customers by either name or username
        //modes:
        //  1: Search by name
        //  2: username
        //  default: email
        public Customer SearchCust(int mode=0,params string[] search_param)
        {
            var CustList = GetList();
            foreach(var cust in CustList)
            {
                if (mode == 0)
                {
                    if ((cust.Name == search_param[0] && cust.LastName == search_param[1]))
                    {
                        return cust;
                    }
                }
                else if(mode ==1)
                {
                    if(cust.Username==search_param[0] || cust.Email == search_param[0])
                    {
                        return cust;
                    }
                }
                else
                {
                    if(cust.Email==search_param[0])
                    {
                        return cust;
                    }
                }
            }
            return null;
            
        }

        //assigns id of verified customer -1 if does not exist/invalid etc.
        //returns actual pwd of customer
        public string VerifyCustomer(string username,out int id)
        {
            var cust = SearchCust(1,username);
            if(cust==null)
            {
                id = -1;
            }
            else
            {
                id = cust.Id;
            }
            
            return cust.Pwd;
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
            using(var context = new WheyMenContext())
            {
                var listCustomerModel = context.Customer.ToList();
                return listCustomerModel;
            }
        }
    }
}