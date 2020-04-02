using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheyMen.Domain.Model;
using WheyMen.Infrastructure;

namespace WheyMenII.Test
{
    [TestClass]
    public class ProductRepositoryTest
    {
        CustomerDAL Repo;
        [TestInitialize]
        public void TestSetup()
        {
            Repo = new CustomerDAL();
        }
        [TestMethod]
        public async Task TestCustAdd()
        {
            var custs = await Repo.GetCusts();
            int initial_count = custs.ToList().Count;
            Customer cust1 = new Customer
            {
                Name = "jon",
                LastName = "alt",
                Pwd = "abc",
                Email = "abc@def.com",
                Username = "jhbui3"
            };
            Customer cust2 = new Customer
            {
                Name = "jon",
                LastName = "alt",
                Pwd = "abc",
                Email = "abc@dasf.com",
                Username = "jhbui4"
            };
            Customer cust3 = new Customer
            {
                Name = "jon",
                LastName = "alt",
                Pwd = "abc",
                Email = "abasdc@def.com",
                Username = "jhbui5"
            };
            int x=Repo.Add(cust1);int y= Repo.Add(cust2);int z= Repo.Add(cust3);
            custs = await Repo.GetCusts();
            int final_count = custs.ToList().Count;
            Assert.IsTrue(final_count == (initial_count + 3));
            Repo.Remove(x);
            Repo.Remove(y);
            Repo.Remove(z);
        }
    }
}
